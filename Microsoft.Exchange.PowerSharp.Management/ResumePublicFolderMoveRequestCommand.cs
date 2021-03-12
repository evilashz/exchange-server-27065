using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A71 RID: 2673
	public class ResumePublicFolderMoveRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMoveRequestIdParameter, PublicFolderMoveRequestIdParameter>
	{
		// Token: 0x060084B2 RID: 33970 RVA: 0x000C4045 File Offset: 0x000C2245
		private ResumePublicFolderMoveRequestCommand() : base("Resume-PublicFolderMoveRequest")
		{
		}

		// Token: 0x060084B3 RID: 33971 RVA: 0x000C4052 File Offset: 0x000C2252
		public ResumePublicFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060084B4 RID: 33972 RVA: 0x000C4061 File Offset: 0x000C2261
		public virtual ResumePublicFolderMoveRequestCommand SetParameters(ResumePublicFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060084B5 RID: 33973 RVA: 0x000C406B File Offset: 0x000C226B
		public virtual ResumePublicFolderMoveRequestCommand SetParameters(ResumePublicFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A72 RID: 2674
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005B89 RID: 23433
			// (set) Token: 0x060084B6 RID: 33974 RVA: 0x000C4075 File Offset: 0x000C2275
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005B8A RID: 23434
			// (set) Token: 0x060084B7 RID: 33975 RVA: 0x000C4093 File Offset: 0x000C2293
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B8B RID: 23435
			// (set) Token: 0x060084B8 RID: 33976 RVA: 0x000C40A6 File Offset: 0x000C22A6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B8C RID: 23436
			// (set) Token: 0x060084B9 RID: 33977 RVA: 0x000C40BE File Offset: 0x000C22BE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B8D RID: 23437
			// (set) Token: 0x060084BA RID: 33978 RVA: 0x000C40D6 File Offset: 0x000C22D6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B8E RID: 23438
			// (set) Token: 0x060084BB RID: 33979 RVA: 0x000C40EE File Offset: 0x000C22EE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B8F RID: 23439
			// (set) Token: 0x060084BC RID: 33980 RVA: 0x000C4106 File Offset: 0x000C2306
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A73 RID: 2675
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005B90 RID: 23440
			// (set) Token: 0x060084BE RID: 33982 RVA: 0x000C4126 File Offset: 0x000C2326
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B91 RID: 23441
			// (set) Token: 0x060084BF RID: 33983 RVA: 0x000C4139 File Offset: 0x000C2339
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B92 RID: 23442
			// (set) Token: 0x060084C0 RID: 33984 RVA: 0x000C4151 File Offset: 0x000C2351
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B93 RID: 23443
			// (set) Token: 0x060084C1 RID: 33985 RVA: 0x000C4169 File Offset: 0x000C2369
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B94 RID: 23444
			// (set) Token: 0x060084C2 RID: 33986 RVA: 0x000C4181 File Offset: 0x000C2381
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B95 RID: 23445
			// (set) Token: 0x060084C3 RID: 33987 RVA: 0x000C4199 File Offset: 0x000C2399
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
