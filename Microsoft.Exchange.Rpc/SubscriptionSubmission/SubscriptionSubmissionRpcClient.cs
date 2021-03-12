using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.SubscriptionSubmission
{
	// Token: 0x020003F7 RID: 1015
	internal class SubscriptionSubmissionRpcClient : RpcClientBase
	{
		// Token: 0x06001161 RID: 4449 RVA: 0x00057178 File Offset: 0x00056578
		public SubscriptionSubmissionRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
		{
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00057164 File Offset: 0x00056564
		public SubscriptionSubmissionRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00057190 File Offset: 0x00056590
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] SubscriptionSubmit(int version, byte[] inBlob)
		{
			byte[] result = null;
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			try
			{
				int num = 0;
				ptr = <Module>.MToUBytesClient(inBlob, &num);
				bool flag = true;
				RpcRetryState rpcRetryState = 0;
				*(ref rpcRetryState + 4) = 0;
				do
				{
					try
					{
						<Module>.cli_SubscriptionSubmit(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (<Module>.RpcRetryState.Retry(ref rpcRetryState, exceptionCode) == null)
						{
							<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "SubmitSubscription");
						}
					}
				}
				while (flag);
				result = <Module>.UToMBytes(cBytes, ptr2);
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
				if (ptr2 != null)
				{
					<Module>.MIDL_user_free((void*)ptr2);
				}
			}
			return result;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00057140 File Offset: 0x00056540
		// Note: this type is marked as 'beforefieldinit'.
		static SubscriptionSubmissionRpcClient()
		{
			SubscriptionSubmissionRpcClient.RpcServerTooBusy = 1723;
		}

		// Token: 0x04001027 RID: 4135
		public static int RpcServerTooBusy = 1723;

		// Token: 0x04001028 RID: 4136
		public static int RpcCallFailedDidNotExecute = 1727;
	}
}
