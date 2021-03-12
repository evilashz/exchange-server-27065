using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004FA RID: 1274
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ExchangeVolumeInfoMultipleDbMountPointsException : DatabaseVolumeInfoException
	{
		// Token: 0x06002EDB RID: 11995 RVA: 0x000C43BE File Offset: 0x000C25BE
		public ExchangeVolumeInfoMultipleDbMountPointsException(string volumeName, string dbVolRootPath, string dbMountPoints, int maxDbs) : base(ReplayStrings.ExchangeVolumeInfoMultipleDbMountPointsException(volumeName, dbVolRootPath, dbMountPoints, maxDbs))
		{
			this.volumeName = volumeName;
			this.dbVolRootPath = dbVolRootPath;
			this.dbMountPoints = dbMountPoints;
			this.maxDbs = maxDbs;
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000C43F2 File Offset: 0x000C25F2
		public ExchangeVolumeInfoMultipleDbMountPointsException(string volumeName, string dbVolRootPath, string dbMountPoints, int maxDbs, Exception innerException) : base(ReplayStrings.ExchangeVolumeInfoMultipleDbMountPointsException(volumeName, dbVolRootPath, dbMountPoints, maxDbs), innerException)
		{
			this.volumeName = volumeName;
			this.dbVolRootPath = dbVolRootPath;
			this.dbMountPoints = dbMountPoints;
			this.maxDbs = maxDbs;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000C4428 File Offset: 0x000C2628
		protected ExchangeVolumeInfoMultipleDbMountPointsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.dbVolRootPath = (string)info.GetValue("dbVolRootPath", typeof(string));
			this.dbMountPoints = (string)info.GetValue("dbMountPoints", typeof(string));
			this.maxDbs = (int)info.GetValue("maxDbs", typeof(int));
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000C44C0 File Offset: 0x000C26C0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("dbVolRootPath", this.dbVolRootPath);
			info.AddValue("dbMountPoints", this.dbMountPoints);
			info.AddValue("maxDbs", this.maxDbs);
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x000C4519 File Offset: 0x000C2719
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x000C4521 File Offset: 0x000C2721
		public string DbVolRootPath
		{
			get
			{
				return this.dbVolRootPath;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x000C4529 File Offset: 0x000C2729
		public string DbMountPoints
		{
			get
			{
				return this.dbMountPoints;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x000C4531 File Offset: 0x000C2731
		public int MaxDbs
		{
			get
			{
				return this.maxDbs;
			}
		}

		// Token: 0x0400159A RID: 5530
		private readonly string volumeName;

		// Token: 0x0400159B RID: 5531
		private readonly string dbVolRootPath;

		// Token: 0x0400159C RID: 5532
		private readonly string dbMountPoints;

		// Token: 0x0400159D RID: 5533
		private readonly int maxDbs;
	}
}
