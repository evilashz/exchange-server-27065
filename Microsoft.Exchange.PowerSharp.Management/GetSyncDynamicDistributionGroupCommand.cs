using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000D7B RID: 3451
	public class GetSyncDynamicDistributionGroupCommand : SyntheticCommandWithPipelineInput<SyncDynamicDistributionGroup, SyncDynamicDistributionGroup>
	{
		// Token: 0x0600B815 RID: 47125 RVA: 0x00108A74 File Offset: 0x00106C74
		private GetSyncDynamicDistributionGroupCommand() : base("Get-SyncDynamicDistributionGroup")
		{
		}

		// Token: 0x0600B816 RID: 47126 RVA: 0x00108A81 File Offset: 0x00106C81
		public GetSyncDynamicDistributionGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600B817 RID: 47127 RVA: 0x00108A90 File Offset: 0x00106C90
		public virtual GetSyncDynamicDistributionGroupCommand SetParameters(GetSyncDynamicDistributionGroupCommand.CookieSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B818 RID: 47128 RVA: 0x00108A9A File Offset: 0x00106C9A
		public virtual GetSyncDynamicDistributionGroupCommand SetParameters(GetSyncDynamicDistributionGroupCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B819 RID: 47129 RVA: 0x00108AA4 File Offset: 0x00106CA4
		public virtual GetSyncDynamicDistributionGroupCommand SetParameters(GetSyncDynamicDistributionGroupCommand.ManagedBySetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B81A RID: 47130 RVA: 0x00108AAE File Offset: 0x00106CAE
		public virtual GetSyncDynamicDistributionGroupCommand SetParameters(GetSyncDynamicDistributionGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600B81B RID: 47131 RVA: 0x00108AB8 File Offset: 0x00106CB8
		public virtual GetSyncDynamicDistributionGroupCommand SetParameters(GetSyncDynamicDistributionGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000D7C RID: 3452
		public class CookieSetParameters : ParametersBase
		{
			// Token: 0x170088D8 RID: 35032
			// (set) Token: 0x0600B81C RID: 47132 RVA: 0x00108AC2 File Offset: 0x00106CC2
			public virtual byte Cookie
			{
				set
				{
					base.PowerSharpParameters["Cookie"] = value;
				}
			}

			// Token: 0x170088D9 RID: 35033
			// (set) Token: 0x0600B81D RID: 47133 RVA: 0x00108ADA File Offset: 0x00106CDA
			public virtual int Pages
			{
				set
				{
					base.PowerSharpParameters["Pages"] = value;
				}
			}

			// Token: 0x170088DA RID: 35034
			// (set) Token: 0x0600B81E RID: 47134 RVA: 0x00108AF2 File Offset: 0x00106CF2
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170088DB RID: 35035
			// (set) Token: 0x0600B81F RID: 47135 RVA: 0x00108B0A File Offset: 0x00106D0A
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170088DC RID: 35036
			// (set) Token: 0x0600B820 RID: 47136 RVA: 0x00108B1D File Offset: 0x00106D1D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170088DD RID: 35037
			// (set) Token: 0x0600B821 RID: 47137 RVA: 0x00108B3B File Offset: 0x00106D3B
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170088DE RID: 35038
			// (set) Token: 0x0600B822 RID: 47138 RVA: 0x00108B4E File Offset: 0x00106D4E
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170088DF RID: 35039
			// (set) Token: 0x0600B823 RID: 47139 RVA: 0x00108B6C File Offset: 0x00106D6C
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170088E0 RID: 35040
			// (set) Token: 0x0600B824 RID: 47140 RVA: 0x00108B7F File Offset: 0x00106D7F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170088E1 RID: 35041
			// (set) Token: 0x0600B825 RID: 47141 RVA: 0x00108B92 File Offset: 0x00106D92
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170088E2 RID: 35042
			// (set) Token: 0x0600B826 RID: 47142 RVA: 0x00108BAA File Offset: 0x00106DAA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170088E3 RID: 35043
			// (set) Token: 0x0600B827 RID: 47143 RVA: 0x00108BC2 File Offset: 0x00106DC2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170088E4 RID: 35044
			// (set) Token: 0x0600B828 RID: 47144 RVA: 0x00108BDA File Offset: 0x00106DDA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D7D RID: 3453
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170088E5 RID: 35045
			// (set) Token: 0x0600B82A RID: 47146 RVA: 0x00108BFA File Offset: 0x00106DFA
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170088E6 RID: 35046
			// (set) Token: 0x0600B82B RID: 47147 RVA: 0x00108C12 File Offset: 0x00106E12
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170088E7 RID: 35047
			// (set) Token: 0x0600B82C RID: 47148 RVA: 0x00108C2A File Offset: 0x00106E2A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170088E8 RID: 35048
			// (set) Token: 0x0600B82D RID: 47149 RVA: 0x00108C42 File Offset: 0x00106E42
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170088E9 RID: 35049
			// (set) Token: 0x0600B82E RID: 47150 RVA: 0x00108C55 File Offset: 0x00106E55
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170088EA RID: 35050
			// (set) Token: 0x0600B82F RID: 47151 RVA: 0x00108C68 File Offset: 0x00106E68
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170088EB RID: 35051
			// (set) Token: 0x0600B830 RID: 47152 RVA: 0x00108C80 File Offset: 0x00106E80
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170088EC RID: 35052
			// (set) Token: 0x0600B831 RID: 47153 RVA: 0x00108C93 File Offset: 0x00106E93
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170088ED RID: 35053
			// (set) Token: 0x0600B832 RID: 47154 RVA: 0x00108CB1 File Offset: 0x00106EB1
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170088EE RID: 35054
			// (set) Token: 0x0600B833 RID: 47155 RVA: 0x00108CC4 File Offset: 0x00106EC4
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170088EF RID: 35055
			// (set) Token: 0x0600B834 RID: 47156 RVA: 0x00108CE2 File Offset: 0x00106EE2
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170088F0 RID: 35056
			// (set) Token: 0x0600B835 RID: 47157 RVA: 0x00108CF5 File Offset: 0x00106EF5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170088F1 RID: 35057
			// (set) Token: 0x0600B836 RID: 47158 RVA: 0x00108D08 File Offset: 0x00106F08
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170088F2 RID: 35058
			// (set) Token: 0x0600B837 RID: 47159 RVA: 0x00108D20 File Offset: 0x00106F20
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170088F3 RID: 35059
			// (set) Token: 0x0600B838 RID: 47160 RVA: 0x00108D38 File Offset: 0x00106F38
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170088F4 RID: 35060
			// (set) Token: 0x0600B839 RID: 47161 RVA: 0x00108D50 File Offset: 0x00106F50
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D7E RID: 3454
		public class ManagedBySetParameters : ParametersBase
		{
			// Token: 0x170088F5 RID: 35061
			// (set) Token: 0x0600B83B RID: 47163 RVA: 0x00108D70 File Offset: 0x00106F70
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170088F6 RID: 35062
			// (set) Token: 0x0600B83C RID: 47164 RVA: 0x00108D88 File Offset: 0x00106F88
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170088F7 RID: 35063
			// (set) Token: 0x0600B83D RID: 47165 RVA: 0x00108DA0 File Offset: 0x00106FA0
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170088F8 RID: 35064
			// (set) Token: 0x0600B83E RID: 47166 RVA: 0x00108DB8 File Offset: 0x00106FB8
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170088F9 RID: 35065
			// (set) Token: 0x0600B83F RID: 47167 RVA: 0x00108DCB File Offset: 0x00106FCB
			public virtual string ManagedBy
			{
				set
				{
					base.PowerSharpParameters["ManagedBy"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170088FA RID: 35066
			// (set) Token: 0x0600B840 RID: 47168 RVA: 0x00108DE9 File Offset: 0x00106FE9
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x170088FB RID: 35067
			// (set) Token: 0x0600B841 RID: 47169 RVA: 0x00108E01 File Offset: 0x00107001
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170088FC RID: 35068
			// (set) Token: 0x0600B842 RID: 47170 RVA: 0x00108E14 File Offset: 0x00107014
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170088FD RID: 35069
			// (set) Token: 0x0600B843 RID: 47171 RVA: 0x00108E32 File Offset: 0x00107032
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x170088FE RID: 35070
			// (set) Token: 0x0600B844 RID: 47172 RVA: 0x00108E45 File Offset: 0x00107045
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170088FF RID: 35071
			// (set) Token: 0x0600B845 RID: 47173 RVA: 0x00108E63 File Offset: 0x00107063
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17008900 RID: 35072
			// (set) Token: 0x0600B846 RID: 47174 RVA: 0x00108E76 File Offset: 0x00107076
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008901 RID: 35073
			// (set) Token: 0x0600B847 RID: 47175 RVA: 0x00108E89 File Offset: 0x00107089
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008902 RID: 35074
			// (set) Token: 0x0600B848 RID: 47176 RVA: 0x00108EA1 File Offset: 0x001070A1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008903 RID: 35075
			// (set) Token: 0x0600B849 RID: 47177 RVA: 0x00108EB9 File Offset: 0x001070B9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008904 RID: 35076
			// (set) Token: 0x0600B84A RID: 47178 RVA: 0x00108ED1 File Offset: 0x001070D1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D7F RID: 3455
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17008905 RID: 35077
			// (set) Token: 0x0600B84C RID: 47180 RVA: 0x00108EF1 File Offset: 0x001070F1
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17008906 RID: 35078
			// (set) Token: 0x0600B84D RID: 47181 RVA: 0x00108F09 File Offset: 0x00107109
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17008907 RID: 35079
			// (set) Token: 0x0600B84E RID: 47182 RVA: 0x00108F21 File Offset: 0x00107121
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17008908 RID: 35080
			// (set) Token: 0x0600B84F RID: 47183 RVA: 0x00108F39 File Offset: 0x00107139
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17008909 RID: 35081
			// (set) Token: 0x0600B850 RID: 47184 RVA: 0x00108F4C File Offset: 0x0010714C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DynamicGroupIdParameter(value) : null);
				}
			}

			// Token: 0x1700890A RID: 35082
			// (set) Token: 0x0600B851 RID: 47185 RVA: 0x00108F6A File Offset: 0x0010716A
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x1700890B RID: 35083
			// (set) Token: 0x0600B852 RID: 47186 RVA: 0x00108F82 File Offset: 0x00107182
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x1700890C RID: 35084
			// (set) Token: 0x0600B853 RID: 47187 RVA: 0x00108F95 File Offset: 0x00107195
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700890D RID: 35085
			// (set) Token: 0x0600B854 RID: 47188 RVA: 0x00108FB3 File Offset: 0x001071B3
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x1700890E RID: 35086
			// (set) Token: 0x0600B855 RID: 47189 RVA: 0x00108FC6 File Offset: 0x001071C6
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700890F RID: 35087
			// (set) Token: 0x0600B856 RID: 47190 RVA: 0x00108FE4 File Offset: 0x001071E4
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17008910 RID: 35088
			// (set) Token: 0x0600B857 RID: 47191 RVA: 0x00108FF7 File Offset: 0x001071F7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17008911 RID: 35089
			// (set) Token: 0x0600B858 RID: 47192 RVA: 0x0010900A File Offset: 0x0010720A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17008912 RID: 35090
			// (set) Token: 0x0600B859 RID: 47193 RVA: 0x00109022 File Offset: 0x00107222
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17008913 RID: 35091
			// (set) Token: 0x0600B85A RID: 47194 RVA: 0x0010903A File Offset: 0x0010723A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17008914 RID: 35092
			// (set) Token: 0x0600B85B RID: 47195 RVA: 0x00109052 File Offset: 0x00107252
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000D80 RID: 3456
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17008915 RID: 35093
			// (set) Token: 0x0600B85D RID: 47197 RVA: 0x00109072 File Offset: 0x00107272
			public virtual SwitchParameter IncludeSoftDeletedObjects
			{
				set
				{
					base.PowerSharpParameters["IncludeSoftDeletedObjects"] = value;
				}
			}

			// Token: 0x17008916 RID: 35094
			// (set) Token: 0x0600B85E RID: 47198 RVA: 0x0010908A File Offset: 0x0010728A
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17008917 RID: 35095
			// (set) Token: 0x0600B85F RID: 47199 RVA: 0x0010909D File Offset: 0x0010729D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17008918 RID: 35096
			// (set) Token: 0x0600B860 RID: 47200 RVA: 0x001090BB File Offset: 0x001072BB
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17008919 RID: 35097
			// (set) Token: 0x0600B861 RID: 47201 RVA: 0x001090CE File Offset: 0x001072CE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700891A RID: 35098
			// (set) Token: 0x0600B862 RID: 47202 RVA: 0x001090EC File Offset: 0x001072EC
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700891B RID: 35099
			// (set) Token: 0x0600B863 RID: 47203 RVA: 0x001090FF File Offset: 0x001072FF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700891C RID: 35100
			// (set) Token: 0x0600B864 RID: 47204 RVA: 0x00109112 File Offset: 0x00107312
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700891D RID: 35101
			// (set) Token: 0x0600B865 RID: 47205 RVA: 0x0010912A File Offset: 0x0010732A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700891E RID: 35102
			// (set) Token: 0x0600B866 RID: 47206 RVA: 0x00109142 File Offset: 0x00107342
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700891F RID: 35103
			// (set) Token: 0x0600B867 RID: 47207 RVA: 0x0010915A File Offset: 0x0010735A
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
