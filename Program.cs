using System;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ASPCoreWithKV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                        var builtConfig = config.Build();
                        config.AddAzureKeyVault( new Uri("https://cm-identity-kv.vault.azure.net"),
                            //new DefaultAzureCredential());
                            new ChainedTokenCredential(
                                new AzureCliCredential(),
                                new ManagedIdentityCredential()
                                ));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}