using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000615 RID: 1557
	[Cmdlet("Remove", "PartnerApplication", ConfirmImpact = ConfirmImpact.High, SupportsShouldProcess = true)]
	public sealed class RemovePartnerApplication : RemoveSystemConfigurationObjectTask<PartnerApplicationIdParameter, PartnerApplication>
	{
		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06003746 RID: 14150 RVA: 0x000E4A64 File Offset: 0x000E2C64
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemovePartnerApplication(this.Identity.RawIdentity);
			}
		}
	}
}
