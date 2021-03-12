using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000506 RID: 1286
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DbFixupFailedVolumeHasMaxDbMountPointsException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F2A RID: 12074 RVA: 0x000C4FF0 File Offset: 0x000C31F0
		public DbFixupFailedVolumeHasMaxDbMountPointsException(string dbName, string volumeName) : base(ReplayStrings.DbFixupFailedVolumeHasMaxDbMountPointsException(dbName, volumeName))
		{
			this.dbName = dbName;
			this.volumeName = volumeName;
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000C5012 File Offset: 0x000C3212
		public DbFixupFailedVolumeHasMaxDbMountPointsException(string dbName, string volumeName, Exception innerException) : base(ReplayStrings.DbFixupFailedVolumeHasMaxDbMountPointsException(dbName, volumeName), innerException)
		{
			this.dbName = dbName;
			this.volumeName = volumeName;
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000C5038 File Offset: 0x000C3238
		protected DbFixupFailedVolumeHasMaxDbMountPointsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000C508D File Offset: 0x000C328D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("volumeName", this.volumeName);
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x000C50B9 File Offset: 0x000C32B9
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000C50C1 File Offset: 0x000C32C1
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x040015B9 RID: 5561
		private readonly string dbName;

		// Token: 0x040015BA RID: 5562
		private readonly string volumeName;
	}
}
