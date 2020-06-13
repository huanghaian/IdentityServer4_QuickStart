// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> Apis
        {
            get
            {
                return new List<ApiResource>()
                {
                    new ApiResource("api1","My Api")
                };
            }
        }

        public static IEnumerable<Client> Clients
        {
            get
            {
                var clients = new List<Client>()
                {
                    new Client
                    {
                        ClientId="client",
                        AllowedGrantTypes = GrantTypes.ClientCredentials,
                        ClientSecrets=
                        {
                            new Secret("secret".Sha256())
                        },
                        AllowedScopes = { "api1"}
                    },
                    new Client
                    {
                        ClientId="mvc",
                        ClientSecrets = {new Secret("secret".Sha256()) },
                        AllowedGrantTypes = GrantTypes.Code,
                        RequireConsent = false,
                        RequirePkce = true,
                        RedirectUris = { "https://localhost:5002/signin-oidc" },
                        PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile
                        }
                    }
                };
                return clients; 
            }
        }
        
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId="1",
                    Username = "admin",
                    Password="123@abc",
                    Claims = new List<Claim>()
                    {
                        new Claim("name","admin"),
                        new Claim("SubjectId","1")
                    }
                },
                 new TestUser
                {
                    SubjectId="2",
                    Username = "root",
                    Password="123@abc",
                    Claims = new List<Claim>()
                    {
                        new Claim("name","root"),
                        new Claim("SubjectId","2")
                    }
                },
            };
        }
        
    }
}