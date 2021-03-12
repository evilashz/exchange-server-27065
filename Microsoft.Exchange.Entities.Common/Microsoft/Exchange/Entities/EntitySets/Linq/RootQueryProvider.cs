using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors;

namespace Microsoft.Exchange.Entities.EntitySets.Linq
{
	// Token: 0x0200003D RID: 61
	public abstract class RootQueryProvider<T> : QueryProvider, IQueryable<T>, IEnumerable<T>, IQueryable, IEnumerable
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00004C25 File Offset: 0x00002E25
		protected RootQueryProvider(string name)
		{
			this.rootExpression = Expression.Constant(this);
			this.description = "value(" + name + ")";
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004C4F File Offset: 0x00002E4F
		Type IQueryable.ElementType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004C5B File Offset: 0x00002E5B
		Expression IQueryable.Expression
		{
			get
			{
				return this.rootExpression;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004C63 File Offset: 0x00002E63
		IQueryProvider IQueryable.Provider
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004C66 File Offset: 0x00002E66
		public sealed override string ToString()
		{
			return this.description;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004C6E File Offset: 0x00002E6E
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return this.FindAll().GetEnumerator();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004C7B File Offset: 0x00002E7B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004C83 File Offset: 0x00002E83
		protected sealed override object OnExecute(Expression expression)
		{
			if (expression.IsConstantExpressionWithValue(this))
			{
				return this.FindAll();
			}
			return this.ExecuteQuery(expression);
		}

		// Token: 0x06000151 RID: 337
		protected abstract object ExecuteQuery(Expression queryExpression);

		// Token: 0x06000152 RID: 338
		protected abstract IEnumerable<T> FindAll();

		// Token: 0x04000057 RID: 87
		private readonly string description;

		// Token: 0x04000058 RID: 88
		private readonly Expression rootExpression;
	}
}
