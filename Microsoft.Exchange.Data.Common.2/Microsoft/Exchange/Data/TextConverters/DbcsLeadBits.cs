using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000180 RID: 384
	[Flags]
	internal enum DbcsLeadBits : byte
	{
		// Token: 0x04001148 RID: 4424
		Lead1361 = 1,
		// Token: 0x04001149 RID: 4425
		Lead10001 = 2,
		// Token: 0x0400114A RID: 4426
		Lead10002 = 4,
		// Token: 0x0400114B RID: 4427
		Lead10003 = 8,
		// Token: 0x0400114C RID: 4428
		Lead10008 = 16,
		// Token: 0x0400114D RID: 4429
		Lead932 = 32,
		// Token: 0x0400114E RID: 4430
		Lead9XX = 64
	}
}
