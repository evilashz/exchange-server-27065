using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000093 RID: 147
	internal class MailboxAuditLogSearchSchema : AuditLogSearchBaseSchema
	{
		// Token: 0x04000264 RID: 612
		public static readonly ProviderPropertyDefinition MailboxIds = new SimpleProviderPropertyDefinition("MailboxIds", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000265 RID: 613
		public static readonly ProviderPropertyDefinition ShowDetails = new SimpleProviderPropertyDefinition("ShowDetails", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000266 RID: 614
		public static readonly ProviderPropertyDefinition LogonTypeStrings = new SimpleProviderPropertyDefinition("LogonTypes", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000267 RID: 615
		public static readonly ProviderPropertyDefinition Operations = new SimpleProviderPropertyDefinition("Operations", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
