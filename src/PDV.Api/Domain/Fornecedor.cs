namespace PDV.Api.Domain;

public class Fornecedor
{
    public Guid FornecedorId { get; set; }


    public Guid EmpresaId { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string? NomeFantasia { get; set; }

    public string? Documento { get; set; }

    public string? TipoPessoa { get; set; }

    public string? Codigo { get; set; }

    public bool Ativo { get; set; } = true;


    public string? InscricaoEstadual { get; set; }

    public bool InscricaoEstadualIsento { get; set; }

    public string? InscricaoMunicipal { get; set; }

    public string? CnaePrincipal { get; set; }

    public string? RegimeTributario { get; set; }

    public string? ObservacaoFiscal { get; set; }


   
    public string? Cep { get; set; }

    public string? Logradouro { get; set; }

    public string? Numero { get; set; }

    public string? Complemento { get; set; }

    public string? Bairro { get; set; }

    public string? Cidade { get; set; }

    public string? Uf { get; set; }

    public string? CodigoMunicipioIbge { get; set; }



    public string? Telefone { get; set; }

    public string? Telefone2 { get; set; }

    public string? Celular { get; set; }

    public string? Email { get; set; }

    public string? EmailFinanceiro { get; set; }

    public string? Responsavel { get; set; }


  

    public string? ContatoComercial { get; set; }

    public string? EmailComercial { get; set; }

    public string? PrazoPagamento { get; set; }

    public string? CondicaoPagamento { get; set; }

    public decimal? LimiteCredito { get; set; }

    public string? VendedorResponsavel { get; set; }

    public string? ObservacaoComercial { get; set; }




    public string? Banco { get; set; }

    public string? Agencia { get; set; }

    public string? Conta { get; set; }

    public string? TipoConta { get; set; }

    public string? Pix { get; set; }

    public string? TitularConta { get; set; }

    public string? DocumentoTitularConta { get; set; }

    public string? ObservacaoFinanceira { get; set; }



    public string? Observacoes { get; set; }



    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    public DateTime? DataAtualizacao { get; set; }
}