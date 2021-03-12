using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200033B RID: 827
	internal class GranularLogCloseData
	{
		// Token: 0x04000DC4 RID: 3524
		public GranularLogCloseData.ChecksumAlgorithm ChecksumUsed;

		// Token: 0x04000DC5 RID: 3525
		public long Generation;

		// Token: 0x04000DC6 RID: 3526
		public DateTime LastWriteUtc;

		// Token: 0x04000DC7 RID: 3527
		public byte[] ChecksumBytes;

		// Token: 0x0200033C RID: 828
		public enum ChecksumAlgorithm : uint
		{
			// Token: 0x04000DC9 RID: 3529
			None,
			// Token: 0x04000DCA RID: 3530
			MD5,
			// Token: 0x04000DCB RID: 3531
			Sha1
		}
	}
}
