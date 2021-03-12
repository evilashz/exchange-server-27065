using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring
{
	// Token: 0x020004FB RID: 1275
	internal class MonitoringOverrideSchema : SimpleProviderObjectSchema
	{
		// Token: 0x040020CA RID: 8394
		public static readonly SimpleProviderPropertyDefinition ItemType = new SimpleProviderPropertyDefinition("ItemType", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020CB RID: 8395
		public static readonly SimpleProviderPropertyDefinition PropertyName = new SimpleProviderPropertyDefinition("PropertyName", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020CC RID: 8396
		public static readonly SimpleProviderPropertyDefinition PropertyValue = new SimpleProviderPropertyDefinition("PropertyValue", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020CD RID: 8397
		public static readonly SimpleProviderPropertyDefinition HealthSetName = new SimpleProviderPropertyDefinition("HealthSetName", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020CE RID: 8398
		public static readonly SimpleProviderPropertyDefinition MonitoringItemName = new SimpleProviderPropertyDefinition("MonitoringItemName", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020CF RID: 8399
		public static readonly SimpleProviderPropertyDefinition TargetResource = new SimpleProviderPropertyDefinition("TargetResource", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020D0 RID: 8400
		public static readonly SimpleProviderPropertyDefinition ExpirationTime = new SimpleProviderPropertyDefinition("ExpirationTime", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020D1 RID: 8401
		public static readonly SimpleProviderPropertyDefinition ApplyVersion = new SimpleProviderPropertyDefinition("ApplyVersion", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020D2 RID: 8402
		public static readonly SimpleProviderPropertyDefinition CreatedBy = new SimpleProviderPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040020D3 RID: 8403
		public static readonly SimpleProviderPropertyDefinition CreatedTime = new SimpleProviderPropertyDefinition("CreatedTime", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
