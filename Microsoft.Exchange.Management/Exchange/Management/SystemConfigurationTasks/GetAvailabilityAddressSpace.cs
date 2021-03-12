using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007F1 RID: 2033
	[Cmdlet("Get", "AvailabilityAddressSpace", DefaultParameterSetName = "Identity")]
	public sealed class GetAvailabilityAddressSpace : GetMultitenancySystemConfigurationObjectTask<AvailabilityAddressSpaceIdParameter, AvailabilityAddressSpace>
	{
		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06004701 RID: 18177 RVA: 0x0012371F File Offset: 0x0012191F
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
