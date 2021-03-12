using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000058 RID: 88
	public class SetActiveSyncDeviceAccessRuleCommand : SyntheticCommandWithPipelineInputNoOutput<ActiveSyncDeviceAccessRule>
	{
		// Token: 0x06001700 RID: 5888 RVA: 0x000358EC File Offset: 0x00033AEC
		private SetActiveSyncDeviceAccessRuleCommand() : base("Set-ActiveSyncDeviceAccessRule")
		{
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x000358F9 File Offset: 0x00033AF9
		public SetActiveSyncDeviceAccessRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00035908 File Offset: 0x00033B08
		public virtual SetActiveSyncDeviceAccessRuleCommand SetParameters(SetActiveSyncDeviceAccessRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00035912 File Offset: 0x00033B12
		public virtual SetActiveSyncDeviceAccessRuleCommand SetParameters(SetActiveSyncDeviceAccessRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000059 RID: 89
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000209 RID: 521
			// (set) Token: 0x06001704 RID: 5892 RVA: 0x0003591C File Offset: 0x00033B1C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700020A RID: 522
			// (set) Token: 0x06001705 RID: 5893 RVA: 0x0003592F File Offset: 0x00033B2F
			public virtual DeviceAccessLevel AccessLevel
			{
				set
				{
					base.PowerSharpParameters["AccessLevel"] = value;
				}
			}

			// Token: 0x1700020B RID: 523
			// (set) Token: 0x06001706 RID: 5894 RVA: 0x00035947 File Offset: 0x00033B47
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700020C RID: 524
			// (set) Token: 0x06001707 RID: 5895 RVA: 0x0003595F File Offset: 0x00033B5F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700020D RID: 525
			// (set) Token: 0x06001708 RID: 5896 RVA: 0x00035977 File Offset: 0x00033B77
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700020E RID: 526
			// (set) Token: 0x06001709 RID: 5897 RVA: 0x0003598F File Offset: 0x00033B8F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700020F RID: 527
			// (set) Token: 0x0600170A RID: 5898 RVA: 0x000359A7 File Offset: 0x00033BA7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200005A RID: 90
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000210 RID: 528
			// (set) Token: 0x0600170C RID: 5900 RVA: 0x000359C7 File Offset: 0x00033BC7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceAccessRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17000211 RID: 529
			// (set) Token: 0x0600170D RID: 5901 RVA: 0x000359E5 File Offset: 0x00033BE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000212 RID: 530
			// (set) Token: 0x0600170E RID: 5902 RVA: 0x000359F8 File Offset: 0x00033BF8
			public virtual DeviceAccessLevel AccessLevel
			{
				set
				{
					base.PowerSharpParameters["AccessLevel"] = value;
				}
			}

			// Token: 0x17000213 RID: 531
			// (set) Token: 0x0600170F RID: 5903 RVA: 0x00035A10 File Offset: 0x00033C10
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000214 RID: 532
			// (set) Token: 0x06001710 RID: 5904 RVA: 0x00035A28 File Offset: 0x00033C28
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000215 RID: 533
			// (set) Token: 0x06001711 RID: 5905 RVA: 0x00035A40 File Offset: 0x00033C40
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000216 RID: 534
			// (set) Token: 0x06001712 RID: 5906 RVA: 0x00035A58 File Offset: 0x00033C58
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000217 RID: 535
			// (set) Token: 0x06001713 RID: 5907 RVA: 0x00035A70 File Offset: 0x00033C70
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
