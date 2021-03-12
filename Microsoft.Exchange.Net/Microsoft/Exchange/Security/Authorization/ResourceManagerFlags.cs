using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x0200001B RID: 27
	[Flags]
	internal enum ResourceManagerFlags
	{
		// Token: 0x0400007F RID: 127
		None = 0,
		// Token: 0x04000080 RID: 128
		NoAudit = 1,
		// Token: 0x04000081 RID: 129
		InitializeUnderImpersonation = 2
	}
}
