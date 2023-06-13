// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Linq;
using IdentityModel;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServer4.EntityFramework.Mappers
{
    internal class ScopeMappersInternal
    {
        internal Models.ApiScope ToModel(ApiScope entity)
        {
            if (entity == null)
            {
                return default;
            }

            var target = new Models.ApiScope
            {
                Enabled = entity.Enabled,
                Description = entity.Description,
                DisplayName = entity.DisplayName,
                Emphasize = entity.Emphasize,
                Name = entity.Name,
                Required = entity.Required,
                ShowInDiscoveryDocument = entity.ShowInDiscoveryDocument,
                UserClaims = entity.UserClaims.Select(x => x.Type).ToList(),
                Properties = entity.Properties.ToDictionary(x => x.Key, x => x.Value)
            };

            return target;
        }

        internal ApiScope ToEntity(Models.ApiScope entity)
        {
            if (entity == null)
            {
                return default;
            }

            var target = new ApiScope
            {
                Enabled = entity.Enabled,
                Description = entity.Description,
                DisplayName = entity.DisplayName,
                Emphasize = entity.Emphasize,
                Name = entity.Name,
                Required = entity.Required,
                ShowInDiscoveryDocument = entity.ShowInDiscoveryDocument,
                UserClaims = entity.UserClaims.Select(x => new ApiScopeClaim(){ Type = x } ).ToList(),
            };

            if (entity.Properties != null)
            {
                target.Properties = entity.Properties.Select(x => new ApiScopeProperty() { Key = x.Key, Value = x.Value }).ToList();
            }

            return target;
        }
    }


    /// <summary>
    /// Extension methods to map to/from entity/model for scopes.
    /// </summary>
    public static class ScopeMappers
    {
        static ScopeMappers()
        {
            Mapper = new ScopeMappersInternal();
        }

        internal static ScopeMappersInternal Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.ApiScope ToModel(this ApiScope entity)
        {
            return entity == null ? null : Mapper.ToModel(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static ApiScope ToEntity(this Models.ApiScope model)
        {
            return model == null ? null : Mapper.ToEntity(model);
        }
    }
}