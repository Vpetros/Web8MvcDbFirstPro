using Microsoft.EntityFrameworkCore;
using SchoolApp.Core.Enums;
using SchoolApp.Data;
using SchoolApp.Models;
using System.Linq.Expressions;

namespace SchoolApp.Repositories
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(Mvc8DbProContext context) : base(context)
        {
        }

        public async Task<Teacher?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await context.Teachers
                .Where(t => t.PhoneNumber == phoneNumber)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Course>> GetTeacherCoursesAsync(int teacherId)
        {
            List<Course> courses;

            courses = await context.Teachers
                .Where(t => t.Id == teacherId)
                .SelectMany(t => t.Courses)
                .ToListAsync();
            return courses;

        }

        public async Task<List<User>> GetAllUsersTeachersAsync()
        {
            var usersWithRoleTeacher = await context.Users
                .Where(u => u.UserRole == UserRole.Teacher)
                .Include(u => u.Teacher) // Εager loading της σχετικής οντότητας Teacher
                .ToListAsync();
            return usersWithRoleTeacher;
        }

        public async Task<PaginatedResult<User>> GetPaginatedUsersTeachersAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            var usersWithRolesTeacher = await context.Users
                .Where(u => u.UserRole == UserRole.Teacher)
                .Include(u => u.Teacher) // Εager loading της σχετικής οντότητας Teacher
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            int totalRecords = await context.Users
                .Where(u => u.UserRole == UserRole.Teacher)
                .CountAsync();
            return new PaginatedResult<User>(usersWithRolesTeacher, totalRecords, pageNumber, pageSize);
        }

  
        public async Task<PaginatedResult<User>> GetPaginatedUsersTeachersFilteredAsync(int pageNumber, int pageSize,
            List<Expression<Func<User, bool>>> predicates)
        {
            IQueryable<User> query = context.Users
                .Where(u => u.UserRole == UserRole.Teacher)
                .Include(u => u.Teacher); // Εager loading της σχετικής οντότητας Teacher

            if (predicates != null && predicates.Count > 0)
            {
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate); 
                }
            }

            int totalRecords = await query.CountAsync();
            int skip = (pageNumber - 1) * pageSize;
            var data = await query
                .OrderBy(u => u.Id) // πάντα χρειάζεται ένα OrderBy πριν το Skip 
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResult<User>(data, totalRecords, pageNumber, pageSize);
        }

     

        public Task<User?> GetUserTeacherByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }
    }
}
