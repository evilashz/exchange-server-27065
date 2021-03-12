using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.EdgeSync
{
	// Token: 0x020001D2 RID: 466
	internal class EdgeSyncRpcClient : RpcClientBase
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x0001B36C File Offset: 0x0001A76C
		public EdgeSyncRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001B380 File Offset: 0x0001A780
		[HandleProcessCorruptedStateExceptions]
		public unsafe StartResults StartSyncNow(string targetServer, [MarshalAs(UnmanagedType.U1)] bool forceFullSync, [MarshalAs(UnmanagedType.U1)] bool forceUpdateCookie)
		{
			int result = -1;
			ushort* ptr = null;
			try
			{
				if (targetServer != null)
				{
					ptr = <Module>.StringToUnmanaged(targetServer);
				}
				try
				{
					int num = forceUpdateCookie ? 1 : 0;
					int num2 = forceFullSync ? 1 : 0;
					int num3 = <Module>.cli_StartSyncNow(base.BindingHandle, ptr, num2, num, &result);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_StartSyncNow");
				}
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
			return (StartResults)result;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001B420 File Offset: 0x0001A820
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] GetSyncNowResult(ref GetResultResults continueFlag)
		{
			byte* ptr = null;
			byte[] result = null;
			try
			{
				int num2;
				int cBytes;
				try
				{
					int num = <Module>.cli_GetSyncNowResult(base.BindingHandle, &num2, &cBytes, &ptr);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "cli_GetSyncNowResult");
				}
				result = <Module>.UToMBytes(cBytes, ptr);
				continueFlag = (GetResultResults)num2;
			}
			finally
			{
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
			return result;
		}
	}
}
