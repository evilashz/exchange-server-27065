using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.SubscriptionCompletion
{
	// Token: 0x020003F3 RID: 1011
	internal class SubscriptionCompletionRpcClient : RpcClientBase
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x00056E38 File Offset: 0x00056238
		public SubscriptionCompletionRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
		{
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00056E24 File Offset: 0x00056224
		public SubscriptionCompletionRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00056E50 File Offset: 0x00056250
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] SubscriptionComplete(int version, byte[] inBlob)
		{
			byte[] result = null;
			byte* ptr = null;
			byte* ptr2 = null;
			int cBytes = 0;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						int num = 0;
						ptr = <Module>.MToUBytesClient(inBlob, &num);
						<Module>.cli_SubscriptionComplete(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode)
						{
							flag = (!flag || flag);
						}
						RpcClientBase.ThrowRpcException(exceptionCode, "SubscriptionComplete");
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

		// Token: 0x04001023 RID: 4131
		public static int RpcServerTooBusy = 1723;
	}
}
