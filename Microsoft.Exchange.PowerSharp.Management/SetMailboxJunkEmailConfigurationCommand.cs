using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200049E RID: 1182
	public class SetMailboxJunkEmailConfigurationCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxJunkEmailConfiguration>
	{
		// Token: 0x06004262 RID: 16994 RVA: 0x0006DE12 File Offset: 0x0006C012
		private SetMailboxJunkEmailConfigurationCommand() : base("Set-MailboxJunkEmailConfiguration")
		{
		}

		// Token: 0x06004263 RID: 16995 RVA: 0x0006DE1F File Offset: 0x0006C01F
		public SetMailboxJunkEmailConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x0006DE2E File Offset: 0x0006C02E
		public virtual SetMailboxJunkEmailConfigurationCommand SetParameters(SetMailboxJunkEmailConfigurationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x0006DE38 File Offset: 0x0006C038
		public virtual SetMailboxJunkEmailConfigurationCommand SetParameters(SetMailboxJunkEmailConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200049F RID: 1183
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170024DF RID: 9439
			// (set) Token: 0x06004266 RID: 16998 RVA: 0x0006DE42 File Offset: 0x0006C042
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170024E0 RID: 9440
			// (set) Token: 0x06004267 RID: 16999 RVA: 0x0006DE60 File Offset: 0x0006C060
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170024E1 RID: 9441
			// (set) Token: 0x06004268 RID: 17000 RVA: 0x0006DE78 File Offset: 0x0006C078
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024E2 RID: 9442
			// (set) Token: 0x06004269 RID: 17001 RVA: 0x0006DE8B File Offset: 0x0006C08B
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170024E3 RID: 9443
			// (set) Token: 0x0600426A RID: 17002 RVA: 0x0006DEA3 File Offset: 0x0006C0A3
			public virtual bool TrustedListsOnly
			{
				set
				{
					base.PowerSharpParameters["TrustedListsOnly"] = value;
				}
			}

			// Token: 0x170024E4 RID: 9444
			// (set) Token: 0x0600426B RID: 17003 RVA: 0x0006DEBB File Offset: 0x0006C0BB
			public virtual bool ContactsTrusted
			{
				set
				{
					base.PowerSharpParameters["ContactsTrusted"] = value;
				}
			}

			// Token: 0x170024E5 RID: 9445
			// (set) Token: 0x0600426C RID: 17004 RVA: 0x0006DED3 File Offset: 0x0006C0D3
			public virtual MultiValuedProperty<string> TrustedSendersAndDomains
			{
				set
				{
					base.PowerSharpParameters["TrustedSendersAndDomains"] = value;
				}
			}

			// Token: 0x170024E6 RID: 9446
			// (set) Token: 0x0600426D RID: 17005 RVA: 0x0006DEE6 File Offset: 0x0006C0E6
			public virtual MultiValuedProperty<string> BlockedSendersAndDomains
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersAndDomains"] = value;
				}
			}

			// Token: 0x170024E7 RID: 9447
			// (set) Token: 0x0600426E RID: 17006 RVA: 0x0006DEF9 File Offset: 0x0006C0F9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024E8 RID: 9448
			// (set) Token: 0x0600426F RID: 17007 RVA: 0x0006DF11 File Offset: 0x0006C111
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024E9 RID: 9449
			// (set) Token: 0x06004270 RID: 17008 RVA: 0x0006DF29 File Offset: 0x0006C129
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024EA RID: 9450
			// (set) Token: 0x06004271 RID: 17009 RVA: 0x0006DF41 File Offset: 0x0006C141
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170024EB RID: 9451
			// (set) Token: 0x06004272 RID: 17010 RVA: 0x0006DF59 File Offset: 0x0006C159
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020004A0 RID: 1184
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170024EC RID: 9452
			// (set) Token: 0x06004274 RID: 17012 RVA: 0x0006DF79 File Offset: 0x0006C179
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170024ED RID: 9453
			// (set) Token: 0x06004275 RID: 17013 RVA: 0x0006DF91 File Offset: 0x0006C191
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170024EE RID: 9454
			// (set) Token: 0x06004276 RID: 17014 RVA: 0x0006DFA4 File Offset: 0x0006C1A4
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170024EF RID: 9455
			// (set) Token: 0x06004277 RID: 17015 RVA: 0x0006DFBC File Offset: 0x0006C1BC
			public virtual bool TrustedListsOnly
			{
				set
				{
					base.PowerSharpParameters["TrustedListsOnly"] = value;
				}
			}

			// Token: 0x170024F0 RID: 9456
			// (set) Token: 0x06004278 RID: 17016 RVA: 0x0006DFD4 File Offset: 0x0006C1D4
			public virtual bool ContactsTrusted
			{
				set
				{
					base.PowerSharpParameters["ContactsTrusted"] = value;
				}
			}

			// Token: 0x170024F1 RID: 9457
			// (set) Token: 0x06004279 RID: 17017 RVA: 0x0006DFEC File Offset: 0x0006C1EC
			public virtual MultiValuedProperty<string> TrustedSendersAndDomains
			{
				set
				{
					base.PowerSharpParameters["TrustedSendersAndDomains"] = value;
				}
			}

			// Token: 0x170024F2 RID: 9458
			// (set) Token: 0x0600427A RID: 17018 RVA: 0x0006DFFF File Offset: 0x0006C1FF
			public virtual MultiValuedProperty<string> BlockedSendersAndDomains
			{
				set
				{
					base.PowerSharpParameters["BlockedSendersAndDomains"] = value;
				}
			}

			// Token: 0x170024F3 RID: 9459
			// (set) Token: 0x0600427B RID: 17019 RVA: 0x0006E012 File Offset: 0x0006C212
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170024F4 RID: 9460
			// (set) Token: 0x0600427C RID: 17020 RVA: 0x0006E02A File Offset: 0x0006C22A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170024F5 RID: 9461
			// (set) Token: 0x0600427D RID: 17021 RVA: 0x0006E042 File Offset: 0x0006C242
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170024F6 RID: 9462
			// (set) Token: 0x0600427E RID: 17022 RVA: 0x0006E05A File Offset: 0x0006C25A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170024F7 RID: 9463
			// (set) Token: 0x0600427F RID: 17023 RVA: 0x0006E072 File Offset: 0x0006C272
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
