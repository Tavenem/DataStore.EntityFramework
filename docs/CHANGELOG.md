# Changelog

## 0.35.1-preview
### Changed
- Update to .NET 10
- The original `EntityFrameworkDataStore` has been divided into three separate implementations:
  - `EntityFrameworkDataStore<TItem>` which is an abstract class that allows specifying the item type for stored items
    - `EntityFrameworkDataStore<TItem>` implements the updated `IDataStore<TItem>` interface (see [the `Tavenem.DataStore` project](https://github.com/Tavenem/DataStore) for details)
  - `EntityFrameworkDataStore<TKey, TItem>` which is an abstract class that extends `EntityFrameworkDataStore<TItem>` and allows specifying the key types for stored items
    - `EntityFrameworkDataStore<TKey, TItem>` implements the updated `IDataStore<TKey, TItem>` interface (see [the `Tavenem.DataStore` project](https://github.com/Tavenem/DataStore) for details)
  - `EntityFrameworkDataStore` which replicates the original by extending `EntityFrameworkDataStore<string, IIdItem>`
    - `EntityFrameworkDataStore` implements the updated `IIdItemDataStore` interface (see [the `Tavenem.DataStore` project](https://github.com/Tavenem/DataStore) for details)
- `EntityFrameworkDataStoreQueryable<TSource>` implements the following updated interfaces (see [the `Tavenem.DataStore` project](https://github.com/Tavenem/DataStore) for details):
  - `IDataStoreDistinctByQueryable<TSource>`
  - `IDataStoreDistinctQueryable<TSource>`
  - `IDataStoreFirstQueryable<TSource>`
  - `IDataStoreGroupByQueryable<TSource>`
  - `IDataStoreGroupJoinQueryable<TSource>`
  - `IDataStoreIntersectByQueryable<TSource>`
  - `IDataStoreIntersectQueryable<TSource>`
  - `IDataStoreJoinQueryable<TSource>`
  - `IDataStoreLastQueryable<TSource>`
  - `IDataStoreOfTypeQueryable<TSource>`
  - `IDataStoreOrderableQueryable<TSource>`
  - `IDataStoreReverseQueryable<TSource>`
  - `IDataStoreSelectManyQueryable<TSource>`
  - `IDataStoreSelectQueryable<TSource>`
  - `IDataStoreSkipLastQueryable<TSource>`
  - `IDataStoreSkipQueryable<TSource>`
  - `IDataStoreSkipWhileQueryable<TSource>`
  - `IDataStoreTakeLastQueryable<TSource>`
  - `IDataStoreTakeQueryable<TSource>`
  - `IDataStoreTakeWhileQueryable<TSource>`
  - `IDataStoreUnionByQueryable<TSource>`
  - `IDataStoreUnionQueryable<TSource>`
  - `IDataStoreWhereQueryable<TSource>`
  - `IDataStoreZipQueryable<TSource>`

## 0.34.1-preview
### Changed
- Clarify 1-based indexing of page numbers in `IPagedList`.

## 0.32.0-preview - 0.34.0-preview
### Updated
- Update dependencies

## 0.31.0-preview
### Changed
- Update to .NET 7 preview

## 0.30.0-preview
### Changed
- Update to .NET 6 preview
- Update to C# 10 preview

## 0.29.1-preview
### Added
- Initial preview release