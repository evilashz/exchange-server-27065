using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000480 RID: 1152
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmFailedToDetermineDatabaseMountStatusException : AmDbActionException
	{
		// Token: 0x06002C23 RID: 11299 RVA: 0x000BEBF1 File Offset: 0x000BCDF1
		public AmFailedToDetermineDatabaseMountStatusException(string serverName, string dbName) : base(ReplayStrings.AmFailedToDetermineDatabaseMountStatus(serverName, dbName))
		{
			this.serverName = serverName;
			this.dbName = dbName;
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000BEC13 File Offset: 0x000BCE13
		public AmFailedToDetermineDatabaseMountStatusException(string serverName, string dbName, Exception innerException) : base(ReplayStrings.AmFailedToDetermineDatabaseMountStatus(serverName, dbName), innerException)
		{
			this.serverName = serverName;
			this.dbName = dbName;
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000BEC38 File Offset: 0x000BCE38
		protected AmFailedToDetermineDatabaseMountStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serverName = (string)info.GetValue("serverName", typeof(string));
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000BEC8D File Offset: 0x000BCE8D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serverName", this.serverName);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002C27 RID: 11303 RVA: 0x000BECB9 File Offset: 0x000BCEB9
		public string ServerName
		{
			get
			{
				return this.serverName;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x000BECC1 File Offset: 0x000BCEC1
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x040014CA RID: 5322
		private readonly string serverName;

		// Token: 0x040014CB RID: 5323
		private readonly string dbName;
	}
}
