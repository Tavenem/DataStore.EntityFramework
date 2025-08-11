using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.Json.Serialization.Metadata;
using Tavenem.DataStorage.Interfaces;

namespace Tavenem.DataStorage.EntityFramework;

/// <summary>
/// Provides LINQ operations on a <see cref="EntityFrameworkDataStore"/>'s data.
/// </summary>
/// <typeparam name="TSource">
/// The type of the elements of the source.
/// </typeparam>
public class EntityFrameworkDataStoreQueryable<TSource>(IDataStore provider, IQueryable<TSource> source)
    : IDataStoreDistinctByQueryable<TSource>,
    IDataStoreDistinctQueryable<TSource>,
    IDataStoreFirstQueryable<TSource>,
    IDataStoreGroupByQueryable<TSource>,
    IDataStoreGroupJoinQueryable<TSource>,
    IDataStoreIntersectByQueryable<TSource>,
    IDataStoreIntersectQueryable<TSource>,
    IDataStoreJoinQueryable<TSource>,
    IDataStoreLastQueryable<TSource>,
    IDataStoreOfTypeQueryable<TSource>,
    IDataStoreOrderableQueryable<TSource>,
    IDataStoreReverseQueryable<TSource>,
    IDataStoreSelectManyQueryable<TSource>,
    IDataStoreSelectQueryable<TSource>,
    IDataStoreSkipLastQueryable<TSource>,
    IDataStoreSkipQueryable<TSource>,
    IDataStoreSkipWhileQueryable<TSource>,
    IDataStoreTakeLastQueryable<TSource>,
    IDataStoreTakeQueryable<TSource>,
    IDataStoreTakeWhileQueryable<TSource>,
    IDataStoreUnionByQueryable<TSource>,
    IDataStoreUnionQueryable<TSource>,
    IDataStoreWhereQueryable<TSource>,
    IDataStoreZipQueryable<TSource>
    where TSource : notnull
{
    /// <inheritdoc />
    public IDataStore Provider { get; } = provider;

    /// <summary>
    /// Determines whether all the elements of a sequence satisfy a condition.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// <see langword="true"/> if all the elements of a sequence satisfy a condition; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public async ValueTask<bool> AllAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
        => await source.AllAsync(predicate, cancellationToken);

    /// <summary>
    /// Determines whether a sequence contains any elements.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// <see langword="true"/> if the source sequence contains any elements; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    public async ValueTask<bool> AnyAsync(CancellationToken cancellationToken)
        => await source.AnyAsync(cancellationToken);

    /// <summary>
    /// Determines whether any element of a sequence satisfies a condition.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// <see langword="true"/>> if the source sequence is not empty and at least one of its elements
    /// passes the test in the specified predicate; otherwise, <see langword="false"/>.
    /// </returns>
    public async ValueTask<bool> AnyAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
        => await source.AnyAsync(predicate, cancellationToken);

    /// <summary>
    /// Determines whether a sequence contains a specified element.
    /// </summary>
    /// <param name="value">The value to locate in the sequence.</param>
    /// <param name="comparer">Ignored. Not supported by EntityFramework.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// <see langword="true"/> if the source sequence contains an element that has the specified
    /// value; otherwise, <see langword="false"/>.
    /// </returns>
#pragma warning disable IDE0060 // Remove unused parameter. Provided to match extension on IAsyncEnumerable<T> so that this implementation takes precedence.
    public async ValueTask<bool> ContainsAsync(TSource value, IEqualityComparer<TSource>? comparer = null, CancellationToken cancellationToken = default)
        => await source.ContainsAsync(value, cancellationToken);
#pragma warning restore IDE0060 // Remove unused parameter

    /// <summary>
    /// Returns the number of elements in this source.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>The number of elements in the this source.</returns>
    /// <exception cref="OverflowException">
    /// The number of elements in this source is larger than <see cref="int.MaxValue"/>.
    /// </exception>
    public async ValueTask<int> CountAsync(CancellationToken cancellationToken)
        => await source.CountAsync(cancellationToken);

    /// <summary>
    /// Returns the number of elements in this source that satisfy a condition.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>The number of elements in the this source.</returns>
    /// <exception cref="OverflowException">
    /// The number of elements in this source that satisfy the condition is larger than <see
    /// cref="int.MaxValue"/>.
    /// </exception>
    public async ValueTask<int> CountAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
        => await source.CountAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public IDataStoreDistinctByQueryable<TSource> DistinctBy<TKey>(Expression<Func<TSource, TKey>> keySelector, IEqualityComparer<TKey>? comparer = null)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.DistinctBy(keySelector, comparer));

    /// <inheritdoc />
    public IDataStoreDistinctQueryable<TSource> Distinct(IEqualityComparer<TSource>? comparer = null)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Distinct(comparer));

    /// <summary>
    /// Returns the element at a specified index in a sequence.
    /// </summary>
    /// <param name="index">The index of the element to retrieve.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" />.</param>
    /// <returns>
    /// The element at the specified position in this sequence.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index" /> is outside the bounds of this sequence.
    /// </exception>
    public async ValueTask<TSource> ElementAtAsync(int index, CancellationToken cancellationToken = default)
        => await source.ElementAtAsync(index, cancellationToken);

    /// <summary>
    /// Returns the element at a specified index in a sequence.
    /// </summary>
    /// <param name="index">
    /// The index of the element to retrieve, which is either from the start or the end.
    /// </param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" />.</param>
    /// <returns>
    /// The element at the specified position in this sequence.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="index" /> is outside the bounds of this sequence.
    /// </exception>
    public async ValueTask<TSource> ElementAtAsync(Index index, CancellationToken cancellationToken = default)
        => await source.ElementAtAsync(
            index.IsFromEnd
                ? index.GetOffset(await source.CountAsync(cancellationToken))
                : index.Value,
            cancellationToken);

    /// <summary>
    /// Returns the element at a specified index in a sequence, or a default value if the index is
    /// out of range.
    /// </summary>
    /// <param name="index">The index of the element to retrieve.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// The default value of <typeparamref name="TSource"/> if <paramref name="index"/> is outside
    /// the bounds of the source sequence; otherwise, the element at the specified position in the
    /// source sequence.
    /// </returns>
    public async ValueTask<TSource?> ElementAtOrDefaultAsync(int index, CancellationToken cancellationToken = default)
    {
        if (index < 0)
        {
            return default;
        }
        try
        {
            return await source.ElementAtOrDefaultAsync(index, cancellationToken);
        }
        catch (ArgumentOutOfRangeException)
        {
            return default;
        }
    }

    /// <summary>
    /// Returns the element at a specified index in a sequence, or a default value if the index is
    /// out of range.
    /// </summary>
    /// <param name="index">
    /// The index of the element to retrieve, which is either from the start or the end.
    /// </param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// The default value of <typeparamref name="TSource"/> if <paramref name="index"/> is outside
    /// the bounds of the source sequence; otherwise, the element at the specified position in the
    /// source sequence.
    /// </returns>
    public async ValueTask<TSource?> ElementAtOrDefaultAsync(Index index, CancellationToken cancellationToken = default)
    {
        var indexValue = index.IsFromEnd
            ? index.GetOffset(await source.CountAsync(cancellationToken))
            : index.Value;
        if (indexValue < 0)
        {
            return default;
        }
        try
        {
            return await source.ElementAtOrDefaultAsync(indexValue, cancellationToken);
        }
        catch (ArgumentOutOfRangeException)
        {
            return default;
        }
    }

    /// <inheritdoc />
    public async ValueTask<TSource> FirstAsync(CancellationToken cancellationToken = default)
        => await source.FirstAsync(cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource> FirstAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
        => await source.FirstAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource?> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
        => await source.FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource?> FirstOrDefaultAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
        => await source.FirstOrDefaultAsync(predicate, cancellationToken);

    /// <inheritdoc />
    IAsyncEnumerator<TSource> IAsyncEnumerable<TSource>.GetAsyncEnumerator(CancellationToken cancellationToken)
        => source.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

    /// <summary>
    /// Returns an <see cref="IEnumerator{T}"/> for this source. The enumerator provides a simple
    /// way to access all the contents of the collection.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>An <see cref="IEnumerable{T}" />.</returns>
    /// <exception cref="OperationCanceledException">
    /// If the <see cref="CancellationToken"/> is cancelled.
    /// </exception>
    public ValueTask<IEnumerator<TSource>> GetEnumeratorAsync(CancellationToken cancellationToken = default)
        => new(source.GetEnumerator());

    /// <inheritdoc />
    public async ValueTask<IPagedList<TSource>> GetPageAsync(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default) => (await source
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync(cancellationToken))
        .AsPagedList(pageNumber, pageSize, await source.LongCountAsync(cancellationToken));

    /// <inheritdoc />
    public IDataStoreGroupByQueryable<TResult> GroupBy<TKey, TResult>(
        Expression<Func<TSource, TKey>> keySelector,
        Expression<Func<TKey, IEnumerable<TSource>, TResult>> resultSelector,
        IEqualityComparer<TKey>? comparer = null) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.GroupBy(keySelector, resultSelector, comparer));

    /// <inheritdoc />
    public IDataStoreGroupByQueryable<TResult> GroupBy<TKey, TElement, TResult>(
        Expression<Func<TSource, TKey>> keySelector,
        Expression<Func<TSource, TElement>> elementSelector,
        Expression<Func<TKey, IEnumerable<TElement>, TResult>> resultSelector,
        IEqualityComparer<TKey>? comparer = null) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.GroupBy(keySelector, elementSelector, resultSelector, comparer));

    /// <inheritdoc />
    public IDataStoreGroupByQueryable<IGrouping<TKey, TSource>> GroupBy<TKey>(
        Expression<Func<TSource, TKey>> keySelector,
        IEqualityComparer<TKey>? comparer = null)
        => new EntityFrameworkDataStoreQueryable<IGrouping<TKey, TSource>>(Provider, source.GroupBy(keySelector, comparer));

    /// <inheritdoc />
    public IDataStoreGroupByQueryable<IGrouping<TKey, TElement>> GroupBy<TKey, TElement>(
        Expression<Func<TSource, TKey>> keySelector,
        Expression<Func<TSource, TElement>> elementSelector,
        IEqualityComparer<TKey>? comparer = null)
        => new EntityFrameworkDataStoreQueryable<IGrouping<TKey, TElement>>(Provider, source.GroupBy(keySelector, elementSelector, comparer));

    /// <inheritdoc />
    public IDataStoreGroupJoinQueryable<TResult> GroupJoin<TInner, TKey, TResult>(
        IEnumerable<TInner> inner,
        Expression<Func<TSource, TKey>> outerKeySelector,
        Expression<Func<TInner, TKey>> innerKeySelector,
        Expression<Func<TSource, IEnumerable<TInner>, TResult>> resultSelector,
        IEqualityComparer<TKey>? comparer = null) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.GroupJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer));

    /// <inheritdoc />
    public IDataStoreIntersectQueryable<TSource> Intersect(IEnumerable<TSource> source2, IEqualityComparer<TSource>? comparer = null)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Intersect(source2, comparer));

    /// <inheritdoc />
    public IDataStoreIntersectByQueryable<TSource> IntersectBy<TKey>(
        IEnumerable<TKey> source2,
        Expression<Func<TSource, TKey>> keySelector,
        IEqualityComparer<TKey>? comparer = null)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.IntersectBy(source2, keySelector, comparer));

    /// <inheritdoc />
    public IDataStoreJoinQueryable<TResult> Join<TInner, TKey, TResult>(
        IEnumerable<TInner> inner,
        Expression<Func<TSource, TKey>> outerKeySelector,
        Expression<Func<TInner, TKey>> innerKeySelector,
        Expression<Func<TSource, TInner, TResult>> resultSelector,
        IEqualityComparer<TKey>? comparer = null) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.Join(inner, outerKeySelector, innerKeySelector, resultSelector, comparer));

    /// <inheritdoc />
    public IDataStoreJoinQueryable<TResult> LeftJoin<TInner, TKey, TResult>(
        IEnumerable<TInner> inner,
        Expression<Func<TSource, TKey>> outerKeySelector,
        Expression<Func<TInner, TKey>> innerKeySelector,
        Expression<Func<TSource, TInner?, TResult>> resultSelector,
        IEqualityComparer<TKey>? comparer = null) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.LeftJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer));

    /// <inheritdoc />
    public IDataStoreJoinQueryable<TResult> RightJoin<TInner, TKey, TResult>(
        IEnumerable<TInner> inner,
        Expression<Func<TSource, TKey>> outerKeySelector,
        Expression<Func<TInner, TKey>> innerKeySelector,
        Expression<Func<TSource?, TInner, TResult>> resultSelector,
        IEqualityComparer<TKey>? comparer = null) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.RightJoin(inner, outerKeySelector, innerKeySelector, resultSelector, comparer));

    /// <inheritdoc />
    public async ValueTask<TSource> LastAsync(CancellationToken cancellationToken = default)
        => await source.LastAsync(cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource> LastAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
        => await source.LastAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource?> LastOrDefaultAsync(CancellationToken cancellationToken = default)
        => await source.LastOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource?> LastOrDefaultAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
        => await source.LastOrDefaultAsync(predicate, cancellationToken);

    /// <summary>
    /// Returns the number of elements in this source.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>The number of elements in the this source.</returns>
    /// <exception cref="OverflowException">
    /// The number of elements in this source is larger than <see cref="long.MaxValue"/>.
    /// </exception>
    public async ValueTask<long> LongCountAsync(CancellationToken cancellationToken)
        => await source.LongCountAsync(cancellationToken);

    /// <summary>
    /// Returns the number of elements in this source that satisfy a condition.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>The number of elements in the this source.</returns>
    /// <exception cref="OverflowException">
    /// The number of elements in this source that satisfy the condition is larger than <see
    /// cref="long.MaxValue"/>.
    /// </exception>
    public async ValueTask<long> LongCountAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken)
        => await source.LongCountAsync(predicate, cancellationToken);

    /// <summary>
    /// Returns the maximum value in a generic sequence.
    /// </summary>
    /// <param name="comparer">Ignored. Not supported by EntityFramework.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>The maximum value in the sequence.</returns>
