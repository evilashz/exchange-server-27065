using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A60 RID: 2656
	public class SuspendMailboxRestoreRequestCommand : SyntheticCommandWithPipelineInput<MailboxRestoreRequestIdParameter, MailboxRestoreRequestIdParameter>
	{
		// Token: 0x0600841A RID: 33818 RVA: 0x000C3428 File Offset: 0x000C1628
		private SuspendMailboxRestoreRequestCommand() : base("Suspend-MailboxRestoreRequest")
		{
		}

		// Token: 0x0600841B RID: 33819 RVA: 0x000C3435 File Offset: 0x000C1635
		public SuspendMailboxRestoreRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600841C RID: 33820 RVA: 0x000C3444 File Offset: 0x000C1644
		public virtual SuspendMailboxRestoreRequestCommand SetParameters(SuspendMailboxRestoreRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600841D RID: 33821 RVA: 0x000C344E File Offset: 0x000C164E
		public virtual SuspendMailboxRestoreRequestCommand SetParameters(SuspendMailboxRestoreRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A61 RID: 2657
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005B13 RID: 23315
			// (set) Token: 0x0600841E RID: 33822 RVA: 0x000C3458 File Offset: 0x000C1658
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRestoreRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005B14 RID: 23316
			// (set) Token: 0x0600841F RID: 33823 RVA: 0x000C3476 File Offset: 0x000C1676
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005B15 RID: 23317
			// (set) Token: 0x06008420 RID: 33824 RVA: 0x000C3489 File Offset: 0x000C1689
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B16 RID: 23318
			// (set) Token: 0x06008421 RID: 33825 RVA: 0x000C349C File Offset: 0x000C169C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B17 RID: 23319
			// (set) Token: 0x06008422 RID: 33826 RVA: 0x000C34B4 File Offset: 0x000C16B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B18 RID: 23320
			// (set) Token: 0x06008423 RID: 33827 RVA: 0x000C34CC File Offset: 0x000C16CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B19 RID: 23321
			// (set) Token: 0x06008424 RID: 33828 RVA: 0x000C34E4 File Offset: 0x000C16E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B1A RID: 23322
			// (set) Token: 0x06008425 RID: 33829 RVA: 0x000C34FC File Offset: 0x000C16FC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005B1B RID: 23323
			// (set) Token: 0x06008426 RID: 33830 RVA: 0x000C3514 File Offset: 0x000C1714
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A62 RID: 2658
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005B1C RID: 23324
			// (set) Token: 0x06008428 RID: 33832 RVA: 0x000C3534 File Offset: 0x000C1734
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005B1D RID: 23325
			// (set) Token: 0x06008429 RID: 33833 RVA: 0x000C3547 File Offset: 0x000C1747
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B1E RID: 23326
			// (set) Token: 0x0600842A RID: 33834 RVA: 0x000C355A File Offset: 0x000C175A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B1F RID: 23327
			// (set) Token: 0x0600842B RID: 33835 RVA: 0x000C3572 File Offset: 0x000C1772
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B20 RID: 23328
			// (set) Token: 0x0600842C RID: 33836 RVA: 0x000C358A File Offset: 0x000C178A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B21 RID: 23329
			// (set) Token: 0x0600842D RID: 33837 RVA: 0x000C35A2 File Offset: 0x000C17A2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B22 RID: 23330
			// (set) Token: 0x0600842E RID: 33838 RVA: 0x000C35BA File Offset: 0x000C17BA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005B23 RID: 23331
			// (set) Token: 0x0600842F RID: 33839 RVA: 0x000C35D2 File Offset: 0x000C17D2
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
