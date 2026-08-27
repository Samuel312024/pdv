using Microsoft.EntityFrameworkCore;
using PDV.Api.Data;
using PDV.Api.Domain;
using PDV.Api.DTOs;

namespace PDV.Api.Services;

public class LoginBannerService(AppDbContext db, IWebHostEnvironment env)
{
    private const string PastaUploads = "uploads/login-banners";

    public async Task<IReadOnlyCollection<LoginBannerDto>> GetAllAsync()
    {
        var banners = await db.LoginBanners.OrderBy(b => b.Ordem).ToListAsync();
        return banners.Select(ParaDto).ToList();
    }

    public async Task<IReadOnlyCollection<LoginBannerDto>> GetPublicAsync()
    {
        var banners = await db.LoginBanners
            .Where(b => b.Ativo)
            .OrderBy(b => b.Ordem)
            .ToListAsync();
        return banners.Select(ParaDto).ToList();
    }

    // retorna null em caso de validacao invalida - o controller decide o status code
    public async Task<LoginBannerDto?> CreateAsync(IFormFile? imagem)
    {
        if (imagem is null || imagem.Length == 0) return null;
        if (!imagem.ContentType.StartsWith("image/")) return null;

        var pastaFisica = Path.Combine(env.ContentRootPath, PastaUploads);
        Directory.CreateDirectory(pastaFisica);

        var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(imagem.FileName)}";
        var caminhoFisico = Path.Combine(pastaFisica, nomeArquivo);

        await using (var stream = new FileStream(caminhoFisico, FileMode.Create))
        {
            await imagem.CopyToAsync(stream);
        }

        var proximaOrdem = await db.LoginBanners.AnyAsync()
            ? await db.LoginBanners.MaxAsync(b => b.Ordem) + 1
            : 0;

        var banner = new LoginBanner
        {
            ImagemCaminho = $"{PastaUploads}/{nomeArquivo}",
            ImagemUrl = $"/{PastaUploads}/{nomeArquivo}",
            Ordem = proximaOrdem,
            Ativo = true
        };

        db.LoginBanners.Add(banner);
        await db.SaveChangesAsync();
        return ParaDto(banner);
    }

    // retorna null quando o banner nao existe
    public async Task<LoginBannerDto?> UpdateAsync(Guid bannerId, AtualizarLoginBannerRequest request)
    {
        var banner = await db.LoginBanners.FindAsync(bannerId);
        if (banner is null) return null;

        if (request.Ordem.HasValue) banner.Ordem = request.Ordem.Value;
        if (request.Ativo.HasValue) banner.Ativo = request.Ativo.Value;

        await db.SaveChangesAsync();
        return ParaDto(banner);
    }

    // retorna false quando o banner nao existe
    public async Task<bool> DeleteAsync(Guid bannerId)
    {
        var banner = await db.LoginBanners.FindAsync(bannerId);
        if (banner is null) return false;

        var caminhoFisico = Path.Combine(env.ContentRootPath, banner.ImagemCaminho);
        if (File.Exists(caminhoFisico))
            File.Delete(caminhoFisico);

        db.LoginBanners.Remove(banner);
        await db.SaveChangesAsync();
        return true;
    }

    private static LoginBannerDto ParaDto(LoginBanner b) => new(b.BannerId, b.ImagemUrl, b.Ordem, b.Ativo);
}