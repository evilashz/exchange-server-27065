using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000527 RID: 1319
	public class NewClientAccessRuleCommand : SyntheticCommandWithPipelineInput<ADClientAccessRule, ADClientAccessRule>
	{
		// Token: 0x060046D2 RID: 18130 RVA: 0x00073631 File Offset: 0x00071831
		private NewClientAccessRuleCommand() : base("New-ClientAccessRule")
		{
		}

		// Token: 0x060046D3 RID: 18131 RVA: 0x0007363E File Offset: 0x0007183E
		public NewClientAccessRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x0007364D File Offset: 0x0007184D
		public virtual NewClientAccessRuleCommand SetParameters(NewClientAccessRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000528 RID: 1320
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700283D RID: 10301
			// (set) Token: 0x060046D5 RID: 18133 RVA: 0x00073657 File Offset: 0x00071857
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700283E RID: 10302
			// (set) Token: 0x060046D6 RID: 18134 RVA: 0x0007366F File Offset: 0x0007186F
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700283F RID: 10303
			// (set) Token: 0x060046D7 RID: 18135 RVA: 0x00073687 File Offset: 0x00071887
			public virtual bool DatacenterAdminsOnly
			{
				set
				{
					base.PowerSharpParameters["DatacenterAdminsOnly"] = value;
				}
			}

			// Token: 0x17002840 RID: 10304
			// (set) Token: 0x060046D8 RID: 18136 RVA: 0x0007369F File Offset: 0x0007189F
			public virtual ClientAccessRulesAction Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17002841 RID: 10305
			// (set) Token: 0x060046D9 RID: 18137 RVA: 0x000736B7 File Offset: 0x000718B7
			public virtual MultiValuedProperty<IPRange> AnyOfClientIPAddressesOrRanges
			{
				set
				{
					base.PowerSharpParameters["AnyOfClientIPAddressesOrRanges"] = value;
				}
			}

			// Token: 0x17002842 RID: 10306
			// (set) Token: 0x060046DA RID: 18138 RVA: 0x000736CA File Offset: 0x000718CA
			public virtual MultiValuedProperty<IPRange> ExceptAnyOfClientIPAddressesOrRanges
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfClientIPAddressesOrRanges"] = value;
				}
			}

			// Token: 0x17002843 RID: 10307
			// (set) Token: 0x060046DB RID: 18139 RVA: 0x000736DD File Offset: 0x000718DD
			public virtual MultiValuedProperty<IntRange> AnyOfSourceTcpPortNumbers
			{
				set
				{
					base.PowerSharpParameters["AnyOfSourceTcpPortNumbers"] = value;
				}
			}

			// Token: 0x17002844 RID: 10308
			// (set) Token: 0x060046DC RID: 18140 RVA: 0x000736F0 File Offset: 0x000718F0
			public virtual MultiValuedProperty<IntRange> ExceptAnyOfSourceTcpPortNumbers
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfSourceTcpPortNumbers"] = value;
				}
			}

			// Token: 0x17002845 RID: 10309
			// (set) Token: 0x060046DD RID: 18141 RVA: 0x00073703 File Offset: 0x00071903
			public virtual MultiValuedProperty<string> UsernameMatchesAnyOfPatterns
			{
				set
				{
					base.PowerSharpParameters["UsernameMatchesAnyOfPatterns"] = value;
				}
			}

			// Token: 0x17002846 RID: 10310
			// (set) Token: 0x060046DE RID: 18142 RVA: 0x00073716 File Offset: 0x00071916
			public virtual MultiValuedProperty<string> ExceptUsernameMatchesAnyOfPatterns
			{
				set
				{
					base.PowerSharpParameters["ExceptUsernameMatchesAnyOfPatterns"] = value;
				}
			}

			// Token: 0x17002847 RID: 10311
			// (set) Token: 0x060046DF RID: 18143 RVA: 0x00073729 File Offset: 0x00071929
			public virtual MultiValuedProperty<string> UserIsMemberOf
			{
				set
				{
					base.PowerSharpParameters["UserIsMemberOf"] = value;
				}
			}

			// Token: 0x17002848 RID: 10312
			// (set) Token: 0x060046E0 RID: 18144 RVA: 0x0007373C File Offset: 0x0007193C
			public virtual MultiValuedProperty<string> ExceptUserIsMemberOf
			{
				set
				{
					base.PowerSharpParameters["ExceptUserIsMemberOf"] = value;
				}
			}

			// Token: 0x17002849 RID: 10313
			// (set) Token: 0x060046E1 RID: 18145 RVA: 0x0007374F File Offset: 0x0007194F
			public virtual MultiValuedProperty<ClientAccessAuthenticationMethod> AnyOfAuthenticationTypes
			{
				set
				{
					base.PowerSharpParameters["AnyOfAuthenticationTypes"] = value;
				}
			}

			// Token: 0x1700284A RID: 10314
			// (set) Token: 0x060046E2 RID: 18146 RVA: 0x00073762 File Offset: 0x00071962
			public virtual MultiValuedProperty<ClientAccessAuthenticationMethod> ExceptAnyOfAuthenticationTypes
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfAuthenticationTypes"] = value;
				}
			}

			// Token: 0x1700284B RID: 10315
			// (set) Token: 0x060046E3 RID: 18147 RVA: 0x00073775 File Offset: 0x00071975
			public virtual MultiValuedProperty<ClientAccessProtocol> AnyOfProtocols
			{
				set
				{
					base.PowerSharpParameters["AnyOfProtocols"] = value;
				}
			}

			// Token: 0x1700284C RID: 10316
			// (set) Token: 0x060046E4 RID: 18148 RVA: 0x00073788 File Offset: 0x00071988
			public virtual MultiValuedProperty<ClientAccessProtocol> ExceptAnyOfProtocols
			{
				set
				{
					base.PowerSharpParameters["ExceptAnyOfProtocols"] = value;
				}
			}

			// Token: 0x1700284D RID: 10317
			// (set) Token: 0x060046E5 RID: 18149 RVA: 0x0007379B File Offset: 0x0007199B
			public virtual string UserRecipientFilter
			{
				set
				{
					base.PowerSharpParameters["UserRecipientFilter"] = value;
				}
			}

			// Token: 0x1700284E RID: 10318
			// (set) Token: 0x060046E6 RID: 18150 RVA: 0x000737AE File Offset: 0x000719AE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700284F RID: 10319
			// (set) Token: 0x060046E7 RID: 18151 RVA: 0x000737CC File Offset: 0x000719CC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002850 RID: 10320
			// (set) Token: 0x060046E8 RID: 18152 RVA: 0x000737DF File Offset: 0x000719DF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002851 RID: 10321
			// (set) Token: 0x060046E9 RID: 18153 RVA: 0x000737F2 File Offset: 0x000719F2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002852 RID: 10322
			// (set) Token: 0x060046EA RID: 18154 RVA: 0x0007380A File Offset: 0x00071A0A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002853 RID: 10323
			// (set) Token: 0x060046EB RID: 18155 RVA: 0x00073822 File Offset: 0x00071A22
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002854 RID: 10324
			// (set) Token: 0x060046EC RID: 18156 RVA: 0x0007383A File Offset: 0x00071A3A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002855 RID: 10325
			// (set) Token: 0x060046ED RID: 18157 RVA: 0x00073852 File Offset: 0x00071A52
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
