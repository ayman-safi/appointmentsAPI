using AppointmentsAPI.Business.Data;
using AppointmentsAPI.Business.Data.Concrete;
using AppointmentsAPI.Business.Data.Contracts;
using AppointmentsAPI.Business.Services.Concrete;
using AppointmentsAPI.Business.Services.Contracts;
using AppointmentsAPI.Models.Entity;
using AppointmentsAPI.Models.Projections;
using AutoMapper;
using StructureMap;
using StructureMap.Graph;
using System;

namespace AppointmentsAPI.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Appointment, AppointmentProjection>().ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => DateTime.Parse(src.AppointmentDate)));
                cfg.CreateMap<AppointmentProjection, Appointment>().ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate.ToString()));
            });

            var mapper = mapperConfig.CreateMapper();

            For<IMapper>().Use(mapper);
            For<IDataContext>().Use<FullDbContext>();
            For<IAppointmentDataService>().Use<SqliteAppointmentDataService>();
            For<IAppointmentService>().Use<AppointmentService>().Ctor<IMapper>().Is(mapper);
            //For<IDisposable>().Add<FullDbContext>();
        }
    }
}