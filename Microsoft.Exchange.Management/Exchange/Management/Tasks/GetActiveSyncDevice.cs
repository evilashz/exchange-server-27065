using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200006D RID: 109
	[Cmdlet("Get", "ActiveSyncDevice", DefaultParameterSetName = "Identity")]
	public sealed class GetActiveSyncDevice : GetMobileDeviceBase<ActiveSyncDeviceIdParameter, ActiveSyncDevice>
	{
		// Token: 0x06000368 RID: 872 RVA: 0x0000E608 File Offset: 0x0000C808
		public GetActiveSyncDevice()
		{
			this.ActiveSync = true;
			this.OWAforDevices = false;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000E628 File Offset: 0x0000C828
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000E630 File Offset: 0x0000C830
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

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000E639 File Offset: 0x0000C839
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000E641 File Offset: 0x0000C841
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

		// Token: 0x0600036D RID: 877 RVA: 0x0000E64C File Offset: 0x0000C84C
		protected override void WriteResult(IConfigurable dataObject)
		{
			this.WriteWarning(Strings.WarningCmdletIsDeprecated("Get-ActiveSyncDevice", "Get-MobileDevice"));
			MobileDevice mobileDevice = (MobileDevice)dataObject;
			base.WriteResult(new ActiveSyncDevice(mobileDevice));
		}
	}
}
