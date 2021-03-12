using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B27 RID: 2855
	[Cmdlet("Get", "AdSite", DefaultParameterSetName = "Identity")]
	public sealed class GetAdSite : GetSystemConfigurationObjectTask<AdSiteIdParameter, ADSite>
	{
		// Token: 0x060066D3 RID: 26323 RVA: 0x001A9180 File Offset: 0x001A7380
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 39, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Transport\\getAdSite.cs");
		}

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x060066D4 RID: 26324 RVA: 0x001A91B8 File Offset: 0x001A73B8
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
