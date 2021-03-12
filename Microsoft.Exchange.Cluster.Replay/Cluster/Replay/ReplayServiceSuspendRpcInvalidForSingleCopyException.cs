using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000404 RID: 1028
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendRpcInvalidForSingleCopyException : TaskServerException
	{
		// Token: 0x0600297E RID: 10622 RVA: 0x000B9AF5 File Offset: 0x000B7CF5
		public ReplayServiceSuspendRpcInvalidForSingleCopyException(string dbCopy) : base(ReplayStrings.ReplayServiceSuspendRpcInvalidForSingleCopyException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000B9B0F File Offset: 0x000B7D0F
		public ReplayServiceSuspendRpcInvalidForSingleCopyException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceSuspendRpcInvalidForSingleCopyException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000B9B2A File Offset: 0x000B7D2A
		protected ReplayServiceSuspendRpcInvalidForSingleCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x000B9B54 File Offset: 0x000B7D54
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000B9B6F File Offset: 0x000B7D6F
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001415 RID: 5141
		private readonly string dbCopy;
	}
}
