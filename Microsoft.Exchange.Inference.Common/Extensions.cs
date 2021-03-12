using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Inference.Common
{
	// Token: 0x02000025 RID: 37
	public static class Extensions
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00002F5C File Offset: 0x0000115C
		public static int IndexOf<TSource>(this IEnumerable<TSource> collection, TSource value, IEqualityComparer<TSource> comparer)
		{
			ArgumentValidator.ThrowIfNull("collection", collection);
			ArgumentValidator.ThrowIfNull("comparer", comparer);
			int num = 0;
			foreach (TSource x in collection)
			{
				if (comparer.Equals(x, value))
				{
					return num;
				}
				num++;
			}
			return -1;
		}
	}
}
