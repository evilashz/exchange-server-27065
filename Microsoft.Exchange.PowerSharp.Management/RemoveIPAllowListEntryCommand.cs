using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000746 RID: 1862
	public class RemoveIPAllowListEntryCommand : SyntheticCommandWithPipelineInput<IPAllowListEntry, IPAllowListEntry>
	{
		// Token: 0x06005F5D RID: 24413 RVA: 0x00093547 File Offset: 0x00091747
		private RemoveIPAllowListEntryCommand() : base("Remove-IPAllowListEntry")
		{
		}

		// Token: 0x06005F5E RID: 24414 RVA: 0x00093554 File Offset: 0x00091754
		public RemoveIPAllowListEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005F5F RID: 24415 RVA: 0x00093563 File Offset: 0x00091763
		public virtual RemoveIPAllowListEntryCommand SetParameters(RemoveIPAllowListEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005F60 RID: 24416 RVA: 0x0009356D File Offset: 0x0009176D
		public virtual RemoveIPAllowListEntryCommand SetParameters(RemoveIPAllowListEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000747 RID: 1863
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C8A RID: 15498
			// (set) Token: 0x06005F61 RID: 24417 RVA: 0x00093577 File Offset: 0x00091777
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C8B RID: 15499
			// (set) Token: 0x06005F62 RID: 24418 RVA: 0x0009358A File Offset: 0x0009178A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C8C RID: 15500
			// (set) Token: 0x06005F63 RID: 24419 RVA: 0x000935A2 File Offset: 0x000917A2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C8D RID: 15501
			// (set) Token: 0x06005F64 RID: 24420 RVA: 0x000935BA File Offset: 0x000917BA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C8E RID: 15502
			// (set) Token: 0x06005F65 RID: 24421 RVA: 0x000935D2 File Offset: 0x000917D2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C8F RID: 15503
			// (set) Token: 0x06005F66 RID: 24422 RVA: 0x000935EA File Offset: 0x000917EA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003C90 RID: 15504
			// (set) Token: 0x06005F67 RID: 24423 RVA: 0x00093602 File Offset: 0x00091802
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000748 RID: 1864
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003C91 RID: 15505
			// (set) Token: 0x06005F69 RID: 24425 RVA: 0x00093622 File Offset: 0x00091822
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPListEntryIdentity(value) : null);
				}
			}

			// Token: 0x17003C92 RID: 15506
			// (set) Token: 0x06005F6A RID: 24426 RVA: 0x00093640 File Offset: 0x00091840
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C93 RID: 15507
			// (set) Token: 0x06005F6B RID: 24427 RVA: 0x00093653 File Offset: 0x00091853
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C94 RID: 15508
			// (set) Token: 0x06005F6C RID: 24428 RVA: 0x0009366B File Offset: 0x0009186B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C95 RID: 15509
			// (set) Token: 0x06005F6D RID: 24429 RVA: 0x00093683 File Offset: 0x00091883
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C96 RID: 15510
			// (set) Token: 0x06005F6E RID: 24430 RVA: 0x0009369B File Offset: 0x0009189B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C97 RID: 15511
			// (set) Token: 0x06005F6F RID: 24431 RVA: 0x000936B3 File Offset: 0x000918B3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003C98 RID: 15512
			// (set) Token: 0x06005F70 RID: 24432 RVA: 0x000936CB File Offset: 0x000918CB
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
