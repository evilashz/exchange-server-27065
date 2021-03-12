using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000265 RID: 613
	public class GetMigrationStatisticsCommand : SyntheticCommandWithPipelineInput<MigrationStatistics, MigrationStatistics>
	{
		// Token: 0x06002D09 RID: 11529 RVA: 0x000523C4 File Offset: 0x000505C4
		private GetMigrationStatisticsCommand() : base("Get-MigrationStatistics")
		{
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000523D1 File Offset: 0x000505D1
		public GetMigrationStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000523E0 File Offset: 0x000505E0
		public virtual GetMigrationStatisticsCommand SetParameters(GetMigrationStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000523EA File Offset: 0x000505EA
		public virtual GetMigrationStatisticsCommand SetParameters(GetMigrationStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000266 RID: 614
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170013F8 RID: 5112
			// (set) Token: 0x06002D0D RID: 11533 RVA: 0x000523F4 File Offset: 0x000505F4
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013F9 RID: 5113
			// (set) Token: 0x06002D0E RID: 11534 RVA: 0x00052412 File Offset: 0x00050612
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170013FA RID: 5114
			// (set) Token: 0x06002D0F RID: 11535 RVA: 0x0005242A File Offset: 0x0005062A
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170013FB RID: 5115
			// (set) Token: 0x06002D10 RID: 11536 RVA: 0x0005243D File Offset: 0x0005063D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013FC RID: 5116
			// (set) Token: 0x06002D11 RID: 11537 RVA: 0x00052450 File Offset: 0x00050650
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013FD RID: 5117
			// (set) Token: 0x06002D12 RID: 11538 RVA: 0x00052468 File Offset: 0x00050668
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013FE RID: 5118
			// (set) Token: 0x06002D13 RID: 11539 RVA: 0x00052480 File Offset: 0x00050680
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013FF RID: 5119
			// (set) Token: 0x06002D14 RID: 11540 RVA: 0x00052498 File Offset: 0x00050698
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000267 RID: 615
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001400 RID: 5120
			// (set) Token: 0x06002D16 RID: 11542 RVA: 0x000524B8 File Offset: 0x000506B8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationStatisticsIdParameter(value) : null);
				}
			}

			// Token: 0x17001401 RID: 5121
			// (set) Token: 0x06002D17 RID: 11543 RVA: 0x000524D6 File Offset: 0x000506D6
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001402 RID: 5122
			// (set) Token: 0x06002D18 RID: 11544 RVA: 0x000524F4 File Offset: 0x000506F4
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17001403 RID: 5123
			// (set) Token: 0x06002D19 RID: 11545 RVA: 0x0005250C File Offset: 0x0005070C
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17001404 RID: 5124
			// (set) Token: 0x06002D1A RID: 11546 RVA: 0x0005251F File Offset: 0x0005071F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001405 RID: 5125
			// (set) Token: 0x06002D1B RID: 11547 RVA: 0x00052532 File Offset: 0x00050732
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001406 RID: 5126
			// (set) Token: 0x06002D1C RID: 11548 RVA: 0x0005254A File Offset: 0x0005074A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001407 RID: 5127
			// (set) Token: 0x06002D1D RID: 11549 RVA: 0x00052562 File Offset: 0x00050762
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001408 RID: 5128
			// (set) Token: 0x06002D1E RID: 11550 RVA: 0x0005257A File Offset: 0x0005077A
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
