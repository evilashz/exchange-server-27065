using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Monitoring.Reporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002AB RID: 683
	public class GetMessageLatencyReportCommand : SyntheticCommandWithPipelineInputNoOutput<ExDateTime>
	{
		// Token: 0x06003080 RID: 12416 RVA: 0x00056E8F File Offset: 0x0005508F
		private GetMessageLatencyReportCommand() : base("Get-MessageLatencyReport")
		{
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x00056E9C File Offset: 0x0005509C
		public GetMessageLatencyReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x00056EAB File Offset: 0x000550AB
		public virtual GetMessageLatencyReportCommand SetParameters(GetMessageLatencyReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x00056EB5 File Offset: 0x000550B5
		public virtual GetMessageLatencyReportCommand SetParameters(GetMessageLatencyReportCommand.StartEndDateTimeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x00056EBF File Offset: 0x000550BF
		public virtual GetMessageLatencyReportCommand SetParameters(GetMessageLatencyReportCommand.ReportingPeriodParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002AC RID: 684
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170016E3 RID: 5859
			// (set) Token: 0x06003085 RID: 12421 RVA: 0x00056EC9 File Offset: 0x000550C9
			public virtual EnhancedTimeSpan SlaTargetTimespan
			{
				set
				{
					base.PowerSharpParameters["SlaTargetTimespan"] = value;
				}
			}

			// Token: 0x170016E4 RID: 5860
			// (set) Token: 0x06003086 RID: 12422 RVA: 0x00056EE1 File Offset: 0x000550E1
			public virtual string AdSite
			{
				set
				{
					base.PowerSharpParameters["AdSite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x170016E5 RID: 5861
			// (set) Token: 0x06003087 RID: 12423 RVA: 0x00056EFF File Offset: 0x000550FF
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x170016E6 RID: 5862
			// (set) Token: 0x06003088 RID: 12424 RVA: 0x00056F17 File Offset: 0x00055117
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170016E7 RID: 5863
			// (set) Token: 0x06003089 RID: 12425 RVA: 0x00056F35 File Offset: 0x00055135
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x170016E8 RID: 5864
			// (set) Token: 0x0600308A RID: 12426 RVA: 0x00056F48 File Offset: 0x00055148
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x170016E9 RID: 5865
			// (set) Token: 0x0600308B RID: 12427 RVA: 0x00056F5B File Offset: 0x0005515B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170016EA RID: 5866
			// (set) Token: 0x0600308C RID: 12428 RVA: 0x00056F6E File Offset: 0x0005516E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016EB RID: 5867
			// (set) Token: 0x0600308D RID: 12429 RVA: 0x00056F86 File Offset: 0x00055186
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016EC RID: 5868
			// (set) Token: 0x0600308E RID: 12430 RVA: 0x00056F9E File Offset: 0x0005519E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016ED RID: 5869
			// (set) Token: 0x0600308F RID: 12431 RVA: 0x00056FB6 File Offset: 0x000551B6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002AD RID: 685
		public class StartEndDateTimeParameters : ParametersBase
		{
			// Token: 0x170016EE RID: 5870
			// (set) Token: 0x06003091 RID: 12433 RVA: 0x00056FD6 File Offset: 0x000551D6
			public virtual ExDateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x170016EF RID: 5871
			// (set) Token: 0x06003092 RID: 12434 RVA: 0x00056FEE File Offset: 0x000551EE
			public virtual ExDateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170016F0 RID: 5872
			// (set) Token: 0x06003093 RID: 12435 RVA: 0x00057006 File Offset: 0x00055206
			public virtual EnhancedTimeSpan SlaTargetTimespan
			{
				set
				{
					base.PowerSharpParameters["SlaTargetTimespan"] = value;
				}
			}

			// Token: 0x170016F1 RID: 5873
			// (set) Token: 0x06003094 RID: 12436 RVA: 0x0005701E File Offset: 0x0005521E
			public virtual string AdSite
			{
				set
				{
					base.PowerSharpParameters["AdSite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x170016F2 RID: 5874
			// (set) Token: 0x06003095 RID: 12437 RVA: 0x0005703C File Offset: 0x0005523C
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x170016F3 RID: 5875
			// (set) Token: 0x06003096 RID: 12438 RVA: 0x00057054 File Offset: 0x00055254
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170016F4 RID: 5876
			// (set) Token: 0x06003097 RID: 12439 RVA: 0x00057072 File Offset: 0x00055272
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x170016F5 RID: 5877
			// (set) Token: 0x06003098 RID: 12440 RVA: 0x00057085 File Offset: 0x00055285
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x170016F6 RID: 5878
			// (set) Token: 0x06003099 RID: 12441 RVA: 0x00057098 File Offset: 0x00055298
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170016F7 RID: 5879
			// (set) Token: 0x0600309A RID: 12442 RVA: 0x000570AB File Offset: 0x000552AB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016F8 RID: 5880
			// (set) Token: 0x0600309B RID: 12443 RVA: 0x000570C3 File Offset: 0x000552C3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016F9 RID: 5881
			// (set) Token: 0x0600309C RID: 12444 RVA: 0x000570DB File Offset: 0x000552DB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016FA RID: 5882
			// (set) Token: 0x0600309D RID: 12445 RVA: 0x000570F3 File Offset: 0x000552F3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002AE RID: 686
		public class ReportingPeriodParameters : ParametersBase
		{
			// Token: 0x170016FB RID: 5883
			// (set) Token: 0x0600309F RID: 12447 RVA: 0x00057113 File Offset: 0x00055313
			public virtual ReportingPeriod ReportingPeriod
			{
				set
				{
					base.PowerSharpParameters["ReportingPeriod"] = value;
				}
			}

			// Token: 0x170016FC RID: 5884
			// (set) Token: 0x060030A0 RID: 12448 RVA: 0x0005712B File Offset: 0x0005532B
			public virtual EnhancedTimeSpan SlaTargetTimespan
			{
				set
				{
					base.PowerSharpParameters["SlaTargetTimespan"] = value;
				}
			}

			// Token: 0x170016FD RID: 5885
			// (set) Token: 0x060030A1 RID: 12449 RVA: 0x00057143 File Offset: 0x00055343
			public virtual string AdSite
			{
				set
				{
					base.PowerSharpParameters["AdSite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x170016FE RID: 5886
			// (set) Token: 0x060030A2 RID: 12450 RVA: 0x00057161 File Offset: 0x00055361
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x170016FF RID: 5887
			// (set) Token: 0x060030A3 RID: 12451 RVA: 0x00057179 File Offset: 0x00055379
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001700 RID: 5888
			// (set) Token: 0x060030A4 RID: 12452 RVA: 0x00057197 File Offset: 0x00055397
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001701 RID: 5889
			// (set) Token: 0x060030A5 RID: 12453 RVA: 0x000571AA File Offset: 0x000553AA
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x17001702 RID: 5890
			// (set) Token: 0x060030A6 RID: 12454 RVA: 0x000571BD File Offset: 0x000553BD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001703 RID: 5891
			// (set) Token: 0x060030A7 RID: 12455 RVA: 0x000571D0 File Offset: 0x000553D0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001704 RID: 5892
			// (set) Token: 0x060030A8 RID: 12456 RVA: 0x000571E8 File Offset: 0x000553E8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001705 RID: 5893
			// (set) Token: 0x060030A9 RID: 12457 RVA: 0x00057200 File Offset: 0x00055400
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001706 RID: 5894
			// (set) Token: 0x060030AA RID: 12458 RVA: 0x00057218 File Offset: 0x00055418
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
