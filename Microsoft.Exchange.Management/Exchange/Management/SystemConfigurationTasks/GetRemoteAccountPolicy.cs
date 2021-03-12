using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B14 RID: 2836
	[Cmdlet("Get", "RemoteAccountPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetRemoteAccountPolicy : GetMultitenancySystemConfigurationObjectTask<RemoteAccountPolicyIdParameter, RemoteAccountPolicy>
	{
		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x060064BC RID: 25788 RVA: 0x001A4A5E File Offset: 0x001A2C5E
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
