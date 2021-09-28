﻿using System.Net.Http;

namespace Commentor.GivEtPraj.WebApi.Contracts.Requests
{
    public record CreateCaseRequest(
        string Title, 
        string Description,
        MultipartFormDataContent? Images
    );
}