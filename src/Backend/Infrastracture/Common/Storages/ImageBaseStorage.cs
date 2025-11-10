using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Storages;

public class ImageBaseStorage : IImageBaseStorage
{
    private readonly ContextBase1 _contextBase1;

    public ImageBaseStorage(
        ContextBase1 contextBase1)
    {
        _contextBase1 = contextBase1;
    }

    public async Task<ImageBase> CreateImageAsync(Guid id, string description, string filePath,
        CancellationToken cancellationToken)
    {
        await _contextBase1.Images.AddAsync(new ImageBase
        {
            Id = id,
            Description = description,
            FilePath = filePath,
            CreatedAt = DateTimeOffset.UtcNow,
            IsCopied = false
        }, cancellationToken);

        await _contextBase1.SaveChangesAsync(cancellationToken);

        return await _contextBase1.Images
            .AsNoTracking()
            .FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ImageBase>> GetAllImagesAsync(CancellationToken cancellationToken)
    {
        return await _contextBase1.Images
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ImageBase?> GetImageAsync(Guid id,
        CancellationToken cancellationToken)
    {
        return await _contextBase1.Images
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetImageCopiedFlagAsync(Guid id, bool flag, CancellationToken cancellationToken)
    {
        await _contextBase1.Images
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(c => c.SetProperty(x => x.IsCopied, flag), cancellationToken);
    }

    public async Task RemoveImageAsync(Guid id, CancellationToken cancellationToken)
    {
        _contextBase1.Images.Remove(new ImageBase
        {
            Id = id
        });

        await _contextBase1.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateImageAsync(Guid id, string description,
        CancellationToken cancellationToken)
    {
        await _contextBase1.Images
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(c => c.SetProperty(x => x.Description, description), cancellationToken);
    }
}