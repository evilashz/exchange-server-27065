using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A40 RID: 2624
	[Cmdlet("Get", "HostedContentFilterPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetHostedContentFilterPolicy : GetMultitenancySystemConfigurationObjectTask<HostedContentFilterPolicyIdParameter, HostedContentFilterPolicy>
	{
		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x0018A19C File Offset: 0x0018839C
		// (set) Token: 0x06005DB1 RID: 23985 RVA: 0x0018A1A4 File Offset: 0x001883A4
		[Parameter]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x0018A1AD File Offset: 0x001883AD
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x06005DB3 RID: 23987 RVA: 0x0018A1B0 File Offset: 0x001883B0
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x0018A1C4 File Offset: 0x001883C4
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ExchangeVersion, ExchangeObjectVersion.Exchange2012);
				if (base.InternalFilter != null)
				{
					return new AndFilter(new QueryFilter[]
					{
						base.InternalFilter,
						queryFilter
					});
				}
				return queryFilter;
			}
		}
	}
}
