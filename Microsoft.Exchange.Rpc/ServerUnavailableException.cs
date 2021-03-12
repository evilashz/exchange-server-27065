using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal class ServerUnavailableException : RpcException
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x000010E0 File Offset: 0x000004E0
		public ServerUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000010C0 File Offset: 0x000004C0
		public ServerUnavailableException(string message) : base(message)
		{
			base.HResult = 1722;
		}
	}
}
