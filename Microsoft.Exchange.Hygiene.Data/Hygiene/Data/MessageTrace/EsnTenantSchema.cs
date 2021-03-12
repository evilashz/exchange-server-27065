using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000142 RID: 322
	internal class EsnTenantSchema
	{
		// Token: 0x04000641 RID: 1601
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonMessageTraceSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000642 RID: 1602
		internal static readonly HygienePropertyDefinition RecipientCountProperty = new HygienePropertyDefinition("RecipientCount", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000643 RID: 1603
		internal static readonly HygienePropertyDefinition MessageCountProperty = new HygienePropertyDefinition("MessageCount", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000644 RID: 1604
		internal static readonly HygienePropertyDefinition UpperBoundaryQueryProperty = new HygienePropertyDefinition("UpperBoundaryDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
