using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.NotificationsBroker
{
	// Token: 0x020002DE RID: 734
	internal class NotificationsBrokerAsyncRpcState_Subscribe : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcState_Subscribe,Microsoft::Exchange::Rpc::NotificationsBroker::NotificationsBrokerAsyncRpcServer,Microsoft::Exchange::Rpc::INotificationsBrokerAsyncDispatch>
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x00032740 File Offset: 0x00031B40
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NotificationsBrokerAsyncRpcServer asyncServer, IntPtr bindingHandle, IntPtr pSubscription)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.m_bindingHandle = bindingHandle;
			this.m_pSubscription = pSubscription;
			this.m_clientBinding = null;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00031EE4 File Offset: 0x000312E4
		public override void InternalReset()
		{
			this.m_bindingHandle = IntPtr.Zero;
			this.m_pSubscription = IntPtr.Zero;
			this.m_clientBinding = null;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003276C File Offset: 0x00031B6C
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			string subscription = null;
			if (this.m_pSubscription != IntPtr.Zero)
			{
				subscription = Marshal.PtrToStringUni(this.m_pSubscription);
			}
			RpcClientBinding clientBinding = new RpcClientBinding(this.m_bindingHandle, base.AsyncState);
			this.m_clientBinding = clientBinding;
			base.AsyncDispatch.BeginSubscribe(null, clientBinding, subscription, asyncCallback, this);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x000327D4 File Offset: 0x00031BD4
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			return (int)base.AsyncDispatch.EndSubscribe(asyncResult);
		}

		// Token: 0x04000D9C RID: 3484
		private IntPtr m_bindingHandle;

		// Token: 0x04000D9D RID: 3485
		private IntPtr m_pSubscription;

		// Token: 0x04000D9E RID: 3486
		private ClientBinding m_clientBinding;
	}
}
