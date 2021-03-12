using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000052 RID: 82
	public class RemoveActiveSyncDeviceAccessRuleCommand : SyntheticCommandWithPipelineInput<ActiveSyncDeviceAccessRule, ActiveSyncDeviceAccessRule>
	{
		// Token: 0x060016D6 RID: 5846 RVA: 0x000355A4 File Offset: 0x000337A4
		private RemoveActiveSyncDeviceAccessRuleCommand() : base("Remove-ActiveSyncDeviceAccessRule")
		{
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x000355B1 File Offset: 0x000337B1
		public RemoveActiveSyncDeviceAccessRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x000355C0 File Offset: 0x000337C0
		public virtual RemoveActiveSyncDeviceAccessRuleCommand SetParameters(RemoveActiveSyncDeviceAccessRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x000355CA File Offset: 0x000337CA
		public virtual RemoveActiveSyncDeviceAccessRuleCommand SetParameters(RemoveActiveSyncDeviceAccessRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000053 RID: 83
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170001EB RID: 491
			// (set) Token: 0x060016DA RID: 5850 RVA: 0x000355D4 File Offset: 0x000337D4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001EC RID: 492
			// (set) Token: 0x060016DB RID: 5851 RVA: 0x000355E7 File Offset: 0x000337E7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001ED RID: 493
			// (set) Token: 0x060016DC RID: 5852 RVA: 0x000355FF File Offset: 0x000337FF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001EE RID: 494
			// (set) Token: 0x060016DD RID: 5853 RVA: 0x00035617 File Offset: 0x00033817
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001EF RID: 495
			// (set) Token: 0x060016DE RID: 5854 RVA: 0x0003562F File Offset: 0x0003382F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (set) Token: 0x060016DF RID: 5855 RVA: 0x00035647 File Offset: 0x00033847
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170001F1 RID: 497
			// (set) Token: 0x060016E0 RID: 5856 RVA: 0x0003565F File Offset: 0x0003385F
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000054 RID: 84
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170001F2 RID: 498
			// (set) Token: 0x060016E2 RID: 5858 RVA: 0x0003567F File Offset: 0x0003387F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceAccessRuleIdParameter(value) : null);
				}
			}

			// Token: 0x170001F3 RID: 499
			// (set) Token: 0x060016E3 RID: 5859 RVA: 0x0003569D File Offset: 0x0003389D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001F4 RID: 500
			// (set) Token: 0x060016E4 RID: 5860 RVA: 0x000356B0 File Offset: 0x000338B0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001F5 RID: 501
			// (set) Token: 0x060016E5 RID: 5861 RVA: 0x000356C8 File Offset: 0x000338C8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001F6 RID: 502
			// (set) Token: 0x060016E6 RID: 5862 RVA: 0x000356E0 File Offset: 0x000338E0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001F7 RID: 503
			// (set) Token: 0x060016E7 RID: 5863 RVA: 0x000356F8 File Offset: 0x000338F8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001F8 RID: 504
			// (set) Token: 0x060016E8 RID: 5864 RVA: 0x00035710 File Offset: 0x00033910
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170001F9 RID: 505
			// (set) Token: 0x060016E9 RID: 5865 RVA: 0x00035728 File Offset: 0x00033928
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
