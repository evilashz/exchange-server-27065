using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.NotificationsBroker
{
	// Token: 0x020002E1 RID: 737
	internal class NotificationsBrokerAsyncRpcState_Unsubscribe : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Unsubscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>
	{
		// Token: 0x06000D2E RID: 3374 RVA: 0x00032804 File Offset: 0x00031C04
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NotificationsBrokerAsyncRpcServer asyncServer, IntPtr bindingHandle, IntPtr pSubscription)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.m_bindingHandle = bindingHandle;
			this.m_pSubscription = pSubscription;
			this.m_clientBinding = null;
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00031F38 File Offset: 0x00031338
		public override void InternalReset()
		{
			this.m_bindingHandle = IntPtr.Zero;
			this.m_pSubscription = IntPtr.Zero;
			this.m_clientBinding = null;
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00032830 File Offset: 0x00031C30
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			string subscription = null;
			if (this.m_pSubscription != IntPtr.Zero)
			{
				subscription = Marshal.PtrToStringUni(this.m_pSubscription);
			}
			RpcClientBinding clientBinding = new RpcClientBinding(this.m_bindingHandle, base.AsyncState);
			this.m_clientBinding = clientBinding;
			base.AsyncDispatch.BeginUnsubscribe(null, clientBinding, subscription, asyncCallback, this);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00032898 File Offset: 0x00031C98
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			return (int)base.AsyncDispatch.EndUnsubscribe(asyncResult);
		}

		// Token: 0x04000DA7 RID: 3495
		private IntPtr m_bindingHandle;

		// Token: 0x04000DA8 RID: 3496
		private IntPtr m_pSubscription;

		// Token: 0x04000DA9 RID: 3497
		private ClientBinding m_clientBinding;
	}
}
