using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200063E RID: 1598
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationAcceptedDomainsCache : OrganizationBaseCache
	{
		// Token: 0x06004B60 RID: 19296 RVA: 0x00115D5E File Offset: 0x00113F5E
		public OrganizationAcceptedDomainsCache(OrganizationId organizationId, IConfigurationSession session) : base(organizationId, session)
		{
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06004B61 RID: 19297 RVA: 0x00115D68 File Offset: 0x00113F68
		public Dictionary<string, AuthenticationType> Value
		{
			get
			{
				this.PopulateCacheIfNeeded();
				return this.namespaceAuthenticationTypeHash;
			}
		}

		// Token: 0x06004B62 RID: 19298 RVA: 0x00115D76 File Offset: 0x00113F76
		private void PopulateCacheIfNeeded()
		{
			if (!this.cached)
			{
				this.cached = this.PopulateCache();
			}
		}

		// Token: 0x06004B63 RID: 19299 RVA: 0x00115DC4 File Offset: 0x00113FC4
		private bool PopulateCache()
		{
			OrganizationBaseCache.Tracer.TraceDebug<OrganizationId>((long)this.GetHashCode(), "Searching for AcceptedDomain instances associated with OrganizationId '{0}'", base.OrganizationId);
			AcceptedDomain[] acceptedDomains = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				acceptedDomains = ((ITenantConfigurationSession)this.Session).FindAllAcceptedDomainsInOrg(this.OrganizationId.ConfigurationUnit);
			});
			if (!adoperationResult.Succeeded)
			{
				OrganizationBaseCache.Tracer.TraceError<OrganizationId, Exception>((long)this.GetHashCode(), "Unable to find AcceptedDomain instances associated with the OrganizationId '{0}' due to exception: {1}", base.OrganizationId, adoperationResult.Exception);
				throw adoperationResult.Exception;
			}
			if (acceptedDomains == null || acceptedDomains.Length == 0)
			{
				OrganizationBaseCache.Tracer.TraceError<OrganizationId>((long)this.GetHashCode(), "Unable to find any AcceptedDomain associated with the OrganizationId '{0}'", base.OrganizationId);
				return adoperationResult.Succeeded;
			}
			Dictionary<string, AuthenticationType> dictionary = new Dictionary<string, AuthenticationType>(acceptedDomains.Length, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < acceptedDomains.Length; i++)
			{
				dictionary[acceptedDomains[i].DomainName.Domain] = acceptedDomains[i].RawAuthenticationType;
			}
			this.namespaceAuthenticationTypeHash = dictionary;
			OrganizationBaseCache.Tracer.TraceDebug<EnumerableTracer<string>>((long)this.GetHashCode(), "Found the following Accepted Domains: {0}", new EnumerableTracer<string>(this.namespaceAuthenticationTypeHash.Keys));
			return adoperationResult.Succeeded;
		}

		// Token: 0x040033C9 RID: 13257
		private Dictionary<string, AuthenticationType> namespaceAuthenticationTypeHash;

		// Token: 0x040033CA RID: 13258
		private bool cached;
	}
}
