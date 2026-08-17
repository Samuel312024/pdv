using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Api.Common;
using PDV.Api.DTOs;

namespace PDV.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/pdv/version")]
public class PdvVersaoController(IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiResponse<PdvVersaoDto>> GetVersion()
    {
        var version =
            configuration["Pdv:Version"]
            ?? "0.1.0";

        var downloadUrl =
            configuration["Pdv:DownloadUrl"]
            ?? (Url.ActionLink(
                "Download",
                "Instalador",
                values: null)
                ?? "/api/instalador/pdv/download");

        var mandatory =
            bool.TryParse(configuration["Pdv:Mandatory"], out var parsedMandatory)
                && parsedMandatory;

        var result = new PdvVersaoDto(
            version,
            downloadUrl,
            mandatory);

        return Ok(ApiResponse<PdvVersaoDto>.Ok(result));
    }
}