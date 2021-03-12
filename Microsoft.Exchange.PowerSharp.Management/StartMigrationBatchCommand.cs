using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000285 RID: 645
	public class StartMigrationBatchCommand : SyntheticCommandWithPipelineInput<MigrationBatchIdParameter, MigrationBatchIdParameter>
	{
		// Token: 0x06002ECD RID: 11981 RVA: 0x00054A7C File Offset: 0x00052C7C
		private StartMigrationBatchCommand() : base("Start-MigrationBatch")
		{
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x00054A89 File Offset: 0x00052C89
		public StartMigrationBatchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x00054A98 File Offset: 0x00052C98
		public virtual StartMigrationBatchCommand SetParameters(StartMigrationBatchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x00054AA2 File Offset: 0x00052CA2
		public virtual StartMigrationBatchCommand SetParameters(StartMigrationBatchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000286 RID: 646
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700157C RID: 5500
			// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x00054AAC File Offset: 0x00052CAC
			public virtual SwitchParameter Validate
			{
				set
				{
					base.PowerSharpParameters["Validate"] = value;
				}
			}

			// Token: 0x1700157D RID: 5501
			// (set) Token: 0x06002ED2 RID: 11986 RVA: 0x00054AC4 File Offset: 0x00052CC4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700157E RID: 5502
			// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x00054AE2 File Offset: 0x00052CE2
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700157F RID: 5503
			// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x00054B00 File Offset: 0x00052D00
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001580 RID: 5504
			// (set) Token: 0x06002ED5 RID: 11989 RVA: 0x00054B13 File Offset: 0x00052D13
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001581 RID: 5505
			// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x00054B2B File Offset: 0x00052D2B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001582 RID: 5506
			// (set) Token: 0x06002ED7 RID: 11991 RVA: 0x00054B43 File Offset: 0x00052D43
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001583 RID: 5507
			// (set) Token: 0x06002ED8 RID: 11992 RVA: 0x00054B5B File Offset: 0x00052D5B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001584 RID: 5508
			// (set) Token: 0x06002ED9 RID: 11993 RVA: 0x00054B73 File Offset: 0x00052D73
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000287 RID: 647
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001585 RID: 5509
			// (set) Token: 0x06002EDB RID: 11995 RVA: 0x00054B93 File Offset: 0x00052D93
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationBatchIdParameter(value) : null);
				}
			}

			// Token: 0x17001586 RID: 5510
			// (set) Token: 0x06002EDC RID: 11996 RVA: 0x00054BB1 File Offset: 0x00052DB1
			public virtual SwitchParameter Validate
			{
				set
				{
					base.PowerSharpParameters["Validate"] = value;
				}
			}

			// Token: 0x17001587 RID: 5511
			// (set) Token: 0x06002EDD RID: 11997 RVA: 0x00054BC9 File Offset: 0x00052DC9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001588 RID: 5512
			// (set) Token: 0x06002EDE RID: 11998 RVA: 0x00054BE7 File Offset: 0x00052DE7
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001589 RID: 5513
			// (set) Token: 0x06002EDF RID: 11999 RVA: 0x00054C05 File Offset: 0x00052E05
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700158A RID: 5514
			// (set) Token: 0x06002EE0 RID: 12000 RVA: 0x00054C18 File Offset: 0x00052E18
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700158B RID: 5515
			// (set) Token: 0x06002EE1 RID: 12001 RVA: 0x00054C30 File Offset: 0x00052E30
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700158C RID: 5516
			// (set) Token: 0x06002EE2 RID: 12002 RVA: 0x00054C48 File Offset: 0x00052E48
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700158D RID: 5517
			// (set) Token: 0x06002EE3 RID: 12003 RVA: 0x00054C60 File Offset: 0x00052E60
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700158E RID: 5518
			// (set) Token: 0x06002EE4 RID: 12004 RVA: 0x00054C78 File Offset: 0x00052E78
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
