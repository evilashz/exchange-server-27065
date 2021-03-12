using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Rpc.NotificationsBroker
{
	// Token: 0x020002D9 RID: 729
	internal class ClientAsyncCallState_Unsubscribe : ClientAsyncCallState
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00031774 File Offset: 0x00030B74
		private void Cleanup()
		{
			if (this.m_szSubscription != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_szSubscription);
				this.m_szSubscription = IntPtr.Zero;
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00031A00 File Offset: 0x00030E00
		public ClientAsyncCallState_Unsubscribe(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr bindingHandle, string subscription) : base("Unsubscribe", asyncCallback, asyncState)
		{
			try
			{
				this.m_pRpcBindingHandle = bindingHandle;
				this.m_szSubscription = IntPtr.Zero;
				bool flag = false;
				try
				{
					IntPtr szSubscription = Marshal.StringToHGlobalUni(subscription);
					this.m_szSubscription = szSubscription;
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.Cleanup();
					}
				}
			}
			catch
			{
				base.Dispose(true);
				throw;
			}
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000317B4 File Offset: 0x00030BB4
		private void ~ClientAsyncCallState_Unsubscribe()
		{
			this.Cleanup();
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000317C8 File Offset: 0x00030BC8
		public unsafe override void InternalBegin()
		{
			<Module>.cli_Unsubscribe((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_pRpcBindingHandle.ToPointer(), (ushort*)this.m_szSubscription.ToPointer());
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00031A8C File Offset: 0x00030E8C
		public BrokerStatus End()
		{
			BrokerStatus result;
			try
			{
				result = (BrokerStatus)base.CheckCompletion();
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00031AC8 File Offset: 0x00030EC8
		[HandleProcessCorruptedStateExceptions]
		protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				try
				{
					this.Cleanup();
					return;
				}
				finally
				{
					base.Dispose(true);
				}
			}
			base.Dispose(false);
		}

		// Token: 0x04000D85 RID: 3461
		private IntPtr m_pRpcBindingHandle;

		// Token: 0x04000D86 RID: 3462
		private IntPtr m_szSubscription;
	}
}
