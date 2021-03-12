using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001A5 RID: 421
	public class NewManagedFolderMailboxPolicyCommand : SyntheticCommandWithPipelineInput<ManagedFolderMailboxPolicy, ManagedFolderMailboxPolicy>
	{
		// Token: 0x06002499 RID: 9369 RVA: 0x00047155 File Offset: 0x00045355
		private NewManagedFolderMailboxPolicyCommand() : base("New-ManagedFolderMailboxPolicy")
		{
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00047162 File Offset: 0x00045362
		public NewManagedFolderMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00047171 File Offset: 0x00045371
		public virtual NewManagedFolderMailboxPolicyCommand SetParameters(NewManagedFolderMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001A6 RID: 422
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000D08 RID: 3336
			// (set) Token: 0x0600249C RID: 9372 RVA: 0x0004717B File Offset: 0x0004537B
			public virtual ELCFolderIdParameter ManagedFolderLinks
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderLinks"] = value;
				}
			}

			// Token: 0x17000D09 RID: 3337
			// (set) Token: 0x0600249D RID: 9373 RVA: 0x0004718E File Offset: 0x0004538E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000D0A RID: 3338
			// (set) Token: 0x0600249E RID: 9374 RVA: 0x000471AC File Offset: 0x000453AC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000D0B RID: 3339
			// (set) Token: 0x0600249F RID: 9375 RVA: 0x000471BF File Offset: 0x000453BF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D0C RID: 3340
			// (set) Token: 0x060024A0 RID: 9376 RVA: 0x000471D2 File Offset: 0x000453D2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D0D RID: 3341
			// (set) Token: 0x060024A1 RID: 9377 RVA: 0x000471EA File Offset: 0x000453EA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D0E RID: 3342
			// (set) Token: 0x060024A2 RID: 9378 RVA: 0x00047202 File Offset: 0x00045402
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D0F RID: 3343
			// (set) Token: 0x060024A3 RID: 9379 RVA: 0x0004721A File Offset: 0x0004541A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D10 RID: 3344
			// (set) Token: 0x060024A4 RID: 9380 RVA: 0x00047232 File Offset: 0x00045432
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
