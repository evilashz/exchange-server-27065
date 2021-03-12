using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors;

namespace Microsoft.Exchange.Entities.EntitySets.Linq
{
	// Token: 0x0200003E RID: 62
	public class EntitySetQueryProvider<TEntity> : RootQueryProvider<TEntity> where TEntity : class, IEntity
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00004C9C File Offset: 0x00002E9C
		public EntitySetQueryProvider(IEntitySet<TEntity> entitySet, CommandContext commandContext) : base(entitySet.ToString())
		{
			this.entitySet = entitySet;
			this.commandContext = commandContext;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00004CB8 File Offset: 0x00002EB8
		protected sealed override object ExecuteQuery(Expression queryExpression)
		{
			this.Validate(queryExpression);
			if (queryExpression.IsMethodCall(QueryableMethods.Count, QueryableMethods.LongCount))
			{
				return this.entitySet.EstimateTotalCount(this.lastExpressionQueryOptions, this.commandContext);
			}
			string key;
			if (this.lastExpressionQueryOptions.Filter.IsWhereIdEqualsKey(out key))
			{
				if (this.lastExpressionQueryOptions.Skip.GetValueOrDefault(0) == 0)
				{
					TEntity tentity = this.entitySet.Read(key, this.commandContext);
					if (tentity != null)
					{
						return new TEntity[]
						{
							tentity
						};
					}
				}
				return Enumerable.Empty<TEntity>();
			}
			return this.entitySet.Find(this.lastExpressionQueryOptions, this.commandContext);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00004D6E File Offset: 0x00002F6E
		protected sealed override IEnumerable<TEntity> FindAll()
		{
			return this.entitySet.Find(null, this.commandContext);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00004D84 File Offset: 0x00002F84
		protected virtual IEntityQueryOptions GetQueryOptions(Expression expression)
		{
			EntityQueryOptionsBuilder entityQueryOptionsBuilder = new EntityQueryOptionsBuilder();
			EntityQueryOptionsVisitor entityQueryOptionsVisitor = new EntityQueryOptionsVisitor(entityQueryOptionsBuilder, this.lastValidatedExpression, this.lastExpressionQueryOptions);
			entityQueryOptionsVisitor.Visit(expression);
			return entityQueryOptionsBuilder;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00004DB4 File Offset: 0x00002FB4
		protected sealed override void Validate(Expression expression)
		{
			if (this.lastValidatedExpression != expression)
			{
				IEntityQueryOptions queryOptions = this.GetQueryOptions(expression);
				this.lastValidatedExpression = expression;
				this.lastExpressionQueryOptions = queryOptions;
			}
		}

		// Token: 0x04000059 RID: 89
		private readonly CommandContext commandContext;

		// Token: 0x0400005A RID: 90
		private readonly IEntitySet<TEntity> entitySet;

		// Token: 0x0400005B RID: 91
		private IEntityQueryOptions lastExpressionQueryOptions;

		// Token: 0x0400005C RID: 92
		private Expression lastValidatedExpression;
	}
}
