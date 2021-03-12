using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F3 RID: 1267
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoReseedUnhandledException : AutoReseedException
	{
		// Token: 0x06002EB1 RID: 11953 RVA: 0x000C3DEE File Offset: 0x000C1FEE
		public AutoReseedUnhandledException(string databaseName, string serverName) : base(ReplayStrings.AutoReseedUnhandledException(databaseName, serverName))
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000C3E10 File Offset: 0x000C2010
		public AutoReseedUnhandledException(string databaseName, string serverName, Exception innerException) : base(ReplayStrings.AutoReseedUnhandledException(databaseName, serverName), innerException)
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000C3E34 File Offset: 0x000C2034
		protected AutoReseedUnhandledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000C3E89 File Offset: 0x000C2089
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("serverName", this.serverName);
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000C3EB5 File Offset: 0x000C20B5
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000C3EBD File Offset: 0x000C20BD
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x0400158C RID: 5516
		private readonly string databaseName;

		// Token: 0x0400158D RID: 5517
		private readonly string serverName;
	}
}
