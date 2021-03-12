using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000069 RID: 105
	internal static class Extensions
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		public static IEnumerable<T> Cache<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			return new CachedIterator<T>(enumerable.GetEnumerator());
		}
	}
}
