using AttendanceManagement.Hangfire.Email.Interface;

namespace AttendanceManagement.Hangfire
{
    public class MaintananceService : IManitanaceService
    {
        public void SyncRecords()
        {
            Console.WriteLine("The sync has started");
        }
    }
}
