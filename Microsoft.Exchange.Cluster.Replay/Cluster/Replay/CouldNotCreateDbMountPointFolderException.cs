using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004FF RID: 1279
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotCreateDbMountPointFolderException : DatabaseVolumeInfoException
	{
		// Token: 0x06002EFE RID: 12030 RVA: 0x000C4979 File Offset: 0x000C2B79
		public CouldNotCreateDbMountPointFolderException(string database, string dbMountPoint, string errMsg) : base(ReplayStrings.CouldNotCreateDbMountPointFolderException(database, dbMountPoint, errMsg))
		{
			this.database = database;
			this.dbMountPoint = dbMountPoint;
			this.errMsg = errMsg;
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000C49A3 File Offset: 0x000C2BA3
		public CouldNotCreateDbMountPointFolderException(string database, string dbMountPoint, string errMsg, Exception innerException) : base(ReplayStrings.CouldNotCreateDbMountPointFolderException(database, dbMountPoint, errMsg), innerException)
		{
			this.database = database;
			this.dbMountPoint = dbMountPoint;
			this.errMsg = errMsg;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000C49D0 File Offset: 0x000C2BD0
		protected CouldNotCreateDbMountPointFolderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.dbMountPoint = (string)info.GetValue("dbMountPoint", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000C4A45 File Offset: 0x000C2C45
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("dbMountPoint", this.dbMountPoint);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x000C4A82 File Offset: 0x000C2C82
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x000C4A8A File Offset: 0x000C2C8A
		public string DbMountPoint
		{
			get
			{
				return this.dbMountPoint;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000C4A92 File Offset: 0x000C2C92
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040015A9 RID: 5545
		private readonly string database;

		// Token: 0x040015AA RID: 5546
		private readonly string dbMountPoint;

		// Token: 0x040015AB RID: 5547
		private readonly string errMsg;
	}
}
