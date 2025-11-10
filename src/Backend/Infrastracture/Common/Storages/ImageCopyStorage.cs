using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Storages;

public class ImageCopyStorage : IImageCopyStorage
{
    private readonly ContextBase2 _contextBase2;

    public ImageCopyStorage(
        ContextBase2 contextBase2)
    {
        _contextBase2 = contextBase2;
    }

    public async Task<ImageCopy> CopyImageBaseAsync(Guid sourceId, string sourceDescription, byte[] blob,
        CancellationToken cancellationToken)
    {
        var imageId = Guid.NewGuid();

        await _contextBase2.ImagesCopies.AddAsync(new ImageCopy
        {
            Id = imageId,
            SourceId = sourceId,
            Blob = blob,
            SourceDescription = sourceDescription
        }, cancellationToken);

        await _contextBase2.SaveChangesAsync(cancellationToken);

        return await _contextBase2.ImagesCopies
            .AsNoTracking()
            .FirstAsync(x => x.Id == imageId, cancellationToken);
    }


    public Task<ImageCopy?> GetImageAsync(Guid id, CancellationToken cancellationToken)
    {
        return _contextBase2.ImagesCopies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SourceId == id, cancellationToken);
    }

    public async Task RemoveImageAsync(Guid id, CancellationToken cancellationToken)
    {
        _contextBase2.Remove(new ImageCopy
        {
            Id = id
        });

        await _contextBase2.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateImageCopyAsync(Guid id, string description, CancellationToken cancellationToken)
    {
        await _contextBase2.ImagesCopies
            .Where(c => c.Id == id)
            .ExecuteUpdateAsync(c => c.SetProperty(x => x.SourceDescription, description), cancellationToken);
    }
}