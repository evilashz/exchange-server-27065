using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020002B6 RID: 694
	internal class TransportProcessingQuotaConfigSchema : ObjectSchema
	{
		// Token: 0x04000EC0 RID: 3776
		public static readonly SimpleProviderPropertyDefinition Id = PropertyDefinitionsHelper.CreatePropertyDefinition("Id", typeof(Guid));

		// Token: 0x04000EC1 RID: 3777
		public static readonly SimpleProviderPropertyDefinition SettingName = PropertyDefinitionsHelper.CreatePropertyDefinition("RawName", typeof(string));

		// Token: 0x04000EC2 RID: 3778
		public static readonly SimpleProviderPropertyDefinition ThrottlingEnabled = PropertyDefinitionsHelper.CreatePropertyDefinition("ThrottlingEnabled", typeof(bool), true, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EC3 RID: 3779
		public static readonly SimpleProviderPropertyDefinition CalculationEnabled = PropertyDefinitionsHelper.CreatePropertyDefinition("CalculationEnabled", typeof(bool), true, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EC4 RID: 3780
		public static readonly SimpleProviderPropertyDefinition AmWeight = PropertyDefinitionsHelper.CreatePropertyDefinition("AmWeight", typeof(double), 0.0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EC5 RID: 3781
		public static readonly SimpleProviderPropertyDefinition AsWeight = PropertyDefinitionsHelper.CreatePropertyDefinition("AsWeight", typeof(double), 0.0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EC6 RID: 3782
		public static readonly SimpleProviderPropertyDefinition CalculationFrequency = PropertyDefinitionsHelper.CreatePropertyDefinition("CalculationFrequency", typeof(int), 15, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EC7 RID: 3783
		public static readonly SimpleProviderPropertyDefinition CostThreshold = PropertyDefinitionsHelper.CreatePropertyDefinition("CostThreshold", typeof(int), 100, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EC8 RID: 3784
		public static readonly SimpleProviderPropertyDefinition EtrWeight = PropertyDefinitionsHelper.CreatePropertyDefinition("EtrWeight", typeof(double), 1.0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EC9 RID: 3785
		public static readonly SimpleProviderPropertyDefinition TimeWindow = PropertyDefinitionsHelper.CreatePropertyDefinition("TimeWindow", typeof(int), 15, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000ECA RID: 3786
		public static readonly SimpleProviderPropertyDefinition ThrottleFactor = PropertyDefinitionsHelper.CreatePropertyDefinition("ThrottleFactor", typeof(double), 0.01, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000ECB RID: 3787
		public static readonly SimpleProviderPropertyDefinition RelativeCostThreshold = PropertyDefinitionsHelper.CreatePropertyDefinition("RelativeCostThreshold", typeof(double), 5.0, PropertyDefinitionFlags.PersistDefaultValue);
	}
}
