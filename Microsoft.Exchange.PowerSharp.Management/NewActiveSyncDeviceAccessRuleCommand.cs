using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000046 RID: 70
	public class NewActiveSyncDeviceAccessRuleCommand : SyntheticCommandWithPipelineInput<ActiveSyncDeviceAccessRule, ActiveSyncDeviceAccessRule>
	{
		// Token: 0x0600167E RID: 5758 RVA: 0x00034EB8 File Offset: 0x000330B8
		private NewActiveSyncDeviceAccessRuleCommand() : base("New-ActiveSyncDeviceAccessRule")
		{
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00034EC5 File Offset: 0x000330C5
		public NewActiveSyncDeviceAccessRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00034ED4 File Offset: 0x000330D4
		public virtual NewActiveSyncDeviceAccessRuleCommand SetParameters(NewActiveSyncDeviceAccessRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000047 RID: 71
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170001AB RID: 427
			// (set) Token: 0x06001681 RID: 5761 RVA: 0x00034EDE File Offset: 0x000330DE
			public virtual DeviceAccessLevel AccessLevel
			{
				set
				{
					base.PowerSharpParameters["AccessLevel"] = value;
				}
			}

			// Token: 0x170001AC RID: 428
			// (set) Token: 0x06001682 RID: 5762 RVA: 0x00034EF6 File Offset: 0x000330F6
			public virtual DeviceAccessCharacteristic Characteristic
			{
				set
				{
					base.PowerSharpParameters["Characteristic"] = value;
				}
			}

			// Token: 0x170001AD RID: 429
			// (set) Token: 0x06001683 RID: 5763 RVA: 0x00034F0E File Offset: 0x0003310E
			public virtual string QueryString
			{
				set
				{
					base.PowerSharpParameters["QueryString"] = value;
				}
			}

			// Token: 0x170001AE RID: 430
			// (set) Token: 0x06001684 RID: 5764 RVA: 0x00034F21 File Offset: 0x00033121
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170001AF RID: 431
			// (set) Token: 0x06001685 RID: 5765 RVA: 0x00034F3F File Offset: 0x0003313F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001B0 RID: 432
			// (set) Token: 0x06001686 RID: 5766 RVA: 0x00034F52 File Offset: 0x00033152
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001B1 RID: 433
			// (set) Token: 0x06001687 RID: 5767 RVA: 0x00034F6A File Offset: 0x0003316A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001B2 RID: 434
			// (set) Token: 0x06001688 RID: 5768 RVA: 0x00034F82 File Offset: 0x00033182
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001B3 RID: 435
			// (set) Token: 0x06001689 RID: 5769 RVA: 0x00034F9A File Offset: 0x0003319A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001B4 RID: 436
			// (set) Token: 0x0600168A RID: 5770 RVA: 0x00034FB2 File Offset: 0x000331B2
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
