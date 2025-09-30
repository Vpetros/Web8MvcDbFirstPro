﻿using SchoolApp.Data;

namespace SchoolApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Mvc8DbProContext context;

        public UnitOfWork(Mvc8DbProContext context)
        {
            this.context = context;
        }


        public UserRepository UserRepository => new(context);
        public StudentRepository StudentRepository => new(context);

        public TeacherRepository TeacherRepository => new(context);
        public CourseRepository CourseRepository => new(context);


        public async Task<bool> SaveAsync()
        {
            return await context.SaveChangesAsync() > 0;    // κάνει commit και αυτόματα rollback αν αποτύχει
        }
    }
}
