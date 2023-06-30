using HealthCare_BigbangAPI.Models.DTO_s;

namespace HealthCare_BigbangAPI.Interfaces
{
    public interface IGenerateToken
    {
        public string GenerateToken(UserDTO user);

    }
}
