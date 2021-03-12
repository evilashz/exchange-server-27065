using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Entities.EntitySets.Linq
{
	// Token: 0x02000052 RID: 82
	internal class Query<T> : IOrderedQueryable<T>, IQueryable<T>, IEnumerable<!0>, IOrderedQueryable, IQueryable, IEnumerable
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000067DF File Offset: 0x000049DF
		public Query(IQueryProvider queryProvider, Expression expression)
		{
			this.queryProvider = queryProvider;
			this.expression = expression;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000067F5 File Offset: 0x000049F5
		Type IQueryable.ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00006801 File Offset: 0x00004A01
		Expression IQueryable.Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006809 File Offset: 0x00004A09
		IQueryProvider IQueryable.Provider
		{
			get
			{
				return this.queryProvider;
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006811 File Offset: 0x00004A11
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.queryProvider.Execute<IEnumerable<T>>(this.expression).GetEnumerator();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006829 File Offset: 0x00004A29
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x04000080 RID: 128
		private readonly Expression expression;

		// Token: 0x04000081 RID: 129
		private readonly IQueryProvider queryProvider;
	}
}
