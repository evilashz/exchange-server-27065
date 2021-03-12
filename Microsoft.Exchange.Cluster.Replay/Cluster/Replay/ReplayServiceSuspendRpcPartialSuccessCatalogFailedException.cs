using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F7 RID: 1015
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendRpcPartialSuccessCatalogFailedException : TaskServerException
	{
		// Token: 0x06002948 RID: 10568 RVA: 0x000B977E File Offset: 0x000B797E
		public ReplayServiceSuspendRpcPartialSuccessCatalogFailedException(string errMsg) : base(ReplayStrings.ReplayServiceSuspendRpcPartialSuccessCatalogFailedException(errMsg))
		{
			this.errMsg = errMsg;
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000B9798 File Offset: 0x000B7998
		public ReplayServiceSuspendRpcPartialSuccessCatalogFailedException(string errMsg, Exception innerException) : base(ReplayStrings.ReplayServiceSuspendRpcPartialSuccessCatalogFailedException(errMsg), innerException)
		{
			this.errMsg = errMsg;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000B97B3 File Offset: 0x000B79B3
		protected ReplayServiceSuspendRpcPartialSuccessCatalogFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000B97DD File Offset: 0x000B79DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000B97F8 File Offset: 0x000B79F8
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x04001413 RID: 5139
		private readonly string errMsg;
	}
}
