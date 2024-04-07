using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.IRepository;
using WebApplication1.Model;
using WebApplication1.Util;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace WebApplication1.Repository
{
    //<T> : IRepository<T> where T : class
    public class UserRepository<T> : IUserRepository<T> where T : class
    {
        
        protected readonly MyDbContext _dbContextFactory;
        private static object BCryptGenerator;

        public UserRepository(MyDbContext dbContextFactory)
        {
            
            _dbContextFactory = dbContextFactory;
        }

        

        async Task<T> IUserRepository<T>.CreateUser(User user)
        {
            CustomErrorClass<User> er = new Util.CustomErrorClass<User>();

            var retrievedUser = await _dbContextFactory.Users
                                             .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (retrievedUser == null)
            {
               // HashingUtil hu = new HashingUtil();
               // user.Password = Convert.ToBase64String(hu.HashPassword(user.Password));
               user.Password = HashPassword(user.Password);
                await _dbContextFactory.Users.AddAsync(user);
            await _dbContextFactory.SaveChangesAsync();

                return user as T;
            }
            return "user already exist" as T;
            
           
        }

       async Task<string> IUserRepository<T>.LoginUser(User user)
        {
            CustomErrorClass<string> er = new Util.CustomErrorClass<string>();
       
             bool userExist =  await _dbContextFactory.Users.AnyAsync(u => u.Email == user.Email);
            var user1 = await _dbContextFactory.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
          
            if (userExist == true )
            {
                bool passdwordVerify = VerifyPassword(user.Password, user1!.Password);
                if(passdwordVerify == true)
                {

                TokenGeneration tk = new TokenGeneration();
                var token = tk.GenerateToken("your_issuer", "your_audience", user.Email );
                return token;
                }
                else{
                    return "unAuthorized";
                }
            }
          
            else
            {
                return "unAuthorized";       
            }
        }

        

        public async Task<IEnumerable<T>> GetAllUsers()
        {
            var allUser = (IEnumerable<T>) await _dbContextFactory.Users.ToListAsync();
            return allUser;
        }
        public   string HashPassword(string password)
        {
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
        public   bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
