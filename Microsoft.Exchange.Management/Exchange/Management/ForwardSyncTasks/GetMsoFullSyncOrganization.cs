using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x0200034E RID: 846
	[Cmdlet("Get", "MsoFullSyncOrganization")]
	public sealed class GetMsoFullSyncOrganization : GetSystemConfigurationObjectTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x06001D38 RID: 7480 RVA: 0x00081194 File Offset: 0x0007F394
		public GetMsoFullSyncOrganization()
		{
			base.OptionalIdentityData.AdditionalFilter = new OrFilter(new QueryFilter[]
			{
				new ExistsFilter(ExchangeConfigurationUnitSchema.MsoForwardSyncRecipientCookie),
				new ExistsFilter(ExchangeConfigurationUnitSchema.MsoForwardSyncNonRecipientCookie)
			});
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x000811D9 File Offset: 0x0007F3D9
		// (set) Token: 0x06001D3A RID: 7482 RVA: 0x000811E1 File Offset: 0x0007F3E1
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return base.AccountPartition;
			}
			set
			{
				base.AccountPartition = value;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x000811EC File Offset: 0x0007F3EC
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = new ExistsFilter(ExchangeConfigurationUnitSchema.MsoForwardSyncNonRecipientCookie);
				QueryFilter queryFilter2 = new ExistsFilter(ExchangeConfigurationUnitSchema.MsoForwardSyncRecipientCookie);
				QueryFilter queryFilter3 = new OrFilter(new QueryFilter[]
				{
					queryFilter,
					queryFilter2
				});
				QueryFilter internalFilter = base.InternalFilter;
				if (internalFilter != null)
				{
					return new AndFilter(new QueryFilter[]
					{
						queryFilter3,
						internalFilter
					});
				}
				return queryFilter3;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0008124E File Offset: 0x0007F44E
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00081254 File Offset: 0x0007F454
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 80, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\GetMsoFullSyncOrganization.cs");
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x0008128C File Offset: 0x0007F48C
		protected override void WriteResult(IConfigurable dataObject)
		{
			ExchangeConfigurationUnit dataObject2 = (ExchangeConfigurationUnit)dataObject;
			TenantOrganizationPresentationObject dataObject3 = new TenantOrganizationPresentationObject(dataObject2);
			base.WriteResult(dataObject3);
		}
	}
}
