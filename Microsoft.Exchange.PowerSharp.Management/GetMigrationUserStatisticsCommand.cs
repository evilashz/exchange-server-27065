using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.Migration;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200026D RID: 621
	public class GetMigrationUserStatisticsCommand : SyntheticCommandWithPipelineInput<MigrationUserStatistics, MigrationUserStatistics>
	{
		// Token: 0x06002D4F RID: 11599 RVA: 0x0005299E File Offset: 0x00050B9E
		private GetMigrationUserStatisticsCommand() : base("Get-MigrationUserStatistics")
		{
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000529AB File Offset: 0x00050BAB
		public GetMigrationUserStatisticsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000529BA File Offset: 0x00050BBA
		public virtual GetMigrationUserStatisticsCommand SetParameters(GetMigrationUserStatisticsCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x000529C4 File Offset: 0x00050BC4
		public virtual GetMigrationUserStatisticsCommand SetParameters(GetMigrationUserStatisticsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200026E RID: 622
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700142E RID: 5166
			// (set) Token: 0x06002D53 RID: 11603 RVA: 0x000529CE File Offset: 0x00050BCE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationUserIdParameter(value) : null);
				}
			}

			// Token: 0x1700142F RID: 5167
			// (set) Token: 0x06002D54 RID: 11604 RVA: 0x000529EC File Offset: 0x00050BEC
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x17001430 RID: 5168
			// (set) Token: 0x06002D55 RID: 11605 RVA: 0x00052A04 File Offset: 0x00050C04
			public virtual int? LimitSkippedItemsTo
			{
				set
				{
					base.PowerSharpParameters["LimitSkippedItemsTo"] = value;
				}
			}

			// Token: 0x17001431 RID: 5169
			// (set) Token: 0x06002D56 RID: 11606 RVA: 0x00052A1C File Offset: 0x00050C1C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001432 RID: 5170
			// (set) Token: 0x06002D57 RID: 11607 RVA: 0x00052A3A File Offset: 0x00050C3A
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001433 RID: 5171
			// (set) Token: 0x06002D58 RID: 11608 RVA: 0x00052A58 File Offset: 0x00050C58
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x17001434 RID: 5172
			// (set) Token: 0x06002D59 RID: 11609 RVA: 0x00052A70 File Offset: 0x00050C70
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17001435 RID: 5173
			// (set) Token: 0x06002D5A RID: 11610 RVA: 0x00052A83 File Offset: 0x00050C83
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001436 RID: 5174
			// (set) Token: 0x06002D5B RID: 11611 RVA: 0x00052A96 File Offset: 0x00050C96
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001437 RID: 5175
			// (set) Token: 0x06002D5C RID: 11612 RVA: 0x00052AAE File Offset: 0x00050CAE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001438 RID: 5176
			// (set) Token: 0x06002D5D RID: 11613 RVA: 0x00052AC6 File Offset: 0x00050CC6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001439 RID: 5177
			// (set) Token: 0x06002D5E RID: 11614 RVA: 0x00052ADE File Offset: 0x00050CDE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200026F RID: 623
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700143A RID: 5178
			// (set) Token: 0x06002D60 RID: 11616 RVA: 0x00052AFE File Offset: 0x00050CFE
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700143B RID: 5179
			// (set) Token: 0x06002D61 RID: 11617 RVA: 0x00052B16 File Offset: 0x00050D16
			public virtual int? LimitSkippedItemsTo
			{
				set
				{
					base.PowerSharpParameters["LimitSkippedItemsTo"] = value;
				}
			}

			// Token: 0x1700143C RID: 5180
			// (set) Token: 0x06002D62 RID: 11618 RVA: 0x00052B2E File Offset: 0x00050D2E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700143D RID: 5181
			// (set) Token: 0x06002D63 RID: 11619 RVA: 0x00052B4C File Offset: 0x00050D4C
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700143E RID: 5182
			// (set) Token: 0x06002D64 RID: 11620 RVA: 0x00052B6A File Offset: 0x00050D6A
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x1700143F RID: 5183
			// (set) Token: 0x06002D65 RID: 11621 RVA: 0x00052B82 File Offset: 0x00050D82
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x17001440 RID: 5184
			// (set) Token: 0x06002D66 RID: 11622 RVA: 0x00052B95 File Offset: 0x00050D95
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001441 RID: 5185
			// (set) Token: 0x06002D67 RID: 11623 RVA: 0x00052BA8 File Offset: 0x00050DA8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001442 RID: 5186
			// (set) Token: 0x06002D68 RID: 11624 RVA: 0x00052BC0 File Offset: 0x00050DC0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001443 RID: 5187
			// (set) Token: 0x06002D69 RID: 11625 RVA: 0x00052BD8 File Offset: 0x00050DD8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001444 RID: 5188
			// (set) Token: 0x06002D6A RID: 11626 RVA: 0x00052BF0 File Offset: 0x00050DF0
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
