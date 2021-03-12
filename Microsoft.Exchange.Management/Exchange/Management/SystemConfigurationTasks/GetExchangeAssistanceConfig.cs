using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200099A RID: 2458
	[Cmdlet("Get", "ExchangeAssistanceConfig")]
	public sealed class GetExchangeAssistanceConfig : GetMultitenancySingletonSystemConfigurationObjectTask<ExchangeAssistance>
	{
		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x060057F3 RID: 22515 RVA: 0x0016F6B7 File Offset: 0x0016D8B7
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x060057F4 RID: 22516 RVA: 0x0016F6BA File Offset: 0x0016D8BA
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x0016F6BD File Offset: 0x0016D8BD
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, NewExchangeAssistanceConfig.CurrentVersionContainerName);
			}
		}
	}
}
