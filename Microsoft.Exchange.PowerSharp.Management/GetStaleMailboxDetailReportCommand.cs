using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003DB RID: 987
	public class GetStaleMailboxDetailReportCommand : SyntheticCommandWithPipelineInput<StaleMailboxDetailReport, StaleMailboxDetailReport>
	{
		// Token: 0x06003B2F RID: 15151 RVA: 0x0006497F File Offset: 0x00062B7F
		private GetStaleMailboxDetailReportCommand() : base("Get-StaleMailboxDetailReport")
		{
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x0006498C File Offset: 0x00062B8C
		public GetStaleMailboxDetailReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x0006499B File Offset: 0x00062B9B
		public virtual GetStaleMailboxDetailReportCommand SetParameters(GetStaleMailboxDetailReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003DC RID: 988
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F32 RID: 7986
			// (set) Token: 0x06003B32 RID: 15154 RVA: 0x000649A5 File Offset: 0x00062BA5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F33 RID: 7987
			// (set) Token: 0x06003B33 RID: 15155 RVA: 0x000649C3 File Offset: 0x00062BC3
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001F34 RID: 7988
			// (set) Token: 0x06003B34 RID: 15156 RVA: 0x000649DB File Offset: 0x00062BDB
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001F35 RID: 7989
			// (set) Token: 0x06003B35 RID: 15157 RVA: 0x000649F3 File Offset: 0x00062BF3
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F36 RID: 7990
			// (set) Token: 0x06003B36 RID: 15158 RVA: 0x00064A0B File Offset: 0x00062C0B
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001F37 RID: 7991
			// (set) Token: 0x06003B37 RID: 15159 RVA: 0x00064A1E File Offset: 0x00062C1E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F38 RID: 7992
			// (set) Token: 0x06003B38 RID: 15160 RVA: 0x00064A36 File Offset: 0x00062C36
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F39 RID: 7993
			// (set) Token: 0x06003B39 RID: 15161 RVA: 0x00064A4E File Offset: 0x00062C4E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F3A RID: 7994
			// (set) Token: 0x06003B3A RID: 15162 RVA: 0x00064A66 File Offset: 0x00062C66
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
