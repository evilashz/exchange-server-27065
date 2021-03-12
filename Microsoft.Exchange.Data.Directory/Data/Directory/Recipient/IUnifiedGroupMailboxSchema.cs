using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000271 RID: 625
	internal abstract class IUnifiedGroupMailboxSchema
	{
		// Token: 0x04001041 RID: 4161
		public static readonly ADPropertyDefinition UnifiedGroupMembersLink = new ADPropertyDefinition("UnifiedGroupMembersLink", ExchangeObjectVersion.Exchange2012, typeof(ADObjectId), "msExchUGMemberLink", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001042 RID: 4162
		public static readonly ADPropertyDefinition UnifiedGroupMembersBL = new ADPropertyDefinition("UnifiedGroupMembersBL", ExchangeObjectVersion.Exchange2012, typeof(ADObjectId), "msExchUGMemberBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
