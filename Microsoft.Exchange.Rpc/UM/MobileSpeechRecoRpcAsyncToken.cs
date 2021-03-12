using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x0200040B RID: 1035
	internal class MobileSpeechRecoRpcAsyncToken
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x000585D4 File Offset: 0x000579D4
		public unsafe MobileSpeechRecoRpcAsyncToken(_RPC_ASYNC_STATE* pAsyncState, int* pOutBytesLen, byte** ppOutBytes)
		{
			ExTraceGlobals.RpcTracer.TraceDebug((long)this.GetHashCode(), "Entering MobileSpeechRecoRpcAsyncToken constructor");
			IntPtr handle = new IntPtr((void*)pAsyncState);
			this.asyncStateHandle = new SafeRpcAsyncStateHandle(handle);
			this.pOutBytesLen = pOutBytesLen;
			this.ppOutBytes = ppOutBytes;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00058620 File Offset: 0x00057A20
		public unsafe void CompleteAsync(int returnVal, byte[] outBlob)
		{
			ExTraceGlobals.RpcTracer.TraceDebug((long)this.GetHashCode(), "Entering MobileSpeechRecoRpcAsyncToken.CompleteAsync");
			if (null != this.asyncStateHandle)
			{
				*(long*)this.ppOutBytes = <Module>.MToUBytesClient(outBlob, this.pOutBytesLen);
				this.asyncStateHandle.CompleteCall(returnVal);
				this.asyncStateHandle = null;
			}
		}

		// Token: 0x04001041 RID: 4161
		private SafeRpcAsyncStateHandle asyncStateHandle;

		// Token: 0x04001042 RID: 4162
		private unsafe int* pOutBytesLen;

		// Token: 0x04001043 RID: 4163
		private unsafe byte** ppOutBytes;
	}
}
