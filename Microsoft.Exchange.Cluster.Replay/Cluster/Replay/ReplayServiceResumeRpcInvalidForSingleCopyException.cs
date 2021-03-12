using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000405 RID: 1029
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceResumeRpcInvalidForSingleCopyException : TaskServerException
	{
		// Token: 0x06002983 RID: 10627 RVA: 0x000B9B77 File Offset: 0x000B7D77
		public ReplayServiceResumeRpcInvalidForSingleCopyException(string dbCopy) : base(ReplayStrings.ReplayServiceResumeRpcInvalidForSingleCopyException(dbCopy))
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x000B9B91 File Offset: 0x000B7D91
		public ReplayServiceResumeRpcInvalidForSingleCopyException(string dbCopy, Exception innerException) : base(ReplayStrings.ReplayServiceResumeRpcInvalidForSingleCopyException(dbCopy), innerException)
		{
			this.dbCopy = dbCopy;
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000B9BAC File Offset: 0x000B7DAC
		protected ReplayServiceResumeRpcInvalidForSingleCopyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbCopy = (string)info.GetValue("dbCopy", typeof(string));
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000B9BD6 File Offset: 0x000B7DD6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbCopy", this.dbCopy);
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x000B9BF1 File Offset: 0x000B7DF1
		public string DbCopy
		{
			get
			{
				return this.dbCopy;
			}
		}

		// Token: 0x04001416 RID: 5142
		private readonly string dbCopy;
	}
}
