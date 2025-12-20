namespace AttendanceManagement.Hangfire.Email.Interface
{
    public interface IEmailService
    {
        void SendWelcomeMail(string email, string name);
        void SendGettingStartedEmail(string email, string name);
    }
}
