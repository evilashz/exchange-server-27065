using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004FC RID: 1276
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotCreateDbDirectoriesException : DatabaseVolumeInfoException
	{
		// Token: 0x06002EE8 RID: 12008 RVA: 0x000C45BB File Offset: 0x000C27BB
		public CouldNotCreateDbDirectoriesException(string database, string volumeName, string errMsg) : base(ReplayStrings.CouldNotCreateDbDirectoriesException(database, volumeName, errMsg))
		{
			this.database = database;
			this.volumeName = volumeName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000C45E5 File Offset: 0x000C27E5
		public CouldNotCreateDbDirectoriesException(string database, string volumeName, string errMsg, Exception innerException) : base(ReplayStrings.CouldNotCreateDbDirectoriesException(database, volumeName, errMsg), innerException)
		{
			this.database = database;
			this.volumeName = volumeName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000C4614 File Offset: 0x000C2814
		protected CouldNotCreateDbDirectoriesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000C4689 File Offset: 0x000C2889
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002EEC RID: 12012 RVA: 0x000C46C6 File Offset: 0x000C28C6
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000C46CE File Offset: 0x000C28CE
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000C46D6 File Offset: 0x000C28D6
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x0400159F RID: 5535
		private readonly string database;

		// Token: 0x040015A0 RID: 5536
		private readonly string volumeName;

		// Token: 0x040015A1 RID: 5537
		private readonly string errMsg;
	}
}
