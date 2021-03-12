using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000012 RID: 18
	internal sealed class DisconnectDispatchTask : ExchangeDispatchTask
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000047C4 File Offset: 0x000029C4
		public DisconnectDispatchTask(IExchangeDispatch exchangeDispatch, CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, IntPtr contextHandle) : base("DisconnectDispatchTask", exchangeDispatch, protocolRequestInfo, asyncCallback, asyncState)
		{
			this.contextHandleIn = contextHandle;
			this.contextHandleOut = contextHandle;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000047E6 File Offset: 0x000029E6
		internal override IntPtr ContextHandle
		{
			get
			{
				return this.contextHandleIn;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000047EE File Offset: 0x000029EE
		internal override int? InternalExecute()
		{
			return new int?(base.ExchangeDispatch.Disconnect(base.ProtocolRequestInfo, ref this.contextHandleOut, false));
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004810 File Offset: 0x00002A10
		public int End(out IntPtr contextHandle)
		{
			int result = base.CheckCompletion();
			contextHandle = this.contextHandleOut;
			return result;
		}

		// Token: 0x04000074 RID: 116
		private IntPtr contextHandleIn;

		// Token: 0x04000075 RID: 117
		private IntPtr contextHandleOut;
	}
}
