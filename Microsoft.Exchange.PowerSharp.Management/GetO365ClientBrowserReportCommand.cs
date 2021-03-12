using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000397 RID: 919
	public class GetO365ClientBrowserReportCommand : SyntheticCommandWithPipelineInput<ClientSoftwareBrowserSummaryReport, ClientSoftwareBrowserSummaryReport>
	{
		// Token: 0x0600395A RID: 14682 RVA: 0x00062422 File Offset: 0x00060622
		private GetO365ClientBrowserReportCommand() : base("Get-O365ClientBrowserReport")
		{
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x0006242F File Offset: 0x0006062F
		public GetO365ClientBrowserReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x0006243E File Offset: 0x0006063E
		public virtual GetO365ClientBrowserReportCommand SetParameters(GetO365ClientBrowserReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000398 RID: 920
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001DE5 RID: 7653
			// (set) Token: 0x0600395D RID: 14685 RVA: 0x00062448 File Offset: 0x00060648
			public virtual string Browser
			{
				set
				{
					base.PowerSharpParameters["Browser"] = value;
				}
			}

			// Token: 0x17001DE6 RID: 7654
			// (set) Token: 0x0600395E RID: 14686 RVA: 0x0006245B File Offset: 0x0006065B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001DE7 RID: 7655
			// (set) Token: 0x0600395F RID: 14687 RVA: 0x00062479 File Offset: 0x00060679
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001DE8 RID: 7656
			// (set) Token: 0x06003960 RID: 14688 RVA: 0x00062491 File Offset: 0x00060691
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001DE9 RID: 7657
			// (set) Token: 0x06003961 RID: 14689 RVA: 0x000624A9 File Offset: 0x000606A9
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001DEA RID: 7658
			// (set) Token: 0x06003962 RID: 14690 RVA: 0x000624C1 File Offset: 0x000606C1
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001DEB RID: 7659
			// (set) Token: 0x06003963 RID: 14691 RVA: 0x000624D4 File Offset: 0x000606D4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DEC RID: 7660
			// (set) Token: 0x06003964 RID: 14692 RVA: 0x000624EC File Offset: 0x000606EC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DED RID: 7661
			// (set) Token: 0x06003965 RID: 14693 RVA: 0x00062504 File Offset: 0x00060704
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DEE RID: 7662
			// (set) Token: 0x06003966 RID: 14694 RVA: 0x0006251C File Offset: 0x0006071C
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
