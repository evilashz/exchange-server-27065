using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.ProcessAccess
{
	// Token: 0x0200039A RID: 922
	internal class ProcessAccessRpcClient : RpcClientBase
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x0004AB94 File Offset: 0x00049F94
		public ProcessAccessRpcClient(string machineName, NetworkCredential nc) : base(machineName, nc)
		{
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0004AB7C File Offset: 0x00049F7C
		public ProcessAccessRpcClient(string machineName, ValueType clientObjectGuid) : base(machineName, clientObjectGuid)
		{
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0004AB68 File Offset: 0x00049F68
		public ProcessAccessRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0004ABAC File Offset: 0x00049FAC
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] RunProcessCommand(byte[] inBytes)
		{
			byte[] result = null;
			IntPtr hglobal = IntPtr.Zero;
			byte* ptr = null;
			int cBytes = 0;
			try
			{
				bool flag = false;
				do
				{
					try
					{
						int num = 0;
						hglobal = <Module>.MToUBytes(inBytes, &num);
						<Module>.cli_RunProcessCommand(base.BindingHandle, num, (byte*)hglobal.ToPointer(), &cBytes, &ptr);
						flag = false;
					}
					catch when (endfilter(true))
					{
						int exceptionCode = Marshal.GetExceptionCode();
						if (1727 == exceptionCode)
						{
							flag = (!flag || flag);
						}
						<Module>.Microsoft.Exchange.Rpc.ThrowRpcExceptionWithEEInfo(exceptionCode, "RunProcessCommand");
					}
				}
				while (flag);
				result = <Module>.UToMBytes(cBytes, ptr);
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
			return result;
		}

		// Token: 0x04000F75 RID: 3957
		public static int RpcServerTooBusy = 1723;
	}
}
