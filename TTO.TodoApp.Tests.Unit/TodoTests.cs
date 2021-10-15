using FluentAssertions;
using TTO.TodoApp.Domain;
using Xunit;

namespace TTO.TodoApp.Tests.Unit;
public class TodoTests
{
    [Fact]
    public void GivenValidTodoDataShouldCreated()
    {
        var id = Guid.NewGuid();
        var name = "Test";
        var isCompleted = true;

        var todo = new Todo(id, name, isCompleted);

        todo.Id.Should().Be(id);
        todo.Text.Should().Be(name);
        todo.IsCompleted.Should().Be(isCompleted);
    }
}