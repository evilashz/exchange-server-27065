using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000EC RID: 236
	internal class SiteMailboxAddressesTemplate
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00041B94 File Offset: 0x0003FD94
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x00041B9C File Offset: 0x0003FD9C
		public string UserPrincipalNameDomain { get; set; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00041BA5 File Offset: 0x0003FDA5
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x00041BAD File Offset: 0x0003FDAD
		public MultiValuedProperty<ProxyAddressTemplate> AddressTemplates { get; private set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00041BB6 File Offset: 0x0003FDB6
		public bool IsValid
		{
			get
			{
				return !string.IsNullOrEmpty(this.UserPrincipalNameDomain) && this.AddressTemplates.Count > 0;
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00041BD5 File Offset: 0x0003FDD5
		public SiteMailboxAddressesTemplate()
		{
			this.AddressTemplates = new MultiValuedProperty<ProxyAddressTemplate>();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x00041E60 File Offset: 0x00040060
		public static SiteMailboxAddressesTemplate GetSiteMailboxAddressesTemplate(IConfigurationSession cfgSession, ProvisioningCache provisioningCache)
		{
			if (cfgSession == null)
			{
				throw new ArgumentNullException("cfgSession");
			}
			if (provisioningCache == null)
			{
				throw new ArgumentNullException("provisioningCache");
			}
			OrganizationId orgId = cfgSession.GetOrgContainer().OrganizationId;
			return provisioningCache.TryAddAndGetOrganizationData<SiteMailboxAddressesTemplate>(CannedProvisioningCacheKeys.OrganizationSiteMailboxAddressesTemplate, orgId, delegate()
			{
				ADObjectId rootId = orgId.ConfigurationUnit ?? provisioningCache.TryAddAndGetGlobalData<ADObjectId>(CannedProvisioningCacheKeys.FirstOrgContainerId, () => cfgSession.GetOrgContainerId());
				MultiValuedProperty<SmtpDomain> multiValuedProperty = null;
				ADPagedReader<OnPremisesOrganization> adpagedReader = cfgSession.FindPaged<OnPremisesOrganization>(rootId, QueryScope.SubTree, null, null, 0);
				using (IEnumerator<OnPremisesOrganization> enumerator = adpagedReader.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						OnPremisesOrganization onPremisesOrganization = enumerator.Current;
						multiValuedProperty = onPremisesOrganization.HybridDomains;
					}
				}
				QueryFilter filter = new NotFilter(new BitMaskAndFilter(AcceptedDomainSchema.AcceptedDomainFlags, 1UL));
				ADPagedReader<AcceptedDomain> adpagedReader2 = cfgSession.FindPaged<AcceptedDomain>(rootId, QueryScope.SubTree, filter, new SortBy(ADObjectSchema.Name, SortOrder.Ascending), 0);
				bool flag = false;
				string text = string.Empty;
				string text2 = string.Empty;
				string text3 = string.Empty;
				foreach (AcceptedDomain acceptedDomain in adpagedReader2)
				{
					if (acceptedDomain.AuthenticationType != AuthenticationType.Federated && (string.IsNullOrEmpty(text) || acceptedDomain.Default))
					{
						text = acceptedDomain.DomainName.Domain;
					}
					if ((multiValuedProperty == null || multiValuedProperty.Count == 0 || multiValuedProperty.Contains(acceptedDomain.DomainName.SmtpDomain)) && (string.IsNullOrEmpty(text2) || acceptedDomain.Default))
					{
						text2 = acceptedDomain.DomainName.Domain;
					}
					if (acceptedDomain.IsCoexistenceDomain && string.IsNullOrEmpty(text3))
					{
						text3 = acceptedDomain.DomainName.Domain;
					}
					flag = (flag || acceptedDomain.Default);
					if (flag && !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text3))
					{
						break;
					}
				}
				SiteMailboxAddressesTemplate siteMailboxAddressesTemplate = new SiteMailboxAddressesTemplate();
				siteMailboxAddressesTemplate.UserPrincipalNameDomain = text;
				if (!string.IsNullOrEmpty(text2))
				{
					siteMailboxAddressesTemplate.AddressTemplates.Add(new SmtpProxyAddressTemplate(string.Format("@{0}", text2), true));
					if (!string.IsNullOrEmpty(text3) && !string.Equals(text2, text3, StringComparison.OrdinalIgnoreCase))
					{
						siteMailboxAddressesTemplate.AddressTemplates.Add(new SmtpProxyAddressTemplate(string.Format("@{0}", text3), false));
					}
				}
				if (!siteMailboxAddressesTemplate.IsValid)
				{
					throw new ErrorSiteMailboxCannotLoadAddressTemplateException();
				}
				return siteMailboxAddressesTemplate;
			});
		}
	}
}
