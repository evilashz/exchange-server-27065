using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A39 RID: 2617
	[Cmdlet("Test", "IPBlockListProvider", SupportsShouldProcess = true)]
	public class TestIPBlockListProvider : TestIPListProvider<IPBlockListProviderIdParameter, IPBlockListProvider>
	{
		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x0018999F File Offset: 0x00187B9F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageTestIPBlockListProvider(this.Identity.ToString(), base.IPAddress.ToString());
			}
		}
	}
}
