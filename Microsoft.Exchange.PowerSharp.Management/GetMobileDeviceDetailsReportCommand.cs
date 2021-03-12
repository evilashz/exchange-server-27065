using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200016B RID: 363
	public class GetMobileDeviceDetailsReportCommand : SyntheticCommandWithPipelineInput<MobileDeviceDetailsReport, MobileDeviceDetailsReport>
	{
		// Token: 0x06002252 RID: 8786 RVA: 0x0004414C File Offset: 0x0004234C
		private GetMobileDeviceDetailsReportCommand() : base("Get-MobileDeviceDetailsReport")
		{
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00044159 File Offset: 0x00042359
		public GetMobileDeviceDetailsReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x00044168 File Offset: 0x00042368
		public virtual GetMobileDeviceDetailsReportCommand SetParameters(GetMobileDeviceDetailsReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200016C RID: 364
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B35 RID: 2869
			// (set) Token: 0x06002255 RID: 8789 RVA: 0x00044172 File Offset: 0x00042372
			public virtual DateTime? StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x17000B36 RID: 2870
			// (set) Token: 0x06002256 RID: 8790 RVA: 0x0004418A File Offset: 0x0004238A
			public virtual DateTime? EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x17000B37 RID: 2871
			// (set) Token: 0x06002257 RID: 8791 RVA: 0x000441A2 File Offset: 0x000423A2
			public virtual int Page
			{
				set
				{
					base.PowerSharpParameters["Page"] = value;
				}
			}

			// Token: 0x17000B38 RID: 2872
			// (set) Token: 0x06002258 RID: 8792 RVA: 0x000441BA File Offset: 0x000423BA
			public virtual int PageSize
			{
				set
				{
					base.PowerSharpParameters["PageSize"] = value;
				}
			}

			// Token: 0x17000B39 RID: 2873
			// (set) Token: 0x06002259 RID: 8793 RVA: 0x000441D2 File Offset: 0x000423D2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B3A RID: 2874
			// (set) Token: 0x0600225A RID: 8794 RVA: 0x000441F0 File Offset: 0x000423F0
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000B3B RID: 2875
			// (set) Token: 0x0600225B RID: 8795 RVA: 0x00044203 File Offset: 0x00042403
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000B3C RID: 2876
			// (set) Token: 0x0600225C RID: 8796 RVA: 0x00044216 File Offset: 0x00042416
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B3D RID: 2877
			// (set) Token: 0x0600225D RID: 8797 RVA: 0x0004422E File Offset: 0x0004242E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B3E RID: 2878
			// (set) Token: 0x0600225E RID: 8798 RVA: 0x00044246 File Offset: 0x00042446
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B3F RID: 2879
			// (set) Token: 0x0600225F RID: 8799 RVA: 0x0004425E File Offset: 0x0004245E
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
