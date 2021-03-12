using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200023F RID: 575
	public class GetPublicFolderMailboxDiagnosticsCommand : SyntheticCommandWithPipelineInput<ADRecipient, ADRecipient>
	{
		// Token: 0x06002B4A RID: 11082 RVA: 0x0004FF24 File Offset: 0x0004E124
		private GetPublicFolderMailboxDiagnosticsCommand() : base("Get-PublicFolderMailboxDiagnostics")
		{
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0004FF31 File Offset: 0x0004E131
		public GetPublicFolderMailboxDiagnosticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0004FF40 File Offset: 0x0004E140
		public virtual GetPublicFolderMailboxDiagnosticsCommand SetParameters(GetPublicFolderMailboxDiagnosticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0004FF4A File Offset: 0x0004E14A
		public virtual GetPublicFolderMailboxDiagnosticsCommand SetParameters(GetPublicFolderMailboxDiagnosticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000240 RID: 576
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001285 RID: 4741
			// (set) Token: 0x06002B4E RID: 11086 RVA: 0x0004FF54 File Offset: 0x0004E154
			public virtual SwitchParameter IncludeDumpsterInfo
			{
				set
				{
					base.PowerSharpParameters["IncludeDumpsterInfo"] = value;
				}
			}

			// Token: 0x17001286 RID: 4742
			// (set) Token: 0x06002B4F RID: 11087 RVA: 0x0004FF6C File Offset: 0x0004E16C
			public virtual SwitchParameter IncludeHierarchyInfo
			{
				set
				{
					base.PowerSharpParameters["IncludeHierarchyInfo"] = value;
				}
			}

			// Token: 0x17001287 RID: 4743
			// (set) Token: 0x06002B50 RID: 11088 RVA: 0x0004FF84 File Offset: 0x0004E184
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001288 RID: 4744
			// (set) Token: 0x06002B51 RID: 11089 RVA: 0x0004FF97 File Offset: 0x0004E197
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001289 RID: 4745
			// (set) Token: 0x06002B52 RID: 11090 RVA: 0x0004FFAF File Offset: 0x0004E1AF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700128A RID: 4746
			// (set) Token: 0x06002B53 RID: 11091 RVA: 0x0004FFC7 File Offset: 0x0004E1C7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700128B RID: 4747
			// (set) Token: 0x06002B54 RID: 11092 RVA: 0x0004FFDF File Offset: 0x0004E1DF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700128C RID: 4748
			// (set) Token: 0x06002B55 RID: 11093 RVA: 0x0004FFF7 File Offset: 0x0004E1F7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000241 RID: 577
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700128D RID: 4749
			// (set) Token: 0x06002B57 RID: 11095 RVA: 0x00050017 File Offset: 0x0004E217
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700128E RID: 4750
			// (set) Token: 0x06002B58 RID: 11096 RVA: 0x00050035 File Offset: 0x0004E235
			public virtual SwitchParameter IncludeDumpsterInfo
			{
				set
				{
					base.PowerSharpParameters["IncludeDumpsterInfo"] = value;
				}
			}

			// Token: 0x1700128F RID: 4751
			// (set) Token: 0x06002B59 RID: 11097 RVA: 0x0005004D File Offset: 0x0004E24D
			public virtual SwitchParameter IncludeHierarchyInfo
			{
				set
				{
					base.PowerSharpParameters["IncludeHierarchyInfo"] = value;
				}
			}

			// Token: 0x17001290 RID: 4752
			// (set) Token: 0x06002B5A RID: 11098 RVA: 0x00050065 File Offset: 0x0004E265
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001291 RID: 4753
			// (set) Token: 0x06002B5B RID: 11099 RVA: 0x00050078 File Offset: 0x0004E278
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001292 RID: 4754
			// (set) Token: 0x06002B5C RID: 11100 RVA: 0x00050090 File Offset: 0x0004E290
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001293 RID: 4755
			// (set) Token: 0x06002B5D RID: 11101 RVA: 0x000500A8 File Offset: 0x0004E2A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001294 RID: 4756
			// (set) Token: 0x06002B5E RID: 11102 RVA: 0x000500C0 File Offset: 0x0004E2C0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001295 RID: 4757
			// (set) Token: 0x06002B5F RID: 11103 RVA: 0x000500D8 File Offset: 0x0004E2D8
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
