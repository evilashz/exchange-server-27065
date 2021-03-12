using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200004A RID: 74
	internal class RpcServiceAbortException : Exception
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00009168 File Offset: 0x00007368
		public RpcServiceAbortException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
