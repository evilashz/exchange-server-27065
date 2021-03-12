using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004F2 RID: 1266
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AutoReseedThrottledException : AutoReseedException
	{
		// Token: 0x06002EAA RID: 11946 RVA: 0x000C3CCE File Offset: 0x000C1ECE
		public AutoReseedThrottledException(string databaseName, string serverName, string throttlingInterval) : base(ReplayStrings.AutoReseedThrottledException(databaseName, serverName, throttlingInterval))
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
			this.throttlingInterval = throttlingInterval;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000C3CF8 File Offset: 0x000C1EF8
		public AutoReseedThrottledException(string databaseName, string serverName, string throttlingInterval, Exception innerException) : base(ReplayStrings.AutoReseedThrottledException(databaseName, serverName, throttlingInterval), innerException)
		{
			this.databaseName = databaseName;
			this.serverName = serverName;
			this.throttlingInterval = throttlingInterval;
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000C3D24 File Offset: 0x000C1F24
		protected AutoReseedThrottledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.throttlingInterval = (string)info.GetValue("throttlingInterval", typeof(string));
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000C3D99 File Offset: 0x000C1F99
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("serverName", this.serverName);
			info.AddValue("throttlingInterval", this.throttlingInterval);
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002EAE RID: 11950 RVA: 0x000C3DD6 File Offset: 0x000C1FD6
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000C3DDE File Offset: 0x000C1FDE
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x000C3DE6 File Offset: 0x000C1FE6
		public string ThrottlingInterval
		{
			get
			{
				return this.throttlingInterval;
			}
		}

		// Token: 0x04001589 RID: 5513
		private readonly string databaseName;

		// Token: 0x0400158A RID: 5514
		private readonly string serverName;

		// Token: 0x0400158B RID: 5515
		private readonly string throttlingInterval;
	}
}
