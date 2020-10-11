using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Api.Domain.DTOs;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Api.Service.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _Repository;

        private SigningConfigurations _SigningConfigurations;

        private TokenConfiguration _TokenConfiguration;

        private IConfiguration _Configuration;

        public LoginService(IUserRepository repository,
                            SigningConfigurations signingConfigurations,
                            TokenConfiguration tokenConfiguration,
                            IConfiguration configuration)
        {
            _Repository = repository;
            _SigningConfigurations = signingConfigurations;
            _TokenConfiguration = tokenConfiguration;
            _Configuration = configuration;
        }
        public async Task<object> Login(LoginDTO loginInfo)
        {
            if (loginInfo == null || string.IsNullOrWhiteSpace(loginInfo.Email))
                return new
                {
                    authenticated = false,
                    message = "Authentication failed"
                };

            var user = await _Repository.SelectByEmailAsync(loginInfo.Email);
            if (user == null)
                return new
                {
                    authenticated = false,
                    message = "Authentication failed"
                };

            var identity = new ClaimsIdentity(
                new GenericIdentity(user.Email),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                }
            );

            var createDate = DateTime.Now;
            var expirationDate = createDate + TimeSpan.FromSeconds(_TokenConfiguration.Seconds);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _TokenConfiguration.Issuer,
                Audience = _TokenConfiguration.Audience,
                SigningCredentials = _SigningConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return SuccessObject(createDate, expirationDate, token, loginInfo);
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, LoginDTO loginInfo) => new
        {
            authenticated = true,
            created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
            expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
            accessToken = token,
            user = loginInfo.Email,
            message = "User succesfully logged in"
        };

    }

}