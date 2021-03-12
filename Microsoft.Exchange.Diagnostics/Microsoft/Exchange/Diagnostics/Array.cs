using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000162 RID: 354
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class Array<T>
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x00026199 File Offset: 0x00024399
		public static T[] New(int size)
		{
			if (size != 0)
			{
				return new T[size];
			}
			return Array<T>.Empty;
		}

		// Token: 0x040006E6 RID: 1766
		public static readonly T[] Empty = new T[0];

		// Token: 0x040006E7 RID: 1767
		public static readonly ArraySegment<T> EmptySegment = new ArraySegment<T>(Array<T>.Empty);
	}
}
