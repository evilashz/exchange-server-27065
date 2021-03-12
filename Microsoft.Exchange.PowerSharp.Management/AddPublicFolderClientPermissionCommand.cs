using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AD4 RID: 2772
	public class AddPublicFolderClientPermissionCommand : SyntheticCommandWithPipelineInput<MailboxFolder, MailboxFolder>
	{
		// Token: 0x06008922 RID: 35106 RVA: 0x000C9D14 File Offset: 0x000C7F14
		private AddPublicFolderClientPermissionCommand() : base("Add-PublicFolderClientPermission")
		{
		}

		// Token: 0x06008923 RID: 35107 RVA: 0x000C9D21 File Offset: 0x000C7F21
		public AddPublicFolderClientPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008924 RID: 35108 RVA: 0x000C9D30 File Offset: 0x000C7F30
		public virtual AddPublicFolderClientPermissionCommand SetParameters(AddPublicFolderClientPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008925 RID: 35109 RVA: 0x000C9D3A File Offset: 0x000C7F3A
		public virtual AddPublicFolderClientPermissionCommand SetParameters(AddPublicFolderClientPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AD5 RID: 2773
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005F33 RID: 24371
			// (set) Token: 0x06008926 RID: 35110 RVA: 0x000C9D44 File Offset: 0x000C7F44
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17005F34 RID: 24372
			// (set) Token: 0x06008927 RID: 35111 RVA: 0x000C9D62 File Offset: 0x000C7F62
			public virtual MailboxFolderAccessRight AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17005F35 RID: 24373
			// (set) Token: 0x06008928 RID: 35112 RVA: 0x000C9D7A File Offset: 0x000C7F7A
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F36 RID: 24374
			// (set) Token: 0x06008929 RID: 35113 RVA: 0x000C9D98 File Offset: 0x000C7F98
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F37 RID: 24375
			// (set) Token: 0x0600892A RID: 35114 RVA: 0x000C9DAB File Offset: 0x000C7FAB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F38 RID: 24376
			// (set) Token: 0x0600892B RID: 35115 RVA: 0x000C9DC3 File Offset: 0x000C7FC3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F39 RID: 24377
			// (set) Token: 0x0600892C RID: 35116 RVA: 0x000C9DDB File Offset: 0x000C7FDB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F3A RID: 24378
			// (set) Token: 0x0600892D RID: 35117 RVA: 0x000C9DF3 File Offset: 0x000C7FF3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F3B RID: 24379
			// (set) Token: 0x0600892E RID: 35118 RVA: 0x000C9E0B File Offset: 0x000C800B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000AD6 RID: 2774
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005F3C RID: 24380
			// (set) Token: 0x06008930 RID: 35120 RVA: 0x000C9E2B File Offset: 0x000C802B
			public virtual MailboxFolderAccessRight AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17005F3D RID: 24381
			// (set) Token: 0x06008931 RID: 35121 RVA: 0x000C9E43 File Offset: 0x000C8043
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F3E RID: 24382
			// (set) Token: 0x06008932 RID: 35122 RVA: 0x000C9E61 File Offset: 0x000C8061
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F3F RID: 24383
			// (set) Token: 0x06008933 RID: 35123 RVA: 0x000C9E74 File Offset: 0x000C8074
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F40 RID: 24384
			// (set) Token: 0x06008934 RID: 35124 RVA: 0x000C9E8C File Offset: 0x000C808C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F41 RID: 24385
			// (set) Token: 0x06008935 RID: 35125 RVA: 0x000C9EA4 File Offset: 0x000C80A4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F42 RID: 24386
			// (set) Token: 0x06008936 RID: 35126 RVA: 0x000C9EBC File Offset: 0x000C80BC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F43 RID: 24387
			// (set) Token: 0x06008937 RID: 35127 RVA: 0x000C9ED4 File Offset: 0x000C80D4
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
