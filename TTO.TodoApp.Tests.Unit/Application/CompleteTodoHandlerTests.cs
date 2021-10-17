using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTO.TodoApp.Application;
using TTO.TodoApp.Domain;
using Xunit;

namespace TTO.TodoApp.Tests.Unit.Application
{
    public class CompleteTodoHandlerTests
    {
        private readonly Mock<ITodoRepository> _todoRepository;
        private readonly CompleteTodoHandler _completeHandler;
        private readonly GetTodoHandler _getTodoHandler;

        public CompleteTodoHandlerTests()
        {
            _todoRepository = new Mock<ITodoRepository>();
            _completeHandler = new CompleteTodoHandler(_todoRepository.Object);
            _getTodoHandler = new GetTodoHandler(_todoRepository.Object);
        }

        [Fact]
        public async void GivenNullObjectExceptionShouldBeThrown()
        {
            var id = Guid.NewGuid();
            var token = CancellationToken.None;

            var request = new CompleteTodo(id);

            _todoRepository.Setup(x => x.GetAsync(request.Id)).Returns((Task<Todo>)null);

            var action = async () => await _completeHandler.Handle(request, token);

            await action.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void GivenIsCompletedTrueObjectExceptionShouldBeThrown()
        {
            var id = Guid.NewGuid();
            var token = CancellationToken.None;

            var request = new CompleteTodo(id);

            var mock = new Mock<Todo>(id, "task", true);

            _todoRepository.Setup(x => x.GetAsync(id).Result).Returns(mock.Object);

            var action = async () => await _completeHandler.Handle(request, token);

            await action.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async void GivenIsCompletedFalseObjectExceptionShouldBeThrown()
        {
            var id = Guid.NewGuid();
            var token = CancellationToken.None;

            var request = new CompleteTodo(id);

            var mock = new Mock<Todo>(id, "task", false);

            _todoRepository.Setup(x => x.GetAsync(id).Result).Returns(mock.Object);

            var action = async () => await _completeHandler.Handle(request, token);

            await action.Should().NotThrowAsync<Exception>();
        }

    }
}
