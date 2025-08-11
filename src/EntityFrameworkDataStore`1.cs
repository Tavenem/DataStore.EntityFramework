using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;
using Tavenem.DataStorage.Interfaces;

namespace Tavenem.DataStorage.EntityFramework;

/// <summary>
/// A data store for <typeparamref name="TItem"/> instances backed by Entity Framework (Core).
/// </summary>
/// <typeparam name="TItem">A shared interface for all stored items.</typeparam>
/// <param name="context">The <see cref="DbContext"/> used for all transactions.</param>
public abstract class EntityFrameworkDataStore<TItem>(DbContext context) : IDataStore<TItem> where TItem : class
{
    /// <summary>
    /// The <see cref="DbContext"/> used for all transactions.
    /// </summary>
    public DbContext Context { get; set; } = context;

    /// <inheritdoc />
    public TimeSpan DefaultCacheTimeout { get; set; }

    /// <summary>
    /// <para>
    /// Indicates whether this <see cref="IDataStore"/> implementation allows items to be
    /// cached.
    /// </para>
    /// <para>
    /// This is <see langword="false"/> for an <see cref="EntityFrameworkDataStore"/>, which
    /// utilizes its own caching strategy.
    /// </para>
    /// </summary>
    public bool SupportsCaching => false;

    /// <inheritdoc />
    public IDataStoreQueryable<T> Query<T>() where T : class, TItem
    => new EntityFrameworkDataStoreQueryable<T>(this, Context.Set<T>().AsQueryable());

    [DoesNotReturn]
    IDataStoreQueryable<T> IDataStore<TItem>.Query<T>(JsonTypeInfo<T>? typeInfo)
        => throw new NotImplementedException("EntityFramework does not support a query with the IDataStore<TItem> interface constraints (notnull, TItem). Use the EntityFrameworkDataStore.Query<T>() method instead, which uses the supported constraints (class, TItem).");

    /// <inheritdoc />
    public async ValueTask<bool> RemoveItemAsync<T>(T? item, CancellationToken cancellationToken = default) where T : TItem
    {
        if (item is null)
        {
            return true;
        }
        Context.Remove(item);
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public abstract ValueTask<T?> StoreItemAsync<T>(T? item, CancellationToken cancellationToken = default) where T : class, TItem;

    [DoesNotReturn]
    ValueTask<T?> IDataStore<TItem>.StoreItemAsync<T>(T? item, TimeSpan? cacheTimeout, CancellationToken cancellationToken) where T : default
        => throw new NotImplementedException("EntityFramework does not support saving with the IDataStore<TItem> interface constraints (notnull, TItem). Use the EntityFrameworkDataStore.StoreItemAsync<T>() method instead, which uses the supported constraints (class, TItem).");

    [DoesNotReturn]
    ValueTask<T?> IDataStore<TItem>.StoreItemAsync<T>(T? item, JsonTypeInfo<T>? typeInfo, TimeSpan? cacheTimeout, CancellationToken cancellationToken) where T : default
        => throw new NotImplementedException("EntityFramework does not support saving with the IDataStore<TItem> interface constraints (notnull, TItem). Use the EntityFrameworkDataStore.StoreItemAsync<T>() method instead, which uses the supported constraints (class, TItem).");
}
