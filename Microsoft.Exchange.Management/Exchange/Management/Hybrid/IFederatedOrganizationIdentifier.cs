using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008E7 RID: 2279
	internal interface IFederatedOrganizationIdentifier
	{
		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x060050D7 RID: 20695
		bool Enabled { get; }

		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x060050D8 RID: 20696
		SmtpDomain AccountNamespace { get; }

		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x060050D9 RID: 20697
		ADObjectId DelegationTrustLink { get; }

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x060050DA RID: 20698
		MultiValuedProperty<FederatedDomain> Domains { get; }

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x060050DB RID: 20699
		SmtpDomain DefaultDomain { get; }
	}
}
