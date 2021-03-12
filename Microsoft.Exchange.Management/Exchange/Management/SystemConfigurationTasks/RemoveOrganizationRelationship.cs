using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F4 RID: 2548
	[Cmdlet("Remove", "OrganizationRelationship", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveOrganizationRelationship : RemoveSystemConfigurationObjectTask<OrganizationRelationshipIdParameter, OrganizationRelationship>
	{
		// Token: 0x17001B45 RID: 6981
		// (get) Token: 0x06005B23 RID: 23331 RVA: 0x0017D49C File Offset: 0x0017B69C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOrganizationRelationship(this.Identity.ToString());
			}
		}
	}
}
