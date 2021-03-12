using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000619 RID: 1561
	[Cmdlet("Remove", "AuthServer", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemoveAuthServer : RemoveSystemConfigurationObjectTask<AuthServerIdParameter, AuthServer>
	{
		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x000E5100 File Offset: 0x000E3300
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAuthServer(this.Identity.RawIdentity);
			}
		}
	}
}
