using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Monitoring.Reporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002AF RID: 687
	public class GetMessageStatisticsReportCommand : SyntheticCommandWithPipelineInputNoOutput<ExDateTime>
	{
		// Token: 0x060030AC RID: 12460 RVA: 0x00057238 File Offset: 0x00055438
		private GetMessageStatisticsReportCommand() : base("Get-MessageStatisticsReport")
		{
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x00057245 File Offset: 0x00055445
		public GetMessageStatisticsReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x00057254 File Offset: 0x00055454
		public virtual GetMessageStatisticsReportCommand SetParameters(GetMessageStatisticsReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x0005725E File Offset: 0x0005545E
		public virtual GetMessageStatisticsReportCommand SetParameters(GetMessageStatisticsReportCommand.StartEndDateTimeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x00057268 File Offset: 0x00055468
		public virtual GetMessageStatisticsReportCommand SetParameters(GetMessageStatisticsReportCommand.ReportingPeriodParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002B0 RID: 688
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001707 RID: 5895
			// (set) Token: 0x060030B1 RID: 12465 RVA: 0x00057272 File Offset: 0x00055472
			public virtual string AdSite
			{
				set
				{
					base.PowerSharpParameters["AdSite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17001708 RID: 5896
			// (set) Token: 0x060030B2 RID: 12466 RVA: 0x00057290 File Offset: 0x00055490
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x17001709 RID: 5897
			// (set) Token: 0x060030B3 RID: 12467 RVA: 0x000572A8 File Offset: 0x000554A8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700170A RID: 5898
			// (set) Token: 0x060030B4 RID: 12468 RVA: 0x000572C6 File Offset: 0x000554C6
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x1700170B RID: 5899
			// (set) Token: 0x060030B5 RID: 12469 RVA: 0x000572D9 File Offset: 0x000554D9
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x1700170C RID: 5900
			// (set) Token: 0x060030B6 RID: 12470 RVA: 0x000572EC File Offset: 0x000554EC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700170D RID: 5901
			// (set) Token: 0x060030B7 RID: 12471 RVA: 0x000572FF File Offset: 0x000554FF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700170E RID: 5902
			// (set) Token: 0x060030B8 RID: 12472 RVA: 0x00057317 File Offset: 0x00055517
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700170F RID: 5903
			// (set) Token: 0x060030B9 RID: 12473 RVA: 0x0005732F File Offset: 0x0005552F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001710 RID: 5904
			// (set) Token: 0x060030BA RID: 12474 RVA: 0x00057347 File Offset: 0x00055547
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002B1 RID: 689
		public class StartEndDateTimeParameters : ParametersBase
		{
			// Token: 0x17001711 RID: 5905
			// (set) Token: 0x060030BC RID: 12476 RVA: 0x00057367 File Offset: 0x00055567
			public virtual ExDateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001712 RID: 5906
			// (set) Token: 0x060030BD RID: 12477 RVA: 0x0005737F File Offset: 0x0005557F
			public virtual ExDateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001713 RID: 5907
			// (set) Token: 0x060030BE RID: 12478 RVA: 0x00057397 File Offset: 0x00055597
			public virtual string AdSite
			{
				set
				{
					base.PowerSharpParameters["AdSite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x17001714 RID: 5908
			// (set) Token: 0x060030BF RID: 12479 RVA: 0x000573B5 File Offset: 0x000555B5
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x17001715 RID: 5909
			// (set) Token: 0x060030C0 RID: 12480 RVA: 0x000573CD File Offset: 0x000555CD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001716 RID: 5910
			// (set) Token: 0x060030C1 RID: 12481 RVA: 0x000573EB File Offset: 0x000555EB
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001717 RID: 5911
			// (set) Token: 0x060030C2 RID: 12482 RVA: 0x000573FE File Offset: 0x000555FE
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x17001718 RID: 5912
			// (set) Token: 0x060030C3 RID: 12483 RVA: 0x00057411 File Offset: 0x00055611
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001719 RID: 5913
			// (set) Token: 0x060030C4 RID: 12484 RVA: 0x00057424 File Offset: 0x00055624
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700171A RID: 5914
			// (set) Token: 0x060030C5 RID: 12485 RVA: 0x0005743C File Offset: 0x0005563C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700171B RID: 5915
			// (set) Token: 0x060030C6 RID: 12486 RVA: 0x00057454 File Offset: 0x00055654
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700171C RID: 5916
			// (set) Token: 0x060030C7 RID: 12487 RVA: 0x0005746C File Offset: 0x0005566C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002B2 RID: 690
		public class ReportingPeriodParameters : ParametersBase
		{
			// Token: 0x1700171D RID: 5917
			// (set) Token: 0x060030C9 RID: 12489 RVA: 0x0005748C File Offset: 0x0005568C
			public virtual ReportingPeriod ReportingPeriod
			{
				set
				{
					base.PowerSharpParameters["ReportingPeriod"] = value;
				}
			}

			// Token: 0x1700171E RID: 5918
			// (set) Token: 0x060030CA RID: 12490 RVA: 0x000574A4 File Offset: 0x000556A4
			public virtual string AdSite
			{
				set
				{
					base.PowerSharpParameters["AdSite"] = ((value != null) ? new AdSiteIdParameter(value) : null);
				}
			}

			// Token: 0x1700171F RID: 5919
			// (set) Token: 0x060030CB RID: 12491 RVA: 0x000574C2 File Offset: 0x000556C2
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x17001720 RID: 5920
			// (set) Token: 0x060030CC RID: 12492 RVA: 0x000574DA File Offset: 0x000556DA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001721 RID: 5921
			// (set) Token: 0x060030CD RID: 12493 RVA: 0x000574F8 File Offset: 0x000556F8
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001722 RID: 5922
			// (set) Token: 0x060030CE RID: 12494 RVA: 0x0005750B File Offset: 0x0005570B
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x17001723 RID: 5923
			// (set) Token: 0x060030CF RID: 12495 RVA: 0x0005751E File Offset: 0x0005571E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001724 RID: 5924
			// (set) Token: 0x060030D0 RID: 12496 RVA: 0x00057531 File Offset: 0x00055731
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001725 RID: 5925
			// (set) Token: 0x060030D1 RID: 12497 RVA: 0x00057549 File Offset: 0x00055749
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001726 RID: 5926
			// (set) Token: 0x060030D2 RID: 12498 RVA: 0x00057561 File Offset: 0x00055761
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001727 RID: 5927
			// (set) Token: 0x060030D3 RID: 12499 RVA: 0x00057579 File Offset: 0x00055779
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
