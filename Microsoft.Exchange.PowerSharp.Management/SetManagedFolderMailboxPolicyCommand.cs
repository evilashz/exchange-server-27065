using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001AD RID: 429
	public class SetManagedFolderMailboxPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<ManagedFolderMailboxPolicy>
	{
		// Token: 0x060024D0 RID: 9424 RVA: 0x000475A6 File Offset: 0x000457A6
		private SetManagedFolderMailboxPolicyCommand() : base("Set-ManagedFolderMailboxPolicy")
		{
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000475B3 File Offset: 0x000457B3
		public SetManagedFolderMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000475C2 File Offset: 0x000457C2
		public virtual SetManagedFolderMailboxPolicyCommand SetParameters(SetManagedFolderMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000475CC File Offset: 0x000457CC
		public virtual SetManagedFolderMailboxPolicyCommand SetParameters(SetManagedFolderMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001AE RID: 430
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000D2F RID: 3375
			// (set) Token: 0x060024D4 RID: 9428 RVA: 0x000475D6 File Offset: 0x000457D6
			public virtual ELCFolderIdParameter ManagedFolderLinks
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderLinks"] = value;
				}
			}

			// Token: 0x17000D30 RID: 3376
			// (set) Token: 0x060024D5 RID: 9429 RVA: 0x000475E9 File Offset: 0x000457E9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D31 RID: 3377
			// (set) Token: 0x060024D6 RID: 9430 RVA: 0x000475FC File Offset: 0x000457FC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000D32 RID: 3378
			// (set) Token: 0x060024D7 RID: 9431 RVA: 0x0004760F File Offset: 0x0004580F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D33 RID: 3379
			// (set) Token: 0x060024D8 RID: 9432 RVA: 0x00047627 File Offset: 0x00045827
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D34 RID: 3380
			// (set) Token: 0x060024D9 RID: 9433 RVA: 0x0004763F File Offset: 0x0004583F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D35 RID: 3381
			// (set) Token: 0x060024DA RID: 9434 RVA: 0x00047657 File Offset: 0x00045857
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D36 RID: 3382
			// (set) Token: 0x060024DB RID: 9435 RVA: 0x0004766F File Offset: 0x0004586F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001AF RID: 431
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000D37 RID: 3383
			// (set) Token: 0x060024DD RID: 9437 RVA: 0x0004768F File Offset: 0x0004588F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000D38 RID: 3384
			// (set) Token: 0x060024DE RID: 9438 RVA: 0x000476AD File Offset: 0x000458AD
			public virtual ELCFolderIdParameter ManagedFolderLinks
			{
				set
				{
					base.PowerSharpParameters["ManagedFolderLinks"] = value;
				}
			}

			// Token: 0x17000D39 RID: 3385
			// (set) Token: 0x060024DF RID: 9439 RVA: 0x000476C0 File Offset: 0x000458C0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D3A RID: 3386
			// (set) Token: 0x060024E0 RID: 9440 RVA: 0x000476D3 File Offset: 0x000458D3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000D3B RID: 3387
			// (set) Token: 0x060024E1 RID: 9441 RVA: 0x000476E6 File Offset: 0x000458E6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D3C RID: 3388
			// (set) Token: 0x060024E2 RID: 9442 RVA: 0x000476FE File Offset: 0x000458FE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D3D RID: 3389
			// (set) Token: 0x060024E3 RID: 9443 RVA: 0x00047716 File Offset: 0x00045916
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D3E RID: 3390
			// (set) Token: 0x060024E4 RID: 9444 RVA: 0x0004772E File Offset: 0x0004592E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D3F RID: 3391
			// (set) Token: 0x060024E5 RID: 9445 RVA: 0x00047746 File Offset: 0x00045946
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
