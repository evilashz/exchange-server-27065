using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200045C RID: 1116
	public class GetMailboxFolderPermissionCommand : SyntheticCommandWithPipelineInput<MailboxFolder, MailboxFolder>
	{
		// Token: 0x0600401D RID: 16413 RVA: 0x0006AF9D File Offset: 0x0006919D
		private GetMailboxFolderPermissionCommand() : base("Get-MailboxFolderPermission")
		{
		}

		// Token: 0x0600401E RID: 16414 RVA: 0x0006AFAA File Offset: 0x000691AA
		public GetMailboxFolderPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600401F RID: 16415 RVA: 0x0006AFB9 File Offset: 0x000691B9
		public virtual GetMailboxFolderPermissionCommand SetParameters(GetMailboxFolderPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x0006AFC3 File Offset: 0x000691C3
		public virtual GetMailboxFolderPermissionCommand SetParameters(GetMailboxFolderPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200045D RID: 1117
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700231E RID: 8990
			// (set) Token: 0x06004021 RID: 16417 RVA: 0x0006AFCD File Offset: 0x000691CD
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700231F RID: 8991
			// (set) Token: 0x06004022 RID: 16418 RVA: 0x0006AFEB File Offset: 0x000691EB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002320 RID: 8992
			// (set) Token: 0x06004023 RID: 16419 RVA: 0x0006AFFE File Offset: 0x000691FE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002321 RID: 8993
			// (set) Token: 0x06004024 RID: 16420 RVA: 0x0006B016 File Offset: 0x00069216
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002322 RID: 8994
			// (set) Token: 0x06004025 RID: 16421 RVA: 0x0006B02E File Offset: 0x0006922E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002323 RID: 8995
			// (set) Token: 0x06004026 RID: 16422 RVA: 0x0006B046 File Offset: 0x00069246
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200045E RID: 1118
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002324 RID: 8996
			// (set) Token: 0x06004028 RID: 16424 RVA: 0x0006B066 File Offset: 0x00069266
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002325 RID: 8997
			// (set) Token: 0x06004029 RID: 16425 RVA: 0x0006B084 File Offset: 0x00069284
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17002326 RID: 8998
			// (set) Token: 0x0600402A RID: 16426 RVA: 0x0006B0A2 File Offset: 0x000692A2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002327 RID: 8999
			// (set) Token: 0x0600402B RID: 16427 RVA: 0x0006B0B5 File Offset: 0x000692B5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002328 RID: 9000
			// (set) Token: 0x0600402C RID: 16428 RVA: 0x0006B0CD File Offset: 0x000692CD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002329 RID: 9001
			// (set) Token: 0x0600402D RID: 16429 RVA: 0x0006B0E5 File Offset: 0x000692E5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700232A RID: 9002
			// (set) Token: 0x0600402E RID: 16430 RVA: 0x0006B0FD File Offset: 0x000692FD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
