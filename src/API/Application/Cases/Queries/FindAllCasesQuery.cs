﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Commentor.GivEtPraj.Application.Common.Interfaces;
using Commentor.GivEtPraj.Application.Contracts;
using Commentor.GivEtPraj.Domain.Errors;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Commentor.GivEtPraj.Application.Cases.Queries
{
    public record FindAllCasesQuery : IRequest<List<CaseSummaryDto>>;

    public class FindAllCasesQueryHandler : IRequestHandler<FindAllCasesQuery, List<CaseSummaryDto>>
    {
        private readonly IAppDbContext _db;

        public FindAllCasesQueryHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<List<CaseSummaryDto>> Handle(FindAllCasesQuery request, CancellationToken cancellationToken)
        {
            var cases = await _db.Cases
                .Select(c => new CaseSummaryDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    AmountOfPictures = c.Pictures.Count
                })
                .ToListAsync(cancellationToken);
            
            return cases;
        }
    }
}