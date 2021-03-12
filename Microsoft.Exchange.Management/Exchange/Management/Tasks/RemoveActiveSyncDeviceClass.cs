using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000080 RID: 128
	[Cmdlet("Remove", "ActiveSyncDeviceClass", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveActiveSyncDeviceClass : RemoveSystemConfigurationObjectTask<ActiveSyncDeviceClassIdParameter, ActiveSyncDeviceClass>
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0001075B File Offset: 0x0000E95B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveActiveSyncDeviceClass(this.Identity.ToString());
			}
		}
	}
}
