using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization.Metadata;

namespace Tavenem.DataStorage.EntityFramework;

/// <summary>
/// A data store for <typeparamref name="TItem"/> instances backed by Entity Framework (Core).
/// </summary>
/// <typeparam name="TKey">The type of primary key for all stored items.</typeparam>
/// <typeparam name="TItem">A shared interface for all stored items.</typeparam>
/// <param name="context">The <see cref="DbContext"/> used for all transactions.</param>
public abstract class EntityFrameworkDataStore<TKey, TItem>(DbContext context) : EntityFrameworkDataStore<TItem>(context), IDataStore<TKey, TItem>
    where TKey : notnull
    where TItem : class
{
    /// <inheritdoc />
    public abstract TKey? CreateNewIdFor<T>() where T : TItem;

    /// <inheritdoc />
    public abstract TKey? CreateNewIdFor(Type type);

    /// <inheritdoc />
    public async ValueTask<T?> GetItemAsync<T>(TKey? id, CancellationToken cancellationToken = default) where T : class, TItem
    {
        if (id is null)
        {
            return default;
        }
        return await Context.FindAsync<T>(id, cancellationToken).ConfigureAwait(false);
    }

    [DoesNotReturn]
    ValueTask<T?> IDataStore<TKey, TItem>.GetItemAsync<T>(TKey? id, TimeSpan? cacheTimeout, CancellationToken cancellationToken) where T : default
        => throw new NotImplementedException("EntityFramework does not support getting with the IDataStore<TKey, TItem> interface constraint (TItem). Use the EntityFrameworkDataStore.GetItemAsync<T>() method instead, which uses the supported constraints (class, TItem).");

    [DoesNotReturn]
    ValueTask<T?> IDataStore<TKey, TItem>.GetItemAsync<T>(
        TKey? id,
        JsonTypeInfo<T>? typeInfo,
        TimeSpan? cacheTimeout,
        CancellationToken cancellationToken) where T : default
        => throw new NotImplementedException("EntityFramework does not support getting with the IDataStore<TKey, TItem> interface constraint (TItem). Use the EntityFrameworkDataStore.GetItemAsync<T>() method instead, which uses the supported constraints (class, TItem).");

    /// <inheritdoc />
    public abstract TKey GetKey<T>(T item) where T : TItem;

    /// <summary>
    /// Removes the stored item with the given id.
    /// </summary>
    /// <param name="id">
    /// <para>
    /// The id of the item to remove.
    /// </para>
    /// <para>
    /// If <see langword="null"/> or empty no operation takes place, and <see langword="true"/>
    /// is returned to indicate that there was no failure.
    /// </para>
    /// </param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// <see langword="true"/> if the item was successfully removed; otherwise <see
    /// langword="false"/>.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    /// If the <see cref="CancellationToken"/> is cancelled.
    /// </exception>
    public abstract ValueTask<bool> RemoveItemByKeyAsync<T>(TKey? id, CancellationToken cancellationToken = default) where T : class, TItem;

    [DoesNotReturn]
    ValueTask<bool> IDataStore<TKey, TItem>.RemoveItemAsync<T>(TKey? id, CancellationToken cancellationToken) where T : default
        => throw new NotImplementedException("EntityFramework does not support deleting with the IDataStore<TKey, TItem> interface constraint (TItem). Use the EntityFrameworkDataStore.RemoveItemByKeyAsync<T>() method instead, which uses the supported constraints (class, TItem).");
}
