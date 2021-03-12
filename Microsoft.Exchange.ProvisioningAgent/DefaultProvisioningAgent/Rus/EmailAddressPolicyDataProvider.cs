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
	// Token: 0x02000059 RID: 89
	internal class EmailAddressPolicyDataProvider : RusDataProviderBase<ADRawEntry>
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000E804 File Offset: 0x0000CA04
		public EmailAddressPolicyDataProvider(IConfigurationSession session, IConfigurationSession rootOrgSession, ProvisioningCache provisioningCache) : base(session, rootOrgSession, provisioningCache)
		{
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000E810 File Offset: 0x0000CA10
		protected override void LoadPolicies(PolicyContainer<ADRawEntry> container, LogMessageDelegate logger)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			List<ADRawEntry> list = new List<ADRawEntry>();
			ADObjectId adobjectId = container.OrganizationId.ConfigurationUnit ?? base.OrgContainerId;
			IConfigurationSession configurationSession = (adobjectId == base.OrgContainerId) ? base.RootOrgConfigurationSession : base.ConfigurationSession;
			adobjectId = adobjectId.GetDescendantId(EmailAddressPolicy.RdnEapContainerToOrganization);
			if (logger != null)
			{
				logger(Strings.VerboseEmailAddressPoliciesForOganizationFromDC(container.OrganizationId.ToString(), base.DomainController));
			}
			ADPagedReader<ADRawEntry> adpagedReader = configurationSession.FindPagedADRawEntry(adobjectId, QueryScope.SubTree, new AndFilter(new QueryFilter[]
			{
				EmailAddressPolicyDataProvider.dummyObject.ImplicitFilter,
				EmailAddressPolicyDataProvider.dummyObject.VersioningFilter,
				new ComparisonFilter(ComparisonOperator.Equal, EmailAddressPolicySchema.PolicyOptionListValue, EmailAddressPolicy.PolicyGuid.ToByteArray())
			}), null, 0, EmailAddressPolicyDataProvider.attributes);
			if (adpagedReader != null)
			{
				foreach (ADRawEntry item in adpagedReader)
				{
					list.Add(item);
				}
			}
			container.Policies = list;
		}

		// Token: 0x0400012D RID: 301
		private static EmailAddressPolicy dummyObject = new EmailAddressPolicy();

		// Token: 0x0400012E RID: 302
		private static ADPropertyDefinition[] attributes = new ADPropertyDefinition[]
		{
			EmailAddressPolicySchema.LdapRecipientFilter,
			EmailAddressPolicySchema.RecipientContainer,
			EmailAddressPolicySchema.Priority,
			EmailAddressPolicySchema.EnabledEmailAddressTemplates,
			EmailAddressPolicySchema.PolicyOptionListValue
		};
	}
}
