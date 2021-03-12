using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.NotificationsBroker
{
	// Token: 0x020002E4 RID: 740
	internal class NotificationsBrokerAsyncRpcState_GetNextNotification : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_GetNextNotification,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x000328C8 File Offset: 0x00031CC8
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NotificationsBrokerAsyncRpcServer asyncServer, IntPtr bindingHandle, int consumerId, Guid ackNotificationId, IntPtr ppNotification)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.m_bindingHandle = bindingHandle;
			this.m_consumerId = consumerId;
			this.m_ackNotificationId = ackNotificationId;
			this.m_ppNotification = ppNotification;
			this.m_clientBinding = null;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00031F8C File Offset: 0x0003138C
		public override void InternalReset()
		{
			this.m_bindingHandle = IntPtr.Zero;
			this.m_consumerId = 0;
			this.m_ackNotificationId = Guid.Empty;
			this.m_ppNotification = IntPtr.Zero;
			this.m_clientBinding = null;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00032904 File Offset: 0x00031D04
		public unsafe override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			if (this.m_ppNotification != IntPtr.Zero)
			{
				*(long*)this.m_ppNotification.ToPointer() = 0L;
			}
			RpcClientBinding clientBinding = new RpcClientBinding(this.m_bindingHandle, base.AsyncState);
			this.m_clientBinding = clientBinding;
			base.AsyncDispatch.BeginGetNextNotification(null, clientBinding, this.m_consumerId, this.m_ackNotificationId, asyncCallback, this);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x00032978 File Offset: 0x00031D78
		public unsafe override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			string @string = null;
			bool flag = false;
			ushort* ptr = null;
			int result;
			try
			{
				@string = null;
				int num = (int)base.AsyncDispatch.EndGetNextNotification(asyncResult, out @string);
				if (num == 0 && this.m_ppNotification != IntPtr.Zero)
				{
					ptr = <Module>.StringToUnmanaged(@string);
					*(long*)this.m_ppNotification.ToPointer() = ptr;
				}
				flag = true;
				result = num;
			}
			finally
			{
				if (!flag && ptr != null)
				{
					<Module>.FreeString(ptr);
					if (this.m_ppNotification != IntPtr.Zero)
					{
						*(long*)this.m_ppNotification.ToPointer() = 0L;
					}
				}
			}
			return result;
		}

		// Token: 0x04000DB2 RID: 3506
		private IntPtr m_bindingHandle;

		// Token: 0x04000DB3 RID: 3507
		private int m_consumerId;

		// Token: 0x04000DB4 RID: 3508
		private Guid m_ackNotificationId;

		// Token: 0x04000DB5 RID: 3509
		private IntPtr m_ppNotification;

		// Token: 0x04000DB6 RID: 3510
		private ClientBinding m_clientBinding;
	}
}
