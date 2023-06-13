// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Storage.Mappers;

namespace IdentityServer4.EntityFramework.Mappers
{

    internal class ApiResourceMappersInternal
    {
        internal Models.ApiResource ToModel(ApiResource entity)
        {
            if (entity == null)
            {
                return default;
            }

            var target = new Models.ApiResource
            {
                Enabled = entity.Enabled,
                Name = entity.Name,
                DisplayName = entity.DisplayName,
                Description = entity.Description,
                ShowInDiscoveryDocument = entity.ShowInDiscoveryDocument,
                AllowedAccessTokenSigningAlgorithms =
                    SharedInternalMappers.MapAllowedSigningAlgorithms(entity.AllowedAccessTokenSigningAlgorithms)
            };

            if (entity.Properties != null)
            {
                target.Properties = entity.Properties.ToDictionary(x => x.Key, x => x.Value);
            }

            if (entity.Scopes != null)
            {
                target.Scopes = entity.Scopes.Select(x => x.Scope).ToList();
            }

            if (entity.UserClaims != null)
            {
                target.UserClaims = entity.UserClaims.Select(x => x.Type).ToList();
            }

            if (entity.Secrets != null)
            {
                target.ApiSecrets = entity.Secrets.Select(x => new Models.Secret
                {
                    Type = x.Type,
                    Value = x.Value,
                    Expiration = x.Expiration,
                    Description = x.Description
                }).ToList();
            }

            return target;
        }

        internal ApiResource ToEntity(Models.ApiResource entity)
        {
            if (entity == null)
            {
                return default;
            }

            var target = new ApiResource
            {
                Enabled = entity.Enabled,
                Name = entity.Name,
                DisplayName = entity.DisplayName,
                Description = entity.Description,
                ShowInDiscoveryDocument = entity.ShowInDiscoveryDocument,
                AllowedAccessTokenSigningAlgorithms = SharedInternalMappers.MapAllowedSigningAlgorithms(entity.AllowedAccessTokenSigningAlgorithms)
            };

            if (entity.Properties != null)
            {
                target.Properties = entity.Properties.Select(x => new ApiResourceProperty(){Key = x.Key, Value = x.Value}).ToList();
            }

            if (entity.Scopes != null)
            {
                target.Scopes = entity.Scopes.Select(x => new ApiResourceScope() { Scope = x } ).ToList();
            }

            if (entity.UserClaims != null)
            {
                target.UserClaims = entity.UserClaims.Select(x => new ApiResourceClaim() { Type = x}).ToList();
            }

            if (entity.ApiSecrets != null)
            {
                target.Secrets = entity.ApiSecrets.Select(x => new ApiResourceSecret
                {
                    Type = x.Type,
                    Value = x.Value,
                    Expiration = x.Expiration,
                    Description = x.Description
                }).ToList();
            }

            return target;
        }
    }
    /// <summary>
    /// Extension methods to map to/from entity/model for API resources.
    /// </summary>
    public static class ApiResourceMappers
    {
        static ApiResourceMappers()
        {
            Mapper = new ApiResourceMappersInternal();
        }

        internal static ApiResourceMappersInternal Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.ApiResource ToModel(this ApiResource entity)
        {
            return entity == null ? null : Mapper.ToModel(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ApiResource ToEntity(this Models.ApiResource model)
        {
            return model == null ? null : Mapper.ToEntity(model);
        }
    }
}