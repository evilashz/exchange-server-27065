using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003A3 RID: 931
	public class GetCsAVConferenceTimeReportCommand : SyntheticCommandWithPipelineInput<CsAVConferenceTimeReport, CsAVConferenceTimeReport>
	{
		// Token: 0x060039B0 RID: 14768 RVA: 0x00062AF3 File Offset: 0x00060CF3
		private GetCsAVConferenceTimeReportCommand() : base("Get-CsAVConferenceTimeReport")
		{
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x00062B00 File Offset: 0x00060D00
		public GetCsAVConferenceTimeReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060039B2 RID: 14770 RVA: 0x00062B0F File Offset: 0x00060D0F
		public virtual GetCsAVConferenceTimeReportCommand SetParameters(GetCsAVConferenceTimeReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003A4 RID: 932
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E23 RID: 7715
			// (set) Token: 0x060039B3 RID: 14771 RVA: 0x00062B19 File Offset: 0x00060D19
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E24 RID: 7716
			// (set) Token: 0x060039B4 RID: 14772 RVA: 0x00062B31 File Offset: 0x00060D31
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E25 RID: 7717
			// (set) Token: 0x060039B5 RID: 14773 RVA: 0x00062B4F File Offset: 0x00060D4F
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E26 RID: 7718
			// (set) Token: 0x060039B6 RID: 14774 RVA: 0x00062B67 File Offset: 0x00060D67
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E27 RID: 7719
			// (set) Token: 0x060039B7 RID: 14775 RVA: 0x00062B7F File Offset: 0x00060D7F
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E28 RID: 7720
			// (set) Token: 0x060039B8 RID: 14776 RVA: 0x00062B97 File Offset: 0x00060D97
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E29 RID: 7721
			// (set) Token: 0x060039B9 RID: 14777 RVA: 0x00062BAA File Offset: 0x00060DAA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E2A RID: 7722
			// (set) Token: 0x060039BA RID: 14778 RVA: 0x00062BC2 File Offset: 0x00060DC2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E2B RID: 7723
			// (set) Token: 0x060039BB RID: 14779 RVA: 0x00062BDA File Offset: 0x00060DDA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E2C RID: 7724
			// (set) Token: 0x060039BC RID: 14780 RVA: 0x00062BF2 File Offset: 0x00060DF2
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
