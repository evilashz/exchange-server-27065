using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200021F RID: 543
	[Cmdlet("Remove", "ExchangeUpgradeBucket", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveExchangeUpgradeBucket : RemoveSystemConfigurationObjectTask<ExchangeUpgradeBucketIdParameter, ExchangeUpgradeBucket>
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0005163B File Offset: 0x0004F83B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveExchangeUpgradeBucket(base.DataObject.Name);
			}
		}
	}
}
