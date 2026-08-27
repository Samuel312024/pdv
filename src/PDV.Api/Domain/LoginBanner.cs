namespace PDV.Api.Domain;

public class LoginBanner
{
    public Guid BannerId { get; set; } = Guid.NewGuid();
    public string ImagemUrl { get; set; } = string.Empty;     // caminho público servido pro front (ex: /uploads/login-banners/xxx.jpg)
    public string ImagemCaminho { get; set; } = string.Empty; // caminho físico relativo, usado só no backend pra deletar o arquivo
    public int Ordem { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}