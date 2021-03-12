using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000A3 RID: 163
	public class CompleteOrganizationUpgradeCommand : SyntheticCommandWithPipelineInputNoOutput<OrganizationIdParameter>
	{
		// Token: 0x0600199E RID: 6558 RVA: 0x00038D76 File Offset: 0x00036F76
		private CompleteOrganizationUpgradeCommand() : base("Complete-OrganizationUpgrade")
		{
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00038D83 File Offset: 0x00036F83
		public CompleteOrganizationUpgradeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00038D92 File Offset: 0x00036F92
		public virtual CompleteOrganizationUpgradeCommand SetParameters(CompleteOrganizationUpgradeCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00038D9C File Offset: 0x00036F9C
		public virtual CompleteOrganizationUpgradeCommand SetParameters(CompleteOrganizationUpgradeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000A4 RID: 164
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000411 RID: 1041
			// (set) Token: 0x060019A2 RID: 6562 RVA: 0x00038DA6 File Offset: 0x00036FA6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000412 RID: 1042
			// (set) Token: 0x060019A3 RID: 6563 RVA: 0x00038DC4 File Offset: 0x00036FC4
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x17000413 RID: 1043
			// (set) Token: 0x060019A4 RID: 6564 RVA: 0x00038DDC File Offset: 0x00036FDC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000414 RID: 1044
			// (set) Token: 0x060019A5 RID: 6565 RVA: 0x00038DEF File Offset: 0x00036FEF
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x17000415 RID: 1045
			// (set) Token: 0x060019A6 RID: 6566 RVA: 0x00038E07 File Offset: 0x00037007
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x17000416 RID: 1046
			// (set) Token: 0x060019A7 RID: 6567 RVA: 0x00038E1F File Offset: 0x0003701F
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x17000417 RID: 1047
			// (set) Token: 0x060019A8 RID: 6568 RVA: 0x00038E37 File Offset: 0x00037037
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000418 RID: 1048
			// (set) Token: 0x060019A9 RID: 6569 RVA: 0x00038E4F File Offset: 0x0003704F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000419 RID: 1049
			// (set) Token: 0x060019AA RID: 6570 RVA: 0x00038E67 File Offset: 0x00037067
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700041A RID: 1050
			// (set) Token: 0x060019AB RID: 6571 RVA: 0x00038E7F File Offset: 0x0003707F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700041B RID: 1051
			// (set) Token: 0x060019AC RID: 6572 RVA: 0x00038E97 File Offset: 0x00037097
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700041C RID: 1052
			// (set) Token: 0x060019AD RID: 6573 RVA: 0x00038EAF File Offset: 0x000370AF
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020000A5 RID: 165
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700041D RID: 1053
			// (set) Token: 0x060019AF RID: 6575 RVA: 0x00038ECF File Offset: 0x000370CF
			public virtual SwitchParameter EnableFileLogging
			{
				set
				{
					base.PowerSharpParameters["EnableFileLogging"] = value;
				}
			}

			// Token: 0x1700041E RID: 1054
			// (set) Token: 0x060019B0 RID: 6576 RVA: 0x00038EE7 File Offset: 0x000370E7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700041F RID: 1055
			// (set) Token: 0x060019B1 RID: 6577 RVA: 0x00038EFA File Offset: 0x000370FA
			public virtual SwitchParameter IsDatacenter
			{
				set
				{
					base.PowerSharpParameters["IsDatacenter"] = value;
				}
			}

			// Token: 0x17000420 RID: 1056
			// (set) Token: 0x060019B2 RID: 6578 RVA: 0x00038F12 File Offset: 0x00037112
			public virtual SwitchParameter IsDatacenterDedicated
			{
				set
				{
					base.PowerSharpParameters["IsDatacenterDedicated"] = value;
				}
			}

			// Token: 0x17000421 RID: 1057
			// (set) Token: 0x060019B3 RID: 6579 RVA: 0x00038F2A File Offset: 0x0003712A
			public virtual SwitchParameter IsPartnerHosted
			{
				set
				{
					base.PowerSharpParameters["IsPartnerHosted"] = value;
				}
			}

			// Token: 0x17000422 RID: 1058
			// (set) Token: 0x060019B4 RID: 6580 RVA: 0x00038F42 File Offset: 0x00037142
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000423 RID: 1059
			// (set) Token: 0x060019B5 RID: 6581 RVA: 0x00038F5A File Offset: 0x0003715A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000424 RID: 1060
			// (set) Token: 0x060019B6 RID: 6582 RVA: 0x00038F72 File Offset: 0x00037172
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000425 RID: 1061
			// (set) Token: 0x060019B7 RID: 6583 RVA: 0x00038F8A File Offset: 0x0003718A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000426 RID: 1062
			// (set) Token: 0x060019B8 RID: 6584 RVA: 0x00038FA2 File Offset: 0x000371A2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000427 RID: 1063
			// (set) Token: 0x060019B9 RID: 6585 RVA: 0x00038FBA File Offset: 0x000371BA
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
