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
        if(spec.IsPageEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        if(spec.IsDistinct)
        {
            query = query.Distinct();
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

        if(spec.IsPageEnabled)
        {
            SelectQuery = SelectQuery?.Skip(spec.Skip).Take(spec.Take);
        }

        if(spec.IsDistinct)
        {
            SelectQuery = SelectQuery?.Distinct();
        }
        
        return SelectQuery ?? query.Cast<TResult>();
    }
}