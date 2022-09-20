using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.Brands.Rules
{
    public class LanguageBusinessRules
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageBusinessRules(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task LanguageNameCanNotBeDuplicateWhenInserted(string name)
        {
            IPaginate<Language> result = await _languageRepository.GetListAsync(l => l.Name == name);
            if (result.Items.Any()) throw new BusinessException("Language name exists.");
        }
    }
}
