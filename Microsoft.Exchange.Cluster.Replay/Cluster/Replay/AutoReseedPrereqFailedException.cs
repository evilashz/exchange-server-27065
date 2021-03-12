using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F1 RID: 1265
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoReseedPrereqFailedException : AutoReseedException
	{
		// Token: 0x06002EA3 RID: 11939 RVA: 0x000C3BAC File Offset: 0x000C1DAC
		public AutoReseedPrereqFailedException(string databaseName, string serverName, string error) : base(ReplayStrings.AutoReseedPrereqFailedException(databaseName, serverName, error))
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
			this.error = error;
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000C3BD6 File Offset: 0x000C1DD6
		public AutoReseedPrereqFailedException(string databaseName, string serverName, string error, Exception innerException) : base(ReplayStrings.AutoReseedPrereqFailedException(databaseName, serverName, error), innerException)
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
			this.error = error;
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000C3C04 File Offset: 0x000C1E04
		protected AutoReseedPrereqFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000C3C79 File Offset: 0x000C1E79
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("serverName", this.serverName);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000C3CB6 File Offset: 0x000C1EB6
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002EA8 RID: 11944 RVA: 0x000C3CBE File Offset: 0x000C1EBE
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000C3CC6 File Offset: 0x000C1EC6
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001586 RID: 5510
		private readonly string databaseName;

		// Token: 0x04001587 RID: 5511
		private readonly string serverName;

		// Token: 0x04001588 RID: 5512
		private readonly string error;
	}
}
