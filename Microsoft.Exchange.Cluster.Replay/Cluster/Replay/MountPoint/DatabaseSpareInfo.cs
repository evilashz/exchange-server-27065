using System;

namespace Microsoft.Exchange.Cluster.Replay.MountPoint
{
	// Token: 0x02000242 RID: 578
	internal struct DatabaseSpareInfo
	{
		// Token: 0x06001623 RID: 5667 RVA: 0x0005A668 File Offset: 0x00058868
		public DatabaseSpareInfo(string dbName, MountedFolderPath dbMountPoint)
		{
			this.DbName = dbName;
			this.DatabaseMountPoint = dbMountPoint;
		}

		// Token: 0x040008B8 RID: 2232
		public string DbName;

		// Token: 0x040008B9 RID: 2233
		public MountedFolderPath DatabaseMountPoint;
	}
}
