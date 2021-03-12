using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AE4 RID: 2788
	[Cmdlet("Remove", "OnPremisesOrganization", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveOnPremisesOrganization : RemoveSystemConfigurationObjectTask<OnPremisesOrganizationIdParameter, OnPremisesOrganization>
	{
		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06006307 RID: 25351 RVA: 0x0019DE91 File Offset: 0x0019C091
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.OnPremisesOrganizationConfirmationMessageRemove(this.Identity);
			}
		}
	}
}
