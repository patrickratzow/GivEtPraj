﻿using Commentor.GivEtPraj.Domain.Enums;
using Commentor.GivEtPraj.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Commentor.GivEtPraj.Application.Cases.Commands;

public class CreateCaseCommand : IRequest<OneOf<int, InvalidCategory>>
{

    public CreateCaseCommand()
    {
    }

    public CreateCaseCommand(string description, List<Stream> images, string category, double longitude,
        double latitude,
        Priority priority, IPAddress ipAddress)
    {
        Description = description;
        Images = images;
        Category = category;
        Longitude = longitude;
        Latitude = latitude;
        Priority = priority;
        IpAddress = ipAddress;
    }

    public string Description { get; set; }
    public List<Stream> Images { get; set; } = new();
    public string Category { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public Priority Priority { get; set; }
    public IPAddress IpAddress { get; set; }
}

public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, OneOf<int, InvalidCategory>>
{
    private readonly IAppDbContext _db;
    private readonly IImageStorage _imageStorage;
    private readonly IMapper _mapper;

    public CreateCaseCommandHandler(IAppDbContext db, IMapper mapper, IImageStorage imageStorage)
    {
        _db = db;
        _mapper = mapper;
        _imageStorage = imageStorage;
    }

    public async Task<OneOf<int, InvalidCategory>>
        Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        var category = await _db.Categories
            .FirstOrDefaultAsync(c => request.Category == c.Name, cancellationToken);
        if (category is null) return new InvalidCategory(request.Category);

        var images = await CreateImages(request);

        var newCase = new Case
        {
            Description = request.Description,
            Pictures = images,
            Category = category,
            GeographicLocation = GeographicLocation.From(request.Latitude, request.Longitude),
            Priority = request.Priority,
            IpAddress = request.IpAddress
        };

        _db.Cases.Add(newCase);
        await _db.SaveChangesAsync(cancellationToken);

        return newCase.Id;
    }

    private async Task<List<CaseImage>> CreateImages(CreateCaseCommand request)
    {
        var images = new List<CaseImage>();
        var list = new List<(Stream Image, Guid Id)>();
        foreach (var image in request.Images)
        {
            var guid = Guid.NewGuid();
            list.Add((image, guid));
            images.Add(new()
            {
                Id = guid
            });
        }

        await UploadImages(list);

        return images;
    }

    private async ValueTask UploadImages(IReadOnlyCollection<(Stream Image, Guid Id)> images)
    {
        if (images.Count == 0) return;

        var imageUploads = images.Select(img => _imageStorage.UploadImage($"{img.Id}.jpg", img.Image));

        await Task.WhenAll(imageUploads);
    }
}

public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
{
    public CreateCaseCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(4096);

        RuleFor(x => x.Longitude)
            .LessThanOrEqualTo(180)
            .GreaterThanOrEqualTo(-180);

        RuleFor(x => x.Latitude)
            .LessThanOrEqualTo(90)
            .GreaterThanOrEqualTo(-90);

        RuleFor(x => x.Category)
            .NotEmpty();

        RuleForEach(x => x.Images)
            .NotEmpty();

        RuleFor(x => x.Priority)
            .IsInEnum();

        RuleFor(x => x.IpAddress)
            .NotNull()
            .Must(x => ValidateIP(x.ToString()));
    }

    private bool ValidateIP(string ipString) => IPAddress.TryParse(ipString, out _);
}