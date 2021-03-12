using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E05 RID: 3589
	public class UpdateSiteMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<RecipientIdParameter>
	{
		// Token: 0x0600D59E RID: 54686 RVA: 0x0012F99B File Offset: 0x0012DB9B
		private UpdateSiteMailboxCommand() : base("Update-SiteMailbox")
		{
		}

		// Token: 0x0600D59F RID: 54687 RVA: 0x0012F9A8 File Offset: 0x0012DBA8
		public UpdateSiteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D5A0 RID: 54688 RVA: 0x0012F9B7 File Offset: 0x0012DBB7
		public virtual UpdateSiteMailboxCommand SetParameters(UpdateSiteMailboxCommand.TeamMailboxITProParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D5A1 RID: 54689 RVA: 0x0012F9C1 File Offset: 0x0012DBC1
		public virtual UpdateSiteMailboxCommand SetParameters(UpdateSiteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E06 RID: 3590
		public class TeamMailboxITProParameters : ParametersBase
		{
			// Token: 0x1700A54D RID: 42317
			// (set) Token: 0x0600D5A2 RID: 54690 RVA: 0x0012F9CB File Offset: 0x0012DBCB
			public virtual string Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700A54E RID: 42318
			// (set) Token: 0x0600D5A3 RID: 54691 RVA: 0x0012F9DE File Offset: 0x0012DBDE
			public virtual SwitchParameter FullSync
			{
				set
				{
					base.PowerSharpParameters["FullSync"] = value;
				}
			}

			// Token: 0x1700A54F RID: 42319
			// (set) Token: 0x0600D5A4 RID: 54692 RVA: 0x0012F9F6 File Offset: 0x0012DBF6
			public virtual SwitchParameter BypassOwnerCheck
			{
				set
				{
					base.PowerSharpParameters["BypassOwnerCheck"] = value;
				}
			}

			// Token: 0x1700A550 RID: 42320
			// (set) Token: 0x0600D5A5 RID: 54693 RVA: 0x0012FA0E File Offset: 0x0012DC0E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A551 RID: 42321
			// (set) Token: 0x0600D5A6 RID: 54694 RVA: 0x0012FA2C File Offset: 0x0012DC2C
			public virtual TeamMailboxDiagnosticsBase.TargetType? Target
			{
				set
				{
					base.PowerSharpParameters["Target"] = value;
				}
			}

			// Token: 0x1700A552 RID: 42322
			// (set) Token: 0x0600D5A7 RID: 54695 RVA: 0x0012FA44 File Offset: 0x0012DC44
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A553 RID: 42323
			// (set) Token: 0x0600D5A8 RID: 54696 RVA: 0x0012FA62 File Offset: 0x0012DC62
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A554 RID: 42324
			// (set) Token: 0x0600D5A9 RID: 54697 RVA: 0x0012FA7A File Offset: 0x0012DC7A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A555 RID: 42325
			// (set) Token: 0x0600D5AA RID: 54698 RVA: 0x0012FA92 File Offset: 0x0012DC92
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A556 RID: 42326
			// (set) Token: 0x0600D5AB RID: 54699 RVA: 0x0012FAAA File Offset: 0x0012DCAA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A557 RID: 42327
			// (set) Token: 0x0600D5AC RID: 54700 RVA: 0x0012FAC2 File Offset: 0x0012DCC2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E07 RID: 3591
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A558 RID: 42328
			// (set) Token: 0x0600D5AE RID: 54702 RVA: 0x0012FAE2 File Offset: 0x0012DCE2
			public virtual TeamMailboxDiagnosticsBase.TargetType? Target
			{
				set
				{
					base.PowerSharpParameters["Target"] = value;
				}
			}

			// Token: 0x1700A559 RID: 42329
			// (set) Token: 0x0600D5AF RID: 54703 RVA: 0x0012FAFA File Offset: 0x0012DCFA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A55A RID: 42330
			// (set) Token: 0x0600D5B0 RID: 54704 RVA: 0x0012FB18 File Offset: 0x0012DD18
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A55B RID: 42331
			// (set) Token: 0x0600D5B1 RID: 54705 RVA: 0x0012FB30 File Offset: 0x0012DD30
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A55C RID: 42332
			// (set) Token: 0x0600D5B2 RID: 54706 RVA: 0x0012FB48 File Offset: 0x0012DD48
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A55D RID: 42333
			// (set) Token: 0x0600D5B3 RID: 54707 RVA: 0x0012FB60 File Offset: 0x0012DD60
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A55E RID: 42334
			// (set) Token: 0x0600D5B4 RID: 54708 RVA: 0x0012FB78 File Offset: 0x0012DD78
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
