using AttendanceManagement.Hangfire.Email.Interface;

namespace AttendanceManagement.Hangfire.Email
{
    public class EmailService : IEmailService
    {
        public void SendGettingStartedEmail(string email, string name)
        {
            Console.WriteLine($"This will send a welcome email to ${name} using the following email ${email}");
        }

        public void SendWelcomeMail(string email, string name)
        {
            Console.WriteLine($"This will send a started email to ${name} using the following email ${email}");
        }
    }
}
