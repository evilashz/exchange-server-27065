using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.Exchange.Entities.EntitySets.Linq.ExtensionMethods;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x02000048 RID: 72
	public static class QueryFilterBuilder
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00005B00 File Offset: 0x00003D00
		public static QueryFilter ToQueryFilter(this Expression expression, IPropertyDefinitionMap propertyDefinitionMap)
		{
			QueryFilterBuilder.QueryFilterBuilderVisitor queryFilterBuilderVisitor = new QueryFilterBuilder.QueryFilterBuilderVisitor(propertyDefinitionMap);
			QueryFilter filter = queryFilterBuilderVisitor.GetFilter(expression);
			return QueryFilter.SimplifyFilter(filter);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005B38 File Offset: 0x00003D38
		public static IEnumerable<SortBy> ToSortBy(this IEnumerable<OrderByClause> clauses, IPropertyDefinitionMap propertyDefinitionMap)
		{
			QueryFilterBuilder.QueryFilterBuilderVisitor visitor = new QueryFilterBuilder.QueryFilterBuilderVisitor(propertyDefinitionMap);
			return from clause in clauses
			select clause.ToSortBy(visitor);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00005B69 File Offset: 0x00003D69
		private static SortBy ToSortBy(this OrderByClause clause, QueryFilterBuilder.QueryFilterBuilderVisitor visitor)
		{
			return new SortBy(visitor.GetPropertyDefinition(clause.Expression), clause.Direction.ToSortOrder());
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005B87 File Offset: 0x00003D87
		private static SortOrder ToSortOrder(this ListSortDirection sortDirection)
		{
			if (sortDirection != ListSortDirection.Ascending)
			{
				return SortOrder.Descending;
			}
			return SortOrder.Ascending;
		}

		// Token: 0x02000049 RID: 73
		private sealed class QueryFilterBuilderVisitor : ExpressionVisitor
		{
			// Token: 0x06000187 RID: 391 RVA: 0x00005B8F File Offset: 0x00003D8F
			public QueryFilterBuilderVisitor(IPropertyDefinitionMap propertyDefinitionMap)
			{
				this.propertyDefinitionMap = propertyDefinitionMap;
			}

			// Token: 0x06000188 RID: 392 RVA: 0x00005BA0 File Offset: 0x00003DA0
			public QueryFilter GetFilter(Expression expression)
			{
				QueryFilter result;
				try
				{
					try
					{
						this.Visit(expression);
					}
					catch (NotSupportedException innerException)
					{
						throw new UnsupportedExpressionException(Strings.UnsupportedFilterExpression(expression), innerException);
					}
					QueryFilter queryFilter;
					if ((queryFilter = this.filter) == null)
					{
						queryFilter = (this.propertyDefinition.AsBooleanComparisonQueryFilter() ?? (this.hasPropertyValue ? this.propertyValue.AsBooleanQueryFilter() : null));
					}
					this.filter = queryFilter;
					if (this.filter == null)
					{
						throw new UnsupportedExpressionException(Strings.UnsupportedFilterExpression(expression));
					}
					result = this.filter;
				}
				finally
				{
					this.ResetState();
				}
				return result;
			}

			// Token: 0x06000189 RID: 393 RVA: 0x00005C3C File Offset: 0x00003E3C
			public PropertyDefinition GetPropertyDefinition(Expression expression)
			{
				PropertyDefinition result;
				try
				{
					try
					{
						this.Visit(expression);
					}
					catch (NotSupportedException innerException)
					{
						throw new UnsupportedExpressionException(Strings.UnsupportedPropertyExpression(expression), innerException);
					}
					if (this.propertyDefinition == null)
					{
						throw new UnsupportedExpressionException(Strings.UnsupportedPropertyExpression(expression));
					}
					result = this.propertyDefinition;
				}
				finally
				{
					this.ResetState();
				}
				return result;
			}

			// Token: 0x0600018A RID: 394 RVA: 0x00005CA4 File Offset: 0x00003EA4
			public object GetPropertyValue(Expression expression)
			{
				object result;
				try
				{
					try
					{
						this.Visit(expression);
					}
					catch (NotSupportedException innerException)
					{
						throw new UnsupportedExpressionException(Strings.UnsupportedPropertyValue(expression), innerException);
					}
					if (!this.hasPropertyValue)
					{
						throw new UnsupportedExpressionException(Strings.UnsupportedPropertyValue(expression));
					}
					result = this.propertyValue;
				}
				finally
				{
					this.ResetState();
				}
				return result;
			}

			// Token: 0x0600018B RID: 395 RVA: 0x00005D0C File Offset: 0x00003F0C
			public override Expression Visit(Expression node)
			{
				ExAssert.RetailAssert(this.filter == null && this.propertyDefinition == null && !this.hasPropertyValue, "Cannot visit a new node until filter, propertyDefiniton or value from previous visit has been consumed.");
				if (node is BinaryExpression || node is ConstantExpression || node is MemberExpression || node is UnaryExpression || node is MethodCallExpression || node is NewArrayExpression || node is LambdaExpression)
				{
					return base.Visit(node);
				}
				return node;
			}

			// Token: 0x0600018C RID: 396 RVA: 0x00005D80 File Offset: 0x00003F80
			protected override Expression VisitBinary(BinaryExpression node)
			{
				if (!this.ProduceAndFilter(node) && !this.ProduceOrFilter(node) && !this.ProduceBlobComparisonFilter(node) && !this.ProduceStringComparisonFilter(node))
				{
					this.ProduceGeneralComparisonFilter(node);
				}
				return node;
			}

			// Token: 0x0600018D RID: 397 RVA: 0x00005DAF File Offset: 0x00003FAF
			protected override Expression VisitConstant(ConstantExpression node)
			{
				this.propertyValue = node.Value;
				this.hasPropertyValue = true;
				return node;
			}

			// Token: 0x0600018E RID: 398 RVA: 0x00005DC5 File Offset: 0x00003FC5
			protected override Expression VisitLambda<T>(Expression<T> node)
			{
				this.Visit(node.Body);
				return node;
			}

			// Token: 0x0600018F RID: 399 RVA: 0x00005DD5 File Offset: 0x00003FD5
			protected override Expression VisitMember(MemberExpression node)
			{
				if (this.ProducePropertyDefinition(node) || this.ProduceConstantValue(node))
				{
					return node;
				}
				return node;
			}

			// Token: 0x06000190 RID: 400 RVA: 0x00005DEC File Offset: 0x00003FEC
			protected override Expression VisitMethodCall(MethodCallExpression node)
			{
				if (this.ProduceTextFilter(node))
				{
					return node;
				}
				throw new UnsupportedExpressionException(Strings.UnsupportedMethodCall(node.Method));
			}

			// Token: 0x06000191 RID: 401 RVA: 0x00005E0C File Offset: 0x0000400C
			protected override Expression VisitNewArray(NewArrayExpression node)
			{
				if (node.Type == typeof(byte[]))
				{
					this.propertyValue = Expression.Lambda(node, new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]);
					this.hasPropertyValue = true;
				}
				return node;
			}

			// Token: 0x06000192 RID: 402 RVA: 0x00005E5C File Offset: 0x0000405C
			protected override Expression VisitUnary(UnaryExpression node)
			{
				ExpressionType nodeType = node.NodeType;
				if (nodeType != ExpressionType.Not)
				{
					if (nodeType == ExpressionType.Quote)
					{
						base.VisitUnary(node);
					}
				}
				else
				{
					this.filter = new NotFilter(this.GetFilter(node.Operand));
				}
				return node;
			}

			// Token: 0x06000193 RID: 403 RVA: 0x00005EA0 File Offset: 0x000040A0
			private bool ProduceAndFilter(BinaryExpression node)
			{
				if (node.NodeType == ExpressionType.AndAlso)
				{
					this.filter = new AndFilter(new QueryFilter[]
					{
						this.GetFilter(node.Left),
						this.GetFilter(node.Right)
					});
					return true;
				}
				return false;
			}

			// Token: 0x06000194 RID: 404 RVA: 0x00005EEC File Offset: 0x000040EC
			private bool ProduceBlobComparisonFilter(BinaryExpression node)
			{
				if (node.Left.Type == typeof(byte[]))
				{
					PropertyDefinition propertyDefinition = this.GetPropertyDefinition(node.Left);
					byte[] array = (byte[])this.GetPropertyValue(node.Right);
					this.filter = node.NodeType.ToBlobComparisonFilter(propertyDefinition, array);
					return true;
				}
				return false;
			}

			// Token: 0x06000195 RID: 405 RVA: 0x00005F4C File Offset: 0x0000414C
			private bool ProduceConstantValue(Expression node)
			{
				bool result;
				try
				{
					this.propertyValue = ReduceToConstantVisitor.Reduce<object>(node);
					this.hasPropertyValue = true;
					result = true;
				}
				catch (NotSupportedException)
				{
					result = false;
				}
				return result;
			}

			// Token: 0x06000196 RID: 406 RVA: 0x00005F88 File Offset: 0x00004188
			private bool ProduceGeneralComparisonFilter(BinaryExpression node)
			{
				PropertyDefinition property = this.GetPropertyDefinition(node.Left);
				object obj = this.GetPropertyValue(node.Right);
				this.filter = ((obj != null) ? new ComparisonFilter(node.NodeType.ToComparisonOperator(), property, obj) : node.NodeType.ToNullComparisonFilter(property));
				return true;
			}

			// Token: 0x06000197 RID: 407 RVA: 0x00005FDC File Offset: 0x000041DC
			private bool ProduceOrFilter(BinaryExpression node)
			{
				if (node.NodeType == ExpressionType.OrElse)
				{
					this.filter = new OrFilter(new QueryFilter[]
					{
						this.GetFilter(node.Left),
						this.GetFilter(node.Right)
					});
					return true;
				}
				return false;
			}

			// Token: 0x06000198 RID: 408 RVA: 0x00006028 File Offset: 0x00004228
			private bool ProducePropertyDefinition(MemberExpression node)
			{
				if (node.Expression is ParameterExpression)
				{
					PropertyInfo propertyInfo = node.Member as PropertyInfo;
					if (propertyInfo != null)
					{
						this.propertyDefinitionMap.TryGetPropertyDefinition(propertyInfo, out this.propertyDefinition);
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000199 RID: 409 RVA: 0x00006070 File Offset: 0x00004270
			private bool ProduceStringComparisonFilter(BinaryExpression node)
			{
				if (!node.Left.IsMethodCall(StringMethods.Compare))
				{
					return false;
				}
				MethodCallExpression methodCallExpression = (MethodCallExpression)node.Left;
				int num = ReduceToConstantVisitor.Reduce<int>(node.Right);
				if (num != 0)
				{
					throw new UnsupportedExpressionException(Strings.StringCompareMustCompareToZero);
				}
				this.filter = new ComparisonFilter(node.NodeType.ToComparisonOperator(), this.GetPropertyDefinition(methodCallExpression.Arguments[0]), this.GetPropertyValue(methodCallExpression.Arguments[1]));
				return true;
			}

			// Token: 0x0600019A RID: 410 RVA: 0x000060F4 File Offset: 0x000042F4
			private bool ProduceTextFilter(MethodCallExpression node)
			{
				MatchOptions? textFilterMatchOptions = node.Method.GetTextFilterMatchOptions();
				if (textFilterMatchOptions != null)
				{
					PropertyDefinition property = this.GetPropertyDefinition(node.Object);
					string text = (string)this.GetPropertyValue(node.Arguments[0]);
					this.filter = new TextFilter(property, text, textFilterMatchOptions.Value, MatchFlags.IgnoreCase);
					return true;
				}
				return false;
			}

			// Token: 0x0600019B RID: 411 RVA: 0x00006153 File Offset: 0x00004353
			private void ResetState()
			{
				this.filter = null;
				this.propertyDefinition = null;
				this.hasPropertyValue = false;
				this.propertyValue = null;
			}

			// Token: 0x04000077 RID: 119
			private readonly IPropertyDefinitionMap propertyDefinitionMap;

			// Token: 0x04000078 RID: 120
			private QueryFilter filter;

			// Token: 0x04000079 RID: 121
			private bool hasPropertyValue;

			// Token: 0x0400007A RID: 122
			private PropertyDefinition propertyDefinition;

			// Token: 0x0400007B RID: 123
			private object propertyValue;
		}
	}
}
