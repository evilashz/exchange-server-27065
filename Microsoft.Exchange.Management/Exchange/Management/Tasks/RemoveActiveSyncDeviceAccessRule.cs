using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200007F RID: 127
	[Cmdlet("Remove", "ActiveSyncDeviceAccessRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveActiveSyncDeviceAccessRule : RemoveSystemConfigurationObjectTask<ActiveSyncDeviceAccessRuleIdParameter, ActiveSyncDeviceAccessRule>
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00010741 File Offset: 0x0000E941
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveActiveSyncDeviceAccessRule(this.Identity.ToString());
			}
		}
	}
}
