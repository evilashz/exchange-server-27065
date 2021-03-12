using System;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x02000042 RID: 66
	public static class ExpressionNormalizer
	{
		// Token: 0x06000174 RID: 372 RVA: 0x0000538A File Offset: 0x0000358A
		public static Expression Normalize(this Expression expression)
		{
			return ExpressionNormalizer.Visitor.Visit(expression);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005398 File Offset: 0x00003598
		private static bool IsNoOpSelectCall(this Expression expression)
		{
			if (expression.IsMethodCall(QueryableMethods.Select, QueryableMethods.IndexedSelect))
			{
				MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
				LambdaExpression lambdaExpression = methodCallExpression.Arguments[1].FindLambdaExpression();
				return lambdaExpression != null && lambdaExpression.Body == lambdaExpression.Parameters[0];
			}
			return false;
		}

		// Token: 0x04000065 RID: 101
		private static readonly ExpressionNormalizer.NormalizeVisitor Visitor = new ExpressionNormalizer.NormalizeVisitor();

		// Token: 0x02000043 RID: 67
		private class NormalizeVisitor : ExpressionVisitor
		{
			// Token: 0x06000177 RID: 375 RVA: 0x000053F7 File Offset: 0x000035F7
			protected override Expression VisitMethodCall(MethodCallExpression node)
			{
				if (node.IsNoOpSelectCall())
				{
					return this.Visit(node.Arguments[0]);
				}
				return base.VisitMethodCall(node);
			}

			// Token: 0x06000178 RID: 376 RVA: 0x0000541B File Offset: 0x0000361B
			protected override Expression VisitUnary(UnaryExpression node)
			{
				if (node.NodeType == ExpressionType.Convert && node.Type == node.Operand.Type)
				{
					return this.Visit(node.Operand);
				}
				return base.VisitUnary(node);
			}
		}
	}
}
