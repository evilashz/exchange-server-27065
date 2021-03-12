using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x0200081E RID: 2078
	internal class SyncCompanySchema : SyncObjectSchema
	{
		// Token: 0x17002437 RID: 9271
		// (get) Token: 0x060066AD RID: 26285 RVA: 0x0016B5EF File Offset: 0x001697EF
		public override DirectoryObjectClass DirectoryObjectClass
		{
			get
			{
				return DirectoryObjectClass.Company;
			}
		}

		// Token: 0x040043C7 RID: 17351
		public static SyncPropertyDefinition DisplayName = new SyncPropertyDefinition(ADRecipientSchema.DisplayName, "DisplayName", typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043C8 RID: 17352
		public static SyncPropertyDefinition VerifiedDomain = new SyncPropertyDefinition("VerifiedDomain", "VerifiedDomain", typeof(CompanyVerifiedDomainValue), typeof(CompanyVerifiedDomainValue), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x040043C9 RID: 17353
		public static SyncPropertyDefinition AssignedPlan = new SyncPropertyDefinition("AssignedPlan", "AssignedPlan", typeof(AssignedPlanValue), typeof(AssignedPlanValue), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion, null);

		// Token: 0x040043CA RID: 17354
		public static SyncPropertyDefinition C = new SyncPropertyDefinition(ADOrgPersonSchema.C, "C", typeof(DirectoryPropertyStringSingleLength1To128), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043CB RID: 17355
		public static SyncPropertyDefinition IsDirSyncRunning = new SyncPropertyDefinition(OrganizationSchema.IsDirSyncRunning, "DirSyncEnabled", typeof(DirectoryPropertyBooleanSingle), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043CC RID: 17356
		public static SyncPropertyDefinition DirSyncStatus = new SyncPropertyDefinition(OrganizationSchema.DirSyncStatus, "DirSyncStatus", typeof(DirectoryPropertyXmlDirSyncStatus), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043CD RID: 17357
		public static SyncPropertyDefinition DirSyncStatusAck = new SyncPropertyDefinition(ExtendedOrganizationalUnitSchema.DirSyncStatusAck, "DirSyncStatusAck", typeof(DirectoryPropertyXmlDirSyncStatus), SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043CE RID: 17358
		public static SyncPropertyDefinition RelocationInProgress = new SyncPropertyDefinition(ADOrganizationalUnitSchema.RelocationInProgress, null, typeof(bool), SyncPropertyDefinitionFlags.BackSync, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x040043CF RID: 17359
		public static SyncPropertyDefinition Description = new SyncPropertyDefinition("Description", "Description", typeof(string), typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.SyncPropertySetVersion4, string.Empty);

		// Token: 0x040043D0 RID: 17360
		public static SyncPropertyDefinition TenantType = new SyncPropertyDefinition("TenantType", "TenantType", typeof(int?), typeof(DirectoryPropertyInt32Single), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.SyncPropertySetVersion4, null);

		// Token: 0x040043D1 RID: 17361
		public static SyncPropertyDefinition RightsManagementTenantConfiguration = new SyncPropertyDefinition("RightsManagementTenantConfiguration", "RightsManagementTenantConfiguration", typeof(RightsManagementTenantConfigurationValue), typeof(XmlValueRightsManagementTenantConfiguration), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion11, null);

		// Token: 0x040043D2 RID: 17362
		public static SyncPropertyDefinition RightsManagementTenantKey = new SyncPropertyDefinition("RightsManagementTenantKey", "RightsManagementTenantKey", typeof(RightsManagementTenantKeyValue), typeof(RightsManagementTenantKeyValue), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion11, null);

		// Token: 0x040043D3 RID: 17363
		public static SyncPropertyDefinition ServiceInfo = new SyncPropertyDefinition("ServiceInfo", "ServiceInfo", typeof(ServiceInfoValue), typeof(ServiceInfoValue), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion13, null);

		// Token: 0x040043D4 RID: 17364
		public static SyncPropertyDefinition CompanyPartnership = new SyncPropertyDefinition("CompanyPartnership", "CompanyPartnership", typeof(string), typeof(DirectoryPropertyXmlCompanyPartnershipSingle), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.FilteringOnly, SyncPropertyDefinition.SyncPropertySetVersion4, string.Empty);

		// Token: 0x040043D5 RID: 17365
		public static SyncPropertyDefinition QuotaAmount = new SyncPropertyDefinition("QuotaAmount", "QuotaAmount", typeof(int), typeof(DirectoryPropertyInt32SingleMin0), SyncPropertyDefinitionFlags.ForwardSync, SyncPropertyDefinition.SyncPropertySetVersion6, -1);

		// Token: 0x040043D6 RID: 17366
		public static SyncPropertyDefinition CompanyTags = new SyncPropertyDefinition("CompanyTags", "CompanyTags", typeof(string), typeof(DirectoryPropertyStringLength1To256), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.SyncPropertySetVersion10, null);

		// Token: 0x040043D7 RID: 17367
		public static SyncPropertyDefinition PersistedCapabilities = new SyncPropertyDefinition("PersistedCapabilities", null, typeof(Capability), typeof(Capability), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.MultiValued | SyncPropertyDefinitionFlags.Calculated | SyncPropertyDefinitionFlags.ReadOnly, SyncPropertyDefinition.InitialSyncPropertySetVersion, null, new ProviderPropertyDefinition[]
		{
			SyncCompanySchema.AssignedPlan
		}, new GetterDelegate(SyncCompany.PersistedCapabilityGetter), null);
	}
}
