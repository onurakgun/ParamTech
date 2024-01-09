using ParamTech.Core.Persistence.Repositoires.BaseEntity;
using ParamTech.Core.Persistence.Repositoires.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
namespace ParamTech.Core.Persistence.Repositoires.Repository.Concrate;
public class EfRepostiyory<TEntity,TContext> : IAsyncRepository<TEntity>, IRepository<TEntity> where TContext : DbContext where TEntity : Entity
{
    protected readonly TContext _context;
    public EfRepostiyory(TContext context)
    {
        _context = context;
    }

    public bool Any(Expression<Func<TEntity, bool>> predicate = null, bool withDeleted = false, bool enableTracking = true)
    {
        IQueryable<TEntity> query = Query();
        if (!enableTracking)
            query = query.AsNoTracking();
        if (withDeleted)
            query = query.IgnoreQueryFilters();
        if (predicate != null)
            query = query.Where(predicate);
        return query.Any();
    }


    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Query();
        if (!enableTracking)
            query = query.AsNoTracking();
        if (withDeleted)
            query = query.IgnoreQueryFilters();
        if (predicate != null)
            query = query.Where(predicate);
        return await query.AnyAsync(cancellationToken);

    }


    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filitre = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool enableTracking = true, bool withDeleted = false)
    {
        IQueryable<TEntity> query = Query();
        if (!enableTracking)
            query = query.AsNoTracking();
        if (include != null)
            query = include(query);
        if (withDeleted)
            query = query.IgnoreQueryFilters();
        return query.FirstOrDefault(predicate: filitre);
    }


    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filitre = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool enableTracking = true, bool withDeleted = false, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Query();
        if (!enableTracking)
            query = query.AsNoTracking();
        if (include != null)
            query = include(query);
        if (withDeleted)
            query = query.IgnoreQueryFilters();
        return await query.FirstOrDefaultAsync(predicate: filitre, cancellationToken: cancellationToken);
    }


    public IQueryable<TEntity> Query()
    {
        return _context.Set<TEntity>();
    }


    public List<TEntity> ToList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool withDeleted = false, bool enableTracking = true)
    {
        IQueryable<TEntity> query = Query();
        if (!enableTracking)
            query = query.AsNoTracking();
        if (include != null)
            query = include(query);
        if (withDeleted)
            query = query.IgnoreQueryFilters();
        if (predicate != null)
            query = query.Where(predicate);
        if (orderBy != null)
            return orderBy(query).ToList();
        return query.ToList();
    }


    public async Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Query();
        if (!enableTracking)
            query = query.AsNoTracking();
        if (include != null)
            query = include(query);
        if (withDeleted)
            query = query.IgnoreQueryFilters();
        if (predicate != null)
            query = query.Where(predicate);
        if (orderBy != null)
            return await orderBy(query).ToListAsync();
        return await query.ToListAsync(cancellationToken);
    }


    public TEntity Add(TEntity entity)
    {
        try
        {
            entity.Createdate = DateTime.Now;
            var response = _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            string message = ex.Message == null ? "" : ex.Message;
            string innerexcaption = ex.InnerException == null ? "" : ex.InnerException.Message;
            string messages = message + " " + innerexcaption;
            throw new Exception(messages);
        }
    }


    public async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        {
            entity.Createdate = DateTime.Now;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            string message = ex.Message == null ? "" : ex.Message;
            string innerexcaption = ex.InnerException == null ? "" : ex.InnerException.Message;
            string messages = message + " " + innerexcaption;
            throw new Exception(messages);
        }
    }


    public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
    {
        try
        {
            entities.Select(S => { S.Createdate = DateTime.Now; return S; }).ToList();
            _context.AddRange(entities);
            _context.SaveChanges();
            return entities;
        }
        catch (Exception ex)
        {
            _context.Dispose();
            string message = ex.Message == null ? "" : ex.Message;
            string innerexcaption = ex.InnerException == null ? "" : ex.InnerException.Message;
            string messages = message + " " + innerexcaption;
            throw new Exception(messages, ex);
        }
    }


    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        try
        {
            entities.Select(S => { S.Createdate = DateTime.Now; return S; }).ToList();
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }
        catch (Exception ex)
        {
            _context.Dispose();
            string message = ex.Message == null ? "" : ex.Message;
            string innerexcaption = ex.InnerException == null ? "" : ex.InnerException.Message;
            string messages = message + " " + innerexcaption;
            throw new Exception(messages, ex);
        }
    }


    public TEntity Update(TEntity entity)
    {
        entity.Updatedate = DateTime.Now;
        _context.SaveChanges();
        return entity;
    }


    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.Updatedate = DateTime.Now;
        await _context.SaveChangesAsync();
        return entity;
    }


    public ICollection<TEntity> UpdateRange(ICollection<TEntity> entities)
    {
        foreach (var item in entities)
            item.Createdate = DateTime.Now;
        _context.UpdateRange(entities);
        _context.SaveChanges();
        return entities;


    }


    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
    {
        foreach (var item in entities)
            item.Createdate = DateTime.Now;
        _context.UpdateRange(entities);
        await _context.SaveChangesAsync();
        return entities;
    }
}