using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x0200001E RID: 30
	[Flags]
	public enum AutodiscoverStatus
	{
		// Token: 0x040000DE RID: 222
		Success = 1,
		// Token: 0x040000DF RID: 223
		ProtocolError = 4098,
		// Token: 0x040000E0 RID: 224
		LowOrderByte = 255,
		// Token: 0x040000E1 RID: 225
		EveryStepFailed = 260,
		// Token: 0x040000E2 RID: 226
		StatusOutOfRange = 511,
		// Token: 0x040000E3 RID: 227
		TransientError = 256,
		// Token: 0x040000E4 RID: 228
		PermanentError = 4096
	}
}
