using ServerBase.Interfaces;
using ServerFromScratch.Models;

namespace ServerFromScratch.Controllers;

public class UsersController : IController
{
    public User[] Index()
    {
        return new[]
        {
            new User("Anton", "Kharchenko", "Zzzzoro"),
            new User("Johny", "Martin", "HHH")
        };
    }
}