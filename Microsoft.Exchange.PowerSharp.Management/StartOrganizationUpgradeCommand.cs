using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000CC RID: 204
	public class StartOrganizationUpgradeCommand : SyntheticCommandWithPipelineInputNoOutput<OrganizationIdParameter>
	{
		// Token: 0x06001CCB RID: 7371 RVA: 0x0003D1A5 File Offset: 0x0003B3A5
		private StartOrganizationUpgradeCommand() : base("Start-OrganizationUpgrade")
		{
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0003D1B2 File Offset: 0x0003B3B2
		public StartOrganizationUpgradeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0003D1C1 File Offset: 0x0003B3C1
		public virtual StartOrganizationUpgradeCommand SetParameters(StartOrganizationUpgradeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0003D1CB File Offset: 0x0003B3CB
		public virtual StartOrganizationUpgradeCommand SetParameters(StartOrganizationUpgradeCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000CD RID: 205
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170006EC RID: 1772
			// (set) Token: 0x06001CCF RID: 7375 RVA: 0x0003D1D5 File Offset: 0x0003B3D5
			public virtual SwitchParameter AuthoritativeOnly
			{
				set
				{
					base.PowerSharpParameters["AuthoritativeOnly"] = value;
				}
			}

			// Token: 0x170006ED RID: 1773
			// (set) Token: 0x06001CD0 RID: 7376 RVA: 0x0003D1ED File Offset: 0x0003B3ED
			public virtual SwitchParameter ConfigOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigOnly"] = value;
				}
			}

			// Token: 0x170006EE RID: 1774
			// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x0003D205 File Offset: 0x0003B405
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x170006EF RID: 1775
			// (set) Token: 0x06001CD2 RID: 7378 RVA: 0x0003D21D File Offset: 0x0003B41D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170006F0 RID: 1776
			// (set) Token: 0x06001CD3 RID: 7379 RVA: 0x0003D230 File Offset: 0x0003B430
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170006F1 RID: 1777
			// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0003D248 File Offset: 0x0003B448
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x170006F2 RID: 1778
			// (set) Token: 0x06001CD5 RID: 7381 RVA: 0x0003D260 File Offset: 0x0003B460
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x170006F3 RID: 1779
			// (set) Token: 0x06001CD6 RID: 7382 RVA: 0x0003D278 File Offset: 0x0003B478
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170006F4 RID: 1780
			// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0003D290 File Offset: 0x0003B490
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170006F5 RID: 1781
			// (set) Token: 0x06001CD8 RID: 7384 RVA: 0x0003D2A8 File Offset: 0x0003B4A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170006F6 RID: 1782
			// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0003D2C0 File Offset: 0x0003B4C0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170006F7 RID: 1783
			// (set) Token: 0x06001CDA RID: 7386 RVA: 0x0003D2D8 File Offset: 0x0003B4D8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170006F8 RID: 1784
			// (set) Token: 0x06001CDB RID: 7387 RVA: 0x0003D2F0 File Offset: 0x0003B4F0
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020000CE RID: 206
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170006F9 RID: 1785
			// (set) Token: 0x06001CDD RID: 7389 RVA: 0x0003D310 File Offset: 0x0003B510
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170006FA RID: 1786
			// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0003D32E File Offset: 0x0003B52E
			public virtual SwitchParameter AuthoritativeOnly
			{
				set
				{
					base.PowerSharpParameters["AuthoritativeOnly"] = value;
				}
			}

			// Token: 0x170006FB RID: 1787
			// (set) Token: 0x06001CDF RID: 7391 RVA: 0x0003D346 File Offset: 0x0003B546
			public virtual SwitchParameter ConfigOnly
			{
				set
				{
					base.PowerSharpParameters["ConfigOnly"] = value;
				}
			}

			// Token: 0x170006FC RID: 1788
			// (set) Token: 0x06001CE0 RID: 7392 RVA: 0x0003D35E File Offset: 0x0003B55E
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x170006FD RID: 1789
			// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x0003D376 File Offset: 0x0003B576
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170006FE RID: 1790
			// (set) Token: 0x06001CE2 RID: 7394 RVA: 0x0003D389 File Offset: 0x0003B589
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x170006FF RID: 1791
			// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0003D3A1 File Offset: 0x0003B5A1
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x17000700 RID: 1792
			// (set) Token: 0x06001CE4 RID: 7396 RVA: 0x0003D3B9 File Offset: 0x0003B5B9
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x17000701 RID: 1793
			// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0003D3D1 File Offset: 0x0003B5D1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000702 RID: 1794
			// (set) Token: 0x06001CE6 RID: 7398 RVA: 0x0003D3E9 File Offset: 0x0003B5E9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000703 RID: 1795
			// (set) Token: 0x06001CE7 RID: 7399 RVA: 0x0003D401 File Offset: 0x0003B601
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000704 RID: 1796
			// (set) Token: 0x06001CE8 RID: 7400 RVA: 0x0003D419 File Offset: 0x0003B619
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000705 RID: 1797
			// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x0003D431 File Offset: 0x0003B631
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000706 RID: 1798
			// (set) Token: 0x06001CEA RID: 7402 RVA: 0x0003D449 File Offset: 0x0003B649
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
