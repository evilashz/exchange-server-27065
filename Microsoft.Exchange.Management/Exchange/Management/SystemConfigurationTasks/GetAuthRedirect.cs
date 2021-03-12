using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007EC RID: 2028
	[Cmdlet("Get", "AuthRedirect")]
	public sealed class GetAuthRedirect : GetSystemConfigurationObjectTask<AuthRedirectIdParameter, AuthRedirect>
	{
		// Token: 0x1700156F RID: 5487
		// (get) Token: 0x060046E1 RID: 18145 RVA: 0x00123348 File Offset: 0x00121548
		protected override ObjectId RootId
		{
			get
			{
				return this.ConfigurationSession.GetOrgContainerId().GetChildId(ServiceEndpointContainer.DefaultName);
			}
		}

		// Token: 0x17001570 RID: 5488
		// (get) Token: 0x060046E2 RID: 18146 RVA: 0x00123360 File Offset: 0x00121560
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = base.InternalFilter;
				if (queryFilter == null)
				{
					queryFilter = AuthRedirect.AuthRedirectKeywordsFilter;
				}
				else
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						AuthRedirect.AuthRedirectKeywordsFilter
					});
				}
				return queryFilter;
			}
		}
	}
}
