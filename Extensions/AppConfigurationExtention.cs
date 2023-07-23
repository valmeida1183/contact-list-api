using System.Text.Json.Serialization;
using ContactListApi.Data;
using Microsoft.AspNetCore.ResponseCompression;

namespace ContactListApi.Extensions;

public static class AppConfigurationExtension
{
    public static void ConfigureWebApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });

        builder.Services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Optimal;
            });

        builder
            .Services
            .AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                // Configura para o ModelBindig não validar a model automaticamente, pois o default é validar mas eu quero um retorno personalizado na api
                options.SuppressModelStateInvalidFilter = true;
            })
            .AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // remove o problema das referencias dem ciclos
                x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; // Remove objetos filhos quando nullos
            });
    }

    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>();
    }
}