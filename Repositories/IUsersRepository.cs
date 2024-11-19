using System.Collections.Generic;

using DemoCoink.Models;

namespace DemoCoink.Repositories
{
    public interface IUsersRepository
    {
        IEnumerable<Users> ListUser();
        Dictionary<string, string> Create(Users User);
        void Delete(int id);
    }
}
