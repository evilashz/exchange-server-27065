using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007F2 RID: 2034
	[Cmdlet("Get", "AvailabilityConfig")]
	public sealed class GetAvailabilityConfig : GetMultitenancySingletonSystemConfigurationObjectTask<AvailabilityConfig>
	{
		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x06004703 RID: 18179 RVA: 0x0012372C File Offset: 0x0012192C
		protected override ObjectId RootId
		{
			get
			{
				IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
				return configurationSession.GetOrgContainerId();
			}
		}
	}
}
