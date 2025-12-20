using AttendanceManagement.Hangfire.Email.Interface;

namespace AttendanceManagement.Hangfire.Email
{
    public class MerchService : IMerchService
    {
        public void CreaateMerch(string id)
        {
            Console.WriteLine($"This will create Merch for the user {id}");
        }

        public void RemoveMerch(string id)
        {
            Console.WriteLine($"This will remove Merch for the user {id}");
        }
    }
}
