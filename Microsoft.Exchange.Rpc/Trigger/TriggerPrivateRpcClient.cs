using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.Trigger
{
	// Token: 0x020003FC RID: 1020
	internal class TriggerPrivateRpcClient : RpcClientBase
	{
		// Token: 0x06001175 RID: 4469 RVA: 0x000576FC File Offset: 0x00056AFC
		public TriggerPrivateRpcClient(string machineName)
		{
			this.m_machineName = machineName;
			base..ctor(machineName, null, null, AuthenticationService.Negotiate, <Module>.cli_TriggerPrivateRPC_v6_0_c_ifspec, true);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000576D4 File Offset: 0x00056AD4
		public TriggerPrivateRpcClient()
		{
			this.m_machineName = null;
			base..ctor(null, null, null, AuthenticationService.Negotiate, <Module>.cli_TriggerPrivateRPC_v6_0_c_ifspec, true);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00057724 File Offset: 0x00056B24
		[HandleProcessCorruptedStateExceptions]
		public unsafe void RunOabGenTask(string oabDN)
		{
			IntPtr hglobal = Marshal.StringToHGlobalUni(oabDN);
			try
			{
				int num = <Module>.cli_ScRunOffLineABTask(base.BindingHandle, (ushort*)hglobal.ToPointer());
				if (num != null)
				{
					Marshal.ThrowExceptionForHR(<Module>.HrFromSc(num));
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "RunOabGenTask");
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000577B0 File Offset: 0x00056BB0
		[HandleProcessCorruptedStateExceptions]
		public unsafe void CreateExchangeOabFolder()
		{
			IntPtr hglobal = Marshal.StringToHGlobalUni(this.m_machineName);
			try
			{
				int num = <Module>.cli_ScCreateExchangeOabFolder(base.BindingHandle, (ushort*)hglobal.ToPointer());
				if (num != null)
				{
					Marshal.ThrowExceptionForHR(<Module>.HrFromSc(num));
				}
			}
			catch when (endfilter(true))
			{
				RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "CreateExchangeOabFolder");
			}
			finally
			{
				Marshal.FreeHGlobal(hglobal);
			}
		}

		// Token: 0x0400102D RID: 4141
		private string m_machineName;
	}
}
