using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000399 RID: 921
	public class GetO365ClientOSDetailReportCommand : SyntheticCommandWithPipelineInput<ClientSoftwareOSDetailReport, ClientSoftwareOSDetailReport>
	{
		// Token: 0x06003968 RID: 14696 RVA: 0x0006253C File Offset: 0x0006073C
		private GetO365ClientOSDetailReportCommand() : base("Get-O365ClientOSDetailReport")
		{
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x00062549 File Offset: 0x00060749
		public GetO365ClientOSDetailReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x00062558 File Offset: 0x00060758
		public virtual GetO365ClientOSDetailReportCommand SetParameters(GetO365ClientOSDetailReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200039A RID: 922
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001DEF RID: 7663
			// (set) Token: 0x0600396B RID: 14699 RVA: 0x00062562 File Offset: 0x00060762
			public virtual string OperatingSystem
			{
				set
				{
					base.PowerSharpParameters["OperatingSystem"] = value;
				}
			}

			// Token: 0x17001DF0 RID: 7664
			// (set) Token: 0x0600396C RID: 14700 RVA: 0x00062575 File Offset: 0x00060775
			public virtual string OperatingSystemVersion
			{
				set
				{
					base.PowerSharpParameters["OperatingSystemVersion"] = value;
				}
			}

			// Token: 0x17001DF1 RID: 7665
			// (set) Token: 0x0600396D RID: 14701 RVA: 0x00062588 File Offset: 0x00060788
			public virtual string WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x17001DF2 RID: 7666
			// (set) Token: 0x0600396E RID: 14702 RVA: 0x0006259B File Offset: 0x0006079B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001DF3 RID: 7667
			// (set) Token: 0x0600396F RID: 14703 RVA: 0x000625B9 File Offset: 0x000607B9
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001DF4 RID: 7668
			// (set) Token: 0x06003970 RID: 14704 RVA: 0x000625D1 File Offset: 0x000607D1
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001DF5 RID: 7669
			// (set) Token: 0x06003971 RID: 14705 RVA: 0x000625E9 File Offset: 0x000607E9
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001DF6 RID: 7670
			// (set) Token: 0x06003972 RID: 14706 RVA: 0x00062601 File Offset: 0x00060801
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001DF7 RID: 7671
			// (set) Token: 0x06003973 RID: 14707 RVA: 0x00062614 File Offset: 0x00060814
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001DF8 RID: 7672
			// (set) Token: 0x06003974 RID: 14708 RVA: 0x0006262C File Offset: 0x0006082C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001DF9 RID: 7673
			// (set) Token: 0x06003975 RID: 14709 RVA: 0x00062644 File Offset: 0x00060844
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001DFA RID: 7674
			// (set) Token: 0x06003976 RID: 14710 RVA: 0x0006265C File Offset: 0x0006085C
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
