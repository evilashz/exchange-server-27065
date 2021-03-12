using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000416 RID: 1046
	[Flags]
	internal enum SecureState : byte
	{
		// Token: 0x0400178E RID: 6030
		None = 0,
		// Token: 0x0400178F RID: 6031
		StartTls = 1,
		// Token: 0x04001790 RID: 6032
		AnonymousTls = 2,
		// Token: 0x04001791 RID: 6033
		NegotiationRequested = 128
	}
}
