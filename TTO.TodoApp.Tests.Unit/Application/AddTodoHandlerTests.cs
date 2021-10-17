using FluentAssertions;
using Moq;
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
    public class AddTodoHandlerTests
    {
        //moq
        //private readonly Mock<ITodoRepository> _todoRepository;
        //nsubstitute
        private readonly ITodoRepository _todoRepository;
        private readonly AddTodoHandler _addTodoHandler;



        public AddTodoHandlerTests()
        {
            //mock
            //_todoRepository = new Mock<ITodoRepository>();
            //nsubstitute
            _todoRepository = Substitute.For<ITodoRepository>();
            
            //mock
            //_addTodoHandler = new AddTodoHandler(_todoRepository.Object);
            //nsubstitute
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

            //mock
            //_todoRepository.Setup(x => x.ExistsById(id)).ReturnsAsync(false);
            //nsubstitute
            _todoRepository.ExistsById(id).Returns(Task.FromResult(false));

            await _addTodoHandler.Handle(request, token);

            //chcemy sprawdzić, czy handler uruchamia uruchomie e AddAsync tylko 1 raz i czy z parametrem todo != null
            //mock
            //_todoRepository.Verify(x => x.AddAsync(It.IsAny<Todo>(), token), Times.Once()); //zweryfikuj czy add async wykonała się tylko raz na jakimkolwiek obiekcie todo
            //nsubstitue
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

            //mockowanie metodą anonimową nie najlepsze dla metod o dluższych nazwach i wielu parametrach - zob. nsubstitute
            //_todoRepository.Setup(x => x.ExistsById(id)).ReturnsAsync(true);
            //nsubstitute
            _todoRepository.ExistsById(id).Returns(Task.FromResult(true));

            Func<Task> act = async () => await _addTodoHandler.Handle(request, token);

            await act.Should().ThrowAsync<Exception>();
            
        }
    }
}
