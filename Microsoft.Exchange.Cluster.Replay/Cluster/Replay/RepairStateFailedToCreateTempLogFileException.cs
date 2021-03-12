using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004CE RID: 1230
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RepairStateFailedToCreateTempLogFileException : RepairStateException
	{
		// Token: 0x06002DE4 RID: 11748 RVA: 0x000C2537 File Offset: 0x000C0737
		public RepairStateFailedToCreateTempLogFileException(string dbName, string errorMsg) : base(ReplayStrings.RepairStateFailedToCreateTempLogFile(dbName, errorMsg))
		{
			this.dbName = dbName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000C2559 File Offset: 0x000C0759
		public RepairStateFailedToCreateTempLogFileException(string dbName, string errorMsg, Exception innerException) : base(ReplayStrings.RepairStateFailedToCreateTempLogFile(dbName, errorMsg), innerException)
		{
			this.dbName = dbName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x000C257C File Offset: 0x000C077C
		protected RepairStateFailedToCreateTempLogFileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbName = (string)info.GetValue("dbName", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x000C25D1 File Offset: 0x000C07D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbName", this.dbName);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x000C25FD File Offset: 0x000C07FD
		public string DbName
		{
			get
			{
				return this.dbName;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002DE9 RID: 11753 RVA: 0x000C2605 File Offset: 0x000C0805
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001553 RID: 5459
		private readonly string dbName;

		// Token: 0x04001554 RID: 5460
		private readonly string errorMsg;
	}
}
