using System;

namespace Microsoft.Isam.Esent.Interop.Windows7
{
	// Token: 0x0200030D RID: 781
	[Flags]
	public enum CrashDumpGrbit
	{
		// Token: 0x04000989 RID: 2441
		None = 0,
		// Token: 0x0400098A RID: 2442
		Minimum = 1,
		// Token: 0x0400098B RID: 2443
		Maximum = 2,
		// Token: 0x0400098C RID: 2444
		CacheMinimum = 4,
		// Token: 0x0400098D RID: 2445
		CacheMaximum = 8,
		// Token: 0x0400098E RID: 2446
		CacheIncludeDirtyPages = 16,
		// Token: 0x0400098F RID: 2447
		CacheIncludeCachedPages = 32,
		// Token: 0x04000990 RID: 2448
		CacheIncludeCorruptedPages = 64
	}
}
