using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.MigrationService
{
	// Token: 0x0200029B RID: 667
	internal class MigrationNotificationRpcClient : RpcClientBase, IMigrationNotificationRpc
	{
		// Token: 0x06000C55 RID: 3157 RVA: 0x0002CCE4 File Offset: 0x0002C0E4
		public MigrationNotificationRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0002CCD0 File Offset: 0x0002C0D0
		public MigrationNotificationRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002CCFC File Offset: 0x0002C0FC
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual byte[] UpdateMigrationRequest(int version, byte[] inBlob)
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
						<Module>.cli_UpdateMigrationRequest(base.BindingHandle, version, num, ptr, &cBytes, &ptr2);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode)
						{
							flag = (!flag || flag);
						}
						RpcClientBase.ThrowRpcException(exceptionCode, "UpdateMigrationRequest");
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

		// Token: 0x04000D54 RID: 3412
		public static int RpcServerTooBusy = 1723;
	}
}
