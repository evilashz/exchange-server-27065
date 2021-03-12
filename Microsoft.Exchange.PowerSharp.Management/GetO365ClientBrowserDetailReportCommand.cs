using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000395 RID: 917
	public class GetO365ClientBrowserDetailReportCommand : SyntheticCommandWithPipelineInput<ClientSoftwareBrowserDetailReport, ClientSoftwareBrowserDetailReport>
	{
		// Token: 0x0600394A RID: 14666 RVA: 0x000622E2 File Offset: 0x000604E2
		private GetO365ClientBrowserDetailReportCommand() : base("Get-O365ClientBrowserDetailReport")
		{
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000622EF File Offset: 0x000604EF
		public GetO365ClientBrowserDetailReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000622FE File Offset: 0x000604FE
		public virtual GetO365ClientBrowserDetailReportCommand SetParameters(GetO365ClientBrowserDetailReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000396 RID: 918
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001DD9 RID: 7641
			// (set) Token: 0x0600394D RID: 14669 RVA: 0x00062308 File Offset: 0x00060508
			public virtual string Browser
			{
				set
				{
					base.PowerSharpParameters["Browser"] = value;
				}
			}

			// Token: 0x17001DDA RID: 7642
			// (set) Token: 0x0600394E RID: 14670 RVA: 0x0006231B File Offset: 0x0006051B
			public virtual string BrowserVersion
			{
				set
				{
					base.PowerSharpParameters["BrowserVersion"] = value;
				}
			}

			// Token: 0x17001DDB RID: 7643
			// (set) Token: 0x0600394F RID: 14671 RVA: 0x0006232E File Offset: 0x0006052E
			public virtual string WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17001DDC RID: 7644
			// (set) Token: 0x06003950 RID: 14672 RVA: 0x00062341 File Offset: 0x00060541
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001DDD RID: 7645
			// (set) Token: 0x06003951 RID: 14673 RVA: 0x0006235F File Offset: 0x0006055F
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001DDE RID: 7646
			// (set) Token: 0x06003952 RID: 14674 RVA: 0x00062377 File Offset: 0x00060577
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001DDF RID: 7647
			// (set) Token: 0x06003953 RID: 14675 RVA: 0x0006238F File Offset: 0x0006058F
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001DE0 RID: 7648
			// (set) Token: 0x06003954 RID: 14676 RVA: 0x000623A7 File Offset: 0x000605A7
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001DE1 RID: 7649
			// (set) Token: 0x06003955 RID: 14677 RVA: 0x000623BA File Offset: 0x000605BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DE2 RID: 7650
			// (set) Token: 0x06003956 RID: 14678 RVA: 0x000623D2 File Offset: 0x000605D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DE3 RID: 7651
			// (set) Token: 0x06003957 RID: 14679 RVA: 0x000623EA File Offset: 0x000605EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DE4 RID: 7652
			// (set) Token: 0x06003958 RID: 14680 RVA: 0x00062402 File Offset: 0x00060602
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
