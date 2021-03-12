using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AA6 RID: 2726
	[Cmdlet("Get", "X400AuthoritativeDomain", DefaultParameterSetName = "Identity")]
	public sealed class GetX400AuthoritativeDomain : GetSystemConfigurationObjectTask<X400AuthoritativeDomainIdParameter, X400AuthoritativeDomain>
	{
		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06006073 RID: 24691 RVA: 0x00191DC1 File Offset: 0x0018FFC1
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
