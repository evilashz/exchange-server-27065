using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200040B RID: 1035
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendRpcInvalidForActiveCopyException : TaskServerException
	{
		// Token: 0x060029A6 RID: 10662 RVA: 0x000BA039 File Offset: 0x000B8239
		public ReplayServiceSuspendRpcInvalidForActiveCopyException(string dbCopy) : base(ReplayStrings.ReplayServiceSuspendRpcInvalidForActiveCopyException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000BA053 File Offset: 0x000B8253
		public ReplayServiceSuspendRpcInvalidForActiveCopyException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceSuspendRpcInvalidForActiveCopyException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x000BA06E File Offset: 0x000B826E
		protected ReplayServiceSuspendRpcInvalidForActiveCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000BA098 File Offset: 0x000B8298
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x000BA0B3 File Offset: 0x000B82B3
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001421 RID: 5153
		private readonly string dbCopy;
	}
}
