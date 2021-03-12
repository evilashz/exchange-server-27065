using System;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200010D RID: 269
	public enum SupervisionRecipientType
	{
		// Token: 0x040003CB RID: 971
		None,
		// Token: 0x040003CC RID: 972
		[LocDescription(Strings.IDs.IndividualRecipient)]
		IndividualRecipient,
		// Token: 0x040003CD RID: 973
		[LocDescription(Strings.IDs.DistributionGroup)]
		DistributionGroup,
		// Token: 0x040003CE RID: 974
		[LocDescription(Strings.IDs.ExternalAddress)]
		ExternalAddress
	}
}
