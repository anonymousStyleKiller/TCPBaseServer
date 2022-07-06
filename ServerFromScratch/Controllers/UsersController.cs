using ServerBase.Interfaces;
using ServerFromScratch.Models;

namespace ServerFromScratch.Controllers;

public class UsersController : IController
{
    public static User[] Index()
    {
        Thread.Sleep(5);
        return new[]
        {
            new User("Anton", "Kharchenko", "Zzzzoro"),
            new User("Johny", "Martin", "HHH")
        };
    }
    
    public static async Task<User[]> IndexAsync()
    {
        await Task.Delay(5);
        return new[]
        {
            new User("Anton", "Kharchenko", "Zzzzoro"),
            new User("Johny", "Martin", "HHH")
        };
    }
}