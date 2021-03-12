using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003F2 RID: 1010
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ReplayServiceRpcUnknownInstanceException : TaskServerException
	{
		// Token: 0x06002930 RID: 10544 RVA: 0x000B9532 File Offset: 0x000B7732
		public ReplayServiceRpcUnknownInstanceException() : base(ReplayStrings.ReplayServiceRpcUnknownInstanceException)
		{
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000B9544 File Offset: 0x000B7744
		public ReplayServiceRpcUnknownInstanceException(Exception innerException) : base(ReplayStrings.ReplayServiceRpcUnknownInstanceException, innerException)
		{
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x000B9557 File Offset: 0x000B7757
		protected ReplayServiceRpcUnknownInstanceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x000B9561 File Offset: 0x000B7761
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
