using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DEB RID: 3563
	public class UndoSyncSoftDeletedUserCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600D486 RID: 54406 RVA: 0x0012E2EB File Offset: 0x0012C4EB
		private UndoSyncSoftDeletedUserCommand() : base("Undo-SyncSoftDeletedUser")
		{
		}

		// Token: 0x0600D487 RID: 54407 RVA: 0x0012E2F8 File Offset: 0x0012C4F8
		public UndoSyncSoftDeletedUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D488 RID: 54408 RVA: 0x0012E307 File Offset: 0x0012C507
		public virtual UndoSyncSoftDeletedUserCommand SetParameters(UndoSyncSoftDeletedUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DEC RID: 3564
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A469 RID: 42089
			// (set) Token: 0x0600D489 RID: 54409 RVA: 0x0012E311 File Offset: 0x0012C511
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A46A RID: 42090
			// (set) Token: 0x0600D48A RID: 54410 RVA: 0x0012E324 File Offset: 0x0012C524
			public virtual string SoftDeletedObject
			{
				set
				{
					base.PowerSharpParameters["SoftDeletedObject"] = ((value != null) ? new NonMailEnabledUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700A46B RID: 42091
			// (set) Token: 0x0600D48B RID: 54411 RVA: 0x0012E342 File Offset: 0x0012C542
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A46C RID: 42092
			// (set) Token: 0x0600D48C RID: 54412 RVA: 0x0012E355 File Offset: 0x0012C555
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700A46D RID: 42093
			// (set) Token: 0x0600D48D RID: 54413 RVA: 0x0012E368 File Offset: 0x0012C568
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x1700A46E RID: 42094
			// (set) Token: 0x0600D48E RID: 54414 RVA: 0x0012E380 File Offset: 0x0012C580
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A46F RID: 42095
			// (set) Token: 0x0600D48F RID: 54415 RVA: 0x0012E393 File Offset: 0x0012C593
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A470 RID: 42096
			// (set) Token: 0x0600D490 RID: 54416 RVA: 0x0012E3B1 File Offset: 0x0012C5B1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A471 RID: 42097
			// (set) Token: 0x0600D491 RID: 54417 RVA: 0x0012E3C4 File Offset: 0x0012C5C4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A472 RID: 42098
			// (set) Token: 0x0600D492 RID: 54418 RVA: 0x0012E3DC File Offset: 0x0012C5DC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A473 RID: 42099
			// (set) Token: 0x0600D493 RID: 54419 RVA: 0x0012E3F4 File Offset: 0x0012C5F4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A474 RID: 42100
			// (set) Token: 0x0600D494 RID: 54420 RVA: 0x0012E40C File Offset: 0x0012C60C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A475 RID: 42101
			// (set) Token: 0x0600D495 RID: 54421 RVA: 0x0012E424 File Offset: 0x0012C624
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
