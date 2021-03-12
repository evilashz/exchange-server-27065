using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000749 RID: 1865
	public class RemoveIPBlocklistEntryCommand : SyntheticCommandWithPipelineInput<IPBlockListEntry, IPBlockListEntry>
	{
		// Token: 0x06005F72 RID: 24434 RVA: 0x000936EB File Offset: 0x000918EB
		private RemoveIPBlocklistEntryCommand() : base("Remove-IPBlocklistEntry")
		{
		}

		// Token: 0x06005F73 RID: 24435 RVA: 0x000936F8 File Offset: 0x000918F8
		public RemoveIPBlocklistEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005F74 RID: 24436 RVA: 0x00093707 File Offset: 0x00091907
		public virtual RemoveIPBlocklistEntryCommand SetParameters(RemoveIPBlocklistEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005F75 RID: 24437 RVA: 0x00093711 File Offset: 0x00091911
		public virtual RemoveIPBlocklistEntryCommand SetParameters(RemoveIPBlocklistEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200074A RID: 1866
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C99 RID: 15513
			// (set) Token: 0x06005F76 RID: 24438 RVA: 0x0009371B File Offset: 0x0009191B
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C9A RID: 15514
			// (set) Token: 0x06005F77 RID: 24439 RVA: 0x0009372E File Offset: 0x0009192E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C9B RID: 15515
			// (set) Token: 0x06005F78 RID: 24440 RVA: 0x00093746 File Offset: 0x00091946
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C9C RID: 15516
			// (set) Token: 0x06005F79 RID: 24441 RVA: 0x0009375E File Offset: 0x0009195E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C9D RID: 15517
			// (set) Token: 0x06005F7A RID: 24442 RVA: 0x00093776 File Offset: 0x00091976
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C9E RID: 15518
			// (set) Token: 0x06005F7B RID: 24443 RVA: 0x0009378E File Offset: 0x0009198E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003C9F RID: 15519
			// (set) Token: 0x06005F7C RID: 24444 RVA: 0x000937A6 File Offset: 0x000919A6
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200074B RID: 1867
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003CA0 RID: 15520
			// (set) Token: 0x06005F7E RID: 24446 RVA: 0x000937C6 File Offset: 0x000919C6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPListEntryIdentity(value) : null);
				}
			}

			// Token: 0x17003CA1 RID: 15521
			// (set) Token: 0x06005F7F RID: 24447 RVA: 0x000937E4 File Offset: 0x000919E4
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003CA2 RID: 15522
			// (set) Token: 0x06005F80 RID: 24448 RVA: 0x000937F7 File Offset: 0x000919F7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CA3 RID: 15523
			// (set) Token: 0x06005F81 RID: 24449 RVA: 0x0009380F File Offset: 0x00091A0F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CA4 RID: 15524
			// (set) Token: 0x06005F82 RID: 24450 RVA: 0x00093827 File Offset: 0x00091A27
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CA5 RID: 15525
			// (set) Token: 0x06005F83 RID: 24451 RVA: 0x0009383F File Offset: 0x00091A3F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003CA6 RID: 15526
			// (set) Token: 0x06005F84 RID: 24452 RVA: 0x00093857 File Offset: 0x00091A57
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003CA7 RID: 15527
			// (set) Token: 0x06005F85 RID: 24453 RVA: 0x0009386F File Offset: 0x00091A6F
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
