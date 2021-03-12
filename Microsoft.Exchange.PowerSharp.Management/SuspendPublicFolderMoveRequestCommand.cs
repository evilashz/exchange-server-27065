using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A77 RID: 2679
	public class SuspendPublicFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMoveRequestIdParameter, PublicFolderMoveRequestIdParameter>
	{
		// Token: 0x060084E1 RID: 34017 RVA: 0x000C440B File Offset: 0x000C260B
		private SuspendPublicFolderMoveRequestCommand() : base("Suspend-PublicFolderMoveRequest")
		{
		}

		// Token: 0x060084E2 RID: 34018 RVA: 0x000C4418 File Offset: 0x000C2618
		public SuspendPublicFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060084E3 RID: 34019 RVA: 0x000C4427 File Offset: 0x000C2627
		public virtual SuspendPublicFolderMoveRequestCommand SetParameters(SuspendPublicFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060084E4 RID: 34020 RVA: 0x000C4431 File Offset: 0x000C2631
		public virtual SuspendPublicFolderMoveRequestCommand SetParameters(SuspendPublicFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A78 RID: 2680
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005BAC RID: 23468
			// (set) Token: 0x060084E5 RID: 34021 RVA: 0x000C443B File Offset: 0x000C263B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005BAD RID: 23469
			// (set) Token: 0x060084E6 RID: 34022 RVA: 0x000C4459 File Offset: 0x000C2659
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005BAE RID: 23470
			// (set) Token: 0x060084E7 RID: 34023 RVA: 0x000C446C File Offset: 0x000C266C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BAF RID: 23471
			// (set) Token: 0x060084E8 RID: 34024 RVA: 0x000C447F File Offset: 0x000C267F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BB0 RID: 23472
			// (set) Token: 0x060084E9 RID: 34025 RVA: 0x000C4497 File Offset: 0x000C2697
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BB1 RID: 23473
			// (set) Token: 0x060084EA RID: 34026 RVA: 0x000C44AF File Offset: 0x000C26AF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BB2 RID: 23474
			// (set) Token: 0x060084EB RID: 34027 RVA: 0x000C44C7 File Offset: 0x000C26C7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005BB3 RID: 23475
			// (set) Token: 0x060084EC RID: 34028 RVA: 0x000C44DF File Offset: 0x000C26DF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005BB4 RID: 23476
			// (set) Token: 0x060084ED RID: 34029 RVA: 0x000C44F7 File Offset: 0x000C26F7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A79 RID: 2681
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005BB5 RID: 23477
			// (set) Token: 0x060084EF RID: 34031 RVA: 0x000C4517 File Offset: 0x000C2717
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005BB6 RID: 23478
			// (set) Token: 0x060084F0 RID: 34032 RVA: 0x000C452A File Offset: 0x000C272A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BB7 RID: 23479
			// (set) Token: 0x060084F1 RID: 34033 RVA: 0x000C453D File Offset: 0x000C273D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BB8 RID: 23480
			// (set) Token: 0x060084F2 RID: 34034 RVA: 0x000C4555 File Offset: 0x000C2755
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BB9 RID: 23481
			// (set) Token: 0x060084F3 RID: 34035 RVA: 0x000C456D File Offset: 0x000C276D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BBA RID: 23482
			// (set) Token: 0x060084F4 RID: 34036 RVA: 0x000C4585 File Offset: 0x000C2785
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005BBB RID: 23483
			// (set) Token: 0x060084F5 RID: 34037 RVA: 0x000C459D File Offset: 0x000C279D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005BBC RID: 23484
			// (set) Token: 0x060084F6 RID: 34038 RVA: 0x000C45B5 File Offset: 0x000C27B5
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
