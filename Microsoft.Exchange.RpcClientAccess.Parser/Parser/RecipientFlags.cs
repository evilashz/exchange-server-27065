using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000081 RID: 129
	[Flags]
	internal enum RecipientFlags : ushort
	{
		// Token: 0x040001AD RID: 429
		None = 0,
		// Token: 0x040001AE RID: 430
		TransmitSameAsDisplayName = 64,
		// Token: 0x040001AF RID: 431
		Responsibility = 128,
		// Token: 0x040001B0 RID: 432
		SendNoRichInformation = 256,
		// Token: 0x040001B1 RID: 433
		ValidMask = 448
	}
}
