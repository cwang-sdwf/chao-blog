using Chao_Blog.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Chao_Blog.Data
{
    public class RoutineDbContext: DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options): base(options)
        {
        }

        // 数据库两个表
        public DbSet<User> Users  { get; set; }

        public DbSet<Resume> Resumes { get; set; }
		// 定义数据长度 required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(x => x.UserLevel).IsRequired().HasMaxLength(1);

            modelBuilder.Entity<Resume>().Property(x => x.Introduction).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<Resume>().Property(x => x.Skills).IsRequired().HasMaxLength(1000);
            modelBuilder.Entity<Resume>().Property(x => x.Experiences).IsRequired().HasMaxLength(5000);

            // 1个company 对应多个employee
            modelBuilder.Entity<Resume>().HasOne(x => x.User)
                .WithMany(x => x.Resumes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //添加数据
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716c"),
                    FirstName = "Chao",
                    LastName = "Wang",
                    Password = "wc1234",
                    UserLevel = Level.admin,
                });

            modelBuilder.Entity<Resume>().HasData(
                new Resume
                {
                    Id = Guid.Parse("4b501cb3-d168-4cc0-b375-48fb33f318a4"),
                    UserId = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716c"),
                    Introduction = "testintroduction1",
                    Skills = "testskills1",
                    Experiences = "testexperience1",
                    Educations = "testEdutions1",
                }, new Resume{
                Id = Guid.Parse("4b501cb3-d168-4cc0-b375-48fb33f333a5"),
                    UserId = Guid.Parse("bbdee09c-089b-4d30-bece-44df5923716c"),
                    Introduction = "testintroduction2",
                    Skills = "testskills2",
                    Experiences = "testexperience2",
                    Educations = "testEdutions2",
                });
               
        }
    }
}

