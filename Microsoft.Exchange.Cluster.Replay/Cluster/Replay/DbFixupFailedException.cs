using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000507 RID: 1287
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DbFixupFailedException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F30 RID: 12080 RVA: 0x000C50C9 File Offset: 0x000C32C9
		public DbFixupFailedException(string dbName, string volumeName, string reason) : base(ReplayStrings.DbFixupFailedException(dbName, volumeName, reason))
		{
			this.dbName = dbName;
			this.volumeName = volumeName;
			this.reason = reason;
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000C50F3 File Offset: 0x000C32F3
		public DbFixupFailedException(string dbName, string volumeName, string reason, Exception innerException) : base(ReplayStrings.DbFixupFailedException(dbName, volumeName, reason), innerException)
		{
			this.dbName = dbName;
			this.volumeName = volumeName;
			this.reason = reason;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000C5120 File Offset: 0x000C3320
		protected DbFixupFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
			this.reason = (string)info.GetValue("reason", typeof(string));
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000C5195 File Offset: 0x000C3395
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("volumeName", this.volumeName);
			info.AddValue("reason", this.reason);
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x000C51D2 File Offset: 0x000C33D2
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x000C51DA File Offset: 0x000C33DA
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000C51E2 File Offset: 0x000C33E2
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040015BB RID: 5563
		private readonly string dbName;

		// Token: 0x040015BC RID: 5564
		private readonly string volumeName;

		// Token: 0x040015BD RID: 5565
		private readonly string reason;
	}
}
