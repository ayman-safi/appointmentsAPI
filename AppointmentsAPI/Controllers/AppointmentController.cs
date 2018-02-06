using AppointmentsAPI.Business.Services.Contracts;
using AppointmentsAPI.Helper;
using AppointmentsAPI.Models.Projections;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppointmentsAPI.Controllers
{
    [RoutePrefix("api/appointments")]
    public class AppointmentController : ApiController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            Guard.ArgumentNotNull(appointmentService, nameof(appointmentService));

            _appointmentService = appointmentService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAppointments()
        {
            var appointments = await _appointmentService.GetAppointments();
            if (appointments == null)
            {
                return InternalServerError(new Exception("Something went wrong, please try again."));
            }

            return Ok(appointments);
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateAppointment(AppointmentProjection appointment)
        {
            var createdAppointment = await _appointmentService.CreateAppointment(appointment);
            return Ok(appointment);
        }
    }
}
