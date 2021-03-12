using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x020009A6 RID: 2470
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EnqueueRequestRpcOutParameters : RpcParameters
	{
		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x06005B35 RID: 23349 RVA: 0x0017D3A7 File Offset: 0x0017B5A7
		// (set) Token: 0x06005B36 RID: 23350 RVA: 0x0017D3AF File Offset: 0x0017B5AF
		public EnqueueResult Result { get; private set; }

		// Token: 0x06005B37 RID: 23351 RVA: 0x0017D3B8 File Offset: 0x0017B5B8
		public EnqueueRequestRpcOutParameters(byte[] data) : base(data)
		{
			this.Result = (EnqueueResult)base.GetParameterValue("EnqueueResult");
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x0017D3D7 File Offset: 0x0017B5D7
		public EnqueueRequestRpcOutParameters(EnqueueResult enqueueResult)
		{
			this.Result = enqueueResult;
			base.SetParameterValue("EnqueueResult", this.Result);
		}

		// Token: 0x04003257 RID: 12887
		private const string EnqueueResultParameterName = "EnqueueResult";
	}
}
