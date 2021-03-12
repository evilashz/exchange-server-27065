using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000462 RID: 1122
	public class SetMailboxFolderPermissionCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxFolder>
	{
		// Token: 0x06004047 RID: 16455 RVA: 0x0006B2FD File Offset: 0x000694FD
		private SetMailboxFolderPermissionCommand() : base("Set-MailboxFolderPermission")
		{
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x0006B30A File Offset: 0x0006950A
		public SetMailboxFolderPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004049 RID: 16457 RVA: 0x0006B319 File Offset: 0x00069519
		public virtual SetMailboxFolderPermissionCommand SetParameters(SetMailboxFolderPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600404A RID: 16458 RVA: 0x0006B323 File Offset: 0x00069523
		public virtual SetMailboxFolderPermissionCommand SetParameters(SetMailboxFolderPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000463 RID: 1123
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700233C RID: 9020
			// (set) Token: 0x0600404B RID: 16459 RVA: 0x0006B32D File Offset: 0x0006952D
			public virtual MailboxFolderAccessRight AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x1700233D RID: 9021
			// (set) Token: 0x0600404C RID: 16460 RVA: 0x0006B345 File Offset: 0x00069545
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700233E RID: 9022
			// (set) Token: 0x0600404D RID: 16461 RVA: 0x0006B363 File Offset: 0x00069563
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700233F RID: 9023
			// (set) Token: 0x0600404E RID: 16462 RVA: 0x0006B376 File Offset: 0x00069576
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002340 RID: 9024
			// (set) Token: 0x0600404F RID: 16463 RVA: 0x0006B38E File Offset: 0x0006958E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002341 RID: 9025
			// (set) Token: 0x06004050 RID: 16464 RVA: 0x0006B3A6 File Offset: 0x000695A6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002342 RID: 9026
			// (set) Token: 0x06004051 RID: 16465 RVA: 0x0006B3BE File Offset: 0x000695BE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002343 RID: 9027
			// (set) Token: 0x06004052 RID: 16466 RVA: 0x0006B3D6 File Offset: 0x000695D6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000464 RID: 1124
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002344 RID: 9028
			// (set) Token: 0x06004054 RID: 16468 RVA: 0x0006B3F6 File Offset: 0x000695F6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002345 RID: 9029
			// (set) Token: 0x06004055 RID: 16469 RVA: 0x0006B414 File Offset: 0x00069614
			public virtual MailboxFolderAccessRight AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17002346 RID: 9030
			// (set) Token: 0x06004056 RID: 16470 RVA: 0x0006B42C File Offset: 0x0006962C
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17002347 RID: 9031
			// (set) Token: 0x06004057 RID: 16471 RVA: 0x0006B44A File Offset: 0x0006964A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002348 RID: 9032
			// (set) Token: 0x06004058 RID: 16472 RVA: 0x0006B45D File Offset: 0x0006965D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002349 RID: 9033
			// (set) Token: 0x06004059 RID: 16473 RVA: 0x0006B475 File Offset: 0x00069675
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700234A RID: 9034
			// (set) Token: 0x0600405A RID: 16474 RVA: 0x0006B48D File Offset: 0x0006968D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700234B RID: 9035
			// (set) Token: 0x0600405B RID: 16475 RVA: 0x0006B4A5 File Offset: 0x000696A5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700234C RID: 9036
			// (set) Token: 0x0600405C RID: 16476 RVA: 0x0006B4BD File Offset: 0x000696BD
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
