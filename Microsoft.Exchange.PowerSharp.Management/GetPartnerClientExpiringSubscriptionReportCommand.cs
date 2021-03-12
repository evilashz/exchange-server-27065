using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003BF RID: 959
	public class GetPartnerClientExpiringSubscriptionReportCommand : SyntheticCommandWithPipelineInput<PartnerClientExpiringSubscriptionReport, PartnerClientExpiringSubscriptionReport>
	{
		// Token: 0x06003A70 RID: 14960 RVA: 0x00063A45 File Offset: 0x00061C45
		private GetPartnerClientExpiringSubscriptionReportCommand() : base("Get-PartnerClientExpiringSubscriptionReport")
		{
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x00063A52 File Offset: 0x00061C52
		public GetPartnerClientExpiringSubscriptionReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x00063A61 File Offset: 0x00061C61
		public virtual GetPartnerClientExpiringSubscriptionReportCommand SetParameters(GetPartnerClientExpiringSubscriptionReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003C0 RID: 960
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EAB RID: 7851
			// (set) Token: 0x06003A73 RID: 14963 RVA: 0x00063A6B File Offset: 0x00061C6B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EAC RID: 7852
			// (set) Token: 0x06003A74 RID: 14964 RVA: 0x00063A89 File Offset: 0x00061C89
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001EAD RID: 7853
			// (set) Token: 0x06003A75 RID: 14965 RVA: 0x00063AA1 File Offset: 0x00061CA1
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001EAE RID: 7854
			// (set) Token: 0x06003A76 RID: 14966 RVA: 0x00063AB9 File Offset: 0x00061CB9
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001EAF RID: 7855
			// (set) Token: 0x06003A77 RID: 14967 RVA: 0x00063AD1 File Offset: 0x00061CD1
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001EB0 RID: 7856
			// (set) Token: 0x06003A78 RID: 14968 RVA: 0x00063AE4 File Offset: 0x00061CE4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001EB1 RID: 7857
			// (set) Token: 0x06003A79 RID: 14969 RVA: 0x00063AFC File Offset: 0x00061CFC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001EB2 RID: 7858
			// (set) Token: 0x06003A7A RID: 14970 RVA: 0x00063B14 File Offset: 0x00061D14
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001EB3 RID: 7859
			// (set) Token: 0x06003A7B RID: 14971 RVA: 0x00063B2C File Offset: 0x00061D2C
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
