using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B0C RID: 2828
	[Cmdlet("Get", "ResourceConfig")]
	public sealed class GetResourceConfig : GetMultitenancySingletonSystemConfigurationObjectTask<ResourceBookingConfig>
	{
		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x060064A7 RID: 25767 RVA: 0x001A4448 File Offset: 0x001A2648
		protected override ObjectId RootId
		{
			get
			{
				ADObjectId orgContainerId = base.CurrentOrgContainerId;
				if (base.SharedConfiguration != null)
				{
					orgContainerId = base.SharedConfiguration.SharedConfigurationCU.Id;
				}
				return ResourceBookingConfig.GetWellKnownParentLocation(orgContainerId);
			}
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x001A447B File Offset: 0x001A267B
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}
	}
}
