using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200045F RID: 1119
	public class RemoveMailboxFolderPermissionCommand : SyntheticCommandWithPipelineInput<MailboxFolder, MailboxFolder>
	{
		// Token: 0x06004030 RID: 16432 RVA: 0x0006B11D File Offset: 0x0006931D
		private RemoveMailboxFolderPermissionCommand() : base("Remove-MailboxFolderPermission")
		{
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x0006B12A File Offset: 0x0006932A
		public RemoveMailboxFolderPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004032 RID: 16434 RVA: 0x0006B139 File Offset: 0x00069339
		public virtual RemoveMailboxFolderPermissionCommand SetParameters(RemoveMailboxFolderPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004033 RID: 16435 RVA: 0x0006B143 File Offset: 0x00069343
		public virtual RemoveMailboxFolderPermissionCommand SetParameters(RemoveMailboxFolderPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000460 RID: 1120
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700232B RID: 9003
			// (set) Token: 0x06004034 RID: 16436 RVA: 0x0006B14D File Offset: 0x0006934D
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700232C RID: 9004
			// (set) Token: 0x06004035 RID: 16437 RVA: 0x0006B16B File Offset: 0x0006936B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700232D RID: 9005
			// (set) Token: 0x06004036 RID: 16438 RVA: 0x0006B17E File Offset: 0x0006937E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700232E RID: 9006
			// (set) Token: 0x06004037 RID: 16439 RVA: 0x0006B196 File Offset: 0x00069396
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700232F RID: 9007
			// (set) Token: 0x06004038 RID: 16440 RVA: 0x0006B1AE File Offset: 0x000693AE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002330 RID: 9008
			// (set) Token: 0x06004039 RID: 16441 RVA: 0x0006B1C6 File Offset: 0x000693C6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002331 RID: 9009
			// (set) Token: 0x0600403A RID: 16442 RVA: 0x0006B1DE File Offset: 0x000693DE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002332 RID: 9010
			// (set) Token: 0x0600403B RID: 16443 RVA: 0x0006B1F6 File Offset: 0x000693F6
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000461 RID: 1121
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002333 RID: 9011
			// (set) Token: 0x0600403D RID: 16445 RVA: 0x0006B216 File Offset: 0x00069416
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002334 RID: 9012
			// (set) Token: 0x0600403E RID: 16446 RVA: 0x0006B234 File Offset: 0x00069434
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17002335 RID: 9013
			// (set) Token: 0x0600403F RID: 16447 RVA: 0x0006B252 File Offset: 0x00069452
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002336 RID: 9014
			// (set) Token: 0x06004040 RID: 16448 RVA: 0x0006B265 File Offset: 0x00069465
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002337 RID: 9015
			// (set) Token: 0x06004041 RID: 16449 RVA: 0x0006B27D File Offset: 0x0006947D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002338 RID: 9016
			// (set) Token: 0x06004042 RID: 16450 RVA: 0x0006B295 File Offset: 0x00069495
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002339 RID: 9017
			// (set) Token: 0x06004043 RID: 16451 RVA: 0x0006B2AD File Offset: 0x000694AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700233A RID: 9018
			// (set) Token: 0x06004044 RID: 16452 RVA: 0x0006B2C5 File Offset: 0x000694C5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700233B RID: 9019
			// (set) Token: 0x06004045 RID: 16453 RVA: 0x0006B2DD File Offset: 0x000694DD
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
