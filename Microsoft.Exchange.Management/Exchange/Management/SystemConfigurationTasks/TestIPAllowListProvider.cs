using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A38 RID: 2616
	[Cmdlet("Test", "IPAllowListProvider", SupportsShouldProcess = true)]
	public class TestIPAllowListProvider : TestIPListProvider<IPAllowListProviderIdParameter, IPAllowListProvider>
	{
		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x06005D6B RID: 23915 RVA: 0x0018997A File Offset: 0x00187B7A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestIPAllowListProvider(this.Identity.ToString(), base.IPAddress.ToString());
			}
		}
	}
}
