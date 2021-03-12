using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A92 RID: 2706
	public class SuspendPublicFolderMigrationRequestCommand : SyntheticCommandWithPipelineInput<PublicFolderMigrationRequestIdParameter, PublicFolderMigrationRequestIdParameter>
	{
		// Token: 0x06008602 RID: 34306 RVA: 0x000C5B4C File Offset: 0x000C3D4C
		private SuspendPublicFolderMigrationRequestCommand() : base("Suspend-PublicFolderMigrationRequest")
		{
		}

		// Token: 0x06008603 RID: 34307 RVA: 0x000C5B59 File Offset: 0x000C3D59
		public SuspendPublicFolderMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008604 RID: 34308 RVA: 0x000C5B68 File Offset: 0x000C3D68
		public virtual SuspendPublicFolderMigrationRequestCommand SetParameters(SuspendPublicFolderMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008605 RID: 34309 RVA: 0x000C5B72 File Offset: 0x000C3D72
		public virtual SuspendPublicFolderMigrationRequestCommand SetParameters(SuspendPublicFolderMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A93 RID: 2707
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005C97 RID: 23703
			// (set) Token: 0x06008606 RID: 34310 RVA: 0x000C5B7C File Offset: 0x000C3D7C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005C98 RID: 23704
			// (set) Token: 0x06008607 RID: 34311 RVA: 0x000C5B9A File Offset: 0x000C3D9A
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005C99 RID: 23705
			// (set) Token: 0x06008608 RID: 34312 RVA: 0x000C5BAD File Offset: 0x000C3DAD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C9A RID: 23706
			// (set) Token: 0x06008609 RID: 34313 RVA: 0x000C5BC0 File Offset: 0x000C3DC0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C9B RID: 23707
			// (set) Token: 0x0600860A RID: 34314 RVA: 0x000C5BD8 File Offset: 0x000C3DD8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C9C RID: 23708
			// (set) Token: 0x0600860B RID: 34315 RVA: 0x000C5BF0 File Offset: 0x000C3DF0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C9D RID: 23709
			// (set) Token: 0x0600860C RID: 34316 RVA: 0x000C5C08 File Offset: 0x000C3E08
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C9E RID: 23710
			// (set) Token: 0x0600860D RID: 34317 RVA: 0x000C5C20 File Offset: 0x000C3E20
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005C9F RID: 23711
			// (set) Token: 0x0600860E RID: 34318 RVA: 0x000C5C38 File Offset: 0x000C3E38
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A94 RID: 2708
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005CA0 RID: 23712
			// (set) Token: 0x06008610 RID: 34320 RVA: 0x000C5C58 File Offset: 0x000C3E58
			public virtual string SuspendComment
			{
				set
				{
					base.PowerSharpParameters["SuspendComment"] = value;
				}
			}

			// Token: 0x17005CA1 RID: 23713
			// (set) Token: 0x06008611 RID: 34321 RVA: 0x000C5C6B File Offset: 0x000C3E6B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005CA2 RID: 23714
			// (set) Token: 0x06008612 RID: 34322 RVA: 0x000C5C7E File Offset: 0x000C3E7E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005CA3 RID: 23715
			// (set) Token: 0x06008613 RID: 34323 RVA: 0x000C5C96 File Offset: 0x000C3E96
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005CA4 RID: 23716
			// (set) Token: 0x06008614 RID: 34324 RVA: 0x000C5CAE File Offset: 0x000C3EAE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005CA5 RID: 23717
			// (set) Token: 0x06008615 RID: 34325 RVA: 0x000C5CC6 File Offset: 0x000C3EC6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005CA6 RID: 23718
			// (set) Token: 0x06008616 RID: 34326 RVA: 0x000C5CDE File Offset: 0x000C3EDE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005CA7 RID: 23719
			// (set) Token: 0x06008617 RID: 34327 RVA: 0x000C5CF6 File Offset: 0x000C3EF6
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
