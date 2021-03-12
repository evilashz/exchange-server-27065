using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Monitoring.Reporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002C1 RID: 705
	public class GetServiceAvailabilityReportCommand : SyntheticCommandWithPipelineInput<OrganizationIdParameter, OrganizationIdParameter>
	{
		// Token: 0x0600312F RID: 12591 RVA: 0x00057C34 File Offset: 0x00055E34
		private GetServiceAvailabilityReportCommand() : base("Get-ServiceAvailabilityReport")
		{
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x00057C41 File Offset: 0x00055E41
		public GetServiceAvailabilityReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x00057C50 File Offset: 0x00055E50
		public virtual GetServiceAvailabilityReportCommand SetParameters(GetServiceAvailabilityReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x00057C5A File Offset: 0x00055E5A
		public virtual GetServiceAvailabilityReportCommand SetParameters(GetServiceAvailabilityReportCommand.StartEndDateSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x00057C64 File Offset: 0x00055E64
		public virtual GetServiceAvailabilityReportCommand SetParameters(GetServiceAvailabilityReportCommand.ReportingPeriodSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002C2 RID: 706
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001766 RID: 5990
			// (set) Token: 0x06003134 RID: 12596 RVA: 0x00057C6E File Offset: 0x00055E6E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001767 RID: 5991
			// (set) Token: 0x06003135 RID: 12597 RVA: 0x00057C8C File Offset: 0x00055E8C
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001768 RID: 5992
			// (set) Token: 0x06003136 RID: 12598 RVA: 0x00057C9F File Offset: 0x00055E9F
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x17001769 RID: 5993
			// (set) Token: 0x06003137 RID: 12599 RVA: 0x00057CB2 File Offset: 0x00055EB2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700176A RID: 5994
			// (set) Token: 0x06003138 RID: 12600 RVA: 0x00057CC5 File Offset: 0x00055EC5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700176B RID: 5995
			// (set) Token: 0x06003139 RID: 12601 RVA: 0x00057CDD File Offset: 0x00055EDD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700176C RID: 5996
			// (set) Token: 0x0600313A RID: 12602 RVA: 0x00057CF5 File Offset: 0x00055EF5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700176D RID: 5997
			// (set) Token: 0x0600313B RID: 12603 RVA: 0x00057D0D File Offset: 0x00055F0D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002C3 RID: 707
		public class StartEndDateSetParameters : ParametersBase
		{
			// Token: 0x1700176E RID: 5998
			// (set) Token: 0x0600313D RID: 12605 RVA: 0x00057D2D File Offset: 0x00055F2D
			public virtual DateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x1700176F RID: 5999
			// (set) Token: 0x0600313E RID: 12606 RVA: 0x00057D45 File Offset: 0x00055F45
			public virtual DateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001770 RID: 6000
			// (set) Token: 0x0600313F RID: 12607 RVA: 0x00057D5D File Offset: 0x00055F5D
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x17001771 RID: 6001
			// (set) Token: 0x06003140 RID: 12608 RVA: 0x00057D75 File Offset: 0x00055F75
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001772 RID: 6002
			// (set) Token: 0x06003141 RID: 12609 RVA: 0x00057D93 File Offset: 0x00055F93
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x17001773 RID: 6003
			// (set) Token: 0x06003142 RID: 12610 RVA: 0x00057DA6 File Offset: 0x00055FA6
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x17001774 RID: 6004
			// (set) Token: 0x06003143 RID: 12611 RVA: 0x00057DB9 File Offset: 0x00055FB9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001775 RID: 6005
			// (set) Token: 0x06003144 RID: 12612 RVA: 0x00057DCC File Offset: 0x00055FCC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001776 RID: 6006
			// (set) Token: 0x06003145 RID: 12613 RVA: 0x00057DE4 File Offset: 0x00055FE4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001777 RID: 6007
			// (set) Token: 0x06003146 RID: 12614 RVA: 0x00057DFC File Offset: 0x00055FFC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001778 RID: 6008
			// (set) Token: 0x06003147 RID: 12615 RVA: 0x00057E14 File Offset: 0x00056014
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020002C4 RID: 708
		public class ReportingPeriodSetParameters : ParametersBase
		{
			// Token: 0x17001779 RID: 6009
			// (set) Token: 0x06003149 RID: 12617 RVA: 0x00057E34 File Offset: 0x00056034
			public virtual ReportingPeriod ReportingPeriod
			{
				set
				{
					base.PowerSharpParameters["ReportingPeriod"] = value;
				}
			}

			// Token: 0x1700177A RID: 6010
			// (set) Token: 0x0600314A RID: 12618 RVA: 0x00057E4C File Offset: 0x0005604C
			public virtual SwitchParameter DailyStatistics
			{
				set
				{
					base.PowerSharpParameters["DailyStatistics"] = value;
				}
			}

			// Token: 0x1700177B RID: 6011
			// (set) Token: 0x0600314B RID: 12619 RVA: 0x00057E64 File Offset: 0x00056064
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700177C RID: 6012
			// (set) Token: 0x0600314C RID: 12620 RVA: 0x00057E82 File Offset: 0x00056082
			public virtual Fqdn ReportingServer
			{
				set
				{
					base.PowerSharpParameters["ReportingServer"] = value;
				}
			}

			// Token: 0x1700177D RID: 6013
			// (set) Token: 0x0600314D RID: 12621 RVA: 0x00057E95 File Offset: 0x00056095
			public virtual string ReportingDatabase
			{
				set
				{
					base.PowerSharpParameters["ReportingDatabase"] = value;
				}
			}

			// Token: 0x1700177E RID: 6014
			// (set) Token: 0x0600314E RID: 12622 RVA: 0x00057EA8 File Offset: 0x000560A8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700177F RID: 6015
			// (set) Token: 0x0600314F RID: 12623 RVA: 0x00057EBB File Offset: 0x000560BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001780 RID: 6016
			// (set) Token: 0x06003150 RID: 12624 RVA: 0x00057ED3 File Offset: 0x000560D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001781 RID: 6017
			// (set) Token: 0x06003151 RID: 12625 RVA: 0x00057EEB File Offset: 0x000560EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001782 RID: 6018
			// (set) Token: 0x06003152 RID: 12626 RVA: 0x00057F03 File Offset: 0x00056103
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
