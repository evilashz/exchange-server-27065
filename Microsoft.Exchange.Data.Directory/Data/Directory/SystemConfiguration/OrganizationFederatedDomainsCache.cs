using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200063D RID: 1597
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OrganizationFederatedDomainsCache : OrganizationBaseCache
	{
		// Token: 0x06004B5B RID: 19291 RVA: 0x00115B35 File Offset: 0x00113D35
		public OrganizationFederatedDomainsCache(OrganizationId organizationId, OrganizationFederatedOrganizationIdCache federatedOrganizationIdCache, IConfigurationSession session) : base(organizationId, session)
		{
			this.federatedOrganizationIdCache = federatedOrganizationIdCache;
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x06004B5C RID: 19292 RVA: 0x00115B46 File Offset: 0x00113D46
		public IEnumerable<string> Value
		{
			get
			{
				this.PopulateCacheIfNeeded();
				return this.value;
			}
		}

		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x06004B5D RID: 19293 RVA: 0x00115B54 File Offset: 0x00113D54
		public string DefaultDomain
		{
			get
			{
				return this.defaultDomain;
			}
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x00115B5C File Offset: 0x00113D5C
		private void PopulateCacheIfNeeded()
		{
			if (!this.cached)
			{
				this.Populate();
				this.cached = true;
			}
		}

		// Token: 0x06004B5F RID: 19295 RVA: 0x00115B9C File Offset: 0x00113D9C
		private void Populate()
		{
			FederatedOrganizationId federatedOrganizationId = this.federatedOrganizationIdCache.Value;
			if (federatedOrganizationId == null)
			{
				OrganizationBaseCache.Tracer.TraceError<OrganizationId>((long)this.GetHashCode(), "Unable to find the FederatedOrganizationId associated with the organization '{0}'", base.OrganizationId);
				return;
			}
			OrganizationBaseCache.Tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "Searching for AcceptedDomain instances associated with FederatedOrganizationId '{0}'", federatedOrganizationId.Id);
			AcceptedDomain[] acceptedDomains = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				acceptedDomains = this.Session.FindAcceptedDomainsByFederatedOrgId(federatedOrganizationId);
			});
			if (!adoperationResult.Succeeded)
			{
				OrganizationBaseCache.Tracer.TraceError<ADObjectId, Exception>((long)this.GetHashCode(), "Unable to find AcceptedDomain instances associated with the FederatedOrganizationId '{0}' due to exception: {1}", federatedOrganizationId.Id, adoperationResult.Exception);
				return;
			}
			if (acceptedDomains == null || acceptedDomains.Length == 0)
			{
				OrganizationBaseCache.Tracer.TraceError<ADObjectId>((long)this.GetHashCode(), "Unable to find any federated AcceptedDomain associated with the FederatedOrganizationId '{0}'", federatedOrganizationId.Id);
				return;
			}
			string[] array = new string[acceptedDomains.Length];
			for (int i = 0; i < acceptedDomains.Length; i++)
			{
				array[i] = acceptedDomains[i].DomainName.Domain;
			}
			string text = null;
			foreach (AcceptedDomain acceptedDomain in acceptedDomains)
			{
				if (acceptedDomain.IsDefaultFederatedDomain)
				{
					OrganizationBaseCache.Tracer.TraceDebug<SmtpDomainWithSubdomains>((long)this.GetHashCode(), "Found AcceptedDomain '{0}' as default federated domain", acceptedDomain.DomainName);
					text = acceptedDomain.DomainName.Domain;
					break;
				}
			}
			OrganizationBaseCache.Tracer.TraceDebug<ADObjectId, ArrayTracer<string>>((long)this.GetHashCode(), "Found the following domains associated with FederatedOrganizationId '{0}': {1}", federatedOrganizationId.Id, new ArrayTracer<string>(array));
			this.value = array;
			this.defaultDomain = text;
		}

		// Token: 0x040033C5 RID: 13253
		private OrganizationFederatedOrganizationIdCache federatedOrganizationIdCache;

		// Token: 0x040033C6 RID: 13254
		private string[] value;

		// Token: 0x040033C7 RID: 13255
		private bool cached;

		// Token: 0x040033C8 RID: 13256
		private string defaultDomain;
	}
}
