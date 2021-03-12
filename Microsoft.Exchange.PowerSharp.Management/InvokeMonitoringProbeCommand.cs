using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020002C7 RID: 711
	public class InvokeMonitoringProbeCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06003161 RID: 12641 RVA: 0x00058020 File Offset: 0x00056220
		private InvokeMonitoringProbeCommand() : base("Invoke-MonitoringProbe")
		{
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x0005802D File Offset: 0x0005622D
		public InvokeMonitoringProbeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x0005803C File Offset: 0x0005623C
		public virtual InvokeMonitoringProbeCommand SetParameters(InvokeMonitoringProbeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020002C8 RID: 712
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700178C RID: 6028
			// (set) Token: 0x06003164 RID: 12644 RVA: 0x00058046 File Offset: 0x00056246
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700178D RID: 6029
			// (set) Token: 0x06003165 RID: 12645 RVA: 0x00058059 File Offset: 0x00056259
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700178E RID: 6030
			// (set) Token: 0x06003166 RID: 12646 RVA: 0x0005806C File Offset: 0x0005626C
			public virtual string ItemTargetExtension
			{
				set
				{
					base.PowerSharpParameters["ItemTargetExtension"] = value;
				}
			}

			// Token: 0x1700178F RID: 6031
			// (set) Token: 0x06003167 RID: 12647 RVA: 0x0005807F File Offset: 0x0005627F
			public virtual string Account
			{
				set
				{
					base.PowerSharpParameters["Account"] = value;
				}
			}

			// Token: 0x17001790 RID: 6032
			// (set) Token: 0x06003168 RID: 12648 RVA: 0x00058092 File Offset: 0x00056292
			public virtual string Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17001791 RID: 6033
			// (set) Token: 0x06003169 RID: 12649 RVA: 0x000580A5 File Offset: 0x000562A5
			public virtual string Endpoint
			{
				set
				{
					base.PowerSharpParameters["Endpoint"] = value;
				}
			}

			// Token: 0x17001792 RID: 6034
			// (set) Token: 0x0600316A RID: 12650 RVA: 0x000580B8 File Offset: 0x000562B8
			public virtual string SecondaryAccount
			{
				set
				{
					base.PowerSharpParameters["SecondaryAccount"] = value;
				}
			}

			// Token: 0x17001793 RID: 6035
			// (set) Token: 0x0600316B RID: 12651 RVA: 0x000580CB File Offset: 0x000562CB
			public virtual string SecondaryPassword
			{
				set
				{
					base.PowerSharpParameters["SecondaryPassword"] = value;
				}
			}

			// Token: 0x17001794 RID: 6036
			// (set) Token: 0x0600316C RID: 12652 RVA: 0x000580DE File Offset: 0x000562DE
			public virtual string SecondaryEndpoint
			{
				set
				{
					base.PowerSharpParameters["SecondaryEndpoint"] = value;
				}
			}

			// Token: 0x17001795 RID: 6037
			// (set) Token: 0x0600316D RID: 12653 RVA: 0x000580F1 File Offset: 0x000562F1
			public virtual string TimeOutSeconds
			{
				set
				{
					base.PowerSharpParameters["TimeOutSeconds"] = value;
				}
			}

			// Token: 0x17001796 RID: 6038
			// (set) Token: 0x0600316E RID: 12654 RVA: 0x00058104 File Offset: 0x00056304
			public virtual string PropertyOverride
			{
				set
				{
					base.PowerSharpParameters["PropertyOverride"] = value;
				}
			}

			// Token: 0x17001797 RID: 6039
			// (set) Token: 0x0600316F RID: 12655 RVA: 0x00058117 File Offset: 0x00056317
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001798 RID: 6040
			// (set) Token: 0x06003170 RID: 12656 RVA: 0x0005812F File Offset: 0x0005632F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001799 RID: 6041
			// (set) Token: 0x06003171 RID: 12657 RVA: 0x00058147 File Offset: 0x00056347
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700179A RID: 6042
			// (set) Token: 0x06003172 RID: 12658 RVA: 0x0005815F File Offset: 0x0005635F
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
