using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Sync.CookieManager;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200036C RID: 876
	internal class MsoTenantSyncRequestSchema : SimpleProviderObjectSchema
	{
		// Token: 0x04001935 RID: 6453
		public static readonly SimpleProviderPropertyDefinition TenantSyncType = new SimpleProviderPropertyDefinition("TenantSyncType", ExchangeObjectVersion.Exchange2010, typeof(TenantSyncType), PropertyDefinitionFlags.PersistDefaultValue, Microsoft.Exchange.Data.Directory.Sync.CookieManager.TenantSyncType.Full, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001936 RID: 6454
		public static readonly SimpleProviderPropertyDefinition Requestor = new SimpleProviderPropertyDefinition("Requestor", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001937 RID: 6455
		public static readonly SimpleProviderPropertyDefinition WhenCreated = new SimpleProviderPropertyDefinition("WhenCreated", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001938 RID: 6456
		public static readonly SimpleProviderPropertyDefinition WhenSyncStarted = new SimpleProviderPropertyDefinition("WhenSyncStarted", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04001939 RID: 6457
		public static readonly SimpleProviderPropertyDefinition WhenLastRecipientCookieCommitted = new SimpleProviderPropertyDefinition("WhenLastRecipientCookieCommitted", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400193A RID: 6458
		public static readonly SimpleProviderPropertyDefinition WhenLastCompanyCookieCommitted = new SimpleProviderPropertyDefinition("WhenLastCompanyCookieCommitted", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400193B RID: 6459
		public static readonly SimpleProviderPropertyDefinition ExternalDirectoryOrganizationId = new SimpleProviderPropertyDefinition("ExternalDirectoryOrganizationId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400193C RID: 6460
		public static readonly SimpleProviderPropertyDefinition ServiceInstanceId = new SimpleProviderPropertyDefinition("ServiceInstanceId", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
