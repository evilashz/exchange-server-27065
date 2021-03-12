using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000004 RID: 4
	[Serializable]
	internal class DuplicateRpcEndpointException : RpcException
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x000010A8 File Offset: 0x000004A8
		public DuplicateRpcEndpointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00001088 File Offset: 0x00000488
		public DuplicateRpcEndpointException(string message) : base(message)
		{
			base.HResult = 1740;
		}
	}
}
