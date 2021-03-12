using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AA3 RID: 2723
	public class ResumePublicFolderMailboxMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMailboxMigrationRequestIdParameter, PublicFolderMailboxMigrationRequestIdParameter>
	{
		// Token: 0x060086D9 RID: 34521 RVA: 0x000C6D1C File Offset: 0x000C4F1C
		private ResumePublicFolderMailboxMigrationRequestCommand() : base("Resume-PublicFolderMailboxMigrationRequest")
		{
		}

		// Token: 0x060086DA RID: 34522 RVA: 0x000C6D29 File Offset: 0x000C4F29
		public ResumePublicFolderMailboxMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060086DB RID: 34523 RVA: 0x000C6D38 File Offset: 0x000C4F38
		public virtual ResumePublicFolderMailboxMigrationRequestCommand SetParameters(ResumePublicFolderMailboxMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060086DC RID: 34524 RVA: 0x000C6D42 File Offset: 0x000C4F42
		public virtual ResumePublicFolderMailboxMigrationRequestCommand SetParameters(ResumePublicFolderMailboxMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AA4 RID: 2724
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005D4C RID: 23884
			// (set) Token: 0x060086DD RID: 34525 RVA: 0x000C6D4C File Offset: 0x000C4F4C
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005D4D RID: 23885
			// (set) Token: 0x060086DE RID: 34526 RVA: 0x000C6D64 File Offset: 0x000C4F64
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMailboxMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005D4E RID: 23886
			// (set) Token: 0x060086DF RID: 34527 RVA: 0x000C6D82 File Offset: 0x000C4F82
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D4F RID: 23887
			// (set) Token: 0x060086E0 RID: 34528 RVA: 0x000C6D95 File Offset: 0x000C4F95
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D50 RID: 23888
			// (set) Token: 0x060086E1 RID: 34529 RVA: 0x000C6DAD File Offset: 0x000C4FAD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D51 RID: 23889
			// (set) Token: 0x060086E2 RID: 34530 RVA: 0x000C6DC5 File Offset: 0x000C4FC5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D52 RID: 23890
			// (set) Token: 0x060086E3 RID: 34531 RVA: 0x000C6DDD File Offset: 0x000C4FDD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D53 RID: 23891
			// (set) Token: 0x060086E4 RID: 34532 RVA: 0x000C6DF5 File Offset: 0x000C4FF5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AA5 RID: 2725
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005D54 RID: 23892
			// (set) Token: 0x060086E6 RID: 34534 RVA: 0x000C6E15 File Offset: 0x000C5015
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005D55 RID: 23893
			// (set) Token: 0x060086E7 RID: 34535 RVA: 0x000C6E28 File Offset: 0x000C5028
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005D56 RID: 23894
			// (set) Token: 0x060086E8 RID: 34536 RVA: 0x000C6E40 File Offset: 0x000C5040
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005D57 RID: 23895
			// (set) Token: 0x060086E9 RID: 34537 RVA: 0x000C6E58 File Offset: 0x000C5058
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005D58 RID: 23896
			// (set) Token: 0x060086EA RID: 34538 RVA: 0x000C6E70 File Offset: 0x000C5070
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005D59 RID: 23897
			// (set) Token: 0x060086EB RID: 34539 RVA: 0x000C6E88 File Offset: 0x000C5088
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
