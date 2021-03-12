using System;
using System.DirectoryServices;
using System.Security.AccessControl;

namespace Microsoft.Exchange.Data.Directory.Permission
{
	// Token: 0x020001D6 RID: 470
	internal class RecipientPermissionSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04000AE0 RID: 2784
		public static readonly SimpleProviderPropertyDefinition Trustee = new SimpleProviderPropertyDefinition("Trustee", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000AE1 RID: 2785
		public static readonly SimpleProviderPropertyDefinition AccessControlType = new SimpleProviderPropertyDefinition("AccessControlType", ExchangeObjectVersion.Exchange2003, typeof(AccessControlType), PropertyDefinitionFlags.None, System.Security.AccessControl.AccessControlType.Allow, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000AE2 RID: 2786
		public static readonly SimpleProviderPropertyDefinition AccessRights = new SimpleProviderPropertyDefinition("AccessRights", ExchangeObjectVersion.Exchange2003, typeof(RecipientAccessRight), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000AE3 RID: 2787
		public static readonly SimpleProviderPropertyDefinition IsInherited = new SimpleProviderPropertyDefinition("IsInherited", ExchangeObjectVersion.Exchange2003, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000AE4 RID: 2788
		public static readonly SimpleProviderPropertyDefinition InheritanceType = new SimpleProviderPropertyDefinition("InheritanceType", ExchangeObjectVersion.Exchange2003, typeof(ActiveDirectorySecurityInheritance), PropertyDefinitionFlags.None, ActiveDirectorySecurityInheritance.All, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
