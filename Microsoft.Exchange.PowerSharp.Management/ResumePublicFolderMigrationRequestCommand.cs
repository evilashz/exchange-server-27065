using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A8A RID: 2698
	public class ResumePublicFolderMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMigrationRequestIdParameter, PublicFolderMigrationRequestIdParameter>
	{
		// Token: 0x060085AB RID: 34219 RVA: 0x000C5446 File Offset: 0x000C3646
		private ResumePublicFolderMigrationRequestCommand() : base("Resume-PublicFolderMigrationRequest")
		{
		}

		// Token: 0x060085AC RID: 34220 RVA: 0x000C5453 File Offset: 0x000C3653
		public ResumePublicFolderMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060085AD RID: 34221 RVA: 0x000C5462 File Offset: 0x000C3662
		public virtual ResumePublicFolderMigrationRequestCommand SetParameters(ResumePublicFolderMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060085AE RID: 34222 RVA: 0x000C546C File Offset: 0x000C366C
		public virtual ResumePublicFolderMigrationRequestCommand SetParameters(ResumePublicFolderMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A8B RID: 2699
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005C50 RID: 23632
			// (set) Token: 0x060085AF RID: 34223 RVA: 0x000C5476 File Offset: 0x000C3676
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005C51 RID: 23633
			// (set) Token: 0x060085B0 RID: 34224 RVA: 0x000C5494 File Offset: 0x000C3694
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C52 RID: 23634
			// (set) Token: 0x060085B1 RID: 34225 RVA: 0x000C54A7 File Offset: 0x000C36A7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C53 RID: 23635
			// (set) Token: 0x060085B2 RID: 34226 RVA: 0x000C54BF File Offset: 0x000C36BF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C54 RID: 23636
			// (set) Token: 0x060085B3 RID: 34227 RVA: 0x000C54D7 File Offset: 0x000C36D7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C55 RID: 23637
			// (set) Token: 0x060085B4 RID: 34228 RVA: 0x000C54EF File Offset: 0x000C36EF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C56 RID: 23638
			// (set) Token: 0x060085B5 RID: 34229 RVA: 0x000C5507 File Offset: 0x000C3707
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A8C RID: 2700
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005C57 RID: 23639
			// (set) Token: 0x060085B7 RID: 34231 RVA: 0x000C5527 File Offset: 0x000C3727
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C58 RID: 23640
			// (set) Token: 0x060085B8 RID: 34232 RVA: 0x000C553A File Offset: 0x000C373A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C59 RID: 23641
			// (set) Token: 0x060085B9 RID: 34233 RVA: 0x000C5552 File Offset: 0x000C3752
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C5A RID: 23642
			// (set) Token: 0x060085BA RID: 34234 RVA: 0x000C556A File Offset: 0x000C376A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C5B RID: 23643
			// (set) Token: 0x060085BB RID: 34235 RVA: 0x000C5582 File Offset: 0x000C3782
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C5C RID: 23644
			// (set) Token: 0x060085BC RID: 34236 RVA: 0x000C559A File Offset: 0x000C379A
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
