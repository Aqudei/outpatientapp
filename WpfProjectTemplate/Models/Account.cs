using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OutPatientApp.Models
{
    public enum AccountType
    {
        Doctor,
        ChiefNurse,
        Clerk
    }

    public class Account : EntityBase
    {
        [StringLength(32)]
        public string UserName { get; set; }
        public string Password { get; private set; }
        public AccountType AccountType { get; set; } = AccountType.Clerk;
        public DateTime Birthday { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public string Specialization { get; set; }

        public Account()
        {

        }

        public void SetPassword(string password)
        {
            using (var hasher = new SHA1CryptoServiceProvider())
            {
                Password = Encoding.ASCII.GetString(hasher.ComputeHash(Encoding.ASCII.GetBytes(password)));
            }
        }

        public bool VerifyPassword(string password)
        {
            using (var hasher = new SHA1CryptoServiceProvider())
            {
                var hashed = Encoding.ASCII.GetString(hasher.ComputeHash(Encoding.ASCII.GetBytes(password)));
                return hashed == Password;
            }
        }
    }
}
