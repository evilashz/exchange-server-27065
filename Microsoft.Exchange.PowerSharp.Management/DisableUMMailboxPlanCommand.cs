using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B15 RID: 2837
	public class DisableUMMailboxPlanCommand : SyntheticCommandWithPipelineInput<MailboxPlanIdParameter, MailboxPlanIdParameter>
	{
		// Token: 0x06008B0C RID: 35596 RVA: 0x000CC438 File Offset: 0x000CA638
		private DisableUMMailboxPlanCommand() : base("Disable-UMMailboxPlan")
		{
		}

		// Token: 0x06008B0D RID: 35597 RVA: 0x000CC445 File Offset: 0x000CA645
		public DisableUMMailboxPlanCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008B0E RID: 35598 RVA: 0x000CC454 File Offset: 0x000CA654
		public virtual DisableUMMailboxPlanCommand SetParameters(DisableUMMailboxPlanCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008B0F RID: 35599 RVA: 0x000CC45E File Offset: 0x000CA65E
		public virtual DisableUMMailboxPlanCommand SetParameters(DisableUMMailboxPlanCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B16 RID: 2838
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700609B RID: 24731
			// (set) Token: 0x06008B10 RID: 35600 RVA: 0x000CC468 File Offset: 0x000CA668
			public virtual bool KeepProperties
			{
				set
				{
					base.PowerSharpParameters["KeepProperties"] = value;
				}
			}

			// Token: 0x1700609C RID: 24732
			// (set) Token: 0x06008B11 RID: 35601 RVA: 0x000CC480 File Offset: 0x000CA680
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700609D RID: 24733
			// (set) Token: 0x06008B12 RID: 35602 RVA: 0x000CC498 File Offset: 0x000CA698
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700609E RID: 24734
			// (set) Token: 0x06008B13 RID: 35603 RVA: 0x000CC4AB File Offset: 0x000CA6AB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700609F RID: 24735
			// (set) Token: 0x06008B14 RID: 35604 RVA: 0x000CC4C3 File Offset: 0x000CA6C3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060A0 RID: 24736
			// (set) Token: 0x06008B15 RID: 35605 RVA: 0x000CC4DB File Offset: 0x000CA6DB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060A1 RID: 24737
			// (set) Token: 0x06008B16 RID: 35606 RVA: 0x000CC4F3 File Offset: 0x000CA6F3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060A2 RID: 24738
			// (set) Token: 0x06008B17 RID: 35607 RVA: 0x000CC50B File Offset: 0x000CA70B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170060A3 RID: 24739
			// (set) Token: 0x06008B18 RID: 35608 RVA: 0x000CC523 File Offset: 0x000CA723
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000B17 RID: 2839
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170060A4 RID: 24740
			// (set) Token: 0x06008B1A RID: 35610 RVA: 0x000CC543 File Offset: 0x000CA743
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170060A5 RID: 24741
			// (set) Token: 0x06008B1B RID: 35611 RVA: 0x000CC561 File Offset: 0x000CA761
			public virtual bool KeepProperties
			{
				set
				{
					base.PowerSharpParameters["KeepProperties"] = value;
				}
			}

			// Token: 0x170060A6 RID: 24742
			// (set) Token: 0x06008B1C RID: 35612 RVA: 0x000CC579 File Offset: 0x000CA779
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x170060A7 RID: 24743
			// (set) Token: 0x06008B1D RID: 35613 RVA: 0x000CC591 File Offset: 0x000CA791
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060A8 RID: 24744
			// (set) Token: 0x06008B1E RID: 35614 RVA: 0x000CC5A4 File Offset: 0x000CA7A4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060A9 RID: 24745
			// (set) Token: 0x06008B1F RID: 35615 RVA: 0x000CC5BC File Offset: 0x000CA7BC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060AA RID: 24746
			// (set) Token: 0x06008B20 RID: 35616 RVA: 0x000CC5D4 File Offset: 0x000CA7D4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060AB RID: 24747
			// (set) Token: 0x06008B21 RID: 35617 RVA: 0x000CC5EC File Offset: 0x000CA7EC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060AC RID: 24748
			// (set) Token: 0x06008B22 RID: 35618 RVA: 0x000CC604 File Offset: 0x000CA804
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170060AD RID: 24749
			// (set) Token: 0x06008B23 RID: 35619 RVA: 0x000CC61C File Offset: 0x000CA81C
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
