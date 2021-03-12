using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E20 RID: 3616
	public class RemoveUMCallAnsweringRuleCommand : SyntheticCommandWithPipelineInput<UMCallAnsweringRule, UMCallAnsweringRule>
	{
		// Token: 0x0600D6DF RID: 55007 RVA: 0x00131472 File Offset: 0x0012F672
		private RemoveUMCallAnsweringRuleCommand() : base("Remove-UMCallAnsweringRule")
		{
		}

		// Token: 0x0600D6E0 RID: 55008 RVA: 0x0013147F File Offset: 0x0012F67F
		public RemoveUMCallAnsweringRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D6E1 RID: 55009 RVA: 0x0013148E File Offset: 0x0012F68E
		public virtual RemoveUMCallAnsweringRuleCommand SetParameters(RemoveUMCallAnsweringRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D6E2 RID: 55010 RVA: 0x00131498 File Offset: 0x0012F698
		public virtual RemoveUMCallAnsweringRuleCommand SetParameters(RemoveUMCallAnsweringRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E21 RID: 3617
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A658 RID: 42584
			// (set) Token: 0x0600D6E3 RID: 55011 RVA: 0x001314A2 File Offset: 0x0012F6A2
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A659 RID: 42585
			// (set) Token: 0x0600D6E4 RID: 55012 RVA: 0x001314C0 File Offset: 0x0012F6C0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A65A RID: 42586
			// (set) Token: 0x0600D6E5 RID: 55013 RVA: 0x001314D3 File Offset: 0x0012F6D3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A65B RID: 42587
			// (set) Token: 0x0600D6E6 RID: 55014 RVA: 0x001314EB File Offset: 0x0012F6EB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A65C RID: 42588
			// (set) Token: 0x0600D6E7 RID: 55015 RVA: 0x00131503 File Offset: 0x0012F703
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A65D RID: 42589
			// (set) Token: 0x0600D6E8 RID: 55016 RVA: 0x0013151B File Offset: 0x0012F71B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A65E RID: 42590
			// (set) Token: 0x0600D6E9 RID: 55017 RVA: 0x00131533 File Offset: 0x0012F733
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A65F RID: 42591
			// (set) Token: 0x0600D6EA RID: 55018 RVA: 0x0013154B File Offset: 0x0012F74B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E22 RID: 3618
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A660 RID: 42592
			// (set) Token: 0x0600D6EC RID: 55020 RVA: 0x0013156B File Offset: 0x0012F76B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMCallAnsweringRuleIdParameter(value) : null);
				}
			}

			// Token: 0x1700A661 RID: 42593
			// (set) Token: 0x0600D6ED RID: 55021 RVA: 0x00131589 File Offset: 0x0012F789
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A662 RID: 42594
			// (set) Token: 0x0600D6EE RID: 55022 RVA: 0x001315A7 File Offset: 0x0012F7A7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A663 RID: 42595
			// (set) Token: 0x0600D6EF RID: 55023 RVA: 0x001315BA File Offset: 0x0012F7BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A664 RID: 42596
			// (set) Token: 0x0600D6F0 RID: 55024 RVA: 0x001315D2 File Offset: 0x0012F7D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A665 RID: 42597
			// (set) Token: 0x0600D6F1 RID: 55025 RVA: 0x001315EA File Offset: 0x0012F7EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A666 RID: 42598
			// (set) Token: 0x0600D6F2 RID: 55026 RVA: 0x00131602 File Offset: 0x0012F802
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A667 RID: 42599
			// (set) Token: 0x0600D6F3 RID: 55027 RVA: 0x0013161A File Offset: 0x0012F81A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A668 RID: 42600
			// (set) Token: 0x0600D6F4 RID: 55028 RVA: 0x00131632 File Offset: 0x0012F832
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
