using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000441 RID: 1089
	internal enum GetUcwaUserConfigurationMetaData
	{
		// Token: 0x040014A3 RID: 5283
		[DisplayName("UCWA", "MS")]
		ManagerSipUri,
		// Token: 0x040014A4 RID: 5284
		[DisplayName("UCWA", "ORG")]
		Organization,
		// Token: 0x040014A5 RID: 5285
		[DisplayName("UCWA", "IUS")]
		IsUcwaSupported,
		// Token: 0x040014A6 RID: 5286
		[DisplayName("UCWA", "ALS")]
		AuthenticatedLyncAutodiscoverServer,
		// Token: 0x040014A7 RID: 5287
		[DisplayName("UCWA", "AFC")]
		IsAuthdServerFromCache,
		// Token: 0x040014A8 RID: 5288
		[DisplayName("UCWA", "URL")]
		UcwaUrl,
		// Token: 0x040014A9 RID: 5289
		[DisplayName("UCWA", "UFC")]
		IsUcwaUrlFromCache,
		// Token: 0x040014AA RID: 5290
		[DisplayName("UCWA", "OCID")]
		OAuthCorrelationId,
		// Token: 0x040014AB RID: 5291
		[DisplayName("UCWA", "URH")]
		UnauthenticatedRedirectHops,
		// Token: 0x040014AC RID: 5292
		[DisplayName("UCWA", "ARH")]
		AuthenticatedRedirectHops,
		// Token: 0x040014AD RID: 5293
		[DisplayName("UCWA", "ITC")]
		IsTaskCompleted,
		// Token: 0x040014AE RID: 5294
		[DisplayName("UCWA", "EX")]
		Exceptions,
		// Token: 0x040014AF RID: 5295
		[DisplayName("UCWA", "WEX")]
		WorkerExceptions,
		// Token: 0x040014B0 RID: 5296
		[DisplayName("UCWA", "RQH")]
		RequestHeaders,
		// Token: 0x040014B1 RID: 5297
		[DisplayName("UCWA", "RSH")]
		ResponseHeaders,
		// Token: 0x040014B2 RID: 5298
		[DisplayName("UCWA", "RSB")]
		ResponseBody,
		// Token: 0x040014B3 RID: 5299
		[DisplayName("UCWA", "CO")]
		CacheOperation
	}
}
