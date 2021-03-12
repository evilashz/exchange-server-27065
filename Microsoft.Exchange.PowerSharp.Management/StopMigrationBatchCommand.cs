using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000288 RID: 648
	public class StopMigrationBatchCommand : SyntheticCommandWithPipelineInput<MigrationBatchIdParameter, MigrationBatchIdParameter>
	{
		// Token: 0x06002EE6 RID: 12006 RVA: 0x00054C98 File Offset: 0x00052E98
		private StopMigrationBatchCommand() : base("Stop-MigrationBatch")
		{
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x00054CA5 File Offset: 0x00052EA5
		public StopMigrationBatchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x00054CB4 File Offset: 0x00052EB4
		public virtual StopMigrationBatchCommand SetParameters(StopMigrationBatchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x00054CBE File Offset: 0x00052EBE
		public virtual StopMigrationBatchCommand SetParameters(StopMigrationBatchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000289 RID: 649
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700158F RID: 5519
			// (set) Token: 0x06002EEA RID: 12010 RVA: 0x00054CC8 File Offset: 0x00052EC8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationBatchIdParameter(value) : null);
				}
			}

			// Token: 0x17001590 RID: 5520
			// (set) Token: 0x06002EEB RID: 12011 RVA: 0x00054CE6 File Offset: 0x00052EE6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001591 RID: 5521
			// (set) Token: 0x06002EEC RID: 12012 RVA: 0x00054D04 File Offset: 0x00052F04
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001592 RID: 5522
			// (set) Token: 0x06002EED RID: 12013 RVA: 0x00054D22 File Offset: 0x00052F22
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001593 RID: 5523
			// (set) Token: 0x06002EEE RID: 12014 RVA: 0x00054D35 File Offset: 0x00052F35
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001594 RID: 5524
			// (set) Token: 0x06002EEF RID: 12015 RVA: 0x00054D4D File Offset: 0x00052F4D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001595 RID: 5525
			// (set) Token: 0x06002EF0 RID: 12016 RVA: 0x00054D65 File Offset: 0x00052F65
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001596 RID: 5526
			// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x00054D7D File Offset: 0x00052F7D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001597 RID: 5527
			// (set) Token: 0x06002EF2 RID: 12018 RVA: 0x00054D95 File Offset: 0x00052F95
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001598 RID: 5528
			// (set) Token: 0x06002EF3 RID: 12019 RVA: 0x00054DAD File Offset: 0x00052FAD
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200028A RID: 650
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001599 RID: 5529
			// (set) Token: 0x06002EF5 RID: 12021 RVA: 0x00054DCD File Offset: 0x00052FCD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700159A RID: 5530
			// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x00054DEB File Offset: 0x00052FEB
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700159B RID: 5531
			// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x00054E09 File Offset: 0x00053009
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700159C RID: 5532
			// (set) Token: 0x06002EF8 RID: 12024 RVA: 0x00054E1C File Offset: 0x0005301C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700159D RID: 5533
			// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x00054E34 File Offset: 0x00053034
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700159E RID: 5534
			// (set) Token: 0x06002EFA RID: 12026 RVA: 0x00054E4C File Offset: 0x0005304C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700159F RID: 5535
			// (set) Token: 0x06002EFB RID: 12027 RVA: 0x00054E64 File Offset: 0x00053064
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170015A0 RID: 5536
			// (set) Token: 0x06002EFC RID: 12028 RVA: 0x00054E7C File Offset: 0x0005307C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170015A1 RID: 5537
			// (set) Token: 0x06002EFD RID: 12029 RVA: 0x00054E94 File Offset: 0x00053094
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
