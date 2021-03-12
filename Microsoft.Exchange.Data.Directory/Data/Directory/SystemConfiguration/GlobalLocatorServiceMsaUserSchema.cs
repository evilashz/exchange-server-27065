using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002E0 RID: 736
	internal class GlobalLocatorServiceMsaUserSchema : ObjectSchema
	{
		// Token: 0x04001581 RID: 5505
		public static readonly SimpleProviderPropertyDefinition ExternalDirectoryOrganizationId = new SimpleProviderPropertyDefinition("ExternalDirectoryOrganizationId", ExchangeObjectVersion.Exchange2012, typeof(Guid?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001582 RID: 5506
		public static readonly SimpleProviderPropertyDefinition MsaUserMemberName = new SimpleProviderPropertyDefinition("MsaUserMemberName", ExchangeObjectVersion.Exchange2012, typeof(SmtpAddress), PropertyDefinitionFlags.None, SmtpAddress.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001583 RID: 5507
		public static readonly SimpleProviderPropertyDefinition MsaUserNetId = new SimpleProviderPropertyDefinition("MsaUserNetId", ExchangeObjectVersion.Exchange2012, typeof(NetID), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001584 RID: 5508
		public static readonly SimpleProviderPropertyDefinition ResourceForest = new SimpleProviderPropertyDefinition("ResourceForest", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001585 RID: 5509
		public static readonly SimpleProviderPropertyDefinition AccountForest = new SimpleProviderPropertyDefinition("AccountForest", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001586 RID: 5510
		public static readonly SimpleProviderPropertyDefinition TenantContainerCN = new SimpleProviderPropertyDefinition("TenantContainerCN", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
