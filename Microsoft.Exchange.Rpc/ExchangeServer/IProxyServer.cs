using System;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Rpc.ExchangeServer
{
	// Token: 0x02000258 RID: 600
	public interface IProxyServer
	{
		// Token: 0x06000B9A RID: 2970
		int EcDoConnectEx(ClientSecurityContext callerSecurityContext, out IntPtr contextHandle, string userDn, uint flags, uint conMod, uint cpid, uint lcidString, uint lcidSort, out uint pollsMax, out uint retryCount, out uint retryDelay, out string displayName, short[] clientVersion, byte[] auxIn, out byte[] auxOut);

		// Token: 0x06000B9B RID: 2971
		int EcDoDisconnect(ref IntPtr contextHandle);

		// Token: 0x06000B9C RID: 2972
		int EcDoRpcExt2(ref IntPtr contextHandle, ref uint flags, ArraySegment<byte> request, uint maximumResponseSize, out ArraySegment<byte> response, ArraySegment<byte> auxIn, out ArraySegment<byte> auxOut);

		// Token: 0x06000B9D RID: 2973
		int EcDoAsyncConnectEx(IntPtr contextHandle, out IntPtr asynchronousContextHandle);

		// Token: 0x06000B9E RID: 2974
		int DoAsyncWaitEx(IntPtr asynchronousContextHandle, uint ulFlagsIn, IProxyAsyncWaitCompletion completionObject);

		// Token: 0x06000B9F RID: 2975
		ushort GetVersionDelta();
	}
}
