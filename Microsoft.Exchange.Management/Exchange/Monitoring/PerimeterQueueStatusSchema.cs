using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B2 RID: 1458
	internal class PerimeterQueueStatusSchema : ObjectSchema
	{
		// Token: 0x040023B5 RID: 9141
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(ObjectId), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023B6 RID: 9142
		public static readonly SimpleProviderPropertyDefinition MessageCount = new SimpleProviderPropertyDefinition("MessageCount", ExchangeObjectVersion.Exchange2010, typeof(int), PropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023B7 RID: 9143
		public static readonly SimpleProviderPropertyDefinition Status = new SimpleProviderPropertyDefinition("Status", ExchangeObjectVersion.Exchange2010, typeof(PerimeterQueueStatus), PropertyDefinitionFlags.WriteOnce, PerimeterQueueStatus.Normal, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023B8 RID: 9144
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023B9 RID: 9145
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
