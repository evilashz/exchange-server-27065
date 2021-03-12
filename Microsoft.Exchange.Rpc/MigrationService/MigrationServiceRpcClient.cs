using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x020002BD RID: 701
	internal class MigrationServiceRpcClient : RpcClientBase, IMigrationServiceRpc
	{
		// Token: 0x06000CC8 RID: 3272 RVA: 0x000307F4 File Offset: 0x0002FBF4
		public MigrationServiceRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
		{
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000307E0 File Offset: 0x0002FBE0
		public MigrationServiceRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0003080C File Offset: 0x0002FC0C
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual byte[] InvokeMigrationServiceEndPoint(int version, byte[] inBlob)
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
						<Module>.cli_InvokeMigrationServiceEndPoint(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode)
						{
							flag = (!flag || flag);
						}
						MigrationRpcExceptionHelper.ThrowRpcException(exceptionCode, "InvokeMigrationServiceEndPoint");
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

		// Token: 0x04000D70 RID: 3440
		public static int RpcServerTooBusy = 1723;
	}
}
