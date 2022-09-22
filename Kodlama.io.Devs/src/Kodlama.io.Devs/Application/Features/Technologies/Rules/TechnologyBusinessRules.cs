using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task TechnologyNameCanNotBeDublicateWhenInserted(string name)
        {
            IPaginate<Technology> technologies = await _technologyRepository.GetListAsync(t => t.Name == name);
            if (technologies.Items.Any()) throw new BusinessException("Technology already exists");
        }

        public void TechnologyShouldExistWhenRequested(Technology technology)
        {
            if (technology == null) throw new BusinessException("Requested Technology does not exist");
        }

        public async Task TechnologyNameCanNotBeSameAsEntityModelWhenUpdated(string name)
        {
            var result = await _technologyRepository.GetAsync(t => t.Name == name);
            if (result != null) throw new BusinessException("Language name and your request is same");
        }
    }
}
