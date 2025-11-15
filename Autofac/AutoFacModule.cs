using AttendanceManagement.AppDbContext;
using AttendanceManagement.IRepository;
using AttendanceManagement.Repository;
using AttendanceManagement.Services;
using Autofac;
using Microsoft.EntityFrameworkCore;
namespace AttendanceManagement.Autofac
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CacheService>().As<ICacheService>().InstancePerLifetimeScope();
            builder.RegisterType<ClassRepo>().As<IClassRepo>().InstancePerLifetimeScope();
            builder.RegisterType<StaffRepo>().As<IStaffRepo>().InstancePerLifetimeScope();
            builder.RegisterType<StudentRepo>().As<IStudentRepo>().InstancePerLifetimeScope();
            builder.Register(context =>
            {
                var configuration = context.Resolve<Microsoft.Extensions.Configuration.IConfiguration>();
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                return new ApplicationDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();
        }
    }
}
