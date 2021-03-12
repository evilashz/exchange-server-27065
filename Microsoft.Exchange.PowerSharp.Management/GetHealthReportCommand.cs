using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002A9 RID: 681
	public class GetHealthReportCommand : SyntheticCommandWithPipelineInput<ConsolidatedHealth, ConsolidatedHealth>
	{
		// Token: 0x06003072 RID: 12402 RVA: 0x00056D7B File Offset: 0x00054F7B
		private GetHealthReportCommand() : base("Get-HealthReport")
		{
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x00056D88 File Offset: 0x00054F88
		public GetHealthReportCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x00056D97 File Offset: 0x00054F97
		public virtual GetHealthReportCommand SetParameters(GetHealthReportCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002AA RID: 682
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170016D9 RID: 5849
			// (set) Token: 0x06003075 RID: 12405 RVA: 0x00056DA1 File Offset: 0x00054FA1
			public virtual SwitchParameter RollupGroup
			{
				set
				{
					base.PowerSharpParameters["RollupGroup"] = value;
				}
			}

			// Token: 0x170016DA RID: 5850
			// (set) Token: 0x06003076 RID: 12406 RVA: 0x00056DB9 File Offset: 0x00054FB9
			public virtual int GroupSize
			{
				set
				{
					base.PowerSharpParameters["GroupSize"] = value;
				}
			}

			// Token: 0x170016DB RID: 5851
			// (set) Token: 0x06003077 RID: 12407 RVA: 0x00056DD1 File Offset: 0x00054FD1
			public virtual int MinimumOnlinePercent
			{
				set
				{
					base.PowerSharpParameters["MinimumOnlinePercent"] = value;
				}
			}

			// Token: 0x170016DC RID: 5852
			// (set) Token: 0x06003078 RID: 12408 RVA: 0x00056DE9 File Offset: 0x00054FE9
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170016DD RID: 5853
			// (set) Token: 0x06003079 RID: 12409 RVA: 0x00056DFC File Offset: 0x00054FFC
			public virtual string HealthSet
			{
				set
				{
					base.PowerSharpParameters["HealthSet"] = value;
				}
			}

			// Token: 0x170016DE RID: 5854
			// (set) Token: 0x0600307A RID: 12410 RVA: 0x00056E0F File Offset: 0x0005500F
			public virtual SwitchParameter HaImpactingOnly
			{
				set
				{
					base.PowerSharpParameters["HaImpactingOnly"] = value;
				}
			}

			// Token: 0x170016DF RID: 5855
			// (set) Token: 0x0600307B RID: 12411 RVA: 0x00056E27 File Offset: 0x00055027
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170016E0 RID: 5856
			// (set) Token: 0x0600307C RID: 12412 RVA: 0x00056E3F File Offset: 0x0005503F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170016E1 RID: 5857
			// (set) Token: 0x0600307D RID: 12413 RVA: 0x00056E57 File Offset: 0x00055057
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170016E2 RID: 5858
			// (set) Token: 0x0600307E RID: 12414 RVA: 0x00056E6F File Offset: 0x0005506F
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
