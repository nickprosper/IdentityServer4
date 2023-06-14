// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using IdentityModel;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Storage.Mappers;

namespace IdentityServer4.EntityFramework.Mappers
{

    internal class ClientMappersInternal
    {
        internal Models.Client ToModel(Client entity)
        {
            if (entity == null)
                return default;
            var target = new Models.Client();
            if (entity.ClientSecrets != null)
            {
                target.ClientSecrets = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(entity.ClientSecrets, x => MapToSecret(x)));
            }

            if (entity.AllowedGrantTypes != null)
            {
                target.AllowedGrantTypes = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(entity.AllowedGrantTypes, x => x == null ? default : x.GrantType));
            }

            if (entity.RedirectUris != null)
            {
                target.RedirectUris = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(entity.RedirectUris, x => x == null ? default : x.RedirectUri));
            }

            if (entity.PostLogoutRedirectUris != null)
            {
                target.PostLogoutRedirectUris = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(entity.PostLogoutRedirectUris, x => x == null ? default : x.PostLogoutRedirectUri));
            }

            if (entity.AllowedScopes != null)
            {
                target.AllowedScopes = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(entity.AllowedScopes, x => x == null ? default : x.Scope));
            }

            if (entity.AllowedIdentityTokenSigningAlgorithms != null)
            {
                target.AllowedIdentityTokenSigningAlgorithms = SharedInternalMappers.MapAllowedSigningAlgorithms(entity.AllowedIdentityTokenSigningAlgorithms);
            }

            if (entity.IdentityProviderRestrictions != null)
            {
                target.IdentityProviderRestrictions = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(entity.IdentityProviderRestrictions, x => x == null ? default : x.Provider));
            }

            if (entity.Claims != null)
            {
                target.Claims = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(entity.Claims, x => MapToClientClaim(x)));
            }

            if (entity.AllowedCorsOrigins != null)
            {
                target.AllowedCorsOrigins = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(entity.AllowedCorsOrigins, x => x == null ? default : x.Origin));
            }

            target.Enabled = entity.Enabled;
            target.ClientId = entity.ClientId;
            target.ProtocolType = entity.ProtocolType;
            target.RequireClientSecret = entity.RequireClientSecret;
            target.ClientName = entity.ClientName;
            target.Description = entity.Description;
            target.ClientUri = entity.ClientUri;
            target.LogoUri = entity.LogoUri;
            target.RequireConsent = entity.RequireConsent;
            target.AllowRememberConsent = entity.AllowRememberConsent;
            target.RequirePkce = entity.RequirePkce;
            target.AllowPlainTextPkce = entity.AllowPlainTextPkce;
            target.RequireRequestObject = entity.RequireRequestObject;
            target.AllowAccessTokensViaBrowser = entity.AllowAccessTokensViaBrowser;
            target.FrontChannelLogoutUri = entity.FrontChannelLogoutUri;
            target.FrontChannelLogoutSessionRequired = entity.FrontChannelLogoutSessionRequired;
            target.BackChannelLogoutUri = entity.BackChannelLogoutUri;
            target.BackChannelLogoutSessionRequired = entity.BackChannelLogoutSessionRequired;
            target.AllowOfflineAccess = entity.AllowOfflineAccess;
            target.AlwaysIncludeUserClaimsInIdToken = entity.AlwaysIncludeUserClaimsInIdToken;
            target.IdentityTokenLifetime = entity.IdentityTokenLifetime;
            target.AccessTokenLifetime = entity.AccessTokenLifetime;
            target.AuthorizationCodeLifetime = entity.AuthorizationCodeLifetime;
            target.AbsoluteRefreshTokenLifetime = entity.AbsoluteRefreshTokenLifetime;
            target.SlidingRefreshTokenLifetime = entity.SlidingRefreshTokenLifetime;
            target.ConsentLifetime = entity.ConsentLifetime;
            target.RefreshTokenUsage = (Models.TokenUsage) entity.RefreshTokenUsage;
            target.UpdateAccessTokenClaimsOnRefresh = entity.UpdateAccessTokenClaimsOnRefresh;
            target.RefreshTokenExpiration = (Models.TokenExpiration) entity.RefreshTokenExpiration;
            target.AccessTokenType = (Models.AccessTokenType) entity.AccessTokenType;
            target.EnableLocalLogin = entity.EnableLocalLogin;
            target.IncludeJwtId = entity.IncludeJwtId;
            target.AlwaysSendClientClaims = entity.AlwaysSendClientClaims;
            target.ClientClaimsPrefix = entity.ClientClaimsPrefix;
            target.PairWiseSubjectSalt = entity.PairWiseSubjectSalt;
            target.UserSsoLifetime = entity.UserSsoLifetime;
            target.UserCodeType = entity.UserCodeType;
            target.DeviceCodeLifetime = entity.DeviceCodeLifetime;

            if (entity.Properties != null)
            {
                target.Properties = ClientPropertiesToDictionary(entity.Properties);
            }

            return target;
        }

        private global::IdentityServer4.Models.Secret MapToSecret(ClientSecret source)
        {
            if (source == null)
                return default;
            var target = new global::IdentityServer4.Models.Secret();
            target.Description = source.Description;
            target.Value = source.Value;
            target.Expiration = source.Expiration;
            target.Type = source.Type;
            return target;
        }

        private global::IdentityServer4.Models.ClientClaim MapToClientClaim(ClientClaim source)
        {
            if (source == null)
                return default;
            var target = new global::IdentityServer4.Models.ClientClaim();
            target.Type = source.Type;
            target.Value = source.Value;
            return target;
        }


        //internal partial Entities.Client ToEntity(Models.Client entity);

        IDictionary<string, string> ClientPropertiesToDictionary(List<ClientProperty> clientProperty)
        {
            var dict = new Dictionary<string, string>();

            foreach (var property in clientProperty)
            {
                dict.Add(property.Key, property.Value);
            }

            return dict;
        }


        internal Client ToEntity(Models.Client model)
        {
            if (model == null)
                return default;
            var target = new Client();

            if (model.ClientSecrets != null)
            {
                target.ClientSecrets = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(model.ClientSecrets, x => MapToClientSecret(x)));
            }

            if (model.AllowedGrantTypes != null)
            {
                target.AllowedGrantTypes = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(model.AllowedGrantTypes, x => new ClientGrantType() { GrantType = x}));
            }

            if (model.RedirectUris != null)
            {
                target.RedirectUris = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(model.RedirectUris, x => new ClientRedirectUri() { RedirectUri = x}));
            }

            if (model.PostLogoutRedirectUris != null)
            {
                target.PostLogoutRedirectUris = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(model.PostLogoutRedirectUris, x => new ClientPostLogoutRedirectUri() { PostLogoutRedirectUri = x}));
            }

            if (model.AllowedScopes != null)
            {
                target.AllowedScopes = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(model.AllowedScopes, x => new ClientScope() { Scope = x}));
            }

            if (model.AllowedIdentityTokenSigningAlgorithms != null)
            {
                target.AllowedIdentityTokenSigningAlgorithms = SharedInternalMappers.MapAllowedSigningAlgorithms(model.AllowedIdentityTokenSigningAlgorithms);
            }

            if (model.IdentityProviderRestrictions != null)
            {
                target.IdentityProviderRestrictions = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(model.IdentityProviderRestrictions, x => new ClientIdPRestriction() { Provider = x}));
            }

            if (model.Claims != null)
            {
                target.Claims = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(model.Claims, MapToClientClaim));
            }

            if (model.AllowedCorsOrigins != null)
            {
                target.AllowedCorsOrigins = System.Linq.Enumerable.ToList(System.Linq.Enumerable.Select(model.AllowedCorsOrigins, x => new ClientCorsOrigin() { Origin = x}));
            }

            target.Enabled = model.Enabled;
            target.ClientId = model.ClientId;
            target.ProtocolType = model.ProtocolType;
            target.RequireClientSecret = model.RequireClientSecret;
            target.ClientName = model.ClientName;
            target.Description = model.Description;
            target.ClientUri = model.ClientUri;
            target.LogoUri = model.LogoUri;
            target.RequireConsent = model.RequireConsent;
            target.AllowRememberConsent = model.AllowRememberConsent;
            target.RequirePkce = model.RequirePkce;
            target.AllowPlainTextPkce = model.AllowPlainTextPkce;
            target.RequireRequestObject = model.RequireRequestObject;
            target.AllowAccessTokensViaBrowser = model.AllowAccessTokensViaBrowser;
            target.FrontChannelLogoutUri = model.FrontChannelLogoutUri;
            target.FrontChannelLogoutSessionRequired = model.FrontChannelLogoutSessionRequired;
            target.BackChannelLogoutUri = model.BackChannelLogoutUri;
            target.BackChannelLogoutSessionRequired = model.BackChannelLogoutSessionRequired;
            target.AllowOfflineAccess = model.AllowOfflineAccess;
            target.AlwaysIncludeUserClaimsInIdToken = model.AlwaysIncludeUserClaimsInIdToken;
            target.IdentityTokenLifetime = model.IdentityTokenLifetime;
            target.AccessTokenLifetime = model.AccessTokenLifetime;
            target.AuthorizationCodeLifetime = model.AuthorizationCodeLifetime;
            target.AbsoluteRefreshTokenLifetime = model.AbsoluteRefreshTokenLifetime;
            target.SlidingRefreshTokenLifetime = model.SlidingRefreshTokenLifetime;
            target.ConsentLifetime = model.ConsentLifetime;
            target.RefreshTokenUsage =  (int) model.RefreshTokenUsage;
            target.UpdateAccessTokenClaimsOnRefresh = model.UpdateAccessTokenClaimsOnRefresh;
            target.RefreshTokenExpiration = (int) model.RefreshTokenExpiration;
            target.AccessTokenType = (int) model.AccessTokenType;
            target.EnableLocalLogin = model.EnableLocalLogin;
            target.IncludeJwtId = model.IncludeJwtId;
            target.AlwaysSendClientClaims = model.AlwaysSendClientClaims;
            target.ClientClaimsPrefix = model.ClientClaimsPrefix;
            target.PairWiseSubjectSalt = model.PairWiseSubjectSalt;
            target.UserSsoLifetime = model.UserSsoLifetime;
            target.UserCodeType = model.UserCodeType;
            target.DeviceCodeLifetime = model.DeviceCodeLifetime;

            if (model.Properties != null)
            {
                target.Properties = DictionaryToClientProperties(model.Properties);
            }

            return target;
        }

        private List<ClientProperty> DictionaryToClientProperties(IDictionary<string, string> modelProperties)
        {
            var clientProperties = new List<ClientProperty>();

            foreach (KeyValuePair<string, string> modelProperty in modelProperties)
            {
                clientProperties.Add(new ClientProperty(){ Key = modelProperty.Key, Value = modelProperty.Value});
            }

            return clientProperties;
        }

        private ClientClaim MapToClientClaim(Models.ClientClaim source)
        {
            if (source == null)
                return default;
            var target = new ClientClaim();
            target.Type = source.Type;
            target.Value = source.Value;
            return target;
        }

        private ClientSecret MapToClientSecret(global::IdentityServer4.Models.Secret source)
        {
            if (source == null)
                return default;
            var target = new ClientSecret();
            target.Description = source.Description;
            target.Value = source.Value;
            target.Expiration = source.Expiration;
            target.Type = source.Type;
            return target;
        }
    }

    /// <summary>
    /// Extension methods to map to/from entity/model for clients.
    /// </summary>
    public static class ClientMappers
    {
        static ClientMappers()
        {
            Mapper = new ClientMappersInternal();
        }

        internal static ClientMappersInternal Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.Client ToModel(this Client entity)
        {
            return Mapper.ToModel(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Client ToEntity(this Models.Client model)
        {
            return Mapper.ToEntity(model);
        }
    }
}