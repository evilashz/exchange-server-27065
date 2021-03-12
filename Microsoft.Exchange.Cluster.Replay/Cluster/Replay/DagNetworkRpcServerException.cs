using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003BC RID: 956
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagNetworkRpcServerException : TaskServerException
	{
		// Token: 0x06002804 RID: 10244 RVA: 0x000B704A File Offset: 0x000B524A
		public DagNetworkRpcServerException(string rpcName, string errMsg) : base(ReplayStrings.DagNetworkRpcServerError(rpcName, errMsg))
		{
			this.rpcName = rpcName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000B706C File Offset: 0x000B526C
		public DagNetworkRpcServerException(string rpcName, string errMsg, Exception innerException) : base(ReplayStrings.DagNetworkRpcServerError(rpcName, errMsg), innerException)
		{
			this.rpcName = rpcName;
			this.errMsg = errMsg;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000B7090 File Offset: 0x000B5290
		protected DagNetworkRpcServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.rpcName = (string)info.GetValue("rpcName", typeof(string));
			this.errMsg = (string)info.GetValue("errMsg", typeof(string));
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000B70E5 File Offset: 0x000B52E5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("rpcName", this.rpcName);
			info.AddValue("errMsg", this.errMsg);
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x000B7111 File Offset: 0x000B5311
		public string RpcName
		{
			get
			{
				return this.rpcName;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x000B7119 File Offset: 0x000B5319
		public string ErrMsg
		{
			get
			{
				return this.errMsg;
			}
		}

		// Token: 0x040013BB RID: 5051
		private readonly string rpcName;

		// Token: 0x040013BC RID: 5052
		private readonly string errMsg;
	}
}
