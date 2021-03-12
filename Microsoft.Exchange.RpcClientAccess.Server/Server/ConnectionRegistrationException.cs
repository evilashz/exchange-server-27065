using System;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	internal sealed class ConnectionRegistrationException : Exception
	{
		// Token: 0x06000058 RID: 88 RVA: 0x0000366A File Offset: 0x0000186A
		public ConnectionRegistrationException(string message) : base(message)
		{
		}
	}
}
