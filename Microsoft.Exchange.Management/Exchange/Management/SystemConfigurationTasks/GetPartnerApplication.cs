using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000613 RID: 1555
	[Cmdlet("Get", "PartnerApplication")]
	public sealed class GetPartnerApplication : GetMultitenancySystemConfigurationObjectTask<PartnerApplicationIdParameter, PartnerApplication>
	{
		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x000E46ED File Offset: 0x000E28ED
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
