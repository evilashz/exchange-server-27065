using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AD0 RID: 2768
	[Cmdlet("Get", "RemoteDomain", DefaultParameterSetName = "Identity")]
	public class GetRemoteDomain : GetMultitenancySystemConfigurationObjectTask<RemoteDomainIdParameter, DomainContentConfig>
	{
		// Token: 0x17001DDF RID: 7647
		// (get) Token: 0x06006263 RID: 25187 RVA: 0x0019AB30 File Offset: 0x00198D30
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
