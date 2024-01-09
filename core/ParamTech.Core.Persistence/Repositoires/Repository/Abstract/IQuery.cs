namespace ParamTech.Core.Persistence.Repositoires.Repository.Abstract;
public interface IQuery<T>
{
    IQueryable<T> Query();
}