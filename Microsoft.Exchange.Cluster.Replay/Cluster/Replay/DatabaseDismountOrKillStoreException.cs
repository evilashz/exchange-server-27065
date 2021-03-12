using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003C0 RID: 960
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DatabaseDismountOrKillStoreException : TransientException
	{
		// Token: 0x0600281B RID: 10267 RVA: 0x000B7331 File Offset: 0x000B5531
		public DatabaseDismountOrKillStoreException(string databaseName, string serverName, string errorText) : base(ReplayStrings.DatabaseDismountOrKillStoreException(databaseName, serverName, errorText))
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
			this.errorText = errorText;
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000B7356 File Offset: 0x000B5556
		public DatabaseDismountOrKillStoreException(string databaseName, string serverName, string errorText, Exception innerException) : base(ReplayStrings.DatabaseDismountOrKillStoreException(databaseName, serverName, errorText), innerException)
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
			this.errorText = errorText;
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000B7380 File Offset: 0x000B5580
		protected DatabaseDismountOrKillStoreException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.errorText = (string)info.GetValue("errorText", typeof(string));
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000B73F5 File Offset: 0x000B55F5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("serverName", this.serverName);
			info.AddValue("errorText", this.errorText);
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x000B7432 File Offset: 0x000B5632
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x000B743A File Offset: 0x000B563A
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06002821 RID: 10273 RVA: 0x000B7442 File Offset: 0x000B5642
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x040013C2 RID: 5058
		private readonly string databaseName;

		// Token: 0x040013C3 RID: 5059
		private readonly string serverName;

		// Token: 0x040013C4 RID: 5060
		private readonly string errorText;
	}
}
