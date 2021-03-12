using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x02000283 RID: 643
	[Serializable]
	internal class RpcUnknownInterfaceException : RpcConnectionException
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x0002AAF4 File Offset: 0x00029EF4
		public RpcUnknownInterfaceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002AAD8 File Offset: 0x00029ED8
		public RpcUnknownInterfaceException(string message) : base(message, 1717)
		{
		}
	}
}
