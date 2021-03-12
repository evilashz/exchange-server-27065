using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x02000257 RID: 599
	public interface IProxyAsyncWaitCompletion
	{
		// Token: 0x06000B99 RID: 2969
		void CompleteAsyncCall([MarshalAs(UnmanagedType.U1)] bool notificationsPending, int errorCode);
	}
}
