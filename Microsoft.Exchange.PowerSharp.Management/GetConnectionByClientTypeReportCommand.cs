using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200039F RID: 927
	public class GetConnectionByClientTypeReportCommand : SyntheticCommandWithPipelineInput<ConnectionByClientTypeReport, ConnectionByClientTypeReport>
	{
		// Token: 0x06003994 RID: 14740 RVA: 0x000628B5 File Offset: 0x00060AB5
		private GetConnectionByClientTypeReportCommand() : base("Get-ConnectionByClientTypeReport")
		{
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000628C2 File Offset: 0x00060AC2
		public GetConnectionByClientTypeReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000628D1 File Offset: 0x00060AD1
		public virtual GetConnectionByClientTypeReportCommand SetParameters(GetConnectionByClientTypeReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003A0 RID: 928
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001E0F RID: 7695
			// (set) Token: 0x06003997 RID: 14743 RVA: 0x000628DB File Offset: 0x00060ADB
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001E10 RID: 7696
			// (set) Token: 0x06003998 RID: 14744 RVA: 0x000628F3 File Offset: 0x00060AF3
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001E11 RID: 7697
			// (set) Token: 0x06003999 RID: 14745 RVA: 0x00062911 File Offset: 0x00060B11
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001E12 RID: 7698
			// (set) Token: 0x0600399A RID: 14746 RVA: 0x00062929 File Offset: 0x00060B29
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001E13 RID: 7699
			// (set) Token: 0x0600399B RID: 14747 RVA: 0x00062941 File Offset: 0x00060B41
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001E14 RID: 7700
			// (set) Token: 0x0600399C RID: 14748 RVA: 0x00062959 File Offset: 0x00060B59
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001E15 RID: 7701
			// (set) Token: 0x0600399D RID: 14749 RVA: 0x0006296C File Offset: 0x00060B6C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001E16 RID: 7702
			// (set) Token: 0x0600399E RID: 14750 RVA: 0x00062984 File Offset: 0x00060B84
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001E17 RID: 7703
			// (set) Token: 0x0600399F RID: 14751 RVA: 0x0006299C File Offset: 0x00060B9C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001E18 RID: 7704
			// (set) Token: 0x060039A0 RID: 14752 RVA: 0x000629B4 File Offset: 0x00060BB4
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
