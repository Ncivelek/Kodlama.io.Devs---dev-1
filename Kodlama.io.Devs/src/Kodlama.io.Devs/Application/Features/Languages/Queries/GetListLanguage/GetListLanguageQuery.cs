﻿using Application.Features.Languages.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Languages.Queries.GetListLanguage
{
    public class GetListLanguageQuery : IRequest<LanguageListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListLanguageQueryHandler : IRequestHandler<GetListLanguageQuery, LanguageListModel>
        {
            private readonly ILanguageRepository _languageReapository;
            private readonly IMapper _mapper;

            public GetListLanguageQueryHandler(ILanguageRepository languageReapository, IMapper mapper)
            {
                _languageReapository = languageReapository;
                _mapper = mapper;
            }

            public async Task<LanguageListModel> Handle(GetListLanguageQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Language> languages = await _languageReapository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                LanguageListModel mappedLanguageListModel = _mapper.Map<LanguageListModel>(languages);

                return mappedLanguageListModel;
            }
        }
    }
}
