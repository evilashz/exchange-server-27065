using System;
using System.Linq.Expressions;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.FfoReporting;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200016D RID: 365
	public class GetMxRecordReportCommand : SyntheticCommandWithPipelineInput<MxRecordReport, MxRecordReport>
	{
		// Token: 0x06002261 RID: 8801 RVA: 0x0004427E File Offset: 0x0004247E
		private GetMxRecordReportCommand() : base("Get-MxRecordReport")
		{
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0004428B File Offset: 0x0004248B
		public GetMxRecordReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0004429A File Offset: 0x0004249A
		public virtual GetMxRecordReportCommand SetParameters(GetMxRecordReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200016E RID: 366
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000B40 RID: 2880
			// (set) Token: 0x06002264 RID: 8804 RVA: 0x000442A4 File Offset: 0x000424A4
			public virtual Fqdn Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17000B41 RID: 2881
			// (set) Token: 0x06002265 RID: 8805 RVA: 0x000442B7 File Offset: 0x000424B7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000B42 RID: 2882
			// (set) Token: 0x06002266 RID: 8806 RVA: 0x000442D5 File Offset: 0x000424D5
			public virtual Expression Expression
			{
				set
				{
					base.PowerSharpParameters["Expression"] = value;
				}
			}

			// Token: 0x17000B43 RID: 2883
			// (set) Token: 0x06002267 RID: 8807 RVA: 0x000442E8 File Offset: 0x000424E8
			public virtual string ProbeTag
			{
				set
				{
					base.PowerSharpParameters["ProbeTag"] = value;
				}
			}

			// Token: 0x17000B44 RID: 2884
			// (set) Token: 0x06002268 RID: 8808 RVA: 0x000442FB File Offset: 0x000424FB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000B45 RID: 2885
			// (set) Token: 0x06002269 RID: 8809 RVA: 0x00044313 File Offset: 0x00042513
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000B46 RID: 2886
			// (set) Token: 0x0600226A RID: 8810 RVA: 0x0004432B File Offset: 0x0004252B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000B47 RID: 2887
			// (set) Token: 0x0600226B RID: 8811 RVA: 0x00044343 File Offset: 0x00042543
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
