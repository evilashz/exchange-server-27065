using System;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x02000238 RID: 568
	internal enum VolumeSpareStatus
	{
		// Token: 0x0400087A RID: 2170
		Unknown,
		// Token: 0x0400087B RID: 2171
		UnEncryptedEmptySpare,
		// Token: 0x0400087C RID: 2172
		EncryptingEmptySpare,
		// Token: 0x0400087D RID: 2173
		EncryptedEmptySpare,
		// Token: 0x0400087E RID: 2174
		Quarantined,
		// Token: 0x0400087F RID: 2175
		NotUsableAsSpare,
		// Token: 0x04000880 RID: 2176
		Error,
		// Token: 0x04000881 RID: 2177
		LastIndex
	}
}
