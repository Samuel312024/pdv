using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDV.Api.Contracts.Fornecedores;
using PDV.Api.Data;
using PDV.Api.Domain;

namespace PDV.Api.Controllers;

[ApiController]
[Route("api/fornecedores")]
[Authorize]
public class FornecedoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public FornecedoresController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/fornecedores
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Fornecedor>>> GetFornecedores(
        [FromQuery] string? busca = null)
    {
        var query = _context.Fornecedores
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            busca = busca.Trim();

            query = query.Where(f =>
                f.Nome.Contains(busca) ||
                (f.Documento != null && f.Documento.Contains(busca)));
        }

        var fornecedores = await query
            .OrderBy(f => f.Nome)
            .ToListAsync();

        return Ok(fornecedores);
    }

    // GET: api/fornecedores/{id}
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<Fornecedor>> GetFornecedor(Guid id)
    {
        var fornecedor = await _context.Fornecedores
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FornecedorId == id);

        if (fornecedor is null)
        {
            return NotFound(new
            {
                message = "Fornecedor não encontrado."
            });
        }

        return Ok(fornecedor);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Fornecedor>> CriarFornecedor([FromBody] FornecedorRequest dados)
    {
        var erros = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(dados.RazaoSocial))
        {
            erros["razaoSocial"] = new[]
            {
            "A razão social é obrigatória."
        };
        }

        if (string.IsNullOrWhiteSpace(dados.Cnpj))
        {
            erros["cnpj"] = new[]
            {
            "O CPF/CNPJ é obrigatório."
        };
        }

        if (!string.IsNullOrWhiteSpace(dados.Email))
        {
            var emailValido = new System.ComponentModel.DataAnnotations.EmailAddressAttribute()
                .IsValid(dados.Email);

            if (!emailValido)
            {
                erros["email"] = new[]
                {
                "Informe um e-mail válido."
            };
            }
        }

        if (erros.Count > 0)
        {
            return BadRequest(new
            {
                message = "Existem campos obrigatórios ou inválidos.",
                errors = erros
            });
        }

        var fornecedor = new Fornecedor
        {
            Nome = dados.RazaoSocial!.Trim(),
            Documento = dados.Cnpj!.Trim()
        };

        _context.Fornecedores.Add(fornecedor);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetFornecedor),
            new { id = fornecedor.FornecedorId },
            fornecedor);
    }

    [HttpPut("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> AtualizarFornecedor(Guid id, [FromBody] FornecedorRequest dados)
    {
        var fornecedor = await _context.Fornecedores
            .FirstOrDefaultAsync(f => f.FornecedorId == id);

        if (fornecedor is null)
        {
            return NotFound(new
            {
                message = "Fornecedor não encontrado."
            });
        }

        var erros = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(dados.RazaoSocial))
        {
            erros["razaoSocial"] = new[]
            {
            "A razão social é obrigatória."
        };
        }

        if (string.IsNullOrWhiteSpace(dados.Cnpj))
        {
            erros["cnpj"] = new[]
            {
            "O CPF/CNPJ é obrigatório."
        };
        }

        if (!string.IsNullOrWhiteSpace(dados.Email))
        {
            var emailValido = new System.ComponentModel.DataAnnotations.EmailAddressAttribute()
                .IsValid(dados.Email);

            if (!emailValido)
            {
                erros["email"] = new[]
                {
                "Informe um e-mail válido."
            };
            }
        }

        if (erros.Count > 0)
        {
            return BadRequest(new
            {
                message = "Existem campos obrigatórios ou inválidos.",
                errors = erros
            });
        }

        fornecedor.Nome = dados.RazaoSocial!.Trim();
        fornecedor.Documento = dados.Cnpj!.Trim();

        await _context.SaveChangesAsync();

        return Ok(fornecedor);
    }

    // DELETE: api/fornecedores/{id}
    [HttpDelete("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> ExcluirFornecedor(Guid id)
    {
        var fornecedor = await _context.Fornecedores
            .FirstOrDefaultAsync(f => f.FornecedorId == id);

        if (fornecedor is null)
        {
            return NotFound(new
            {
                message = "Fornecedor não encontrado."
            });
        }

        _context.Fornecedores.Remove(fornecedor);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}