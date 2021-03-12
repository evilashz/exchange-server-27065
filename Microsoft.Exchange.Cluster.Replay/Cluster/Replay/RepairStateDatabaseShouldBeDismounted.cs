using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004CC RID: 1228
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateDatabaseShouldBeDismounted : RepairStateException
	{
		// Token: 0x06002DD9 RID: 11737 RVA: 0x000C23DC File Offset: 0x000C05DC
		public RepairStateDatabaseShouldBeDismounted(string dbName, string mountedServer) : base(ReplayStrings.RepairStateDatabaseShouldBeDismounted(dbName, mountedServer))
		{
			this.dbName = dbName;
			this.mountedServer = mountedServer;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000C23FE File Offset: 0x000C05FE
		public RepairStateDatabaseShouldBeDismounted(string dbName, string mountedServer, Exception innerException) : base(ReplayStrings.RepairStateDatabaseShouldBeDismounted(dbName, mountedServer), innerException)
		{
			this.dbName = dbName;
			this.mountedServer = mountedServer;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000C2424 File Offset: 0x000C0624
		protected RepairStateDatabaseShouldBeDismounted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.mountedServer = (string)info.GetValue("mountedServer", typeof(string));
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000C2479 File Offset: 0x000C0679
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("mountedServer", this.mountedServer);
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x000C24A5 File Offset: 0x000C06A5
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x000C24AD File Offset: 0x000C06AD
		public string MountedServer
		{
			get
			{
				return this.mountedServer;
			}
		}

		// Token: 0x04001550 RID: 5456
		private readonly string dbName;

		// Token: 0x04001551 RID: 5457
		private readonly string mountedServer;
	}
}
