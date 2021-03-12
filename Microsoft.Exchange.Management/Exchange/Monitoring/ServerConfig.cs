using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200054C RID: 1356
	[Flags]
	internal enum ServerConfig
	{
		// Token: 0x0400226B RID: 8811
		Unknown = 0,
		// Token: 0x0400226C RID: 8812
		DagMemberNoDatabases = 1,
		// Token: 0x0400226D RID: 8813
		DagMember = 2,
		// Token: 0x0400226E RID: 8814
		Stopped = 4,
		// Token: 0x0400226F RID: 8815
		RcrSource = 8,
		// Token: 0x04002270 RID: 8816
		RcrTarget = 16
	}
}
