using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000032 RID: 50
	[Flags]
	public enum Days : byte
	{
		// Token: 0x04000108 RID: 264
		None = 0,
		// Token: 0x04000109 RID: 265
		Sunday = 1,
		// Token: 0x0400010A RID: 266
		Monday = 2,
		// Token: 0x0400010B RID: 267
		Tuesday = 4,
		// Token: 0x0400010C RID: 268
		Wednesday = 8,
		// Token: 0x0400010D RID: 269
		Thursday = 16,
		// Token: 0x0400010E RID: 270
		Friday = 32,
		// Token: 0x0400010F RID: 271
		Saturday = 64
	}
}
