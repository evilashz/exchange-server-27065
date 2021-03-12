using System;
using System.Management.Automation;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000072 RID: 114
	[Cmdlet("Get", "ActiveSyncDeviceStatistics", DefaultParameterSetName = "Identity")]
	public sealed class GetActiveSyncDeviceStatistics : GetMobileDeviceStatisticsBase<ActiveSyncDeviceIdParameter, ActiveSyncDevice>
	{
		// Token: 0x06000398 RID: 920 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		public GetActiveSyncDeviceStatistics()
		{
			this.ActiveSync = true;
			this.OWAforDevices = false;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000F704 File Offset: 0x0000D904
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000F70C File Offset: 0x0000D90C
		private new SwitchParameter ActiveSync
		{
			get
			{
				return base.ActiveSync;
			}
			set
			{
				base.ActiveSync = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000F715 File Offset: 0x0000D915
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000F71D File Offset: 0x0000D91D
		private new SwitchParameter OWAforDevices
		{
			get
			{
				return base.OWAforDevices;
			}
			set
			{
				base.OWAforDevices = value;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000F726 File Offset: 0x0000D926
		protected override MobileDeviceConfiguration CreateDeviceConfiguration(DeviceInfo deviceInfo)
		{
			return new ActiveSyncDeviceConfiguration(deviceInfo);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000F72E File Offset: 0x0000D92E
		protected override MobileDeviceIdParameter CreateIdentityObject()
		{
			return new ActiveSyncDeviceIdParameter(this.DataObject);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000F73B File Offset: 0x0000D93B
		protected override void InternalProcessRecord()
		{
			this.WriteWarning(Strings.WarningCmdletIsDeprecated("Get-ActiveSyncDeviceStatistics", "Get-MobileDeviceStatistics"));
			base.InternalProcessRecord();
		}
	}
}
