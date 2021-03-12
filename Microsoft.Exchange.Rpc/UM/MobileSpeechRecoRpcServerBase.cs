using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x0200040C RID: 1036
	internal abstract class MobileSpeechRecoRpcServerBase : RpcServerBase
	{
		// Token: 0x060011A6 RID: 4518 RVA: 0x00058674 File Offset: 0x00057A74
		public void CompleteAsync(int returnVal, byte[] outBlob, object token)
		{
			ExTraceGlobals.RpcTracer.TraceDebug((long)this.GetHashCode(), "Entering MobileSpeechRecoRpcServerBase.CompleteAsync");
			MobileSpeechRecoRpcAsyncToken mobileSpeechRecoRpcAsyncToken = token as MobileSpeechRecoRpcAsyncToken;
			if (null == mobileSpeechRecoRpcAsyncToken)
			{
				throw new ArgumentException("Invalid type", "token");
			}
			mobileSpeechRecoRpcAsyncToken.CompleteAsync(returnVal, outBlob);
		}

		// Token: 0x060011A7 RID: 4519
		public abstract void ExecuteStepAsync(byte[] inBlob, object token);

		// Token: 0x060011A8 RID: 4520 RVA: 0x00058970 File Offset: 0x00057D70
		public MobileSpeechRecoRpcServerBase()
		{
		}

		// Token: 0x04001044 RID: 4164
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IMobileSpeechReco_v1_0_s_ifspec;
	}
}
