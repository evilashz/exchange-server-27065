using System;

namespace Microsoft.Exchange.RpcClientAccess.Diagnostics
{
	// Token: 0x0200002C RID: 44
	[Flags]
	internal enum ProtocolLoggingTag
	{
		// Token: 0x0400013C RID: 316
		None = 0,
		// Token: 0x0400013D RID: 317
		ConnectDisconnect = 1,
		// Token: 0x0400013E RID: 318
		Rops = 2,
		// Token: 0x0400013F RID: 319
		OperationSpecific = 4,
		// Token: 0x04000140 RID: 320
		ApplicationData = 8,
		// Token: 0x04000141 RID: 321
		Failures = 16,
		// Token: 0x04000142 RID: 322
		Logon = 32,
		// Token: 0x04000143 RID: 323
		Throttling = 64,
		// Token: 0x04000144 RID: 324
		Warnings = 128
	}
}
