using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200047F RID: 1151
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDismountSucceededButStillMountedException : AmDbActionException
	{
		// Token: 0x06002C1D RID: 11293 RVA: 0x000BEB1A File Offset: 0x000BCD1A
		public AmDismountSucceededButStillMountedException(string serverName, string dbName) : base(ReplayStrings.AmDismountSucceededButStillMounted(serverName, dbName))
		{
			this.serverName = serverName;
			this.dbName = dbName;
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000BEB3C File Offset: 0x000BCD3C
		public AmDismountSucceededButStillMountedException(string serverName, string dbName, Exception innerException) : base(ReplayStrings.AmDismountSucceededButStillMounted(serverName, dbName), innerException)
		{
			this.serverName = serverName;
			this.dbName = dbName;
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000BEB60 File Offset: 0x000BCD60
		protected AmDismountSucceededButStillMountedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000BEBB5 File Offset: 0x000BCDB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x000BEBE1 File Offset: 0x000BCDE1
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000BEBE9 File Offset: 0x000BCDE9
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014C8 RID: 5320
		private readonly string serverName;

		// Token: 0x040014C9 RID: 5321
		private readonly string dbName;
	}
}
