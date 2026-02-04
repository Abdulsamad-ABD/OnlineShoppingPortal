using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specification;

public class BaseSpecification<T>(Expression<Func<T,bool>>? criteria) : ISpecification<T>
{
    public  BaseSpecification() : this(null) // Constructor
    {
        
    }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy {get; private set;}

    public Expression<Func<T, object>>? OrderByDescending {get; private set;}

    public bool IsDistinct {get; private set;}

    protected void AddOrderBy(Expression<Func<T,Object>> OrderByExpression)
    {
        OrderBy = OrderByExpression;
    }
    protected void AddOrderByDescending(Expression<Func<T,Object>> OrderByDescendingExpression)
    {
        OrderByDescending = OrderByDescendingExpression;
    }
    protected void AddDistinct()
    {
        IsDistinct = true;
    }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    public BaseSpecification() : this(null)
    {

    }
    public Expression<Func<T, TResult>>? Select { get; private set;}
    protected void AddSelect(Expression<Func<T,TResult>> SelectExpression)
    {
        Select = SelectExpression;
    }
}