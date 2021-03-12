using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DF6 RID: 3574
	public class RemoveSyncGroupCommand : SyntheticCommandWithPipelineInput<ADGroup, ADGroup>
	{
		// Token: 0x0600D50F RID: 54543 RVA: 0x0012EE0D File Offset: 0x0012D00D
		private RemoveSyncGroupCommand() : base("Remove-SyncGroup")
		{
		}

		// Token: 0x0600D510 RID: 54544 RVA: 0x0012EE1A File Offset: 0x0012D01A
		public RemoveSyncGroupCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D511 RID: 54545 RVA: 0x0012EE29 File Offset: 0x0012D029
		public virtual RemoveSyncGroupCommand SetParameters(RemoveSyncGroupCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D512 RID: 54546 RVA: 0x0012EE33 File Offset: 0x0012D033
		public virtual RemoveSyncGroupCommand SetParameters(RemoveSyncGroupCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DF7 RID: 3575
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A4DC RID: 42204
			// (set) Token: 0x0600D513 RID: 54547 RVA: 0x0012EE3D File Offset: 0x0012D03D
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x1700A4DD RID: 42205
			// (set) Token: 0x0600D514 RID: 54548 RVA: 0x0012EE55 File Offset: 0x0012D055
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A4DE RID: 42206
			// (set) Token: 0x0600D515 RID: 54549 RVA: 0x0012EE6D File Offset: 0x0012D06D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A4DF RID: 42207
			// (set) Token: 0x0600D516 RID: 54550 RVA: 0x0012EE80 File Offset: 0x0012D080
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A4E0 RID: 42208
			// (set) Token: 0x0600D517 RID: 54551 RVA: 0x0012EE98 File Offset: 0x0012D098
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A4E1 RID: 42209
			// (set) Token: 0x0600D518 RID: 54552 RVA: 0x0012EEB0 File Offset: 0x0012D0B0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A4E2 RID: 42210
			// (set) Token: 0x0600D519 RID: 54553 RVA: 0x0012EEC8 File Offset: 0x0012D0C8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A4E3 RID: 42211
			// (set) Token: 0x0600D51A RID: 54554 RVA: 0x0012EEE0 File Offset: 0x0012D0E0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A4E4 RID: 42212
			// (set) Token: 0x0600D51B RID: 54555 RVA: 0x0012EEF8 File Offset: 0x0012D0F8
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000DF8 RID: 3576
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A4E5 RID: 42213
			// (set) Token: 0x0600D51D RID: 54557 RVA: 0x0012EF18 File Offset: 0x0012D118
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new NonMailEnabledGroupIdParameter(value) : null);
				}
			}

			// Token: 0x1700A4E6 RID: 42214
			// (set) Token: 0x0600D51E RID: 54558 RVA: 0x0012EF36 File Offset: 0x0012D136
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x1700A4E7 RID: 42215
			// (set) Token: 0x0600D51F RID: 54559 RVA: 0x0012EF4E File Offset: 0x0012D14E
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A4E8 RID: 42216
			// (set) Token: 0x0600D520 RID: 54560 RVA: 0x0012EF66 File Offset: 0x0012D166
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A4E9 RID: 42217
			// (set) Token: 0x0600D521 RID: 54561 RVA: 0x0012EF79 File Offset: 0x0012D179
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A4EA RID: 42218
			// (set) Token: 0x0600D522 RID: 54562 RVA: 0x0012EF91 File Offset: 0x0012D191
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A4EB RID: 42219
			// (set) Token: 0x0600D523 RID: 54563 RVA: 0x0012EFA9 File Offset: 0x0012D1A9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A4EC RID: 42220
			// (set) Token: 0x0600D524 RID: 54564 RVA: 0x0012EFC1 File Offset: 0x0012D1C1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A4ED RID: 42221
			// (set) Token: 0x0600D525 RID: 54565 RVA: 0x0012EFD9 File Offset: 0x0012D1D9
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A4EE RID: 42222
			// (set) Token: 0x0600D526 RID: 54566 RVA: 0x0012EFF1 File Offset: 0x0012D1F1
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
