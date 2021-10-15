using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTO.TodoApp.Domain;
using TTO.TodoApp.Infrastructure.Ef;

namespace TTO.ToDoApp.Tests.Integration.Application
{
    internal class TodoInMemoryRepository : ITodoRepository
    {
        private readonly List<Todo> _todos
            = new List<Todo>(10);

        private readonly TodoDbContext _todoDbContext;

        public TodoInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: "Test")
                .Options;

            _todoDbContext = new TodoDbContext(options);

            using var context = new TodoDbContext(options);

            context.Todos.AddRange(
                new Todo(Guid.NewGuid(), "abc", false),
                new Todo(Guid.NewGuid(), "xyz", false),
                new Todo(Guid.NewGuid(), "done", true)
                );
            context.SaveChanges();
        }

        public Task AddAsync(Todo todo, CancellationToken cancellationToken)
        {
            _todos.Add(todo);
            return Task.CompletedTask;
        }

        public Task<Todo> GetAsync(Guid id)
        {
            var todo = _todos
                .SingleOrDefault(x => x.Id.Equals(id));

            return Task.FromResult(todo);
        }

    }
}
