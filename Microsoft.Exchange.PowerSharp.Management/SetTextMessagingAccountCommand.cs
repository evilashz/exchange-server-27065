using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000473 RID: 1139
	public class SetTextMessagingAccountCommand : SyntheticCommandWithPipelineInputNoOutput<TextMessagingAccount>
	{
		// Token: 0x060040C7 RID: 16583 RVA: 0x0006BCFC File Offset: 0x00069EFC
		private SetTextMessagingAccountCommand() : base("Set-TextMessagingAccount")
		{
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x0006BD09 File Offset: 0x00069F09
		public SetTextMessagingAccountCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x0006BD18 File Offset: 0x00069F18
		public virtual SetTextMessagingAccountCommand SetParameters(SetTextMessagingAccountCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0006BD22 File Offset: 0x00069F22
		public virtual SetTextMessagingAccountCommand SetParameters(SetTextMessagingAccountCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000474 RID: 1140
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700239A RID: 9114
			// (set) Token: 0x060040CB RID: 16587 RVA: 0x0006BD2C File Offset: 0x00069F2C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700239B RID: 9115
			// (set) Token: 0x060040CC RID: 16588 RVA: 0x0006BD4A File Offset: 0x00069F4A
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700239C RID: 9116
			// (set) Token: 0x060040CD RID: 16589 RVA: 0x0006BD62 File Offset: 0x00069F62
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700239D RID: 9117
			// (set) Token: 0x060040CE RID: 16590 RVA: 0x0006BD75 File Offset: 0x00069F75
			public virtual RegionInfo CountryRegionId
			{
				set
				{
					base.PowerSharpParameters["CountryRegionId"] = value;
				}
			}

			// Token: 0x1700239E RID: 9118
			// (set) Token: 0x060040CF RID: 16591 RVA: 0x0006BD88 File Offset: 0x00069F88
			public virtual int MobileOperatorId
			{
				set
				{
					base.PowerSharpParameters["MobileOperatorId"] = value;
				}
			}

			// Token: 0x1700239F RID: 9119
			// (set) Token: 0x060040D0 RID: 16592 RVA: 0x0006BDA0 File Offset: 0x00069FA0
			public virtual E164Number NotificationPhoneNumber
			{
				set
				{
					base.PowerSharpParameters["NotificationPhoneNumber"] = value;
				}
			}

			// Token: 0x170023A0 RID: 9120
			// (set) Token: 0x060040D1 RID: 16593 RVA: 0x0006BDB3 File Offset: 0x00069FB3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023A1 RID: 9121
			// (set) Token: 0x060040D2 RID: 16594 RVA: 0x0006BDCB File Offset: 0x00069FCB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023A2 RID: 9122
			// (set) Token: 0x060040D3 RID: 16595 RVA: 0x0006BDE3 File Offset: 0x00069FE3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023A3 RID: 9123
			// (set) Token: 0x060040D4 RID: 16596 RVA: 0x0006BDFB File Offset: 0x00069FFB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170023A4 RID: 9124
			// (set) Token: 0x060040D5 RID: 16597 RVA: 0x0006BE13 File Offset: 0x0006A013
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000475 RID: 1141
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170023A5 RID: 9125
			// (set) Token: 0x060040D7 RID: 16599 RVA: 0x0006BE33 File Offset: 0x0006A033
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170023A6 RID: 9126
			// (set) Token: 0x060040D8 RID: 16600 RVA: 0x0006BE4B File Offset: 0x0006A04B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170023A7 RID: 9127
			// (set) Token: 0x060040D9 RID: 16601 RVA: 0x0006BE5E File Offset: 0x0006A05E
			public virtual RegionInfo CountryRegionId
			{
				set
				{
					base.PowerSharpParameters["CountryRegionId"] = value;
				}
			}

			// Token: 0x170023A8 RID: 9128
			// (set) Token: 0x060040DA RID: 16602 RVA: 0x0006BE71 File Offset: 0x0006A071
			public virtual int MobileOperatorId
			{
				set
				{
					base.PowerSharpParameters["MobileOperatorId"] = value;
				}
			}

			// Token: 0x170023A9 RID: 9129
			// (set) Token: 0x060040DB RID: 16603 RVA: 0x0006BE89 File Offset: 0x0006A089
			public virtual E164Number NotificationPhoneNumber
			{
				set
				{
					base.PowerSharpParameters["NotificationPhoneNumber"] = value;
				}
			}

			// Token: 0x170023AA RID: 9130
			// (set) Token: 0x060040DC RID: 16604 RVA: 0x0006BE9C File Offset: 0x0006A09C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170023AB RID: 9131
			// (set) Token: 0x060040DD RID: 16605 RVA: 0x0006BEB4 File Offset: 0x0006A0B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170023AC RID: 9132
			// (set) Token: 0x060040DE RID: 16606 RVA: 0x0006BECC File Offset: 0x0006A0CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170023AD RID: 9133
			// (set) Token: 0x060040DF RID: 16607 RVA: 0x0006BEE4 File Offset: 0x0006A0E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170023AE RID: 9134
			// (set) Token: 0x060040E0 RID: 16608 RVA: 0x0006BEFC File Offset: 0x0006A0FC
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
