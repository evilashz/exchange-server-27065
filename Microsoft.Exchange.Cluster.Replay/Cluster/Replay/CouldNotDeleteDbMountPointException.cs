using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004FD RID: 1277
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotDeleteDbMountPointException : DatabaseVolumeInfoException
	{
		// Token: 0x06002EEF RID: 12015 RVA: 0x000C46DE File Offset: 0x000C28DE
		public CouldNotDeleteDbMountPointException(string database, string dbMountPoint, string errMsg) : base(ReplayStrings.CouldNotDeleteDbMountPointException(database, dbMountPoint, errMsg))
		{
			this.database = database;
			this.dbMountPoint = dbMountPoint;
			this.errMsg = errMsg;
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000C4708 File Offset: 0x000C2908
		public CouldNotDeleteDbMountPointException(string database, string dbMountPoint, string errMsg, Exception innerException) : base(ReplayStrings.CouldNotDeleteDbMountPointException(database, dbMountPoint, errMsg), innerException)
		{
			this.database = database;
			this.dbMountPoint = dbMountPoint;
			this.errMsg = errMsg;
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000C4734 File Offset: 0x000C2934
		protected CouldNotDeleteDbMountPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.dbMountPoint = (string)info.GetValue("dbMountPoint", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000C47A9 File Offset: 0x000C29A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("dbMountPoint", this.dbMountPoint);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000C47E6 File Offset: 0x000C29E6
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000C47EE File Offset: 0x000C29EE
		public string DbMountPoint
		{
			get
			{
				return this.dbMountPoint;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000C47F6 File Offset: 0x000C29F6
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040015A2 RID: 5538
		private readonly string database;

		// Token: 0x040015A3 RID: 5539
		private readonly string dbMountPoint;

		// Token: 0x040015A4 RID: 5540
		private readonly string errMsg;
	}
}
