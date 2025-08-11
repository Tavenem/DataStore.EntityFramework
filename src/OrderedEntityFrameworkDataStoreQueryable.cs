using System.Linq.Expressions;
using Tavenem.DataStorage.Interfaces;

namespace Tavenem.DataStorage.EntityFramework;

/// <summary>
/// Provides LINQ operations on an <see cref="EntityFrameworkDataStore"/>, after an ordering operation.
/// </summary>
public class OrderedEntityFrameworkDataStoreQueryable<TSource>(IDataStore provider, IOrderedQueryable<TSource> source)
    : EntityFrameworkDataStoreQueryable<TSource>(provider, source),
    IOrderedDataStoreQueryable<TSource>
    where TSource : notnull
{
    /// <inheritdoc />
    public IOrderedDataStoreQueryable<TSource> ThenBy<TKey>(Expression<Func<TSource, TKey>> keySelector, IComparer<TKey>? comparer = null)
        => new OrderedEntityFrameworkDataStoreQueryable<TSource>(Provider, source.ThenBy(keySelector, comparer));

    /// <inheritdoc />
    public IOrderedDataStoreQueryable<TSource> ThenByDescending<TKey>(Expression<Func<TSource, TKey>> keySelector, IComparer<TKey>? comparer = null)
        => new OrderedEntityFrameworkDataStoreQueryable<TSource>(Provider, source.ThenByDescending(keySelector, comparer));
}
