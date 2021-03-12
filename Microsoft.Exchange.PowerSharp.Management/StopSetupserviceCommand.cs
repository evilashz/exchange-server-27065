using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000D1 RID: 209
	public class StopSetupserviceCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06001CFA RID: 7418 RVA: 0x0003D57D File Offset: 0x0003B77D
		private StopSetupserviceCommand() : base("Stop-Setupservice")
		{
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0003D58A File Offset: 0x0003B78A
		public StopSetupserviceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x0003D599 File Offset: 0x0003B799
		public virtual StopSetupserviceCommand SetParameters(StopSetupserviceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000D2 RID: 210
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000711 RID: 1809
			// (set) Token: 0x06001CFD RID: 7421 RVA: 0x0003D5A3 File Offset: 0x0003B7A3
			public virtual string ServiceName
			{
				set
				{
					base.PowerSharpParameters["ServiceName"] = value;
				}
			}

			// Token: 0x17000712 RID: 1810
			// (set) Token: 0x06001CFE RID: 7422 RVA: 0x0003D5B6 File Offset: 0x0003B7B6
			public virtual bool IgnoreTimeout
			{
				set
				{
					base.PowerSharpParameters["IgnoreTimeout"] = value;
				}
			}

			// Token: 0x17000713 RID: 1811
			// (set) Token: 0x06001CFF RID: 7423 RVA: 0x0003D5CE File Offset: 0x0003B7CE
			public virtual string ServiceParameters
			{
				set
				{
					base.PowerSharpParameters["ServiceParameters"] = value;
				}
			}

			// Token: 0x17000714 RID: 1812
			// (set) Token: 0x06001D00 RID: 7424 RVA: 0x0003D5E1 File Offset: 0x0003B7E1
			public virtual bool FailIfServiceNotInstalled
			{
				set
				{
					base.PowerSharpParameters["FailIfServiceNotInstalled"] = value;
				}
			}

			// Token: 0x17000715 RID: 1813
			// (set) Token: 0x06001D01 RID: 7425 RVA: 0x0003D5F9 File Offset: 0x0003B7F9
			public virtual Unlimited<EnhancedTimeSpan> MaximumWaitTime
			{
				set
				{
					base.PowerSharpParameters["MaximumWaitTime"] = value;
				}
			}

			// Token: 0x17000716 RID: 1814
			// (set) Token: 0x06001D02 RID: 7426 RVA: 0x0003D611 File Offset: 0x0003B811
			public virtual Unlimited<EnhancedTimeSpan> MaxWaitTimeForRunningState
			{
				set
				{
					base.PowerSharpParameters["MaxWaitTimeForRunningState"] = value;
				}
			}

			// Token: 0x17000717 RID: 1815
			// (set) Token: 0x06001D03 RID: 7427 RVA: 0x0003D629 File Offset: 0x0003B829
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000718 RID: 1816
			// (set) Token: 0x06001D04 RID: 7428 RVA: 0x0003D641 File Offset: 0x0003B841
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000719 RID: 1817
			// (set) Token: 0x06001D05 RID: 7429 RVA: 0x0003D659 File Offset: 0x0003B859
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700071A RID: 1818
			// (set) Token: 0x06001D06 RID: 7430 RVA: 0x0003D671 File Offset: 0x0003B871
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
