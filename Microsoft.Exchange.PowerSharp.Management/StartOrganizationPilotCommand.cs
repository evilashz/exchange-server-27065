using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000C9 RID: 201
	public class StartOrganizationPilotCommand : SyntheticCommandWithPipelineInputNoOutput<OrganizationIdParameter>
	{
		// Token: 0x06001CAE RID: 7342 RVA: 0x0003CF41 File Offset: 0x0003B141
		private StartOrganizationPilotCommand() : base("Start-OrganizationPilot")
		{
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0003CF4E File Offset: 0x0003B14E
		public StartOrganizationPilotCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0003CF5D File Offset: 0x0003B15D
		public virtual StartOrganizationPilotCommand SetParameters(StartOrganizationPilotCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0003CF67 File Offset: 0x0003B167
		public virtual StartOrganizationPilotCommand SetParameters(StartOrganizationPilotCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000CA RID: 202
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170006D5 RID: 1749
			// (set) Token: 0x06001CB2 RID: 7346 RVA: 0x0003CF71 File Offset: 0x0003B171
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170006D6 RID: 1750
			// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x0003CF8F File Offset: 0x0003B18F
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x170006D7 RID: 1751
			// (set) Token: 0x06001CB4 RID: 7348 RVA: 0x0003CFA7 File Offset: 0x0003B1A7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170006D8 RID: 1752
			// (set) Token: 0x06001CB5 RID: 7349 RVA: 0x0003CFBA File Offset: 0x0003B1BA
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170006D9 RID: 1753
			// (set) Token: 0x06001CB6 RID: 7350 RVA: 0x0003CFD2 File Offset: 0x0003B1D2
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170006DA RID: 1754
			// (set) Token: 0x06001CB7 RID: 7351 RVA: 0x0003CFEA File Offset: 0x0003B1EA
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170006DB RID: 1755
			// (set) Token: 0x06001CB8 RID: 7352 RVA: 0x0003D002 File Offset: 0x0003B202
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170006DC RID: 1756
			// (set) Token: 0x06001CB9 RID: 7353 RVA: 0x0003D01A File Offset: 0x0003B21A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170006DD RID: 1757
			// (set) Token: 0x06001CBA RID: 7354 RVA: 0x0003D032 File Offset: 0x0003B232
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170006DE RID: 1758
			// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0003D04A File Offset: 0x0003B24A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170006DF RID: 1759
			// (set) Token: 0x06001CBC RID: 7356 RVA: 0x0003D062 File Offset: 0x0003B262
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170006E0 RID: 1760
			// (set) Token: 0x06001CBD RID: 7357 RVA: 0x0003D07A File Offset: 0x0003B27A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020000CB RID: 203
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170006E1 RID: 1761
			// (set) Token: 0x06001CBF RID: 7359 RVA: 0x0003D09A File Offset: 0x0003B29A
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x170006E2 RID: 1762
			// (set) Token: 0x06001CC0 RID: 7360 RVA: 0x0003D0B2 File Offset: 0x0003B2B2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170006E3 RID: 1763
			// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x0003D0C5 File Offset: 0x0003B2C5
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170006E4 RID: 1764
			// (set) Token: 0x06001CC2 RID: 7362 RVA: 0x0003D0DD File Offset: 0x0003B2DD
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170006E5 RID: 1765
			// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x0003D0F5 File Offset: 0x0003B2F5
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170006E6 RID: 1766
			// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x0003D10D File Offset: 0x0003B30D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170006E7 RID: 1767
			// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0003D125 File Offset: 0x0003B325
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170006E8 RID: 1768
			// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x0003D13D File Offset: 0x0003B33D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170006E9 RID: 1769
			// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x0003D155 File Offset: 0x0003B355
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170006EA RID: 1770
			// (set) Token: 0x06001CC8 RID: 7368 RVA: 0x0003D16D File Offset: 0x0003B36D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170006EB RID: 1771
			// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0003D185 File Offset: 0x0003B385
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
