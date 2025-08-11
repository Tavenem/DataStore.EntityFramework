using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Tavenem.DataStorage.EntityFramework;

/// <summary>
/// A data store for <see cref="IIdItem"/> instances backed by Entity Framework (Core).
/// </summary>
/// <param name="context">The <see cref="DbContext"/> used for all transactions.</param>
public class EntityFrameworkDataStore(DbContext context) : EntityFrameworkDataStore<string, IIdItem>(context), IIdItemDataStore
{
    /// <inheritdoc />
    public override string CreateNewIdFor<T>() => Guid.NewGuid().ToString();

    /// <inheritdoc />
    public override string CreateNewIdFor(Type type) => Guid.NewGuid().ToString();

    /// <inheritdoc />
    public override string GetKey<T>(T item) => item.Id;

    /// <inheritdoc />
    public override async ValueTask<bool> RemoveItemByKeyAsync<T>(string? id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            return false;
        }
        var entity = await Context.FindAsync<T>(id).ConfigureAwait(false);
        if (entity is null)
        {
            return true;
        }
        Context.Remove(entity);
        await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return true;
    }

    /// <inheritdoc />
    public override async ValueTask<T?> StoreItemAsync<T>(T? item, CancellationToken cancellationToken = default) where T : class
    {
        if (item is null)
        {
            return item;
        }
        var key = GetKey(item);
        if (key is null)
        {
            return default;
        }
        var entity = Context.Find<T>(GetKey(item));
        EntityEntry<T>? entry;
        if (entity is null)
        {
            entry = await Context.AddAsync(item, cancellationToken).ConfigureAwait(false);
        }
        else
        {
            entry = Context.Update(item);
        }
        await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return entry.Entity;
    }
}
