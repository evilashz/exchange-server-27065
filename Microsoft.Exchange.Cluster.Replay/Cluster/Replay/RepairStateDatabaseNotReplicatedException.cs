using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C8 RID: 1224
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateDatabaseNotReplicatedException : RepairStateException
	{
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000C221D File Offset: 0x000C041D
		public RepairStateDatabaseNotReplicatedException(string dbName) : base(ReplayStrings.RepairStateDatabaseNotReplicated(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000C2237 File Offset: 0x000C0437
		public RepairStateDatabaseNotReplicatedException(string dbName, Exception innerException) : base(ReplayStrings.RepairStateDatabaseNotReplicated(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000C2252 File Offset: 0x000C0452
		protected RepairStateDatabaseNotReplicatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000C227C File Offset: 0x000C047C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x000C2297 File Offset: 0x000C0497
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x0400154D RID: 5453
		private readonly string dbName;
	}
}
