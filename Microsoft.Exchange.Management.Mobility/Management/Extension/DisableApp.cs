using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Mobility;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000006 RID: 6
	[Cmdlet("Disable", "App", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableApp : EnableDisableOWAExtensionBase
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002C40 File Offset: 0x00000E40
		public DisableApp() : base(false)
		{
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002C49 File Offset: 0x00000E49
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableOwaExtension(this.Identity.ToString(), this.mailboxOwner);
			}
		}
	}
}
