using AppointmentsAPI.Models.Projections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppointmentsAPI.Business.Services.Contracts
{
    public interface IAppointmentService
    {
        Task<List<AppointmentProjection>> GetAppointments();

        Task<AppointmentProjection> CreateAppointment(AppointmentProjection appointment);
    }
}
