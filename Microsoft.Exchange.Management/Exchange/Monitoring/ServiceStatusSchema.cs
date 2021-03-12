using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005C0 RID: 1472
	internal class ServiceStatusSchema : ObjectSchema
	{
		// Token: 0x040023DE RID: 9182
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(ObjectId), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023DF RID: 9183
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023E0 RID: 9184
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023E1 RID: 9185
		public static readonly SimpleProviderPropertyDefinition MaintenanceWindowDays = new SimpleProviderPropertyDefinition("MaintenanceWindowDays", ExchangeObjectVersion.Exchange2010, typeof(uint), PropertyDefinitionFlags.PersistDefaultValue, 0U, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
