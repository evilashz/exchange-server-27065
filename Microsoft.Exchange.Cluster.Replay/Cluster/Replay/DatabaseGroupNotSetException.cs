using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000511 RID: 1297
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseGroupNotSetException : DatabaseVolumeInfoException
	{
		// Token: 0x06002F69 RID: 12137 RVA: 0x000C5830 File Offset: 0x000C3A30
		public DatabaseGroupNotSetException(string databaseName) : base(ReplayStrings.DatabaseGroupNotSetException(databaseName))
		{
			this.databaseName = databaseName;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000C584A File Offset: 0x000C3A4A
		public DatabaseGroupNotSetException(string databaseName, Exception innerException) : base(ReplayStrings.DatabaseGroupNotSetException(databaseName), innerException)
		{
			this.databaseName = databaseName;
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000C5865 File Offset: 0x000C3A65
		protected DatabaseGroupNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000C588F File Offset: 0x000C3A8F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06002F6D RID: 12141 RVA: 0x000C58AA File Offset: 0x000C3AAA
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x040015CC RID: 5580
		private readonly string databaseName;
	}
}
