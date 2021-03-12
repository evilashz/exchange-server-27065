using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x02000044 RID: 68
	internal static class FilterAnalyzer
	{
		// Token: 0x0600017A RID: 378 RVA: 0x0000545C File Offset: 0x0000365C
		public static bool IsWhereIdEqualsKey<TKey>(this Expression expression, out TKey key)
		{
			key = default(TKey);
			LambdaExpression lambdaExpression = expression.FindLambdaExpression();
			if (lambdaExpression != null)
			{
				BinaryExpression binaryExpression = lambdaExpression.Body as BinaryExpression;
				if (binaryExpression != null && binaryExpression.NodeType == ExpressionType.Equal && binaryExpression.Left.Type == typeof(TKey) && binaryExpression.Right.Type == typeof(TKey) && binaryExpression.Left is MemberExpression && ReduceToConstantVisitor.CanReduce(binaryExpression.Right))
				{
					MemberExpression memberExpression = (MemberExpression)binaryExpression.Left;
					MemberInfo member = memberExpression.Member;
					if (memberExpression.Expression == lambdaExpression.Parameters[0] && member.MemberType == MemberTypes.Property && member.Name == "Id")
					{
						key = ReduceToConstantVisitor.Reduce<TKey>(binaryExpression.Right);
						return true;
					}
				}
			}
			return false;
		}
	}
}
