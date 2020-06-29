using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
        
           
                CreateHostBuilder(args).Build().Run();
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddAzureWebAppDiagnostics();
                    })
                    //.ConfigureAppConfiguration(
                    //    (ctx,builder)=>

                    //    {
                    //        //Build the config from sources we have
                    //        var config = builder.Build();
                    //        //Create Managed Service Identity token provider
                    //        var tokenProvider = new AzureServiceTokenProvider();
                    //        //Create the Key Vault client
                    //        var kvClient = new KeyVaultClient((authority, resource, scope) => tokenProvider.KeyVaultTokenCallback(authority, resource, scope));
                    //        //Add Key Vault to configuration pipeline
                    //        builder.AddAzureKeyVault(config["KeyVault:BaseUrl"], kvClient, new DefaultKeyVaultSecretManager());
                    //    }
                    //    ); 
                });
    }
}
