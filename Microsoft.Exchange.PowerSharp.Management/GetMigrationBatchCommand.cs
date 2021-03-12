using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Migration;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200025E RID: 606
	public class GetMigrationBatchCommand : SyntheticCommandWithPipelineInput<MigrationBatch, MigrationBatch>
	{
		// Token: 0x06002CCB RID: 11467 RVA: 0x00051E98 File Offset: 0x00050098
		private GetMigrationBatchCommand() : base("Get-MigrationBatch")
		{
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x00051EA5 File Offset: 0x000500A5
		public GetMigrationBatchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x00051EB4 File Offset: 0x000500B4
		public virtual GetMigrationBatchCommand SetParameters(GetMigrationBatchCommand.BatchesFromEndpointParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x00051EBE File Offset: 0x000500BE
		public virtual GetMigrationBatchCommand SetParameters(GetMigrationBatchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x00051EC8 File Offset: 0x000500C8
		public virtual GetMigrationBatchCommand SetParameters(GetMigrationBatchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200025F RID: 607
		public class BatchesFromEndpointParameters : ParametersBase
		{
			// Token: 0x170013C8 RID: 5064
			// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x00051ED2 File Offset: 0x000500D2
			public virtual string Endpoint
			{
				set
				{
					base.PowerSharpParameters["Endpoint"] = ((value != null) ? new MigrationEndpointIdParameter(value) : null);
				}
			}

			// Token: 0x170013C9 RID: 5065
			// (set) Token: 0x06002CD1 RID: 11473 RVA: 0x00051EF0 File Offset: 0x000500F0
			public virtual MigrationBatchStatus? Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170013CA RID: 5066
			// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x00051F08 File Offset: 0x00050108
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x170013CB RID: 5067
			// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x00051F20 File Offset: 0x00050120
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170013CC RID: 5068
			// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x00051F3E File Offset: 0x0005013E
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013CD RID: 5069
			// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x00051F5C File Offset: 0x0005015C
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170013CE RID: 5070
			// (set) Token: 0x06002CD6 RID: 11478 RVA: 0x00051F74 File Offset: 0x00050174
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170013CF RID: 5071
			// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x00051F87 File Offset: 0x00050187
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013D0 RID: 5072
			// (set) Token: 0x06002CD8 RID: 11480 RVA: 0x00051F9A File Offset: 0x0005019A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013D1 RID: 5073
			// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x00051FB2 File Offset: 0x000501B2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013D2 RID: 5074
			// (set) Token: 0x06002CDA RID: 11482 RVA: 0x00051FCA File Offset: 0x000501CA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013D3 RID: 5075
			// (set) Token: 0x06002CDB RID: 11483 RVA: 0x00051FE2 File Offset: 0x000501E2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000260 RID: 608
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170013D4 RID: 5076
			// (set) Token: 0x06002CDD RID: 11485 RVA: 0x00052002 File Offset: 0x00050202
			public virtual MigrationBatchStatus? Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170013D5 RID: 5077
			// (set) Token: 0x06002CDE RID: 11486 RVA: 0x0005201A File Offset: 0x0005021A
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x170013D6 RID: 5078
			// (set) Token: 0x06002CDF RID: 11487 RVA: 0x00052032 File Offset: 0x00050232
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170013D7 RID: 5079
			// (set) Token: 0x06002CE0 RID: 11488 RVA: 0x00052050 File Offset: 0x00050250
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013D8 RID: 5080
			// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x0005206E File Offset: 0x0005026E
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170013D9 RID: 5081
			// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x00052086 File Offset: 0x00050286
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170013DA RID: 5082
			// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x00052099 File Offset: 0x00050299
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013DB RID: 5083
			// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x000520AC File Offset: 0x000502AC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013DC RID: 5084
			// (set) Token: 0x06002CE5 RID: 11493 RVA: 0x000520C4 File Offset: 0x000502C4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013DD RID: 5085
			// (set) Token: 0x06002CE6 RID: 11494 RVA: 0x000520DC File Offset: 0x000502DC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013DE RID: 5086
			// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x000520F4 File Offset: 0x000502F4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000261 RID: 609
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170013DF RID: 5087
			// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x00052114 File Offset: 0x00050314
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MigrationBatchIdParameter(value) : null);
				}
			}

			// Token: 0x170013E0 RID: 5088
			// (set) Token: 0x06002CEA RID: 11498 RVA: 0x00052132 File Offset: 0x00050332
			public virtual MigrationBatchStatus? Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x170013E1 RID: 5089
			// (set) Token: 0x06002CEB RID: 11499 RVA: 0x0005214A File Offset: 0x0005034A
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x170013E2 RID: 5090
			// (set) Token: 0x06002CEC RID: 11500 RVA: 0x00052162 File Offset: 0x00050362
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170013E3 RID: 5091
			// (set) Token: 0x06002CED RID: 11501 RVA: 0x00052180 File Offset: 0x00050380
			public virtual string Partition
			{
				set
				{
					base.PowerSharpParameters["Partition"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170013E4 RID: 5092
			// (set) Token: 0x06002CEE RID: 11502 RVA: 0x0005219E File Offset: 0x0005039E
			public virtual SwitchParameter Diagnostic
			{
				set
				{
					base.PowerSharpParameters["Diagnostic"] = value;
				}
			}

			// Token: 0x170013E5 RID: 5093
			// (set) Token: 0x06002CEF RID: 11503 RVA: 0x000521B6 File Offset: 0x000503B6
			public virtual string DiagnosticArgument
			{
				set
				{
					base.PowerSharpParameters["DiagnosticArgument"] = value;
				}
			}

			// Token: 0x170013E6 RID: 5094
			// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x000521C9 File Offset: 0x000503C9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170013E7 RID: 5095
			// (set) Token: 0x06002CF1 RID: 11505 RVA: 0x000521DC File Offset: 0x000503DC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170013E8 RID: 5096
			// (set) Token: 0x06002CF2 RID: 11506 RVA: 0x000521F4 File Offset: 0x000503F4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170013E9 RID: 5097
			// (set) Token: 0x06002CF3 RID: 11507 RVA: 0x0005220C File Offset: 0x0005040C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170013EA RID: 5098
			// (set) Token: 0x06002CF4 RID: 11508 RVA: 0x00052224 File Offset: 0x00050424
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
