using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200044B RID: 1099
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SeederInstanceReseedBlockedException : SeederServerException
	{
		// Token: 0x06002B0B RID: 11019 RVA: 0x000BCBF7 File Offset: 0x000BADF7
		public SeederInstanceReseedBlockedException(string dbCopyName, string errorMsg) : base(ReplayStrings.SeederInstanceReseedBlockedException(dbCopyName, errorMsg))
		{
			this.dbCopyName = dbCopyName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000BCC19 File Offset: 0x000BAE19
		public SeederInstanceReseedBlockedException(string dbCopyName, string errorMsg, Exception innerException) : base(ReplayStrings.SeederInstanceReseedBlockedException(dbCopyName, errorMsg), innerException)
		{
			this.dbCopyName = dbCopyName;
			this.errorMsg = errorMsg;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000BCC3C File Offset: 0x000BAE3C
		protected SeederInstanceReseedBlockedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopyName = (string)info.GetValue("dbCopyName", typeof(string));
			this.errorMsg = (string)info.GetValue("errorMsg", typeof(string));
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000BCC91 File Offset: 0x000BAE91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopyName", this.dbCopyName);
			info.AddValue("errorMsg", this.errorMsg);
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000BCCBD File Offset: 0x000BAEBD
		public string DbCopyName
		{
			get
			{
				return this.dbCopyName;
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000BCCC5 File Offset: 0x000BAEC5
		public string ErrorMsg
		{
			get
			{
				return this.errorMsg;
			}
		}

		// Token: 0x04001486 RID: 5254
		private readonly string dbCopyName;

		// Token: 0x04001487 RID: 5255
		private readonly string errorMsg;
	}
}
