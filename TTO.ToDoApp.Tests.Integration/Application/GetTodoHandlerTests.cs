using FluentAssertions;
using TTO.TodoApp.Application;
using TTO.TodoApp.Domain;
using Xunit;

namespace TTO.ToDoApp.Tests.Integration.Application
{
    public class GetTodoHandlerTests: IClassFixture<TodoInMemoryRepository>
    {
        private readonly TodoInMemoryRepository _todoRepository;
        private readonly GetTodoHandler _getTodoHandler;

        public GetTodoHandlerTests()
        {
            _todoRepository = new TodoInMemoryRepository();
            _getTodoHandler = new GetTodoHandler(_todoRepository);
        }

        [Fact]
        public async Task GetTodo()
        {
            var todo = new Todo(Guid.NewGuid(), "abc", true);

            await _todoRepository.AddAsync(todo, CancellationToken.None);

            var request = new GetTodo(todo.Id);

            await _getTodoHandler.Handle(request, CancellationToken.None);

            var todo2 = await _todoRepository.GetAsync(todo.Id);

            todo2.Should().NotBeNull();
            todo2.Id.Should().Be(todo.Id);
            todo2.IsCompleted.Should().BeTrue();
            todo2.Text.Should().Be(todo.Text);
            
        }
    }
}
