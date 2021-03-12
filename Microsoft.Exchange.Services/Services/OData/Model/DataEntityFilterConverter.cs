using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EA4 RID: 3748
	internal class DataEntityFilterConverter : ODataFilterConverter
	{
		// Token: 0x0600619A RID: 24986 RVA: 0x001308E4 File Offset: 0x0012EAE4
		public DataEntityFilterConverter(EntitySchema schema) : base(schema)
		{
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x001308F0 File Offset: 0x0012EAF0
		public Expression ConvertFilterClause(FilterClause filterClause)
		{
			ArgumentValidator.ThrowIfNull("filterClause", filterClause);
			Expression body = this.ConvertFilterNode(filterClause.Expression);
			return Expression.Lambda(body, new ParameterExpression[]
			{
				this.parameterExpression
			});
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x0013092E File Offset: 0x0012EB2E
		public IReadOnlyList<OrderByClause> ConvertOrderByClause(OrderByClause orderByClause)
		{
			ArgumentValidator.ThrowIfNull("orderByClause", orderByClause);
			return null;
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x0013093C File Offset: 0x0012EB3C
		private Expression ConvertFilterNode(QueryNode queryNode)
		{
			Expression expression = null;
			if (queryNode.Kind == 5)
			{
				UnaryOperatorNode unaryOperatorNode = (UnaryOperatorNode)queryNode;
				if (unaryOperatorNode.OperatorKind == 1)
				{
					expression = Expression.Not(this.ConvertFilterNode(unaryOperatorNode.Operand));
				}
			}
			else if (queryNode.Kind == 4)
			{
				BinaryOperatorNode binaryOperatorNode = (BinaryOperatorNode)queryNode;
				if (binaryOperatorNode.OperatorKind == 1 || binaryOperatorNode.OperatorKind == null)
				{
					Expression left = this.ConvertFilterNode(binaryOperatorNode.Left);
					Expression right = this.ConvertFilterNode(binaryOperatorNode.Right);
					if (binaryOperatorNode.OperatorKind == 1)
					{
						expression = Expression.AndAlso(left, right);
					}
					else
					{
						expression = Expression.OrElse(left, right);
					}
				}
				else
				{
					IExpressionQueryBuilder propertyProvider = this.GetPropertyProvider(binaryOperatorNode.Left);
					MemberExpression queryPropertyExpression = propertyProvider.GetQueryPropertyExpression();
					this.parameterExpression = (ParameterExpression)queryPropertyExpression.Expression;
					Expression constantExpression = this.GetConstantExpression(binaryOperatorNode.Right, base.GetEntityProperty(binaryOperatorNode.Left), queryPropertyExpression.Type);
					switch (binaryOperatorNode.OperatorKind)
					{
					case 2:
						expression = Expression.Equal(queryPropertyExpression, constantExpression);
						break;
					case 3:
						expression = Expression.NotEqual(queryPropertyExpression, constantExpression);
						break;
					case 4:
						expression = Expression.GreaterThan(queryPropertyExpression, constantExpression);
						break;
					case 5:
						expression = Expression.GreaterThanOrEqual(queryPropertyExpression, constantExpression);
						break;
					case 6:
						expression = Expression.LessThan(queryPropertyExpression, constantExpression);
						break;
					case 7:
						expression = Expression.LessThanOrEqual(queryPropertyExpression, constantExpression);
						break;
					}
				}
			}
			else if (queryNode.Kind == 8)
			{
				SingleValueFunctionCallNode singleValueFunctionCallNode = (SingleValueFunctionCallNode)queryNode;
				ODataFilterConverter.BinaryOperandPair binaryOperandPair = base.ParseBinaryFunctionParameters(singleValueFunctionCallNode);
				IExpressionQueryBuilder propertyProvider2 = this.GetPropertyProvider(binaryOperandPair.Left);
				MemberExpression queryPropertyExpression2 = propertyProvider2.GetQueryPropertyExpression();
				this.parameterExpression = (ParameterExpression)queryPropertyExpression2.Expression;
				Expression constantExpression2 = this.GetConstantExpression(binaryOperandPair.Right, base.GetEntityProperty(binaryOperandPair.Left));
				string name;
				if ((name = singleValueFunctionCallNode.Name) != null)
				{
					if (!(name == "contains"))
					{
						if (name == "startswith")
						{
							expression = Expression.Call(queryPropertyExpression2, "StartsWith", null, new Expression[]
							{
								constantExpression2
							});
						}
					}
					else
					{
						expression = Expression.Call(queryPropertyExpression2, "Contains", null, new Expression[]
						{
							constantExpression2
						});
					}
				}
			}
			if (expression == null)
			{
				throw new InvalidFilterNodeException(queryNode);
			}
			return expression;
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x00130B8B File Offset: 0x0012ED8B
		private IExpressionQueryBuilder GetPropertyProvider(QueryNode queryNode)
		{
			return base.GetEntityProperty(queryNode).DataEntityPropertyProvider as IExpressionQueryBuilder;
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x00130B9E File Offset: 0x0012ED9E
		private Expression GetConstantExpression(QueryNode queryNode, PropertyDefinition propertyDefinition)
		{
			return this.GetConstantExpression(queryNode, propertyDefinition, propertyDefinition.Type);
		}

		// Token: 0x060061A0 RID: 24992 RVA: 0x00130BB0 File Offset: 0x0012EDB0
		private Expression GetConstantExpression(QueryNode queryNode, PropertyDefinition propertyDefinition, Type type)
		{
			IExpressionQueryBuilder expressionQueryBuilder = propertyDefinition.DataEntityPropertyProvider as IExpressionQueryBuilder;
			return expressionQueryBuilder.GetQueryConstant(base.ExtractConstantNodeValue(queryNode, type));
		}

		// Token: 0x040034CE RID: 13518
		private ParameterExpression parameterExpression;
	}
}
