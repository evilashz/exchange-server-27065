using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000459 RID: 1113
	public class AddMailboxFolderPermissionCommand : SyntheticCommandWithPipelineInput<MailboxFolder, MailboxFolder>
	{
		// Token: 0x06004006 RID: 16390 RVA: 0x0006ADBD File Offset: 0x00068FBD
		private AddMailboxFolderPermissionCommand() : base("Add-MailboxFolderPermission")
		{
		}

		// Token: 0x06004007 RID: 16391 RVA: 0x0006ADCA File Offset: 0x00068FCA
		public AddMailboxFolderPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x0006ADD9 File Offset: 0x00068FD9
		public virtual AddMailboxFolderPermissionCommand SetParameters(AddMailboxFolderPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x0006ADE3 File Offset: 0x00068FE3
		public virtual AddMailboxFolderPermissionCommand SetParameters(AddMailboxFolderPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200045A RID: 1114
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700230D RID: 8973
			// (set) Token: 0x0600400A RID: 16394 RVA: 0x0006ADED File Offset: 0x00068FED
			public virtual MailboxFolderAccessRight AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x1700230E RID: 8974
			// (set) Token: 0x0600400B RID: 16395 RVA: 0x0006AE05 File Offset: 0x00069005
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700230F RID: 8975
			// (set) Token: 0x0600400C RID: 16396 RVA: 0x0006AE23 File Offset: 0x00069023
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002310 RID: 8976
			// (set) Token: 0x0600400D RID: 16397 RVA: 0x0006AE36 File Offset: 0x00069036
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002311 RID: 8977
			// (set) Token: 0x0600400E RID: 16398 RVA: 0x0006AE4E File Offset: 0x0006904E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002312 RID: 8978
			// (set) Token: 0x0600400F RID: 16399 RVA: 0x0006AE66 File Offset: 0x00069066
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002313 RID: 8979
			// (set) Token: 0x06004010 RID: 16400 RVA: 0x0006AE7E File Offset: 0x0006907E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002314 RID: 8980
			// (set) Token: 0x06004011 RID: 16401 RVA: 0x0006AE96 File Offset: 0x00069096
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200045B RID: 1115
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002315 RID: 8981
			// (set) Token: 0x06004013 RID: 16403 RVA: 0x0006AEB6 File Offset: 0x000690B6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002316 RID: 8982
			// (set) Token: 0x06004014 RID: 16404 RVA: 0x0006AED4 File Offset: 0x000690D4
			public virtual MailboxFolderAccessRight AccessRights
			{
				set
				{
					base.PowerSharpParameters["AccessRights"] = value;
				}
			}

			// Token: 0x17002317 RID: 8983
			// (set) Token: 0x06004015 RID: 16405 RVA: 0x0006AEEC File Offset: 0x000690EC
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17002318 RID: 8984
			// (set) Token: 0x06004016 RID: 16406 RVA: 0x0006AF0A File Offset: 0x0006910A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002319 RID: 8985
			// (set) Token: 0x06004017 RID: 16407 RVA: 0x0006AF1D File Offset: 0x0006911D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700231A RID: 8986
			// (set) Token: 0x06004018 RID: 16408 RVA: 0x0006AF35 File Offset: 0x00069135
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700231B RID: 8987
			// (set) Token: 0x06004019 RID: 16409 RVA: 0x0006AF4D File Offset: 0x0006914D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700231C RID: 8988
			// (set) Token: 0x0600401A RID: 16410 RVA: 0x0006AF65 File Offset: 0x00069165
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700231D RID: 8989
			// (set) Token: 0x0600401B RID: 16411 RVA: 0x0006AF7D File Offset: 0x0006917D
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
