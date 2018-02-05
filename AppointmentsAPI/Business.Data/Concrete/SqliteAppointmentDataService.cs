using AppointmentsAPI.Business.Data.Contracts;
using AppointmentsAPI.Helper;
using AppointmentsAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Business.Data.Concrete
{
    public class SqliteAppointmentDataService : IAppointmentDataService
    {
        private readonly IDataContext _context;

        public SqliteAppointmentDataService(IDataContext context)
        {
            Guard.ArgumentNotNull(context, nameof(context));
            _context = context;
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            using (var dbTran = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<Appointment>().Add(appointment);
                    await _context.SaveChangesAsync();
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    Trace.Write(ex, nameof(CreateAppointment));
                    throw;
                }

                return appointment;
            }
        }

        public async Task<List<Appointment>> GetCurrentAppointments()
        {
            try
            {
                var appointments = await _context.Set<Appointment>().Where(t => DateTime.Parse(t.AppointmentDate) > DateTime.Now).ToListAsync();
                return appointments;
            }
            catch (Exception ex)
            {
                Trace.Write(ex, nameof(GetCurrentAppointments));
                throw;
            }
        }
    }
}