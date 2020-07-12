using ContosoUniversityCQRS.WebUI.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoUniversityCQRS.WebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly SchoolContext _context;

        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public BaseController(SchoolContext context)
        {
            _context = context;
        }
    }
}
