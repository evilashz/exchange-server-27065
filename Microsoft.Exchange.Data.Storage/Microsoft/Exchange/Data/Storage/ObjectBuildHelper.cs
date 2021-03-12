using System;
using System.Linq.Expressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E40 RID: 3648
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ObjectBuildHelper<T> where T : new()
	{
		// Token: 0x06007E97 RID: 32407 RVA: 0x0022C732 File Offset: 0x0022A932
		public static T Build()
		{
			return ObjectBuildHelper<T>.func();
		}

		// Token: 0x04005606 RID: 22022
		private static Expression<Func<T>> expression = Expression.Lambda<Func<T>>(Expression.New(typeof(T)), new ParameterExpression[0]);

		// Token: 0x04005607 RID: 22023
		private static Func<T> func = ObjectBuildHelper<T>.expression.Compile();
	}
}
