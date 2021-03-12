using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000125 RID: 293
	public sealed class VariantConfigurationUMComponent : VariantConfigurationComponent
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002126C File Offset: 0x0001F46C
		internal VariantConfigurationUMComponent() : base("UM")
		{
			base.Add(new VariantConfigurationSection("UM.settings.ini", "UMDataCenterLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "VoicemailDiskSpaceDatacenterLimit", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "DatacenterUMGrammarTenantCache", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "DirectoryGrammarCountLimit", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "UMDataCenterAD", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "AddressListGrammars", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "GetServerDialPlans", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "UMDataCenterLanguages", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "UMDataCenterCallRouting", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "HuntGroupCreationForSipDialplans", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "DTMFMapGenerator", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "AlwaysLogTraces", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "AnonymizeLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "ServerDialPlanLink", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("UM.settings.ini", "SipInfoNotifications", typeof(IFeature), false));
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00021464 File Offset: 0x0001F664
		public VariantConfigurationSection UMDataCenterLogging
		{
			get
			{
				return base["UMDataCenterLogging"];
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00021471 File Offset: 0x0001F671
		public VariantConfigurationSection VoicemailDiskSpaceDatacenterLimit
		{
			get
			{
				return base["VoicemailDiskSpaceDatacenterLimit"];
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0002147E File Offset: 0x0001F67E
		public VariantConfigurationSection DatacenterUMGrammarTenantCache
		{
			get
			{
				return base["DatacenterUMGrammarTenantCache"];
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0002148B File Offset: 0x0001F68B
		public VariantConfigurationSection DirectoryGrammarCountLimit
		{
			get
			{
				return base["DirectoryGrammarCountLimit"];
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00021498 File Offset: 0x0001F698
		public VariantConfigurationSection UMDataCenterAD
		{
			get
			{
				return base["UMDataCenterAD"];
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x000214A5 File Offset: 0x0001F6A5
		public VariantConfigurationSection AddressListGrammars
		{
			get
			{
				return base["AddressListGrammars"];
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x000214B2 File Offset: 0x0001F6B2
		public VariantConfigurationSection GetServerDialPlans
		{
			get
			{
				return base["GetServerDialPlans"];
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x000214BF File Offset: 0x0001F6BF
		public VariantConfigurationSection UMDataCenterLanguages
		{
			get
			{
				return base["UMDataCenterLanguages"];
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000214CC File Offset: 0x0001F6CC
		public VariantConfigurationSection UMDataCenterCallRouting
		{
			get
			{
				return base["UMDataCenterCallRouting"];
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000214D9 File Offset: 0x0001F6D9
		public VariantConfigurationSection HuntGroupCreationForSipDialplans
		{
			get
			{
				return base["HuntGroupCreationForSipDialplans"];
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000214E6 File Offset: 0x0001F6E6
		public VariantConfigurationSection DTMFMapGenerator
		{
			get
			{
				return base["DTMFMapGenerator"];
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000214F3 File Offset: 0x0001F6F3
		public VariantConfigurationSection AlwaysLogTraces
		{
			get
			{
				return base["AlwaysLogTraces"];
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00021500 File Offset: 0x0001F700
		public VariantConfigurationSection AnonymizeLogging
		{
			get
			{
				return base["AnonymizeLogging"];
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0002150D File Offset: 0x0001F70D
		public VariantConfigurationSection ServerDialPlanLink
		{
			get
			{
				return base["ServerDialPlanLink"];
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x0002151A File Offset: 0x0001F71A
		public VariantConfigurationSection SipInfoNotifications
		{
			get
			{
				return base["SipInfoNotifications"];
			}
		}
	}
}
