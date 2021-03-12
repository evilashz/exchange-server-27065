using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Hygiene.Data.Sync;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000DF RID: 223
	internal class FfoTenantSchema : ADObjectSchema
	{
		// Token: 0x0400047E RID: 1150
		public static readonly HygienePropertyDefinition DescriptionProperty = new HygienePropertyDefinition("Description", typeof(string));

		// Token: 0x0400047F RID: 1151
		public static readonly HygienePropertyDefinition CompanyPartnershipProperty = new HygienePropertyDefinition("CompanyPartnership", typeof(string));

		// Token: 0x04000480 RID: 1152
		public static readonly HygienePropertyDefinition ResellerTypeProperty = new HygienePropertyDefinition("ResellerType", typeof(ResellerType), ResellerType.None, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000481 RID: 1153
		public static readonly HygienePropertyDefinition ServiceInstanceProp = CommonSyncProperties.ServiceInstanceProp;

		// Token: 0x04000482 RID: 1154
		public static readonly HygienePropertyDefinition BatchIdProp = CommonSyncProperties.BatchIdProp;

		// Token: 0x04000483 RID: 1155
		public static readonly HygienePropertyDefinition SyncTypeProp = CommonSyncProperties.SyncTypeProp;

		// Token: 0x04000484 RID: 1156
		public static readonly HygienePropertyDefinition VerifiedDomainsProp = new HygienePropertyDefinition("verifiedDomains", typeof(object));

		// Token: 0x04000485 RID: 1157
		public static readonly HygienePropertyDefinition AssignedPlansProp = new HygienePropertyDefinition("assignedPlans", typeof(object));

		// Token: 0x04000486 RID: 1158
		public static readonly HygienePropertyDefinition CompanyTagsProp = new HygienePropertyDefinition("CompanyTags", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x04000487 RID: 1159
		public static readonly HygienePropertyDefinition RmsoUpgradeStatusProp = new HygienePropertyDefinition("RmsoUpgradeStatus", typeof(RmsoUpgradeStatus), RmsoUpgradeStatus.None, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000488 RID: 1160
		public static readonly HygienePropertyDefinition SharepointTenantAdminUrl = new HygienePropertyDefinition("SharepointTenantAdminUrl", typeof(string));

		// Token: 0x04000489 RID: 1161
		public static readonly HygienePropertyDefinition SharepointRootSiteUrl = new HygienePropertyDefinition("SharepointRootSiteUrl", typeof(string));

		// Token: 0x0400048A RID: 1162
		public static readonly HygienePropertyDefinition OdmsEndpointUrl = new HygienePropertyDefinition("OdmsEndpointUrl", typeof(string));

		// Token: 0x0400048B RID: 1163
		public static readonly HygienePropertyDefinition MigratedToProp = new HygienePropertyDefinition("MigratedTo", typeof(string));

		// Token: 0x0400048C RID: 1164
		public static readonly HygienePropertyDefinition UnifiedPolicyPreReqState = new HygienePropertyDefinition("UnifiedPolicyPreReqState", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x0400048D RID: 1165
		public static readonly HygienePropertyDefinition OrganizationStatusProp = new HygienePropertyDefinition("OrganizationStatus", typeof(OrganizationStatus), OrganizationStatus.Active, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400048E RID: 1166
		public static readonly ADPropertyDefinition C = ADOrgPersonSchema.C;

		// Token: 0x0400048F RID: 1167
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x04000490 RID: 1168
		public static readonly ADPropertyDefinition IsDirSyncRunning = OrganizationSchema.IsDirSyncRunning;

		// Token: 0x04000491 RID: 1169
		public static readonly ADPropertyDefinition DirSyncStatus = OrganizationSchema.DirSyncStatus;

		// Token: 0x04000492 RID: 1170
		public static readonly ADPropertyDefinition DirSyncStatusAck = ExtendedOrganizationalUnitSchema.DirSyncStatusAck;
	}
}
