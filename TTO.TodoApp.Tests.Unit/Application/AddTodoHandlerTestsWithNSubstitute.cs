using FluentAssertions;
using NSubstitute;
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
    public class AddTodoHandlerTestsWithNSubstitute
    {
        private readonly ITodoRepository _todoRepository;
        private readonly AddTodoHandler _addTodoHandler;



        public AddTodoHandlerTestsWithNSubstitute()
        {
            _todoRepository = Substitute.For<ITodoRepository>();

            _addTodoHandler = new AddTodoHandler(_todoRepository);
        }

        [Fact]
        public async Task GivenValidTodoDataShouldBeAdded()
        {

            var id = Guid.NewGuid();
            var text = "Test";
            var isCompleted = false;
            var token = CancellationToken.None;
            var request = new AddTodo(id, text, isCompleted);

            _todoRepository.ExistsById(id).Returns(Task.FromResult(false));

            await _addTodoHandler.Handle(request, token);

            //chcemy sprawdzić, czy handler uruchamia uruchomie e AddAsync tylko 1 raz i czy z parametrem todo != null
            await _todoRepository.Received().AddAsync(Arg.Any<Todo>(), token);
            //await _todoRepository.Received().AddAsync(new Todo(id, text, isCompleted), token); //wymagałoby...? do sprawdzenia

        }

        [Fact]
        public async Task GivenAlreadyExistsTodoDataShouldThrowException()
        {

            var id = Guid.NewGuid();
            var text = "Test";
            var isCompleted = false;
            var token = CancellationToken.None;
            var request = new AddTodo(id, text, isCompleted);

            _todoRepository.ExistsById(id).Returns(Task.FromResult(true));

            Func<Task> act = async () => await _addTodoHandler.Handle(request, token);

            await act.Should().ThrowAsync<Exception>();
        }
    }
}
