using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A15 RID: 2581
	[Cmdlet("Remove", "IntraOrganizationConnector", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveIntraOrganizationConnector : RemoveSystemConfigurationObjectTask<IntraOrganizationConnectorIdParameter, IntraOrganizationConnector>
	{
		// Token: 0x17001BC3 RID: 7107
		// (get) Token: 0x06005C9B RID: 23707 RVA: 0x0018640C File Offset: 0x0018460C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveIntraOrganizationConnector(this.Identity.ToString());
			}
		}
	}
}
