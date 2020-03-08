using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data {
    public class AuthRepository : IAuthRepository {

        private readonly DataContext Context;
        public AuthRepository (DataContext context) {
            this.Context = context;

        }
        public async Task<User> Login (string namename, string password) {
            var user = await Context.Users.FirstOrDefaultAsync (x => x.Name == namename);
            if (user == null)
                return null;
            if (!VerifyPassword (password, user.PasswordHash, user.PasswordSalt))
                return null;
            return user;
        }

        private bool VerifyPassword (string password, byte[] passwordHash, byte[] passwordSalt) {
            using (var Hmach = new System.Security.Cryptography.HMACSHA512 (passwordSalt)) {
                var ComputedHash = Hmach.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
                for (int i = 0; i < passwordHash.Length; i++) {
                    if (ComputedHash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }

        public async Task<User> Register (User user, string password) {
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHash (password, out PasswordHash, out PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            await Context.Users.AddAsync (user);
            await Context.SaveChangesAsync ();
            return user;

        }

        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using (var Hmach = new System.Security.Cryptography.HMACSHA512 ()) {
                passwordSalt = Hmach.Key;
                passwordHash = Hmach.ComputeHash (System.Text.Encoding.UTF8.GetBytes (password));
            }
        }

        public async Task<bool> UserExist (string name) {
            if (await Context.Users.AnyAsync (x => x.Name == name))
                return true;
            return false;
        }
    }
}