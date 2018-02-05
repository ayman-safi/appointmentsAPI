using AppointmentsAPI.Business.Data.Contracts;
using AppointmentsAPI.Business.Services.Contracts;
using AppointmentsAPI.Helper;
using AppointmentsAPI.Models.Entity;
using AppointmentsAPI.Models.Projections;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AppointmentsAPI.Business.Services.Concrete
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;

        private readonly IAppointmentDataService _appointmentDataService;

        public AppointmentService(IMapper mapper, IAppointmentDataService appointmentDataService)
        {
            Guard.ArgumentNotNull(mapper, nameof(mapper));
            Guard.ArgumentNotNull(appointmentDataService, nameof(appointmentDataService));

            _mapper = mapper;
            _appointmentDataService = appointmentDataService;
        }

        public async Task<AppointmentProjection> CreateAppointment(AppointmentProjection appointment)
        {
            try
            {
                if (appointment != null)
                {
                    var appointmentEntity = _mapper.Map<Appointment>(appointment);
                    await _appointmentDataService.CreateAppointment(appointmentEntity);
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message, nameof(CreateAppointment));
            }

            return appointment;
        }

        public async Task<List<AppointmentProjection>> GetAppointments()
        {
            try
            {
                var appointments = await _appointmentDataService.GetCurrentAppointments();
                return _mapper.Map<List<AppointmentProjection>>(appointments);
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message, nameof(GetAppointments));
                return await Task.FromResult(new List<AppointmentProjection>());
            }
        }
    }
}