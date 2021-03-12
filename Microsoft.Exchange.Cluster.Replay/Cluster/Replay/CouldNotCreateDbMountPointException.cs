using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004FE RID: 1278
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotCreateDbMountPointException : DatabaseVolumeInfoException
	{
		// Token: 0x06002EF6 RID: 12022 RVA: 0x000C47FE File Offset: 0x000C29FE
		public CouldNotCreateDbMountPointException(string database, string dbMountPoint, string volumeName, string errMsg) : base(ReplayStrings.CouldNotCreateDbMountPointException(database, dbMountPoint, volumeName, errMsg))
		{
			this.database = database;
			this.dbMountPoint = dbMountPoint;
			this.volumeName = volumeName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000C4832 File Offset: 0x000C2A32
		public CouldNotCreateDbMountPointException(string database, string dbMountPoint, string volumeName, string errMsg, Exception innerException) : base(ReplayStrings.CouldNotCreateDbMountPointException(database, dbMountPoint, volumeName, errMsg), innerException)
		{
			this.database = database;
			this.dbMountPoint = dbMountPoint;
			this.volumeName = volumeName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000C4868 File Offset: 0x000C2A68
		protected CouldNotCreateDbMountPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.dbMountPoint = (string)info.GetValue("dbMountPoint", typeof(string));
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000C4900 File Offset: 0x000C2B00
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("dbMountPoint", this.dbMountPoint);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000C4959 File Offset: 0x000C2B59
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000C4961 File Offset: 0x000C2B61
		public string DbMountPoint
		{
			get
			{
				return this.dbMountPoint;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000C4969 File Offset: 0x000C2B69
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x000C4971 File Offset: 0x000C2B71
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040015A5 RID: 5541
		private readonly string database;

		// Token: 0x040015A6 RID: 5542
		private readonly string dbMountPoint;

		// Token: 0x040015A7 RID: 5543
		private readonly string volumeName;

		// Token: 0x040015A8 RID: 5544
		private readonly string errMsg;
	}
}
