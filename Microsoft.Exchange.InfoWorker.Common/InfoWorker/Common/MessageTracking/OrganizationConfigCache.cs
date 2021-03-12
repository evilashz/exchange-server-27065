using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000309 RID: 777
	internal sealed class OrganizationConfigCache : LazyLookupTimeoutCacheWithDiagnostics<OrganizationId, OrganizationConfigCache.Item>
	{
		// Token: 0x060016FE RID: 5886 RVA: 0x0006ADA8 File Offset: 0x00068FA8
		public OrganizationConfigCache(bool multiTenancyEnabled) : base(2, 100, false, multiTenancyEnabled ? TimeSpan.FromMinutes(10.0) : TimeSpan.FromHours(5.0), multiTenancyEnabled ? TimeSpan.FromMinutes(30.0) : TimeSpan.FromHours(5.0))
		{
			if (multiTenancyEnabled)
			{
				this.domainToOrganizationIdMap = new SynchronizedDictionary<string, OrganizationId>(400, StringComparer.OrdinalIgnoreCase);
			}
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x0006AE1C File Offset: 0x0006901C
		protected override OrganizationConfigCache.Item Create(OrganizationId key, ref bool shouldAdd)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<OrganizationId>(this.GetHashCode(), "OrganizationConfigCache miss, searching for {0}", key);
			shouldAdd = true;
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), key, null, false), 143, "Create", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\MessageTracking\\Caching\\OrganizationConfigCache.cs");
			OrganizationConfigCache.Item orgSettings = OrganizationConfigCache.GetOrgSettings(tenantOrTopologyConfigurationSession, ref shouldAdd);
			if (this.domainToOrganizationIdMap == null)
			{
				shouldAdd = true;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, bool>(0, "Default Domain: {0}, Read Tracking Enabled Setting: {1}", orgSettings.DefaultDomain, orgSettings.ReadTrackingEnabled);
			if (this.domainToOrganizationIdMap != null)
			{
				foreach (string key2 in orgSettings.AuthoritativeDomains)
				{
					this.domainToOrganizationIdMap[key2] = key;
				}
			}
			return orgSettings;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x0006AF08 File Offset: 0x00069108
		protected override void HandleRemove(OrganizationId key, OrganizationConfigCache.Item value, RemoveReason reason)
		{
			base.HandleRemove(key, value, reason);
			if (this.domainToOrganizationIdMap != null)
			{
				this.domainToOrganizationIdMap.RemoveAll((OrganizationId organizationId) => key == organizationId);
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0006AF58 File Offset: 0x00069158
		private static OrganizationConfigCache.Item GetOrgSettings(IConfigurationSession tenantConfigSession, ref bool shouldAdd)
		{
			Organization orgContainer = tenantConfigSession.GetOrgContainer();
			if (orgContainer == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "ADSystemConfigurationSession.GetOrgContainer returned null", new object[0]);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "msExchOrganizationContainer object not found", new object[0]);
			}
			bool readTrackingEnabled = orgContainer.ReadTrackingEnabled;
			QueryFilter filter = new NotFilter(new BitMaskAndFilter(AcceptedDomainSchema.AcceptedDomainFlags, 1UL));
			ADPagedReader<AcceptedDomain> adpagedReader = tenantConfigSession.FindPaged<AcceptedDomain>(tenantConfigSession.GetOrgContainerId(), QueryScope.SubTree, filter, null, 0);
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			HashSet<string> hashSet2 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string text = null;
			int num = 0;
			int num2 = 0;
			foreach (AcceptedDomain acceptedDomain in adpagedReader)
			{
				num2++;
				if (acceptedDomain.Default)
				{
					text = acceptedDomain.DomainName.Domain;
				}
				if (acceptedDomain.DomainType == AcceptedDomainType.Authoritative)
				{
					if (++num > 200)
					{
						shouldAdd = false;
					}
					hashSet.Add(acceptedDomain.DomainName.Domain);
				}
				else if (acceptedDomain.DomainType == AcceptedDomainType.InternalRelay)
				{
					if (++num > 200)
					{
						shouldAdd = false;
					}
					hashSet2.Add(acceptedDomain.DomainName.Domain);
				}
			}
			if (num2 == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "No AcceptedDomain objects returned", new object[0]);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "msExchAcceptedDomain object not found in Organization {0}", new object[]
				{
					orgContainer
				});
			}
			if (string.IsNullOrEmpty(text))
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Null/Empty AcceptedDomainFqdn returned", new object[0]);
				TrackingFatalException.RaiseED(ErrorCode.InvalidADData, "DefaultDomain not found in Organization {0}", new object[]
				{
					orgContainer
				});
			}
			return new OrganizationConfigCache.Item(text, readTrackingEnabled, hashSet, hashSet2);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0006B11C File Offset: 0x0006931C
		public bool TryGetOrganizationId(string domain, out OrganizationId organizationId)
		{
			if (this.domainToOrganizationIdMap != null)
			{
				return this.domainToOrganizationIdMap.TryGetValue(domain, out organizationId);
			}
			organizationId = OrganizationId.ForestWideOrgId;
			return true;
		}

		// Token: 0x04000EB0 RID: 3760
		private const int BucketCount = 2;

		// Token: 0x04000EB1 RID: 3761
		private const int OrganizationsPerBucket = 100;

		// Token: 0x04000EB2 RID: 3762
		private const int EstimatedDomainsPerOrganization = 2;

		// Token: 0x04000EB3 RID: 3763
		private const int MaxDomainsPerOrganization = 200;

		// Token: 0x04000EB4 RID: 3764
		private SynchronizedDictionary<string, OrganizationId> domainToOrganizationIdMap;

		// Token: 0x0200030A RID: 778
		internal sealed class Item
		{
			// Token: 0x06001703 RID: 5891 RVA: 0x0006B13C File Offset: 0x0006933C
			public Item(string defaultDomain, bool readTrackingEnabled, HashSet<string> authoritativeDomains, HashSet<string> internalRelayDomains)
			{
				this.DefaultDomain = defaultDomain;
				this.ReadTrackingEnabled = readTrackingEnabled;
				this.AuthoritativeDomains = authoritativeDomains;
				this.InternalRelayDomains = internalRelayDomains;
			}

			// Token: 0x170005F3 RID: 1523
			// (get) Token: 0x06001704 RID: 5892 RVA: 0x0006B161 File Offset: 0x00069361
			// (set) Token: 0x06001705 RID: 5893 RVA: 0x0006B169 File Offset: 0x00069369
			public bool ReadTrackingEnabled { get; private set; }

			// Token: 0x170005F4 RID: 1524
			// (get) Token: 0x06001706 RID: 5894 RVA: 0x0006B172 File Offset: 0x00069372
			// (set) Token: 0x06001707 RID: 5895 RVA: 0x0006B17A File Offset: 0x0006937A
			public string DefaultDomain { get; private set; }

			// Token: 0x170005F5 RID: 1525
			// (get) Token: 0x06001708 RID: 5896 RVA: 0x0006B183 File Offset: 0x00069383
			// (set) Token: 0x06001709 RID: 5897 RVA: 0x0006B18B File Offset: 0x0006938B
			public HashSet<string> AuthoritativeDomains { get; private set; }

			// Token: 0x170005F6 RID: 1526
			// (get) Token: 0x0600170A RID: 5898 RVA: 0x0006B194 File Offset: 0x00069394
			// (set) Token: 0x0600170B RID: 5899 RVA: 0x0006B19C File Offset: 0x0006939C
			public HashSet<string> InternalRelayDomains { get; private set; }
		}
	}
}
