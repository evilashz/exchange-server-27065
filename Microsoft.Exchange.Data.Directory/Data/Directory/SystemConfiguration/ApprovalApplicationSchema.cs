using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003A3 RID: 931
	internal class ApprovalApplicationSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040019C7 RID: 6599
		public static readonly ADPropertyDefinition ArbitrationMailboxesBacklink = new ADPropertyDefinition("ArbitrationMailboxesBacklink", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchArbitrationMailboxesBL", ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040019C8 RID: 6600
		public static readonly ADPropertyDefinition ELCRetentionPolicyTag = new ADPropertyDefinition("ELCRetentionPolicyTag", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchRetentionPolicyTag", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
