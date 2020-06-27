using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        // get main photo of user
        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            // get all photos in database with userId and then get a main photo of user
            return await _context.Photos.Where(u => u.UserId == userId).
                FirstOrDefaultAsync(p => p.IsMain);
        }

        // get photo with id
        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);

            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            // 'photos' has relationship with 'users' table
            // get user with id and all photos of it
            // like 'SELECT * FROM Users JOIN Photos ON Users.Id = Photos.UserId;'
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }

        // if have any record was saved, return true, else return false
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        
    }
}