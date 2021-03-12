using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AA6 RID: 2726
	public class SuspendPublicFolderMailboxMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMailboxMigrationRequestIdParameter, PublicFolderMailboxMigrationRequestIdParameter>
	{
		// Token: 0x060086ED RID: 34541 RVA: 0x000C6EA8 File Offset: 0x000C50A8
		private SuspendPublicFolderMailboxMigrationRequestCommand() : base("Suspend-PublicFolderMailboxMigrationRequest")
		{
		}

		// Token: 0x060086EE RID: 34542 RVA: 0x000C6EB5 File Offset: 0x000C50B5
		public SuspendPublicFolderMailboxMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060086EF RID: 34543 RVA: 0x000C6EC4 File Offset: 0x000C50C4
		public virtual SuspendPublicFolderMailboxMigrationRequestCommand SetParameters(SuspendPublicFolderMailboxMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060086F0 RID: 34544 RVA: 0x000C6ECE File Offset: 0x000C50CE
		public virtual SuspendPublicFolderMailboxMigrationRequestCommand SetParameters(SuspendPublicFolderMailboxMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AA7 RID: 2727
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005D5A RID: 23898
			// (set) Token: 0x060086F1 RID: 34545 RVA: 0x000C6ED8 File Offset: 0x000C50D8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D5B RID: 23899
			// (set) Token: 0x060086F2 RID: 34546 RVA: 0x000C6EF6 File Offset: 0x000C50F6
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005D5C RID: 23900
			// (set) Token: 0x060086F3 RID: 34547 RVA: 0x000C6F09 File Offset: 0x000C5109
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D5D RID: 23901
			// (set) Token: 0x060086F4 RID: 34548 RVA: 0x000C6F1C File Offset: 0x000C511C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D5E RID: 23902
			// (set) Token: 0x060086F5 RID: 34549 RVA: 0x000C6F34 File Offset: 0x000C5134
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D5F RID: 23903
			// (set) Token: 0x060086F6 RID: 34550 RVA: 0x000C6F4C File Offset: 0x000C514C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D60 RID: 23904
			// (set) Token: 0x060086F7 RID: 34551 RVA: 0x000C6F64 File Offset: 0x000C5164
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D61 RID: 23905
			// (set) Token: 0x060086F8 RID: 34552 RVA: 0x000C6F7C File Offset: 0x000C517C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005D62 RID: 23906
			// (set) Token: 0x060086F9 RID: 34553 RVA: 0x000C6F94 File Offset: 0x000C5194
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000AA8 RID: 2728
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005D63 RID: 23907
			// (set) Token: 0x060086FB RID: 34555 RVA: 0x000C6FB4 File Offset: 0x000C51B4
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005D64 RID: 23908
			// (set) Token: 0x060086FC RID: 34556 RVA: 0x000C6FC7 File Offset: 0x000C51C7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D65 RID: 23909
			// (set) Token: 0x060086FD RID: 34557 RVA: 0x000C6FDA File Offset: 0x000C51DA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D66 RID: 23910
			// (set) Token: 0x060086FE RID: 34558 RVA: 0x000C6FF2 File Offset: 0x000C51F2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D67 RID: 23911
			// (set) Token: 0x060086FF RID: 34559 RVA: 0x000C700A File Offset: 0x000C520A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D68 RID: 23912
			// (set) Token: 0x06008700 RID: 34560 RVA: 0x000C7022 File Offset: 0x000C5222
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D69 RID: 23913
			// (set) Token: 0x06008701 RID: 34561 RVA: 0x000C703A File Offset: 0x000C523A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005D6A RID: 23914
			// (set) Token: 0x06008702 RID: 34562 RVA: 0x000C7052 File Offset: 0x000C5252
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
