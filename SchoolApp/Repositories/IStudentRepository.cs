using SchoolApp.Data;
using SchoolApp.Models;
using System.Linq.Expressions;

namespace SchoolApp.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Course>> GetStudentCoursesAsync(int studentId);
        Task<Student?> GetByAm(string? am);
        Task<PaginatedResult<Student>> GetPaginatedUsersStudentsAsync();
        Task<PaginatedResult<Student>> GetPaginatedUsersStudentsFilteredAsync(int pageNumber, int pageSize,
            List<Expression<Func<User, bool>>> predicates);
    }
}
