namespace PDV.Api.DTOs;

public sealed record PdvVersaoDto(
    string Version,
    string DownloadUrl,
    bool Mandatory);