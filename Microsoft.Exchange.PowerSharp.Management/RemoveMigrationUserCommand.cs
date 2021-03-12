using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Migration;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200027C RID: 636
	public class RemoveMigrationUserCommand : SyntheticCommandWithPipelineInput<MigrationUser, MigrationUser>
	{
		// Token: 0x06002E68 RID: 11880 RVA: 0x000541D8 File Offset: 0x000523D8
		private RemoveMigrationUserCommand() : base("Remove-MigrationUser")
		{
		}

		// Token: 0x06002E69 RID: 11881 RVA: 0x000541E5 File Offset: 0x000523E5
		public RemoveMigrationUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002E6A RID: 11882 RVA: 0x000541F4 File Offset: 0x000523F4
		public virtual RemoveMigrationUserCommand SetParameters(RemoveMigrationUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x000541FE File Offset: 0x000523FE
		public virtual RemoveMigrationUserCommand SetParameters(RemoveMigrationUserCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200027D RID: 637
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001529 RID: 5417
			// (set) Token: 0x06002E6C RID: 11884 RVA: 0x00054208 File Offset: 0x00052408
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700152A RID: 5418
			// (set) Token: 0x06002E6D RID: 11885 RVA: 0x00054220 File Offset: 0x00052420
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700152B RID: 5419
			// (set) Token: 0x06002E6E RID: 11886 RVA: 0x0005423E File Offset: 0x0005243E
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700152C RID: 5420
			// (set) Token: 0x06002E6F RID: 11887 RVA: 0x0005425C File Offset: 0x0005245C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700152D RID: 5421
			// (set) Token: 0x06002E70 RID: 11888 RVA: 0x0005426F File Offset: 0x0005246F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700152E RID: 5422
			// (set) Token: 0x06002E71 RID: 11889 RVA: 0x00054287 File Offset: 0x00052487
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700152F RID: 5423
			// (set) Token: 0x06002E72 RID: 11890 RVA: 0x0005429F File Offset: 0x0005249F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001530 RID: 5424
			// (set) Token: 0x06002E73 RID: 11891 RVA: 0x000542B7 File Offset: 0x000524B7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001531 RID: 5425
			// (set) Token: 0x06002E74 RID: 11892 RVA: 0x000542CF File Offset: 0x000524CF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001532 RID: 5426
			// (set) Token: 0x06002E75 RID: 11893 RVA: 0x000542E7 File Offset: 0x000524E7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200027E RID: 638
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001533 RID: 5427
			// (set) Token: 0x06002E77 RID: 11895 RVA: 0x00054307 File Offset: 0x00052507
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationUserIdParameter(value) : null);
				}
			}

			// Token: 0x17001534 RID: 5428
			// (set) Token: 0x06002E78 RID: 11896 RVA: 0x00054325 File Offset: 0x00052525
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001535 RID: 5429
			// (set) Token: 0x06002E79 RID: 11897 RVA: 0x0005433D File Offset: 0x0005253D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001536 RID: 5430
			// (set) Token: 0x06002E7A RID: 11898 RVA: 0x0005435B File Offset: 0x0005255B
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001537 RID: 5431
			// (set) Token: 0x06002E7B RID: 11899 RVA: 0x00054379 File Offset: 0x00052579
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001538 RID: 5432
			// (set) Token: 0x06002E7C RID: 11900 RVA: 0x0005438C File Offset: 0x0005258C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001539 RID: 5433
			// (set) Token: 0x06002E7D RID: 11901 RVA: 0x000543A4 File Offset: 0x000525A4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700153A RID: 5434
			// (set) Token: 0x06002E7E RID: 11902 RVA: 0x000543BC File Offset: 0x000525BC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700153B RID: 5435
			// (set) Token: 0x06002E7F RID: 11903 RVA: 0x000543D4 File Offset: 0x000525D4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700153C RID: 5436
			// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000543EC File Offset: 0x000525EC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700153D RID: 5437
			// (set) Token: 0x06002E81 RID: 11905 RVA: 0x00054404 File Offset: 0x00052604
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
