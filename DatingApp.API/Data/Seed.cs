using System.Collections.Generic;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext Context;
        public Seed(DataContext context)
        {
            this.Context = context;

        }
        public void SeedUser()
        {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Name = user.Name.ToLower();
                Context.Users.Add(user);

            }
            Context.SaveChanges();
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var Hmach = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Hmach.Key;
                passwordHash = Hmach.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}