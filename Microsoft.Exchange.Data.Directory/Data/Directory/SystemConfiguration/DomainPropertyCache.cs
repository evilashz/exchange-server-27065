using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000635 RID: 1589
	internal sealed class DomainPropertyCache : LazyLookupExactTimeoutCache<SmtpDomainWithSubdomains, DomainProperties>
	{
		// Token: 0x170018D6 RID: 6358
		// (get) Token: 0x06004B2D RID: 19245 RVA: 0x00115334 File Offset: 0x00113534
		public static DomainPropertyCache Singleton
		{
			get
			{
				if (DomainPropertyCache.instance == null)
				{
					lock (DomainPropertyCache.singletonLocker)
					{
						if (DomainPropertyCache.instance == null)
						{
							DomainPropertyCache.instance = new DomainPropertyCache();
						}
					}
				}
				return DomainPropertyCache.instance;
			}
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x0011538C File Offset: 0x0011358C
		private DomainPropertyCache() : base(DomainPropertyCache.MaxCacheCount.Value, false, DomainPropertyCache.SlidingLiveTime.Value, DomainPropertyCache.AbsoluteLiveTime.Value, CacheFullBehavior.ExpireExisting)
		{
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x001153B4 File Offset: 0x001135B4
		protected override DomainProperties CreateOnCacheMiss(SmtpDomainWithSubdomains key, ref bool shouldAdd)
		{
			DomainProperties result = this.QueryFromAD(key);
			shouldAdd = true;
			return result;
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x0011545C File Offset: 0x0011365C
		private DomainProperties QueryFromAD(SmtpDomainWithSubdomains smtpDomainWithSubdomains)
		{
			ADSessionSettings adsessionSettings = null;
			if (Datacenter.IsMultiTenancyEnabled())
			{
				try
				{
					adsessionSettings = ADSessionSettings.FromTenantAcceptedDomain(smtpDomainWithSubdomains.ToString());
					goto IL_5C;
				}
				catch (CannotResolveTenantNameException arg)
				{
					DomainPropertyCache.Tracer.TraceError<SmtpDomainWithSubdomains, CannotResolveTenantNameException>((long)this.GetHashCode(), "Failed to resolve tenant with domain {0}. Error: {1}", smtpDomainWithSubdomains, arg);
					adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
					goto IL_5C;
				}
			}
			adsessionSettings = ADSessionSettings.FromRootOrgScopeSet();
			IL_5C:
			DomainPropertyCache.Tracer.TraceDebug<SmtpDomainWithSubdomains, OrganizationId>((long)this.GetHashCode(), "The OrganizationId for domain {0} is {1}.", smtpDomainWithSubdomains, adsessionSettings.CurrentOrganizationId);
			IConfigurationSession configSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, adsessionSettings, 219, "QueryFromAD", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\DomainPropertyCache.cs");
			DomainProperties domainProperty = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				AcceptedDomain acceptedDomainByDomainName = configSession.GetAcceptedDomainByDomainName(smtpDomainWithSubdomains.ToString());
				if (acceptedDomainByDomainName != null)
				{
					domainProperty = new DomainProperties(smtpDomainWithSubdomains.SmtpDomain);
					domainProperty.OrganizationId = acceptedDomainByDomainName.OrganizationId;
					domainProperty.LiveIdInstanceType = acceptedDomainByDomainName.LiveIdInstanceType;
					DomainPropertyCache.Tracer.TraceDebug<AcceptedDomain, OrganizationId>((long)this.GetHashCode(), "Accepted domain {0} in organization {1}", acceptedDomainByDomainName, acceptedDomainByDomainName.OrganizationId);
				}
			});
			return domainProperty;
		}

		// Token: 0x040033A8 RID: 13224
		private static object singletonLocker = new object();

		// Token: 0x040033A9 RID: 13225
		private static DomainPropertyCache instance;

		// Token: 0x040033AA RID: 13226
		private static readonly Trace Tracer = ExTraceGlobals.SystemConfigurationCacheTracer;

		// Token: 0x040033AB RID: 13227
		private static readonly IntAppSettingsEntry MaxCacheCount = new IntAppSettingsEntry("DomainPropertyCache.MaxCacheCount", 10000, DomainPropertyCache.Tracer);

		// Token: 0x040033AC RID: 13228
		private static readonly TimeSpanAppSettingsEntry SlidingLiveTime = new TimeSpanAppSettingsEntry("DomainPropertyCache.SlidingLiveTime", TimeSpanUnit.Minutes, TimeSpan.FromMinutes(30.0), DomainPropertyCache.Tracer);

		// Token: 0x040033AD RID: 13229
		private static readonly TimeSpanAppSettingsEntry AbsoluteLiveTime = new TimeSpanAppSettingsEntry("DomainPropertyCache.AbsoluteLiveTime", TimeSpanUnit.Minutes, TimeSpan.FromHours(6.0), DomainPropertyCache.Tracer);
	}
}
