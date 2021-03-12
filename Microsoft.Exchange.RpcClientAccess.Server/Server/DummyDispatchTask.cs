using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000018 RID: 24
	internal sealed class DummyDispatchTask : ExchangeDispatchTask
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004B99 File Offset: 0x00002D99
		public DummyDispatchTask(IExchangeDispatch exchangeDispatch, CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding) : base("DummyDispatchTask", exchangeDispatch, protocolRequestInfo, asyncCallback, asyncState)
		{
			this.clientBinding = clientBinding;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004BB3 File Offset: 0x00002DB3
		internal override int? InternalExecute()
		{
			return new int?(base.ExchangeDispatch.Dummy(base.ProtocolRequestInfo, this.clientBinding));
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00004BD1 File Offset: 0x00002DD1
		internal override IntPtr ContextHandle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004BD8 File Offset: 0x00002DD8
		public int End()
		{
			return base.CheckCompletion();
		}

		// Token: 0x0400008D RID: 141
		private readonly ClientBinding clientBinding;
	}
}
