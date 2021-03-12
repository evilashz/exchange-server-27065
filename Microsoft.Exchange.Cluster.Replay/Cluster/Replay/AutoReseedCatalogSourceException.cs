using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F5 RID: 1269
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoReseedCatalogSourceException : AutoReseedException
	{
		// Token: 0x06002EBD RID: 11965 RVA: 0x000C3F9D File Offset: 0x000C219D
		public AutoReseedCatalogSourceException(string databaseName, string serverName) : base(ReplayStrings.AutoReseedCatalogSourceException(databaseName, serverName))
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000C3FBF File Offset: 0x000C21BF
		public AutoReseedCatalogSourceException(string databaseName, string serverName, Exception innerException) : base(ReplayStrings.AutoReseedCatalogSourceException(databaseName, serverName), innerException)
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000C3FE4 File Offset: 0x000C21E4
		protected AutoReseedCatalogSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000C4039 File Offset: 0x000C2239
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000C4065 File Offset: 0x000C2265
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000C406D File Offset: 0x000C226D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x04001590 RID: 5520
		private readonly string databaseName;

		// Token: 0x04001591 RID: 5521
		private readonly string serverName;
	}
}
