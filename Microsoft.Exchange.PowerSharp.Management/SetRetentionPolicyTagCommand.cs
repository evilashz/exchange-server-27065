using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000F9 RID: 249
	public class SetRetentionPolicyTagCommand : SyntheticCommandWithPipelineInputNoOutput<RetentionPolicyTag>
	{
		// Token: 0x06001E68 RID: 7784 RVA: 0x0003F2FB File Offset: 0x0003D4FB
		private SetRetentionPolicyTagCommand() : base("Set-RetentionPolicyTag")
		{
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0003F308 File Offset: 0x0003D508
		public SetRetentionPolicyTagCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0003F317 File Offset: 0x0003D517
		public virtual SetRetentionPolicyTagCommand SetParameters(SetRetentionPolicyTagCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0003F321 File Offset: 0x0003D521
		public virtual SetRetentionPolicyTagCommand SetParameters(SetRetentionPolicyTagCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0003F32B File Offset: 0x0003D52B
		public virtual SetRetentionPolicyTagCommand SetParameters(SetRetentionPolicyTagCommand.ParameterSetMailboxTaskParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000FA RID: 250
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700082F RID: 2095
			// (set) Token: 0x06001E6D RID: 7789 RVA: 0x0003F335 File Offset: 0x0003D535
			public virtual bool RetentionEnabled
			{
				set
				{
					base.PowerSharpParameters["RetentionEnabled"] = value;
				}
			}

			// Token: 0x17000830 RID: 2096
			// (set) Token: 0x06001E6E RID: 7790 RVA: 0x0003F34D File Offset: 0x0003D54D
			public virtual RetentionActionType RetentionAction
			{
				set
				{
					base.PowerSharpParameters["RetentionAction"] = value;
				}
			}

			// Token: 0x17000831 RID: 2097
			// (set) Token: 0x06001E6F RID: 7791 RVA: 0x0003F365 File Offset: 0x0003D565
			public virtual string MessageClass
			{
				set
				{
					base.PowerSharpParameters["MessageClass"] = value;
				}
			}

			// Token: 0x17000832 RID: 2098
			// (set) Token: 0x06001E70 RID: 7792 RVA: 0x0003F378 File Offset: 0x0003D578
			public virtual EnhancedTimeSpan? AgeLimitForRetention
			{
				set
				{
					base.PowerSharpParameters["AgeLimitForRetention"] = value;
				}
			}

			// Token: 0x17000833 RID: 2099
			// (set) Token: 0x06001E71 RID: 7793 RVA: 0x0003F390 File Offset: 0x0003D590
			public virtual bool JournalingEnabled
			{
				set
				{
					base.PowerSharpParameters["JournalingEnabled"] = value;
				}
			}

			// Token: 0x17000834 RID: 2100
			// (set) Token: 0x06001E72 RID: 7794 RVA: 0x0003F3A8 File Offset: 0x0003D5A8
			public virtual string AddressForJournaling
			{
				set
				{
					base.PowerSharpParameters["AddressForJournaling"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17000835 RID: 2101
			// (set) Token: 0x06001E73 RID: 7795 RVA: 0x0003F3C6 File Offset: 0x0003D5C6
			public virtual JournalingFormat MessageFormatForJournaling
			{
				set
				{
					base.PowerSharpParameters["MessageFormatForJournaling"] = value;
				}
			}

			// Token: 0x17000836 RID: 2102
			// (set) Token: 0x06001E74 RID: 7796 RVA: 0x0003F3DE File Offset: 0x0003D5DE
			public virtual string LabelForJournaling
			{
				set
				{
					base.PowerSharpParameters["LabelForJournaling"] = value;
				}
			}

			// Token: 0x17000837 RID: 2103
			// (set) Token: 0x06001E75 RID: 7797 RVA: 0x0003F3F1 File Offset: 0x0003D5F1
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RetentionPolicyTagIdParameter(value) : null);
				}
			}

			// Token: 0x17000838 RID: 2104
			// (set) Token: 0x06001E76 RID: 7798 RVA: 0x0003F40F File Offset: 0x0003D60F
			public virtual string LegacyManagedFolder
			{
				set
				{
					base.PowerSharpParameters["LegacyManagedFolder"] = ((value != null) ? new ELCFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17000839 RID: 2105
			// (set) Token: 0x06001E77 RID: 7799 RVA: 0x0003F42D File Offset: 0x0003D62D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700083A RID: 2106
			// (set) Token: 0x06001E78 RID: 7800 RVA: 0x0003F445 File Offset: 0x0003D645
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700083B RID: 2107
			// (set) Token: 0x06001E79 RID: 7801 RVA: 0x0003F458 File Offset: 0x0003D658
			public virtual bool SystemTag
			{
				set
				{
					base.PowerSharpParameters["SystemTag"] = value;
				}
			}

			// Token: 0x1700083C RID: 2108
			// (set) Token: 0x06001E7A RID: 7802 RVA: 0x0003F470 File Offset: 0x0003D670
			public virtual MultiValuedProperty<string> LocalizedRetentionPolicyTagName
			{
				set
				{
					base.PowerSharpParameters["LocalizedRetentionPolicyTagName"] = value;
				}
			}

			// Token: 0x1700083D RID: 2109
			// (set) Token: 0x06001E7B RID: 7803 RVA: 0x0003F483 File Offset: 0x0003D683
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x1700083E RID: 2110
			// (set) Token: 0x06001E7C RID: 7804 RVA: 0x0003F496 File Offset: 0x0003D696
			public virtual Guid RetentionId
			{
				set
				{
					base.PowerSharpParameters["RetentionId"] = value;
				}
			}

			// Token: 0x1700083F RID: 2111
			// (set) Token: 0x06001E7D RID: 7805 RVA: 0x0003F4AE File Offset: 0x0003D6AE
			public virtual MultiValuedProperty<string> LocalizedComment
			{
				set
				{
					base.PowerSharpParameters["LocalizedComment"] = value;
				}
			}

			// Token: 0x17000840 RID: 2112
			// (set) Token: 0x06001E7E RID: 7806 RVA: 0x0003F4C1 File Offset: 0x0003D6C1
			public virtual bool MustDisplayCommentEnabled
			{
				set
				{
					base.PowerSharpParameters["MustDisplayCommentEnabled"] = value;
				}
			}

			// Token: 0x17000841 RID: 2113
			// (set) Token: 0x06001E7F RID: 7807 RVA: 0x0003F4D9 File Offset: 0x0003D6D9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000842 RID: 2114
			// (set) Token: 0x06001E80 RID: 7808 RVA: 0x0003F4EC File Offset: 0x0003D6EC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000843 RID: 2115
			// (set) Token: 0x06001E81 RID: 7809 RVA: 0x0003F504 File Offset: 0x0003D704
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000844 RID: 2116
			// (set) Token: 0x06001E82 RID: 7810 RVA: 0x0003F51C File Offset: 0x0003D71C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000845 RID: 2117
			// (set) Token: 0x06001E83 RID: 7811 RVA: 0x0003F534 File Offset: 0x0003D734
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000846 RID: 2118
			// (set) Token: 0x06001E84 RID: 7812 RVA: 0x0003F54C File Offset: 0x0003D74C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000FB RID: 251
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000847 RID: 2119
			// (set) Token: 0x06001E86 RID: 7814 RVA: 0x0003F56C File Offset: 0x0003D76C
			public virtual string LegacyManagedFolder
			{
				set
				{
					base.PowerSharpParameters["LegacyManagedFolder"] = ((value != null) ? new ELCFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17000848 RID: 2120
			// (set) Token: 0x06001E87 RID: 7815 RVA: 0x0003F58A File Offset: 0x0003D78A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000849 RID: 2121
			// (set) Token: 0x06001E88 RID: 7816 RVA: 0x0003F5A2 File Offset: 0x0003D7A2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700084A RID: 2122
			// (set) Token: 0x06001E89 RID: 7817 RVA: 0x0003F5B5 File Offset: 0x0003D7B5
			public virtual bool SystemTag
			{
				set
				{
					base.PowerSharpParameters["SystemTag"] = value;
				}
			}

			// Token: 0x1700084B RID: 2123
			// (set) Token: 0x06001E8A RID: 7818 RVA: 0x0003F5CD File Offset: 0x0003D7CD
			public virtual MultiValuedProperty<string> LocalizedRetentionPolicyTagName
			{
				set
				{
					base.PowerSharpParameters["LocalizedRetentionPolicyTagName"] = value;
				}
			}

			// Token: 0x1700084C RID: 2124
			// (set) Token: 0x06001E8B RID: 7819 RVA: 0x0003F5E0 File Offset: 0x0003D7E0
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x1700084D RID: 2125
			// (set) Token: 0x06001E8C RID: 7820 RVA: 0x0003F5F3 File Offset: 0x0003D7F3
			public virtual Guid RetentionId
			{
				set
				{
					base.PowerSharpParameters["RetentionId"] = value;
				}
			}

			// Token: 0x1700084E RID: 2126
			// (set) Token: 0x06001E8D RID: 7821 RVA: 0x0003F60B File Offset: 0x0003D80B
			public virtual MultiValuedProperty<string> LocalizedComment
			{
				set
				{
					base.PowerSharpParameters["LocalizedComment"] = value;
				}
			}

			// Token: 0x1700084F RID: 2127
			// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0003F61E File Offset: 0x0003D81E
			public virtual bool MustDisplayCommentEnabled
			{
				set
				{
					base.PowerSharpParameters["MustDisplayCommentEnabled"] = value;
				}
			}

			// Token: 0x17000850 RID: 2128
			// (set) Token: 0x06001E8F RID: 7823 RVA: 0x0003F636 File Offset: 0x0003D836
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000851 RID: 2129
			// (set) Token: 0x06001E90 RID: 7824 RVA: 0x0003F649 File Offset: 0x0003D849
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000852 RID: 2130
			// (set) Token: 0x06001E91 RID: 7825 RVA: 0x0003F661 File Offset: 0x0003D861
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000853 RID: 2131
			// (set) Token: 0x06001E92 RID: 7826 RVA: 0x0003F679 File Offset: 0x0003D879
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000854 RID: 2132
			// (set) Token: 0x06001E93 RID: 7827 RVA: 0x0003F691 File Offset: 0x0003D891
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000855 RID: 2133
			// (set) Token: 0x06001E94 RID: 7828 RVA: 0x0003F6A9 File Offset: 0x0003D8A9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000FC RID: 252
		public class ParameterSetMailboxTaskParameters : ParametersBase
		{
			// Token: 0x17000856 RID: 2134
			// (set) Token: 0x06001E96 RID: 7830 RVA: 0x0003F6C9 File Offset: 0x0003D8C9
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000857 RID: 2135
			// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0003F6E7 File Offset: 0x0003D8E7
			public virtual RetentionPolicyTagIdParameter OptionalInMailbox
			{
				set
				{
					base.PowerSharpParameters["OptionalInMailbox"] = value;
				}
			}

			// Token: 0x17000858 RID: 2136
			// (set) Token: 0x06001E98 RID: 7832 RVA: 0x0003F6FA File Offset: 0x0003D8FA
			public virtual string LegacyManagedFolder
			{
				set
				{
					base.PowerSharpParameters["LegacyManagedFolder"] = ((value != null) ? new ELCFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17000859 RID: 2137
			// (set) Token: 0x06001E99 RID: 7833 RVA: 0x0003F718 File Offset: 0x0003D918
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700085A RID: 2138
			// (set) Token: 0x06001E9A RID: 7834 RVA: 0x0003F730 File Offset: 0x0003D930
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700085B RID: 2139
			// (set) Token: 0x06001E9B RID: 7835 RVA: 0x0003F743 File Offset: 0x0003D943
			public virtual bool SystemTag
			{
				set
				{
					base.PowerSharpParameters["SystemTag"] = value;
				}
			}

			// Token: 0x1700085C RID: 2140
			// (set) Token: 0x06001E9C RID: 7836 RVA: 0x0003F75B File Offset: 0x0003D95B
			public virtual MultiValuedProperty<string> LocalizedRetentionPolicyTagName
			{
				set
				{
					base.PowerSharpParameters["LocalizedRetentionPolicyTagName"] = value;
				}
			}

			// Token: 0x1700085D RID: 2141
			// (set) Token: 0x06001E9D RID: 7837 RVA: 0x0003F76E File Offset: 0x0003D96E
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x1700085E RID: 2142
			// (set) Token: 0x06001E9E RID: 7838 RVA: 0x0003F781 File Offset: 0x0003D981
			public virtual Guid RetentionId
			{
				set
				{
					base.PowerSharpParameters["RetentionId"] = value;
				}
			}

			// Token: 0x1700085F RID: 2143
			// (set) Token: 0x06001E9F RID: 7839 RVA: 0x0003F799 File Offset: 0x0003D999
			public virtual MultiValuedProperty<string> LocalizedComment
			{
				set
				{
					base.PowerSharpParameters["LocalizedComment"] = value;
				}
			}

			// Token: 0x17000860 RID: 2144
			// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x0003F7AC File Offset: 0x0003D9AC
			public virtual bool MustDisplayCommentEnabled
			{
				set
				{
					base.PowerSharpParameters["MustDisplayCommentEnabled"] = value;
				}
			}

			// Token: 0x17000861 RID: 2145
			// (set) Token: 0x06001EA1 RID: 7841 RVA: 0x0003F7C4 File Offset: 0x0003D9C4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000862 RID: 2146
			// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x0003F7D7 File Offset: 0x0003D9D7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000863 RID: 2147
			// (set) Token: 0x06001EA3 RID: 7843 RVA: 0x0003F7EF File Offset: 0x0003D9EF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000864 RID: 2148
			// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x0003F807 File Offset: 0x0003DA07
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000865 RID: 2149
			// (set) Token: 0x06001EA5 RID: 7845 RVA: 0x0003F81F File Offset: 0x0003DA1F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000866 RID: 2150
			// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x0003F837 File Offset: 0x0003DA37
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
