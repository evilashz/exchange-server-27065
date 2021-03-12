using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000017 RID: 23
	internal sealed class UnregisterPushNotificationDispatchTask : ExchangeDispatchTask
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00004B21 File Offset: 0x00002D21
		public UnregisterPushNotificationDispatchTask(IExchangeDispatch exchangeDispatch, CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle, int notificationHandle) : base("UnregisterPushNotificationDispatchTask", exchangeDispatch, protocolRequestInfo, asyncCallback, asyncState)
		{
			this.contextHandleIn = contextHandle;
			this.contextHandleOut = contextHandle;
			this.notificationHandle = notificationHandle;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004B4B File Offset: 0x00002D4B
		internal override IntPtr ContextHandle
		{
			get
			{
				return this.contextHandleIn;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004B53 File Offset: 0x00002D53
		internal override int? InternalExecute()
		{
			return new int?(base.ExchangeDispatch.UnregisterPushNotification(base.ProtocolRequestInfo, ref this.contextHandleOut, this.notificationHandle));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004B78 File Offset: 0x00002D78
		public int End(out IntPtr contextHandle)
		{
			int result = base.CheckCompletion();
			contextHandle = this.contextHandleOut;
			return result;
		}

		// Token: 0x0400008A RID: 138
		private readonly int notificationHandle;

		// Token: 0x0400008B RID: 139
		private IntPtr contextHandleIn;

		// Token: 0x0400008C RID: 140
		private IntPtr contextHandleOut;
	}
}
