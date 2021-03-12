using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Mobility;

namespace Microsoft.Exchange.Management.Extension
{
	// Token: 0x02000007 RID: 7
	[Cmdlet("Enable", "App", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class EnableApp : EnableDisableOWAExtensionBase
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002C61 File Offset: 0x00000E61
		public EnableApp() : base(true)
		{
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C6A File Offset: 0x00000E6A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableOwaExtension(this.Identity.ToString(), this.mailboxOwner);
			}
		}
	}
}
