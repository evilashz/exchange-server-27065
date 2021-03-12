using System;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000341 RID: 833
	[Serializable]
	internal class ReplicationVersion
	{
		// Token: 0x04000DEB RID: 3563
		public static readonly ReplicationVersion CurrentVersion = new ReplicationVersion
		{
			Major = 2,
			Minor = 0
		};

		// Token: 0x04000DEC RID: 3564
		public int Major;

		// Token: 0x04000DED RID: 3565
		public int Minor;
	}
}
