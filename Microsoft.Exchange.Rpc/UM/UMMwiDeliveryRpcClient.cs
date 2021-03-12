using System;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.UM
{
	// Token: 0x020003FE RID: 1022
	internal class UMMwiDeliveryRpcClient : RpcClientBase
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00057840 File Offset: 0x00056C40
		public string OperationName
		{
			get
			{
				return this.operationName;
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00057C20 File Offset: 0x00057020
		public UMMwiDeliveryRpcClient(string machineName, NetworkCredential nc) : base(machineName, null, nc, AuthenticationService.Negotiate, null, true)
		{
			try
			{
				this.operationName = string.Format("{0}(IUMMwiDelivery)", machineName);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00057BD4 File Offset: 0x00056FD4
		public UMMwiDeliveryRpcClient(string machineName) : base(machineName)
		{
			try
			{
				this.operationName = string.Format("{0}(IUMMwiDelivery)", machineName);
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00057C74 File Offset: 0x00057074
		[HandleProcessCorruptedStateExceptions]
		public unsafe virtual void SendMwiMessage(Guid mailboxGuid, Guid dialPlanGuid, string userExtension, string userName, int unreadVoicemailCount, int totalVoicemailCount, int assistantLatencyMsec, Guid tenantGuid)
		{
			int num = 0;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				try
				{
					intPtr = Marshal.StringToHGlobalUni(userExtension);
					intPtr2 = Marshal.StringToHGlobalUni(userName);
					_GUID guid = <Module>.ToGUID(ref tenantGuid);
					_GUID guid2 = <Module>.ToGUID(ref dialPlanGuid);
					_GUID guid3 = <Module>.ToGUID(ref mailboxGuid);
					num = <Module>.cli_SendMwiMessage_v2_0(base.BindingHandle, guid3, guid2, (ushort*)intPtr.ToPointer(), (ushort*)intPtr2.ToPointer(), unreadVoicemailCount, totalVoicemailCount, assistantLatencyMsec, guid);
				}
				catch when (endfilter(true))
				{
					RpcClientBase.ThrowRpcException(Marshal.GetExceptionCode(), "SendMwiMessage");
				}
				if (num < 0)
				{
					RpcClientBase.ThrowRpcException(num, "SendMwiMessage");
				}
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
			}
		}

		// Token: 0x0400102E RID: 4142
		protected string operationName;
	}
}
