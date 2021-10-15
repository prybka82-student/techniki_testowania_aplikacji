using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Xunit;
using TTO.TodoApp.Infrastructure.Ef;
using TTO.TodoApp.Application;

namespace TTO.ToDoApp.Tests.Integration.Application
{
    public class AddTodoHandlerTests : IClassFixture<TodoInMemoryRepository>
    {
        private readonly TodoInMemoryRepository _todorepository;
        private readonly AddTodoHandler _addTodoHandler;

        public AddTodoHandlerTests()
        {
            _todorepository = new TodoInMemoryRepository();
            _addTodoHandler = new AddTodoHandler(_todorepository);
            
        }

        [Fact]
        public async Task AddTodo()
        {
            var id = Guid.NewGuid();
            var text = "Test";
            var isCompleted = false;
            
            var request = new AddTodo(id, text, isCompleted);

            await _addTodoHandler.Handle(request, CancellationToken.None);

            var todo = await _todorepository.GetAsync(id);

            todo.Should().NotBeNull();
            todo.Id.Should().Be(id);
            todo.Text.Should().Be(text);
            todo.IsCompleted.Should().Be(isCompleted);
        }
    }
}
