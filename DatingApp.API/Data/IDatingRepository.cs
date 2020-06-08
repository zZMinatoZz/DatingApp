using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        // add T (class) method
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         //get all users
         Task<IEnumerable<User>> GetUsers();
         //get one user
         Task<User> GetUser(int id);
    }
}