using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x0200003F RID: 63
	internal class EntityQueryOptionsBuilder : IEntityQueryOptions
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00004DE0 File Offset: 0x00002FE0
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public Expression Filter { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004DF1 File Offset: 0x00002FF1
		public IReadOnlyList<OrderByClause> OrderBy
		{
			get
			{
				return this.orderBy;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004DF9 File Offset: 0x00002FF9
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00004E01 File Offset: 0x00003001
		public int? Skip { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004E0A File Offset: 0x0000300A
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00004E12 File Offset: 0x00003012
		public int? Take { get; private set; }

		// Token: 0x0600015F RID: 351 RVA: 0x00004E1B File Offset: 0x0000301B
		public void ApplyOrderBy(Expression argument)
		{
			this.AddOrderingExpression(new OrderByClause(argument, ListSortDirection.Ascending), true);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00004E2B File Offset: 0x0000302B
		public void ApplyOrderByDescending(Expression argument)
		{
			this.AddOrderingExpression(new OrderByClause(argument, ListSortDirection.Descending), true);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004E3C File Offset: 0x0000303C
		public void ApplySkip(Expression argument)
		{
			if (this.Skip != null)
			{
				throw new NotSupportedException("TODO: LOC: Cannot Apply Skip twice.");
			}
			if (this.Take != null)
			{
				throw new NotSupportedException("TODO: LOC: Skip needs to be applied before Top is applied.");
			}
			int num = ReduceToConstantVisitor.Reduce<int>(argument);
			if (num < 0)
			{
				throw new ArgumentException("TODO: LOC: 'Skip' argument must be non-negative integer.");
			}
			this.Skip = new int?(num);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004EA4 File Offset: 0x000030A4
		public void ApplyTake(Expression argument)
		{
			if (this.Take != null)
			{
				throw new NotSupportedException("TODO: LOC: Cannot Apply Take twice.");
			}
			int num = ReduceToConstantVisitor.Reduce<int>(argument);
			if (num < 0)
			{
				throw new ArgumentException("TODO: LOC: 'Take' argument must be non-negative integer.");
			}
			this.Take = new int?(num);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00004EEE File Offset: 0x000030EE
		public void ApplyThenBy(Expression argument)
		{
			this.AddOrderingExpression(new OrderByClause(argument, ListSortDirection.Ascending), false);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00004EFE File Offset: 0x000030FE
		public void ApplyThenByDescending(Expression argument)
		{
			this.AddOrderingExpression(new OrderByClause(argument, ListSortDirection.Descending), false);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00004F10 File Offset: 0x00003110
		public void ApplyWhere(Expression argument)
		{
			if (this.OrderBy != null || this.Skip != null || this.Take != null)
			{
				throw new NotSupportedException("TODO: LOC: 'Where' needs to be called before ordering functions, 'Skip' or 'Take' are called.");
			}
			if (this.Filter != null)
			{
				throw new NotSupportedException("TODO: LOC: Only one filter is supported and one is already specified.");
			}
			this.Filter = argument;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004F6C File Offset: 0x0000316C
		public void CopyFrom(IEntityQueryOptions options)
		{
			if (this.Skip != null || this.Take != null || this.Filter != null || this.OrderBy != null)
			{
				throw new InvalidOperationException();
			}
			this.Filter = options.Filter;
			IReadOnlyList<OrderByClause> readOnlyList = options.OrderBy;
			if (readOnlyList == null)
			{
				this.orderBy = null;
			}
			else
			{
				for (int i = 0; i < readOnlyList.Count; i++)
				{
					OrderByClause clause = readOnlyList[i];
					this.AddOrderingExpression(clause, i == 0);
				}
			}
			this.Skip = options.Skip;
			this.Take = options.Take;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000500C File Offset: 0x0000320C
		private void AddOrderingExpression(OrderByClause clause, bool beginNewList)
		{
			if (this.Skip != null || this.Take != null)
			{
				throw new NotSupportedException("TODO: LOC: Ordering functions needs to be called before 'Skip' or 'Take' are called.");
			}
			if (beginNewList)
			{
				if (this.OrderBy != null)
				{
					throw new NotSupportedException("TODO: LOC: Only one set of ordering expressions is supported and one is already specified.");
				}
				this.orderBy = new List<OrderByClause>();
			}
			else if (this.OrderBy == null)
			{
				throw new NotSupportedException("ApplyOrderBy or ApplyOrderByDescending needs to be called before ApplyThenBy/ApplyThenByDescending");
			}
			this.orderBy.Add(clause);
		}

		// Token: 0x0400005D RID: 93
		private List<OrderByClause> orderBy;
	}
}
