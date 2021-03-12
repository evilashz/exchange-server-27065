using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001E6 RID: 486
	internal interface IADMailboxRecipient : IADRecipient, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag, IADMailStorage, IADSecurityPrincipal
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001679 RID: 5753
		// (set) Token: 0x0600167A RID: 5754
		ModernGroupObjectType ModernGroupType { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600167B RID: 5755
		MultiValuedProperty<SecurityIdentifier> PublicToGroupSids { get; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600167C RID: 5756
		Uri SharePointUrl { get; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600167D RID: 5757
		DateTime? WhenMailboxCreated { get; }
	}
}
