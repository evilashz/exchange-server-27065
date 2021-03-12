using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AD7 RID: 2775
	public class GetPublicFolderClientPermissionCommand : SyntheticCommandWithPipelineInputNoOutput<PublicFolderIdParameter>
	{
		// Token: 0x06008939 RID: 35129 RVA: 0x000C9EF4 File Offset: 0x000C80F4
		private GetPublicFolderClientPermissionCommand() : base("Get-PublicFolderClientPermission")
		{
		}

		// Token: 0x0600893A RID: 35130 RVA: 0x000C9F01 File Offset: 0x000C8101
		public GetPublicFolderClientPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600893B RID: 35131 RVA: 0x000C9F10 File Offset: 0x000C8110
		public virtual GetPublicFolderClientPermissionCommand SetParameters(GetPublicFolderClientPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600893C RID: 35132 RVA: 0x000C9F1A File Offset: 0x000C811A
		public virtual GetPublicFolderClientPermissionCommand SetParameters(GetPublicFolderClientPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AD8 RID: 2776
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005F44 RID: 24388
			// (set) Token: 0x0600893D RID: 35133 RVA: 0x000C9F24 File Offset: 0x000C8124
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17005F45 RID: 24389
			// (set) Token: 0x0600893E RID: 35134 RVA: 0x000C9F42 File Offset: 0x000C8142
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005F46 RID: 24390
			// (set) Token: 0x0600893F RID: 35135 RVA: 0x000C9F60 File Offset: 0x000C8160
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F47 RID: 24391
			// (set) Token: 0x06008940 RID: 35136 RVA: 0x000C9F7E File Offset: 0x000C817E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F48 RID: 24392
			// (set) Token: 0x06008941 RID: 35137 RVA: 0x000C9F91 File Offset: 0x000C8191
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F49 RID: 24393
			// (set) Token: 0x06008942 RID: 35138 RVA: 0x000C9FA9 File Offset: 0x000C81A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F4A RID: 24394
			// (set) Token: 0x06008943 RID: 35139 RVA: 0x000C9FC1 File Offset: 0x000C81C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F4B RID: 24395
			// (set) Token: 0x06008944 RID: 35140 RVA: 0x000C9FD9 File Offset: 0x000C81D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AD9 RID: 2777
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005F4C RID: 24396
			// (set) Token: 0x06008946 RID: 35142 RVA: 0x000C9FF9 File Offset: 0x000C81F9
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17005F4D RID: 24397
			// (set) Token: 0x06008947 RID: 35143 RVA: 0x000CA017 File Offset: 0x000C8217
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F4E RID: 24398
			// (set) Token: 0x06008948 RID: 35144 RVA: 0x000CA035 File Offset: 0x000C8235
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F4F RID: 24399
			// (set) Token: 0x06008949 RID: 35145 RVA: 0x000CA048 File Offset: 0x000C8248
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F50 RID: 24400
			// (set) Token: 0x0600894A RID: 35146 RVA: 0x000CA060 File Offset: 0x000C8260
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F51 RID: 24401
			// (set) Token: 0x0600894B RID: 35147 RVA: 0x000CA078 File Offset: 0x000C8278
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F52 RID: 24402
			// (set) Token: 0x0600894C RID: 35148 RVA: 0x000CA090 File Offset: 0x000C8290
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
