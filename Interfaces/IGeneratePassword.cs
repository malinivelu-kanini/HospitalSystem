using HealthCare_BigbangAPI.Models;

namespace HealthCare_BigbangAPI.Interfaces
{
    public interface IGeneratePassword
    {
        public Task<string?> GeneratePasswordDoctor(Doctor doctor);
        public Task<string?> GeneratePasswordPatient(Patient patient);

    }
}
