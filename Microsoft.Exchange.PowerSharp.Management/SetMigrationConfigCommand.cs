using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000282 RID: 642
	public class SetMigrationConfigCommand : SyntheticCommandWithPipelineInputNoOutput<MigrationConfig>
	{
		// Token: 0x06002EB2 RID: 11954 RVA: 0x0005483C File Offset: 0x00052A3C
		private SetMigrationConfigCommand() : base("Set-MigrationConfig")
		{
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x00054849 File Offset: 0x00052A49
		public SetMigrationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00054858 File Offset: 0x00052A58
		public virtual SetMigrationConfigCommand SetParameters(SetMigrationConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x00054862 File Offset: 0x00052A62
		public virtual SetMigrationConfigCommand SetParameters(SetMigrationConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000283 RID: 643
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001567 RID: 5479
			// (set) Token: 0x06002EB6 RID: 11958 RVA: 0x0005486C File Offset: 0x00052A6C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationConfigIdParameter(value) : null);
				}
			}

			// Token: 0x17001568 RID: 5480
			// (set) Token: 0x06002EB7 RID: 11959 RVA: 0x0005488A File Offset: 0x00052A8A
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001569 RID: 5481
			// (set) Token: 0x06002EB8 RID: 11960 RVA: 0x000548A8 File Offset: 0x00052AA8
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x1700156A RID: 5482
			// (set) Token: 0x06002EB9 RID: 11961 RVA: 0x000548C0 File Offset: 0x00052AC0
			public virtual int MaxNumberOfBatches
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfBatches"] = value;
				}
			}

			// Token: 0x1700156B RID: 5483
			// (set) Token: 0x06002EBA RID: 11962 RVA: 0x000548D8 File Offset: 0x00052AD8
			public virtual MigrationFeature Features
			{
				set
				{
					base.PowerSharpParameters["Features"] = value;
				}
			}

			// Token: 0x1700156C RID: 5484
			// (set) Token: 0x06002EBB RID: 11963 RVA: 0x000548F0 File Offset: 0x00052AF0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700156D RID: 5485
			// (set) Token: 0x06002EBC RID: 11964 RVA: 0x00054903 File Offset: 0x00052B03
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700156E RID: 5486
			// (set) Token: 0x06002EBD RID: 11965 RVA: 0x0005491B File Offset: 0x00052B1B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700156F RID: 5487
			// (set) Token: 0x06002EBE RID: 11966 RVA: 0x00054933 File Offset: 0x00052B33
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001570 RID: 5488
			// (set) Token: 0x06002EBF RID: 11967 RVA: 0x0005494B File Offset: 0x00052B4B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001571 RID: 5489
			// (set) Token: 0x06002EC0 RID: 11968 RVA: 0x00054963 File Offset: 0x00052B63
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000284 RID: 644
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001572 RID: 5490
			// (set) Token: 0x06002EC2 RID: 11970 RVA: 0x00054983 File Offset: 0x00052B83
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001573 RID: 5491
			// (set) Token: 0x06002EC3 RID: 11971 RVA: 0x000549A1 File Offset: 0x00052BA1
			public virtual Unlimited<int> MaxConcurrentMigrations
			{
				set
				{
					base.PowerSharpParameters["MaxConcurrentMigrations"] = value;
				}
			}

			// Token: 0x17001574 RID: 5492
			// (set) Token: 0x06002EC4 RID: 11972 RVA: 0x000549B9 File Offset: 0x00052BB9
			public virtual int MaxNumberOfBatches
			{
				set
				{
					base.PowerSharpParameters["MaxNumberOfBatches"] = value;
				}
			}

			// Token: 0x17001575 RID: 5493
			// (set) Token: 0x06002EC5 RID: 11973 RVA: 0x000549D1 File Offset: 0x00052BD1
			public virtual MigrationFeature Features
			{
				set
				{
					base.PowerSharpParameters["Features"] = value;
				}
			}

			// Token: 0x17001576 RID: 5494
			// (set) Token: 0x06002EC6 RID: 11974 RVA: 0x000549E9 File Offset: 0x00052BE9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001577 RID: 5495
			// (set) Token: 0x06002EC7 RID: 11975 RVA: 0x000549FC File Offset: 0x00052BFC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001578 RID: 5496
			// (set) Token: 0x06002EC8 RID: 11976 RVA: 0x00054A14 File Offset: 0x00052C14
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001579 RID: 5497
			// (set) Token: 0x06002EC9 RID: 11977 RVA: 0x00054A2C File Offset: 0x00052C2C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700157A RID: 5498
			// (set) Token: 0x06002ECA RID: 11978 RVA: 0x00054A44 File Offset: 0x00052C44
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700157B RID: 5499
			// (set) Token: 0x06002ECB RID: 11979 RVA: 0x00054A5C File Offset: 0x00052C5C
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
