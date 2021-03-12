using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000836 RID: 2102
	internal class SyncSubscribedPlanSchema : SyncObjectSchema
	{
		// Token: 0x170024DD RID: 9437
		// (get) Token: 0x0600682C RID: 26668 RVA: 0x0016F07C File Offset: 0x0016D27C
		public override DirectoryObjectClass DirectoryObjectClass
		{
			get
			{
				return DirectoryObjectClass.SubscribedPlan;
			}
		}

		// Token: 0x04004490 RID: 17552
		public static SyncPropertyDefinition AccountId = new SyncPropertyDefinition("AccountId", "AccountId", typeof(string), typeof(DirectoryPropertyGuidSingle), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, string.Empty);

		// Token: 0x04004491 RID: 17553
		public static SyncPropertyDefinition Capability = new SyncPropertyDefinition("Capability", "Capability", typeof(string), typeof(DirectoryPropertyXmlAnySingle), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, string.Empty);

		// Token: 0x04004492 RID: 17554
		public static SyncPropertyDefinition ServiceType = new SyncPropertyDefinition("ServiceType", "ServiceType", typeof(string), typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, string.Empty);

		// Token: 0x04004493 RID: 17555
		public static SyncPropertyDefinition MaximumOverageUnitsDetail = new SyncPropertyDefinition("MaximumOverageUnitsDetail", "MaximumOverageUnitsDetail", typeof(string), typeof(DirectoryPropertyXmlLicenseUnitsDetailSingle), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, string.Empty);

		// Token: 0x04004494 RID: 17556
		public static SyncPropertyDefinition PrepaidUnitsDetail = new SyncPropertyDefinition("PrepaidUnitsDetail", "PrepaidUnitsDetail", typeof(string), typeof(DirectoryPropertyXmlLicenseUnitsDetailSingle), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, string.Empty);

		// Token: 0x04004495 RID: 17557
		public static SyncPropertyDefinition TotalTrialUnitsDetail = new SyncPropertyDefinition("TotalTrialUnitsDetail", "TotalTrialUnitsDetail", typeof(string), typeof(DirectoryPropertyXmlLicenseUnitsDetailSingle), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, string.Empty);
	}
}
