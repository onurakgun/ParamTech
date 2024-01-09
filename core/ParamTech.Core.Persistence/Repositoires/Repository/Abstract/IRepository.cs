using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
namespace ParamTech.Core.Persistence.Repositoires.Repository.Abstract;
public interface IRepository<TEntity> : IQuery<TEntity> where TEntity : class
{
    TEntity FirstOrDefault(
        Expression<Func<TEntity, bool>> filitre = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool enableTracking = true,
        bool withDeleted = false
        );


    List<TEntity> ToList(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool withDeleted = false,
        bool enableTracking = true
    );
   

    bool Any(
        Expression<Func<TEntity, bool>> predicate = null,
        bool withDeleted = false,
        bool enableTracking = true
    );

    TEntity Add(TEntity entity);

    ICollection<TEntity> AddRange(ICollection<TEntity> entities);

    TEntity Update(TEntity entity);

    ICollection<TEntity> UpdateRange(ICollection<TEntity> entities);

}
