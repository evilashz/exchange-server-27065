using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x02000280 RID: 640
	[Serializable]
	internal class RpcConnectionException : RpcException
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x0002AA58 File Offset: 0x00029E58
		public RpcConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002AA40 File Offset: 0x00029E40
		public RpcConnectionException(string message, int hr) : base(message, hr)
		{
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002AA2C File Offset: 0x00029E2C
		public RpcConnectionException(string message) : base(message)
		{
		}
	}
}
