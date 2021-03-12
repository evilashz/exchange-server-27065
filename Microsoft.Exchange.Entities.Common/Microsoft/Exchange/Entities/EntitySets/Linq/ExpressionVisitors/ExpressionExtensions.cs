using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x02000041 RID: 65
	internal static class ExpressionExtensions
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000052AC File Offset: 0x000034AC
		public static MethodInfo GetGenericMethodDefinition(this MethodCallExpression expression)
		{
			MethodInfo method = expression.Method;
			if (!method.IsGenericMethod)
			{
				return method;
			}
			return method.GetGenericMethodDefinition();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000052D0 File Offset: 0x000034D0
		public static MethodInfo GetGenericMethodDefinition(this LambdaExpression expression)
		{
			return (expression.Body as MethodCallExpression).GetGenericMethodDefinition();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000052E2 File Offset: 0x000034E2
		public static bool IsConstantExpressionWithValue(this Expression expression, object expectedValue)
		{
			return expression is ConstantExpression && object.Equals((expression as ConstantExpression).Value, expectedValue);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005300 File Offset: 0x00003500
		public static bool IsMethodCall(this Expression expression, MethodInfo methodInfo)
		{
			if (expression is MethodCallExpression)
			{
				MethodInfo genericMethodDefinition = (expression as MethodCallExpression).GetGenericMethodDefinition();
				return genericMethodDefinition == methodInfo;
			}
			return false;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000532C File Offset: 0x0000352C
		public static bool IsMethodCall(this Expression expression, MethodInfo methodInfo1, MethodInfo methodInfo2)
		{
			if (expression is MethodCallExpression)
			{
				MethodInfo genericMethodDefinition = (expression as MethodCallExpression).GetGenericMethodDefinition();
				return genericMethodDefinition == methodInfo1 || genericMethodDefinition == methodInfo2;
			}
			return false;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005364 File Offset: 0x00003564
		public static Expression RemoveQuote(this Expression expression)
		{
			if (expression.NodeType == ExpressionType.Quote)
			{
				UnaryExpression unaryExpression = (UnaryExpression)expression;
				return unaryExpression.Operand;
			}
			return expression;
		}
	}
}
