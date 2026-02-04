using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if(spec.Criteria!=null)
        {
            query = query.Where(spec.Criteria);
        }
        if(spec.OrderBy!=null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if(spec.OrderByDescending!=null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        return query;
    }

    public static IQueryable<TResult> GetQuery<Tspec,TResult>(IQueryable<T> query,ISpecification<T,TResult> spec)
    {
        if(spec.Criteria!=null)
        {
            query = query.Where(spec.Criteria);
        }
        if(spec.OrderBy!=null)
        {
            query = query.OrderBy(spec.OrderBy);
            
        }
        if(spec.OrderByDescending!=null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
        var SelectQuery = query as IQueryable<TResult>;
        if(spec.Select!=null)
        {
            SelectQuery = query.Select(spec.Select);
        }
        return SelectQuery ?? query.Cast<TResult>();
    }
}