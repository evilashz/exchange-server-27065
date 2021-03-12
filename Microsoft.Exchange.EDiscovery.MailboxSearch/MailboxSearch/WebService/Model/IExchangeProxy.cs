using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200001B RID: 27
	internal interface IExchangeProxy
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019E RID: 414
		ExchangeService ExchangeService { get; }

		// Token: 0x0600019F RID: 415
		TIn Create<TIn, TOut>() where TIn : SimpleServiceRequestBase where TOut : ServiceResponse;

		// Token: 0x060001A0 RID: 416
		IEnumerable<TOut> Execute<TIn, TOut>(TIn request) where TIn : SimpleServiceRequestBase where TOut : ServiceResponse;

		// Token: 0x060001A1 RID: 417
		IAsyncResult BeginExecute<TIn, TOut>(AsyncCallback callback, object state, TIn request) where TIn : SimpleServiceRequestBase where TOut : ServiceResponse;

		// Token: 0x060001A2 RID: 418
		void Abort(IAsyncResult result);

		// Token: 0x060001A3 RID: 419
		IEnumerable<TOut> EndExecute<TIn, TOut>(IAsyncResult result) where TIn : SimpleServiceRequestBase where TOut : ServiceResponse;
	}
}
