using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000CF RID: 207
	public class StartSetupserviceCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06001CEC RID: 7404 RVA: 0x0003D469 File Offset: 0x0003B669
		private StartSetupserviceCommand() : base("Start-Setupservice")
		{
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0003D476 File Offset: 0x0003B676
		public StartSetupserviceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0003D485 File Offset: 0x0003B685
		public virtual StartSetupserviceCommand SetParameters(StartSetupserviceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000D0 RID: 208
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000707 RID: 1799
			// (set) Token: 0x06001CEF RID: 7407 RVA: 0x0003D48F File Offset: 0x0003B68F
			public virtual string ServiceName
			{
				set
				{
					base.PowerSharpParameters["ServiceName"] = value;
				}
			}

			// Token: 0x17000708 RID: 1800
			// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x0003D4A2 File Offset: 0x0003B6A2
			public virtual bool IgnoreTimeout
			{
				set
				{
					base.PowerSharpParameters["IgnoreTimeout"] = value;
				}
			}

			// Token: 0x17000709 RID: 1801
			// (set) Token: 0x06001CF1 RID: 7409 RVA: 0x0003D4BA File Offset: 0x0003B6BA
			public virtual string ServiceParameters
			{
				set
				{
					base.PowerSharpParameters["ServiceParameters"] = value;
				}
			}

			// Token: 0x1700070A RID: 1802
			// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x0003D4CD File Offset: 0x0003B6CD
			public virtual bool FailIfServiceNotInstalled
			{
				set
				{
					base.PowerSharpParameters["FailIfServiceNotInstalled"] = value;
				}
			}

			// Token: 0x1700070B RID: 1803
			// (set) Token: 0x06001CF3 RID: 7411 RVA: 0x0003D4E5 File Offset: 0x0003B6E5
			public virtual Unlimited<EnhancedTimeSpan> MaximumWaitTime
			{
				set
				{
					base.PowerSharpParameters["MaximumWaitTime"] = value;
				}
			}

			// Token: 0x1700070C RID: 1804
			// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x0003D4FD File Offset: 0x0003B6FD
			public virtual Unlimited<EnhancedTimeSpan> MaxWaitTimeForRunningState
			{
				set
				{
					base.PowerSharpParameters["MaxWaitTimeForRunningState"] = value;
				}
			}

			// Token: 0x1700070D RID: 1805
			// (set) Token: 0x06001CF5 RID: 7413 RVA: 0x0003D515 File Offset: 0x0003B715
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700070E RID: 1806
			// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x0003D52D File Offset: 0x0003B72D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700070F RID: 1807
			// (set) Token: 0x06001CF7 RID: 7415 RVA: 0x0003D545 File Offset: 0x0003B745
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000710 RID: 1808
			// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x0003D55D File Offset: 0x0003B75D
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
