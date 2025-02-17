﻿using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommand : IRequest<DeletedTechnologyDto>
    {
        public int Id { get; set; }


        public class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand, DeletedTechnologyDto>
        {
            private readonly ITechnologyRepository _technologyRepository;
            private readonly IMapper _mapper;
            private readonly TechnologyBusinessRules _technologyBusinessRules;

            public DeleteTechnologyCommandHandler(ITechnologyRepository technologyRepository, IMapper mapper, TechnologyBusinessRules technologyBusinessRules)
            {
                _technologyRepository = technologyRepository;
                _mapper = mapper;
                _technologyBusinessRules = technologyBusinessRules;
            }

            public async Task<DeletedTechnologyDto> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
            {
                var entity = _technologyRepository.Get(t => t.Id == request.Id);
                _technologyBusinessRules.TechnologyShouldExistWhenRequested(entity);

                Technology mappedTechnology = _mapper.Map<Technology>(entity);
                Technology deletedTechnology = _technologyRepository.Delete(entity);
                DeletedTechnologyDto deletedTechnologyDto = _mapper.Map<DeletedTechnologyDto>(deletedTechnology);

                return deletedTechnologyDto;
            }

        }
    }
}
