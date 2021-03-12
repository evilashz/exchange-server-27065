using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F6 RID: 1014
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceResumeRpcPartialSuccessCatalogFailedException : TaskServerException
	{
		// Token: 0x06002943 RID: 10563 RVA: 0x000B96FC File Offset: 0x000B78FC
		public ReplayServiceResumeRpcPartialSuccessCatalogFailedException(string errMsg) : base(ReplayStrings.ReplayServiceResumeRpcPartialSuccessCatalogFailedException(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000B9716 File Offset: 0x000B7916
		public ReplayServiceResumeRpcPartialSuccessCatalogFailedException(string errMsg, Exception innerException) : base(ReplayStrings.ReplayServiceResumeRpcPartialSuccessCatalogFailedException(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000B9731 File Offset: 0x000B7931
		protected ReplayServiceResumeRpcPartialSuccessCatalogFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000B975B File Offset: 0x000B795B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002947 RID: 10567 RVA: 0x000B9776 File Offset: 0x000B7976
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001412 RID: 5138
		private readonly string errMsg;
	}
}
