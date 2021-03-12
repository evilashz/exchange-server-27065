using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Hygiene.Data.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000BB RID: 187
	internal class ADForeignPrincipalSchema : CommonSyncProperties
	{
		// Token: 0x040003C3 RID: 963
		public static readonly HygienePropertyDefinition DisplayNameProperty = new HygienePropertyDefinition("DisplayName", typeof(string));

		// Token: 0x040003C4 RID: 964
		public static readonly HygienePropertyDefinition DescriptionProperty = new HygienePropertyDefinition("Description", typeof(string));

		// Token: 0x040003C5 RID: 965
		public static readonly HygienePropertyDefinition ForeignContextIdProperty = new HygienePropertyDefinition("LinkedPartnerOrganizationId", typeof(ADObjectId));

		// Token: 0x040003C6 RID: 966
		public static readonly HygienePropertyDefinition ForeignPrincipalIdProperty = new HygienePropertyDefinition("LinkedPartnerGroupId", typeof(ADObjectId));
	}
}
