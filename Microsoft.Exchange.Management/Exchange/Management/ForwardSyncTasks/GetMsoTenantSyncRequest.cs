using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync.CookieManager;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000352 RID: 850
	[Cmdlet("Get", "MsoTenantSyncRequest")]
	public sealed class GetMsoTenantSyncRequest : GetSystemConfigurationObjectTask<OrganizationIdParameter, MsoTenantCookieContainer>
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x00081A3E File Offset: 0x0007FC3E
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x00081A46 File Offset: 0x0007FC46
		private new AccountPartitionIdParameter AccountPartition
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

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x00081A50 File Offset: 0x0007FC50
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = QueryFilter.OrTogether(new QueryFilter[]
				{
					new ExistsFilter(ExchangeConfigurationUnitSchema.MsoForwardSyncNonRecipientCookie),
					new ExistsFilter(ExchangeConfigurationUnitSchema.MsoForwardSyncRecipientCookie)
				});
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					base.InternalFilter,
					queryFilter
				});
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00081A9F File Offset: 0x0007FC9F
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00081AA2 File Offset: 0x0007FCA2
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, base.SessionSettings, 75, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\GetMsoTenantSyncRequest.cs");
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x00081AC4 File Offset: 0x0007FCC4
		protected override IEnumerable<MsoTenantCookieContainer> GetPagedData()
		{
			if (this.Identity == null)
			{
				QueryFilter queryFilter = QueryFilter.OrTogether(new QueryFilter[]
				{
					new ExistsFilter(ExchangeConfigurationUnitSchema.MsoForwardSyncNonRecipientCookie),
					new ExistsFilter(ExchangeConfigurationUnitSchema.MsoForwardSyncRecipientCookie)
				});
				QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.NotEqual, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.ReadyForRemoval),
					new ComparisonFilter(ComparisonOperator.NotEqual, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.SoftDeleted),
					new ComparisonFilter(ComparisonOperator.NotEqual, ExchangeConfigurationUnitSchema.OrganizationStatus, OrganizationStatus.PendingRemoval)
				});
				return PartitionDataAggregator.FindTenantCookieContainers(filter);
			}
			return base.GetPagedData();
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00081B60 File Offset: 0x0007FD60
		protected override void WriteResult(IConfigurable dataObject)
		{
			MsoTenantCookieContainer msoTenantCookieContainer = (MsoTenantCookieContainer)dataObject;
			MsoRecipientFullSyncCookieManager msoRecipientFullSyncCookieManager = new MsoRecipientFullSyncCookieManager(Guid.Parse(msoTenantCookieContainer.ExternalDirectoryOrganizationId));
			MsoCompanyFullSyncCookieManager msoCompanyFullSyncCookieManager = new MsoCompanyFullSyncCookieManager(Guid.Parse(msoTenantCookieContainer.ExternalDirectoryOrganizationId));
			msoRecipientFullSyncCookieManager.ReadCookie();
			msoCompanyFullSyncCookieManager.ReadCookie();
			MsoTenantSyncRequest dataObject2 = new MsoTenantSyncRequest(msoTenantCookieContainer, msoRecipientFullSyncCookieManager.LastCookie, msoCompanyFullSyncCookieManager.LastCookie);
			base.WriteResult(dataObject2);
		}
	}
}
