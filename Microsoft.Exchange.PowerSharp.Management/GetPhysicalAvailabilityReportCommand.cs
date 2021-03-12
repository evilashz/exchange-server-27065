using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Monitoring.Reporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002B7 RID: 695
	public class GetPhysicalAvailabilityReportCommand : SyntheticCommandWithPipelineInput<OrganizationIdParameter, OrganizationIdParameter>
	{
		// Token: 0x060030E9 RID: 12521 RVA: 0x00057701 File Offset: 0x00055901
		private GetPhysicalAvailabilityReportCommand() : base("Get-PhysicalAvailabilityReport")
		{
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x0005770E File Offset: 0x0005590E
		public GetPhysicalAvailabilityReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x0005771D File Offset: 0x0005591D
		public virtual GetPhysicalAvailabilityReportCommand SetParameters(GetPhysicalAvailabilityReportCommand.ReportingPeriodSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x00057727 File Offset: 0x00055927
		public virtual GetPhysicalAvailabilityReportCommand SetParameters(GetPhysicalAvailabilityReportCommand.StartEndDateSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x00057731 File Offset: 0x00055931
		public virtual GetPhysicalAvailabilityReportCommand SetParameters(GetPhysicalAvailabilityReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002B8 RID: 696
		public class ReportingPeriodSetParameters : ParametersBase
		{
			// Token: 0x17001734 RID: 5940
			// (set) Token: 0x060030EE RID: 12526 RVA: 0x0005773B File Offset: 0x0005593B
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001735 RID: 5941
			// (set) Token: 0x060030EF RID: 12527 RVA: 0x0005774E File Offset: 0x0005594E
			public virtual ServerIdParameter ExchangeServer
			{
				set
				{
					base.PowerSharpParameters["ExchangeServer"] = value;
				}
			}

			// Token: 0x17001736 RID: 5942
			// (set) Token: 0x060030F0 RID: 12528 RVA: 0x00057761 File Offset: 0x00055961
			public virtual ReportingPeriod ReportingPeriod
			{
				set
				{
					base.PowerSharpParameters["ReportingPeriod"] = value;
				}
			}

			// Token: 0x17001737 RID: 5943
			// (set) Token: 0x060030F1 RID: 12529 RVA: 0x00057779 File Offset: 0x00055979
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x17001738 RID: 5944
			// (set) Token: 0x060030F2 RID: 12530 RVA: 0x00057791 File Offset: 0x00055991
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001739 RID: 5945
			// (set) Token: 0x060030F3 RID: 12531 RVA: 0x000577A4 File Offset: 0x000559A4
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x1700173A RID: 5946
			// (set) Token: 0x060030F4 RID: 12532 RVA: 0x000577B7 File Offset: 0x000559B7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700173B RID: 5947
			// (set) Token: 0x060030F5 RID: 12533 RVA: 0x000577CA File Offset: 0x000559CA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700173C RID: 5948
			// (set) Token: 0x060030F6 RID: 12534 RVA: 0x000577E2 File Offset: 0x000559E2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700173D RID: 5949
			// (set) Token: 0x060030F7 RID: 12535 RVA: 0x000577FA File Offset: 0x000559FA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700173E RID: 5950
			// (set) Token: 0x060030F8 RID: 12536 RVA: 0x00057812 File Offset: 0x00055A12
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002B9 RID: 697
		public class StartEndDateSetParameters : ParametersBase
		{
			// Token: 0x1700173F RID: 5951
			// (set) Token: 0x060030FA RID: 12538 RVA: 0x00057832 File Offset: 0x00055A32
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17001740 RID: 5952
			// (set) Token: 0x060030FB RID: 12539 RVA: 0x00057845 File Offset: 0x00055A45
			public virtual ServerIdParameter ExchangeServer
			{
				set
				{
					base.PowerSharpParameters["ExchangeServer"] = value;
				}
			}

			// Token: 0x17001741 RID: 5953
			// (set) Token: 0x060030FC RID: 12540 RVA: 0x00057858 File Offset: 0x00055A58
			public virtual DateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001742 RID: 5954
			// (set) Token: 0x060030FD RID: 12541 RVA: 0x00057870 File Offset: 0x00055A70
			public virtual DateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001743 RID: 5955
			// (set) Token: 0x060030FE RID: 12542 RVA: 0x00057888 File Offset: 0x00055A88
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x17001744 RID: 5956
			// (set) Token: 0x060030FF RID: 12543 RVA: 0x000578A0 File Offset: 0x00055AA0
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001745 RID: 5957
			// (set) Token: 0x06003100 RID: 12544 RVA: 0x000578B3 File Offset: 0x00055AB3
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x17001746 RID: 5958
			// (set) Token: 0x06003101 RID: 12545 RVA: 0x000578C6 File Offset: 0x00055AC6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001747 RID: 5959
			// (set) Token: 0x06003102 RID: 12546 RVA: 0x000578D9 File Offset: 0x00055AD9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001748 RID: 5960
			// (set) Token: 0x06003103 RID: 12547 RVA: 0x000578F1 File Offset: 0x00055AF1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001749 RID: 5961
			// (set) Token: 0x06003104 RID: 12548 RVA: 0x00057909 File Offset: 0x00055B09
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700174A RID: 5962
			// (set) Token: 0x06003105 RID: 12549 RVA: 0x00057921 File Offset: 0x00055B21
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002BA RID: 698
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700174B RID: 5963
			// (set) Token: 0x06003107 RID: 12551 RVA: 0x00057941 File Offset: 0x00055B41
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x1700174C RID: 5964
			// (set) Token: 0x06003108 RID: 12552 RVA: 0x00057954 File Offset: 0x00055B54
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x1700174D RID: 5965
			// (set) Token: 0x06003109 RID: 12553 RVA: 0x00057967 File Offset: 0x00055B67
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700174E RID: 5966
			// (set) Token: 0x0600310A RID: 12554 RVA: 0x0005797A File Offset: 0x00055B7A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700174F RID: 5967
			// (set) Token: 0x0600310B RID: 12555 RVA: 0x00057992 File Offset: 0x00055B92
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001750 RID: 5968
			// (set) Token: 0x0600310C RID: 12556 RVA: 0x000579AA File Offset: 0x00055BAA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001751 RID: 5969
			// (set) Token: 0x0600310D RID: 12557 RVA: 0x000579C2 File Offset: 0x00055BC2
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
