using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000019 RID: 25
	internal interface IEWSPartnerRequestContext
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000186 RID: 390
		AuthZClientInfo CallerClientInfo { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000187 RID: 391
		ExchangePrincipal ExchangePrincipal { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000188 RID: 392
		string UserAgent { get; }
	}
}
