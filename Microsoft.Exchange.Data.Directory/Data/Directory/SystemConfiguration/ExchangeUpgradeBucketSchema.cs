using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002DA RID: 730
	internal sealed class ExchangeUpgradeBucketSchema : ADConfigurationObjectSchema
	{
		// Token: 0x04001559 RID: 5465
		private const int StartUpgradeStatusBitPosition = 0;

		// Token: 0x0400155A RID: 5466
		private const int UpgradeOrganizationMailboxesStatusBitPosition = 2;

		// Token: 0x0400155B RID: 5467
		private const int UpgradeUserMailboxesStatusBitPosition = 4;

		// Token: 0x0400155C RID: 5468
		private const int CompleteUpgradeStatusBitPosition = 6;

		// Token: 0x0400155D RID: 5469
		private const int UpgradeStageStatusBitLength = 2;

		// Token: 0x0400155E RID: 5470
		private const int DisabledUpgradeStagesBitPosition = 8;

		// Token: 0x0400155F RID: 5471
		private const int DisabledUpgradeStagesBitLength = 4;

		// Token: 0x04001560 RID: 5472
		private const string VersionRegex = "^\\d+\\.(\\*|\\d+\\.(\\*|\\d+\\.(\\*|\\d+)))$";

		// Token: 0x04001561 RID: 5473
		internal static readonly ADPropertyDefinition Status = new ADPropertyDefinition("Status", ExchangeObjectVersion.Exchange2010, typeof(ExchangeUpgradeBucketStatus), "msExchOrganizationUpgradePolicyStatus", ADPropertyDefinitionFlags.PersistDefaultValue, ExchangeUpgradeBucketStatus.NotStarted, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ExchangeUpgradeBucketStatus))
		}, null, null);

		// Token: 0x04001562 RID: 5474
		internal static readonly ADPropertyDefinition Enabled = new ADPropertyDefinition("Enabled", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchOrganizationUpgradePolicyEnabled", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001563 RID: 5475
		internal static readonly ADPropertyDefinition StartDate = new ADPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), "msExchOrganizationUpgradePolicyDate", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001564 RID: 5476
		internal static readonly ADPropertyDefinition MaxMailboxes = new ADPropertyDefinition("MaxMailboxes", ExchangeObjectVersion.Exchange2010, typeof(int?), "msExchOrganizationUpgradePolicyMaxMailboxes", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedNullableValueConstraint<int>(1, int.MaxValue)
		}, null, null);

		// Token: 0x04001565 RID: 5477
		internal static readonly ADPropertyDefinition Priority = new ADPropertyDefinition("Priority", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchOrganizationUpgradePolicyPriority", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 100, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(1, 999)
		}, null, null);

		// Token: 0x04001566 RID: 5478
		internal static readonly ADPropertyDefinition Description = new ADPropertyDefinition("Description", ExchangeObjectVersion.Exchange2010, typeof(string), "Description", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001567 RID: 5479
		internal static readonly ADPropertyDefinition RawSourceVersion = new ADPropertyDefinition("RawSourceVersion", ExchangeObjectVersion.Exchange2010, typeof(long), "msExchOrganizationUpgradePolicySourceVersion", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001568 RID: 5480
		internal static readonly ADPropertyDefinition RawTargetVersion = new ADPropertyDefinition("RawTargetVersion", ExchangeObjectVersion.Exchange2010, typeof(long), "msExchOrganizationUpgradePolicyTargetVersion", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04001569 RID: 5481
		internal static readonly ADPropertyDefinition SourceVersion = new ADPropertyDefinition("SourceVersion", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RegexConstraint("^\\d+\\.(\\*|\\d+\\.(\\*|\\d+\\.(\\*|\\d+)))$", DataStrings.BucketVersionPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			ExchangeUpgradeBucketSchema.RawSourceVersion
		}, null, new GetterDelegate(ExchangeUpgradeBucket.SourceVersionGetterDelegate), new SetterDelegate(ExchangeUpgradeBucket.SourceVersionSetterDelegate), null, null);

		// Token: 0x0400156A RID: 5482
		internal static readonly ADPropertyDefinition TargetVersion = new ADPropertyDefinition("TargetVersion", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.Calculated | ADPropertyDefinitionFlags.Mandatory, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new RegexConstraint("^\\d+\\.(\\*|\\d+\\.(\\*|\\d+\\.(\\*|\\d+)))$", DataStrings.BucketVersionPatternDescription)
		}, new ProviderPropertyDefinition[]
		{
			ExchangeUpgradeBucketSchema.RawTargetVersion
		}, null, new GetterDelegate(ExchangeUpgradeBucket.TargetVersionGetterDelegate), new SetterDelegate(ExchangeUpgradeBucket.TargetVersionSetterDelegate), null, null);

		// Token: 0x0400156B RID: 5483
		public static readonly ADPropertyDefinition Organizations = new ADPropertyDefinition("Organizations", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchOrganizationUpgradePolicyBL", ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400156C RID: 5484
		public static readonly ADPropertyDefinition MailboxCount = new ADPropertyDefinition("MailboxCount", ExchangeObjectVersion.Exchange2010, typeof(int), null, ADPropertyDefinitionFlags.PersistDefaultValue | ADPropertyDefinitionFlags.TaskPopulated, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400156D RID: 5485
		public static readonly ADPropertyDefinition DisabledUpgradeStages = ADObject.BitfieldProperty("EnabledUpgradeStage", 8, 4, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x0400156E RID: 5486
		public static readonly ADPropertyDefinition StartUpgradeStatus = ADObject.BitfieldProperty("StartUpgradeStatus", 0, 2, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x0400156F RID: 5487
		public static readonly ADPropertyDefinition UpgradeOrganizationMailboxesStatus = ADObject.BitfieldProperty("UpgradeOrganizationMailboxesStatus", 2, 2, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x04001570 RID: 5488
		public static readonly ADPropertyDefinition UpgradeUserMailboxesStatus = ADObject.BitfieldProperty("UpgradeUserMailboxesStatus", 4, 2, SharedPropertyDefinitions.ProvisioningFlags);

		// Token: 0x04001571 RID: 5489
		public static readonly ADPropertyDefinition CompleteUpgradeStatus = ADObject.BitfieldProperty("CompleteUpgradeStatus", 6, 2, SharedPropertyDefinitions.ProvisioningFlags);
	}
}
