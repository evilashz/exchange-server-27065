using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000094 RID: 148
	internal class AdminAuditLogSearchSchema : AuditLogSearchBaseSchema
	{
		// Token: 0x04000268 RID: 616
		public static readonly ProviderPropertyDefinition Cmdlets = new SimpleProviderPropertyDefinition("Cmdlets", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000269 RID: 617
		public static readonly ProviderPropertyDefinition Parameters = new SimpleProviderPropertyDefinition("Parameters", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400026A RID: 618
		public static readonly ProviderPropertyDefinition ObjectIds = new SimpleProviderPropertyDefinition("ObjectIds", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400026B RID: 619
		public static readonly ProviderPropertyDefinition UserIds = new SimpleProviderPropertyDefinition("UserIds", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400026C RID: 620
		public static readonly ProviderPropertyDefinition ResolvedUsers = new SimpleProviderPropertyDefinition("ResolvedUsers", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400026D RID: 621
		public static readonly ProviderPropertyDefinition RedactDatacenterAdmins = new SimpleProviderPropertyDefinition("RedactDatacenterAdmins", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
