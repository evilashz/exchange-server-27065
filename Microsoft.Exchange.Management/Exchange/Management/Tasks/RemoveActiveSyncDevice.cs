using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200007E RID: 126
	[Cmdlet("Remove", "ActiveSyncDevice", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveActiveSyncDevice : RemoveMobileDevice
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x000106C8 File Offset: 0x0000E8C8
		protected override void InternalValidate()
		{
			this.WriteWarning(Strings.WarningCmdletIsDeprecated("Remove-ActiveSyncDevice", "Remove-MobileDevice"));
			base.InternalValidate();
			if ((base.DataObject != null && base.DataObject.ClientType != MobileClientType.EAS) || this.Identity.ClientType != MobileClientType.EAS)
			{
				base.WriteError(new LocalizedException(Strings.InvalidIdentityTypeForRemoveCmdletException(this.Identity.ToString())), ErrorCategory.InvalidArgument, null);
			}
		}
	}
}
