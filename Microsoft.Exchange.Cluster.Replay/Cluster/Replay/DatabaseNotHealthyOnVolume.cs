using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200050E RID: 1294
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseNotHealthyOnVolume : DatabaseVolumeInfoException
	{
		// Token: 0x06002F5A RID: 12122 RVA: 0x000C569F File Offset: 0x000C389F
		public DatabaseNotHealthyOnVolume(string databaseName, string volumeName) : base(ReplayStrings.DatabaseNotHealthyOnVolume(databaseName, volumeName))
		{
			this.databaseName = databaseName;
			this.volumeName = volumeName;
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000C56C1 File Offset: 0x000C38C1
		public DatabaseNotHealthyOnVolume(string databaseName, string volumeName, Exception innerException) : base(ReplayStrings.DatabaseNotHealthyOnVolume(databaseName, volumeName), innerException)
		{
			this.databaseName = databaseName;
			this.volumeName = volumeName;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000C56E4 File Offset: 0x000C38E4
		protected DatabaseNotHealthyOnVolume(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.volumeName = (string)info.GetValue("volumeName", typeof(string));
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000C5739 File Offset: 0x000C3939
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("volumeName", this.volumeName);
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x000C5765 File Offset: 0x000C3965
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06002F5F RID: 12127 RVA: 0x000C576D File Offset: 0x000C396D
		public string VolumeName
		{
			get
			{
				return this.volumeName;
			}
		}

		// Token: 0x040015C9 RID: 5577
		private readonly string databaseName;

		// Token: 0x040015CA RID: 5578
		private readonly string volumeName;
	}
}
