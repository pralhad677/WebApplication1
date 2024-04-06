using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.IRepository;
using WebApplication1.Model;
using WebApplication1.Util;

namespace WebApplication1.Repository
{
    //<T> : IRepository<T> where T : class
    public class UserRepository<T> : IUserRepository<T> where T : class
    {
        
        protected readonly MyDbContext _dbContextFactory;

        public UserRepository(MyDbContext dbContextFactory)
        {
            
            _dbContextFactory = dbContextFactory;
        }

        

        async Task<CustomErrorClass<T>> IUserRepository<T>.CreateUser(User user)
        {
            CustomErrorClass<User> er = new Util.CustomErrorClass<User>();

            var retrievedUser = await _dbContextFactory.Users
                                             .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (retrievedUser == null)
            {
                HashingUtil hu = new HashingUtil();
                user.Password = Convert.ToBase64String(hu.HashPassword(user.Password));
                await _dbContextFactory.Users.AddAsync(user);
            await _dbContextFactory.SaveChangesAsync();
                er.Data = user;
                er.IsSuccess = true;
            return er as CustomErrorClass<T>;
            }
            er.Message = "User already exists";
            er.IsSuccess = false;
            return er as CustomErrorClass<T>;
        }

       async Task<CustomErrorClass<T>> IUserRepository<T>.LoginUser(User user)
        {
            CustomErrorClass<string> er = new Util.CustomErrorClass<string>();
            TokenGeneration tk = new TokenGeneration();
             bool userExist =  await _dbContextFactory.Users.AnyAsync(u => u.Email == user.Email);
            if (userExist != true)
            {
                var token = tk.GenerateToken("your_issuer", "your_audience", user.Email );
                er.Message = "Token generated";
                er.Data = token;
                return er as CustomErrorClass<T>;
             // Return the token to the client
            }
            else
            {
                er.Message = "unAuthorized";
                er.Data = null;
                return er as CustomErrorClass<T>; // More informative error message
            }
        }

        

        public Task<IEnumerable<CustomErrorClass<T>>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
