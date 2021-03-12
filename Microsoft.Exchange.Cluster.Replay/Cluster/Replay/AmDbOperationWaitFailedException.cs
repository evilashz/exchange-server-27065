using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200045D RID: 1117
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmDbOperationWaitFailedException : AmDbOperationException
	{
		// Token: 0x06002B67 RID: 11111 RVA: 0x000BD5C1 File Offset: 0x000BB7C1
		public AmDbOperationWaitFailedException() : base(ReplayStrings.AmDbOperationWaitFailedException)
		{
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000BD5D3 File Offset: 0x000BB7D3
		public AmDbOperationWaitFailedException(Exception innerException) : base(ReplayStrings.AmDbOperationWaitFailedException, innerException)
		{
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x000BD5E6 File Offset: 0x000BB7E6
		protected AmDbOperationWaitFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x000BD5F0 File Offset: 0x000BB7F0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
