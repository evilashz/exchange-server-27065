using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200040C RID: 1036
	internal sealed class ProvisioningTasksConfigSchema : ConfigSchemaBase
	{
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000BE54E File Offset: 0x000BC74E
		public override string Name
		{
			get
			{
				return "ProvisioningTasks";
			}
		}

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000BE555 File Offset: 0x000BC755
		public override string SectionName
		{
			get
			{
				return "GlobalConfiguration";
			}
		}

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000BE55C File Offset: 0x000BC75C
		// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x000BE56E File Offset: 0x000BC76E
		[ConfigurationProperty("IsOrganizationSoftDeletionEnabled", DefaultValue = false)]
		public bool IsOrganizationSoftDeletionEnabled
		{
			get
			{
				return (bool)base["IsOrganizationSoftDeletionEnabled"];
			}
			set
			{
				base["IsOrganizationSoftDeletionEnabled"] = value;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000BE581 File Offset: 0x000BC781
		// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x000BE593 File Offset: 0x000BC793
		[ConfigurationProperty("IsFailedOrganizationCleanupEnabled", DefaultValue = false)]
		public bool IsFailedOrganizationCleanupEnabled
		{
			get
			{
				return (bool)base["IsFailedOrganizationCleanupEnabled"];
			}
			set
			{
				base["IsFailedOrganizationCleanupEnabled"] = value;
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000BE5A6 File Offset: 0x000BC7A6
		// (set) Token: 0x06002EFB RID: 12027 RVA: 0x000BE5B8 File Offset: 0x000BC7B8
		[ConfigurationProperty("UseBecAPIsforLiveId", DefaultValue = false)]
		public bool UseBecAPIsforLiveId
		{
			get
			{
				return (bool)base["UseBecAPIsforLiveId"];
			}
			set
			{
				base["UseBecAPIsforLiveId"] = value;
			}
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000BE5CB File Offset: 0x000BC7CB
		// (set) Token: 0x06002EFD RID: 12029 RVA: 0x000BE5DD File Offset: 0x000BC7DD
		[ConfigurationProperty("MaxObjectFullSyncRequestsPerServiceInstance", DefaultValue = 200)]
		public int MaxObjectFullSyncRequestsPerServiceInstance
		{
			get
			{
				return (int)base["MaxObjectFullSyncRequestsPerServiceInstance"];
			}
			set
			{
				base["MaxObjectFullSyncRequestsPerServiceInstance"] = value;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000BE5F0 File Offset: 0x000BC7F0
		// (set) Token: 0x06002EFF RID: 12031 RVA: 0x000BE602 File Offset: 0x000BC802
		[ConfigurationProperty("EnableAutomatedCleaningOfCnfRbacContainer", DefaultValue = false)]
		public bool EnableAutomatedCleaningOfCnfRbacContainer
		{
			get
			{
				return (bool)base["EnableAutomatedCleaningOfCnfRbacContainer"];
			}
			set
			{
				base["EnableAutomatedCleaningOfCnfRbacContainer"] = value;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x000BE615 File Offset: 0x000BC815
		// (set) Token: 0x06002F01 RID: 12033 RVA: 0x000BE627 File Offset: 0x000BC827
		[ConfigurationProperty("EnableAutomatedCleaningOfCnfSoftDeletedContainer", DefaultValue = false)]
		public bool EnableAutomatedCleaningOfCnfSoftDeletedContainer
		{
			get
			{
				return (bool)base["EnableAutomatedCleaningOfCnfSoftDeletedContainer"];
			}
			set
			{
				base["EnableAutomatedCleaningOfCnfSoftDeletedContainer"] = value;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x000BE63A File Offset: 0x000BC83A
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x000BE64C File Offset: 0x000BC84C
		[ConfigurationProperty("EnableAutomatedCleaningOfCnfProvisioningPolicyContainer", DefaultValue = false)]
		public bool EnableAutomatedCleaningOfCnfProvisioningPolicyContainer
		{
			get
			{
				return (bool)base["EnableAutomatedCleaningOfCnfProvisioningPolicyContainer"];
			}
			set
			{
				base["EnableAutomatedCleaningOfCnfProvisioningPolicyContainer"] = value;
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000BE65F File Offset: 0x000BC85F
		// (set) Token: 0x06002F05 RID: 12037 RVA: 0x000BE671 File Offset: 0x000BC871
		[ConfigurationProperty("EnablePowershellBasedDivergenceProcessor", DefaultValue = false)]
		public bool EnablePowershellBasedDivergenceProcessor
		{
			get
			{
				return (bool)base["EnablePowershellBasedDivergenceProcessor"];
			}
			set
			{
				base["EnablePowershellBasedDivergenceProcessor"] = value;
			}
		}

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000BE684 File Offset: 0x000BC884
		// (set) Token: 0x06002F07 RID: 12039 RVA: 0x000BE696 File Offset: 0x000BC896
		[ConfigurationProperty("EnableProcessingMissingLinksInGroupDivergences", DefaultValue = false)]
		public bool EnableProcessingMissingLinksInGroupDivergences
		{
			get
			{
				return (bool)base["EnableProcessingMissingLinksInGroupDivergences"];
			}
			set
			{
				base["EnableProcessingMissingLinksInGroupDivergences"] = value;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000BE6A9 File Offset: 0x000BC8A9
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x000BE6BB File Offset: 0x000BC8BB
		[ConfigurationProperty("EnableProcessingValidationDivergences", DefaultValue = false)]
		public bool EnableProcessingValidationDivergences
		{
			get
			{
				return (bool)base["EnableProcessingValidationDivergences"];
			}
			set
			{
				base["EnableProcessingValidationDivergences"] = value;
			}
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000BE6CE File Offset: 0x000BC8CE
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ExTraceGlobals.DirectoryTasksTracer.TraceDebug<string, string>(0L, "Unrecognized configuration attribute {0}={1}", name, value);
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x0200040D RID: 1037
		internal static class Setting
		{
			// Token: 0x04001F8A RID: 8074
			public const string IsOrganizationSoftDeletionEnabled = "IsOrganizationSoftDeletionEnabled";

			// Token: 0x04001F8B RID: 8075
			public const string IsFailedOrganizationCleanupEnabled = "IsFailedOrganizationCleanupEnabled";

			// Token: 0x04001F8C RID: 8076
			public const string UseBecAPIsforLiveId = "UseBecAPIsforLiveId";

			// Token: 0x04001F8D RID: 8077
			public const string MaxObjectFullSyncRequestsPerServiceInstance = "MaxObjectFullSyncRequestsPerServiceInstance";

			// Token: 0x04001F8E RID: 8078
			public const string EnableAutomatedCleaningOfCnfRbacContainer = "EnableAutomatedCleaningOfCnfRbacContainer";

			// Token: 0x04001F8F RID: 8079
			public const string EnableAutomatedCleaningOfCnfSoftDeletedContainer = "EnableAutomatedCleaningOfCnfSoftDeletedContainer";

			// Token: 0x04001F90 RID: 8080
			public const string EnableAutomatedCleaningOfCnfProvisioningPolicyContainer = "EnableAutomatedCleaningOfCnfProvisioningPolicyContainer";

			// Token: 0x04001F91 RID: 8081
			public const string EnablePowershellBasedDivergenceProcessor = "EnablePowershellBasedDivergenceProcessor";

			// Token: 0x04001F92 RID: 8082
			public const string EnableProcessingMissingLinksInGroupDivergences = "EnableProcessingMissingLinksInGroupDivergences";

			// Token: 0x04001F93 RID: 8083
			public const string EnableProcessingValidationDivergences = "EnableProcessingValidationDivergences";
		}
	}
}
