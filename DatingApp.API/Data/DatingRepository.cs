using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext Context;
        public DatingRepository(DataContext context)
        {
            this.Context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            this.Context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this.Context.Remove(entity);
        }

        public async Task<User> GetUser(int Id)
        {
            var u = await this.Context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Id == Id);
            return u;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var u = await this.Context.Users.Include(p => p.Photos).ToArrayAsync();
            return u;
        }

        public async Task<bool> SaveAll()
        {
            return await this.Context.SaveChangesAsync()>0;
        }
    }
}