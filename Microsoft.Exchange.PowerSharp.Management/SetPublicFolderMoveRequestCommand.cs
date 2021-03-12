using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A74 RID: 2676
	public class SetPublicFolderMoveRequestCommand : SyntheticCommandWithPipelineInputNoOutput<PublicFolderMoveRequestIdParameter>
	{
		// Token: 0x060084C5 RID: 33989 RVA: 0x000C41B9 File Offset: 0x000C23B9
		private SetPublicFolderMoveRequestCommand() : base("Set-PublicFolderMoveRequest")
		{
		}

		// Token: 0x060084C6 RID: 33990 RVA: 0x000C41C6 File Offset: 0x000C23C6
		public SetPublicFolderMoveRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060084C7 RID: 33991 RVA: 0x000C41D5 File Offset: 0x000C23D5
		public virtual SetPublicFolderMoveRequestCommand SetParameters(SetPublicFolderMoveRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060084C8 RID: 33992 RVA: 0x000C41DF File Offset: 0x000C23DF
		public virtual SetPublicFolderMoveRequestCommand SetParameters(SetPublicFolderMoveRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A75 RID: 2677
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005B96 RID: 23446
			// (set) Token: 0x060084C9 RID: 33993 RVA: 0x000C41E9 File Offset: 0x000C23E9
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005B97 RID: 23447
			// (set) Token: 0x060084CA RID: 33994 RVA: 0x000C4201 File Offset: 0x000C2401
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005B98 RID: 23448
			// (set) Token: 0x060084CB RID: 33995 RVA: 0x000C421F File Offset: 0x000C241F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005B99 RID: 23449
			// (set) Token: 0x060084CC RID: 33996 RVA: 0x000C4232 File Offset: 0x000C2432
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005B9A RID: 23450
			// (set) Token: 0x060084CD RID: 33997 RVA: 0x000C424A File Offset: 0x000C244A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005B9B RID: 23451
			// (set) Token: 0x060084CE RID: 33998 RVA: 0x000C4262 File Offset: 0x000C2462
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005B9C RID: 23452
			// (set) Token: 0x060084CF RID: 33999 RVA: 0x000C427A File Offset: 0x000C247A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005B9D RID: 23453
			// (set) Token: 0x060084D0 RID: 34000 RVA: 0x000C4292 File Offset: 0x000C2492
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A76 RID: 2678
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005B9E RID: 23454
			// (set) Token: 0x060084D2 RID: 34002 RVA: 0x000C42B2 File Offset: 0x000C24B2
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005B9F RID: 23455
			// (set) Token: 0x060084D3 RID: 34003 RVA: 0x000C42CA File Offset: 0x000C24CA
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005BA0 RID: 23456
			// (set) Token: 0x060084D4 RID: 34004 RVA: 0x000C42E2 File Offset: 0x000C24E2
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005BA1 RID: 23457
			// (set) Token: 0x060084D5 RID: 34005 RVA: 0x000C42FA File Offset: 0x000C24FA
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005BA2 RID: 23458
			// (set) Token: 0x060084D6 RID: 34006 RVA: 0x000C4312 File Offset: 0x000C2512
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005BA3 RID: 23459
			// (set) Token: 0x060084D7 RID: 34007 RVA: 0x000C432A File Offset: 0x000C252A
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005BA4 RID: 23460
			// (set) Token: 0x060084D8 RID: 34008 RVA: 0x000C4342 File Offset: 0x000C2542
			public virtual bool SuspendWhenReadyToComplete
			{
				set
				{
					base.PowerSharpParameters["SuspendWhenReadyToComplete"] = value;
				}
			}

			// Token: 0x17005BA5 RID: 23461
			// (set) Token: 0x060084D9 RID: 34009 RVA: 0x000C435A File Offset: 0x000C255A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMoveRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005BA6 RID: 23462
			// (set) Token: 0x060084DA RID: 34010 RVA: 0x000C4378 File Offset: 0x000C2578
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005BA7 RID: 23463
			// (set) Token: 0x060084DB RID: 34011 RVA: 0x000C438B File Offset: 0x000C258B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005BA8 RID: 23464
			// (set) Token: 0x060084DC RID: 34012 RVA: 0x000C43A3 File Offset: 0x000C25A3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005BA9 RID: 23465
			// (set) Token: 0x060084DD RID: 34013 RVA: 0x000C43BB File Offset: 0x000C25BB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005BAA RID: 23466
			// (set) Token: 0x060084DE RID: 34014 RVA: 0x000C43D3 File Offset: 0x000C25D3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005BAB RID: 23467
			// (set) Token: 0x060084DF RID: 34015 RVA: 0x000C43EB File Offset: 0x000C25EB
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
