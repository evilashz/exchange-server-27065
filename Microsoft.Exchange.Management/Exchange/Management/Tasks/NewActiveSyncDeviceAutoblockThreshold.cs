using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200007B RID: 123
	[Cmdlet("New", "ActiveSyncDeviceAutoblockThreshold", SupportsShouldProcess = true)]
	public sealed class NewActiveSyncDeviceAutoblockThreshold : NewMultitenancyFixedNameSystemConfigurationObjectTask<ActiveSyncDeviceAutoblockThreshold>
	{
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000FF42 File Offset: 0x0000E142
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000FF4F File Offset: 0x0000E14F
		[Parameter(Mandatory = true)]
		public AutoblockThresholdType BehaviorType
		{
			get
			{
				return this.DataObject.BehaviorType;
			}
			set
			{
				this.DataObject.BehaviorType = value;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000FF5D File Offset: 0x0000E15D
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000FF6A File Offset: 0x0000E16A
		[Parameter(Mandatory = true)]
		public int BehaviorTypeIncidenceLimit
		{
			get
			{
				return this.DataObject.BehaviorTypeIncidenceLimit;
			}
			set
			{
				this.DataObject.BehaviorTypeIncidenceLimit = value;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000FF78 File Offset: 0x0000E178
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000FF85 File Offset: 0x0000E185
		[Parameter(Mandatory = true)]
		public EnhancedTimeSpan BehaviorTypeIncidenceDuration
		{
			get
			{
				return this.DataObject.BehaviorTypeIncidenceDuration;
			}
			set
			{
				this.DataObject.BehaviorTypeIncidenceDuration = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000FF93 File Offset: 0x0000E193
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
		[Parameter(Mandatory = true)]
		public EnhancedTimeSpan DeviceBlockDuration
		{
			get
			{
				return this.DataObject.DeviceBlockDuration;
			}
			set
			{
				this.DataObject.DeviceBlockDuration = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000FFAE File Offset: 0x0000E1AE
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000FFBB File Offset: 0x0000E1BB
		[Parameter(Mandatory = false)]
		public string AdminEmailInsert
		{
			get
			{
				return this.DataObject.AdminEmailInsert;
			}
			set
			{
				this.DataObject.AdminEmailInsert = value;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		protected override IConfigurable PrepareDataObject()
		{
			this.DataObject.Name = this.BehaviorType.ToString();
			ActiveSyncDeviceAutoblockThreshold activeSyncDeviceAutoblockThreshold = (ActiveSyncDeviceAutoblockThreshold)base.PrepareDataObject();
			activeSyncDeviceAutoblockThreshold.SetId((IConfigurationSession)base.DataSession, this.DataObject.Name);
			return activeSyncDeviceAutoblockThreshold;
		}
	}
}
