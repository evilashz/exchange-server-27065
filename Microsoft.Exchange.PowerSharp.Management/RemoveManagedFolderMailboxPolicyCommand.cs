using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001A7 RID: 423
	public class RemoveManagedFolderMailboxPolicyCommand : SyntheticCommandWithPipelineInput<ManagedFolderMailboxPolicy, ManagedFolderMailboxPolicy>
	{
		// Token: 0x060024A6 RID: 9382 RVA: 0x00047252 File Offset: 0x00045452
		private RemoveManagedFolderMailboxPolicyCommand() : base("Remove-ManagedFolderMailboxPolicy")
		{
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0004725F File Offset: 0x0004545F
		public RemoveManagedFolderMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0004726E File Offset: 0x0004546E
		public virtual RemoveManagedFolderMailboxPolicyCommand SetParameters(RemoveManagedFolderMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x00047278 File Offset: 0x00045478
		public virtual RemoveManagedFolderMailboxPolicyCommand SetParameters(RemoveManagedFolderMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001A8 RID: 424
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000D11 RID: 3345
			// (set) Token: 0x060024AA RID: 9386 RVA: 0x00047282 File Offset: 0x00045482
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000D12 RID: 3346
			// (set) Token: 0x060024AB RID: 9387 RVA: 0x0004729A File Offset: 0x0004549A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D13 RID: 3347
			// (set) Token: 0x060024AC RID: 9388 RVA: 0x000472AD File Offset: 0x000454AD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D14 RID: 3348
			// (set) Token: 0x060024AD RID: 9389 RVA: 0x000472C5 File Offset: 0x000454C5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D15 RID: 3349
			// (set) Token: 0x060024AE RID: 9390 RVA: 0x000472DD File Offset: 0x000454DD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D16 RID: 3350
			// (set) Token: 0x060024AF RID: 9391 RVA: 0x000472F5 File Offset: 0x000454F5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D17 RID: 3351
			// (set) Token: 0x060024B0 RID: 9392 RVA: 0x0004730D File Offset: 0x0004550D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000D18 RID: 3352
			// (set) Token: 0x060024B1 RID: 9393 RVA: 0x00047325 File Offset: 0x00045525
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020001A9 RID: 425
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000D19 RID: 3353
			// (set) Token: 0x060024B3 RID: 9395 RVA: 0x00047345 File Offset: 0x00045545
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000D1A RID: 3354
			// (set) Token: 0x060024B4 RID: 9396 RVA: 0x00047363 File Offset: 0x00045563
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17000D1B RID: 3355
			// (set) Token: 0x060024B5 RID: 9397 RVA: 0x0004737B File Offset: 0x0004557B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D1C RID: 3356
			// (set) Token: 0x060024B6 RID: 9398 RVA: 0x0004738E File Offset: 0x0004558E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D1D RID: 3357
			// (set) Token: 0x060024B7 RID: 9399 RVA: 0x000473A6 File Offset: 0x000455A6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D1E RID: 3358
			// (set) Token: 0x060024B8 RID: 9400 RVA: 0x000473BE File Offset: 0x000455BE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D1F RID: 3359
			// (set) Token: 0x060024B9 RID: 9401 RVA: 0x000473D6 File Offset: 0x000455D6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D20 RID: 3360
			// (set) Token: 0x060024BA RID: 9402 RVA: 0x000473EE File Offset: 0x000455EE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17000D21 RID: 3361
			// (set) Token: 0x060024BB RID: 9403 RVA: 0x00047406 File Offset: 0x00045606
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
