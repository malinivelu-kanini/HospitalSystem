using HealthCare_BigbangAPI.Interfaces;
using HealthCare_BigbangAPI.Models.DTO_s;
using HealthCare_BigbangAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HealthCare_BigbangAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IManageUser _manageUser;
        private readonly IRepo<int, Patient> _patientRepo;
        private readonly IRepo<int, Doctor> _doctorRepo;
        private readonly ILogger<User> _logger;

        public UserController(IManageUser manageUser, IRepo<int, Patient> patientRepo, IRepo<int, Doctor> doctorRepo, ILogger<User> logger)
        {
            _manageUser = manageUser;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
            _logger = logger;
        }

        [HttpPost("Patient")]
        [ProducesResponseType(typeof(ActionResult<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> RegisterPatient(PatientDTO intern)
        {
            var result = await _manageUser.RegisterPatient(intern);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to Register Right Now");
        }

        [HttpPost("Doctor")]
        [ProducesResponseType(typeof(ActionResult<UserDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> RegisterDoctor(DoctorDTO doc)
        {
            var result = await _manageUser.RegisterDoctor(doc);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to register Right Now");
        }


        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<UserDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDTO>> Login(UserDTO user)
        {
            var result = await _manageUser.Login(user);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Unable to login Right Now");
        }

        [HttpGet]
        [ProducesResponseType(typeof(ActionResult<Doctor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Doctor>> GetAllDoctors()
        {
            var doctors = await _doctorRepo.GetAll();

            try
            {

                if (doctors != null)
                    return Ok(doctors);

                return NotFound();
            }
            catch (Exception ex)
            {

                return BadRequest("Unable to retriving doctors right now");
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("ApproveRequest")]
        public async Task<ActionResult<Doctor>> ApproveRequest(int doctorId)
        {
            var result = await _manageUser.Approval(doctorId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Doctor not present");

        }
        [HttpPut]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Doctor>> UpdateDoctorDetails(int doctorID, DoctorDTO doctor)
        {
            var existingDoctor = await _doctorRepo.Get(doctorID);
            if (existingDoctor == null)
            {
                return NotFound();
            }

            existingDoctor.DoctorName = doctor.DoctorName;
            existingDoctor.DOB = doctor.DOB;
            existingDoctor.Age = doctor.Age;
            existingDoctor.Gender = doctor.Gender;
            existingDoctor.PhoneNo = doctor.PhoneNo;
            existingDoctor.Email = doctor.Email;
            existingDoctor.Specialization = doctor.Specialization;
            existingDoctor.Experience = doctor.Experience;

            var updatedDoctor = await _doctorRepo.Update(existingDoctor);
            if (updatedDoctor != null)
            {
                return Ok(updatedDoctor);
            }

            return BadRequest("Unable to update the doctor");
        }




        [HttpGet("GetById")]
        [ProducesResponseType(typeof(ActionResult<Doctor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Doctor>> GetDoctorById(int doctorID)
        {
            var doctor = await _doctorRepo.Get(doctorID);
            try
            {
                if (doctor != null)
                    return Ok(doctor);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Not able to get doctor detail based on ID");
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ActionResult<Doctor>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Doctor>> DeleteDoctor(int doctorID)
        {
            var doctor = await _doctorRepo.Delete(doctorID);
            try
            {
                if (doctor != null)
                    return Ok("Doctor details deleted successfully");
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest("Not able to delete doctor details");
            }
        }
    }
}
