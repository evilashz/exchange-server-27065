using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004C9 RID: 1225
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateDatabaseIsActiveException : RepairStateException
	{
		// Token: 0x06002DCB RID: 11723 RVA: 0x000C229F File Offset: 0x000C049F
		public RepairStateDatabaseIsActiveException(string dbName) : base(ReplayStrings.RepairStateDatabaseIsActive(dbName))
		{
			this.dbName = dbName;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000C22B9 File Offset: 0x000C04B9
		public RepairStateDatabaseIsActiveException(string dbName, Exception innerException) : base(ReplayStrings.RepairStateDatabaseIsActive(dbName), innerException)
		{
			this.dbName = dbName;
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000C22D4 File Offset: 0x000C04D4
		protected RepairStateDatabaseIsActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000C22FE File Offset: 0x000C04FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002DCF RID: 11727 RVA: 0x000C2319 File Offset: 0x000C0519
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x0400154E RID: 5454
		private readonly string dbName;
	}
}
