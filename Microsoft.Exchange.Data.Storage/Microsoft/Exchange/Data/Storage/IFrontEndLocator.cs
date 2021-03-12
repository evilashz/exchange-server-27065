using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D50 RID: 3408
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFrontEndLocator
	{
		// Token: 0x0600761E RID: 30238
		Uri GetWebServicesUrl(IExchangePrincipal exchangePrincipal);

		// Token: 0x0600761F RID: 30239
		Uri GetOwaUrl(IExchangePrincipal exchangePrincipal);
	}
}
