using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200047E RID: 1150
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmMountTimeoutException : AmDbActionException
	{
		// Token: 0x06002C16 RID: 11286 RVA: 0x000BE9F9 File Offset: 0x000BCBF9
		public AmMountTimeoutException(string dbName, string serverName, int timeoutInSecs) : base(ReplayStrings.AmMountTimeoutError(dbName, serverName, timeoutInSecs))
		{
			this.dbName = dbName;
			this.serverName = serverName;
			this.timeoutInSecs = timeoutInSecs;
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000BEA23 File Offset: 0x000BCC23
		public AmMountTimeoutException(string dbName, string serverName, int timeoutInSecs, Exception innerException) : base(ReplayStrings.AmMountTimeoutError(dbName, serverName, timeoutInSecs), innerException)
		{
			this.dbName = dbName;
			this.serverName = serverName;
			this.timeoutInSecs = timeoutInSecs;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000BEA50 File Offset: 0x000BCC50
		protected AmMountTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.timeoutInSecs = (int)info.GetValue("timeoutInSecs", typeof(int));
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000BEAC5 File Offset: 0x000BCCC5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("serverName", this.serverName);
			info.AddValue("timeoutInSecs", this.timeoutInSecs);
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000BEB02 File Offset: 0x000BCD02
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000BEB0A File Offset: 0x000BCD0A
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002C1C RID: 11292 RVA: 0x000BEB12 File Offset: 0x000BCD12
		public int TimeoutInSecs
		{
			get
			{
				return this.timeoutInSecs;
			}
		}

		// Token: 0x040014C5 RID: 5317
		private readonly string dbName;

		// Token: 0x040014C6 RID: 5318
		private readonly string serverName;

		// Token: 0x040014C7 RID: 5319
		private readonly int timeoutInSecs;
	}
}
