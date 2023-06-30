using HealthCare_BigbangAPI.Interfaces;
using HealthCare_BigbangAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCare_BigbangAPI.Services
{
    public class DoctorRepo : IRepo<int, Doctor>
    {
        private readonly Context _context;
        private readonly ILogger<User> _logger;

        public DoctorRepo(Context context, ILogger<User> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Doctor?> Add(Doctor item)
        {

            try
            {
                _context.Doctors.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Doctor?> Delete(int key)
        {
            try
            {
                var doc = await Get(key);
                if (doc != null)
                {
                    _context.Doctors.Remove(doc);
                    await _context.SaveChangesAsync();
                    return doc;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Doctor?> Get(int key)
        {
            try
            {
                var doc = await _context.Doctors.FirstOrDefaultAsync(i => i.DoctorId == key);
                return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<ICollection<Doctor>?> GetAll()
        {

            try
            {
                var doc = await _context.Doctors.ToListAsync();
                if (doc.Count > 0)
                    return doc;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        public async Task<Doctor?> Update(Doctor item)
        {
            try
            {
                var doc = await Get(item.DoctorId);
                if (doc != null)
                {
                    doc.DoctorId = item.DoctorId;
                    doc.DoctorName = item.DoctorName;
                    doc.User = item.User;
                    doc.Email = item.Email;
                    doc.PhoneNo = item.PhoneNo;
                    doc.DOB = item.DOB;
                    doc.Age = item.Age;
                    doc.Gender = item.Gender;
                    return doc;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
