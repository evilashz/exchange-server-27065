using System;

namespace Microsoft.Exchange.Data.Reporting
{
	// Token: 0x020002B3 RID: 691
	internal class TenantThrottleInfoSchema : ObjectSchema
	{
		// Token: 0x04000EAB RID: 3755
		internal static readonly SimpleProviderPropertyDefinition TenantIdProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("TenantId", typeof(Guid));

		// Token: 0x04000EAC RID: 3756
		internal static readonly SimpleProviderPropertyDefinition TimeStampProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("TimeStamp", typeof(DateTime), DateTime.MinValue, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EAD RID: 3757
		internal static readonly SimpleProviderPropertyDefinition ThrottleStateProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("ThrottleState", typeof(TenantThrottleState));

		// Token: 0x04000EAE RID: 3758
		internal static readonly SimpleProviderPropertyDefinition MessageCountProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("MessageCount", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EAF RID: 3759
		internal static readonly SimpleProviderPropertyDefinition AvgMessageSizeKbProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("AvgMessageSizeKb", typeof(double));

		// Token: 0x04000EB0 RID: 3760
		internal static readonly SimpleProviderPropertyDefinition AvgMessageCostMsProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("AvgMessageCostMs", typeof(double));

		// Token: 0x04000EB1 RID: 3761
		internal static readonly SimpleProviderPropertyDefinition ThrottlingFactorProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("ThrottleFactor", typeof(double));

		// Token: 0x04000EB2 RID: 3762
		internal static readonly SimpleProviderPropertyDefinition PartitionTenantCountProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("PartitionTenantCount", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EB3 RID: 3763
		internal static readonly SimpleProviderPropertyDefinition PartitionMessageCountProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("PartitionMessageCount", typeof(int), 0, PropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000EB4 RID: 3764
		internal static readonly SimpleProviderPropertyDefinition PartitionAvgMessageSizeKbProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("PartitionAvgMessageSizeKb", typeof(double));

		// Token: 0x04000EB5 RID: 3765
		internal static readonly SimpleProviderPropertyDefinition PartitionAvgMessageCostMsProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("PartitionAvgMessageCostMs", typeof(double));

		// Token: 0x04000EB6 RID: 3766
		internal static readonly SimpleProviderPropertyDefinition StandardDeviationProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("StandardDeviation", typeof(double));

		// Token: 0x04000EB7 RID: 3767
		internal static readonly SimpleProviderPropertyDefinition OverriddenOnlyProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("OverriddenOnly", typeof(bool?));

		// Token: 0x04000EB8 RID: 3768
		internal static readonly SimpleProviderPropertyDefinition ThrottledOnlyProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("ThrottledOnly", typeof(bool?));

		// Token: 0x04000EB9 RID: 3769
		internal static readonly SimpleProviderPropertyDefinition DataCountProperty = PropertyDefinitionsHelper.CreatePropertyDefinition("DataCount", typeof(int?));
	}
}
