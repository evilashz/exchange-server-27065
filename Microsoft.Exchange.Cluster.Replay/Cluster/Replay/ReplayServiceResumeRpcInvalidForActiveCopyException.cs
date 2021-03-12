using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200040C RID: 1036
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceResumeRpcInvalidForActiveCopyException : TaskServerException
	{
		// Token: 0x060029AB RID: 10667 RVA: 0x000BA0BB File Offset: 0x000B82BB
		public ReplayServiceResumeRpcInvalidForActiveCopyException(string dbCopy) : base(ReplayStrings.ReplayServiceResumeRpcInvalidForActiveCopyException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x000BA0D5 File Offset: 0x000B82D5
		public ReplayServiceResumeRpcInvalidForActiveCopyException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceResumeRpcInvalidForActiveCopyException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000BA0F0 File Offset: 0x000B82F0
		protected ReplayServiceResumeRpcInvalidForActiveCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000BA11A File Offset: 0x000B831A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000BA135 File Offset: 0x000B8335
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001422 RID: 5154
		private readonly string dbCopy;
	}
}
