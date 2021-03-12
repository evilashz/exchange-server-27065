using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003DD RID: 989
	public class GetStaleMailboxReportCommand : SyntheticCommandWithPipelineInput<StaleMailboxReport, StaleMailboxReport>
	{
		// Token: 0x06003B3C RID: 15164 RVA: 0x00064A86 File Offset: 0x00062C86
		private GetStaleMailboxReportCommand() : base("Get-StaleMailboxReport")
		{
		}

		// Token: 0x06003B3D RID: 15165 RVA: 0x00064A93 File Offset: 0x00062C93
		public GetStaleMailboxReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B3E RID: 15166 RVA: 0x00064AA2 File Offset: 0x00062CA2
		public virtual GetStaleMailboxReportCommand SetParameters(GetStaleMailboxReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003DE RID: 990
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F3B RID: 7995
			// (set) Token: 0x06003B3F RID: 15167 RVA: 0x00064AAC File Offset: 0x00062CAC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F3C RID: 7996
			// (set) Token: 0x06003B40 RID: 15168 RVA: 0x00064ACA File Offset: 0x00062CCA
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001F3D RID: 7997
			// (set) Token: 0x06003B41 RID: 15169 RVA: 0x00064AE2 File Offset: 0x00062CE2
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001F3E RID: 7998
			// (set) Token: 0x06003B42 RID: 15170 RVA: 0x00064AFA File Offset: 0x00062CFA
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F3F RID: 7999
			// (set) Token: 0x06003B43 RID: 15171 RVA: 0x00064B12 File Offset: 0x00062D12
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001F40 RID: 8000
			// (set) Token: 0x06003B44 RID: 15172 RVA: 0x00064B25 File Offset: 0x00062D25
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F41 RID: 8001
			// (set) Token: 0x06003B45 RID: 15173 RVA: 0x00064B3D File Offset: 0x00062D3D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F42 RID: 8002
			// (set) Token: 0x06003B46 RID: 15174 RVA: 0x00064B55 File Offset: 0x00062D55
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F43 RID: 8003
			// (set) Token: 0x06003B47 RID: 15175 RVA: 0x00064B6D File Offset: 0x00062D6D
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
