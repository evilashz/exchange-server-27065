using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BEE RID: 3054
	public class GetContactCommand : SyntheticCommandWithPipelineInput<ADContact, ADContact>
	{
		// Token: 0x060093F5 RID: 37877 RVA: 0x000D7C4B File Offset: 0x000D5E4B
		private GetContactCommand() : base("Get-Contact")
		{
		}

		// Token: 0x060093F6 RID: 37878 RVA: 0x000D7C58 File Offset: 0x000D5E58
		public GetContactCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060093F7 RID: 37879 RVA: 0x000D7C67 File Offset: 0x000D5E67
		public virtual GetContactCommand SetParameters(GetContactCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060093F8 RID: 37880 RVA: 0x000D7C71 File Offset: 0x000D5E71
		public virtual GetContactCommand SetParameters(GetContactCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060093F9 RID: 37881 RVA: 0x000D7C7B File Offset: 0x000D5E7B
		public virtual GetContactCommand SetParameters(GetContactCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BEF RID: 3055
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170067D2 RID: 26578
			// (set) Token: 0x060093FA RID: 37882 RVA: 0x000D7C85 File Offset: 0x000D5E85
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170067D3 RID: 26579
			// (set) Token: 0x060093FB RID: 37883 RVA: 0x000D7C9D File Offset: 0x000D5E9D
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170067D4 RID: 26580
			// (set) Token: 0x060093FC RID: 37884 RVA: 0x000D7CB0 File Offset: 0x000D5EB0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170067D5 RID: 26581
			// (set) Token: 0x060093FD RID: 37885 RVA: 0x000D7CCE File Offset: 0x000D5ECE
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170067D6 RID: 26582
			// (set) Token: 0x060093FE RID: 37886 RVA: 0x000D7CE1 File Offset: 0x000D5EE1
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170067D7 RID: 26583
			// (set) Token: 0x060093FF RID: 37887 RVA: 0x000D7CF4 File Offset: 0x000D5EF4
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170067D8 RID: 26584
			// (set) Token: 0x06009400 RID: 37888 RVA: 0x000D7D12 File Offset: 0x000D5F12
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170067D9 RID: 26585
			// (set) Token: 0x06009401 RID: 37889 RVA: 0x000D7D2A File Offset: 0x000D5F2A
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170067DA RID: 26586
			// (set) Token: 0x06009402 RID: 37890 RVA: 0x000D7D3D File Offset: 0x000D5F3D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170067DB RID: 26587
			// (set) Token: 0x06009403 RID: 37891 RVA: 0x000D7D55 File Offset: 0x000D5F55
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170067DC RID: 26588
			// (set) Token: 0x06009404 RID: 37892 RVA: 0x000D7D6D File Offset: 0x000D5F6D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170067DD RID: 26589
			// (set) Token: 0x06009405 RID: 37893 RVA: 0x000D7D80 File Offset: 0x000D5F80
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170067DE RID: 26590
			// (set) Token: 0x06009406 RID: 37894 RVA: 0x000D7D98 File Offset: 0x000D5F98
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170067DF RID: 26591
			// (set) Token: 0x06009407 RID: 37895 RVA: 0x000D7DB0 File Offset: 0x000D5FB0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170067E0 RID: 26592
			// (set) Token: 0x06009408 RID: 37896 RVA: 0x000D7DC8 File Offset: 0x000D5FC8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BF0 RID: 3056
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170067E1 RID: 26593
			// (set) Token: 0x0600940A RID: 37898 RVA: 0x000D7DE8 File Offset: 0x000D5FE8
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170067E2 RID: 26594
			// (set) Token: 0x0600940B RID: 37899 RVA: 0x000D7DFB File Offset: 0x000D5FFB
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170067E3 RID: 26595
			// (set) Token: 0x0600940C RID: 37900 RVA: 0x000D7E13 File Offset: 0x000D6013
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170067E4 RID: 26596
			// (set) Token: 0x0600940D RID: 37901 RVA: 0x000D7E26 File Offset: 0x000D6026
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170067E5 RID: 26597
			// (set) Token: 0x0600940E RID: 37902 RVA: 0x000D7E44 File Offset: 0x000D6044
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170067E6 RID: 26598
			// (set) Token: 0x0600940F RID: 37903 RVA: 0x000D7E57 File Offset: 0x000D6057
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170067E7 RID: 26599
			// (set) Token: 0x06009410 RID: 37904 RVA: 0x000D7E6A File Offset: 0x000D606A
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170067E8 RID: 26600
			// (set) Token: 0x06009411 RID: 37905 RVA: 0x000D7E88 File Offset: 0x000D6088
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170067E9 RID: 26601
			// (set) Token: 0x06009412 RID: 37906 RVA: 0x000D7EA0 File Offset: 0x000D60A0
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170067EA RID: 26602
			// (set) Token: 0x06009413 RID: 37907 RVA: 0x000D7EB3 File Offset: 0x000D60B3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170067EB RID: 26603
			// (set) Token: 0x06009414 RID: 37908 RVA: 0x000D7ECB File Offset: 0x000D60CB
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170067EC RID: 26604
			// (set) Token: 0x06009415 RID: 37909 RVA: 0x000D7EE3 File Offset: 0x000D60E3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170067ED RID: 26605
			// (set) Token: 0x06009416 RID: 37910 RVA: 0x000D7EF6 File Offset: 0x000D60F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170067EE RID: 26606
			// (set) Token: 0x06009417 RID: 37911 RVA: 0x000D7F0E File Offset: 0x000D610E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170067EF RID: 26607
			// (set) Token: 0x06009418 RID: 37912 RVA: 0x000D7F26 File Offset: 0x000D6126
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170067F0 RID: 26608
			// (set) Token: 0x06009419 RID: 37913 RVA: 0x000D7F3E File Offset: 0x000D613E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BF1 RID: 3057
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170067F1 RID: 26609
			// (set) Token: 0x0600941B RID: 37915 RVA: 0x000D7F5E File Offset: 0x000D615E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ContactIdParameter(value) : null);
				}
			}

			// Token: 0x170067F2 RID: 26610
			// (set) Token: 0x0600941C RID: 37916 RVA: 0x000D7F7C File Offset: 0x000D617C
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170067F3 RID: 26611
			// (set) Token: 0x0600941D RID: 37917 RVA: 0x000D7F94 File Offset: 0x000D6194
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170067F4 RID: 26612
			// (set) Token: 0x0600941E RID: 37918 RVA: 0x000D7FA7 File Offset: 0x000D61A7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170067F5 RID: 26613
			// (set) Token: 0x0600941F RID: 37919 RVA: 0x000D7FC5 File Offset: 0x000D61C5
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170067F6 RID: 26614
			// (set) Token: 0x06009420 RID: 37920 RVA: 0x000D7FD8 File Offset: 0x000D61D8
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170067F7 RID: 26615
			// (set) Token: 0x06009421 RID: 37921 RVA: 0x000D7FEB File Offset: 0x000D61EB
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170067F8 RID: 26616
			// (set) Token: 0x06009422 RID: 37922 RVA: 0x000D8009 File Offset: 0x000D6209
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170067F9 RID: 26617
			// (set) Token: 0x06009423 RID: 37923 RVA: 0x000D8021 File Offset: 0x000D6221
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170067FA RID: 26618
			// (set) Token: 0x06009424 RID: 37924 RVA: 0x000D8034 File Offset: 0x000D6234
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170067FB RID: 26619
			// (set) Token: 0x06009425 RID: 37925 RVA: 0x000D804C File Offset: 0x000D624C
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170067FC RID: 26620
			// (set) Token: 0x06009426 RID: 37926 RVA: 0x000D8064 File Offset: 0x000D6264
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170067FD RID: 26621
			// (set) Token: 0x06009427 RID: 37927 RVA: 0x000D8077 File Offset: 0x000D6277
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170067FE RID: 26622
			// (set) Token: 0x06009428 RID: 37928 RVA: 0x000D808F File Offset: 0x000D628F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170067FF RID: 26623
			// (set) Token: 0x06009429 RID: 37929 RVA: 0x000D80A7 File Offset: 0x000D62A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006800 RID: 26624
			// (set) Token: 0x0600942A RID: 37930 RVA: 0x000D80BF File Offset: 0x000D62BF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
