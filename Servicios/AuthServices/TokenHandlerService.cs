using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pedidos.Abstraccion;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pedidos.Servicios.AuthServices
{
    public interface ITokenHandlerService
    {
        string GenerateJwtTokens(ITokensParameters parametros);
    }

    public class TokenHandlerService : ITokenHandlerService
    {

        private readonly JwtConfig _jwtConfig; //JWT (JSON Web Token)


        public TokenHandlerService(IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _jwtConfig = optionsMonitor.CurrentValue;//valor actual
        }

        public string GenerateJwtTokens(ITokensParameters parametros)
        {
            var jwtTokendHandler = new JwtSecurityTokenHandler(); //crea y escribe los tokens.

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);// convierte tu clave secreta(_jwtConfig.Secret) en un arreglo de bytes, para usarla como llave criptográfica.

            var claims = new List<Claim>
            {
                new Claim("Id", parametros.Id),
                new Claim(JwtRegisteredClaimNames.Sub, parametros.Id),
                new Claim(JwtRegisteredClaimNames.Name, parametros.UserName),
                new Claim(JwtRegisteredClaimNames.Email, parametros.Email)
            };
            if (parametros.Roles != null && parametros.Roles.Any())
            {
                foreach (var role in parametros.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }


            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                //Subject = new ClaimsIdentity(new[]      //CLAIM: AFIRMAR
                //{
                //    new Claim ("Id", parametros.Id),
                //    new Claim(JwtRegisteredClaimNames.Sub,parametros.Id),
                //    new Claim(JwtRegisteredClaimNames.Name, parametros.UserName),
                //    new Claim(JwtRegisteredClaimNames.Email, parametros.Email)

                //}),
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )


              //  Expires = DateTime.UtcNow.AddHours(20),
                // SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

               // SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokendHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokendHandler.WriteToken(token);
            return jwtToken;
        }

    }
}
