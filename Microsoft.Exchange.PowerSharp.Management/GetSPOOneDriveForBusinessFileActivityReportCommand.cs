using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003CD RID: 973
	public class GetSPOOneDriveForBusinessFileActivityReportCommand : SyntheticCommandWithPipelineInput<SPOODBFileActivityReport, SPOODBFileActivityReport>
	{
		// Token: 0x06003ACF RID: 15055 RVA: 0x000641D6 File Offset: 0x000623D6
		private GetSPOOneDriveForBusinessFileActivityReportCommand() : base("Get-SPOOneDriveForBusinessFileActivityReport")
		{
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000641E3 File Offset: 0x000623E3
		public GetSPOOneDriveForBusinessFileActivityReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x000641F2 File Offset: 0x000623F2
		public virtual GetSPOOneDriveForBusinessFileActivityReportCommand SetParameters(GetSPOOneDriveForBusinessFileActivityReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003CE RID: 974
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EEE RID: 7918
			// (set) Token: 0x06003AD2 RID: 15058 RVA: 0x000641FC File Offset: 0x000623FC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EEF RID: 7919
			// (set) Token: 0x06003AD3 RID: 15059 RVA: 0x0006421A File Offset: 0x0006241A
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001EF0 RID: 7920
			// (set) Token: 0x06003AD4 RID: 15060 RVA: 0x00064232 File Offset: 0x00062432
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001EF1 RID: 7921
			// (set) Token: 0x06003AD5 RID: 15061 RVA: 0x0006424A File Offset: 0x0006244A
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001EF2 RID: 7922
			// (set) Token: 0x06003AD6 RID: 15062 RVA: 0x00064262 File Offset: 0x00062462
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001EF3 RID: 7923
			// (set) Token: 0x06003AD7 RID: 15063 RVA: 0x00064275 File Offset: 0x00062475
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001EF4 RID: 7924
			// (set) Token: 0x06003AD8 RID: 15064 RVA: 0x0006428D File Offset: 0x0006248D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EF5 RID: 7925
			// (set) Token: 0x06003AD9 RID: 15065 RVA: 0x000642A5 File Offset: 0x000624A5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EF6 RID: 7926
			// (set) Token: 0x06003ADA RID: 15066 RVA: 0x000642BD File Offset: 0x000624BD
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
