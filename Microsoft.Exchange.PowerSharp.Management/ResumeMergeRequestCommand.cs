using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009F7 RID: 2551
	public class ResumeMergeRequestCommand : SyntheticCommandWithPipelineInput<MergeRequestIdParameter, MergeRequestIdParameter>
	{
		// Token: 0x06008011 RID: 32785 RVA: 0x000BE128 File Offset: 0x000BC328
		private ResumeMergeRequestCommand() : base("Resume-MergeRequest")
		{
		}

		// Token: 0x06008012 RID: 32786 RVA: 0x000BE135 File Offset: 0x000BC335
		public ResumeMergeRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008013 RID: 32787 RVA: 0x000BE144 File Offset: 0x000BC344
		public virtual ResumeMergeRequestCommand SetParameters(ResumeMergeRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008014 RID: 32788 RVA: 0x000BE14E File Offset: 0x000BC34E
		public virtual ResumeMergeRequestCommand SetParameters(ResumeMergeRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009F8 RID: 2552
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170057DC RID: 22492
			// (set) Token: 0x06008015 RID: 32789 RVA: 0x000BE158 File Offset: 0x000BC358
			public virtual SwitchParameter SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x170057DD RID: 22493
			// (set) Token: 0x06008016 RID: 32790 RVA: 0x000BE170 File Offset: 0x000BC370
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MergeRequestIdParameter(value) : null);
				}
			}

			// Token: 0x170057DE RID: 22494
			// (set) Token: 0x06008017 RID: 32791 RVA: 0x000BE18E File Offset: 0x000BC38E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057DF RID: 22495
			// (set) Token: 0x06008018 RID: 32792 RVA: 0x000BE1A1 File Offset: 0x000BC3A1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057E0 RID: 22496
			// (set) Token: 0x06008019 RID: 32793 RVA: 0x000BE1B9 File Offset: 0x000BC3B9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057E1 RID: 22497
			// (set) Token: 0x0600801A RID: 32794 RVA: 0x000BE1D1 File Offset: 0x000BC3D1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057E2 RID: 22498
			// (set) Token: 0x0600801B RID: 32795 RVA: 0x000BE1E9 File Offset: 0x000BC3E9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057E3 RID: 22499
			// (set) Token: 0x0600801C RID: 32796 RVA: 0x000BE201 File Offset: 0x000BC401
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020009F9 RID: 2553
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170057E4 RID: 22500
			// (set) Token: 0x0600801E RID: 32798 RVA: 0x000BE221 File Offset: 0x000BC421
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170057E5 RID: 22501
			// (set) Token: 0x0600801F RID: 32799 RVA: 0x000BE234 File Offset: 0x000BC434
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170057E6 RID: 22502
			// (set) Token: 0x06008020 RID: 32800 RVA: 0x000BE24C File Offset: 0x000BC44C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170057E7 RID: 22503
			// (set) Token: 0x06008021 RID: 32801 RVA: 0x000BE264 File Offset: 0x000BC464
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170057E8 RID: 22504
			// (set) Token: 0x06008022 RID: 32802 RVA: 0x000BE27C File Offset: 0x000BC47C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170057E9 RID: 22505
			// (set) Token: 0x06008023 RID: 32803 RVA: 0x000BE294 File Offset: 0x000BC494
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
