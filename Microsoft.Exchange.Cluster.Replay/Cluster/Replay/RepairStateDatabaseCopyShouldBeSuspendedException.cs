using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004CD RID: 1229
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateDatabaseCopyShouldBeSuspendedException : RepairStateException
	{
		// Token: 0x06002DDF RID: 11743 RVA: 0x000C24B5 File Offset: 0x000C06B5
		public RepairStateDatabaseCopyShouldBeSuspendedException(string dbName) : base(ReplayStrings.RepairStateDatabaseCopyShouldBeSuspended(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000C24CF File Offset: 0x000C06CF
		public RepairStateDatabaseCopyShouldBeSuspendedException(string dbName, Exception innerException) : base(ReplayStrings.RepairStateDatabaseCopyShouldBeSuspended(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000C24EA File Offset: 0x000C06EA
		protected RepairStateDatabaseCopyShouldBeSuspendedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000C2514 File Offset: 0x000C0714
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x000C252F File Offset: 0x000C072F
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x04001552 RID: 5458
		private readonly string dbName;
	}
}
