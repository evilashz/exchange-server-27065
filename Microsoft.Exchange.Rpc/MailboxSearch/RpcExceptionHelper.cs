using System;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x02000286 RID: 646
	internal class RpcExceptionHelper
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x0002AB74 File Offset: 0x00029F74
		public static void ThrowRpcException(int status, string message)
		{
			if (status == 1717)
			{
				throw new RpcUnknownInterfaceException(message);
			}
			if (status == 1722)
			{
				throw new RpcServerUnavailableException(message);
			}
			if (status == 1723)
			{
				throw new RpcServerTooBusyException(message);
			}
			if (status == 1727)
			{
				throw new RpcFailedException(message);
			}
			if (status != 1753)
			{
				throw new RpcException(message, status);
			}
			throw new RpcNoEndPointException(message);
		}
	}
}
