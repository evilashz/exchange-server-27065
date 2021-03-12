using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001D8 RID: 472
	[Cmdlet("Get", "ServiceEndpoint", DefaultParameterSetName = "Identity")]
	public sealed class GetServiceEndpoint : GetSystemConfigurationObjectTask<ADServiceConnectionPointIdParameter, ADServiceConnectionPoint>
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x000489F7 File Offset: 0x00046BF7
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x000489FC File Offset: 0x00046BFC
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
				return configurationSession.GetOrgContainerId().GetChildId(ServiceEndpointContainer.DefaultName);
			}
		}
	}
}
