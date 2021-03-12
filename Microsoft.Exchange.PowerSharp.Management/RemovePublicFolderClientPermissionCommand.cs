using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.StoreTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000ADA RID: 2778
	public class RemovePublicFolderClientPermissionCommand : SyntheticCommandWithPipelineInput<MailboxFolder, MailboxFolder>
	{
		// Token: 0x0600894E RID: 35150 RVA: 0x000CA0B0 File Offset: 0x000C82B0
		private RemovePublicFolderClientPermissionCommand() : base("Remove-PublicFolderClientPermission")
		{
		}

		// Token: 0x0600894F RID: 35151 RVA: 0x000CA0BD File Offset: 0x000C82BD
		public RemovePublicFolderClientPermissionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008950 RID: 35152 RVA: 0x000CA0CC File Offset: 0x000C82CC
		public virtual RemovePublicFolderClientPermissionCommand SetParameters(RemovePublicFolderClientPermissionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008951 RID: 35153 RVA: 0x000CA0D6 File Offset: 0x000C82D6
		public virtual RemovePublicFolderClientPermissionCommand SetParameters(RemovePublicFolderClientPermissionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000ADB RID: 2779
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005F53 RID: 24403
			// (set) Token: 0x06008952 RID: 35154 RVA: 0x000CA0E0 File Offset: 0x000C82E0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17005F54 RID: 24404
			// (set) Token: 0x06008953 RID: 35155 RVA: 0x000CA0FE File Offset: 0x000C82FE
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F55 RID: 24405
			// (set) Token: 0x06008954 RID: 35156 RVA: 0x000CA11C File Offset: 0x000C831C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F56 RID: 24406
			// (set) Token: 0x06008955 RID: 35157 RVA: 0x000CA12F File Offset: 0x000C832F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F57 RID: 24407
			// (set) Token: 0x06008956 RID: 35158 RVA: 0x000CA147 File Offset: 0x000C8347
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F58 RID: 24408
			// (set) Token: 0x06008957 RID: 35159 RVA: 0x000CA15F File Offset: 0x000C835F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F59 RID: 24409
			// (set) Token: 0x06008958 RID: 35160 RVA: 0x000CA177 File Offset: 0x000C8377
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F5A RID: 24410
			// (set) Token: 0x06008959 RID: 35161 RVA: 0x000CA18F File Offset: 0x000C838F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005F5B RID: 24411
			// (set) Token: 0x0600895A RID: 35162 RVA: 0x000CA1A7 File Offset: 0x000C83A7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000ADC RID: 2780
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005F5C RID: 24412
			// (set) Token: 0x0600895C RID: 35164 RVA: 0x000CA1C7 File Offset: 0x000C83C7
			public virtual string User
			{
				set
				{
					base.PowerSharpParameters["User"] = ((value != null) ? new MailboxFolderUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005F5D RID: 24413
			// (set) Token: 0x0600895D RID: 35165 RVA: 0x000CA1E5 File Offset: 0x000C83E5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005F5E RID: 24414
			// (set) Token: 0x0600895E RID: 35166 RVA: 0x000CA1F8 File Offset: 0x000C83F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005F5F RID: 24415
			// (set) Token: 0x0600895F RID: 35167 RVA: 0x000CA210 File Offset: 0x000C8410
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005F60 RID: 24416
			// (set) Token: 0x06008960 RID: 35168 RVA: 0x000CA228 File Offset: 0x000C8428
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005F61 RID: 24417
			// (set) Token: 0x06008961 RID: 35169 RVA: 0x000CA240 File Offset: 0x000C8440
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005F62 RID: 24418
			// (set) Token: 0x06008962 RID: 35170 RVA: 0x000CA258 File Offset: 0x000C8458
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005F63 RID: 24419
			// (set) Token: 0x06008963 RID: 35171 RVA: 0x000CA270 File Offset: 0x000C8470
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
