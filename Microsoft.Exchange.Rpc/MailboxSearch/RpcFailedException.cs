using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x02000284 RID: 644
	[Serializable]
	internal class RpcFailedException : RpcException
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x0002AB28 File Offset: 0x00029F28
		public RpcFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002AB0C File Offset: 0x00029F0C
		public RpcFailedException(string message) : base(message, 1727)
		{
		}
	}
}
