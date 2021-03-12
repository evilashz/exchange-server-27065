using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000257 RID: 599
	public class CompleteMigrationBatchCommand : SyntheticCommandWithPipelineInput<MigrationBatchIdParameter, MigrationBatchIdParameter>
	{
		// Token: 0x06002C8B RID: 11403 RVA: 0x00051930 File Offset: 0x0004FB30
		private CompleteMigrationBatchCommand() : base("Complete-MigrationBatch")
		{
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x0005193D File Offset: 0x0004FB3D
		public CompleteMigrationBatchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x0005194C File Offset: 0x0004FB4C
		public virtual CompleteMigrationBatchCommand SetParameters(CompleteMigrationBatchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x00051956 File Offset: 0x0004FB56
		public virtual CompleteMigrationBatchCommand SetParameters(CompleteMigrationBatchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000258 RID: 600
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001396 RID: 5014
			// (set) Token: 0x06002C8F RID: 11407 RVA: 0x00051960 File Offset: 0x0004FB60
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x17001397 RID: 5015
			// (set) Token: 0x06002C90 RID: 11408 RVA: 0x00051973 File Offset: 0x0004FB73
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001398 RID: 5016
			// (set) Token: 0x06002C91 RID: 11409 RVA: 0x00051991 File Offset: 0x0004FB91
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001399 RID: 5017
			// (set) Token: 0x06002C92 RID: 11410 RVA: 0x000519AF File Offset: 0x0004FBAF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700139A RID: 5018
			// (set) Token: 0x06002C93 RID: 11411 RVA: 0x000519C2 File Offset: 0x0004FBC2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700139B RID: 5019
			// (set) Token: 0x06002C94 RID: 11412 RVA: 0x000519DA File Offset: 0x0004FBDA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700139C RID: 5020
			// (set) Token: 0x06002C95 RID: 11413 RVA: 0x000519F2 File Offset: 0x0004FBF2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700139D RID: 5021
			// (set) Token: 0x06002C96 RID: 11414 RVA: 0x00051A0A File Offset: 0x0004FC0A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700139E RID: 5022
			// (set) Token: 0x06002C97 RID: 11415 RVA: 0x00051A22 File Offset: 0x0004FC22
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700139F RID: 5023
			// (set) Token: 0x06002C98 RID: 11416 RVA: 0x00051A3A File Offset: 0x0004FC3A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000259 RID: 601
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170013A0 RID: 5024
			// (set) Token: 0x06002C9A RID: 11418 RVA: 0x00051A5A File Offset: 0x0004FC5A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationBatchIdParameter(value) : null);
				}
			}

			// Token: 0x170013A1 RID: 5025
			// (set) Token: 0x06002C9B RID: 11419 RVA: 0x00051A78 File Offset: 0x0004FC78
			public virtual MultiValuedProperty<SmtpAddress> NotificationEmails
			{
				set
				{
					base.PowerSharpParameters["NotificationEmails"] = value;
				}
			}

			// Token: 0x170013A2 RID: 5026
			// (set) Token: 0x06002C9C RID: 11420 RVA: 0x00051A8B File Offset: 0x0004FC8B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170013A3 RID: 5027
			// (set) Token: 0x06002C9D RID: 11421 RVA: 0x00051AA9 File Offset: 0x0004FCA9
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013A4 RID: 5028
			// (set) Token: 0x06002C9E RID: 11422 RVA: 0x00051AC7 File Offset: 0x0004FCC7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013A5 RID: 5029
			// (set) Token: 0x06002C9F RID: 11423 RVA: 0x00051ADA File Offset: 0x0004FCDA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013A6 RID: 5030
			// (set) Token: 0x06002CA0 RID: 11424 RVA: 0x00051AF2 File Offset: 0x0004FCF2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013A7 RID: 5031
			// (set) Token: 0x06002CA1 RID: 11425 RVA: 0x00051B0A File Offset: 0x0004FD0A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013A8 RID: 5032
			// (set) Token: 0x06002CA2 RID: 11426 RVA: 0x00051B22 File Offset: 0x0004FD22
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170013A9 RID: 5033
			// (set) Token: 0x06002CA3 RID: 11427 RVA: 0x00051B3A File Offset: 0x0004FD3A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170013AA RID: 5034
			// (set) Token: 0x06002CA4 RID: 11428 RVA: 0x00051B52 File Offset: 0x0004FD52
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
