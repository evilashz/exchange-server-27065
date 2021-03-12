using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E18 RID: 3608
	public class EnableUMCallAnsweringRuleCommand : SyntheticCommandWithPipelineInputNoOutput<UMCallAnsweringRuleIdParameter>
	{
		// Token: 0x0600D6A2 RID: 54946 RVA: 0x00130F8E File Offset: 0x0012F18E
		private EnableUMCallAnsweringRuleCommand() : base("Enable-UMCallAnsweringRule")
		{
		}

		// Token: 0x0600D6A3 RID: 54947 RVA: 0x00130F9B File Offset: 0x0012F19B
		public EnableUMCallAnsweringRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D6A4 RID: 54948 RVA: 0x00130FAA File Offset: 0x0012F1AA
		public virtual EnableUMCallAnsweringRuleCommand SetParameters(EnableUMCallAnsweringRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D6A5 RID: 54949 RVA: 0x00130FB4 File Offset: 0x0012F1B4
		public virtual EnableUMCallAnsweringRuleCommand SetParameters(EnableUMCallAnsweringRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E19 RID: 3609
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A62B RID: 42539
			// (set) Token: 0x0600D6A6 RID: 54950 RVA: 0x00130FBE File Offset: 0x0012F1BE
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A62C RID: 42540
			// (set) Token: 0x0600D6A7 RID: 54951 RVA: 0x00130FDC File Offset: 0x0012F1DC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A62D RID: 42541
			// (set) Token: 0x0600D6A8 RID: 54952 RVA: 0x00130FEF File Offset: 0x0012F1EF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A62E RID: 42542
			// (set) Token: 0x0600D6A9 RID: 54953 RVA: 0x00131007 File Offset: 0x0012F207
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A62F RID: 42543
			// (set) Token: 0x0600D6AA RID: 54954 RVA: 0x0013101F File Offset: 0x0012F21F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A630 RID: 42544
			// (set) Token: 0x0600D6AB RID: 54955 RVA: 0x00131037 File Offset: 0x0012F237
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A631 RID: 42545
			// (set) Token: 0x0600D6AC RID: 54956 RVA: 0x0013104F File Offset: 0x0012F24F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E1A RID: 3610
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A632 RID: 42546
			// (set) Token: 0x0600D6AE RID: 54958 RVA: 0x0013106F File Offset: 0x0012F26F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMCallAnsweringRuleIdParameter(value) : null);
				}
			}

			// Token: 0x1700A633 RID: 42547
			// (set) Token: 0x0600D6AF RID: 54959 RVA: 0x0013108D File Offset: 0x0012F28D
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A634 RID: 42548
			// (set) Token: 0x0600D6B0 RID: 54960 RVA: 0x001310AB File Offset: 0x0012F2AB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A635 RID: 42549
			// (set) Token: 0x0600D6B1 RID: 54961 RVA: 0x001310BE File Offset: 0x0012F2BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A636 RID: 42550
			// (set) Token: 0x0600D6B2 RID: 54962 RVA: 0x001310D6 File Offset: 0x0012F2D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A637 RID: 42551
			// (set) Token: 0x0600D6B3 RID: 54963 RVA: 0x001310EE File Offset: 0x0012F2EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A638 RID: 42552
			// (set) Token: 0x0600D6B4 RID: 54964 RVA: 0x00131106 File Offset: 0x0012F306
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A639 RID: 42553
			// (set) Token: 0x0600D6B5 RID: 54965 RVA: 0x0013111E File Offset: 0x0012F31E
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
