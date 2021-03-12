using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000279 RID: 633
	public class RemoveMigrationBatchCommand : SyntheticCommandWithPipelineInput<MigrationBatchIdParameter, MigrationBatchIdParameter>
	{
		// Token: 0x06002E4D RID: 11853 RVA: 0x00053F8C File Offset: 0x0005218C
		private RemoveMigrationBatchCommand() : base("Remove-MigrationBatch")
		{
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x00053F99 File Offset: 0x00052199
		public RemoveMigrationBatchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x00053FA8 File Offset: 0x000521A8
		public virtual RemoveMigrationBatchCommand SetParameters(RemoveMigrationBatchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x00053FB2 File Offset: 0x000521B2
		public virtual RemoveMigrationBatchCommand SetParameters(RemoveMigrationBatchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200027A RID: 634
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001514 RID: 5396
			// (set) Token: 0x06002E51 RID: 11857 RVA: 0x00053FBC File Offset: 0x000521BC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001515 RID: 5397
			// (set) Token: 0x06002E52 RID: 11858 RVA: 0x00053FD4 File Offset: 0x000521D4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001516 RID: 5398
			// (set) Token: 0x06002E53 RID: 11859 RVA: 0x00053FF2 File Offset: 0x000521F2
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001517 RID: 5399
			// (set) Token: 0x06002E54 RID: 11860 RVA: 0x00054010 File Offset: 0x00052210
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001518 RID: 5400
			// (set) Token: 0x06002E55 RID: 11861 RVA: 0x00054023 File Offset: 0x00052223
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001519 RID: 5401
			// (set) Token: 0x06002E56 RID: 11862 RVA: 0x0005403B File Offset: 0x0005223B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700151A RID: 5402
			// (set) Token: 0x06002E57 RID: 11863 RVA: 0x00054053 File Offset: 0x00052253
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700151B RID: 5403
			// (set) Token: 0x06002E58 RID: 11864 RVA: 0x0005406B File Offset: 0x0005226B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700151C RID: 5404
			// (set) Token: 0x06002E59 RID: 11865 RVA: 0x00054083 File Offset: 0x00052283
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700151D RID: 5405
			// (set) Token: 0x06002E5A RID: 11866 RVA: 0x0005409B File Offset: 0x0005229B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200027B RID: 635
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700151E RID: 5406
			// (set) Token: 0x06002E5C RID: 11868 RVA: 0x000540BB File Offset: 0x000522BB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationBatchIdParameter(value) : null);
				}
			}

			// Token: 0x1700151F RID: 5407
			// (set) Token: 0x06002E5D RID: 11869 RVA: 0x000540D9 File Offset: 0x000522D9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001520 RID: 5408
			// (set) Token: 0x06002E5E RID: 11870 RVA: 0x000540F1 File Offset: 0x000522F1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001521 RID: 5409
			// (set) Token: 0x06002E5F RID: 11871 RVA: 0x0005410F File Offset: 0x0005230F
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001522 RID: 5410
			// (set) Token: 0x06002E60 RID: 11872 RVA: 0x0005412D File Offset: 0x0005232D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001523 RID: 5411
			// (set) Token: 0x06002E61 RID: 11873 RVA: 0x00054140 File Offset: 0x00052340
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001524 RID: 5412
			// (set) Token: 0x06002E62 RID: 11874 RVA: 0x00054158 File Offset: 0x00052358
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001525 RID: 5413
			// (set) Token: 0x06002E63 RID: 11875 RVA: 0x00054170 File Offset: 0x00052370
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001526 RID: 5414
			// (set) Token: 0x06002E64 RID: 11876 RVA: 0x00054188 File Offset: 0x00052388
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001527 RID: 5415
			// (set) Token: 0x06002E65 RID: 11877 RVA: 0x000541A0 File Offset: 0x000523A0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001528 RID: 5416
			// (set) Token: 0x06002E66 RID: 11878 RVA: 0x000541B8 File Offset: 0x000523B8
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
