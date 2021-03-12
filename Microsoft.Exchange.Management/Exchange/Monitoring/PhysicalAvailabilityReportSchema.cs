using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005B5 RID: 1461
	internal class PhysicalAvailabilityReportSchema : ObjectSchema
	{
		// Token: 0x040023BB RID: 9147
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(ObjectId), PropertyDefinitionFlags.ReadOnly, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023BC RID: 9148
		public static readonly SimpleProviderPropertyDefinition ObjectState = new SimpleProviderPropertyDefinition("ObjectState", ExchangeObjectVersion.Exchange2010, typeof(ObjectState), PropertyDefinitionFlags.ReadOnly, Microsoft.Exchange.Data.ObjectState.New, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023BD RID: 9149
		public static readonly SimpleProviderPropertyDefinition ExchangeVersion = new SimpleProviderPropertyDefinition("ExchangeVersion", ExchangeObjectVersion.Exchange2010, typeof(ExchangeObjectVersion), PropertyDefinitionFlags.ReadOnly, ExchangeObjectVersion.Exchange2010, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023BE RID: 9150
		public static readonly SimpleProviderPropertyDefinition StartDate = new SimpleProviderPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023BF RID: 9151
		public static readonly SimpleProviderPropertyDefinition EndDate = new SimpleProviderPropertyDefinition("EndDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime), PropertyDefinitionFlags.PersistDefaultValue, DateTime.MaxValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023C0 RID: 9152
		public static readonly SimpleProviderPropertyDefinition AvailabilityPercentage = new SimpleProviderPropertyDefinition("AvailabilityPercentage", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023C1 RID: 9153
		public static readonly SimpleProviderPropertyDefinition RawAvailabilityPercentage = new SimpleProviderPropertyDefinition("RawAvailabilityPercentage", ExchangeObjectVersion.Exchange2010, typeof(double), PropertyDefinitionFlags.None, 0.0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023C2 RID: 9154
		public static readonly SimpleProviderPropertyDefinition SiteName = new SimpleProviderPropertyDefinition("SiteName", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023C3 RID: 9155
		public static readonly SimpleProviderPropertyDefinition Database = new SimpleProviderPropertyDefinition("Database", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040023C4 RID: 9156
		public static readonly SimpleProviderPropertyDefinition ExchangeServer = new SimpleProviderPropertyDefinition("ExchangeServer", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
