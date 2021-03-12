using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.ExchangeService;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EA9 RID: 3753
	internal abstract class ExchangeServiceProvider
	{
		// Token: 0x060061BC RID: 25020 RVA: 0x00131024 File Offset: 0x0012F224
		public ExchangeServiceProvider(IExchangeService exchangeService)
		{
			ArgumentValidator.ThrowIfNull("exchangeService", exchangeService);
			this.ExchangeService = exchangeService;
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x060061BD RID: 25021 RVA: 0x0013103E File Offset: 0x0012F23E
		// (set) Token: 0x060061BE RID: 25022 RVA: 0x00131046 File Offset: 0x0012F246
		public IExchangeService ExchangeService { get; private set; }
	}
}
