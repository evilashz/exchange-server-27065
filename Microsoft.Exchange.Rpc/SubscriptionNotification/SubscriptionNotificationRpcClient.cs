using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.SubscriptionNotification
{
	// Token: 0x020003F5 RID: 1013
	internal class SubscriptionNotificationRpcClient : RpcClientBase
	{
		// Token: 0x06001159 RID: 4441 RVA: 0x00056FBC File Offset: 0x000563BC
		public SubscriptionNotificationRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
		{
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00056FA8 File Offset: 0x000563A8
		public SubscriptionNotificationRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00056FD4 File Offset: 0x000563D4
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual byte[] InvokeSubscriptionNotificationEndPoint(int version, byte[] inBlob)
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
						<Module>.cli_InvokeSubscriptionNotificationEndPoint(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (<Module>.RpcRetryState.Retry(ref rpcRetryState, exceptionCode) == null)
						{
							<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "SubscriptionNotification");
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

		// Token: 0x04001025 RID: 4133
		public static int RpcServerTooBusy = 1723;
	}
}
