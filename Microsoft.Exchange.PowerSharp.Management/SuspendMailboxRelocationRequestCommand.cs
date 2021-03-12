using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A46 RID: 2630
	public class SuspendMailboxRelocationRequestCommand : SyntheticCommandWithPipelineInput<MailboxRelocationRequestIdParameter, MailboxRelocationRequestIdParameter>
	{
		// Token: 0x060082FC RID: 33532 RVA: 0x000C1D0A File Offset: 0x000BFF0A
		private SuspendMailboxRelocationRequestCommand() : base("Suspend-MailboxRelocationRequest")
		{
		}

		// Token: 0x060082FD RID: 33533 RVA: 0x000C1D17 File Offset: 0x000BFF17
		public SuspendMailboxRelocationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060082FE RID: 33534 RVA: 0x000C1D26 File Offset: 0x000BFF26
		public virtual SuspendMailboxRelocationRequestCommand SetParameters(SuspendMailboxRelocationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060082FF RID: 33535 RVA: 0x000C1D30 File Offset: 0x000BFF30
		public virtual SuspendMailboxRelocationRequestCommand SetParameters(SuspendMailboxRelocationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A47 RID: 2631
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005A29 RID: 23081
			// (set) Token: 0x06008300 RID: 33536 RVA: 0x000C1D3A File Offset: 0x000BFF3A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRelocationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005A2A RID: 23082
			// (set) Token: 0x06008301 RID: 33537 RVA: 0x000C1D58 File Offset: 0x000BFF58
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005A2B RID: 23083
			// (set) Token: 0x06008302 RID: 33538 RVA: 0x000C1D6B File Offset: 0x000BFF6B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A2C RID: 23084
			// (set) Token: 0x06008303 RID: 33539 RVA: 0x000C1D7E File Offset: 0x000BFF7E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A2D RID: 23085
			// (set) Token: 0x06008304 RID: 33540 RVA: 0x000C1D96 File Offset: 0x000BFF96
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A2E RID: 23086
			// (set) Token: 0x06008305 RID: 33541 RVA: 0x000C1DAE File Offset: 0x000BFFAE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A2F RID: 23087
			// (set) Token: 0x06008306 RID: 33542 RVA: 0x000C1DC6 File Offset: 0x000BFFC6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005A30 RID: 23088
			// (set) Token: 0x06008307 RID: 33543 RVA: 0x000C1DDE File Offset: 0x000BFFDE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005A31 RID: 23089
			// (set) Token: 0x06008308 RID: 33544 RVA: 0x000C1DF6 File Offset: 0x000BFFF6
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A48 RID: 2632
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005A32 RID: 23090
			// (set) Token: 0x0600830A RID: 33546 RVA: 0x000C1E16 File Offset: 0x000C0016
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005A33 RID: 23091
			// (set) Token: 0x0600830B RID: 33547 RVA: 0x000C1E29 File Offset: 0x000C0029
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005A34 RID: 23092
			// (set) Token: 0x0600830C RID: 33548 RVA: 0x000C1E3C File Offset: 0x000C003C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005A35 RID: 23093
			// (set) Token: 0x0600830D RID: 33549 RVA: 0x000C1E54 File Offset: 0x000C0054
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005A36 RID: 23094
			// (set) Token: 0x0600830E RID: 33550 RVA: 0x000C1E6C File Offset: 0x000C006C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005A37 RID: 23095
			// (set) Token: 0x0600830F RID: 33551 RVA: 0x000C1E84 File Offset: 0x000C0084
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005A38 RID: 23096
			// (set) Token: 0x06008310 RID: 33552 RVA: 0x000C1E9C File Offset: 0x000C009C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005A39 RID: 23097
			// (set) Token: 0x06008311 RID: 33553 RVA: 0x000C1EB4 File Offset: 0x000C00B4
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
