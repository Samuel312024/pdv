using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDV.Api.Authorization;
using PDV.Api.Common;
using PDV.Api.Domain;
using PDV.Api.DTOs;
using PDV.Api.Services;
using PDV.Api.Domain;

namespace PDV.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/configuracoes/login-banners")]
public class LoginBannersController(LoginBannerService loginBannerService) : ControllerBase
{
    [HttpGet]
    [RequirePermission(Permissoes.GerenciarConfiguracoes)]
    public async Task<ActionResult<ApiResponse<IReadOnlyCollection<LoginBannerDto>>>> GetAll()
    {
        var response = await loginBannerService.GetAllAsync();
        return Ok(ApiResponse<IReadOnlyCollection<LoginBannerDto>>.Ok(response));
    }

    [HttpPost]
    [RequirePermission(Permissoes.GerenciarConfiguracoes)]
    [RequestSizeLimit(5 * 1024 * 1024)]
    public async Task<ActionResult<ApiResponse<LoginBannerDto>>> Create(IFormFile imagem)
    {
        var response = await loginBannerService.CreateAsync(imagem);
        if (response is null)
            return BadRequest(ApiResponse<LoginBannerDto>.Fail("Envie um arquivo de imagem valido (ate 5MB).")); // ajuste ApiResponse.Fail se o nome do metodo for outro

        return Ok(ApiResponse<LoginBannerDto>.Ok(response, "Banner adicionado com sucesso."));
    }

    [HttpPut("{bannerId:guid}")]
    [RequirePermission(Permissoes.GerenciarConfiguracoes)]
    public async Task<ActionResult<ApiResponse<LoginBannerDto>>> Update(Guid bannerId, [FromBody] AtualizarLoginBannerRequest request)
    {
        var response = await loginBannerService.UpdateAsync(bannerId, request);
        if (response is null)
            return NotFound(ApiResponse<LoginBannerDto>.Fail("Banner nao encontrado."));

        return Ok(ApiResponse<LoginBannerDto>.Ok(response, "Banner atualizado com sucesso."));
    }

    [HttpDelete("{bannerId:guid}")]
    [RequirePermission(Permissoes.GerenciarConfiguracoes)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid bannerId)
    {
        var removido = await loginBannerService.DeleteAsync(bannerId);
        if (!removido)
            return NotFound(ApiResponse<object>.Fail("Banner nao encontrado."));

        return Ok(ApiResponse<object>.Ok(null, "Banner removido com sucesso."));
    }
}

[ApiController]
[AllowAnonymous]
[Route("api/login-banners")]
public class LoginBannersPublicController(LoginBannerService loginBannerService) : ControllerBase
{
    [HttpGet("public")]
    public async Task<ActionResult<ApiResponse<IReadOnlyCollection<LoginBannerDto>>>> GetPublic()
    {
        var response = await loginBannerService.GetPublicAsync();
        return Ok(ApiResponse<IReadOnlyCollection<LoginBannerDto>>.Ok(response));
    }
}