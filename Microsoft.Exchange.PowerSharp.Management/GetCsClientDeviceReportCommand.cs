using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003A5 RID: 933
	public class GetCsClientDeviceReportCommand : SyntheticCommandWithPipelineInput<CsClientDeviceReport, CsClientDeviceReport>
	{
		// Token: 0x060039BE RID: 14782 RVA: 0x00062C12 File Offset: 0x00060E12
		private GetCsClientDeviceReportCommand() : base("Get-CsClientDeviceReport")
		{
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x00062C1F File Offset: 0x00060E1F
		public GetCsClientDeviceReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x00062C2E File Offset: 0x00060E2E
		public virtual GetCsClientDeviceReportCommand SetParameters(GetCsClientDeviceReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003A6 RID: 934
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E2D RID: 7725
			// (set) Token: 0x060039C1 RID: 14785 RVA: 0x00062C38 File Offset: 0x00060E38
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E2E RID: 7726
			// (set) Token: 0x060039C2 RID: 14786 RVA: 0x00062C56 File Offset: 0x00060E56
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E2F RID: 7727
			// (set) Token: 0x060039C3 RID: 14787 RVA: 0x00062C6E File Offset: 0x00060E6E
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E30 RID: 7728
			// (set) Token: 0x060039C4 RID: 14788 RVA: 0x00062C86 File Offset: 0x00060E86
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E31 RID: 7729
			// (set) Token: 0x060039C5 RID: 14789 RVA: 0x00062C9E File Offset: 0x00060E9E
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E32 RID: 7730
			// (set) Token: 0x060039C6 RID: 14790 RVA: 0x00062CB1 File Offset: 0x00060EB1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E33 RID: 7731
			// (set) Token: 0x060039C7 RID: 14791 RVA: 0x00062CC9 File Offset: 0x00060EC9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E34 RID: 7732
			// (set) Token: 0x060039C8 RID: 14792 RVA: 0x00062CE1 File Offset: 0x00060EE1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E35 RID: 7733
			// (set) Token: 0x060039C9 RID: 14793 RVA: 0x00062CF9 File Offset: 0x00060EF9
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
