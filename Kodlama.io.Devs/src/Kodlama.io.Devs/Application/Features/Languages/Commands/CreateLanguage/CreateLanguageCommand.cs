﻿using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateLanguageCommand : IRequest<CreatedLanguageDto>
    {
        public string Name { get; set; }
        public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, CreatedLanguageDto>
        {
            private readonly ILanguageRepository _languageRepository;
            private readonly IMapper _mapper;
            private readonly LanguageBusinessRules _languageBusinessRules;

            public CreateLanguageCommandHandler(ILanguageRepository languageRepository, IMapper mapper, LanguageBusinessRules languageBusinessRules)
            {
                _languageRepository = languageRepository;
                _mapper = mapper;
                _languageBusinessRules = languageBusinessRules;
            }

            public async Task<CreatedLanguageDto> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
            {
                await _languageBusinessRules.LanguageNameCanNotBeDuplicateWhenInserted(request.Name);

                Language mappedBrand = _mapper.Map<Language>(request);

                Language createdBrand = await _languageRepository.AddAsync(mappedBrand);
                CreatedLanguageDto createdBrandDto = _mapper.Map<CreatedLanguageDto>(createdBrand);
               
                return createdBrandDto;
            }
        }
    }
}
