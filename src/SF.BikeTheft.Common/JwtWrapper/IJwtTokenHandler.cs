using Microsoft.IdentityModel.Tokens;

namespace SF.BikeTheft.Common.JwtWrapper;

public interface IJwtTokenHandler
{
    string WriteToken(SecurityToken token);
    SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor);
}
