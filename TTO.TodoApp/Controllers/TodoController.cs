using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TTO.TodoApp.Application;
using TTO.TodoApp.Domain;

namespace TTO.TodoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Todo> Get(GetTodo query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpPost]
        public async Task Add(AddTodo command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }
    }
}
