using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200072C RID: 1836
	internal class MailboxAuditLogRecordSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04003B45 RID: 15173
		public static readonly SimpleProviderPropertyDefinition MailboxGuid = new SimpleProviderPropertyDefinition("MailboxGuid", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B46 RID: 15174
		public static readonly SimpleProviderPropertyDefinition MailboxResolvedOwnerName = new SimpleProviderPropertyDefinition("MailboxResolvedOwnerName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003B47 RID: 15175
		public static readonly SimpleProviderPropertyDefinition LastAccessed = new SimpleProviderPropertyDefinition("LastAccessed", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
