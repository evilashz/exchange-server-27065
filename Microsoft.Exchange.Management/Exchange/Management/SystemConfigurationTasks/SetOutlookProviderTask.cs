using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007FC RID: 2044
	[Cmdlet("Set", "OutlookProvider", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetOutlookProviderTask : SetSystemConfigurationObjectTask<OutlookProviderIdParameter, OutlookProvider>
	{
		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06004749 RID: 18249 RVA: 0x00124BBE File Offset: 0x00122DBE
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetOutlookProvider(this.Identity.ToString());
			}
		}

		// Token: 0x17001597 RID: 5527
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x00124BD0 File Offset: 0x00122DD0
		protected override ObjectId RootId
		{
			get
			{
				return OutlookProvider.GetParentContainer(base.DataSession as ITopologyConfigurationSession);
			}
		}
	}
}
