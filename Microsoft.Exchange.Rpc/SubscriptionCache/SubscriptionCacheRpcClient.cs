using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.SubscriptionCache
{
	// Token: 0x020003F1 RID: 1009
	internal class SubscriptionCacheRpcClient : RpcClientBase
	{
		// Token: 0x0600114B RID: 4427 RVA: 0x00056CB4 File Offset: 0x000560B4
		public SubscriptionCacheRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
		{
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00056CA0 File Offset: 0x000560A0
		public SubscriptionCacheRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00056CCC File Offset: 0x000560CC
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] TestUserCache(int version, byte[] inBlob)
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
						<Module>.cli_TestUserCache(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode)
						{
							flag = (!flag || flag);
						}
						RpcClientBase.ThrowRpcException(exceptionCode, "TestUserCache");
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

		// Token: 0x04001021 RID: 4129
		public static int RpcServerTooBusy = 1723;
	}
}
