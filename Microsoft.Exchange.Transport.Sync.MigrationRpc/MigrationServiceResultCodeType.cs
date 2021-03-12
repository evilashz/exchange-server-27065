using System;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x0200000E RID: 14
	[Flags]
	internal enum MigrationServiceResultCodeType
	{
		// Token: 0x04000045 RID: 69
		Success = 4096,
		// Token: 0x04000046 RID: 70
		CommunicationPipelineError = 8192,
		// Token: 0x04000047 RID: 71
		TargetInvocationException = 16384,
		// Token: 0x04000048 RID: 72
		TransientError = 32768,
		// Token: 0x04000049 RID: 73
		ObjectNotHostedError = 256
	}
}
