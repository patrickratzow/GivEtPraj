﻿using Commentor.GivEtPraj.Domain.ValueObjects;

namespace Commentor.GivEtPraj.Application.Common.Mappings;

public class CaseProfile : Profile
{
    public CaseProfile()
    {
        CreateMap<BaseCase, CaseDto>();
        CreateMap<CaseImage, CaseImageDto>()
            .ForMember(
                c => c.Url,
                opts => opts.MapFrom(
                    m => $"https://givetpraj.blob.core.windows.net/cases/{m.Id}.jpg"
                )
            );
        CreateMap<Category, CategoryDto>();
        CreateMap<SubCategory, SubCategoryDto>();

        CreateMap<LocalizedString, string>().ConvertUsing(new LocalizedStringConverter());
    }
}