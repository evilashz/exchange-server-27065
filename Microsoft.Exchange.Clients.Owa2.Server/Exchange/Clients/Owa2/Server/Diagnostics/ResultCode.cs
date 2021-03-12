using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000457 RID: 1111
	internal enum ResultCode
	{
		// Token: 0x0400157F RID: 5503
		Success,
		// Token: 0x04001580 RID: 5504
		DebugBuild,
		// Token: 0x04001581 RID: 5505
		NoStackTrace,
		// Token: 0x04001582 RID: 5506
		VersionMismatch,
		// Token: 0x04001583 RID: 5507
		NoTraceComponent,
		// Token: 0x04001584 RID: 5508
		NoMatch,
		// Token: 0x04001585 RID: 5509
		FailedToDeConsolidate,
		// Token: 0x04001586 RID: 5510
		FailedToDeMinify,
		// Token: 0x04001587 RID: 5511
		FailedToDeObfuscate,
		// Token: 0x04001588 RID: 5512
		NoFrames
	}
}
