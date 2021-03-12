using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003B3 RID: 947
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NetworkCancelledException : NetworkTransportException
	{
		// Token: 0x060027D5 RID: 10197 RVA: 0x000B6B11 File Offset: 0x000B4D11
		public NetworkCancelledException() : base(ReplayStrings.NetworkCancelled)
		{
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000B6B23 File Offset: 0x000B4D23
		public NetworkCancelledException(Exception innerException) : base(ReplayStrings.NetworkCancelled, innerException)
		{
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000B6B36 File Offset: 0x000B4D36
		protected NetworkCancelledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000B6B40 File Offset: 0x000B4D40
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
