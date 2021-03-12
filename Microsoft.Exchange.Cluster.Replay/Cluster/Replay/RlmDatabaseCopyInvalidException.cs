using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004E9 RID: 1257
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RlmDatabaseCopyInvalidException : ReplayLagManagerException
	{
		// Token: 0x06002E7B RID: 11899 RVA: 0x000C37AE File Offset: 0x000C19AE
		public RlmDatabaseCopyInvalidException(string databaseName, string serverName) : base(ReplayStrings.RlmDatabaseCopyInvalidException(databaseName, serverName))
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000C37D0 File Offset: 0x000C19D0
		public RlmDatabaseCopyInvalidException(string databaseName, string serverName, Exception innerException) : base(ReplayStrings.RlmDatabaseCopyInvalidException(databaseName, serverName), innerException)
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000C37F4 File Offset: 0x000C19F4
		protected RlmDatabaseCopyInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000C3849 File Offset: 0x000C1A49
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000C3875 File Offset: 0x000C1A75
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000C387D File Offset: 0x000C1A7D
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400157E RID: 5502
		private readonly string databaseName;

		// Token: 0x0400157F RID: 5503
		private readonly string serverName;
	}
}
