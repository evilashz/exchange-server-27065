using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000019 RID: 25
	internal class TargetForestAnchorMailbox : DatabaseBasedAnchorMailbox
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00004E8C File Offset: 0x0000308C
		public TargetForestAnchorMailbox(IRequestContext requestContext, string domain, bool supportCookieBasedAffinity) : base(AnchorSource.Domain, domain, requestContext)
		{
			this.supportCookieBasedAffinity = supportCookieBasedAffinity;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004EA0 File Offset: 0x000030A0
		public override ADObjectId GetDatabase()
		{
			if (TargetForestAnchorMailbox.RandomDatabaseInTargetForestEnabled.Value)
			{
				string domain = (string)base.SourceObject;
				ADObjectId randomDatabaseFromDomain = this.GetRandomDatabaseFromDomain(domain);
				if (randomDatabaseFromDomain != null)
				{
					base.RequestContext.Logger.AppendGenericInfo("Database", randomDatabaseFromDomain.ObjectGuid);
				}
				return randomDatabaseFromDomain;
			}
			return base.GetDatabase();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004EF8 File Offset: 0x000030F8
		public override BackEndServer TryDirectBackEndCalculation()
		{
			if (TargetForestAnchorMailbox.RandomBackEndInSameForestEnabled.Value)
			{
				base.RequestContext.Logger.AppendString(HttpProxyMetadata.RoutingHint, "-SameForestRandomBackend");
				if (TargetForestAnchorMailbox.PreferServersCacheForRandomBackEnd.Value)
				{
					try
					{
						return HttpProxyBackEndHelper.GetAnyBackEndServer(true);
					}
					catch (ServerHasNotBeenFoundException ex)
					{
						base.RequestContext.Logger.AppendGenericError("ServersCacheErr", ex.ToString());
					}
				}
				return HttpProxyBackEndHelper.GetAnyBackEndServer(false);
			}
			return base.TryDirectBackEndCalculation();
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004F80 File Offset: 0x00003180
		public override BackEndCookieEntryBase BuildCookieEntryForTarget(BackEndServer routingTarget, bool proxyToDownLevel, bool useResourceForest)
		{
			if (this.supportCookieBasedAffinity)
			{
				return base.BuildCookieEntryForTarget(routingTarget, proxyToDownLevel, useResourceForest);
			}
			return null;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004F95 File Offset: 0x00003195
		public override BackEndServer AcceptBackEndCookie(HttpCookie backEndCookie)
		{
			if (this.supportCookieBasedAffinity)
			{
				return base.AcceptBackEndCookie(backEndCookie);
			}
			return null;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004FA8 File Offset: 0x000031A8
		protected override AnchorMailboxCacheEntry LoadCacheEntryFromIncomingCookie()
		{
			if (this.supportCookieBasedAffinity)
			{
				return base.LoadCacheEntryFromIncomingCookie();
			}
			return null;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004FDC File Offset: 0x000031DC
		private string GetResourceForestFqdnByAcceptedDomainName(string tenantAcceptedDomain)
		{
			string resourceForestFqdn;
			if (!TargetForestAnchorMailbox.domainToResourceForestMap.TryGetValue(tenantAcceptedDomain, out resourceForestFqdn))
			{
				long latency = 0L;
				resourceForestFqdn = LatencyTracker.GetLatency<string>(delegate()
				{
					resourceForestFqdn = ADAccountPartitionLocator.GetResourceForestFqdnByAcceptedDomainName(tenantAcceptedDomain);
					return resourceForestFqdn;
				}, out latency);
				TargetForestAnchorMailbox.domainToResourceForestMap.TryInsertAbsolute(tenantAcceptedDomain, resourceForestFqdn, TargetForestAnchorMailbox.DomainForestAbsoluteTimeout.Value);
				base.RequestContext.LatencyTracker.HandleGlsLatency(latency);
			}
			return resourceForestFqdn;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000050A8 File Offset: 0x000032A8
		private ADObjectId GetRandomDatabasesFromForest(string resourceForestFqdn)
		{
			List<ADObjectId> list = null;
			bool flag = TargetForestAnchorMailbox.resourceForestToDatabaseMap.TryGetValue(resourceForestFqdn, out list);
			if (!flag || list == null || list.Count <= 0)
			{
				lock (TargetForestAnchorMailbox.forestDatabaseLock)
				{
					flag = TargetForestAnchorMailbox.resourceForestToDatabaseMap.TryGetValue(resourceForestFqdn, out list);
					if (!flag || list == null || list.Count <= 0)
					{
						list = new List<ADObjectId>();
						PartitionId partitionId = new PartitionId(resourceForestFqdn);
						ITopologyConfigurationSession resourceForestSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(partitionId), 318, "GetRandomDatabasesFromForest", "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\AnchorMailbox\\TargetForestAnchorMailbox.cs");
						SortBy sortBy = new SortBy(ADObjectSchema.WhenCreatedUTC, SortOrder.Ascending);
						List<PropertyDefinition> databaseSchema = new List<PropertyDefinition>
						{
							ADObjectSchema.Id
						};
						long latency = 0L;
						ADPagedReader<ADRawEntry> latency2 = LatencyTracker.GetLatency<ADPagedReader<ADRawEntry>>(() => resourceForestSession.FindPagedADRawEntry(resourceForestSession.ConfigurationNamingContext, QueryScope.SubTree, TargetForestAnchorMailbox.DatabaseQueryFilter, sortBy, TargetForestAnchorMailbox.DatabasesToLoadPerForest.Value, databaseSchema), out latency);
						base.RequestContext.LatencyTracker.HandleResourceLatency(latency);
						if (latency2 != null)
						{
							foreach (ADRawEntry adrawEntry in latency2)
							{
								list.Add(adrawEntry.Id);
							}
						}
						if (list.Count > 0)
						{
							TargetForestAnchorMailbox.resourceForestToDatabaseMap[resourceForestFqdn] = list;
							if (list.Count < TargetForestAnchorMailbox.MinimumDatabasesForEffectiveLoadBalancing.Value)
							{
								base.RequestContext.Logger.AppendGenericError("TooFewDbsForLoadBalancing", string.Format("DbCount:{0}/Forest:{1}", list.Count, resourceForestFqdn));
							}
						}
					}
				}
			}
			if (list != null && list.Count > 0)
			{
				int index = TargetForestAnchorMailbox.seededRand.Next(0, list.Count);
				return list[index];
			}
			return null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000052A8 File Offset: 0x000034A8
		private ADObjectId GetRandomDatabaseFromDomain(string domain)
		{
			string resourceForestFqdnByAcceptedDomainName = this.GetResourceForestFqdnByAcceptedDomainName(domain);
			base.RequestContext.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "TargetForest-RandomDatabase");
			return this.GetRandomDatabasesFromForest(resourceForestFqdnByAcceptedDomainName);
		}

		// Token: 0x0400003C RID: 60
		private const string PrivateDatabaseObjectClass = "msExchPrivateMDB";

		// Token: 0x0400003D RID: 61
		private const string PublicDatabaseObjectClass = "msExchPublicMDB";

		// Token: 0x0400003E RID: 62
		private static readonly BoolAppSettingsEntry RandomBackEndInSameForestEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("RandomBackEndInSameForestEnabled"), true, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400003F RID: 63
		private static readonly BoolAppSettingsEntry RandomDatabaseInTargetForestEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("RandomDatabaseInTargetForestEnabled"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000040 RID: 64
		private static readonly BoolAppSettingsEntry PreferServersCacheForRandomBackEnd = new BoolAppSettingsEntry(HttpProxySettings.Prefix("PreferServersCacheForRandomBackEnd"), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Cafe.PreferServersCacheForRandomBackEnd.Enabled, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000041 RID: 65
		private static readonly TimeSpanAppSettingsEntry DomainForestAbsoluteTimeout = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("DomainForestAbsoluteTimeout"), TimeSpanUnit.Minutes, TimeSpan.FromMinutes(1440.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000042 RID: 66
		private static readonly IntAppSettingsEntry MinimumDatabasesForEffectiveLoadBalancing = new IntAppSettingsEntry(HttpProxySettings.Prefix("MinimumDbsForEffectiveLoadBalancing"), 100, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000043 RID: 67
		private static readonly IntAppSettingsEntry DatabasesToLoadPerForest = new IntAppSettingsEntry(HttpProxySettings.Prefix("DatabasesToLoadPerForest"), 1000, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000044 RID: 68
		private static readonly IntAppSettingsEntry DomainToForestMapSize = new IntAppSettingsEntry(HttpProxySettings.Prefix("DomainToForestMapSize"), 100, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000045 RID: 69
		private static readonly DateTime MaximumE14DatabaseCreationDate = new DateTime(2013, 6, 1);

		// Token: 0x04000046 RID: 70
		private static readonly QueryFilter DatabaseQueryFilter = new AndFilter(new QueryFilter[]
		{
			new ComparisonFilter(ComparisonOperator.GreaterThan, ADObjectSchema.WhenCreatedUTC, TargetForestAnchorMailbox.MaximumE14DatabaseCreationDate),
			new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "msExchPrivateMDB"),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectClass, "msExchPublicMDB")
			})
		});

		// Token: 0x04000047 RID: 71
		private static ExactTimeoutCache<string, string> domainToResourceForestMap = new ExactTimeoutCache<string, string>(null, null, null, TargetForestAnchorMailbox.DomainToForestMapSize.Value, false);

		// Token: 0x04000048 RID: 72
		private static object forestDatabaseLock = new object();

		// Token: 0x04000049 RID: 73
		private static ConcurrentDictionary<string, List<ADObjectId>> resourceForestToDatabaseMap = new ConcurrentDictionary<string, List<ADObjectId>>();

		// Token: 0x0400004A RID: 74
		private static Random seededRand = new Random(DateTime.Now.Millisecond);

		// Token: 0x0400004B RID: 75
		private bool supportCookieBasedAffinity;
	}
}
