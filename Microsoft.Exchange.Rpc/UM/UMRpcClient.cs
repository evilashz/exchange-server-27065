using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x020003FD RID: 1021
	internal class UMRpcClient : RpcClientBase
	{
		// Token: 0x06001179 RID: 4473 RVA: 0x00057AB4 File Offset: 0x00056EB4
		public UMRpcClient(string machineName) : base(machineName)
		{
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00057AC8 File Offset: 0x00056EC8
		[HandleProcessCorruptedStateExceptions]
		public unsafe byte[] GetUmActiveCalls([MarshalAs(UnmanagedType.U1)] bool isDialPlan, string dialPlan, [MarshalAs(UnmanagedType.U1)] bool isIpGateway, string ipGateway)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			byte[] result = null;
			byte* ptr = null;
			try
			{
				intPtr = Marshal.StringToHGlobalUni((!(dialPlan == null)) ? dialPlan : string.Empty);
				intPtr2 = Marshal.StringToHGlobalUni((!(ipGateway == null)) ? ipGateway : string.Empty);
				int num = isIpGateway ? 1 : 0;
				int num2 = isDialPlan ? 1 : 0;
				int cBytes;
				<Module>.cli_GetUmActiveCalls(base.BindingHandle, num2, (ushort*)intPtr.ToPointer(), num, (ushort*)intPtr2.ToPointer(), &cBytes, &ptr);
				result = <Module>.UToMBytes(cBytes, ptr);
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "GetUmActiveCalls");
			}
			finally
			{
				if (IntPtr.Zero != intPtr)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (IntPtr.Zero != intPtr2)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				if (ptr != null)
				{
					<Module>.MIDL_user_free((void*)ptr);
				}
			}
			return result;
		}
	}
}
