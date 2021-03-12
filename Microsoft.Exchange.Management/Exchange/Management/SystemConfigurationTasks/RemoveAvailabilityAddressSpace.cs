using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007F6 RID: 2038
	[Cmdlet("Remove", "AvailabilityAddressSpace", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAvailabilityAddressSpace : RemoveSystemConfigurationObjectTask<AvailabilityAddressSpaceIdParameter, AvailabilityAddressSpace>
	{
		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x0600471A RID: 18202 RVA: 0x00123EE6 File Offset: 0x001220E6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAvailabilityAddressSpace(this.Identity.ToString());
			}
		}
	}
}
