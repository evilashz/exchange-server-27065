using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003FA RID: 1018
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceSuspendRpcFailedException : TaskServerException
	{
		// Token: 0x06002956 RID: 10582 RVA: 0x000B98BB File Offset: 0x000B7ABB
		public ReplayServiceSuspendRpcFailedException() : base(ReplayStrings.ReplayServiceSuspendRpcFailedException)
		{
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x000B98CD File Offset: 0x000B7ACD
		public ReplayServiceSuspendRpcFailedException(Exception innerException) : base(ReplayStrings.ReplayServiceSuspendRpcFailedException, innerException)
		{
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000B98E0 File Offset: 0x000B7AE0
		protected ReplayServiceSuspendRpcFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000B98EA File Offset: 0x000B7AEA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
