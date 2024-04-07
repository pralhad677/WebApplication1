using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.IRepository;
using WebApplication1.Model;
using WebApplication1.Util;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using WebApplication1.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Data.SqlClient;




namespace WebApplication1.Repository
{
    //<T> : IRepository<T> where T : class
    public class UserRepository<T> : IUserRepository<T> where T : class
    {
        
        protected readonly MyDbContext _dbContextFactory;
        private static object BCryptGenerator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(MyDbContext dbContextFactory, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            
            _dbContextFactory = dbContextFactory;
            _mapper= mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        

        async Task<T> IUserRepository<T>.CreateUser(UserDto user)
        {
            CustomErrorClass<User> er = new Util.CustomErrorClass<User>();

            var retrievedUser = await _dbContextFactory.Users
                                             .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (retrievedUser == null)
            {
               // HashingUtil hu = new HashingUtil();
               // user.Password = Convert.ToBase64String(hu.HashPassword(user.Password));
               user.Password = HashPassword(user.Password);
                var user2 = _mapper.Map<UserDto, User>(user);
                await _dbContextFactory.Users.AddAsync(user2);
            await _dbContextFactory.SaveChangesAsync();

                return user2 as T;
            }
            return "user already exist" as T;
            
           
        }

       async Task<string> IUserRepository<T>.LoginUser(UserDto user)
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
      async  Task<dynamic > IUserRepository<T>.CreateCourse(CourseDto courseDto)
        {
            try
            {
                string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"]!;
                string[] parts = authorizationHeader.Split(' ');
                var token = parts[1];
                TokenValidation tokenValidation = new TokenValidation();
                var claimsPrincipal = tokenValidation.ValidateTokenAndGetClaims(token);

                if (claimsPrincipal != null)
                {
                    string userEmail = tokenValidation.ExtractUserEmailFromClaims(claimsPrincipal);
                    Console.WriteLine($"User email: {userEmail}");
                    var course1 = _mapper.Map<CourseDto, Course>(courseDto);
                    User user =   _dbContextFactory.Users.FirstOrDefault(u => u.Email ==userEmail)!;
                    course1.UserId = user.Id;
                    EntityEntry<Course> courseEntry = await _dbContextFactory.Courses.AddAsync(course1);
                    Course course2 = courseEntry.Entity;
                    await _dbContextFactory.Courses.AddAsync(course2);
                    await _dbContextFactory.SaveChangesAsync();
                    return "courseCreated";
                }
                else
                {
                    return 'b';
                }
            }
            catch (System.Exception ex)
            {
                // Log the exception
                return "error";
               // return StatusCode(500, "An error occurred while processing your request."); // Return HTTP 500 Internal Server Error response
            }
        }

       async  public Task<List<Course>> GetAllCourse()
        {
            //  var entities = await context.YourEntities.Where(e => e.ID == particularID).ToList();
       
         //   return entities; ;
            string authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"]!;
            string[] parts = authorizationHeader.Split(' ');
            var token = parts[1];
            TokenValidation tokenValidation = new TokenValidation();
            var claimsPrincipal = tokenValidation.ValidateTokenAndGetClaims(token);

            if (claimsPrincipal != null)
            {
                string userEmail = tokenValidation.ExtractUserEmailFromClaims(claimsPrincipal);
                Console.WriteLine($"User email: {userEmail}");
               
                User user = _dbContextFactory.Users.FirstOrDefault(u => u.Email == userEmail)!;
                //   var entities = await _dbContextFactory.Courses.Where(e => e.UserId == user.Id).ToListAsync();
                var entities = await _dbContextFactory.Courses.FromSqlRaw("EXEC GetUserCoursesForUser @UserID",
                    new SqlParameter("@UserID", user.Id)).ToListAsync();

                return entities;
            }
            else
            {
                return [];
            }
           
        }
    }
}
