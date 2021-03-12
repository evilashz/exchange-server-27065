using System;
using System.Threading;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000637 RID: 1591
	internal sealed class DomainCache : TimeoutCache<SmtpDomainWithSubdomains, DomainCacheValue>
	{
		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x00115624 File Offset: 0x00113824
		public static DomainCache Singleton
		{
			get
			{
				if (DomainCache.singleton == null)
				{
					DomainCache domainCache = new DomainCache();
					Interlocked.CompareExchange<DomainCache>(ref DomainCache.singleton, domainCache, null);
					if (domainCache != DomainCache.singleton)
					{
						domainCache.Dispose();
					}
				}
				return DomainCache.singleton;
			}
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x0011565E File Offset: 0x0011385E
		public new DomainCacheValue Get(SmtpDomainWithSubdomains smtpDomainWithSubdomains)
		{
			return this.Get(smtpDomainWithSubdomains, null);
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x00115668 File Offset: 0x00113868
		public DomainCacheValue Get(SmtpDomainWithSubdomains smtpDomainWithSubdomains, OrganizationId orgId)
		{
			if (smtpDomainWithSubdomains == null)
			{
				throw new ArgumentNullException("smtpDomainWithSubdomains");
			}
			DomainCacheValue domainCacheValue;
			if (!base.TryGetValue(smtpDomainWithSubdomains, out domainCacheValue))
			{
				domainCacheValue = this.QueryFromADAndAddToCache(smtpDomainWithSubdomains, orgId);
			}
			if (domainCacheValue == null)
			{
				DomainCache.Tracer.TraceError<SmtpDomainWithSubdomains, OrganizationId>((long)this.GetHashCode(), "Unable to get domain property for domain='{0}', orgId='{1}'.", smtpDomainWithSubdomains, orgId);
			}
			return domainCacheValue;
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x00115750 File Offset: 0x00113950
		private DomainCacheValue QueryFromADAndAddToCache(SmtpDomainWithSubdomains smtpDomainWithSubdomains, OrganizationId orgId)
		{
			ADSessionSettings sessionSettings = null;
			if (Datacenter.IsMultiTenancyEnabled())
			{
				try
				{
					if (orgId != null)
					{
						sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(orgId);
					}
					else
					{
						sessionSettings = ADSessionSettings.FromTenantAcceptedDomain(smtpDomainWithSubdomains.ToString());
					}
					goto IL_51;
				}
				catch (CannotResolveTenantNameException)
				{
					sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
					goto IL_51;
				}
			}
			sessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IL_51:
			IConfigurationSession configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 159, "QueryFromADAndAddToCache", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\DomainCache.cs");
			DomainCacheValue domainCacheValue = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				AcceptedDomain acceptedDomainByDomainName = configSession.GetAcceptedDomainByDomainName(smtpDomainWithSubdomains.ToString());
				if (acceptedDomainByDomainName != null)
				{
					domainCacheValue = new DomainCacheValue();
					domainCacheValue.OrganizationId = acceptedDomainByDomainName.OrganizationId;
					domainCacheValue.LiveIdInstanceType = acceptedDomainByDomainName.LiveIdInstanceType;
					domainCacheValue.AuthenticationType = acceptedDomainByDomainName.AuthenticationType;
					this.InsertLimitedSliding(acceptedDomainByDomainName.DomainName, domainCacheValue, DomainCache.slidingTimeOut.Value, DomainCache.absoluteTimeOut.Value, null);
				}
			});
			return domainCacheValue;
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x00115800 File Offset: 0x00113A00
		private DomainCache() : base(1, DomainCache.cacheMaxSize.Value, false)
		{
		}

		// Token: 0x040033AF RID: 13231
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x040033B0 RID: 13232
		private static readonly IntAppSettingsEntry cacheMaxSize = new IntAppSettingsEntry("DomainCacheMaxSize", 2048, DomainCache.Tracer);

		// Token: 0x040033B1 RID: 13233
		private static readonly TimeSpanAppSettingsEntry absoluteTimeOut = new TimeSpanAppSettingsEntry("DomainCacheEntryAbsoluteTimeOut", TimeSpanUnit.Seconds, TimeSpan.FromHours(24.0), DomainCache.Tracer);

		// Token: 0x040033B2 RID: 13234
		private static readonly TimeSpanAppSettingsEntry slidingTimeOut = new TimeSpanAppSettingsEntry("DomainCacheEntrySlidingTimeOut", TimeSpanUnit.Seconds, TimeSpan.FromMinutes(15.0), DomainCache.Tracer);

		// Token: 0x040033B3 RID: 13235
		private static DomainCache singleton;
	}
}
