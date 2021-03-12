using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000061 RID: 97
	[Cmdlet("Clear", "ActiveSyncDevice", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class ClearActiveSyncDevice : ClearMobileDevice
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		protected override void InternalValidate()
		{
			this.WriteWarning(Strings.WarningCmdletIsDeprecated("Clear-ActiveSyncDevice", "Clear-MobileDevice"));
			base.InternalValidate();
			if (this.DataObject.ClientType != MobileClientType.EAS)
			{
				base.WriteError(new LocalizedException(Strings.InvalidIdentityTypeForClearCmdletException(this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}
