using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E5 RID: 485
	internal interface IADSecurityPrincipal
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001673 RID: 5747
		bool IsSecurityPrincipal { get; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001674 RID: 5748
		// (set) Token: 0x06001675 RID: 5749
		string SamAccountName { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001676 RID: 5750
		SecurityIdentifier Sid { get; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001677 RID: 5751
		SecurityIdentifier MasterAccountSid { get; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001678 RID: 5752
		MultiValuedProperty<SecurityIdentifier> SidHistory { get; }
	}
}
