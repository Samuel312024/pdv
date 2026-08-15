using PDV.Api.DTOs;
using PDV.Api.Infrastructure;

namespace PDV.Api.Services;

public class InstaladorPdvService(
    IHostEnvironment environment,
    IConfiguration configuration)
{
    public const string NomeArquivoInstalador = "PDV-Control-Hub-Setup.exe";

    public string DownloadsRootPath => AppDataPathResolver.ResolveDownloadsRootPath(environment, configuration);

    public string? ResolveInstallerPath()
    {
        var candidatePaths = new[]
        {
        // Caminho publicado junto com a API
        Path.Combine(environment.ContentRootPath, "app-data", "downloads", NomeArquivoInstalador),

        // Caminho configurado externamente
        Path.Combine(DownloadsRootPath, NomeArquivoInstalador),

        // Caminho antigo de desenvolvimento
        Path.Combine(environment.ContentRootPath, ".app-data","downloads", NomeArquivoInstalador)
    };

        return candidatePaths.FirstOrDefault(File.Exists);
    }

    public InstaladorPdvStatusDto GetStatus(string downloadUrl)
    {
        var filePath = ResolveInstallerPath();
        if (filePath is null)
        {
            return new InstaladorPdvStatusDto(false, NomeArquivoInstalador, null, null, downloadUrl);
        }

        var fileInfo = new FileInfo(filePath);
        return new InstaladorPdvStatusDto(
            true,
            fileInfo.Name,
            fileInfo.Length,
            fileInfo.LastWriteTimeUtc,
            downloadUrl);
    }
}
