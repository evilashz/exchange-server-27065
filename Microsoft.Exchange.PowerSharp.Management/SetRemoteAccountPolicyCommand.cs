using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200085A RID: 2138
	public class SetRemoteAccountPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<RemoteAccountPolicy>
	{
		// Token: 0x06006A37 RID: 27191 RVA: 0x000A13E5 File Offset: 0x0009F5E5
		private SetRemoteAccountPolicyCommand() : base("Set-RemoteAccountPolicy")
		{
		}

		// Token: 0x06006A38 RID: 27192 RVA: 0x000A13F2 File Offset: 0x0009F5F2
		public SetRemoteAccountPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006A39 RID: 27193 RVA: 0x000A1401 File Offset: 0x0009F601
		public virtual SetRemoteAccountPolicyCommand SetParameters(SetRemoteAccountPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006A3A RID: 27194 RVA: 0x000A140B File Offset: 0x0009F60B
		public virtual SetRemoteAccountPolicyCommand SetParameters(SetRemoteAccountPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200085B RID: 2139
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700453C RID: 17724
			// (set) Token: 0x06006A3B RID: 27195 RVA: 0x000A1415 File Offset: 0x0009F615
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700453D RID: 17725
			// (set) Token: 0x06006A3C RID: 27196 RVA: 0x000A1428 File Offset: 0x0009F628
			public virtual EnhancedTimeSpan PollingInterval
			{
				set
				{
					base.PowerSharpParameters["PollingInterval"] = value;
				}
			}

			// Token: 0x1700453E RID: 17726
			// (set) Token: 0x06006A3D RID: 27197 RVA: 0x000A1440 File Offset: 0x0009F640
			public virtual EnhancedTimeSpan TimeBeforeInactive
			{
				set
				{
					base.PowerSharpParameters["TimeBeforeInactive"] = value;
				}
			}

			// Token: 0x1700453F RID: 17727
			// (set) Token: 0x06006A3E RID: 27198 RVA: 0x000A1458 File Offset: 0x0009F658
			public virtual EnhancedTimeSpan TimeBeforeDormant
			{
				set
				{
					base.PowerSharpParameters["TimeBeforeDormant"] = value;
				}
			}

			// Token: 0x17004540 RID: 17728
			// (set) Token: 0x06006A3F RID: 27199 RVA: 0x000A1470 File Offset: 0x0009F670
			public virtual int MaxSyncAccounts
			{
				set
				{
					base.PowerSharpParameters["MaxSyncAccounts"] = value;
				}
			}

			// Token: 0x17004541 RID: 17729
			// (set) Token: 0x06006A40 RID: 27200 RVA: 0x000A1488 File Offset: 0x0009F688
			public virtual bool SyncNowAllowed
			{
				set
				{
					base.PowerSharpParameters["SyncNowAllowed"] = value;
				}
			}

			// Token: 0x17004542 RID: 17730
			// (set) Token: 0x06006A41 RID: 27201 RVA: 0x000A14A0 File Offset: 0x0009F6A0
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004543 RID: 17731
			// (set) Token: 0x06006A42 RID: 27202 RVA: 0x000A14B3 File Offset: 0x0009F6B3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004544 RID: 17732
			// (set) Token: 0x06006A43 RID: 27203 RVA: 0x000A14CB File Offset: 0x0009F6CB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004545 RID: 17733
			// (set) Token: 0x06006A44 RID: 27204 RVA: 0x000A14E3 File Offset: 0x0009F6E3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004546 RID: 17734
			// (set) Token: 0x06006A45 RID: 27205 RVA: 0x000A14FB File Offset: 0x0009F6FB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004547 RID: 17735
			// (set) Token: 0x06006A46 RID: 27206 RVA: 0x000A1513 File Offset: 0x0009F713
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200085C RID: 2140
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004548 RID: 17736
			// (set) Token: 0x06006A48 RID: 27208 RVA: 0x000A1533 File Offset: 0x0009F733
			public virtual RemoteAccountPolicyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004549 RID: 17737
			// (set) Token: 0x06006A49 RID: 27209 RVA: 0x000A1546 File Offset: 0x0009F746
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700454A RID: 17738
			// (set) Token: 0x06006A4A RID: 27210 RVA: 0x000A1559 File Offset: 0x0009F759
			public virtual EnhancedTimeSpan PollingInterval
			{
				set
				{
					base.PowerSharpParameters["PollingInterval"] = value;
				}
			}

			// Token: 0x1700454B RID: 17739
			// (set) Token: 0x06006A4B RID: 27211 RVA: 0x000A1571 File Offset: 0x0009F771
			public virtual EnhancedTimeSpan TimeBeforeInactive
			{
				set
				{
					base.PowerSharpParameters["TimeBeforeInactive"] = value;
				}
			}

			// Token: 0x1700454C RID: 17740
			// (set) Token: 0x06006A4C RID: 27212 RVA: 0x000A1589 File Offset: 0x0009F789
			public virtual EnhancedTimeSpan TimeBeforeDormant
			{
				set
				{
					base.PowerSharpParameters["TimeBeforeDormant"] = value;
				}
			}

			// Token: 0x1700454D RID: 17741
			// (set) Token: 0x06006A4D RID: 27213 RVA: 0x000A15A1 File Offset: 0x0009F7A1
			public virtual int MaxSyncAccounts
			{
				set
				{
					base.PowerSharpParameters["MaxSyncAccounts"] = value;
				}
			}

			// Token: 0x1700454E RID: 17742
			// (set) Token: 0x06006A4E RID: 27214 RVA: 0x000A15B9 File Offset: 0x0009F7B9
			public virtual bool SyncNowAllowed
			{
				set
				{
					base.PowerSharpParameters["SyncNowAllowed"] = value;
				}
			}

			// Token: 0x1700454F RID: 17743
			// (set) Token: 0x06006A4F RID: 27215 RVA: 0x000A15D1 File Offset: 0x0009F7D1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004550 RID: 17744
			// (set) Token: 0x06006A50 RID: 27216 RVA: 0x000A15E4 File Offset: 0x0009F7E4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004551 RID: 17745
			// (set) Token: 0x06006A51 RID: 27217 RVA: 0x000A15FC File Offset: 0x0009F7FC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004552 RID: 17746
			// (set) Token: 0x06006A52 RID: 27218 RVA: 0x000A1614 File Offset: 0x0009F814
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004553 RID: 17747
			// (set) Token: 0x06006A53 RID: 27219 RVA: 0x000A162C File Offset: 0x0009F82C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004554 RID: 17748
			// (set) Token: 0x06006A54 RID: 27220 RVA: 0x000A1644 File Offset: 0x0009F844
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
