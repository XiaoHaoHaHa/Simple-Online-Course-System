using CoreLib;
using CoreLib.ClientViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseApi.ApiService
{
    public class JwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenToken(UserModel user)
        {
            //JWT 簽章使⽤的對稱式加密的⾦鑰
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenSettings:IssuerSigningKey"]));

            // HmacSha256 SecurityAlgorithms
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // 建立 SecurityTokenDescriptor (JWT PayLoad)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["TokenSettings:Issuer"],
                Audience = _config["TokenSettings:Audience"],
                NotBefore = DateTime.Now,
                IssuedAt = DateTime.Now,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(int.Parse(_config["TokenSettings:ExpireMin"])),
                SigningCredentials = signingCredentials
            };

            // JWT securityToken
            var tokenHandler = new JwtSecurityTokenHandler();         
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);         
            var serializeToken = tokenHandler.WriteToken(securityToken); 

            return serializeToken;
        }
    }
}
