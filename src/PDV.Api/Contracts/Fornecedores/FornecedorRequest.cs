namespace PDV.Api.Contracts.Fornecedores;

public class FornecedorRequest
{
    public string? RazaoSocial { get; set; }
    public string? NomeFantasia { get; set; }

    public string? Cnpj { get; set; }
    public string? TipoPessoa { get; set; }

    public string? InscricaoEstadual { get; set; }
    public string? InscricaoMunicipal { get; set; }

    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? Contato { get; set; }

    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Uf { get; set; }
    public string? CodigoIbge { get; set; }
    public string? Pais { get; set; }

    public string? Observacoes { get; set; }

    public string? Status { get; set; }
    public DateTime? FornecedorDesde { get; set; }

    public bool Bloqueado { get; set; }
}