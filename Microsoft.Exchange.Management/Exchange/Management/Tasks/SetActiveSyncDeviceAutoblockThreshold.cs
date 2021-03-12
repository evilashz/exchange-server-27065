using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000082 RID: 130
	[Cmdlet("Set", "ActiveSyncDeviceAutoblockThreshold", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetActiveSyncDeviceAutoblockThreshold : SetSystemConfigurationObjectTask<ActiveSyncDeviceAutoblockThresholdIdParameter, ActiveSyncDeviceAutoblockThreshold>
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x000107E8 File Offset: 0x0000E9E8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetActiveSyncDeviceAutoblockThreshold(this.Identity.ToString());
			}
		}
	}
}
