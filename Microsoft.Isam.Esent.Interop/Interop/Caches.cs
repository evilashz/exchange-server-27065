using System;
using System.Diagnostics;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000099 RID: 153
	internal static class Caches
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x000104E5 File Offset: 0x0000E6E5
		public static MemoryCache ColumnCache
		{
			[DebuggerStepThrough]
			get
			{
				return Caches.TheColumnCache;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x000104EC File Offset: 0x0000E6EC
		public static MemoryCache BookmarkCache
		{
			[DebuggerStepThrough]
			get
			{
				return Caches.TheBookmarkCache;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x000104F3 File Offset: 0x0000E6F3
		public static MemoryCache SecondaryBookmarkCache
		{
			[DebuggerStepThrough]
			get
			{
				return Caches.TheSecondaryBookmarkCache;
			}
		}

		// Token: 0x04000311 RID: 785
		private const int KeyMostMost = 2000;

		// Token: 0x04000312 RID: 786
		private const int MaxBuffers = 16;

		// Token: 0x04000313 RID: 787
		private static readonly MemoryCache TheColumnCache = new MemoryCache(131072, 16);

		// Token: 0x04000314 RID: 788
		private static readonly MemoryCache TheBookmarkCache = new MemoryCache(2000, 16);

		// Token: 0x04000315 RID: 789
		private static readonly MemoryCache TheSecondaryBookmarkCache = new MemoryCache(2000, 16);
	}
}
