using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BFB RID: 3067
	public class GetDistributionGroupCommand : SyntheticCommandWithPipelineInput<DistributionGroup, DistributionGroup>
	{
		// Token: 0x060094BD RID: 38077 RVA: 0x000D8C33 File Offset: 0x000D6E33
		private GetDistributionGroupCommand() : base("Get-DistributionGroup")
		{
		}

		// Token: 0x060094BE RID: 38078 RVA: 0x000D8C40 File Offset: 0x000D6E40
		public GetDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060094BF RID: 38079 RVA: 0x000D8C4F File Offset: 0x000D6E4F
		public virtual GetDistributionGroupCommand SetParameters(GetDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060094C0 RID: 38080 RVA: 0x000D8C59 File Offset: 0x000D6E59
		public virtual GetDistributionGroupCommand SetParameters(GetDistributionGroupCommand.ManagedBySetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060094C1 RID: 38081 RVA: 0x000D8C63 File Offset: 0x000D6E63
		public virtual GetDistributionGroupCommand SetParameters(GetDistributionGroupCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060094C2 RID: 38082 RVA: 0x000D8C6D File Offset: 0x000D6E6D
		public virtual GetDistributionGroupCommand SetParameters(GetDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BFC RID: 3068
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006880 RID: 26752
			// (set) Token: 0x060094C3 RID: 38083 RVA: 0x000D8C77 File Offset: 0x000D6E77
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17006881 RID: 26753
			// (set) Token: 0x060094C4 RID: 38084 RVA: 0x000D8C8F File Offset: 0x000D6E8F
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17006882 RID: 26754
			// (set) Token: 0x060094C5 RID: 38085 RVA: 0x000D8CA7 File Offset: 0x000D6EA7
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006883 RID: 26755
			// (set) Token: 0x060094C6 RID: 38086 RVA: 0x000D8CBA File Offset: 0x000D6EBA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006884 RID: 26756
			// (set) Token: 0x060094C7 RID: 38087 RVA: 0x000D8CD8 File Offset: 0x000D6ED8
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006885 RID: 26757
			// (set) Token: 0x060094C8 RID: 38088 RVA: 0x000D8CEB File Offset: 0x000D6EEB
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006886 RID: 26758
			// (set) Token: 0x060094C9 RID: 38089 RVA: 0x000D8CFE File Offset: 0x000D6EFE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006887 RID: 26759
			// (set) Token: 0x060094CA RID: 38090 RVA: 0x000D8D1C File Offset: 0x000D6F1C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006888 RID: 26760
			// (set) Token: 0x060094CB RID: 38091 RVA: 0x000D8D34 File Offset: 0x000D6F34
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006889 RID: 26761
			// (set) Token: 0x060094CC RID: 38092 RVA: 0x000D8D47 File Offset: 0x000D6F47
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700688A RID: 26762
			// (set) Token: 0x060094CD RID: 38093 RVA: 0x000D8D5F File Offset: 0x000D6F5F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700688B RID: 26763
			// (set) Token: 0x060094CE RID: 38094 RVA: 0x000D8D77 File Offset: 0x000D6F77
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700688C RID: 26764
			// (set) Token: 0x060094CF RID: 38095 RVA: 0x000D8D8A File Offset: 0x000D6F8A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700688D RID: 26765
			// (set) Token: 0x060094D0 RID: 38096 RVA: 0x000D8DA2 File Offset: 0x000D6FA2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700688E RID: 26766
			// (set) Token: 0x060094D1 RID: 38097 RVA: 0x000D8DBA File Offset: 0x000D6FBA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700688F RID: 26767
			// (set) Token: 0x060094D2 RID: 38098 RVA: 0x000D8DD2 File Offset: 0x000D6FD2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BFD RID: 3069
		public class ManagedBySetParameters : ParametersBase
		{
			// Token: 0x17006890 RID: 26768
			// (set) Token: 0x060094D4 RID: 38100 RVA: 0x000D8DF2 File Offset: 0x000D6FF2
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006891 RID: 26769
			// (set) Token: 0x060094D5 RID: 38101 RVA: 0x000D8E10 File Offset: 0x000D7010
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x17006892 RID: 26770
			// (set) Token: 0x060094D6 RID: 38102 RVA: 0x000D8E28 File Offset: 0x000D7028
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x17006893 RID: 26771
			// (set) Token: 0x060094D7 RID: 38103 RVA: 0x000D8E40 File Offset: 0x000D7040
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006894 RID: 26772
			// (set) Token: 0x060094D8 RID: 38104 RVA: 0x000D8E53 File Offset: 0x000D7053
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006895 RID: 26773
			// (set) Token: 0x060094D9 RID: 38105 RVA: 0x000D8E71 File Offset: 0x000D7071
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006896 RID: 26774
			// (set) Token: 0x060094DA RID: 38106 RVA: 0x000D8E84 File Offset: 0x000D7084
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006897 RID: 26775
			// (set) Token: 0x060094DB RID: 38107 RVA: 0x000D8E97 File Offset: 0x000D7097
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006898 RID: 26776
			// (set) Token: 0x060094DC RID: 38108 RVA: 0x000D8EB5 File Offset: 0x000D70B5
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006899 RID: 26777
			// (set) Token: 0x060094DD RID: 38109 RVA: 0x000D8ECD File Offset: 0x000D70CD
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700689A RID: 26778
			// (set) Token: 0x060094DE RID: 38110 RVA: 0x000D8EE0 File Offset: 0x000D70E0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700689B RID: 26779
			// (set) Token: 0x060094DF RID: 38111 RVA: 0x000D8EF8 File Offset: 0x000D70F8
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700689C RID: 26780
			// (set) Token: 0x060094E0 RID: 38112 RVA: 0x000D8F10 File Offset: 0x000D7110
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700689D RID: 26781
			// (set) Token: 0x060094E1 RID: 38113 RVA: 0x000D8F23 File Offset: 0x000D7123
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700689E RID: 26782
			// (set) Token: 0x060094E2 RID: 38114 RVA: 0x000D8F3B File Offset: 0x000D713B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700689F RID: 26783
			// (set) Token: 0x060094E3 RID: 38115 RVA: 0x000D8F53 File Offset: 0x000D7153
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170068A0 RID: 26784
			// (set) Token: 0x060094E4 RID: 38116 RVA: 0x000D8F6B File Offset: 0x000D716B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BFE RID: 3070
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170068A1 RID: 26785
			// (set) Token: 0x060094E6 RID: 38118 RVA: 0x000D8F8B File Offset: 0x000D718B
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170068A2 RID: 26786
			// (set) Token: 0x060094E7 RID: 38119 RVA: 0x000D8F9E File Offset: 0x000D719E
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x170068A3 RID: 26787
			// (set) Token: 0x060094E8 RID: 38120 RVA: 0x000D8FB6 File Offset: 0x000D71B6
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170068A4 RID: 26788
			// (set) Token: 0x060094E9 RID: 38121 RVA: 0x000D8FCE File Offset: 0x000D71CE
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170068A5 RID: 26789
			// (set) Token: 0x060094EA RID: 38122 RVA: 0x000D8FE1 File Offset: 0x000D71E1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170068A6 RID: 26790
			// (set) Token: 0x060094EB RID: 38123 RVA: 0x000D8FFF File Offset: 0x000D71FF
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170068A7 RID: 26791
			// (set) Token: 0x060094EC RID: 38124 RVA: 0x000D9012 File Offset: 0x000D7212
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170068A8 RID: 26792
			// (set) Token: 0x060094ED RID: 38125 RVA: 0x000D9025 File Offset: 0x000D7225
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170068A9 RID: 26793
			// (set) Token: 0x060094EE RID: 38126 RVA: 0x000D9043 File Offset: 0x000D7243
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170068AA RID: 26794
			// (set) Token: 0x060094EF RID: 38127 RVA: 0x000D905B File Offset: 0x000D725B
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170068AB RID: 26795
			// (set) Token: 0x060094F0 RID: 38128 RVA: 0x000D906E File Offset: 0x000D726E
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170068AC RID: 26796
			// (set) Token: 0x060094F1 RID: 38129 RVA: 0x000D9086 File Offset: 0x000D7286
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170068AD RID: 26797
			// (set) Token: 0x060094F2 RID: 38130 RVA: 0x000D909E File Offset: 0x000D729E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170068AE RID: 26798
			// (set) Token: 0x060094F3 RID: 38131 RVA: 0x000D90B1 File Offset: 0x000D72B1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170068AF RID: 26799
			// (set) Token: 0x060094F4 RID: 38132 RVA: 0x000D90C9 File Offset: 0x000D72C9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170068B0 RID: 26800
			// (set) Token: 0x060094F5 RID: 38133 RVA: 0x000D90E1 File Offset: 0x000D72E1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170068B1 RID: 26801
			// (set) Token: 0x060094F6 RID: 38134 RVA: 0x000D90F9 File Offset: 0x000D72F9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000BFF RID: 3071
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170068B2 RID: 26802
			// (set) Token: 0x060094F8 RID: 38136 RVA: 0x000D9119 File Offset: 0x000D7319
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DistributionGroupIdParameter(value) : null);
				}
			}

			// Token: 0x170068B3 RID: 26803
			// (set) Token: 0x060094F9 RID: 38137 RVA: 0x000D9137 File Offset: 0x000D7337
			public virtual long UsnForReconciliationSearch
			{
				set
				{
					base.PowerSharpParameters["UsnForReconciliationSearch"] = value;
				}
			}

			// Token: 0x170068B4 RID: 26804
			// (set) Token: 0x060094FA RID: 38138 RVA: 0x000D914F File Offset: 0x000D734F
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170068B5 RID: 26805
			// (set) Token: 0x060094FB RID: 38139 RVA: 0x000D9167 File Offset: 0x000D7367
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170068B6 RID: 26806
			// (set) Token: 0x060094FC RID: 38140 RVA: 0x000D917A File Offset: 0x000D737A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170068B7 RID: 26807
			// (set) Token: 0x060094FD RID: 38141 RVA: 0x000D9198 File Offset: 0x000D7398
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170068B8 RID: 26808
			// (set) Token: 0x060094FE RID: 38142 RVA: 0x000D91AB File Offset: 0x000D73AB
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170068B9 RID: 26809
			// (set) Token: 0x060094FF RID: 38143 RVA: 0x000D91BE File Offset: 0x000D73BE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170068BA RID: 26810
			// (set) Token: 0x06009500 RID: 38144 RVA: 0x000D91DC File Offset: 0x000D73DC
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170068BB RID: 26811
			// (set) Token: 0x06009501 RID: 38145 RVA: 0x000D91F4 File Offset: 0x000D73F4
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170068BC RID: 26812
			// (set) Token: 0x06009502 RID: 38146 RVA: 0x000D9207 File Offset: 0x000D7407
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170068BD RID: 26813
			// (set) Token: 0x06009503 RID: 38147 RVA: 0x000D921F File Offset: 0x000D741F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170068BE RID: 26814
			// (set) Token: 0x06009504 RID: 38148 RVA: 0x000D9237 File Offset: 0x000D7437
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170068BF RID: 26815
			// (set) Token: 0x06009505 RID: 38149 RVA: 0x000D924A File Offset: 0x000D744A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170068C0 RID: 26816
			// (set) Token: 0x06009506 RID: 38150 RVA: 0x000D9262 File Offset: 0x000D7462
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170068C1 RID: 26817
			// (set) Token: 0x06009507 RID: 38151 RVA: 0x000D927A File Offset: 0x000D747A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170068C2 RID: 26818
			// (set) Token: 0x06009508 RID: 38152 RVA: 0x000D9292 File Offset: 0x000D7492
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
