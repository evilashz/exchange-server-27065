using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ProvisioningAgent;

namespace Microsoft.Exchange.DefaultProvisioningAgent.Rus
{
	// Token: 0x02000058 RID: 88
	internal class AddressBookDataProvider : RusDataProviderBase<ADRawEntry>
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000E631 File Offset: 0x0000C831
		public AddressBookDataProvider(IConfigurationSession session, IConfigurationSession rootOrgSession, ProvisioningCache provisioningCache) : base(session, rootOrgSession, provisioningCache)
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000E758 File Offset: 0x0000C958
		protected override void LoadPolicies(PolicyContainer<ADRawEntry> container, LogMessageDelegate logger)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Policies = base.ProvisioningCache.TryAddAndGetOrganizationData<List<ADRawEntry>>(CannedProvisioningCacheKeys.AddressBookPolicies, container.OrganizationId, delegate()
			{
				List<ADRawEntry> list = new List<ADRawEntry>();
				ADObjectId adobjectId = container.OrganizationId.ConfigurationUnit ?? this.OrgContainerId;
				IConfigurationSession configurationSession = (adobjectId == this.OrgContainerId) ? this.RootOrgConfigurationSession : this.ConfigurationSession;
				if (logger != null)
				{
					logger(Strings.VerboseAddressListsForOganizationFromDC(container.OrganizationId.ToString(), this.DomainController));
				}
				ADPagedReader<ADRawEntry> adpagedReader = configurationSession.FindPagedADRawEntry(adobjectId, QueryScope.SubTree, new AndFilter(new QueryFilter[]
				{
					AddressBookDataProvider.dummyObject.ImplicitFilter,
					AddressBookDataProvider.dummyObject.VersioningFilter
				}), null, 0, AddressBookDataProvider.attributes);
				if (adpagedReader != null)
				{
					foreach (ADRawEntry item in adpagedReader)
					{
						list.Add(item);
					}
				}
				return list;
			});
		}

		// Token: 0x0400012B RID: 299
		private static AddressBookBase dummyObject = new AddressBookBase();

		// Token: 0x0400012C RID: 300
		private static ADPropertyDefinition[] attributes = new ADPropertyDefinition[]
		{
			AddressBookBaseSchema.LdapRecipientFilter,
			AddressBookBaseSchema.RecipientContainer,
			AddressBookBaseSchema.IsSystemAddressList
		};
	}
}
