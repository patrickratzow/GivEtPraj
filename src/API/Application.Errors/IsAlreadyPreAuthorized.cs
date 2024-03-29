﻿using Commentor.GivEtPraj.Application.Errors.Interfaces;

namespace Commentor.GivEtPraj.Application.Errors;

public readonly record struct IsAlreadyPreAuthorized(Guid Id) : IAlreadyExistsError
{
    public string ErrorMessage => $"The Id {Id} is already authorized";
}