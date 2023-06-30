using HealthCare_BigbangAPI.Models.DTO_s;
using HealthCare_BigbangAPI.Models;

namespace HealthCare_BigbangAPI.Interfaces
{
    public interface IManageUser
    {
        public Task<UserDTO> Login(UserDTO user);
        public Task<UserDTO> RegisterPatient(Patient patient);
        public Task<UserDTO> RegisterDoctor(Doctor doctor);
        public Task<Doctor> Approval(int DoctorId);

    }
}
