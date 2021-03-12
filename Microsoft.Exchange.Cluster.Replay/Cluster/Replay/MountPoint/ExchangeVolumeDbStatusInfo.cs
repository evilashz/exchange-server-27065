using System;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x02000243 RID: 579
	internal struct ExchangeVolumeDbStatusInfo
	{
		// Token: 0x040008BA RID: 2234
		public bool DbFilesFound;

		// Token: 0x040008BB RID: 2235
		public bool DbMissingInAD;

		// Token: 0x040008BC RID: 2236
		public bool DbCopyStatusMissingOrFailedToRetrieve;

		// Token: 0x040008BD RID: 2237
		public bool DbCopyStatusNotHealthy;

		// Token: 0x040008BE RID: 2238
		public bool UnknownFilesFound;

		// Token: 0x040008BF RID: 2239
		public Exception DbFilesException;

		// Token: 0x040008C0 RID: 2240
		public Exception UnknownFilesException;
	}
}
