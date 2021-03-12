using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200060F RID: 1551
	public class SetExchangeAssistanceConfigCommand : SyntheticCommandWithPipelineInputNoOutput<ExchangeAssistance>
	{
		// Token: 0x06004F92 RID: 20370 RVA: 0x0007E701 File Offset: 0x0007C901
		private SetExchangeAssistanceConfigCommand() : base("Set-ExchangeAssistanceConfig")
		{
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x0007E70E File Offset: 0x0007C90E
		public SetExchangeAssistanceConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x0007E71D File Offset: 0x0007C91D
		public virtual SetExchangeAssistanceConfigCommand SetParameters(SetExchangeAssistanceConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x0007E727 File Offset: 0x0007C927
		public virtual SetExchangeAssistanceConfigCommand SetParameters(SetExchangeAssistanceConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000610 RID: 1552
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002F2D RID: 12077
			// (set) Token: 0x06004F96 RID: 20374 RVA: 0x0007E731 File Offset: 0x0007C931
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002F2E RID: 12078
			// (set) Token: 0x06004F97 RID: 20375 RVA: 0x0007E74F File Offset: 0x0007C94F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F2F RID: 12079
			// (set) Token: 0x06004F98 RID: 20376 RVA: 0x0007E762 File Offset: 0x0007C962
			public virtual bool ExchangeHelpAppOnline
			{
				set
				{
					base.PowerSharpParameters["ExchangeHelpAppOnline"] = value;
				}
			}

			// Token: 0x17002F30 RID: 12080
			// (set) Token: 0x06004F99 RID: 20377 RVA: 0x0007E77A File Offset: 0x0007C97A
			public virtual Uri ControlPanelHelpURL
			{
				set
				{
					base.PowerSharpParameters["ControlPanelHelpURL"] = value;
				}
			}

			// Token: 0x17002F31 RID: 12081
			// (set) Token: 0x06004F9A RID: 20378 RVA: 0x0007E78D File Offset: 0x0007C98D
			public virtual Uri ControlPanelFeedbackURL
			{
				set
				{
					base.PowerSharpParameters["ControlPanelFeedbackURL"] = value;
				}
			}

			// Token: 0x17002F32 RID: 12082
			// (set) Token: 0x06004F9B RID: 20379 RVA: 0x0007E7A0 File Offset: 0x0007C9A0
			public virtual bool ControlPanelFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["ControlPanelFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17002F33 RID: 12083
			// (set) Token: 0x06004F9C RID: 20380 RVA: 0x0007E7B8 File Offset: 0x0007C9B8
			public virtual Uri ManagementConsoleHelpURL
			{
				set
				{
					base.PowerSharpParameters["ManagementConsoleHelpURL"] = value;
				}
			}

			// Token: 0x17002F34 RID: 12084
			// (set) Token: 0x06004F9D RID: 20381 RVA: 0x0007E7CB File Offset: 0x0007C9CB
			public virtual Uri ManagementConsoleFeedbackURL
			{
				set
				{
					base.PowerSharpParameters["ManagementConsoleFeedbackURL"] = value;
				}
			}

			// Token: 0x17002F35 RID: 12085
			// (set) Token: 0x06004F9E RID: 20382 RVA: 0x0007E7DE File Offset: 0x0007C9DE
			public virtual bool ManagementConsoleFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["ManagementConsoleFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17002F36 RID: 12086
			// (set) Token: 0x06004F9F RID: 20383 RVA: 0x0007E7F6 File Offset: 0x0007C9F6
			public virtual Uri OWAHelpURL
			{
				set
				{
					base.PowerSharpParameters["OWAHelpURL"] = value;
				}
			}

			// Token: 0x17002F37 RID: 12087
			// (set) Token: 0x06004FA0 RID: 20384 RVA: 0x0007E809 File Offset: 0x0007CA09
			public virtual Uri OWAFeedbackURL
			{
				set
				{
					base.PowerSharpParameters["OWAFeedbackURL"] = value;
				}
			}

			// Token: 0x17002F38 RID: 12088
			// (set) Token: 0x06004FA1 RID: 20385 RVA: 0x0007E81C File Offset: 0x0007CA1C
			public virtual bool OWAFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17002F39 RID: 12089
			// (set) Token: 0x06004FA2 RID: 20386 RVA: 0x0007E834 File Offset: 0x0007CA34
			public virtual Uri OWALightHelpURL
			{
				set
				{
					base.PowerSharpParameters["OWALightHelpURL"] = value;
				}
			}

			// Token: 0x17002F3A RID: 12090
			// (set) Token: 0x06004FA3 RID: 20387 RVA: 0x0007E847 File Offset: 0x0007CA47
			public virtual Uri OWALightFeedbackURL
			{
				set
				{
					base.PowerSharpParameters["OWALightFeedbackURL"] = value;
				}
			}

			// Token: 0x17002F3B RID: 12091
			// (set) Token: 0x06004FA4 RID: 20388 RVA: 0x0007E85A File Offset: 0x0007CA5A
			public virtual bool OWALightFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["OWALightFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17002F3C RID: 12092
			// (set) Token: 0x06004FA5 RID: 20389 RVA: 0x0007E872 File Offset: 0x0007CA72
			public virtual Uri WindowsLiveAccountPageURL
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveAccountPageURL"] = value;
				}
			}

			// Token: 0x17002F3D RID: 12093
			// (set) Token: 0x06004FA6 RID: 20390 RVA: 0x0007E885 File Offset: 0x0007CA85
			public virtual bool WindowsLiveAccountURLEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveAccountURLEnabled"] = value;
				}
			}

			// Token: 0x17002F3E RID: 12094
			// (set) Token: 0x06004FA7 RID: 20391 RVA: 0x0007E89D File Offset: 0x0007CA9D
			public virtual Uri PrivacyStatementURL
			{
				set
				{
					base.PowerSharpParameters["PrivacyStatementURL"] = value;
				}
			}

			// Token: 0x17002F3F RID: 12095
			// (set) Token: 0x06004FA8 RID: 20392 RVA: 0x0007E8B0 File Offset: 0x0007CAB0
			public virtual bool PrivacyLinkDisplayEnabled
			{
				set
				{
					base.PowerSharpParameters["PrivacyLinkDisplayEnabled"] = value;
				}
			}

			// Token: 0x17002F40 RID: 12096
			// (set) Token: 0x06004FA9 RID: 20393 RVA: 0x0007E8C8 File Offset: 0x0007CAC8
			public virtual Uri CommunityURL
			{
				set
				{
					base.PowerSharpParameters["CommunityURL"] = value;
				}
			}

			// Token: 0x17002F41 RID: 12097
			// (set) Token: 0x06004FAA RID: 20394 RVA: 0x0007E8DB File Offset: 0x0007CADB
			public virtual bool CommunityLinkDisplayEnabled
			{
				set
				{
					base.PowerSharpParameters["CommunityLinkDisplayEnabled"] = value;
				}
			}

			// Token: 0x17002F42 RID: 12098
			// (set) Token: 0x06004FAB RID: 20395 RVA: 0x0007E8F3 File Offset: 0x0007CAF3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F43 RID: 12099
			// (set) Token: 0x06004FAC RID: 20396 RVA: 0x0007E90B File Offset: 0x0007CB0B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F44 RID: 12100
			// (set) Token: 0x06004FAD RID: 20397 RVA: 0x0007E923 File Offset: 0x0007CB23
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F45 RID: 12101
			// (set) Token: 0x06004FAE RID: 20398 RVA: 0x0007E93B File Offset: 0x0007CB3B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F46 RID: 12102
			// (set) Token: 0x06004FAF RID: 20399 RVA: 0x0007E953 File Offset: 0x0007CB53
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000611 RID: 1553
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002F47 RID: 12103
			// (set) Token: 0x06004FB1 RID: 20401 RVA: 0x0007E973 File Offset: 0x0007CB73
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002F48 RID: 12104
			// (set) Token: 0x06004FB2 RID: 20402 RVA: 0x0007E986 File Offset: 0x0007CB86
			public virtual bool ExchangeHelpAppOnline
			{
				set
				{
					base.PowerSharpParameters["ExchangeHelpAppOnline"] = value;
				}
			}

			// Token: 0x17002F49 RID: 12105
			// (set) Token: 0x06004FB3 RID: 20403 RVA: 0x0007E99E File Offset: 0x0007CB9E
			public virtual Uri ControlPanelHelpURL
			{
				set
				{
					base.PowerSharpParameters["ControlPanelHelpURL"] = value;
				}
			}

			// Token: 0x17002F4A RID: 12106
			// (set) Token: 0x06004FB4 RID: 20404 RVA: 0x0007E9B1 File Offset: 0x0007CBB1
			public virtual Uri ControlPanelFeedbackURL
			{
				set
				{
					base.PowerSharpParameters["ControlPanelFeedbackURL"] = value;
				}
			}

			// Token: 0x17002F4B RID: 12107
			// (set) Token: 0x06004FB5 RID: 20405 RVA: 0x0007E9C4 File Offset: 0x0007CBC4
			public virtual bool ControlPanelFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["ControlPanelFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17002F4C RID: 12108
			// (set) Token: 0x06004FB6 RID: 20406 RVA: 0x0007E9DC File Offset: 0x0007CBDC
			public virtual Uri ManagementConsoleHelpURL
			{
				set
				{
					base.PowerSharpParameters["ManagementConsoleHelpURL"] = value;
				}
			}

			// Token: 0x17002F4D RID: 12109
			// (set) Token: 0x06004FB7 RID: 20407 RVA: 0x0007E9EF File Offset: 0x0007CBEF
			public virtual Uri ManagementConsoleFeedbackURL
			{
				set
				{
					base.PowerSharpParameters["ManagementConsoleFeedbackURL"] = value;
				}
			}

			// Token: 0x17002F4E RID: 12110
			// (set) Token: 0x06004FB8 RID: 20408 RVA: 0x0007EA02 File Offset: 0x0007CC02
			public virtual bool ManagementConsoleFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["ManagementConsoleFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17002F4F RID: 12111
			// (set) Token: 0x06004FB9 RID: 20409 RVA: 0x0007EA1A File Offset: 0x0007CC1A
			public virtual Uri OWAHelpURL
			{
				set
				{
					base.PowerSharpParameters["OWAHelpURL"] = value;
				}
			}

			// Token: 0x17002F50 RID: 12112
			// (set) Token: 0x06004FBA RID: 20410 RVA: 0x0007EA2D File Offset: 0x0007CC2D
			public virtual Uri OWAFeedbackURL
			{
				set
				{
					base.PowerSharpParameters["OWAFeedbackURL"] = value;
				}
			}

			// Token: 0x17002F51 RID: 12113
			// (set) Token: 0x06004FBB RID: 20411 RVA: 0x0007EA40 File Offset: 0x0007CC40
			public virtual bool OWAFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["OWAFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17002F52 RID: 12114
			// (set) Token: 0x06004FBC RID: 20412 RVA: 0x0007EA58 File Offset: 0x0007CC58
			public virtual Uri OWALightHelpURL
			{
				set
				{
					base.PowerSharpParameters["OWALightHelpURL"] = value;
				}
			}

			// Token: 0x17002F53 RID: 12115
			// (set) Token: 0x06004FBD RID: 20413 RVA: 0x0007EA6B File Offset: 0x0007CC6B
			public virtual Uri OWALightFeedbackURL
			{
				set
				{
					base.PowerSharpParameters["OWALightFeedbackURL"] = value;
				}
			}

			// Token: 0x17002F54 RID: 12116
			// (set) Token: 0x06004FBE RID: 20414 RVA: 0x0007EA7E File Offset: 0x0007CC7E
			public virtual bool OWALightFeedbackEnabled
			{
				set
				{
					base.PowerSharpParameters["OWALightFeedbackEnabled"] = value;
				}
			}

			// Token: 0x17002F55 RID: 12117
			// (set) Token: 0x06004FBF RID: 20415 RVA: 0x0007EA96 File Offset: 0x0007CC96
			public virtual Uri WindowsLiveAccountPageURL
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveAccountPageURL"] = value;
				}
			}

			// Token: 0x17002F56 RID: 12118
			// (set) Token: 0x06004FC0 RID: 20416 RVA: 0x0007EAA9 File Offset: 0x0007CCA9
			public virtual bool WindowsLiveAccountURLEnabled
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveAccountURLEnabled"] = value;
				}
			}

			// Token: 0x17002F57 RID: 12119
			// (set) Token: 0x06004FC1 RID: 20417 RVA: 0x0007EAC1 File Offset: 0x0007CCC1
			public virtual Uri PrivacyStatementURL
			{
				set
				{
					base.PowerSharpParameters["PrivacyStatementURL"] = value;
				}
			}

			// Token: 0x17002F58 RID: 12120
			// (set) Token: 0x06004FC2 RID: 20418 RVA: 0x0007EAD4 File Offset: 0x0007CCD4
			public virtual bool PrivacyLinkDisplayEnabled
			{
				set
				{
					base.PowerSharpParameters["PrivacyLinkDisplayEnabled"] = value;
				}
			}

			// Token: 0x17002F59 RID: 12121
			// (set) Token: 0x06004FC3 RID: 20419 RVA: 0x0007EAEC File Offset: 0x0007CCEC
			public virtual Uri CommunityURL
			{
				set
				{
					base.PowerSharpParameters["CommunityURL"] = value;
				}
			}

			// Token: 0x17002F5A RID: 12122
			// (set) Token: 0x06004FC4 RID: 20420 RVA: 0x0007EAFF File Offset: 0x0007CCFF
			public virtual bool CommunityLinkDisplayEnabled
			{
				set
				{
					base.PowerSharpParameters["CommunityLinkDisplayEnabled"] = value;
				}
			}

			// Token: 0x17002F5B RID: 12123
			// (set) Token: 0x06004FC5 RID: 20421 RVA: 0x0007EB17 File Offset: 0x0007CD17
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002F5C RID: 12124
			// (set) Token: 0x06004FC6 RID: 20422 RVA: 0x0007EB2F File Offset: 0x0007CD2F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002F5D RID: 12125
			// (set) Token: 0x06004FC7 RID: 20423 RVA: 0x0007EB47 File Offset: 0x0007CD47
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002F5E RID: 12126
			// (set) Token: 0x06004FC8 RID: 20424 RVA: 0x0007EB5F File Offset: 0x0007CD5F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002F5F RID: 12127
			// (set) Token: 0x06004FC9 RID: 20425 RVA: 0x0007EB77 File Offset: 0x0007CD77
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
