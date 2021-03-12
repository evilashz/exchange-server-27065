using System;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x02000045 RID: 69
	public static class LamdbaExpressionFinder
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00005548 File Offset: 0x00003748
		public static LambdaExpression FindLambdaExpression(this Expression expression)
		{
			LamdbaExpressionFinder.FindLambdaExpressionVisitor findLambdaExpressionVisitor = new LamdbaExpressionFinder.FindLambdaExpressionVisitor();
			findLambdaExpressionVisitor.Visit(expression);
			return findLambdaExpressionVisitor.LambdaExpression;
		}

		// Token: 0x02000046 RID: 70
		private class FindLambdaExpressionVisitor : ExpressionVisitor
		{
			// Token: 0x17000047 RID: 71
			// (get) Token: 0x0600017C RID: 380 RVA: 0x00005569 File Offset: 0x00003769
			// (set) Token: 0x0600017D RID: 381 RVA: 0x00005571 File Offset: 0x00003771
			public LambdaExpression LambdaExpression { get; private set; }

			// Token: 0x0600017E RID: 382 RVA: 0x0000557A File Offset: 0x0000377A
			public override Expression Visit(Expression node)
			{
				if (this.LambdaExpression == null)
				{
					base.Visit(node);
				}
				return node;
			}

			// Token: 0x0600017F RID: 383 RVA: 0x0000558D File Offset: 0x0000378D
			protected override Expression VisitLambda<T>(Expression<T> node)
			{
				this.LambdaExpression = node;
				return node;
			}
		}
	}
}
