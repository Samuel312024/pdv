namespace PDV.Api.Infrastructure;

public class SupabaseStorageOptions
{
    public string Url { get; set; } = string.Empty;
    public string ServiceRoleKey { get; set; } = string.Empty;
    public string BucketName { get; set; } = "uploads";
}