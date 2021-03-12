using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000ACB RID: 2763
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IPerTenantRMSTrustedPublishingDomainConfiguration
	{
		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x06006478 RID: 25720
		Uri IntranetLicensingUrl { get; }

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x06006479 RID: 25721
		Uri ExtranetLicensingUrl { get; }

		// Token: 0x17001BC1 RID: 7105
		// (get) Token: 0x0600647A RID: 25722
		Uri IntranetCertificationUrl { get; }

		// Token: 0x17001BC2 RID: 7106
		// (get) Token: 0x0600647B RID: 25723
		Uri ExtranetCertificationUrl { get; }

		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x0600647C RID: 25724
		string CompressedSLCCertChain { get; }

		// Token: 0x17001BC4 RID: 7108
		// (get) Token: 0x0600647D RID: 25725
		Dictionary<string, PrivateKeyInformation> PrivateKeys { get; }

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x0600647E RID: 25726
		IList<string> CompressedRMSTemplates { get; }

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x0600647F RID: 25727
		IList<string> CompressedTrustedDomainChains { get; }
	}
}