#pragma warning disable IDE0060 // Remove unused parameter. Provided to match extension on IAsyncEnumerable<T> so that this implementation takes precedence.
    public async ValueTask<TSource?> MaxAsync(IComparer<TSource>? comparer = null, CancellationToken cancellationToken = default)
#pragma warning restore IDE0060 // Remove unused parameter
        => await source.MaxAsync(cancellationToken);

    /// <summary>
    /// Returns the minimum value in a generic sequence.
    /// </summary>
    /// <param name="comparer">Ignored. Not supported by EntityFramework.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>The minimum value in the sequence.</returns>
#pragma warning disable IDE0060 // Remove unused parameter. Provided to match extension on IAsyncEnumerable<T> so that this implementation takes precedence.
    public async ValueTask<TSource?> MinAsync(IComparer<TSource>? comparer = null, CancellationToken cancellationToken = default)
#pragma warning restore IDE0060 // Remove unused parameter
        => await source.MinAsync(cancellationToken);

    /// <inheritdoc />
    public IDataStoreOfTypeQueryable<TResult> OfType<TResult>(JsonTypeInfo<TResult>? typeInfo = null) where TResult : TSource
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.OfType<TResult>());

    /// <inheritdoc />
    public IOrderedDataStoreQueryable<TSource> Order(IComparer<TSource>? comparer = null)
        => comparer is null
        ? new OrderedEntityFrameworkDataStoreQueryable<TSource>(Provider, source.Order())
        : new OrderedEntityFrameworkDataStoreQueryable<TSource>(Provider, source.Order(comparer));

    /// <inheritdoc />
    public IOrderedDataStoreQueryable<TSource> OrderBy<TKey>(Expression<Func<TSource, TKey>> keySelector, IComparer<TKey>? comparer = null)
        => new OrderedEntityFrameworkDataStoreQueryable<TSource>(Provider, source.OrderBy(keySelector, comparer));

    /// <inheritdoc />
    public IOrderedDataStoreQueryable<TSource> OrderByDescending<TKey>(Expression<Func<TSource, TKey>> keySelector, IComparer<TKey>? comparer = null)
        => new OrderedEntityFrameworkDataStoreQueryable<TSource>(Provider, source.OrderByDescending(keySelector, comparer));

    /// <inheritdoc />
    public IOrderedDataStoreQueryable<TSource> OrderDescending(IComparer<TSource>? comparer = null)
        => comparer is null
        ? new OrderedEntityFrameworkDataStoreQueryable<TSource>(Provider, source.OrderDescending())
        : new OrderedEntityFrameworkDataStoreQueryable<TSource>(Provider, source.OrderDescending(comparer));

    /// <inheritdoc />
    public IDataStoreReverseQueryable<TSource> Reverse()
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Reverse());

    /// <inheritdoc />
    public async ValueTask<TSource> SingleAsync(CancellationToken cancellationToken = default)
        => await source.SingleAsync(cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource> SingleAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
        => await source.SingleAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource?> SingleOrDefaultAsync(CancellationToken cancellationToken = default)
        => await source.SingleOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public async ValueTask<TSource?> SingleOrDefaultAsync(Expression<Func<TSource, bool>> predicate, CancellationToken cancellationToken = default)
        => await source.SingleOrDefaultAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public IDataStoreSelectQueryable<TResult> Select<TResult>(Expression<Func<TSource, TResult>> selector) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.Select(selector));

    /// <inheritdoc />
    public IDataStoreSelectQueryable<TResult> Select<TResult>(Expression<Func<TSource, int, TResult>> selector) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.Select(selector));

    /// <inheritdoc />
    public IDataStoreSelectManyQueryable<TResult> SelectMany<TCollection, TResult>(
        Expression<Func<TSource, IEnumerable<TCollection>>> collectionSelector,
        Expression<Func<TSource, TCollection, TResult>> resultSelector) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.SelectMany(collectionSelector, resultSelector));

    /// <inheritdoc />
    public IDataStoreSelectManyQueryable<TResult> SelectMany<TCollection, TResult>(
        Expression<Func<TSource, int, IEnumerable<TCollection>>> collectionSelector,
        Expression<Func<TSource, TCollection, TResult>> resultSelector) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.SelectMany(collectionSelector, resultSelector));

    /// <inheritdoc />
    public IDataStoreSkipQueryable<TSource> Skip(int count)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Skip(count));

    /// <inheritdoc />
    public IDataStoreSkipLastQueryable<TSource> SkipLast(int count)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.SkipLast(count));

    /// <inheritdoc />
    public IDataStoreSkipWhileQueryable<TSource> SkipWhile(Expression<Func<TSource, bool>> predicate)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.SkipWhile(predicate));

    /// <inheritdoc />
    public IDataStoreSkipWhileQueryable<TSource> SkipWhile(Expression<Func<TSource, int, bool>> predicate)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.SkipWhile(predicate));

    /// <inheritdoc />
    public IDataStoreTakeQueryable<TSource> Take(int count)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Take(count));

    /// <inheritdoc />
    public IDataStoreTakeQueryable<TSource> Take(Range range)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Take(range));

    /// <inheritdoc />
    public IDataStoreTakeLastQueryable<TSource> TakeLast(int count)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.TakeLast(count));

    /// <inheritdoc />
    public IDataStoreTakeWhileQueryable<TSource> TakeWhile(Expression<Func<TSource, bool>> predicate)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.TakeWhile(predicate));

    /// <inheritdoc />
    public IDataStoreTakeWhileQueryable<TSource> TakeWhile(Expression<Func<TSource, int, bool>> predicate)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.TakeWhile(predicate));

    /// <summary>
    /// Creates an array from this source.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>An array that contains the elements from the this source.</returns>
    public async ValueTask<TSource[]> ToArrayAsync(CancellationToken cancellationToken = default)
        => await source.ToArrayAsync(cancellationToken);

    /// <summary>
    /// Creates a <see cref="Dictionary{TKey, TValue}"/> from this source
    /// according to a specified key selector function.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys returned by <paramref name="keySelector"/>.</typeparam>
    /// <param name="keySelector">A function to extract a key from each element.</param>
    /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare keys.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A <see cref="Dictionary{TKey, TValue}"/> that contains keys and values.</returns>
    /// <exception cref="ArgumentException">this source contains one or more duplicate keys (via the returned task).</exception>
    public async ValueTask<Dictionary<TKey, TSource>> ToDictionaryAsync<TKey>(
        Func<TSource, TKey> keySelector,
        IEqualityComparer<TKey>? comparer = null,
        CancellationToken cancellationToken = default)
        where TKey : notnull
        => comparer is null
        ? await source.ToDictionaryAsync(keySelector, cancellationToken)
        : await source.ToDictionaryAsync(keySelector, comparer, cancellationToken);

    /// <summary>
    /// Creates a <see cref="HashSet{T}"/> from this source.
    /// </summary>
    /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare keys.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// A <see cref="HashSet{T}"/> that contains values selected from this source.
    /// </returns>
    public async ValueTask<HashSet<TSource>> ToHashSetAsync(IEqualityComparer<TSource>? comparer = null, CancellationToken cancellationToken = default)
        => await source.ToHashSetAsync(comparer, cancellationToken);

    /// <summary>
    /// Creates a list from this source.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/>.</param>
    /// <returns>A list that contains the elements from this source.</returns>
    public async ValueTask<List<TSource>> ToListAsync(CancellationToken cancellationToken = default)
        => await source.ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async ValueTask<(bool Success, int Count)> TryGetNonEnumeratedCountAsync(CancellationToken cancellationToken = default)
    {
        var result = await source.CountAsync(cancellationToken);
        return (true, result);
    }

    /// <inheritdoc />
    public async ValueTask<(bool Success, long Count)> TryGetNonEnumeratedLongCountAsync(CancellationToken cancellationToken = default)
    {
        var result = await source.LongCountAsync(cancellationToken);
        return (true, result);
    }

    /// <inheritdoc />
    public IDataStoreUnionQueryable<TSource> Union(IEnumerable<TSource> source2, IEqualityComparer<TSource>? comparer = null)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Union(source2, comparer));

    /// <inheritdoc />
    public IDataStoreUnionByQueryable<TSource> UnionBy<TKey>(
        IEnumerable<TSource> source2,
        Expression<Func<TSource, TKey>> keySelector,
        IEqualityComparer<TKey>? comparer = null)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.UnionBy(source2, keySelector, comparer));

    /// <inheritdoc />
    public IDataStoreWhereQueryable<TSource> Where(Expression<Func<TSource, bool>> predicate)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Where(predicate));

    /// <inheritdoc />
    public IDataStoreWhereQueryable<TSource> Where(Expression<Func<TSource, int, bool>> predicate)
        => new EntityFrameworkDataStoreQueryable<TSource>(Provider, source.Where(predicate));

    /// <inheritdoc />
    public IDataStoreZipQueryable<TResult> Zip<TSecond, TResult>(
        IEnumerable<TSecond> source2,
        Expression<Func<TSource, TSecond, TResult>> resultSelector) where TResult : notnull
        => new EntityFrameworkDataStoreQueryable<TResult>(Provider, source.Zip(source2, resultSelector));

    /// <inheritdoc />
    public IDataStoreZipQueryable<(TSource First, TSecond Second)> Zip<TSecond>(IEnumerable<TSecond> source2)
        => new EntityFrameworkDataStoreQueryable<(TSource First, TSecond Second)>(Provider, source.Zip(source2));

    /// <inheritdoc />
    public IDataStoreZipQueryable<(TSource First, TSecond Second, TThird Third)> Zip<TSecond, TThird>(IEnumerable<TSecond> source2, IEnumerable<TThird> source3)
        => new EntityFrameworkDataStoreQueryable<(TSource First, TSecond Second, TThird Third)>(Provider, source.Zip(source2, source3));
}
