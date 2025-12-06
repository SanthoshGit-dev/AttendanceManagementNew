using AttendanceManagement.AppDbContext;
using AttendanceManagement.Hangfire;
using AttendanceManagement.Hangfire.Email;
using AttendanceManagement.Hangfire.Email.Interface;
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
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
            builder.RegisterType<MerchService>().As<IMerchService>().InstancePerLifetimeScope();
            builder.RegisterType<MaintananceService>().As<IManitanaceService>().InstancePerLifetimeScope();
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
