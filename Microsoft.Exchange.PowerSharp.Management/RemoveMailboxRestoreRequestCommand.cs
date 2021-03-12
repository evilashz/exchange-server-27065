using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A55 RID: 2645
	public class RemoveMailboxRestoreRequestCommand : SyntheticCommandWithPipelineInput<MailboxRestoreRequestIdParameter, MailboxRestoreRequestIdParameter>
	{
		// Token: 0x060083BB RID: 33723 RVA: 0x000C2C9E File Offset: 0x000C0E9E
		private RemoveMailboxRestoreRequestCommand() : base("Remove-MailboxRestoreRequest")
		{
		}

		// Token: 0x060083BC RID: 33724 RVA: 0x000C2CAB File Offset: 0x000C0EAB
		public RemoveMailboxRestoreRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060083BD RID: 33725 RVA: 0x000C2CBA File Offset: 0x000C0EBA
		public virtual RemoveMailboxRestoreRequestCommand SetParameters(RemoveMailboxRestoreRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060083BE RID: 33726 RVA: 0x000C2CC4 File Offset: 0x000C0EC4
		public virtual RemoveMailboxRestoreRequestCommand SetParameters(RemoveMailboxRestoreRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060083BF RID: 33727 RVA: 0x000C2CCE File Offset: 0x000C0ECE
		public virtual RemoveMailboxRestoreRequestCommand SetParameters(RemoveMailboxRestoreRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A56 RID: 2646
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005ACA RID: 23242
			// (set) Token: 0x060083C0 RID: 33728 RVA: 0x000C2CD8 File Offset: 0x000C0ED8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxRestoreRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005ACB RID: 23243
			// (set) Token: 0x060083C1 RID: 33729 RVA: 0x000C2CF6 File Offset: 0x000C0EF6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005ACC RID: 23244
			// (set) Token: 0x060083C2 RID: 33730 RVA: 0x000C2D09 File Offset: 0x000C0F09
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005ACD RID: 23245
			// (set) Token: 0x060083C3 RID: 33731 RVA: 0x000C2D21 File Offset: 0x000C0F21
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005ACE RID: 23246
			// (set) Token: 0x060083C4 RID: 33732 RVA: 0x000C2D39 File Offset: 0x000C0F39
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005ACF RID: 23247
			// (set) Token: 0x060083C5 RID: 33733 RVA: 0x000C2D51 File Offset: 0x000C0F51
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005AD0 RID: 23248
			// (set) Token: 0x060083C6 RID: 33734 RVA: 0x000C2D69 File Offset: 0x000C0F69
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005AD1 RID: 23249
			// (set) Token: 0x060083C7 RID: 33735 RVA: 0x000C2D81 File Offset: 0x000C0F81
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A57 RID: 2647
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005AD2 RID: 23250
			// (set) Token: 0x060083C9 RID: 33737 RVA: 0x000C2DA1 File Offset: 0x000C0FA1
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005AD3 RID: 23251
			// (set) Token: 0x060083CA RID: 33738 RVA: 0x000C2DB4 File Offset: 0x000C0FB4
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005AD4 RID: 23252
			// (set) Token: 0x060083CB RID: 33739 RVA: 0x000C2DCC File Offset: 0x000C0FCC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005AD5 RID: 23253
			// (set) Token: 0x060083CC RID: 33740 RVA: 0x000C2DDF File Offset: 0x000C0FDF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005AD6 RID: 23254
			// (set) Token: 0x060083CD RID: 33741 RVA: 0x000C2DF7 File Offset: 0x000C0FF7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005AD7 RID: 23255
			// (set) Token: 0x060083CE RID: 33742 RVA: 0x000C2E0F File Offset: 0x000C100F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005AD8 RID: 23256
			// (set) Token: 0x060083CF RID: 33743 RVA: 0x000C2E27 File Offset: 0x000C1027
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005AD9 RID: 23257
			// (set) Token: 0x060083D0 RID: 33744 RVA: 0x000C2E3F File Offset: 0x000C103F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005ADA RID: 23258
			// (set) Token: 0x060083D1 RID: 33745 RVA: 0x000C2E57 File Offset: 0x000C1057
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A58 RID: 2648
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005ADB RID: 23259
			// (set) Token: 0x060083D3 RID: 33747 RVA: 0x000C2E77 File Offset: 0x000C1077
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005ADC RID: 23260
			// (set) Token: 0x060083D4 RID: 33748 RVA: 0x000C2E8A File Offset: 0x000C108A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005ADD RID: 23261
			// (set) Token: 0x060083D5 RID: 33749 RVA: 0x000C2EA2 File Offset: 0x000C10A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005ADE RID: 23262
			// (set) Token: 0x060083D6 RID: 33750 RVA: 0x000C2EBA File Offset: 0x000C10BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005ADF RID: 23263
			// (set) Token: 0x060083D7 RID: 33751 RVA: 0x000C2ED2 File Offset: 0x000C10D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005AE0 RID: 23264
			// (set) Token: 0x060083D8 RID: 33752 RVA: 0x000C2EEA File Offset: 0x000C10EA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005AE1 RID: 23265
			// (set) Token: 0x060083D9 RID: 33753 RVA: 0x000C2F02 File Offset: 0x000C1102
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
