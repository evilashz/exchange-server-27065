using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007FD RID: 2045
	[Cmdlet("Remove", "OutlookProvider", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveOutlookProviderTask : RemoveSystemConfigurationObjectTask<OutlookProviderIdParameter, OutlookProvider>
	{
		// Token: 0x17001598 RID: 5528
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x00124BEA File Offset: 0x00122DEA
		protected override ObjectId RootId
		{
			get
			{
				return OutlookProvider.GetParentContainer(base.DataSession as ITopologyConfigurationSession);
			}
		}

		// Token: 0x17001599 RID: 5529
		// (get) Token: 0x0600474D RID: 18253 RVA: 0x00124BFC File Offset: 0x00122DFC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveOutlookProvider(this.Identity.ToString());
			}
		}
	}
}
