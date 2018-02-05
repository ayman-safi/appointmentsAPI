using AppointmentsAPI.Business.Data.Contracts;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace AppointmentsAPI.Business.Data
{
    public class FullDbContext : DbContext, IDataContext
    {
        public FullDbContext() : base("AppointmentsContext")
        {
            Database.SetInitializer<FullDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //TODO: Move this assembly scanning to the reflection helper
            var markerType = typeof(IFullContextDbMapping);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("AppointmentsAPI"));
            var mappingTypes = assemblies.SelectMany(
                                            x => x.GetTypes().Where(t => !t.IsInterface && markerType.IsAssignableFrom(t)));

            try
            {
                foreach (var m in mappingTypes)
                {
                    dynamic map = Activator.CreateInstance(m);
                    modelBuilder.Configurations.Add(map);
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message, nameof(OnModelCreating));
                throw;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}