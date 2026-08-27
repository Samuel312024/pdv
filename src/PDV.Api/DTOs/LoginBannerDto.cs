namespace PDV.Api.DTOs;

public record LoginBannerDto(Guid BannerId, string ImagemUrl, int Ordem, bool Ativo);
public record AtualizarLoginBannerRequest(int? Ordem, bool? Ativo);