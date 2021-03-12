using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007EF RID: 2031
	[Cmdlet("Set", "AuthRedirect", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetAuthRedirect : SetSystemConfigurationObjectTask<AuthRedirectIdParameter, AuthRedirect>
	{
		// Token: 0x17001576 RID: 5494
		// (get) Token: 0x060046F0 RID: 18160 RVA: 0x00123532 File Offset: 0x00121732
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAuthRedirect(this.Identity.RawIdentity);
			}
		}
	}
}
