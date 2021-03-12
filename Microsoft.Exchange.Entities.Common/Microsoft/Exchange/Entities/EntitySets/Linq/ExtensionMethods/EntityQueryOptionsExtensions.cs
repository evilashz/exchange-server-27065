using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods
{
	// Token: 0x0200004D RID: 77
	internal static class EntityQueryOptionsExtensions
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x00006508 File Offset: 0x00004708
		public static IQueryable<T> ApplyTo<T>(this IEntityQueryOptions queryOptions, IQueryable<T> query)
		{
			IQueryable<T> queryable = query;
			if (queryOptions != null)
			{
				if (queryOptions.Filter != null)
				{
					Expression expression = queryOptions.Filter.RemoveQuote();
					queryable = queryable.Where((Expression<Func<T, bool>>)expression);
				}
				if (queryOptions.OrderBy != null)
				{
					for (int i = 0; i < queryOptions.OrderBy.Count; i++)
					{
						queryable = (IQueryable<T>)queryOptions.OrderBy[i].ApplyTo(queryable, i != 0);
					}
				}
				if (queryOptions.Skip != null)
				{
					queryable = queryable.Skip(queryOptions.Skip.Value);
				}
				if (queryOptions.Take != null)
				{
					queryable = queryable.Take(queryOptions.Take.Value);
				}
			}
			return queryable;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000065CC File Offset: 0x000047CC
		public static IQueryable<T> ApplySkipTakeTo<T>(this IEntityQueryOptions queryOptions, IQueryable<T> query)
		{
			IQueryable<T> queryable = query;
			if (queryOptions != null)
			{
				if (queryOptions.Skip != null)
				{
					queryable = queryable.Skip(queryOptions.Skip.Value);
				}
				if (queryOptions.Take != null)
				{
					queryable = queryable.Take(queryOptions.Take.Value);
				}
			}
			return queryable;
		}
	}
}
