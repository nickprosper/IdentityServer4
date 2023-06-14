// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Configuration;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer4.Services
{
    /// <summary>
    /// Default token creation service
    /// </summary>
    public class DefaultTokenCreationService : ITokenCreationService
    {
        /// <summary>
        /// The key service
        /// </summary>
        protected readonly IKeyMaterialService Keys;

        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        ///  The clock
        /// </summary>
        protected readonly ISystemClock Clock;

        /// <summary>
        /// The options
        /// </summary>
        protected readonly IdentityServerOptions Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTokenCreationService"/> class.
        /// </summary>
        /// <param name="clock">The options.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        public DefaultTokenCreationService(
            ISystemClock clock,
            IKeyMaterialService keys,
            IdentityServerOptions options,
            ILogger<DefaultTokenCreationService> logger)
        {
            Clock = clock;
            Keys = keys;
            Options = options;
            Logger = logger;
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// A protected and serialized security token
        /// </returns>
        public virtual async Task<string> CreateTokenAsync(Token token)
        {
            var header = await CreateHeaderAsync(token);
            var payload = await CreatePayloadAsync(token);

            return await CreateJwtAsync(new JwtSecurityToken(header, payload));
        }
        

        /// <summary>
        /// Creates the JWT header
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The JWT header</returns>
        protected virtual async Task<JwtHeader> CreateHeaderAsync(Token token)
        {
            var credential = await Keys.GetSigningCredentialsAsync(token.AllowedSigningAlgorithms);

            if (credential == null)
            {
                throw new InvalidOperationException("No signing credential is configured. Can't create JWT token");
            }

            var header = new JwtHeader(credential);

            // emit x5t claim for backwards compatibility with v4 of MS JWT library
            if (credential.Key is X509SecurityKey x509Key)
            {
                var cert = x509Key.Certificate;
                if (Clock.UtcNow.UtcDateTime > cert.NotAfter)
                {
                    Logger.LogWarning("Certificate {subjectName} has expired on {expiration}", cert.Subject, cert.NotAfter.ToString(CultureInfo.InvariantCulture));
                }

                header["x5t"] = Base64Url.Encode(cert.GetCertHash());
            }

            if (token.Type == TokenTypes.AccessToken)
            {
                if (Options.AccessTokenJwtType.IsPresent())
                {
                    header["typ"] = Options.AccessTokenJwtType;
                }
            }

            return header;
        }

        /// <summary>
        /// Creates the JWT payload
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The JWT payload</returns>
        protected virtual Task<JwtPayload> CreatePayloadAsync(Token token)
        {
            var payload = token.CreateJwtPayload(Clock, Options, Logger);
            return Task.FromResult(payload);
        }

        /// <summary>
        /// Applies the signature to the JWT
        /// </summary>
        /// <param name="jwt">The JWT object.</param>
        /// <returns>The signed JWT</returns>
        protected virtual Task<string> CreateJwtAsync(JwtSecurityToken jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            
            // HACK: Json Serialization doesn't work for things like cnf claim,
            // Switching to the Newtonsoft serializer for now but we want to revert this after serializing the token
            
            var oldSerializer = JsonExtensions.Serializer;
            var oldDeserializer = JsonExtensions.Deserializer;
            JsonExtensions.Serializer = JsonConvert.SerializeObject;
            JsonExtensions.Deserializer = JsonConvert.DeserializeObject;

            var writtenToken = handler.WriteToken(jwt);
            
            JsonExtensions.Serializer = oldSerializer;
            JsonExtensions.Deserializer = oldDeserializer;
            return Task.FromResult(writtenToken);
        }
    }
}