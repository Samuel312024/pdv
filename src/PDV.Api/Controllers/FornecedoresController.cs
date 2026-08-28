using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDV.Api.Contracts.Fornecedores;
using PDV.Api.Data;
using PDV.Api.Domain;
using PDV.Api.Infrastructure;
using PDV.Api.Services;

namespace PDV.Api.Controllers;

[ApiController]
[Route("api/fornecedores")]
[Authorize]
public class FornecedoresController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly CurrentUserService _currentUser;

    public FornecedoresController(AppDbContext context, CurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    [HttpGet]
    public async Task<ActionResult> GetFornecedores(
        [FromQuery] string? search = null,
        [FromQuery] string? status = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 25)
    {
        var empresaId = _currentUser.EmpresaId; // ajuste o nome da propriedade se for diferente no seu CurrentUserService

        var query = _context.Fornecedores
            .AsNoTracking()
            .Where(f => f.EmpresaId == empresaId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var termo = search.Trim();
            query = query.Where(f =>
                f.Nome.Contains(termo) ||
                (f.NomeFantasia != null && f.NomeFantasia.Contains(termo)) ||
                (f.Documento != null && f.Documento.Contains(termo)) ||
                (f.Cidade != null && f.Cidade.Contains(termo)));
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            switch (status.ToUpperInvariant())
            {
                case "ATIVO":
                    query = query.Where(f => f.Ativo);
                    break;
                case "INATIVO":
                    query = query.Where(f => !f.Ativo);
                    break;
                case "PENDENTE":
                    // Ainda nao existe um criterio de "pendente" no dominio de Fornecedor.
                    // Por enquanto nenhum fornecedor cai nesse filtro; ajuste aqui quando
                    // definir a regra (ex: documento ausente, dados bancarios incompletos etc.).
                    query = query.Where(f => false);
                    break;
            }
        }

        var total = await query.CountAsync();

        var items = await query
            .OrderBy(f => f.Nome)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(f => new
            {
                id = f.FornecedorId,
                razaoSocial = f.Nome,
                nomeFantasia = f.NomeFantasia,
                documento = f.Documento,
                cidade = f.Cidade,
                uf = f.Uf,
                contato = f.ContatoComercial ?? f.Responsavel,
                email = f.EmailComercial ?? f.Email,
                telefone = f.Telefone ?? f.Celular,
                categoria = (string?)null,
                status = f.Ativo ? "ATIVO" : "INATIVO",
                ultimaCompraEm = (DateTime?)null,
                comprasNoPeriodo = (int?)null,
                valorComprasPeriodo = (decimal?)null,
            })
            .ToListAsync();

        var totalCadastrados = await _context.Fornecedores
            .AsNoTracking()
            .CountAsync(f => f.EmpresaId == empresaId);

        var totalAtivos = await _context.Fornecedores
            .AsNoTracking()
            .CountAsync(f => f.EmpresaId == empresaId && f.Ativo);

        return Ok(new
        {
            items,
            total,
            summary = new
            {
                total = totalCadastrados,
                ativos = totalAtivos,
                pendentes = 0, // sem regra de negocio definida ainda
                inativos = totalCadastrados - totalAtivos
            }
        });
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Fornecedor>> GetFornecedor(Guid id)
    {
        var fornecedor = await _context.Fornecedores
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FornecedorId == id && f.EmpresaId == _currentUser.EmpresaId);

        if (fornecedor is null)
        {
            return NotFound(new { message = "Fornecedor não encontrado." });
        }

        return Ok(fornecedor);
    }

    [HttpPost]
    public async Task<ActionResult<Fornecedor>> CriarFornecedor([FromBody] FornecedorRequest dados)
    {
        var erros = ValidarFornecedor(dados);
        if (erros.Count > 0)
        {
            return BadRequest(new { message = "Existem campos obrigatórios ou inválidos.", errors = erros });
        }

        var fornecedor = new Fornecedor
        {
            EmpresaId = _currentUser.EmpresaId,
            Nome = dados.RazaoSocial!.Trim(),
            Documento = dados.Cnpj!.Trim()
        };

        _context.Fornecedores.Add(fornecedor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFornecedor), new { id = fornecedor.FornecedorId }, fornecedor);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarFornecedor(Guid id, [FromBody] FornecedorRequest dados)
    {
        var fornecedor = await _context.Fornecedores
            .FirstOrDefaultAsync(f => f.FornecedorId == id && f.EmpresaId == _currentUser.EmpresaId);

        if (fornecedor is null)
        {
            return NotFound(new { message = "Fornecedor não encontrado." });
        }

        var erros = ValidarFornecedor(dados);
        if (erros.Count > 0)
        {
            return BadRequest(new { message = "Existem campos obrigatórios ou inválidos.", errors = erros });
        }

        fornecedor.Nome = dados.RazaoSocial!.Trim();
        fornecedor.Documento = dados.Cnpj!.Trim();
        fornecedor.DataAtualizacao = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok(fornecedor);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> ExcluirFornecedor(Guid id)
    {
        var fornecedor = await _context.Fornecedores
            .FirstOrDefaultAsync(f => f.FornecedorId == id && f.EmpresaId == _currentUser.EmpresaId);

        if (fornecedor is null)
        {
            return NotFound(new { message = "Fornecedor não encontrado." });
        }

        _context.Fornecedores.Remove(fornecedor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private static Dictionary<string, string[]> ValidarFornecedor(FornecedorRequest dados)
    {
        var erros = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(dados.RazaoSocial))
        {
            erros["razaoSocial"] = new[] { "A razão social é obrigatória." };
        }

        if (string.IsNullOrWhiteSpace(dados.Cnpj))
        {
            erros["cnpj"] = new[] { "O CPF/CNPJ é obrigatório." };
        }

        if (!string.IsNullOrWhiteSpace(dados.Email))
        {
            var emailValido = new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(dados.Email);
            if (!emailValido)
            {
                erros["email"] = new[] { "Informe um e-mail válido." };
            }
        }

        return erros;
    }
}