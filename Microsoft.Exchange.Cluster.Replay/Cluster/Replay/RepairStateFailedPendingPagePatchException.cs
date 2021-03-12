using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004CF RID: 1231
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateFailedPendingPagePatchException : RepairStateException
	{
		// Token: 0x06002DEA RID: 11754 RVA: 0x000C260D File Offset: 0x000C080D
		public RepairStateFailedPendingPagePatchException(string dbName, string errorMsg) : base(ReplayStrings.RepairStateFailedPendingPagePatchException(dbName, errorMsg))
		{
			this.dbName = dbName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000C262F File Offset: 0x000C082F
		public RepairStateFailedPendingPagePatchException(string dbName, string errorMsg, Exception innerException) : base(ReplayStrings.RepairStateFailedPendingPagePatchException(dbName, errorMsg), innerException)
		{
			this.dbName = dbName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000C2654 File Offset: 0x000C0854
		protected RepairStateFailedPendingPagePatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000C26A9 File Offset: 0x000C08A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002DEE RID: 11758 RVA: 0x000C26D5 File Offset: 0x000C08D5
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002DEF RID: 11759 RVA: 0x000C26DD File Offset: 0x000C08DD
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001555 RID: 5461
		private readonly string dbName;

		// Token: 0x04001556 RID: 5462
		private readonly string errorMsg;
	}
}
