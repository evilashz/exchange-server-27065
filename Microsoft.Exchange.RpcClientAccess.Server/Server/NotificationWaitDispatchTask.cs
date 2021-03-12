using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000015 RID: 21
	internal sealed class NotificationWaitDispatchTask : ExchangeDispatchTask
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000049D1 File Offset: 0x00002BD1
		public NotificationWaitDispatchTask(IExchangeDispatch exchangeDispatch, CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, IntPtr notificationContextHandle, int flagsIn) : base("NotificationWaitDispatchTask", exchangeDispatch, protocolRequestInfo, asyncCallback, asyncState)
		{
			this.notificationContextHandle = notificationContextHandle;
			this.flagsIn = flagsIn;
			this.flagsOut = 1;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000049FA File Offset: 0x00002BFA
		internal override IntPtr ContextHandle
		{
			get
			{
				return this.notificationContextHandle;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004A20 File Offset: 0x00002C20
		internal override int? InternalExecute()
		{
			base.ExchangeDispatch.NotificationWait(base.ProtocolRequestInfo, this.notificationContextHandle, this.flagsIn, delegate(bool notificationsAvailable, int errorCode)
			{
				if (notificationsAvailable)
				{
					this.flagsOut = 1;
				}
				else
				{
					this.flagsOut = 0;
				}
				base.Completion(null, errorCode);
			});
			return null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004A60 File Offset: 0x00002C60
		public int End(out int flagsOut)
		{
			int result = base.CheckCompletion();
			flagsOut = this.flagsOut;
			return result;
		}

		// Token: 0x04000081 RID: 129
		private readonly IntPtr notificationContextHandle;

		// Token: 0x04000082 RID: 130
		private readonly int flagsIn;

		// Token: 0x04000083 RID: 131
		private int flagsOut;
	}
}
