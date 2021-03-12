using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Notifications.Broker;

namespace Microsoft.Exchange.Rpc.NotificationsBroker
{
	// Token: 0x020002DA RID: 730
	internal class ClientAsyncCallState_GetNextNotification : ClientAsyncCallState
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x00031800 File Offset: 0x00030C00
		private void Cleanup()
		{
			if (this.m_pszNotification != IntPtr.Zero)
			{
				IntPtr intPtr = Marshal.ReadIntPtr(this.m_pszNotification);
				IntPtr intPtr2 = intPtr;
				if (intPtr != IntPtr.Zero)
				{
					<Module>.MIDL_user_free(intPtr2.ToPointer());
				}
				Marshal.FreeHGlobal(this.m_pszNotification);
				this.m_pszNotification = IntPtr.Zero;
			}
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00031B0C File Offset: 0x00030F0C
		public ClientAsyncCallState_GetNextNotification(CancelableAsyncCallback asyncCallback, object asyncState, IntPtr bindingHandle, int consumerId, Guid ackNotificationId) : base("GetNextNotification", asyncCallback, asyncState)
		{
			try
			{
				this.m_pRpcBindingHandle = bindingHandle;
				this.m_consumerId = consumerId;
				this.m_ackNotificationId = ackNotificationId;
				this.m_pszNotification = IntPtr.Zero;
				bool flag = false;
				try
				{
					IntPtr pszNotification = Marshal.AllocHGlobal(IntPtr.Size);
					this.m_pszNotification = pszNotification;
					Marshal.WriteIntPtr(this.m_pszNotification, IntPtr.Zero);
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

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0003186C File Offset: 0x00030C6C
		private void ~ClientAsyncCallState_GetNextNotification()
		{
			this.Cleanup();
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00031880 File Offset: 0x00030C80
		public unsafe override void InternalBegin()
		{
			Guid ackNotificationId = this.m_ackNotificationId;
			_GUID guid = <Module>.ToGUID(ref ackNotificationId);
			<Module>.cli_GetNextNotification((_RPC_ASYNC_STATE*)base.RpcAsyncState().ToPointer(), this.m_pRpcBindingHandle.ToPointer(), this.m_consumerId, guid, (ushort**)this.m_pszNotification.ToPointer());
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00031BC0 File Offset: 0x00030FC0
		public BrokerStatus End(out string notification)
		{
			BrokerStatus result;
			try
			{
				BrokerStatus brokerStatus = (BrokerStatus)base.CheckCompletion();
				notification = null;
				IntPtr intPtr = Marshal.ReadIntPtr(this.m_pszNotification);
				if (intPtr != IntPtr.Zero)
				{
					notification = Marshal.PtrToStringUni(intPtr);
				}
				result = brokerStatus;
			}
			finally
			{
				this.Cleanup();
			}
			return result;
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00031C28 File Offset: 0x00031028
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

		// Token: 0x04000D87 RID: 3463
		private IntPtr m_pRpcBindingHandle;

		// Token: 0x04000D88 RID: 3464
		private int m_consumerId;

		// Token: 0x04000D89 RID: 3465
		private Guid m_ackNotificationId;

		// Token: 0x04000D8A RID: 3466
		private IntPtr m_pszNotification;
	}
}
