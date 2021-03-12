using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F3 RID: 1011
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceRpcCopyStatusTimeoutException : TaskServerException
	{
		// Token: 0x06002934 RID: 10548 RVA: 0x000B956B File Offset: 0x000B776B
		public ReplayServiceRpcCopyStatusTimeoutException(string dbCopyName, int timeoutSecs) : base(ReplayStrings.ReplayServiceRpcCopyStatusTimeoutException(dbCopyName, timeoutSecs))
		{
			this.dbCopyName = dbCopyName;
			this.timeoutSecs = timeoutSecs;
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000B958D File Offset: 0x000B778D
		public ReplayServiceRpcCopyStatusTimeoutException(string dbCopyName, int timeoutSecs, Exception innerException) : base(ReplayStrings.ReplayServiceRpcCopyStatusTimeoutException(dbCopyName, timeoutSecs), innerException)
		{
			this.dbCopyName = dbCopyName;
			this.timeoutSecs = timeoutSecs;
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000B95B0 File Offset: 0x000B77B0
		protected ReplayServiceRpcCopyStatusTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopyName = (string)info.GetValue("dbCopyName", typeof(string));
			this.timeoutSecs = (int)info.GetValue("timeoutSecs", typeof(int));
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000B9605 File Offset: 0x000B7805
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopyName", this.dbCopyName);
			info.AddValue("timeoutSecs", this.timeoutSecs);
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x000B9631 File Offset: 0x000B7831
		public string DbCopyName
		{
			get
			{
				return this.dbCopyName;
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x000B9639 File Offset: 0x000B7839
		public int TimeoutSecs
		{
			get
			{
				return this.timeoutSecs;
			}
		}

		// Token: 0x0400140F RID: 5135
		private readonly string dbCopyName;

		// Token: 0x04001410 RID: 5136
		private readonly int timeoutSecs;
	}
}
