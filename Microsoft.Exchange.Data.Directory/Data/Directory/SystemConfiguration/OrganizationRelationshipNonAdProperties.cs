using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200052B RID: 1323
	internal static class OrganizationRelationshipNonAdProperties
	{
		// Token: 0x040027EB RID: 10219
		internal static readonly ADPropertyDefinition FreeBusyAccessScopeCache = new ADPropertyDefinition("FreeBusyAccessScopeCache", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "FreeBusyAccessScopeCache", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040027EC RID: 10220
		internal static readonly ADPropertyDefinition MailTipsAccessScopeScopeCache = new ADPropertyDefinition("MailTipsAccessScopeScopeCache", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "MailTipsAccessScopeScopeCache", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
