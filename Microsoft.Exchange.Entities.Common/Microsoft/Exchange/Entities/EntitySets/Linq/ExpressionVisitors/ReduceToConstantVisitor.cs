using System;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x0200004A RID: 74
	internal class ReduceToConstantVisitor : ExpressionVisitor
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00006171 File Offset: 0x00004371
		public static bool CanReduce(Expression expression)
		{
			return expression is ConstantExpression || expression is MemberExpression;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006188 File Offset: 0x00004388
		public static T Reduce<T>(Expression expression)
		{
			ReduceToConstantVisitor reduceToConstantVisitor = new ReduceToConstantVisitor();
			ConstantExpression constantExpression = (ConstantExpression)reduceToConstantVisitor.Visit(expression);
			return (T)((object)constantExpression.Value);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000061B5 File Offset: 0x000043B5
		public override Expression Visit(Expression node)
		{
			if (node == null || ReduceToConstantVisitor.CanReduce(node))
			{
				return base.Visit(node);
			}
			throw new NotSupportedException(string.Format("TODO: LOC: ReduceToConstantVisitor needs to handle {0}", node.GetType().Name));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000061E4 File Offset: 0x000043E4
		protected override Expression VisitMember(MemberExpression node)
		{
			MemberExpression body = node.Update(this.Visit(node.Expression));
			object value = Expression.Lambda(body, new ParameterExpression[0]).Compile().DynamicInvoke(new object[0]);
			return Expression.Constant(value);
		}
	}
}
