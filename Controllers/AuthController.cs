using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers;

[ApiController]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        // For demo purpose we assume valid credentials
        if (userLogin.Username != "testUser" || userLogin.Password != "testPasswd")
            return Task.FromResult<IActionResult>(Unauthorized());
        string token = GenerateJwtToken(userLogin.Username);
        return Task.FromResult<IActionResult>(Ok(new JwtResponse
            { Token = token, Expiration = DateTime.UtcNow.AddHours(1).ToString(CultureInfo.InvariantCulture) }));
    }

    /*private string GenerateJwtToken(string userName)
    {
       var claims = new[]
       {
          new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ?? string.Empty),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
          new Claim("username", userName)
       };
       var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? string.Empty);
     //  SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
       var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
       var token = new JwtSecurityToken(
          issuer: _configuration["Jwt:Issuer"],
          audience: _configuration["Jwt:Audience"],
          claims: claims,
          expires: DateTime.UtcNow.AddHours(1),
          signingCredentials: creds);

       return new JwtSecurityTokenHandler().WriteToken(token);
    }*/

    private string GenerateJwtToken(string userName)
    {
        //
        // var claims = new[]
        // {
        //     new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"] ?? string.Empty),
        //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //     new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
        //     new Claim("username", userName)
        // };
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? string.Empty);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userName)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}