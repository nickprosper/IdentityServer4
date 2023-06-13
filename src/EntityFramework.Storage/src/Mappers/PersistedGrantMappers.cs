// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

namespace IdentityServer4.EntityFramework.Mappers
{

    internal class PersistedGrantMappersInternal
    {
        internal PersistedGrant ToModel(Entities.PersistedGrant entity)
        {
            if (entity == null)
            {
                return default;
            }

            var target = new PersistedGrant
            {
                Key = entity.Key,
                Type = entity.Type,
                SubjectId = entity.SubjectId,
                ClientId = entity.ClientId,
                CreationTime = entity.CreationTime,
                Expiration = entity.Expiration,
                Data = entity.Data,
                ConsumedTime = entity.ConsumedTime,
                SessionId = entity.SessionId,
                Description = entity.Description
            };

            return target;
        }

        internal Entities.PersistedGrant ToEntity(PersistedGrant model)
        {
            if (model == null)
            {
                return default;
            }

            var target = new Entities.PersistedGrant
            {
                Key = model.Key,
                Type = model.Type,
                SubjectId = model.SubjectId,
                ClientId = model.ClientId,
                CreationTime = model.CreationTime,
                Expiration = model.Expiration,
                Data = model.Data,
                ConsumedTime = model.ConsumedTime,
                SessionId = model.SessionId,
                Description = model.Description
            };

            return target;
        }

        internal void UpdateEntity(PersistedGrant model, Entities.PersistedGrant entity)
        {
            entity.Key = model.Key;
            entity.Type = model.Type;
            entity.SubjectId = model.SubjectId;
            entity.ClientId = model.ClientId;
            entity.CreationTime = model.CreationTime;
            entity.Expiration = model.Expiration;
            entity.Data = model.Data;
            entity.ConsumedTime = model.ConsumedTime;
            entity.SessionId = model.SessionId;
            entity.Description = model.Description;
        }
    }


    /// <summary>
    /// Extension methods to map to/from entity/model for persisted grants.
    /// </summary>
    public static class PersistedGrantMappers
    {
        static PersistedGrantMappers()
        {
            Mapper = new PersistedGrantMappersInternal();
        }

        internal static PersistedGrantMappersInternal Mapper { get; }

        /// <summary>
        /// Maps an entity to a model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public static PersistedGrant ToModel(this Entities.PersistedGrant entity)
        {
            return entity == null ? null : Mapper.ToModel(entity);
        }

        /// <summary>
        /// Maps a model to an entity.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static Entities.PersistedGrant ToEntity(this PersistedGrant model)
        {
            return model == null ? null : Mapper.ToEntity(model);
        }

        /// <summary>
        /// Updates an entity from a model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="entity">The entity.</param>
        public static void UpdateEntity(this PersistedGrant model, Entities.PersistedGrant entity)
        {
            Mapper.UpdateEntity(model, entity);
        }
    }
}