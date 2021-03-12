using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200005B RID: 91
	public class SetActiveSyncDeviceAutoblockThresholdCommand : SyntheticCommandWithPipelineInputNoOutput<ActiveSyncDeviceAutoblockThreshold>
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x00035A90 File Offset: 0x00033C90
		private SetActiveSyncDeviceAutoblockThresholdCommand() : base("Set-ActiveSyncDeviceAutoblockThreshold")
		{
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00035A9D File Offset: 0x00033C9D
		public SetActiveSyncDeviceAutoblockThresholdCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00035AAC File Offset: 0x00033CAC
		public virtual SetActiveSyncDeviceAutoblockThresholdCommand SetParameters(SetActiveSyncDeviceAutoblockThresholdCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00035AB6 File Offset: 0x00033CB6
		public virtual SetActiveSyncDeviceAutoblockThresholdCommand SetParameters(SetActiveSyncDeviceAutoblockThresholdCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200005C RID: 92
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000218 RID: 536
			// (set) Token: 0x06001719 RID: 5913 RVA: 0x00035AC0 File Offset: 0x00033CC0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000219 RID: 537
			// (set) Token: 0x0600171A RID: 5914 RVA: 0x00035AD3 File Offset: 0x00033CD3
			public virtual int BehaviorTypeIncidenceLimit
			{
				set
				{
					base.PowerSharpParameters["BehaviorTypeIncidenceLimit"] = value;
				}
			}

			// Token: 0x1700021A RID: 538
			// (set) Token: 0x0600171B RID: 5915 RVA: 0x00035AEB File Offset: 0x00033CEB
			public virtual EnhancedTimeSpan BehaviorTypeIncidenceDuration
			{
				set
				{
					base.PowerSharpParameters["BehaviorTypeIncidenceDuration"] = value;
				}
			}

			// Token: 0x1700021B RID: 539
			// (set) Token: 0x0600171C RID: 5916 RVA: 0x00035B03 File Offset: 0x00033D03
			public virtual EnhancedTimeSpan DeviceBlockDuration
			{
				set
				{
					base.PowerSharpParameters["DeviceBlockDuration"] = value;
				}
			}

			// Token: 0x1700021C RID: 540
			// (set) Token: 0x0600171D RID: 5917 RVA: 0x00035B1B File Offset: 0x00033D1B
			public virtual string AdminEmailInsert
			{
				set
				{
					base.PowerSharpParameters["AdminEmailInsert"] = value;
				}
			}

			// Token: 0x1700021D RID: 541
			// (set) Token: 0x0600171E RID: 5918 RVA: 0x00035B2E File Offset: 0x00033D2E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700021E RID: 542
			// (set) Token: 0x0600171F RID: 5919 RVA: 0x00035B46 File Offset: 0x00033D46
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700021F RID: 543
			// (set) Token: 0x06001720 RID: 5920 RVA: 0x00035B5E File Offset: 0x00033D5E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000220 RID: 544
			// (set) Token: 0x06001721 RID: 5921 RVA: 0x00035B76 File Offset: 0x00033D76
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000221 RID: 545
			// (set) Token: 0x06001722 RID: 5922 RVA: 0x00035B8E File Offset: 0x00033D8E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200005D RID: 93
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000222 RID: 546
			// (set) Token: 0x06001724 RID: 5924 RVA: 0x00035BAE File Offset: 0x00033DAE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ActiveSyncDeviceAutoblockThresholdIdParameter(value) : null);
				}
			}

			// Token: 0x17000223 RID: 547
			// (set) Token: 0x06001725 RID: 5925 RVA: 0x00035BCC File Offset: 0x00033DCC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000224 RID: 548
			// (set) Token: 0x06001726 RID: 5926 RVA: 0x00035BDF File Offset: 0x00033DDF
			public virtual int BehaviorTypeIncidenceLimit
			{
				set
				{
					base.PowerSharpParameters["BehaviorTypeIncidenceLimit"] = value;
				}
			}

			// Token: 0x17000225 RID: 549
			// (set) Token: 0x06001727 RID: 5927 RVA: 0x00035BF7 File Offset: 0x00033DF7
			public virtual EnhancedTimeSpan BehaviorTypeIncidenceDuration
			{
				set
				{
					base.PowerSharpParameters["BehaviorTypeIncidenceDuration"] = value;
				}
			}

			// Token: 0x17000226 RID: 550
			// (set) Token: 0x06001728 RID: 5928 RVA: 0x00035C0F File Offset: 0x00033E0F
			public virtual EnhancedTimeSpan DeviceBlockDuration
			{
				set
				{
					base.PowerSharpParameters["DeviceBlockDuration"] = value;
				}
			}

			// Token: 0x17000227 RID: 551
			// (set) Token: 0x06001729 RID: 5929 RVA: 0x00035C27 File Offset: 0x00033E27
			public virtual string AdminEmailInsert
			{
				set
				{
					base.PowerSharpParameters["AdminEmailInsert"] = value;
				}
			}

			// Token: 0x17000228 RID: 552
			// (set) Token: 0x0600172A RID: 5930 RVA: 0x00035C3A File Offset: 0x00033E3A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000229 RID: 553
			// (set) Token: 0x0600172B RID: 5931 RVA: 0x00035C52 File Offset: 0x00033E52
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700022A RID: 554
			// (set) Token: 0x0600172C RID: 5932 RVA: 0x00035C6A File Offset: 0x00033E6A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700022B RID: 555
			// (set) Token: 0x0600172D RID: 5933 RVA: 0x00035C82 File Offset: 0x00033E82
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700022C RID: 556
			// (set) Token: 0x0600172E RID: 5934 RVA: 0x00035C9A File Offset: 0x00033E9A
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
