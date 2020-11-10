using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ASPCoreWithKV.Pages
{
    public class KeyVaultSecretsModel : PageModel
    {
        public KeyVaultSecretsModel(IConfiguration config)
        {
            configuration = config;
        }
        private readonly IConfiguration configuration;
        public string SecretValue { get; set; }
        public string ConfigSetting { get; set; }

        public async Task OnGetAsync()
        {
            SecretValue = "Secret value is currently empty";

            // Get the secret from the preloaded config settings
            ConfigSetting = configuration["SomeConfigValueFromKV"];
            try
            {
                // grab the KeyVault Secret value on demand
                var keyVaultClient = new SecretClient(vaultUri: new Uri("https://cm-identity-kv.vault.azure.net"),
                                                                              credential: new ChainedTokenCredential(
                                                                                                        new AzureCliCredential(), 
                                                                                                        new ManagedIdentityCredential()));
                var secretOperation = await keyVaultClient.GetSecretAsync("KVSercret");
                var secret = secretOperation.Value;
                SecretValue = secret.Value;
            }
            catch (Exception keyVaultException)
            {
                SecretValue = keyVaultException.Message;
            }
        }
    }
}