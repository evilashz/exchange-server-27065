using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000059 RID: 89
	[Cmdlet("Get", "AdSiteLink", DefaultParameterSetName = "Identity")]
	public sealed class GetAdSiteLink : GetSystemConfigurationObjectTask<AdSiteLinkIdParameter, ADSiteLink>
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00009FCE File Offset: 0x000081CE
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
