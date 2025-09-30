namespace SchoolApp.Repositories
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        
        TeacherRepository TeacherRepository { get; }
        

        Task<bool> SaveAsync();
    }
}
