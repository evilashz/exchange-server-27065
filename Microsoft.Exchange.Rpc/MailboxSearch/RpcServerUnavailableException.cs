using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x02000282 RID: 642
	[Serializable]
	internal class RpcServerUnavailableException : RpcConnectionException
	{
		// Token: 0x06000C0E RID: 3086 RVA: 0x0002AAC0 File Offset: 0x00029EC0
		public RpcServerUnavailableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0002AAA4 File Offset: 0x00029EA4
		public RpcServerUnavailableException(string message) : base(message, 1722)
		{
		}
	}
}
