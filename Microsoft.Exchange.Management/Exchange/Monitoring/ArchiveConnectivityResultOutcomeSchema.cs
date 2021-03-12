using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000503 RID: 1283
	internal class ArchiveConnectivityResultOutcomeSchema : ObjectSchema
	{
		// Token: 0x040020F1 RID: 8433
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(ObjectId), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020F2 RID: 8434
		public static readonly SimpleProviderPropertyDefinition UserSmtp = new SimpleProviderPropertyDefinition("MailboxSmtp", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020F3 RID: 8435
		public static readonly SimpleProviderPropertyDefinition PrimaryMRMConfiguration = new SimpleProviderPropertyDefinition("PrimaryMRMConfiguration", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020F4 RID: 8436
		public static readonly SimpleProviderPropertyDefinition PrimaryLastProcessedTime = new SimpleProviderPropertyDefinition("PrimaryLastProcessedTime", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020F5 RID: 8437
		public static readonly SimpleProviderPropertyDefinition ArchiveDatabase = new SimpleProviderPropertyDefinition("ArchiveDatabase", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020F6 RID: 8438
		public static readonly SimpleProviderPropertyDefinition ArchiveDomain = new SimpleProviderPropertyDefinition("ArchiveDomain", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020F7 RID: 8439
		public static readonly SimpleProviderPropertyDefinition Result = new SimpleProviderPropertyDefinition("Result", ExchangeObjectVersion.Exchange2003, typeof(ArchiveConnectivityResult), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020F8 RID: 8440
		public static readonly SimpleProviderPropertyDefinition Error = new SimpleProviderPropertyDefinition("Error", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020F9 RID: 8441
		public static readonly SimpleProviderPropertyDefinition ArchiveMRMConfiguration = new SimpleProviderPropertyDefinition("ArchiveMRMConfiguration", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020FA RID: 8442
		public static readonly SimpleProviderPropertyDefinition ArchiveLastProcessedTime = new SimpleProviderPropertyDefinition("ArchiveLastProcessedTime", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020FB RID: 8443
		public static readonly SimpleProviderPropertyDefinition ComplianceConfiguration = new SimpleProviderPropertyDefinition("ComplianceConfiguration", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020FC RID: 8444
		public static readonly SimpleProviderPropertyDefinition ItemMRMProperties = new SimpleProviderPropertyDefinition("ItemMRMProperties", ExchangeObjectVersion.Exchange2003, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
