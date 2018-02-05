using AppointmentsAPI.Business.Data.Contracts;
using AppointmentsAPI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AppointmentsAPI.Business.Data.Maps
{
    public class AppointmentMap : EntityTypeConfiguration<Appointment>, IFullContextDbMapping
    {
        public AppointmentMap()
        {
            ToTable("Appointments");

            HasKey(p => p.Id);

            Property(p => p.Id).HasColumnName("Id");
            Property(p => p.FirstName).HasColumnName("FirstName");
            Property(p => p.LastName).HasColumnName("LastName");
            Property(p => p.Email).HasColumnName("Email");
            Property(p => p.Reason).HasColumnName("Reason");
            Property(p => p.AppointmentDate).HasColumnName("AppointmentDate");
            Property(p => p.AppointmentTime).HasColumnName("AppointmentTime");
            Property(p => p.Gender).HasColumnName("Gender");
            Property(p => p.ContactNumber).HasColumnName("ContactNumber");
            Property(p => p.EstablishedPatient).HasColumnName("EstablishedPatient");
            Property(p => p.InsuranceOption).HasColumnName("InsuranceOption");
        }
    }
}