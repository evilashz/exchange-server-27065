using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000048 RID: 72
	public class NewActiveSyncDeviceAutoblockThresholdCommand : SyntheticCommandWithPipelineInput<ActiveSyncDeviceAutoblockThreshold, ActiveSyncDeviceAutoblockThreshold>
	{
		// Token: 0x0600168C RID: 5772 RVA: 0x00034FD2 File Offset: 0x000331D2
		private NewActiveSyncDeviceAutoblockThresholdCommand() : base("New-ActiveSyncDeviceAutoblockThreshold")
		{
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00034FDF File Offset: 0x000331DF
		public NewActiveSyncDeviceAutoblockThresholdCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00034FEE File Offset: 0x000331EE
		public virtual NewActiveSyncDeviceAutoblockThresholdCommand SetParameters(NewActiveSyncDeviceAutoblockThresholdCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000049 RID: 73
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170001B5 RID: 437
			// (set) Token: 0x0600168F RID: 5775 RVA: 0x00034FF8 File Offset: 0x000331F8
			public virtual AutoblockThresholdType BehaviorType
			{
				set
				{
					base.PowerSharpParameters["BehaviorType"] = value;
				}
			}

			// Token: 0x170001B6 RID: 438
			// (set) Token: 0x06001690 RID: 5776 RVA: 0x00035010 File Offset: 0x00033210
			public virtual int BehaviorTypeIncidenceLimit
			{
				set
				{
					base.PowerSharpParameters["BehaviorTypeIncidenceLimit"] = value;
				}
			}

			// Token: 0x170001B7 RID: 439
			// (set) Token: 0x06001691 RID: 5777 RVA: 0x00035028 File Offset: 0x00033228
			public virtual EnhancedTimeSpan BehaviorTypeIncidenceDuration
			{
				set
				{
					base.PowerSharpParameters["BehaviorTypeIncidenceDuration"] = value;
				}
			}

			// Token: 0x170001B8 RID: 440
			// (set) Token: 0x06001692 RID: 5778 RVA: 0x00035040 File Offset: 0x00033240
			public virtual EnhancedTimeSpan DeviceBlockDuration
			{
				set
				{
					base.PowerSharpParameters["DeviceBlockDuration"] = value;
				}
			}

			// Token: 0x170001B9 RID: 441
			// (set) Token: 0x06001693 RID: 5779 RVA: 0x00035058 File Offset: 0x00033258
			public virtual string AdminEmailInsert
			{
				set
				{
					base.PowerSharpParameters["AdminEmailInsert"] = value;
				}
			}

			// Token: 0x170001BA RID: 442
			// (set) Token: 0x06001694 RID: 5780 RVA: 0x0003506B File Offset: 0x0003326B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170001BB RID: 443
			// (set) Token: 0x06001695 RID: 5781 RVA: 0x00035089 File Offset: 0x00033289
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170001BC RID: 444
			// (set) Token: 0x06001696 RID: 5782 RVA: 0x0003509C File Offset: 0x0003329C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170001BD RID: 445
			// (set) Token: 0x06001697 RID: 5783 RVA: 0x000350B4 File Offset: 0x000332B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170001BE RID: 446
			// (set) Token: 0x06001698 RID: 5784 RVA: 0x000350CC File Offset: 0x000332CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170001BF RID: 447
			// (set) Token: 0x06001699 RID: 5785 RVA: 0x000350E4 File Offset: 0x000332E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170001C0 RID: 448
			// (set) Token: 0x0600169A RID: 5786 RVA: 0x000350FC File Offset: 0x000332FC
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
