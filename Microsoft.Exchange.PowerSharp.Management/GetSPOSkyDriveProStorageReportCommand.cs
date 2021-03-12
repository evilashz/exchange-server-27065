using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.ReportingTask.Common;
using Microsoft.Exchange.Management.ReportingTask.TenantReport;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003D3 RID: 979
	public class GetSPOSkyDriveProStorageReportCommand : SyntheticCommandWithPipelineInput<SPOSkyDriveProStorageReport, SPOSkyDriveProStorageReport>
	{
		// Token: 0x06003AF7 RID: 15095 RVA: 0x00064503 File Offset: 0x00062703
		private GetSPOSkyDriveProStorageReportCommand() : base("Get-SPOSkyDriveProStorageReport")
		{
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x00064510 File Offset: 0x00062710
		public GetSPOSkyDriveProStorageReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x0006451F File Offset: 0x0006271F
		public virtual GetSPOSkyDriveProStorageReportCommand SetParameters(GetSPOSkyDriveProStorageReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003D4 RID: 980
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001F0A RID: 7946
			// (set) Token: 0x06003AFA RID: 15098 RVA: 0x00064529 File Offset: 0x00062729
			public virtual ReportType ReportType
			{
				set
				{
					base.PowerSharpParameters["ReportType"] = value;
				}
			}

			// Token: 0x17001F0B RID: 7947
			// (set) Token: 0x06003AFB RID: 15099 RVA: 0x00064541 File Offset: 0x00062741
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001F0C RID: 7948
			// (set) Token: 0x06003AFC RID: 15100 RVA: 0x0006455F File Offset: 0x0006275F
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17001F0D RID: 7949
			// (set) Token: 0x06003AFD RID: 15101 RVA: 0x00064577 File Offset: 0x00062777
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17001F0E RID: 7950
			// (set) Token: 0x06003AFE RID: 15102 RVA: 0x0006458F File Offset: 0x0006278F
			public virtual Unlimited<int> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001F0F RID: 7951
			// (set) Token: 0x06003AFF RID: 15103 RVA: 0x000645A7 File Offset: 0x000627A7
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17001F10 RID: 7952
			// (set) Token: 0x06003B00 RID: 15104 RVA: 0x000645BA File Offset: 0x000627BA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001F11 RID: 7953
			// (set) Token: 0x06003B01 RID: 15105 RVA: 0x000645D2 File Offset: 0x000627D2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001F12 RID: 7954
			// (set) Token: 0x06003B02 RID: 15106 RVA: 0x000645EA File Offset: 0x000627EA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001F13 RID: 7955
			// (set) Token: 0x06003B03 RID: 15107 RVA: 0x00064602 File Offset: 0x00062802
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
