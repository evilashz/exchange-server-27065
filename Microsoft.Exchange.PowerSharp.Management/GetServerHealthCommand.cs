using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Office.Datacenter.ActiveMonitoring.Management.Common;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002BD RID: 701
	public class GetServerHealthCommand : SyntheticCommandWithPipelineInput<MonitorHealthEntry, MonitorHealthEntry>
	{
		// Token: 0x0600311B RID: 12571 RVA: 0x00057AC7 File Offset: 0x00055CC7
		private GetServerHealthCommand() : base("Get-ServerHealth")
		{
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x00057AD4 File Offset: 0x00055CD4
		public GetServerHealthCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x00057AE3 File Offset: 0x00055CE3
		public virtual GetServerHealthCommand SetParameters(GetServerHealthCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002BE RID: 702
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700175A RID: 5978
			// (set) Token: 0x0600311E RID: 12574 RVA: 0x00057AED File Offset: 0x00055CED
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700175B RID: 5979
			// (set) Token: 0x0600311F RID: 12575 RVA: 0x00057B00 File Offset: 0x00055D00
			public virtual string HealthSet
			{
				set
				{
					base.PowerSharpParameters["HealthSet"] = value;
				}
			}

			// Token: 0x1700175C RID: 5980
			// (set) Token: 0x06003120 RID: 12576 RVA: 0x00057B13 File Offset: 0x00055D13
			public virtual SwitchParameter HaImpactingOnly
			{
				set
				{
					base.PowerSharpParameters["HaImpactingOnly"] = value;
				}
			}

			// Token: 0x1700175D RID: 5981
			// (set) Token: 0x06003121 RID: 12577 RVA: 0x00057B2B File Offset: 0x00055D2B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700175E RID: 5982
			// (set) Token: 0x06003122 RID: 12578 RVA: 0x00057B43 File Offset: 0x00055D43
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700175F RID: 5983
			// (set) Token: 0x06003123 RID: 12579 RVA: 0x00057B5B File Offset: 0x00055D5B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001760 RID: 5984
			// (set) Token: 0x06003124 RID: 12580 RVA: 0x00057B73 File Offset: 0x00055D73
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
