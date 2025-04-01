using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace DealGeinieCrmService.Utilities
{
    public class JwtManager
    {
        // 生产环境建议从环境变量或密钥管理服务读取
        public static readonly string SecretKey = "U2ZqZ3JvZ3V0L1N0YXRpY1JhbmRvbUJ5dGVzKysrKyo=";

        public static string GenerateToken(string username, string role, int expireMinutes = 60)
        {
            // 验证密钥长度
            var keyBytes = Convert.FromBase64String(SecretKey);
            if (keyBytes.Length < 32)
                throw new ArgumentException("SecretKey 必须为至少 32 字节的 Base64 字符串");

            var securityKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role), // 或 JwtRegisteredClaimNames.Role
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:60026", // 必须与 Startup.cs 中的 ValidIssuer 一致
                audience: "web-client",            // 必须与 Startup.cs 中的 ValidAudience 一致
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}