using System;
using Microsoft.Exchange.Services.ExchangeService;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E29 RID: 3625
	internal abstract class ExchangeServiceCommand<TRequest, TResponse> : ODataCommand<TRequest, TResponse> where TRequest : ODataRequest where TResponse : ODataResponse
	{
		// Token: 0x06005D72 RID: 23922 RVA: 0x00123196 File Offset: 0x00121396
		public ExchangeServiceCommand(TRequest request) : base(request)
		{
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x0012319F File Offset: 0x0012139F
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.exchangeService != null)
			{
				this.exchangeService.Dispose();
				this.exchangeService = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x17001521 RID: 5409
		// (get) Token: 0x06005D74 RID: 23924 RVA: 0x001231C8 File Offset: 0x001213C8
		protected IExchangeService ExchangeService
		{
			get
			{
				if (this.exchangeService == null)
				{
					ExchangeServiceFactory @default = ExchangeServiceFactory.Default;
					TRequest request = base.Request;
					this.exchangeService = @default.CreateForEws(request.ODataContext.CallContext);
				}
				return this.exchangeService;
			}
		}

		// Token: 0x04003276 RID: 12918
		private IExchangeService exchangeService;
	}
}
