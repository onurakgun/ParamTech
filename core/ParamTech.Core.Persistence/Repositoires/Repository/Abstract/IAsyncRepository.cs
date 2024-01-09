using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
namespace ParamTech.Core.Persistence.Repositoires.Repository.Abstract;
public interface IAsyncRepository<TEntity> where TEntity : class
{
    Task<TEntity> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> filitre = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool enableTracking = true,
        bool withDeleted = false,
        CancellationToken cancellationToken = default 
        );


    Task<List<TEntity>> ToListAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );


    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default

    );
    Task<TEntity> AddAsync(TEntity entity);

    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

    Task<TEntity> UpdateAsync(TEntity entity);

    Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);
}