using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Commands.DeleteLanguage
{
    public class DeleteLanguageCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
    
            public DeleteLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
            {
                Language? language = await _languageRepository.GetAsync(l => l.Id == request.Id);

                await _languageRepository.DeleteAsync(language);

                return Unit.Value;
            }
        }
    }
}
