using ContosoUniversityCQRS.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityCQRS.Application.System.Commands
{
    public class SeedDataCommand : IRequest
    {
    }

    public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand>
    {
        ISchoolContext _schoolContext;

        public SeedDataCommandHandler(ISchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
        {


            return Unit.Value;
        }
    }
}
