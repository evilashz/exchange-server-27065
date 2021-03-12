using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200050A RID: 1290
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabasesMissingInADException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F42 RID: 12098 RVA: 0x000C5345 File Offset: 0x000C3545
		public DatabasesMissingInADException(string databaseName, string volumeName) : base(ReplayStrings.DatabasesMissingInADException(databaseName, volumeName))
		{
			this.databaseName = databaseName;
			this.volumeName = volumeName;
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000C5367 File Offset: 0x000C3567
		public DatabasesMissingInADException(string databaseName, string volumeName, Exception innerException) : base(ReplayStrings.DatabasesMissingInADException(databaseName, volumeName), innerException)
		{
			this.databaseName = databaseName;
			this.volumeName = volumeName;
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000C538C File Offset: 0x000C358C
		protected DatabasesMissingInADException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x000C53E1 File Offset: 0x000C35E1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("volumeName", this.volumeName);
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000C540D File Offset: 0x000C360D
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06002F47 RID: 12103 RVA: 0x000C5415 File Offset: 0x000C3615
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x040015C1 RID: 5569
		private readonly string databaseName;

		// Token: 0x040015C2 RID: 5570
		private readonly string volumeName;
	}
}
