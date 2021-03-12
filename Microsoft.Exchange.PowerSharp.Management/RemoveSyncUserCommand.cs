using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DE8 RID: 3560
	public class RemoveSyncUserCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600D469 RID: 54377 RVA: 0x0012E087 File Offset: 0x0012C287
		private RemoveSyncUserCommand() : base("Remove-SyncUser")
		{
		}

		// Token: 0x0600D46A RID: 54378 RVA: 0x0012E094 File Offset: 0x0012C294
		public RemoveSyncUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D46B RID: 54379 RVA: 0x0012E0A3 File Offset: 0x0012C2A3
		public virtual RemoveSyncUserCommand SetParameters(RemoveSyncUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D46C RID: 54380 RVA: 0x0012E0AD File Offset: 0x0012C2AD
		public virtual RemoveSyncUserCommand SetParameters(RemoveSyncUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DE9 RID: 3561
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A452 RID: 42066
			// (set) Token: 0x0600D46D RID: 54381 RVA: 0x0012E0B7 File Offset: 0x0012C2B7
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x1700A453 RID: 42067
			// (set) Token: 0x0600D46E RID: 54382 RVA: 0x0012E0CF File Offset: 0x0012C2CF
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x1700A454 RID: 42068
			// (set) Token: 0x0600D46F RID: 54383 RVA: 0x0012E0E7 File Offset: 0x0012C2E7
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A455 RID: 42069
			// (set) Token: 0x0600D470 RID: 54384 RVA: 0x0012E0FF File Offset: 0x0012C2FF
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A456 RID: 42070
			// (set) Token: 0x0600D471 RID: 54385 RVA: 0x0012E117 File Offset: 0x0012C317
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A457 RID: 42071
			// (set) Token: 0x0600D472 RID: 54386 RVA: 0x0012E12A File Offset: 0x0012C32A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A458 RID: 42072
			// (set) Token: 0x0600D473 RID: 54387 RVA: 0x0012E142 File Offset: 0x0012C342
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A459 RID: 42073
			// (set) Token: 0x0600D474 RID: 54388 RVA: 0x0012E15A File Offset: 0x0012C35A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A45A RID: 42074
			// (set) Token: 0x0600D475 RID: 54389 RVA: 0x0012E172 File Offset: 0x0012C372
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A45B RID: 42075
			// (set) Token: 0x0600D476 RID: 54390 RVA: 0x0012E18A File Offset: 0x0012C38A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A45C RID: 42076
			// (set) Token: 0x0600D477 RID: 54391 RVA: 0x0012E1A2 File Offset: 0x0012C3A2
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000DEA RID: 3562
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A45D RID: 42077
			// (set) Token: 0x0600D479 RID: 54393 RVA: 0x0012E1C2 File Offset: 0x0012C3C2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new NonMailEnabledUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A45E RID: 42078
			// (set) Token: 0x0600D47A RID: 54394 RVA: 0x0012E1E0 File Offset: 0x0012C3E0
			public virtual SwitchParameter ForReconciliation
			{
				set
				{
					base.PowerSharpParameters["ForReconciliation"] = value;
				}
			}

			// Token: 0x1700A45F RID: 42079
			// (set) Token: 0x0600D47B RID: 54395 RVA: 0x0012E1F8 File Offset: 0x0012C3F8
			public virtual bool Permanent
			{
				set
				{
					base.PowerSharpParameters["Permanent"] = value;
				}
			}

			// Token: 0x1700A460 RID: 42080
			// (set) Token: 0x0600D47C RID: 54396 RVA: 0x0012E210 File Offset: 0x0012C410
			public virtual SwitchParameter KeepWindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["KeepWindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A461 RID: 42081
			// (set) Token: 0x0600D47D RID: 54397 RVA: 0x0012E228 File Offset: 0x0012C428
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700A462 RID: 42082
			// (set) Token: 0x0600D47E RID: 54398 RVA: 0x0012E240 File Offset: 0x0012C440
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A463 RID: 42083
			// (set) Token: 0x0600D47F RID: 54399 RVA: 0x0012E253 File Offset: 0x0012C453
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A464 RID: 42084
			// (set) Token: 0x0600D480 RID: 54400 RVA: 0x0012E26B File Offset: 0x0012C46B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A465 RID: 42085
			// (set) Token: 0x0600D481 RID: 54401 RVA: 0x0012E283 File Offset: 0x0012C483
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A466 RID: 42086
			// (set) Token: 0x0600D482 RID: 54402 RVA: 0x0012E29B File Offset: 0x0012C49B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A467 RID: 42087
			// (set) Token: 0x0600D483 RID: 54403 RVA: 0x0012E2B3 File Offset: 0x0012C4B3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A468 RID: 42088
			// (set) Token: 0x0600D484 RID: 54404 RVA: 0x0012E2CB File Offset: 0x0012C4CB
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
