using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200007D RID: 125
	public static class DynamicExpression
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0000FB80 File Offset: 0x0000DD80
		public static Expression Parse(Type resultType, string expression, params object[] values)
		{
			ExpressionParser expressionParser = new ExpressionParser(null, expression, values);
			return expressionParser.Parse(resultType);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000FBA0 File Offset: 0x0000DDA0
		public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
		{
			return DynamicExpression.ParseLambda(new ParameterExpression[]
			{
				Expression.Parameter(itType, "")
			}, resultType, expression, null, values);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, Type[] servicePredefinedTypes, params object[] values)
		{
			ExpressionParser expressionParser = new ExpressionParser(parameters, expression, servicePredefinedTypes, values);
			if (parameters.Length < 5)
			{
				return Expression.Lambda(expressionParser.Parse(resultType), parameters);
			}
			List<Type> list = (from param in parameters
			select param.Type).ToList<Type>();
			list.Add(resultType);
			Type delegateType = DynamicExpression.funcTypes[parameters.Length - 5].MakeGenericType(list.ToArray());
			return Expression.Lambda(delegateType, expressionParser.Parse(resultType), parameters);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000FC55 File Offset: 0x0000DE55
		public static Expression<Func<T, S>> ParseLambda<T, S>(string expression, params object[] values)
		{
			return (Expression<Func<T, S>>)DynamicExpression.ParseLambda(typeof(T), typeof(S), expression, values);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000FC77 File Offset: 0x0000DE77
		public static Type CreateClass(params DynamicProperty[] properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000FC84 File Offset: 0x0000DE84
		public static Type CreateClass(IEnumerable<DynamicProperty> properties)
		{
			return ClassFactory.Instance.GetDynamicClass(properties);
		}

		// Token: 0x04000117 RID: 279
		private static readonly Type[] funcTypes = new Type[]
		{
			typeof(Func<, , , , , >),
			typeof(Func<, , , , , , >)
		};
	}
}
