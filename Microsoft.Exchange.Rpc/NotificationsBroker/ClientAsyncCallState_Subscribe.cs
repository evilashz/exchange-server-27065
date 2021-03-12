using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Rpc.NotificationsBroker
{
	// Token: 0x020002D8 RID: 728
	internal class ClientAsyncCallState_Subscribe : ClientAsyncCallState
	{
		// Token: 0x06000CE2 RID: 3298 RVA: 0x000316E8 File Offset: 0x00030AE8
		private void Cleanup()
		{
			if (this.m_szSubscription != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.m_szSubscription);
				this.m_szSubscription = IntPtr.Zero;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000318F4 File Offset: 0x00030CF4
		public ClientAsyncCallState_Subscribe(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr bindingHandle, string subscription) : base("Subscribe", asyncCallback, asyncState)
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

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00031728 File Offset: 0x00030B28
		private void ~ClientAsyncCallState_Subscribe()
		{
			this.Cleanup();
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0003173C File Offset: 0x00030B3C
		public unsafe override void InternalBegin()
		{
			<Module>.cli_Subscribe((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_pRpcBindingHandle.ToPointer(), (ushort*)this.m_szSubscription.ToPointer());
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00031980 File Offset: 0x00030D80
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

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000319BC File Offset: 0x00030DBC
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

		// Token: 0x04000D83 RID: 3459
		private IntPtr m_pRpcBindingHandle;

		// Token: 0x04000D84 RID: 3460
		private IntPtr m_szSubscription;
	}
}
