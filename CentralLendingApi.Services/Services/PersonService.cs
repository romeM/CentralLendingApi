using CentralLendingApi.Data.Dtos;
using CentralLendingApi.Data.Models;
using CentralLendingApi.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CentralLendingApi.Services.Services
{
    public interface IPersonService
    {
        Person Authenticate(string username, string password);
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        Person Create(Person person, string password);
        void Update(PersonDto userDto);
        void Delete(int id);
    }

    public class PersonService : IPersonService
    {
        private CentralLendingContext _context;

        public PersonService(CentralLendingContext context)
        {
            _context = context;
        }

        public Person Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var person = _context.Person.SingleOrDefault(x => x.UserName == username);

            // check if username exists
            if (person == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, person.PasswordHash, person.PasswordSalt))
                return null;

            // authentication successful
            return person;
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Person;
        }

        public Person GetById(int id)
        {
            return _context.Person.Find(id);
        }

        public Person Create(Person person, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_context.Person.Any(x => x.UserName == person.UserName))
                throw new AppException("Username \"" + person.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            person.CreatedOn = DateTime.Now;
            person.UpdatedOn = DateTime.Now;
            person.PasswordHash = passwordHash;
            person.PasswordSalt = passwordSalt;

            _context.Person.Add(person);
            _context.SaveChanges();

            return person;
        }

        public void Update(PersonDto userDto)
        {
            var person = _context.Person.Find(userDto.Id);

            if (person == null)
                throw new AppException("User not found");

            if (userDto.UserName != person.UserName)
            {
                // username has changed so check if the new username is already taken
                if (_context.Person.Any(x => x.UserName == userDto.UserName))
                    throw new AppException("Username " + userDto.UserName + " is already taken");
            }

            // update user properties
            HMapper.Mapper.Fill(userDto, person);
            person.UpdatedOn = DateTime.Now;
            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(userDto.Password, out passwordHash, out passwordSalt);

                person.PasswordHash = passwordHash;
                person.PasswordSalt = passwordSalt;
            }

            _context.Person.Update(person);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Person.Find(id);
            if (user != null)
            {
                _context.Person.Remove(user);
                _context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            //using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            //{
            //    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            //    for (int i = 0; i < computedHash.Length; i++)
            //    {
            //        if (computedHash[i] != storedHash[i]) return false;
            //    }
            //}

            return true;
        }
    }
}
