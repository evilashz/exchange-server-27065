using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x02000285 RID: 645
	[Serializable]
	internal class RpcServerTooBusyException : RpcException
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x0002AB5C File Offset: 0x00029F5C
		public RpcServerTooBusyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0002AB40 File Offset: 0x00029F40
		public RpcServerTooBusyException(string message) : base(message, 1723)
		{
		}
	}
}
