using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003C5 RID: 965
	public class GetScorecardClientOSReportCommand : SyntheticCommandWithPipelineInput<ScorecardClientOSReport, ScorecardClientOSReport>
	{
		// Token: 0x06003A98 RID: 15000 RVA: 0x00063D72 File Offset: 0x00061F72
		private GetScorecardClientOSReportCommand() : base("Get-ScorecardClientOSReport")
		{
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x00063D7F File Offset: 0x00061F7F
		public GetScorecardClientOSReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x00063D8E File Offset: 0x00061F8E
		public virtual GetScorecardClientOSReportCommand SetParameters(GetScorecardClientOSReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003C6 RID: 966
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001EC7 RID: 7879
			// (set) Token: 0x06003A9B RID: 15003 RVA: 0x00063D98 File Offset: 0x00061F98
			public virtual DataCategory Category
			{
				set
				{
					base.PowerSharpParameters["Category"] = value;
				}
			}

			// Token: 0x17001EC8 RID: 7880
			// (set) Token: 0x06003A9C RID: 15004 RVA: 0x00063DB0 File Offset: 0x00061FB0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001EC9 RID: 7881
			// (set) Token: 0x06003A9D RID: 15005 RVA: 0x00063DCE File Offset: 0x00061FCE
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001ECA RID: 7882
			// (set) Token: 0x06003A9E RID: 15006 RVA: 0x00063DE6 File Offset: 0x00061FE6
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001ECB RID: 7883
			// (set) Token: 0x06003A9F RID: 15007 RVA: 0x00063DFE File Offset: 0x00061FFE
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001ECC RID: 7884
			// (set) Token: 0x06003AA0 RID: 15008 RVA: 0x00063E16 File Offset: 0x00062016
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001ECD RID: 7885
			// (set) Token: 0x06003AA1 RID: 15009 RVA: 0x00063E29 File Offset: 0x00062029
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001ECE RID: 7886
			// (set) Token: 0x06003AA2 RID: 15010 RVA: 0x00063E41 File Offset: 0x00062041
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001ECF RID: 7887
			// (set) Token: 0x06003AA3 RID: 15011 RVA: 0x00063E59 File Offset: 0x00062059
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001ED0 RID: 7888
			// (set) Token: 0x06003AA4 RID: 15012 RVA: 0x00063E71 File Offset: 0x00062071
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
