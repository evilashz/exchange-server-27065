using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x02000281 RID: 641
	[Serializable]
	internal class RpcNoEndPointException : RpcConnectionException
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x0002AA8C File Offset: 0x00029E8C
		public RpcNoEndPointException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002AA70 File Offset: 0x00029E70
		public RpcNoEndPointException(string message) : base(message, 1753)
		{
		}
	}
}
