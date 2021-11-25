using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Cocktail.WebApi.Helpers;
using Cocktail.WebApi.Models;
using CocktailApp.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Cocktail.WebApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<UserAPI> GetAll();
        UserAPI GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        //private List<UserAPI> _users = new List<UserAPI>
        //{
        //    new UserAPI { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        //};

        private readonly IConfiguration _config;
        private readonly CocktailsDbContext _context;

        public UserService(CocktailsDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
            if (_context == null)
            {
                throw new ArgumentNullException("Context is null");
            }

            Console.WriteLine("Ctr UserService avec context");
        }

        //public UserService(IOptions<AppSettings> appSettings)
        //{
        //    _appSettings = appSettings.Value;
        //}

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {

            //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            var userDb = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            // return null if user not found
            if (userDb == null) return null;

            var user = new UserAPI
            {
                Id = userDb.PkId,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                Username = userDb.Username,
                Password = userDb.Password
            };


            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<UserAPI> GetAll()
        {
            var userDb = _context.Users.ToList();
            List <UserAPI> users= new();

            foreach(User u in userDb)
            {
                users.Add(
                    new UserAPI
                    {
                        Id = u.PkId,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Username = u.Username,
                        UserType = (int)u.UserType,
                        Password = u.Password
                    }
                );
            }

            return users;
        }

        public UserAPI GetById(int id)
        {
            var userDb = _context.Users.FirstOrDefault(x => x.PkId == id);
            var user = new UserAPI
            {
                Id = userDb.PkId,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                Username = userDb.Username,
                Password = userDb.Password
            };
            return user;
        }

        // helper methods
        private string generateJwtToken(UserAPI user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("AppSettings:Secret"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}