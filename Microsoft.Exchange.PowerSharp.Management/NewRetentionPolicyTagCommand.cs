using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000EF RID: 239
	public class NewRetentionPolicyTagCommand : SyntheticCommandWithPipelineInput<RetentionPolicyTag, RetentionPolicyTag>
	{
		// Token: 0x06001DF5 RID: 7669 RVA: 0x0003E98A File Offset: 0x0003CB8A
		private NewRetentionPolicyTagCommand() : base("New-RetentionPolicyTag")
		{
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0003E997 File Offset: 0x0003CB97
		public NewRetentionPolicyTagCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0003E9A6 File Offset: 0x0003CBA6
		public virtual NewRetentionPolicyTagCommand SetParameters(NewRetentionPolicyTagCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x0003E9B0 File Offset: 0x0003CBB0
		public virtual NewRetentionPolicyTagCommand SetParameters(NewRetentionPolicyTagCommand.RetentionPolicyParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0003E9BA File Offset: 0x0003CBBA
		public virtual NewRetentionPolicyTagCommand SetParameters(NewRetentionPolicyTagCommand.UpgradeManagedFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000F0 RID: 240
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170007D0 RID: 2000
			// (set) Token: 0x06001DFA RID: 7674 RVA: 0x0003E9C4 File Offset: 0x0003CBC4
			public virtual MultiValuedProperty<string> LocalizedRetentionPolicyTagName
			{
				set
				{
					base.PowerSharpParameters["LocalizedRetentionPolicyTagName"] = value;
				}
			}

			// Token: 0x170007D1 RID: 2001
			// (set) Token: 0x06001DFB RID: 7675 RVA: 0x0003E9D7 File Offset: 0x0003CBD7
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170007D2 RID: 2002
			// (set) Token: 0x06001DFC RID: 7676 RVA: 0x0003E9EF File Offset: 0x0003CBEF
			public virtual SwitchParameter IsDefaultAutoGroupPolicyTag
			{
				set
				{
					base.PowerSharpParameters["IsDefaultAutoGroupPolicyTag"] = value;
				}
			}

			// Token: 0x170007D3 RID: 2003
			// (set) Token: 0x06001DFD RID: 7677 RVA: 0x0003EA07 File Offset: 0x0003CC07
			public virtual SwitchParameter IsDefaultModeratedRecipientsPolicyTag
			{
				set
				{
					base.PowerSharpParameters["IsDefaultModeratedRecipientsPolicyTag"] = value;
				}
			}

			// Token: 0x170007D4 RID: 2004
			// (set) Token: 0x06001DFE RID: 7678 RVA: 0x0003EA1F File Offset: 0x0003CC1F
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170007D5 RID: 2005
			// (set) Token: 0x06001DFF RID: 7679 RVA: 0x0003EA32 File Offset: 0x0003CC32
			public virtual MultiValuedProperty<string> LocalizedComment
			{
				set
				{
					base.PowerSharpParameters["LocalizedComment"] = value;
				}
			}

			// Token: 0x170007D6 RID: 2006
			// (set) Token: 0x06001E00 RID: 7680 RVA: 0x0003EA45 File Offset: 0x0003CC45
			public virtual bool MustDisplayCommentEnabled
			{
				set
				{
					base.PowerSharpParameters["MustDisplayCommentEnabled"] = value;
				}
			}

			// Token: 0x170007D7 RID: 2007
			// (set) Token: 0x06001E01 RID: 7681 RVA: 0x0003EA5D File Offset: 0x0003CC5D
			public virtual ElcFolderType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x170007D8 RID: 2008
			// (set) Token: 0x06001E02 RID: 7682 RVA: 0x0003EA75 File Offset: 0x0003CC75
			public virtual bool SystemTag
			{
				set
				{
					base.PowerSharpParameters["SystemTag"] = value;
				}
			}

			// Token: 0x170007D9 RID: 2009
			// (set) Token: 0x06001E03 RID: 7683 RVA: 0x0003EA8D File Offset: 0x0003CC8D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170007DA RID: 2010
			// (set) Token: 0x06001E04 RID: 7684 RVA: 0x0003EAAB File Offset: 0x0003CCAB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170007DB RID: 2011
			// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0003EABE File Offset: 0x0003CCBE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170007DC RID: 2012
			// (set) Token: 0x06001E06 RID: 7686 RVA: 0x0003EAD1 File Offset: 0x0003CCD1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170007DD RID: 2013
			// (set) Token: 0x06001E07 RID: 7687 RVA: 0x0003EAE9 File Offset: 0x0003CCE9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170007DE RID: 2014
			// (set) Token: 0x06001E08 RID: 7688 RVA: 0x0003EB01 File Offset: 0x0003CD01
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170007DF RID: 2015
			// (set) Token: 0x06001E09 RID: 7689 RVA: 0x0003EB19 File Offset: 0x0003CD19
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170007E0 RID: 2016
			// (set) Token: 0x06001E0A RID: 7690 RVA: 0x0003EB31 File Offset: 0x0003CD31
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000F1 RID: 241
		public class RetentionPolicyParameters : ParametersBase
		{
			// Token: 0x170007E1 RID: 2017
			// (set) Token: 0x06001E0C RID: 7692 RVA: 0x0003EB51 File Offset: 0x0003CD51
			public virtual Guid RetentionId
			{
				set
				{
					base.PowerSharpParameters["RetentionId"] = value;
				}
			}

			// Token: 0x170007E2 RID: 2018
			// (set) Token: 0x06001E0D RID: 7693 RVA: 0x0003EB69 File Offset: 0x0003CD69
			public virtual string MessageClass
			{
				set
				{
					base.PowerSharpParameters["MessageClass"] = value;
				}
			}

			// Token: 0x170007E3 RID: 2019
			// (set) Token: 0x06001E0E RID: 7694 RVA: 0x0003EB7C File Offset: 0x0003CD7C
			public virtual bool RetentionEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionEnabled"] = value;
				}
			}

			// Token: 0x170007E4 RID: 2020
			// (set) Token: 0x06001E0F RID: 7695 RVA: 0x0003EB94 File Offset: 0x0003CD94
			public virtual RetentionActionType RetentionAction
			{
				set
				{
					base.PowerSharpParameters["RetentionAction"] = value;
				}
			}

			// Token: 0x170007E5 RID: 2021
			// (set) Token: 0x06001E10 RID: 7696 RVA: 0x0003EBAC File Offset: 0x0003CDAC
			public virtual EnhancedTimeSpan? AgeLimitForRetention
			{
				set
				{
					base.PowerSharpParameters["AgeLimitForRetention"] = value;
				}
			}

			// Token: 0x170007E6 RID: 2022
			// (set) Token: 0x06001E11 RID: 7697 RVA: 0x0003EBC4 File Offset: 0x0003CDC4
			public virtual bool JournalingEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalingEnabled"] = value;
				}
			}

			// Token: 0x170007E7 RID: 2023
			// (set) Token: 0x06001E12 RID: 7698 RVA: 0x0003EBDC File Offset: 0x0003CDDC
			public virtual string AddressForJournaling
			{
				set
				{
					base.PowerSharpParameters["AddressForJournaling"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170007E8 RID: 2024
			// (set) Token: 0x06001E13 RID: 7699 RVA: 0x0003EBFA File Offset: 0x0003CDFA
			public virtual JournalingFormat MessageFormatForJournaling
			{
				set
				{
					base.PowerSharpParameters["MessageFormatForJournaling"] = value;
				}
			}

			// Token: 0x170007E9 RID: 2025
			// (set) Token: 0x06001E14 RID: 7700 RVA: 0x0003EC12 File Offset: 0x0003CE12
			public virtual string LabelForJournaling
			{
				set
				{
					base.PowerSharpParameters["LabelForJournaling"] = value;
				}
			}

			// Token: 0x170007EA RID: 2026
			// (set) Token: 0x06001E15 RID: 7701 RVA: 0x0003EC25 File Offset: 0x0003CE25
			public virtual MultiValuedProperty<string> LocalizedRetentionPolicyTagName
			{
				set
				{
					base.PowerSharpParameters["LocalizedRetentionPolicyTagName"] = value;
				}
			}

			// Token: 0x170007EB RID: 2027
			// (set) Token: 0x06001E16 RID: 7702 RVA: 0x0003EC38 File Offset: 0x0003CE38
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170007EC RID: 2028
			// (set) Token: 0x06001E17 RID: 7703 RVA: 0x0003EC50 File Offset: 0x0003CE50
			public virtual SwitchParameter IsDefaultAutoGroupPolicyTag
			{
				set
				{
					base.PowerSharpParameters["IsDefaultAutoGroupPolicyTag"] = value;
				}
			}

			// Token: 0x170007ED RID: 2029
			// (set) Token: 0x06001E18 RID: 7704 RVA: 0x0003EC68 File Offset: 0x0003CE68
			public virtual SwitchParameter IsDefaultModeratedRecipientsPolicyTag
			{
				set
				{
					base.PowerSharpParameters["IsDefaultModeratedRecipientsPolicyTag"] = value;
				}
			}

			// Token: 0x170007EE RID: 2030
			// (set) Token: 0x06001E19 RID: 7705 RVA: 0x0003EC80 File Offset: 0x0003CE80
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x170007EF RID: 2031
			// (set) Token: 0x06001E1A RID: 7706 RVA: 0x0003EC93 File Offset: 0x0003CE93
			public virtual MultiValuedProperty<string> LocalizedComment
			{
				set
				{
					base.PowerSharpParameters["LocalizedComment"] = value;
				}
			}

			// Token: 0x170007F0 RID: 2032
			// (set) Token: 0x06001E1B RID: 7707 RVA: 0x0003ECA6 File Offset: 0x0003CEA6
			public virtual bool MustDisplayCommentEnabled
			{
				set
				{
					base.PowerSharpParameters["MustDisplayCommentEnabled"] = value;
				}
			}

			// Token: 0x170007F1 RID: 2033
			// (set) Token: 0x06001E1C RID: 7708 RVA: 0x0003ECBE File Offset: 0x0003CEBE
			public virtual ElcFolderType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (set) Token: 0x06001E1D RID: 7709 RVA: 0x0003ECD6 File Offset: 0x0003CED6
			public virtual bool SystemTag
			{
				set
				{
					base.PowerSharpParameters["SystemTag"] = value;
				}
			}

			// Token: 0x170007F3 RID: 2035
			// (set) Token: 0x06001E1E RID: 7710 RVA: 0x0003ECEE File Offset: 0x0003CEEE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170007F4 RID: 2036
			// (set) Token: 0x06001E1F RID: 7711 RVA: 0x0003ED0C File Offset: 0x0003CF0C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170007F5 RID: 2037
			// (set) Token: 0x06001E20 RID: 7712 RVA: 0x0003ED1F File Offset: 0x0003CF1F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170007F6 RID: 2038
			// (set) Token: 0x06001E21 RID: 7713 RVA: 0x0003ED32 File Offset: 0x0003CF32
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170007F7 RID: 2039
			// (set) Token: 0x06001E22 RID: 7714 RVA: 0x0003ED4A File Offset: 0x0003CF4A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170007F8 RID: 2040
			// (set) Token: 0x06001E23 RID: 7715 RVA: 0x0003ED62 File Offset: 0x0003CF62
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170007F9 RID: 2041
			// (set) Token: 0x06001E24 RID: 7716 RVA: 0x0003ED7A File Offset: 0x0003CF7A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170007FA RID: 2042
			// (set) Token: 0x06001E25 RID: 7717 RVA: 0x0003ED92 File Offset: 0x0003CF92
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000F2 RID: 242
		public class UpgradeManagedFolderParameters : ParametersBase
		{
			// Token: 0x170007FB RID: 2043
			// (set) Token: 0x06001E27 RID: 7719 RVA: 0x0003EDB2 File Offset: 0x0003CFB2
			public virtual string ManagedFolderToUpgrade
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderToUpgrade"] = ((value != null) ? new ELCFolderIdParameter(value) : null);
				}
			}

			// Token: 0x170007FC RID: 2044
			// (set) Token: 0x06001E28 RID: 7720 RVA: 0x0003EDD0 File Offset: 0x0003CFD0
			public virtual MultiValuedProperty<string> LocalizedRetentionPolicyTagName
			{
				set
				{
					base.PowerSharpParameters["LocalizedRetentionPolicyTagName"] = value;
				}
			}

			// Token: 0x170007FD RID: 2045
			// (set) Token: 0x06001E29 RID: 7721 RVA: 0x0003EDE3 File Offset: 0x0003CFE3
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170007FE RID: 2046
			// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0003EDFB File Offset: 0x0003CFFB
			public virtual SwitchParameter IsDefaultAutoGroupPolicyTag
			{
				set
				{
					base.PowerSharpParameters["IsDefaultAutoGroupPolicyTag"] = value;
				}
			}

			// Token: 0x170007FF RID: 2047
			// (set) Token: 0x06001E2B RID: 7723 RVA: 0x0003EE13 File Offset: 0x0003D013
			public virtual SwitchParameter IsDefaultModeratedRecipientsPolicyTag
			{
				set
				{
					base.PowerSharpParameters["IsDefaultModeratedRecipientsPolicyTag"] = value;
				}
			}

			// Token: 0x17000800 RID: 2048
			// (set) Token: 0x06001E2C RID: 7724 RVA: 0x0003EE2B File Offset: 0x0003D02B
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17000801 RID: 2049
			// (set) Token: 0x06001E2D RID: 7725 RVA: 0x0003EE3E File Offset: 0x0003D03E
			public virtual MultiValuedProperty<string> LocalizedComment
			{
				set
				{
					base.PowerSharpParameters["LocalizedComment"] = value;
				}
			}

			// Token: 0x17000802 RID: 2050
			// (set) Token: 0x06001E2E RID: 7726 RVA: 0x0003EE51 File Offset: 0x0003D051
			public virtual bool MustDisplayCommentEnabled
			{
				set
				{
					base.PowerSharpParameters["MustDisplayCommentEnabled"] = value;
				}
			}

			// Token: 0x17000803 RID: 2051
			// (set) Token: 0x06001E2F RID: 7727 RVA: 0x0003EE69 File Offset: 0x0003D069
			public virtual ElcFolderType Type
			{
				set
				{
					base.PowerSharpParameters["Type"] = value;
				}
			}

			// Token: 0x17000804 RID: 2052
			// (set) Token: 0x06001E30 RID: 7728 RVA: 0x0003EE81 File Offset: 0x0003D081
			public virtual bool SystemTag
			{
				set
				{
					base.PowerSharpParameters["SystemTag"] = value;
				}
			}

			// Token: 0x17000805 RID: 2053
			// (set) Token: 0x06001E31 RID: 7729 RVA: 0x0003EE99 File Offset: 0x0003D099
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000806 RID: 2054
			// (set) Token: 0x06001E32 RID: 7730 RVA: 0x0003EEB7 File Offset: 0x0003D0B7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000807 RID: 2055
			// (set) Token: 0x06001E33 RID: 7731 RVA: 0x0003EECA File Offset: 0x0003D0CA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000808 RID: 2056
			// (set) Token: 0x06001E34 RID: 7732 RVA: 0x0003EEDD File Offset: 0x0003D0DD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000809 RID: 2057
			// (set) Token: 0x06001E35 RID: 7733 RVA: 0x0003EEF5 File Offset: 0x0003D0F5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700080A RID: 2058
			// (set) Token: 0x06001E36 RID: 7734 RVA: 0x0003EF0D File Offset: 0x0003D10D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700080B RID: 2059
			// (set) Token: 0x06001E37 RID: 7735 RVA: 0x0003EF25 File Offset: 0x0003D125
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700080C RID: 2060
			// (set) Token: 0x06001E38 RID: 7736 RVA: 0x0003EF3D File Offset: 0x0003D13D
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
