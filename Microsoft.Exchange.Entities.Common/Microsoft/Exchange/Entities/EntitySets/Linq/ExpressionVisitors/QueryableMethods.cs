using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x02000047 RID: 71
	internal static class QueryableMethods
	{
		// Token: 0x06000181 RID: 385 RVA: 0x0000559F File Offset: 0x0000379F
		private static MethodInfo GetMethod<TReturn>(Expression<Func<object, TReturn>> expression)
		{
			return expression.GetGenericMethodDefinition();
		}

		// Token: 0x04000067 RID: 103
		public static readonly MethodInfo Count = QueryableMethods.GetMethod<int>((object _) => QueryableMethods.Queryable.Count<int>());

		// Token: 0x04000068 RID: 104
		public static readonly MethodInfo IndexedSelect = QueryableMethods.GetMethod<IQueryable<int>>((object _) => QueryableMethods.Queryable.Select(QueryableMethods.IndexedSelectExpression));

		// Token: 0x04000069 RID: 105
		public static readonly MethodInfo LongCount = QueryableMethods.GetMethod<long>((object _) => QueryableMethods.Queryable.LongCount<int>());

		// Token: 0x0400006A RID: 106
		public static readonly MethodInfo OrderBy = QueryableMethods.GetMethod<IOrderedQueryable<int>>((object _) => QueryableMethods.Queryable.OrderBy(QueryableMethods.OrderingExpression));

		// Token: 0x0400006B RID: 107
		public static readonly MethodInfo OrderByDescending = QueryableMethods.GetMethod<IOrderedQueryable<int>>((object _) => QueryableMethods.Queryable.OrderByDescending(QueryableMethods.OrderingExpression));

		// Token: 0x0400006C RID: 108
		public static readonly MethodInfo Select = QueryableMethods.GetMethod<IQueryable<int>>((object _) => QueryableMethods.Queryable.Select(QueryableMethods.SelectExpression));

		// Token: 0x0400006D RID: 109
		public static readonly MethodInfo Skip = QueryableMethods.GetMethod<IQueryable<int>>((object _) => QueryableMethods.Queryable.Skip(0));

		// Token: 0x0400006E RID: 110
		public static readonly MethodInfo Take = QueryableMethods.GetMethod<IQueryable<int>>((object _) => QueryableMethods.Queryable.Take(0));

		// Token: 0x0400006F RID: 111
		public static readonly MethodInfo ThenBy = QueryableMethods.GetMethod<IOrderedQueryable<int>>((object _) => QueryableMethods.Queryable.ThenBy(QueryableMethods.OrderingExpression));

		// Token: 0x04000070 RID: 112
		public static readonly MethodInfo ThenByDescending = QueryableMethods.GetMethod<IOrderedQueryable<int>>((object _) => QueryableMethods.Queryable.ThenByDescending(QueryableMethods.OrderingExpression));

		// Token: 0x04000071 RID: 113
		public static readonly MethodInfo Where = QueryableMethods.GetMethod<IQueryable<int>>((object _) => QueryableMethods.Queryable.Where(QueryableMethods.FilteringExpression));

		// Token: 0x04000072 RID: 114
		private static readonly Expression<Func<int, bool>> FilteringExpression = null;

		// Token: 0x04000073 RID: 115
		private static readonly Expression<Func<int, int, int>> IndexedSelectExpression = null;

		// Token: 0x04000074 RID: 116
		private static readonly Expression<Func<int, int>> OrderingExpression = null;

		// Token: 0x04000075 RID: 117
		private static readonly IOrderedQueryable<int> Queryable = null;

		// Token: 0x04000076 RID: 118
		private static readonly Expression<Func<int, int>> SelectExpression = null;
	}
}
