using System;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200023A RID: 570
	internal interface IRoutingComponent : ITransportComponent
	{
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600190B RID: 6411
		IMailRouter MailRouter { get; }

		// Token: 0x0600190C RID: 6412
		void SetLoadTimeDependencies(TransportAppConfig appConfig, ITransportConfiguration transportConfig);

		// Token: 0x0600190D RID: 6413
		void SetRunTimeDependencies(ShadowRedundancyComponent shadowRedundancy, UnhealthyTargetFilterComponent unhealthyTargetFilter, CategorizerComponent categorizer);
	}
}
