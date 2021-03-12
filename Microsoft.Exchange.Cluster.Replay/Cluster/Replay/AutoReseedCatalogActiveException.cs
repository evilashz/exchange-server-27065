using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F4 RID: 1268
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoReseedCatalogActiveException : AutoReseedException
	{
		// Token: 0x06002EB7 RID: 11959 RVA: 0x000C3EC5 File Offset: 0x000C20C5
		public AutoReseedCatalogActiveException(string databaseName, string serverName) : base(ReplayStrings.AutoReseedCatalogActiveException(databaseName, serverName))
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000C3EE7 File Offset: 0x000C20E7
		public AutoReseedCatalogActiveException(string databaseName, string serverName, Exception innerException) : base(ReplayStrings.AutoReseedCatalogActiveException(databaseName, serverName), innerException)
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000C3F0C File Offset: 0x000C210C
		protected AutoReseedCatalogActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000C3F61 File Offset: 0x000C2161
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x000C3F8D File Offset: 0x000C218D
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000C3F95 File Offset: 0x000C2195
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400158E RID: 5518
		private readonly string databaseName;

		// Token: 0x0400158F RID: 5519
		private readonly string serverName;
	}
}
