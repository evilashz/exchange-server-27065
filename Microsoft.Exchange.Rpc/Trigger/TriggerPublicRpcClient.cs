using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Trigger
{
	// Token: 0x020003FB RID: 1019
	internal class TriggerPublicRpcClient : RpcClientBase
	{
		// Token: 0x06001172 RID: 4466 RVA: 0x000575FC File Offset: 0x000569FC
		public TriggerPublicRpcClient(string machineName) : base(machineName, null, null, AuthenticationService.Negotiate, <Module>.cli_TriggerPublicRPC_v16_1_c_ifspec, true)
		{
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000575DC File Offset: 0x000569DC
		public TriggerPublicRpcClient() : base(null, null, null, AuthenticationService.Negotiate, <Module>.cli_TriggerPublicRPC_v16_1_c_ifspec, true)
		{
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0005761C File Offset: 0x00056A1C
		[HandleProcessCorruptedStateExceptions]
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe bool ValidateEmailAddressTemplate(string dc, string templateString)
		{
			int num = 0;
			IntPtr hglobal = Marshal.StringToHGlobalUni(dc);
			IntPtr hglobal2 = Marshal.StringToHGlobalUni(templateString);
			ushort* ptr = null;
			try
			{
				int num2 = <Module>.cli_ScSiteProxyValidateDC(base.BindingHandle, (ushort*)hglobal.ToPointer(), (ushort*)hglobal2.ToPointer(), &num, &ptr);
				if (num2 != null)
				{
					Marshal.ThrowExceptionForHR(<Module>.HrFromSc(num2));
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "ValidateEmailAddressTemplate");
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
				Marshal.FreeHGlobal(hglobal2);
				<Module>.MIDL_user_free((void*)ptr);
			}
			return ((num != 0) ? 1 : 0) != 0;
		}
	}
}
