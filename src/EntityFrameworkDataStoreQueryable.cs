﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tavenem.DataStorage.EntityFramework;

/// <summary>
/// Provides LINQ operations on a <see cref="EntityFrameworkDataStore"/>.
/// </summary>
public class EntityFrameworkDataStoreQueryable<T> : IDataStoreQueryable<T>
{
    private protected readonly IQueryable<T> _source;

    /// <summary>
    /// Initializes a new instance of <see cref="EntityFrameworkDataStoreQueryable{T}"/>.
    /// </summary>
    public EntityFrameworkDataStoreQueryable(IQueryable<T> source) => _source = source;

    /// <summary>
    /// Determines whether this <see cref="IDataStoreQueryable{T}"/> contains any elements.
    /// </summary>
    /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise,
    /// <see langword="false"/>.</returns>
    public bool Any() => _source.Any();

    /// <summary>
    /// Determines whether any element of this <see cref="IDataStoreQueryable{T}"/> satisfies a
    /// condition.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns><see langword="true"/> if any elements in the source sequence pass the test in
    /// the specified predicate; otherwise,
    /// <see langword="false"/>.</returns>
    public bool Any(Expression<Func<T, bool>> predicate) => _source.Any(predicate);

    /// <summary>
    /// Asynchronously determines whether this <see cref="IDataStoreQueryable{T}"/> contains any
    /// elements.
    /// </summary>
    /// <returns><see langword="true"/> if the source sequence contains any elements; otherwise,
    /// <see langword="false"/>.</returns>
    public Task<bool> AnyAsync() => _source.AnyAsync();

    /// <summary>
    /// Asynchronously determines whether any element of this <see
    /// cref="IDataStoreQueryable{T}"/> satisfies a condition.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns><see langword="true"/> if any elements in the source sequence pass the test in
    /// the specified predicate; otherwise,
    /// <see langword="false"/>.</returns>
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _source.AnyAsync(predicate);

