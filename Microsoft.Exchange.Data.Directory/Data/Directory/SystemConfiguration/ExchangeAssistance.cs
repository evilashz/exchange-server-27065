using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000443 RID: 1091
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ExchangeAssistance : ADConfigurationObject
	{
		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000C6A5A File Offset: 0x000C4C5A
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeAssistance.schema;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x000C6A61 File Offset: 0x000C4C61
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000C6A68 File Offset: 0x000C4C68
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x000C6A6B File Offset: 0x000C4C6B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeAssistance.mostDerivedClass;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000C6A72 File Offset: 0x000C4C72
		// (set) Token: 0x0600314F RID: 12623 RVA: 0x000C6A84 File Offset: 0x000C4C84
		[Parameter]
		public bool ExchangeHelpAppOnline
		{
			get
			{
				return (bool)this[ExchangeAssistanceSchema.ExchangeHelpAppOnline];
			}
			set
			{
				this[ExchangeAssistanceSchema.ExchangeHelpAppOnline] = value;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000C6A97 File Offset: 0x000C4C97
		// (set) Token: 0x06003151 RID: 12625 RVA: 0x000C6AA9 File Offset: 0x000C4CA9
		[Parameter]
		public Uri ControlPanelHelpURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.ControlPanelHelpURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.ControlPanelHelpURL] = value;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x000C6AB7 File Offset: 0x000C4CB7
		// (set) Token: 0x06003153 RID: 12627 RVA: 0x000C6AC9 File Offset: 0x000C4CC9
		[Parameter]
		public Uri ControlPanelFeedbackURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.ControlPanelFeedbackURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.ControlPanelFeedbackURL] = value;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003154 RID: 12628 RVA: 0x000C6AD7 File Offset: 0x000C4CD7
		// (set) Token: 0x06003155 RID: 12629 RVA: 0x000C6AE9 File Offset: 0x000C4CE9
		[Parameter]
		public bool ControlPanelFeedbackEnabled
		{
			get
			{
				return (bool)this[ExchangeAssistanceSchema.ControlPanelFeedbackEnabled];
			}
			set
			{
				this[ExchangeAssistanceSchema.ControlPanelFeedbackEnabled] = value;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000C6AFC File Offset: 0x000C4CFC
		// (set) Token: 0x06003157 RID: 12631 RVA: 0x000C6B0E File Offset: 0x000C4D0E
		[Parameter]
		public Uri ManagementConsoleHelpURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.ManagementConsoleHelpURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.ManagementConsoleHelpURL] = value;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000C6B1C File Offset: 0x000C4D1C
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x000C6B2E File Offset: 0x000C4D2E
		[Parameter]
		public Uri ManagementConsoleFeedbackURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.ManagementConsoleFeedbackURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.ManagementConsoleFeedbackURL] = value;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x000C6B3C File Offset: 0x000C4D3C
		// (set) Token: 0x0600315B RID: 12635 RVA: 0x000C6B4E File Offset: 0x000C4D4E
		[Parameter]
		public bool ManagementConsoleFeedbackEnabled
		{
			get
			{
				return (bool)this[ExchangeAssistanceSchema.ManagementConsoleFeedbackEnabled];
			}
			set
			{
				this[ExchangeAssistanceSchema.ManagementConsoleFeedbackEnabled] = value;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000C6B61 File Offset: 0x000C4D61
		// (set) Token: 0x0600315D RID: 12637 RVA: 0x000C6B73 File Offset: 0x000C4D73
		[Parameter]
		public Uri OWAHelpURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.OWAHelpURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.OWAHelpURL] = value;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000C6B81 File Offset: 0x000C4D81
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x000C6B93 File Offset: 0x000C4D93
		[Parameter]
		public Uri OWAFeedbackURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.OWAFeedbackURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.OWAFeedbackURL] = value;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000C6BA1 File Offset: 0x000C4DA1
		// (set) Token: 0x06003161 RID: 12641 RVA: 0x000C6BB3 File Offset: 0x000C4DB3
		[Parameter]
		public bool OWAFeedbackEnabled
		{
			get
			{
				return (bool)this[ExchangeAssistanceSchema.OWAFeedbackEnabled];
			}
			set
			{
				this[ExchangeAssistanceSchema.OWAFeedbackEnabled] = value;
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000C6BC6 File Offset: 0x000C4DC6
		// (set) Token: 0x06003163 RID: 12643 RVA: 0x000C6BD8 File Offset: 0x000C4DD8
		[Parameter]
		public Uri OWALightHelpURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.OWALightHelpURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.OWALightHelpURL] = value;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000C6BE6 File Offset: 0x000C4DE6
		// (set) Token: 0x06003165 RID: 12645 RVA: 0x000C6BF8 File Offset: 0x000C4DF8
		[Parameter]
		public Uri OWALightFeedbackURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.OWALightFeedbackURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.OWALightFeedbackURL] = value;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000C6C06 File Offset: 0x000C4E06
		// (set) Token: 0x06003167 RID: 12647 RVA: 0x000C6C18 File Offset: 0x000C4E18
		[Parameter]
		public bool OWALightFeedbackEnabled
		{
			get
			{
				return (bool)this[ExchangeAssistanceSchema.OWALightFeedbackEnabled];
			}
			set
			{
				this[ExchangeAssistanceSchema.OWALightFeedbackEnabled] = value;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000C6C2B File Offset: 0x000C4E2B
		// (set) Token: 0x06003169 RID: 12649 RVA: 0x000C6C3D File Offset: 0x000C4E3D
		[Parameter]
		public Uri WindowsLiveAccountPageURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.WindowsLiveAccountPageURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.WindowsLiveAccountPageURL] = value;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000C6C4B File Offset: 0x000C4E4B
		// (set) Token: 0x0600316B RID: 12651 RVA: 0x000C6C5D File Offset: 0x000C4E5D
		[Parameter]
		public bool WindowsLiveAccountURLEnabled
		{
			get
			{
				return (bool)this[ExchangeAssistanceSchema.WindowsLiveAccountURLEnabled];
			}
			set
			{
				this[ExchangeAssistanceSchema.WindowsLiveAccountURLEnabled] = value;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000C6C70 File Offset: 0x000C4E70
		// (set) Token: 0x0600316D RID: 12653 RVA: 0x000C6C82 File Offset: 0x000C4E82
		[Parameter]
		public Uri PrivacyStatementURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.PrivacyStatementURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.PrivacyStatementURL] = value;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000C6C90 File Offset: 0x000C4E90
		// (set) Token: 0x0600316F RID: 12655 RVA: 0x000C6CA2 File Offset: 0x000C4EA2
		[Parameter]
		public bool PrivacyLinkDisplayEnabled
		{
			get
			{
				return (bool)this[ExchangeAssistanceSchema.PrivacyLinkDisplayEnabled];
			}
			set
			{
				this[ExchangeAssistanceSchema.PrivacyLinkDisplayEnabled] = value;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000C6CB5 File Offset: 0x000C4EB5
		// (set) Token: 0x06003171 RID: 12657 RVA: 0x000C6CC7 File Offset: 0x000C4EC7
		[Parameter]
		public Uri CommunityURL
		{
			get
			{
				return (Uri)this[ExchangeAssistanceSchema.CommunityURL];
			}
			set
			{
				this[ExchangeAssistanceSchema.CommunityURL] = value;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000C6CD5 File Offset: 0x000C4ED5
		// (set) Token: 0x06003173 RID: 12659 RVA: 0x000C6CE7 File Offset: 0x000C4EE7
		[Parameter]
		public bool CommunityLinkDisplayEnabled
		{
			get
			{
				return (bool)this[ExchangeAssistanceSchema.CommunityLinkDisplayEnabled];
			}
			set
			{
				this[ExchangeAssistanceSchema.CommunityLinkDisplayEnabled] = value;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000C6CFA File Offset: 0x000C4EFA
		// (set) Token: 0x06003175 RID: 12661 RVA: 0x000C6D02 File Offset: 0x000C4F02
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x04002139 RID: 8505
		private static ExchangeAssistanceSchema schema = ObjectSchema.GetInstance<ExchangeAssistanceSchema>();

		// Token: 0x0400213A RID: 8506
		private static string mostDerivedClass = "msExchExchangeAssistance";
	}
}
