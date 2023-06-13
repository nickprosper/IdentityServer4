// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using IdentityServer4.EntityFramework.Entities;

namespace IdentityServer4.EntityFramework.Mappers
{
    internal class IdentityResourceMappersInternal
    {
        internal Models.IdentityResource ToModel(IdentityResource entity)
        {
            if (entity == null)
            {
                return default;
            }

            var target = new Models.IdentityResource
            {
                Enabled = entity.Enabled,
                Name = entity.Name,
                DisplayName = entity.DisplayName,
                Description = entity.Description,
                Required = entity.Required,
                Emphasize = entity.Emphasize,
                ShowInDiscoveryDocument = entity.ShowInDiscoveryDocument,
                UserClaims = entity.UserClaims.Select(x => x.Type).ToList(),
                Properties = entity.Properties.ToDictionary(x => x.Key, x => x.Value)
            };

            return target;
        }

        internal IdentityResource ToEntity(Models.IdentityResource model)
        {
            if (model == null)
            {
                return default;
            }

            var target = new IdentityResource
            {
                Enabled = model.Enabled,
                Name = model.Name,
                DisplayName = model.DisplayName,
                Description = model.Description,
                Required = model.Required,
                Emphasize = model.Emphasize,
                ShowInDiscoveryDocument = model.ShowInDiscoveryDocument,
                UserClaims = model.UserClaims.Select(x => new IdentityResourceClaim() { Type = x }).ToList(),
            };

            if (model.Properties != null)
            {
                target.Properties = model.Properties.Select(x => new IdentityResourceProperty() { Key = x.Key, Value = x.Value }).ToList();
            }

            return target;
        }
    }


    /// <summary>
    /// Extension methods to map to/from entity/model for identity resources.
    /// </summary>
    public static class IdentityResourceMappers
    {
        static IdentityResourceMappers()
        {
            Mapper = new IdentityResourceMappersInternal();
        }

        internal static IdentityResourceMappersInternal Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static Models.IdentityResource ToModel(this IdentityResource entity)
        {
            return entity == null ? null : Mapper.ToModel(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static IdentityResource ToEntity(this Models.IdentityResource model)
        {
            return model == null ? null : Mapper.ToEntity(model);
        }
    }
}