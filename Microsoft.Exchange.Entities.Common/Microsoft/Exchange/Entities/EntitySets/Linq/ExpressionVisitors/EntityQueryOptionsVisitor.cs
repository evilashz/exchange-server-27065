using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x02000040 RID: 64
	internal class EntityQueryOptionsVisitor : ExpressionVisitor
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00005090 File Offset: 0x00003290
		public EntityQueryOptionsVisitor(EntityQueryOptionsBuilder entityQueryOptionsBuilder, Expression knownExpression = null, IEntityQueryOptions knownQueryOptions = null)
		{
			this.entityQueryOptionsBuilder = entityQueryOptionsBuilder;
			this.knownExpression = knownExpression;
			this.knownQueryOptions = knownQueryOptions;
			this.oneArgMethods = new Dictionary<MethodInfo, Action<Expression>>
			{
				{
					QueryableMethods.Take,
					new Action<Expression>(this.entityQueryOptionsBuilder.ApplyTake)
				},
				{
					QueryableMethods.Skip,
					new Action<Expression>(this.entityQueryOptionsBuilder.ApplySkip)
				},
				{
					QueryableMethods.Where,
					new Action<Expression>(this.entityQueryOptionsBuilder.ApplyWhere)
				},
				{
					QueryableMethods.OrderBy,
					new Action<Expression>(this.entityQueryOptionsBuilder.ApplyOrderBy)
				},
				{
					QueryableMethods.ThenBy,
					new Action<Expression>(this.entityQueryOptionsBuilder.ApplyThenBy)
				},
				{
					QueryableMethods.OrderByDescending,
					new Action<Expression>(this.entityQueryOptionsBuilder.ApplyOrderByDescending)
				},
				{
					QueryableMethods.ThenByDescending,
					new Action<Expression>(this.entityQueryOptionsBuilder.ApplyThenByDescending)
				}
			};
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000518C File Offset: 0x0000338C
		public override Expression Visit(Expression node)
		{
			if (node == this.knownExpression)
			{
				this.entityQueryOptionsBuilder.CopyFrom(this.knownQueryOptions);
				return node;
			}
			if (node is ConstantExpression || node is MethodCallExpression)
			{
				return base.Visit(node);
			}
			throw new NotSupportedException(string.Format("TODO: LOC: EntityProviderExpressionVisitor does not support {0}", node.GetType().Name));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000051E7 File Offset: 0x000033E7
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (this.TryApplyMethodCall(node))
			{
				return node;
			}
			throw new NotSupportedException(string.Format("TODO: LOC: EntityProviderExpressionVisitor does not support calls to {0}", node.Method));
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000520C File Offset: 0x0000340C
		private bool TryApplyMethodCall(MethodCallExpression node)
		{
			ReadOnlyCollection<Expression> arguments = node.Arguments;
			if (arguments.Count > 0)
			{
				this.Visit(arguments[0]);
			}
			MethodInfo genericMethodDefinition = node.GetGenericMethodDefinition();
			switch (arguments.Count)
			{
			case 1:
				return genericMethodDefinition == QueryableMethods.Count || genericMethodDefinition == QueryableMethods.LongCount;
			case 2:
				return this.TryApplyMethodCall(genericMethodDefinition, arguments[1]);
			default:
				return false;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005284 File Offset: 0x00003484
		private bool TryApplyMethodCall(MethodInfo method, Expression argument)
		{
			Action<Expression> action;
			if (this.oneArgMethods.TryGetValue(method, out action))
			{
				action(argument);
				return true;
			}
			return false;
		}

		// Token: 0x04000061 RID: 97
		private readonly EntityQueryOptionsBuilder entityQueryOptionsBuilder;

		// Token: 0x04000062 RID: 98
		private readonly Expression knownExpression;

		// Token: 0x04000063 RID: 99
		private readonly IEntityQueryOptions knownQueryOptions;

		// Token: 0x04000064 RID: 100
		private readonly Dictionary<MethodInfo, Action<Expression>> oneArgMethods;
	}
}
