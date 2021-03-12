using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000010 RID: 16
	internal abstract class ExchangeDispatchTask : DispatchTask
	{
		// Token: 0x06000086 RID: 134 RVA: 0x000045B1 File Offset: 0x000027B1
		public ExchangeDispatchTask(string description, IExchangeDispatch exchangeDispatch, ProtocolRequestInfo protocolRequestInfo, CancelableAsyncCallback asyncCallback, object asyncState) : base(description, asyncCallback, asyncState)
		{
			this.exchangeDispatch = exchangeDispatch;
			this.protocolRequestInfo = protocolRequestInfo;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000045CC File Offset: 0x000027CC
		internal IExchangeDispatch ExchangeDispatch
		{
			get
			{
				return this.exchangeDispatch;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000045D4 File Offset: 0x000027D4
		internal ProtocolRequestInfo ProtocolRequestInfo
		{
			get
			{
				return this.protocolRequestInfo;
			}
		}

		// Token: 0x0400005F RID: 95
		private readonly IExchangeDispatch exchangeDispatch;

		// Token: 0x04000060 RID: 96
		private readonly ProtocolRequestInfo protocolRequestInfo;
	}
}
