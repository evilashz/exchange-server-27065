using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000872 RID: 2162
	public class GetThrottlingPolicyAssociationCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x06006AFD RID: 27389 RVA: 0x000A2362 File Offset: 0x000A0562
		private GetThrottlingPolicyAssociationCommand() : base("Get-ThrottlingPolicyAssociation")
		{
		}

		// Token: 0x06006AFE RID: 27390 RVA: 0x000A236F File Offset: 0x000A056F
		public GetThrottlingPolicyAssociationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006AFF RID: 27391 RVA: 0x000A237E File Offset: 0x000A057E
		public virtual GetThrottlingPolicyAssociationCommand SetParameters(GetThrottlingPolicyAssociationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006B00 RID: 27392 RVA: 0x000A2388 File Offset: 0x000A0588
		public virtual GetThrottlingPolicyAssociationCommand SetParameters(GetThrottlingPolicyAssociationCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006B01 RID: 27393 RVA: 0x000A2392 File Offset: 0x000A0592
		public virtual GetThrottlingPolicyAssociationCommand SetParameters(GetThrottlingPolicyAssociationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000873 RID: 2163
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170045D2 RID: 17874
			// (set) Token: 0x06006B02 RID: 27394 RVA: 0x000A239C File Offset: 0x000A059C
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170045D3 RID: 17875
			// (set) Token: 0x06006B03 RID: 27395 RVA: 0x000A23BA File Offset: 0x000A05BA
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170045D4 RID: 17876
			// (set) Token: 0x06006B04 RID: 27396 RVA: 0x000A23CD File Offset: 0x000A05CD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170045D5 RID: 17877
			// (set) Token: 0x06006B05 RID: 27397 RVA: 0x000A23EB File Offset: 0x000A05EB
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170045D6 RID: 17878
			// (set) Token: 0x06006B06 RID: 27398 RVA: 0x000A23FE File Offset: 0x000A05FE
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170045D7 RID: 17879
			// (set) Token: 0x06006B07 RID: 27399 RVA: 0x000A241C File Offset: 0x000A061C
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170045D8 RID: 17880
			// (set) Token: 0x06006B08 RID: 27400 RVA: 0x000A2434 File Offset: 0x000A0634
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170045D9 RID: 17881
			// (set) Token: 0x06006B09 RID: 27401 RVA: 0x000A2447 File Offset: 0x000A0647
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170045DA RID: 17882
			// (set) Token: 0x06006B0A RID: 27402 RVA: 0x000A245F File Offset: 0x000A065F
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170045DB RID: 17883
			// (set) Token: 0x06006B0B RID: 27403 RVA: 0x000A2477 File Offset: 0x000A0677
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170045DC RID: 17884
			// (set) Token: 0x06006B0C RID: 27404 RVA: 0x000A248A File Offset: 0x000A068A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170045DD RID: 17885
			// (set) Token: 0x06006B0D RID: 27405 RVA: 0x000A24A2 File Offset: 0x000A06A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170045DE RID: 17886
			// (set) Token: 0x06006B0E RID: 27406 RVA: 0x000A24BA File Offset: 0x000A06BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170045DF RID: 17887
			// (set) Token: 0x06006B0F RID: 27407 RVA: 0x000A24D2 File Offset: 0x000A06D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000874 RID: 2164
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x170045E0 RID: 17888
			// (set) Token: 0x06006B11 RID: 27409 RVA: 0x000A24F2 File Offset: 0x000A06F2
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x170045E1 RID: 17889
			// (set) Token: 0x06006B12 RID: 27410 RVA: 0x000A2505 File Offset: 0x000A0705
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170045E2 RID: 17890
			// (set) Token: 0x06006B13 RID: 27411 RVA: 0x000A2523 File Offset: 0x000A0723
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170045E3 RID: 17891
			// (set) Token: 0x06006B14 RID: 27412 RVA: 0x000A2536 File Offset: 0x000A0736
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170045E4 RID: 17892
			// (set) Token: 0x06006B15 RID: 27413 RVA: 0x000A2554 File Offset: 0x000A0754
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170045E5 RID: 17893
			// (set) Token: 0x06006B16 RID: 27414 RVA: 0x000A2567 File Offset: 0x000A0767
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170045E6 RID: 17894
			// (set) Token: 0x06006B17 RID: 27415 RVA: 0x000A2585 File Offset: 0x000A0785
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170045E7 RID: 17895
			// (set) Token: 0x06006B18 RID: 27416 RVA: 0x000A259D File Offset: 0x000A079D
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170045E8 RID: 17896
			// (set) Token: 0x06006B19 RID: 27417 RVA: 0x000A25B0 File Offset: 0x000A07B0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170045E9 RID: 17897
			// (set) Token: 0x06006B1A RID: 27418 RVA: 0x000A25C8 File Offset: 0x000A07C8
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170045EA RID: 17898
			// (set) Token: 0x06006B1B RID: 27419 RVA: 0x000A25E0 File Offset: 0x000A07E0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170045EB RID: 17899
			// (set) Token: 0x06006B1C RID: 27420 RVA: 0x000A25F3 File Offset: 0x000A07F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170045EC RID: 17900
			// (set) Token: 0x06006B1D RID: 27421 RVA: 0x000A260B File Offset: 0x000A080B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170045ED RID: 17901
			// (set) Token: 0x06006B1E RID: 27422 RVA: 0x000A2623 File Offset: 0x000A0823
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170045EE RID: 17902
			// (set) Token: 0x06006B1F RID: 27423 RVA: 0x000A263B File Offset: 0x000A083B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000875 RID: 2165
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170045EF RID: 17903
			// (set) Token: 0x06006B21 RID: 27425 RVA: 0x000A265B File Offset: 0x000A085B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ThrottlingPolicyAssociationIdParameter(value) : null);
				}
			}

			// Token: 0x170045F0 RID: 17904
			// (set) Token: 0x06006B22 RID: 27426 RVA: 0x000A2679 File Offset: 0x000A0879
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170045F1 RID: 17905
			// (set) Token: 0x06006B23 RID: 27427 RVA: 0x000A2697 File Offset: 0x000A0897
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x170045F2 RID: 17906
			// (set) Token: 0x06006B24 RID: 27428 RVA: 0x000A26AA File Offset: 0x000A08AA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170045F3 RID: 17907
			// (set) Token: 0x06006B25 RID: 27429 RVA: 0x000A26C8 File Offset: 0x000A08C8
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x170045F4 RID: 17908
			// (set) Token: 0x06006B26 RID: 27430 RVA: 0x000A26DB File Offset: 0x000A08DB
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170045F5 RID: 17909
			// (set) Token: 0x06006B27 RID: 27431 RVA: 0x000A26F9 File Offset: 0x000A08F9
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170045F6 RID: 17910
			// (set) Token: 0x06006B28 RID: 27432 RVA: 0x000A2711 File Offset: 0x000A0911
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x170045F7 RID: 17911
			// (set) Token: 0x06006B29 RID: 27433 RVA: 0x000A2724 File Offset: 0x000A0924
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x170045F8 RID: 17912
			// (set) Token: 0x06006B2A RID: 27434 RVA: 0x000A273C File Offset: 0x000A093C
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x170045F9 RID: 17913
			// (set) Token: 0x06006B2B RID: 27435 RVA: 0x000A2754 File Offset: 0x000A0954
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170045FA RID: 17914
			// (set) Token: 0x06006B2C RID: 27436 RVA: 0x000A2767 File Offset: 0x000A0967
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170045FB RID: 17915
			// (set) Token: 0x06006B2D RID: 27437 RVA: 0x000A277F File Offset: 0x000A097F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170045FC RID: 17916
			// (set) Token: 0x06006B2E RID: 27438 RVA: 0x000A2797 File Offset: 0x000A0997
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170045FD RID: 17917
			// (set) Token: 0x06006B2F RID: 27439 RVA: 0x000A27AF File Offset: 0x000A09AF
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
