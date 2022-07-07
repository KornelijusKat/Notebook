using Notebook.Database.DatabaseRecords;
using Notebook.Database.DatabaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Database
{
    public class AccountService :IAccount
    {
        private readonly NoteContext _context;
        public AccountService(NoteContext context)
        {
            _context = context;
        }
        public User SignUpAccount(string username, string password)
        {
            var account = CreateAccount(username, password);
            _context.Users.Add(account);
            _context.SaveChanges();
            return account;
        }

        public User CreateAccount(string username, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            var account = new User
            {
                Id = Guid.NewGuid(),
                FirstName = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
            return account;
        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        public bool Login(string username, string password)
        {
            var account = _context.Users.FirstOrDefault(x => x.FirstName == username);
            if (account is not null)
            {
                if (VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
                {
                    return true;
                }
            }
            return false;
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
