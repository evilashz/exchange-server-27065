using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020009EB RID: 2539
	public class GetMergeRequestStatisticsCommand : SyntheticCommandWithPipelineInput<MergeRequestStatistics, MergeRequestStatistics>
	{
		// Token: 0x06007F64 RID: 32612 RVA: 0x000BD2C9 File Offset: 0x000BB4C9
		private GetMergeRequestStatisticsCommand() : base("Get-MergeRequestStatistics")
		{
		}

		// Token: 0x06007F65 RID: 32613 RVA: 0x000BD2D6 File Offset: 0x000BB4D6
		public GetMergeRequestStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007F66 RID: 32614 RVA: 0x000BD2E5 File Offset: 0x000BB4E5
		public virtual GetMergeRequestStatisticsCommand SetParameters(GetMergeRequestStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007F67 RID: 32615 RVA: 0x000BD2EF File Offset: 0x000BB4EF
		public virtual GetMergeRequestStatisticsCommand SetParameters(GetMergeRequestStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007F68 RID: 32616 RVA: 0x000BD2F9 File Offset: 0x000BB4F9
		public virtual GetMergeRequestStatisticsCommand SetParameters(GetMergeRequestStatisticsCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020009EC RID: 2540
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005747 RID: 22343
			// (set) Token: 0x06007F69 RID: 32617 RVA: 0x000BD303 File Offset: 0x000BB503
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MergeRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005748 RID: 22344
			// (set) Token: 0x06007F6A RID: 32618 RVA: 0x000BD321 File Offset: 0x000BB521
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005749 RID: 22345
			// (set) Token: 0x06007F6B RID: 32619 RVA: 0x000BD339 File Offset: 0x000BB539
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700574A RID: 22346
			// (set) Token: 0x06007F6C RID: 32620 RVA: 0x000BD34C File Offset: 0x000BB54C
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x1700574B RID: 22347
			// (set) Token: 0x06007F6D RID: 32621 RVA: 0x000BD364 File Offset: 0x000BB564
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x1700574C RID: 22348
			// (set) Token: 0x06007F6E RID: 32622 RVA: 0x000BD377 File Offset: 0x000BB577
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700574D RID: 22349
			// (set) Token: 0x06007F6F RID: 32623 RVA: 0x000BD38F File Offset: 0x000BB58F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700574E RID: 22350
			// (set) Token: 0x06007F70 RID: 32624 RVA: 0x000BD3A7 File Offset: 0x000BB5A7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700574F RID: 22351
			// (set) Token: 0x06007F71 RID: 32625 RVA: 0x000BD3BF File Offset: 0x000BB5BF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009ED RID: 2541
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005750 RID: 22352
			// (set) Token: 0x06007F73 RID: 32627 RVA: 0x000BD3DF File Offset: 0x000BB5DF
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17005751 RID: 22353
			// (set) Token: 0x06007F74 RID: 32628 RVA: 0x000BD3F7 File Offset: 0x000BB5F7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005752 RID: 22354
			// (set) Token: 0x06007F75 RID: 32629 RVA: 0x000BD40A File Offset: 0x000BB60A
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17005753 RID: 22355
			// (set) Token: 0x06007F76 RID: 32630 RVA: 0x000BD422 File Offset: 0x000BB622
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17005754 RID: 22356
			// (set) Token: 0x06007F77 RID: 32631 RVA: 0x000BD435 File Offset: 0x000BB635
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005755 RID: 22357
			// (set) Token: 0x06007F78 RID: 32632 RVA: 0x000BD44D File Offset: 0x000BB64D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005756 RID: 22358
			// (set) Token: 0x06007F79 RID: 32633 RVA: 0x000BD465 File Offset: 0x000BB665
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005757 RID: 22359
			// (set) Token: 0x06007F7A RID: 32634 RVA: 0x000BD47D File Offset: 0x000BB67D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020009EE RID: 2542
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005758 RID: 22360
			// (set) Token: 0x06007F7C RID: 32636 RVA: 0x000BD49D File Offset: 0x000BB69D
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005759 RID: 22361
			// (set) Token: 0x06007F7D RID: 32637 RVA: 0x000BD4B0 File Offset: 0x000BB6B0
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x1700575A RID: 22362
			// (set) Token: 0x06007F7E RID: 32638 RVA: 0x000BD4C8 File Offset: 0x000BB6C8
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700575B RID: 22363
			// (set) Token: 0x06007F7F RID: 32639 RVA: 0x000BD4E0 File Offset: 0x000BB6E0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700575C RID: 22364
			// (set) Token: 0x06007F80 RID: 32640 RVA: 0x000BD4F3 File Offset: 0x000BB6F3
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x1700575D RID: 22365
			// (set) Token: 0x06007F81 RID: 32641 RVA: 0x000BD50B File Offset: 0x000BB70B
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x1700575E RID: 22366
			// (set) Token: 0x06007F82 RID: 32642 RVA: 0x000BD51E File Offset: 0x000BB71E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700575F RID: 22367
			// (set) Token: 0x06007F83 RID: 32643 RVA: 0x000BD536 File Offset: 0x000BB736
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005760 RID: 22368
			// (set) Token: 0x06007F84 RID: 32644 RVA: 0x000BD54E File Offset: 0x000BB74E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005761 RID: 22369
			// (set) Token: 0x06007F85 RID: 32645 RVA: 0x000BD566 File Offset: 0x000BB766
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
