using System;
using System.DirectoryServices;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000092 RID: 146
	internal class AcePresentationObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040001FA RID: 506
		public static readonly SimpleProviderPropertyDefinition IsInherited = new SimpleProviderPropertyDefinition("IsInherited", ExchangeObjectVersion.Exchange2003, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001FB RID: 507
		public static readonly SimpleProviderPropertyDefinition User = new SimpleProviderPropertyDefinition("User", ExchangeObjectVersion.Exchange2003, typeof(SecurityPrincipalIdParameter), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001FC RID: 508
		public static readonly SimpleProviderPropertyDefinition InheritanceType = new SimpleProviderPropertyDefinition("InheritanceType", ExchangeObjectVersion.Exchange2003, typeof(ActiveDirectorySecurityInheritance), PropertyDefinitionFlags.None, ActiveDirectorySecurityInheritance.All, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040001FD RID: 509
		public static readonly SimpleProviderPropertyDefinition Deny = new SimpleProviderPropertyDefinition("Deny", ExchangeObjectVersion.Exchange2003, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
