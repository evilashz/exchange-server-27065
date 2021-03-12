using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C05 RID: 3077
	public class GetGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x0600954F RID: 38223 RVA: 0x000D9871 File Offset: 0x000D7A71
		private GetGroupCommand() : base("Get-Group")
		{
		}

		// Token: 0x06009550 RID: 38224 RVA: 0x000D987E File Offset: 0x000D7A7E
		public GetGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06009551 RID: 38225 RVA: 0x000D988D File Offset: 0x000D7A8D
		public virtual GetGroupCommand SetParameters(GetGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009552 RID: 38226 RVA: 0x000D9897 File Offset: 0x000D7A97
		public virtual GetGroupCommand SetParameters(GetGroupCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06009553 RID: 38227 RVA: 0x000D98A1 File Offset: 0x000D7AA1
		public virtual GetGroupCommand SetParameters(GetGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C06 RID: 3078
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170068FE RID: 26878
			// (set) Token: 0x06009554 RID: 38228 RVA: 0x000D98AB File Offset: 0x000D7AAB
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x170068FF RID: 26879
			// (set) Token: 0x06009555 RID: 38229 RVA: 0x000D98C3 File Offset: 0x000D7AC3
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006900 RID: 26880
			// (set) Token: 0x06009556 RID: 38230 RVA: 0x000D98D6 File Offset: 0x000D7AD6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006901 RID: 26881
			// (set) Token: 0x06009557 RID: 38231 RVA: 0x000D98F4 File Offset: 0x000D7AF4
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006902 RID: 26882
			// (set) Token: 0x06009558 RID: 38232 RVA: 0x000D9907 File Offset: 0x000D7B07
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006903 RID: 26883
			// (set) Token: 0x06009559 RID: 38233 RVA: 0x000D991A File Offset: 0x000D7B1A
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006904 RID: 26884
			// (set) Token: 0x0600955A RID: 38234 RVA: 0x000D9938 File Offset: 0x000D7B38
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006905 RID: 26885
			// (set) Token: 0x0600955B RID: 38235 RVA: 0x000D9950 File Offset: 0x000D7B50
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006906 RID: 26886
			// (set) Token: 0x0600955C RID: 38236 RVA: 0x000D9963 File Offset: 0x000D7B63
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006907 RID: 26887
			// (set) Token: 0x0600955D RID: 38237 RVA: 0x000D997B File Offset: 0x000D7B7B
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006908 RID: 26888
			// (set) Token: 0x0600955E RID: 38238 RVA: 0x000D9993 File Offset: 0x000D7B93
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006909 RID: 26889
			// (set) Token: 0x0600955F RID: 38239 RVA: 0x000D99A6 File Offset: 0x000D7BA6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700690A RID: 26890
			// (set) Token: 0x06009560 RID: 38240 RVA: 0x000D99BE File Offset: 0x000D7BBE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700690B RID: 26891
			// (set) Token: 0x06009561 RID: 38241 RVA: 0x000D99D6 File Offset: 0x000D7BD6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700690C RID: 26892
			// (set) Token: 0x06009562 RID: 38242 RVA: 0x000D99EE File Offset: 0x000D7BEE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C07 RID: 3079
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x1700690D RID: 26893
			// (set) Token: 0x06009564 RID: 38244 RVA: 0x000D9A0E File Offset: 0x000D7C0E
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x1700690E RID: 26894
			// (set) Token: 0x06009565 RID: 38245 RVA: 0x000D9A21 File Offset: 0x000D7C21
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700690F RID: 26895
			// (set) Token: 0x06009566 RID: 38246 RVA: 0x000D9A39 File Offset: 0x000D7C39
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006910 RID: 26896
			// (set) Token: 0x06009567 RID: 38247 RVA: 0x000D9A4C File Offset: 0x000D7C4C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006911 RID: 26897
			// (set) Token: 0x06009568 RID: 38248 RVA: 0x000D9A6A File Offset: 0x000D7C6A
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006912 RID: 26898
			// (set) Token: 0x06009569 RID: 38249 RVA: 0x000D9A7D File Offset: 0x000D7C7D
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006913 RID: 26899
			// (set) Token: 0x0600956A RID: 38250 RVA: 0x000D9A90 File Offset: 0x000D7C90
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006914 RID: 26900
			// (set) Token: 0x0600956B RID: 38251 RVA: 0x000D9AAE File Offset: 0x000D7CAE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006915 RID: 26901
			// (set) Token: 0x0600956C RID: 38252 RVA: 0x000D9AC6 File Offset: 0x000D7CC6
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006916 RID: 26902
			// (set) Token: 0x0600956D RID: 38253 RVA: 0x000D9AD9 File Offset: 0x000D7CD9
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006917 RID: 26903
			// (set) Token: 0x0600956E RID: 38254 RVA: 0x000D9AF1 File Offset: 0x000D7CF1
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006918 RID: 26904
			// (set) Token: 0x0600956F RID: 38255 RVA: 0x000D9B09 File Offset: 0x000D7D09
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006919 RID: 26905
			// (set) Token: 0x06009570 RID: 38256 RVA: 0x000D9B1C File Offset: 0x000D7D1C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700691A RID: 26906
			// (set) Token: 0x06009571 RID: 38257 RVA: 0x000D9B34 File Offset: 0x000D7D34
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700691B RID: 26907
			// (set) Token: 0x06009572 RID: 38258 RVA: 0x000D9B4C File Offset: 0x000D7D4C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700691C RID: 26908
			// (set) Token: 0x06009573 RID: 38259 RVA: 0x000D9B64 File Offset: 0x000D7D64
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C08 RID: 3080
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700691D RID: 26909
			// (set) Token: 0x06009575 RID: 38261 RVA: 0x000D9B84 File Offset: 0x000D7D84
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new GroupIdParameter(value) : null);
				}
			}

			// Token: 0x1700691E RID: 26910
			// (set) Token: 0x06009576 RID: 38262 RVA: 0x000D9BA2 File Offset: 0x000D7DA2
			public virtual RecipientTypeDetails RecipientTypeDetails
			{
				set
				{
					base.PowerSharpParameters["RecipientTypeDetails"] = value;
				}
			}

			// Token: 0x1700691F RID: 26911
			// (set) Token: 0x06009577 RID: 38263 RVA: 0x000D9BBA File Offset: 0x000D7DBA
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006920 RID: 26912
			// (set) Token: 0x06009578 RID: 38264 RVA: 0x000D9BCD File Offset: 0x000D7DCD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006921 RID: 26913
			// (set) Token: 0x06009579 RID: 38265 RVA: 0x000D9BEB File Offset: 0x000D7DEB
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006922 RID: 26914
			// (set) Token: 0x0600957A RID: 38266 RVA: 0x000D9BFE File Offset: 0x000D7DFE
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006923 RID: 26915
			// (set) Token: 0x0600957B RID: 38267 RVA: 0x000D9C11 File Offset: 0x000D7E11
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006924 RID: 26916
			// (set) Token: 0x0600957C RID: 38268 RVA: 0x000D9C2F File Offset: 0x000D7E2F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006925 RID: 26917
			// (set) Token: 0x0600957D RID: 38269 RVA: 0x000D9C47 File Offset: 0x000D7E47
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006926 RID: 26918
			// (set) Token: 0x0600957E RID: 38270 RVA: 0x000D9C5A File Offset: 0x000D7E5A
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006927 RID: 26919
			// (set) Token: 0x0600957F RID: 38271 RVA: 0x000D9C72 File Offset: 0x000D7E72
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006928 RID: 26920
			// (set) Token: 0x06009580 RID: 38272 RVA: 0x000D9C8A File Offset: 0x000D7E8A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006929 RID: 26921
			// (set) Token: 0x06009581 RID: 38273 RVA: 0x000D9C9D File Offset: 0x000D7E9D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700692A RID: 26922
			// (set) Token: 0x06009582 RID: 38274 RVA: 0x000D9CB5 File Offset: 0x000D7EB5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700692B RID: 26923
			// (set) Token: 0x06009583 RID: 38275 RVA: 0x000D9CCD File Offset: 0x000D7ECD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700692C RID: 26924
			// (set) Token: 0x06009584 RID: 38276 RVA: 0x000D9CE5 File Offset: 0x000D7EE5
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
