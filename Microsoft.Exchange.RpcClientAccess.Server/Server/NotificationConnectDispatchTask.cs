using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000014 RID: 20
	internal sealed class NotificationConnectDispatchTask : ExchangeDispatchTask
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004969 File Offset: 0x00002B69
		public NotificationConnectDispatchTask(IExchangeDispatch exchangeDispatch, CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle) : base("NotificationConnectDispatchTask", exchangeDispatch, protocolRequestInfo, asyncCallback, asyncState)
		{
			this.contextHandle = contextHandle;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004983 File Offset: 0x00002B83
		internal override IntPtr ContextHandle
		{
			get
			{
				return this.contextHandle;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000498B File Offset: 0x00002B8B
		internal override int? InternalExecute()
		{
			return new int?(base.ExchangeDispatch.NotificationConnect(base.ProtocolRequestInfo, this.contextHandle, out this.notificationContextHandle));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000049B0 File Offset: 0x00002BB0
		public int End(out IntPtr notificationContextHandle)
		{
			int result = base.CheckCompletion();
			notificationContextHandle = this.notificationContextHandle;
			return result;
		}

		// Token: 0x0400007F RID: 127
		private readonly IntPtr contextHandle;

		// Token: 0x04000080 RID: 128
		private IntPtr notificationContextHandle;
	}
}
