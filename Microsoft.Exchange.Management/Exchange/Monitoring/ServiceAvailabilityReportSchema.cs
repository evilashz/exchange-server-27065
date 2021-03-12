using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005BD RID: 1469
	internal class ServiceAvailabilityReportSchema : ObjectSchema
	{
		// Token: 0x040023D6 RID: 9174
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(ObjectId), PropertyDefinitionFlags.WriteOnce, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023D7 RID: 9175
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023D8 RID: 9176
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023D9 RID: 9177
		public static readonly SimpleProviderPropertyDefinition StartDate = new SimpleProviderPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023DA RID: 9178
		public static readonly SimpleProviderPropertyDefinition EndDate = new SimpleProviderPropertyDefinition("EndDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MaxValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023DB RID: 9179
		public static readonly SimpleProviderPropertyDefinition AvailabilityPercentage = new SimpleProviderPropertyDefinition("AvailabilityPercentage", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
