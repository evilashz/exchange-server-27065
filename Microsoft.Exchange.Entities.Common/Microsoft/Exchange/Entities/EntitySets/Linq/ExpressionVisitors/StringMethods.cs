using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Entities.EntitySets.Linq.ExpressionVisitors
{
	// Token: 0x0200004B RID: 75
	internal static class StringMethods
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00006230 File Offset: 0x00004430
		public static MatchOptions? GetTextFilterMatchOptions(this MethodInfo method)
		{
			if (method == StringMethods.Contains)
			{
				return new MatchOptions?(MatchOptions.SubString);
			}
			if (method == StringMethods.EndsWith)
			{
				return new MatchOptions?(MatchOptions.Suffix);
			}
			if (method == StringMethods.StartsWith)
			{
				return new MatchOptions?(MatchOptions.Prefix);
			}
			return null;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006282 File Offset: 0x00004482
		private static MethodInfo GetMethod<TReturn>(Expression<Func<string, TReturn>> expression)
		{
			return ((MethodCallExpression)expression.Body).Method;
		}

		// Token: 0x0400007C RID: 124
		public static readonly MethodInfo Compare = StringMethods.GetMethod<int>((string x) => string.Compare(null, null));

		// Token: 0x0400007D RID: 125
		public static readonly MethodInfo Contains = StringMethods.GetMethod<bool>((string s) => s.Contains(null));

		// Token: 0x0400007E RID: 126
		public static readonly MethodInfo EndsWith = StringMethods.GetMethod<bool>((string s) => s.EndsWith(null));

		// Token: 0x0400007F RID: 127
		public static readonly MethodInfo StartsWith = StringMethods.GetMethod<bool>((string s) => s.StartsWith(null));
	}
}
