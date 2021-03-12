using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007EE RID: 2030
	[Cmdlet("Remove", "AuthRedirect", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveAuthRedirect : RemoveSystemConfigurationObjectTask<AuthRedirectIdParameter, AuthRedirect>
	{
		// Token: 0x17001575 RID: 5493
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x00123518 File Offset: 0x00121718
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAuthRedirect(this.Identity.RawIdentity);
			}
		}
	}
}