    /// <summary>
    /// Asynchronously determines whether any element of this <see
    /// cref="IDataStoreQueryable{T}"/> satisfies a condition.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns><see langword="true"/> if any elements in the source sequence pass the test in
    /// the specified predicate; otherwise,
    /// <see langword="false"/>.</returns>
    public async Task<bool> AnyAsync(Func<T, ValueTask<bool>> predicate)
    {
        foreach (var item in _source)
        {
            if (await predicate.Invoke(item).ConfigureAwait(false))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Enumerates the results of this <see cref="IDataStoreQueryable{T}" /> as an asynchronous
    /// operation.
    /// </summary>
    /// <returns>An <see cref="IAsyncEnumerable{T}" />.</returns>
    public IAsyncEnumerable<T> AsAsyncEnumerable() => _source.AsAsyncEnumerable();

    /// <summary>
    /// Enumerates the results of this <see cref="IDataStoreQueryable{T}" />.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{T}" />.</returns>
    public IEnumerable<T> AsEnumerable() => _source;

    /// <summary>
    /// Returns the number of elements in this <see cref="IDataStoreQueryable{T}"/>.
    /// </summary>
    /// <returns>The number of elements in this <see cref="IDataStoreQueryable{T}"/>.</returns>
    /// <exception cref="OverflowException">
    /// The number of elements in source is larger than <see cref="int.MaxValue"/>.
    /// </exception>
    public int Count() => _source.Count();

    /// <summary>
    /// Asynchronously returns the number of elements in this <see cref="IDataStoreQueryable{T}"/>.
    /// </summary>
    /// <returns>The number of elements in this <see cref="IDataStoreQueryable{T}"/>.</returns>
    /// <exception cref="OverflowException">
    /// The number of elements in source is larger than <see cref="int.MaxValue"/>.
    /// </exception>
    public Task<int> CountAsync() => _source.CountAsync();

    /// <summary>
    /// Returns the first element of this <see cref="IDataStoreQueryable{T}" />, or a default
    /// value if the sequence contains no elements.
    /// </summary>
    /// <returns>
    /// The first element in this <see cref="IDataStoreQueryable{T}" />, or a default value if
    /// the sequence contains no elements.
    /// </returns>
    public T? FirstOrDefault() => _source.FirstOrDefault();

    /// <summary>
    /// Returns the first element of this <see cref="IDataStoreQueryable{T}"/> that satisfies a
    /// specified condition or a default value if no such element is found.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>
    /// default(TSource) if this <see cref="IDataStoreQueryable{T}"/> is empty or if no element
    /// passes the test specified by <paramref name="predicate"/>; otherwise, the first element
    /// in source that passes the test specified by <paramref name="predicate"/>.
    /// </returns>
    public T? FirstOrDefault(Expression<Func<T, bool>> predicate) => _source.FirstOrDefault(predicate);

    /// <summary>
    /// Returns the first element of this <see cref="IDataStoreQueryable{T}" />, or a default
    /// value if the sequence contains no elements, asynchronously.
    /// </summary>
    /// <returns>
    /// The first element in this <see cref="IDataStoreQueryable{T}" />, or a default value if
    /// the sequence contains no elements.
    /// </returns>
    public async Task<T?> FirstOrDefaultAsync() => await _source.FirstOrDefaultAsync().ConfigureAwait(false);

    /// <summary>
    /// Asynchronously returns the first element of this <see cref="IDataStoreQueryable{T}"/>
    /// that satisfies a specified condition or a default value if no such element is found.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>
    /// default(TSource) if this <see cref="IDataStoreQueryable{T}"/> is empty or if no element
    /// passes the test specified by <paramref name="predicate"/>; otherwise, the first element
    /// in source that passes the test specified by <paramref name="predicate"/>.
    /// </returns>
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate) => await _source.FirstOrDefaultAsync(predicate).ConfigureAwait(false);

    /// <summary>
    /// Asynchronously returns the first element of this <see cref="IDataStoreQueryable{T}"/>
    /// that satisfies a specified condition or a default value if no such element is found.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>
    /// default(TSource) if this <see cref="IDataStoreQueryable{T}"/> is empty or if no element
    /// passes the test specified by <paramref name="predicate"/>; otherwise, the first element
    /// in source that passes the test specified by <paramref name="predicate"/>.
    /// </returns>
    public async Task<T?> FirstOrDefaultAsync(Func<T, ValueTask<bool>> predicate)
    {
        await foreach (var item in _source.AsAsyncEnumerable())
        {
            if (await predicate.Invoke(item).ConfigureAwait(false))
            {
                return item;
            }
        }
        return default!;
    }

    /// <summary>
    /// Gets a number of items from this <see cref="IDataStoreQueryable{T}" /> equal to
    /// <paramref name="pageSize" />, after skipping <paramref name="pageNumber" />-1 multiples of
    /// that amount.
    /// </summary>
    /// <param name="pageNumber">The current page number. The first page is 1.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>An <see cref="IPagedList{T}" /> of items from this <see
    /// cref="IDataStoreQueryable{T}" />.</returns>
    public IPagedList<T> GetPage(int pageNumber, int pageSize) => _source
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .AsPagedList(pageNumber, pageSize, _source.LongCount());

    /// <summary>
    /// Asynchronously gets a number of items from this <see cref="IDataStoreQueryable{T}"/>
    /// equal to <paramref name="pageSize"/>, after skipping <paramref name="pageNumber"/>-1
    /// multiples of that amount.
    /// </summary>
    /// <param name="pageNumber">The current page number. The first page is 1.</param>
    /// <param name="pageSize">The page size.</param>
    /// <returns>An <see cref="IPagedList{T}"/> of items from this <see
    /// cref="IDataStoreQueryable{T}"/>.</returns>
    public Task<IPagedList<T>> GetPageAsync(int pageNumber, int pageSize) => _source
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .AsPagedListAsync(pageNumber, pageSize, _source.LongCount());

    /// <summary>
    /// Returns the maximum value of this <see cref="IDataStoreQueryable{T}"/>.
    /// </summary>
    /// <returns>The maximum value of this <see cref="IDataStoreQueryable{T}"/>.</returns>
    public T? Max() => _source.Max();

    /// <summary>
    /// Asynchronously returns the maximum value of this <see cref="IDataStoreQueryable{T}"/>.
    /// </summary>
    /// <returns>
    /// The maximum value of this <see cref="IDataStoreQueryable{T}"/>.
    /// </returns>
    public async Task<T?> MaxAsync() => await _source.MaxAsync().ConfigureAwait(false);

    /// <summary>
    /// Returns the minimum value of this <see cref="IDataStoreQueryable{T}"/>.
    /// </summary>
    /// <returns>The minimum value of this <see cref="IDataStoreQueryable{T}"/>.</returns>
    public T? Min() => _source.Min();

    /// <summary>
    /// Asynchronously returns the minimum value of this <see cref="IDataStoreQueryable{T}"/>.
    /// </summary>
    /// <returns>
    /// The minimum value of this <see cref="IDataStoreQueryable{T}"/>.
    /// </returns>
    public async Task<T?> MinAsync() => await _source.MinAsync().ConfigureAwait(false);

    /// <summary>
    /// Filters the elements of this <see cref="IDataStoreQueryable{T}"/> based on a specified
    /// type.
    /// </summary>
    /// <typeparam name="TResult">The type to filter the elements of the sequence
    /// on.</typeparam>
    /// <returns>
    /// A collection that contains the elements from source that have type <typeparamref
    /// name="TResult"/>.
    /// </returns>
    public IDataStoreQueryable<TResult> OfType<TResult>()
        => new EntityFrameworkDataStoreQueryable<TResult>(_source.OfType<TResult>());

    /// <summary>
    /// Sorts the elements of this <see cref="IDataStoreQueryable{T}" /> in ascending order
    /// according to a key.
    /// </summary>
    /// <param name="keySelector">A function to extract a key from an element.</param>
    /// <param name="descending">Whether results will be ordered in descending order.</param>
    /// <returns>An <see cref="IDataStoreQueryable{T}" />.</returns>
    public IOrderedDataStoreQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector, bool descending = false)
        => new OrderedEntityFrameworkDataStoreQueryable<T>(descending
            ? _source.OrderByDescending(keySelector)
            : _source.OrderBy(keySelector));

