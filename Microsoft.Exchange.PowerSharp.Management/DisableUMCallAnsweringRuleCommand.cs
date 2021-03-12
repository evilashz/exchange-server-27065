using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E15 RID: 3605
	public class DisableUMCallAnsweringRuleCommand : SyntheticCommandWithPipelineInputNoOutput<UMCallAnsweringRuleIdParameter>
	{
		// Token: 0x0600D68B RID: 54923 RVA: 0x00130DAE File Offset: 0x0012EFAE
		private DisableUMCallAnsweringRuleCommand() : base("Disable-UMCallAnsweringRule")
		{
		}

		// Token: 0x0600D68C RID: 54924 RVA: 0x00130DBB File Offset: 0x0012EFBB
		public DisableUMCallAnsweringRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D68D RID: 54925 RVA: 0x00130DCA File Offset: 0x0012EFCA
		public virtual DisableUMCallAnsweringRuleCommand SetParameters(DisableUMCallAnsweringRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D68E RID: 54926 RVA: 0x00130DD4 File Offset: 0x0012EFD4
		public virtual DisableUMCallAnsweringRuleCommand SetParameters(DisableUMCallAnsweringRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E16 RID: 3606
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A61A RID: 42522
			// (set) Token: 0x0600D68F RID: 54927 RVA: 0x00130DDE File Offset: 0x0012EFDE
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A61B RID: 42523
			// (set) Token: 0x0600D690 RID: 54928 RVA: 0x00130DFC File Offset: 0x0012EFFC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A61C RID: 42524
			// (set) Token: 0x0600D691 RID: 54929 RVA: 0x00130E0F File Offset: 0x0012F00F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A61D RID: 42525
			// (set) Token: 0x0600D692 RID: 54930 RVA: 0x00130E27 File Offset: 0x0012F027
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A61E RID: 42526
			// (set) Token: 0x0600D693 RID: 54931 RVA: 0x00130E3F File Offset: 0x0012F03F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A61F RID: 42527
			// (set) Token: 0x0600D694 RID: 54932 RVA: 0x00130E57 File Offset: 0x0012F057
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A620 RID: 42528
			// (set) Token: 0x0600D695 RID: 54933 RVA: 0x00130E6F File Offset: 0x0012F06F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A621 RID: 42529
			// (set) Token: 0x0600D696 RID: 54934 RVA: 0x00130E87 File Offset: 0x0012F087
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E17 RID: 3607
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A622 RID: 42530
			// (set) Token: 0x0600D698 RID: 54936 RVA: 0x00130EA7 File Offset: 0x0012F0A7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMCallAnsweringRuleIdParameter(value) : null);
				}
			}

			// Token: 0x1700A623 RID: 42531
			// (set) Token: 0x0600D699 RID: 54937 RVA: 0x00130EC5 File Offset: 0x0012F0C5
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A624 RID: 42532
			// (set) Token: 0x0600D69A RID: 54938 RVA: 0x00130EE3 File Offset: 0x0012F0E3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A625 RID: 42533
			// (set) Token: 0x0600D69B RID: 54939 RVA: 0x00130EF6 File Offset: 0x0012F0F6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A626 RID: 42534
			// (set) Token: 0x0600D69C RID: 54940 RVA: 0x00130F0E File Offset: 0x0012F10E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A627 RID: 42535
			// (set) Token: 0x0600D69D RID: 54941 RVA: 0x00130F26 File Offset: 0x0012F126
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A628 RID: 42536
			// (set) Token: 0x0600D69E RID: 54942 RVA: 0x00130F3E File Offset: 0x0012F13E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A629 RID: 42537
			// (set) Token: 0x0600D69F RID: 54943 RVA: 0x00130F56 File Offset: 0x0012F156
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A62A RID: 42538
			// (set) Token: 0x0600D6A0 RID: 54944 RVA: 0x00130F6E File Offset: 0x0012F16E
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
