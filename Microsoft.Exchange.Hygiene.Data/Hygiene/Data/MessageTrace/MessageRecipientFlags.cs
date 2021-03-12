using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000163 RID: 355
	[Flags]
	internal enum MessageRecipientFlags
	{
		// Token: 0x040006A3 RID: 1699
		None = 0,
		// Token: 0x040006A4 RID: 1700
		Quarantined = 1,
		// Token: 0x040006A5 RID: 1701
		Notified = 2,
		// Token: 0x040006A6 RID: 1702
		Reported = 4,
		// Token: 0x040006A7 RID: 1703
		Released = 8
	}
}
