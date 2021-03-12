using System;

namespace Microsoft.Exchange.Connections.Eas.Commands.Connect
{
	// Token: 0x0200002C RID: 44
	[Flags]
	public enum ConnectStatus
	{
		// Token: 0x04000104 RID: 260
		IsPermanent = 1,
		// Token: 0x04000105 RID: 261
		RequiresSyncKeyReset = 2,
		// Token: 0x04000106 RID: 262
		Success = 4,
		// Token: 0x04000107 RID: 263
		AutodiscoverFailed = 8
	}
}
