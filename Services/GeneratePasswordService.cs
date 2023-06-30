using HealthCare_BigbangAPI.Interfaces;
using HealthCare_BigbangAPI.Models;

namespace HealthCare_BigbangAPI.Services
{
    public class GeneratePasswordService : IGeneratePassword
    {
        public async Task<string?> GeneratePasswordDoctor(Doctor doctor)
        {
            string password = String.Empty;
            password = doctor.DoctorName.Substring(0, 4);
            password += doctor.DOB.Day;
            password += doctor.DOB.Month;
            return password;
        }
        public async Task<string?> GeneratePasswordPatient(Patient patient)
        {
            string password = String.Empty;
            password = patient.PatientName.Substring(0, 4);
            password += patient.DOB.Day;
            password += patient.DOB.Month;
            return password;
        }
    }
}