    /// <summary>
    /// Projects each element of this <see cref="IDataStoreQueryable{T}"/> into a new form.
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the function represented by
    /// selector.</typeparam>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <returns>
    /// An <see cref="IDataStoreQueryable{T}"/> whose elements are the result of invoking a
    /// projection function on each element of this <see cref="IDataStoreQueryable{T}"/>.
    /// </returns>
    public IDataStoreQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector)
        => new EntityFrameworkDataStoreQueryable<TResult>(_source.Select(selector));

    /// <summary>
    /// Projects each element of this <see cref="IDataStoreQueryable{T}"/> into a new form.
    /// </summary>
    /// <typeparam name="TResult">The type of the value returned by the function represented by
    /// selector.</typeparam>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> whose elements are the result of invoking a
    /// projection function on each element of this <see cref="IDataStoreQueryable{T}"/>.
    /// </returns>
    public async IAsyncEnumerable<TResult> SelectAsync<TResult>(Func<T, ValueTask<TResult>> selector)
    {
        foreach (var item in _source)
        {
            yield return await selector.Invoke(item).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Projects each element of this <see cref="IDataStoreQueryable{T}"/> to an <see
    /// cref="IEnumerable{T}"/> and combines the resulting sequences into one sequence.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the elements of the sequence returned by the function represented by
    /// <paramref name="selector"/>.
    /// </typeparam>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <returns>
    /// An <see cref="IDataStoreQueryable{T}"/> whose elements are the result of invoking a
    /// one-to-many projection function on each element of the input sequence.
    /// </returns>
    public IDataStoreQueryable<TResult> SelectMany<TResult>(Expression<Func<T, IEnumerable<TResult>>> selector)
        => new EntityFrameworkDataStoreQueryable<TResult>(_source.SelectMany(selector));

    /// <summary>
    /// Projects each element of this <see cref="IDataStoreQueryable{T}"/> to an <see
    /// cref="IEnumerable{T}"/> and invokes a result selector function on each element therein.
    /// The resulting values from each intermediate sequence are combined into a single,
    /// one-dimensional sequence and returned.
    /// </summary>
    /// <typeparam name="TCollection">
    /// The type of the intermediate elements collected by the function represented by
    /// <paramref name="collectionSelector"/>.
    /// </typeparam>
    /// <typeparam name="TResult">The type of the elements of the resulting
    /// sequence.</typeparam>
    /// <param name="collectionSelector">A projection function to apply to each element of the
    /// input sequence.</param>
    /// <param name="resultSelector">A projection function to apply to each element of each
    /// intermediate sequence.</param>
    /// <returns>
    /// An <see cref="IDataStoreQueryable{T}"/> whose elements are the result of invoking the
    /// one-to-many projection function <paramref name="collectionSelector"/> on each element of
    /// source and then mapping each of those sequence elements and their corresponding source
    /// element to a result element.
    /// </returns>
    public IDataStoreQueryable<TResult> SelectMany<TCollection, TResult>(
        Expression<Func<T, IEnumerable<TCollection>>> collectionSelector,
        Expression<Func<T, TCollection, TResult>> resultSelector)
        => new EntityFrameworkDataStoreQueryable<TResult>(_source.SelectMany(collectionSelector, resultSelector));

    /// <summary>
    /// Projects each element of this <see cref="IDataStoreQueryable{T}"/> to an <see
    /// cref="IEnumerable{T}"/> and combines the resulting sequences into one sequence.
    /// </summary>
    /// <typeparam name="TResult">
    /// The type of the elements of the sequence returned by the function represented by
    /// <paramref name="selector"/>.
    /// </typeparam>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <returns>
    /// An <see cref="IDataStoreQueryable{T}"/> whose elements are the result of invoking a
    /// one-to-many projection function on each element of the input sequence.
    /// </returns>
    public async IAsyncEnumerable<TResult> SelectManyAsync<TResult>(Func<T, IAsyncEnumerable<TResult>> selector)
    {
        foreach (var item in _source)
        {
            await foreach (var subItem in selector.Invoke(item))
            {
                yield return subItem;
            }
        }
    }

    /// <summary>
    /// Projects each element of this <see cref="IDataStoreQueryable{T}"/> to an <see
    /// cref="IEnumerable{T}"/> and invokes a result selector function on each element therein.
    /// The resulting values from each intermediate sequence are combined into a single,
    /// one-dimensional sequence and returned.
    /// </summary>
    /// <typeparam name="TCollection">
    /// The type of the intermediate elements collected by the function represented by
    /// <paramref name="collectionSelector"/>.
    /// </typeparam>
    /// <typeparam name="TResult">The type of the elements of the resulting
    /// sequence.</typeparam>
    /// <param name="collectionSelector">A projection function to apply to each element of the
    /// input sequence.</param>
    /// <param name="resultSelector">A projection function to apply to each element of each
    /// intermediate sequence.</param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> whose elements are the result of invoking the
    /// one-to-many projection function <paramref name="collectionSelector"/> on each element of
    /// source and then mapping each of those sequence elements and their corresponding source
    /// element to a result element.
    /// </returns>
    public async IAsyncEnumerable<TResult> SelectManyAsync<TCollection, TResult>(
        Func<T, IEnumerable<TCollection>> collectionSelector,
        Func<T, TCollection, ValueTask<TResult>> resultSelector)
    {
        foreach (var item in _source)
        {
            foreach (var subItem in collectionSelector.Invoke(item))
            {
                yield return await resultSelector.Invoke(item, subItem).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Projects each element of this <see cref="IDataStoreQueryable{T}"/> to an <see
    /// cref="IEnumerable{T}"/> and invokes a result selector function on each element therein.
    /// The resulting values from each intermediate sequence are combined into a single,
    /// one-dimensional sequence and returned.
    /// </summary>
    /// <typeparam name="TCollection">
    /// The type of the intermediate elements collected by the function represented by
    /// <paramref name="collectionSelector"/>.
    /// </typeparam>
    /// <typeparam name="TResult">The type of the elements of the resulting
    /// sequence.</typeparam>
    /// <param name="collectionSelector">A projection function to apply to each element of the
    /// input sequence.</param>
    /// <param name="resultSelector">A projection function to apply to each element of each
    /// intermediate sequence.</param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> whose elements are the result of invoking the
    /// one-to-many projection function <paramref name="collectionSelector"/> on each element of
    /// source and then mapping each of those sequence elements and their corresponding source
    /// element to a result element.
    /// </returns>
    public async IAsyncEnumerable<TResult> SelectManyAsync<TCollection, TResult>(
        Func<T, IAsyncEnumerable<TCollection>> collectionSelector,
        Func<T, TCollection, TResult> resultSelector)
    {
        foreach (var item in _source)
        {
            await foreach (var subItem in collectionSelector.Invoke(item))
            {
                yield return resultSelector.Invoke(item, subItem);
            }
        }
    }

    /// <summary>
    /// Projects each element of this <see cref="IDataStoreQueryable{T}"/> to an <see
    /// cref="IEnumerable{T}"/> and invokes a result selector function on each element therein.
    /// The resulting values from each intermediate sequence are combined into a single,
    /// one-dimensional sequence and returned.
    /// </summary>
    /// <typeparam name="TCollection">
    /// The type of the intermediate elements collected by the function represented by
    /// <paramref name="collectionSelector"/>.
    /// </typeparam>
    /// <typeparam name="TResult">The type of the elements of the resulting
    /// sequence.</typeparam>
    /// <param name="collectionSelector">A projection function to apply to each element of the
    /// input sequence.</param>
    /// <param name="resultSelector">A projection function to apply to each element of each
    /// intermediate sequence.</param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> whose elements are the result of invoking the
    /// one-to-many projection function <paramref name="collectionSelector"/> on each element of
    /// source and then mapping each of those sequence elements and their corresponding source
    /// element to a result element.
    /// </returns>
    public async IAsyncEnumerable<TResult> SelectManyAsync<TCollection, TResult>(
        Func<T, IAsyncEnumerable<TCollection>> collectionSelector,
        Func<T, TCollection, ValueTask<TResult>> resultSelector)
    {
        foreach (var item in _source)
        {
            await foreach (var subItem in collectionSelector.Invoke(item))
            {
                yield return await resultSelector.Invoke(item, subItem).ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// Bypasses a specified number of elements in a sequence and then returns the remaining
    /// elements.
    /// </summary>
    /// <param name="count">The number of elements to skip before returning the remaining
    /// elements.</param>
    /// <returns>
    /// An <see cref="IDataStoreQueryable{T}"/> that contains elements that occur after the
    /// specified index in the input sequence.
    /// </returns>
    public IDataStoreQueryable<T> Skip(int count)
        => new EntityFrameworkDataStoreQueryable<T>(_source.Skip(count));

    /// <summary>
    /// Returns a specified number of contiguous elements from the start of this <see
    /// cref="IDataStoreQueryable{T}"/>.
    /// </summary>
    /// <param name="count">The number of elements to return.</param>
    /// <returns>
    /// An <see cref="IDataStoreQueryable{T}"/> that contains the specified number of elements
    /// from the start of this <see cref="IDataStoreQueryable{T}"/>.
    /// </returns>
    public IDataStoreQueryable<T> Take(int count)
        => new EntityFrameworkDataStoreQueryable<T>(_source.Take(count));

    /// <summary>
    /// Enumerates the results of this <see cref="IDataStoreQueryable{T}" /> and returns them as
    /// a <see cref="IReadOnlyList{T}" />.
    /// </summary>
    /// <returns>A <see cref="IReadOnlyList{T}" />.</returns>
    public IReadOnlyList<T> ToList() => _source.ToList();

    /// <summary>
    /// Asynchronously enumerates the results of this <see cref="IDataStoreQueryable{T}"/> and
    /// returns them as a <see cref="IReadOnlyList{T}"/>.
    /// </summary>
    /// <returns>A <see cref="IReadOnlyList{T}"/>.</returns>
    public async Task<IReadOnlyList<T>> ToListAsync() => await _source.ToListAsync().ConfigureAwait(false);

    /// <summary>
    /// Filters this <see cref="IDataStoreQueryable{T}" /> based on a <paramref
    /// name="predicate"/>.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>An <see cref="IDataStoreQueryable{T}" />.</returns>
    public IDataStoreQueryable<T> Where(Expression<Func<T, bool>> predicate)
        => new EntityFrameworkDataStoreQueryable<T>(_source.Where(predicate));

    /// <summary>
    /// Filters this <see cref="IDataStoreQueryable{T}" /> based on an asynchronous <paramref
    /// name="predicate" />.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>An <see cref="IAsyncEnumerable{T}" />.</returns>
    public async IAsyncEnumerable<T> WhereAsync(Func<T, ValueTask<bool>> predicate)
    {
        foreach (var item in _source)
        {
            if (await predicate.Invoke(item).ConfigureAwait(false))
            {
                yield return item;
            }
        }
    }
}
