using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace OcelotIdentityServer
{
    public class SSOConfig
    {
        /// <summary>
        /// 获取API的资源文件
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources(IConfigurationSection section)
        {
            List<ApiResource> resources = new List<ApiResource>();
            if (section != null)
            {
                List<ApiConfig> configs = new List<ApiConfig>();
                section.Bind("ApiResources", configs);
                foreach (var config in configs)
                {
                    resources.Add(new ApiResource(config.Name, config.DisplayName));
                }
            }
            return resources;
        }

        public static IEnumerable<Client> GetClients(IConfigurationSection section)
        {
            List<Client> clients = new List<Client>();

            if (section != null)
            {
                List<ClientConfig> configs = new List<ClientConfig>();
                section.Bind("Clients", configs);
                foreach (var config in configs)
                {
                    Client client = new Client();
                    client.ClientId = config.ClientId;
                    List<Secret> secrets = new List<Secret>();
                    foreach (var secret in config.ClientSecrets)
                    {
                        secrets.Add(new Secret(secret.Sha256()));
                    }
                    client.ClientSecrets = secrets.ToArray();

                    var grantTypes = new GrantTypes();
                    var allowedGrantTypes = grantTypes.GetType().GetProperty(config.AllowedGrantTypes);
                    client.AllowedGrantTypes = allowedGrantTypes == null ? GrantTypes.ClientCredentials : (ICollection<string>)allowedGrantTypes.GetValue(grantTypes, null);

                    client.AllowedScopes = config.AllowedScopes.ToArray();
                    clients.Add(client);
                }
            }
            return clients.ToArray();
        }
    }

    public class ApiConfig
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }
    }

    public class ClientConfig
    {
        public string ClientId { get; set; }
        public List<string> ClientSecrets { get; set; }
        public string AllowedGrantTypes { get; set; }
        public List<string> AllowedScopes { get; set; }
    }
}
