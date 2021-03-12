using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200039B RID: 923
	public class GetO365ClientOSReportCommand : SyntheticCommandWithPipelineInput<ClientSoftwareOSSummaryReport, ClientSoftwareOSSummaryReport>
	{
		// Token: 0x06003978 RID: 14712 RVA: 0x0006267C File Offset: 0x0006087C
		private GetO365ClientOSReportCommand() : base("Get-O365ClientOSReport")
		{
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x00062689 File Offset: 0x00060889
		public GetO365ClientOSReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x00062698 File Offset: 0x00060898
		public virtual GetO365ClientOSReportCommand SetParameters(GetO365ClientOSReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200039C RID: 924
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001DFB RID: 7675
			// (set) Token: 0x0600397B RID: 14715 RVA: 0x000626A2 File Offset: 0x000608A2
			public virtual string OS
			{
				set
				{
					base.PowerSharpParameters["OS"] = value;
				}
			}

			// Token: 0x17001DFC RID: 7676
			// (set) Token: 0x0600397C RID: 14716 RVA: 0x000626B5 File Offset: 0x000608B5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001DFD RID: 7677
			// (set) Token: 0x0600397D RID: 14717 RVA: 0x000626D3 File Offset: 0x000608D3
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001DFE RID: 7678
			// (set) Token: 0x0600397E RID: 14718 RVA: 0x000626EB File Offset: 0x000608EB
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001DFF RID: 7679
			// (set) Token: 0x0600397F RID: 14719 RVA: 0x00062703 File Offset: 0x00060903
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E00 RID: 7680
			// (set) Token: 0x06003980 RID: 14720 RVA: 0x0006271B File Offset: 0x0006091B
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E01 RID: 7681
			// (set) Token: 0x06003981 RID: 14721 RVA: 0x0006272E File Offset: 0x0006092E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E02 RID: 7682
			// (set) Token: 0x06003982 RID: 14722 RVA: 0x00062746 File Offset: 0x00060946
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E03 RID: 7683
			// (set) Token: 0x06003983 RID: 14723 RVA: 0x0006275E File Offset: 0x0006095E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E04 RID: 7684
			// (set) Token: 0x06003984 RID: 14724 RVA: 0x00062776 File Offset: 0x00060976
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
