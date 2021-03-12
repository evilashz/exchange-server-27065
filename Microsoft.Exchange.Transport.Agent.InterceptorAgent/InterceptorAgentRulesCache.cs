using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000014 RID: 20
	internal sealed class InterceptorAgentRulesCache
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00005CBC File Offset: 0x00003EBC
		private InterceptorAgentRulesCache()
		{
			this.cache = new ConfigurationLoader<IList<InterceptorAgentRule>, InterceptorAgentRulesCache.Builder>(Components.TransportAppConfig.ADPolling.InterceptorRulesReloadInterval);
			this.cache.Changed += this.OnRulesChanged;
			Components.ConfigChanged += this.cache.Reload;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005D37 File Offset: 0x00003F37
		public static InterceptorAgentRulesCache Instance
		{
			get
			{
				return InterceptorAgentRulesCache.instance;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005D40 File Offset: 0x00003F40
		internal void Load()
		{
			if (Monitor.TryEnter(this.syncObject, TimeSpan.Zero))
			{
				try
				{
					if (!this.loaded)
					{
						this.cache.Load();
						this.loaded = true;
					}
				}
				finally
				{
					Monitor.Exit(this.syncObject);
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005D9C File Offset: 0x00003F9C
		internal void Reload()
		{
			if (Monitor.TryEnter(this.syncObject, TimeSpan.Zero))
			{
				try
				{
					this.cache.Reload(null, null);
				}
				finally
				{
					Monitor.Exit(this.syncObject);
				}
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005DE8 File Offset: 0x00003FE8
		internal void RegisterCache(FilteredRuleCache filteredRuleCache)
		{
			ArgumentValidator.ThrowIfNull("filteredRuleCache", filteredRuleCache);
			lock (this.ruleCacheGuard)
			{
				this.ruleCaches.Add(new WeakReference(filteredRuleCache));
				if (this.loaded)
				{
					this.Reload();
				}
				else
				{
					this.Load();
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005E54 File Offset: 0x00004054
		private void OnRulesChanged(IList<InterceptorAgentRule> allRulesFromAd)
		{
			List<InterceptorAgentRule> list = new List<InterceptorAgentRule>();
			lock (this.ruleCacheGuard)
			{
				List<WeakReference> list2 = new List<WeakReference>();
				foreach (WeakReference weakReference in this.ruleCaches)
				{
					FilteredRuleCache filteredRuleCache = weakReference.Target as FilteredRuleCache;
					if (filteredRuleCache == null)
					{
						list2.Add(weakReference);
					}
					else
					{
						filteredRuleCache.UpdateCache(allRulesFromAd);
						list.AddRange(filteredRuleCache.Rules);
					}
				}
				foreach (WeakReference item in list2)
				{
					this.ruleCaches.Remove(item);
				}
			}
			this.TraceNewConfiguration(list);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005F54 File Offset: 0x00004154
		private void TraceNewConfiguration(IEnumerable<InterceptorAgentRule> activeRules)
		{
			if (activeRules == null || !activeRules.Any<InterceptorAgentRule>())
			{
				return;
			}
			bool rulesEmpty = true;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<rules>\n");
			foreach (InterceptorAgentRule interceptorAgentRule in activeRules)
			{
				rulesEmpty = false;
				interceptorAgentRule.ToString(stringBuilder);
			}
			stringBuilder.Append("</rules>");
			this.TraceNewConfiguration(stringBuilder.ToString(), rulesEmpty);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005FD8 File Offset: 0x000041D8
		private void TraceNewConfiguration(string configuration, bool rulesEmpty)
		{
			ExTraceGlobals.InterceptorAgentTracer.TraceDebug((long)this.GetHashCode(), "New configuration: " + configuration);
			if (!rulesEmpty)
			{
				Util.EventLog.LogEvent(TransportEventLogConstants.Tuple_InterceptorAgentConfigurationReplaced, null, new object[]
				{
					configuration
				});
			}
		}

		// Token: 0x04000099 RID: 153
		private static readonly InterceptorAgentRulesCache instance = new InterceptorAgentRulesCache();

		// Token: 0x0400009A RID: 154
		private readonly object syncObject = new object();

		// Token: 0x0400009B RID: 155
		private readonly ConfigurationLoader<IList<InterceptorAgentRule>, InterceptorAgentRulesCache.Builder> cache;

		// Token: 0x0400009C RID: 156
		private readonly List<WeakReference> ruleCaches = new List<WeakReference>();

		// Token: 0x0400009D RID: 157
		private readonly object ruleCacheGuard = new object();

		// Token: 0x0400009E RID: 158
		private bool loaded;

		// Token: 0x02000015 RID: 21
		internal class Builder : ConfigurationLoader<IList<InterceptorAgentRule>, InterceptorAgentRulesCache.Builder>.SimpleBuilder<InterceptorRule>
		{
			// Token: 0x06000108 RID: 264 RVA: 0x00006030 File Offset: 0x00004230
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope = QueryScope.SubTree)
			{
				ArgumentValidator.ThrowIfNull("session", session);
				ADObjectId orgContainerId = session.GetOrgContainerId();
				if (orgContainerId == null)
				{
					throw new OrgContainerNotFoundException();
				}
				ADObjectId childId = orgContainerId.GetChildId("Transport Settings");
				if (childId == null)
				{
					throw new EndpointContainerNotFoundException("Transport Settings");
				}
				ADObjectId childId2 = childId.GetChildId("Interceptor Rules");
				if (childId2 == null)
				{
					throw new EndpointContainerNotFoundException("Interceptor Rules");
				}
				base.RootId = childId2;
				base.LoadData(session, QueryScope.OneLevel);
			}

			// Token: 0x06000109 RID: 265 RVA: 0x000060F0 File Offset: 0x000042F0
			protected override IList<InterceptorAgentRule> BuildCache(List<InterceptorRule> configObjects)
			{
				if (configObjects == null || configObjects.Count == 0)
				{
					return new InterceptorAgentRule[0];
				}
				List<InterceptorAgentRule> list = new List<InterceptorAgentRule>();
				List<InterceptorAgentRule> list2 = new List<InterceptorAgentRule>();
				List<InterceptorAgentRule> list3 = new List<InterceptorAgentRule>();
				foreach (InterceptorRule interceptorRule in configObjects)
				{
					InterceptorAgentRulesCache.Builder.InterceptorRuleScope ruleScope = InterceptorAgentRulesCache.Builder.GetRuleScope(interceptorRule);
					Version version;
					if (ruleScope != InterceptorAgentRulesCache.Builder.InterceptorRuleScope.None && Version.TryParse(interceptorRule.Version, out version) && version.Major <= InterceptorAgentRule.Version.Major)
					{
						try
						{
							InterceptorAgentRule interceptorAgentRule = InterceptorAgentRule.CreateRuleFromXml(interceptorRule.Xml);
							if (!interceptorAgentRule.MatchRoleAndServerVersion(Components.Configuration.ProcessTransportRole, Components.Configuration.LocalServer.TransportServer.AdminDisplayVersion))
							{
								ExTraceGlobals.InterceptorAgentTracer.TraceDebug(0L, "Skipping rule as process role and server version does not match");
							}
							else
							{
								interceptorAgentRule.SetPropertiesFromAdObjet(interceptorRule);
								switch (ruleScope)
								{
								case InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Server:
									list.Add(interceptorAgentRule);
									break;
								case InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Dag:
								case InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Site:
									list2.Add(interceptorAgentRule);
									break;
								case InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Forest:
									list3.Add(interceptorAgentRule);
									break;
								default:
									throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "InterceptorRuleScope {0} not expected", new object[]
									{
										ruleScope
									}));
								}
							}
						}
						catch (MissingFieldException arg)
						{
							ExTraceGlobals.InterceptorAgentTracer.TraceError<MissingFieldException>(0L, "Cannot parse rule from xml. Error: {0}", arg);
						}
						catch (InvalidOperationException arg2)
						{
							ExTraceGlobals.InterceptorAgentTracer.TraceError<InvalidOperationException>(0L, "Cannot parse rule from xml. Error: {0}", arg2);
						}
						catch (ArgumentException arg3)
						{
							ExTraceGlobals.InterceptorAgentTracer.TraceError<ArgumentException>(0L, "Cannot parse rule from xml. Error: {0}", arg3);
						}
						catch (FormatException arg4)
						{
							ExTraceGlobals.InterceptorAgentTracer.TraceError<FormatException>(0L, "Cannot parse rule from xml. Error: {0}", arg4);
						}
					}
				}
				IOrderedEnumerable<InterceptorAgentRule> collection = from rule in list
				orderby rule.WhenCreatedUtc.Value descending
				select rule;
				IOrderedEnumerable<InterceptorAgentRule> collection2 = from rule in list2
				orderby rule.WhenCreatedUtc.Value descending
				select rule;
				IOrderedEnumerable<InterceptorAgentRule> collection3 = from rule in list3
				orderby rule.WhenCreatedUtc.Value descending
				select rule;
				List<InterceptorAgentRule> list4 = new List<InterceptorAgentRule>(list.Count + list2.Count + list3.Count);
				list4.AddRange(collection);
				list4.AddRange(collection2);
				list4.AddRange(collection3);
				return list4;
			}

			// Token: 0x0600010A RID: 266 RVA: 0x000063C4 File Offset: 0x000045C4
			private static InterceptorAgentRulesCache.Builder.InterceptorRuleScope GetRuleScope(InterceptorRule rule)
			{
				InterceptorAgentRulesCache.Builder.InterceptorRuleScope interceptorRuleScope = InterceptorAgentRulesCache.Builder.InterceptorRuleScope.None;
				DateTime expireTimeUtc = rule.ExpireTimeUtc;
				if (expireTimeUtc < DateTime.UtcNow)
				{
					return InterceptorAgentRulesCache.Builder.InterceptorRuleScope.None;
				}
				if (rule.Target != null && rule.Target.Count > 0)
				{
					using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = rule.Target.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							ADObjectId id = enumerator.Current;
							if (Components.Configuration.LocalServer.TransportServer.Id.Equals(id))
							{
								interceptorRuleScope = InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Server;
							}
							else if (Components.Configuration.LocalServer.TransportServer.ServerSite != null && Components.Configuration.LocalServer.TransportServer.ServerSite.Equals(id))
							{
								interceptorRuleScope = InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Site;
							}
							else if (Components.Configuration.LocalServer.TransportServer.DatabaseAvailabilityGroup != null && Components.Configuration.LocalServer.TransportServer.DatabaseAvailabilityGroup.Equals(id))
							{
								interceptorRuleScope = InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Dag;
							}
							if (interceptorRuleScope == InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Server)
							{
								break;
							}
						}
						return interceptorRuleScope;
					}
				}
				interceptorRuleScope = InterceptorAgentRulesCache.Builder.InterceptorRuleScope.Forest;
				return interceptorRuleScope;
			}

			// Token: 0x0400009F RID: 159
			private const string TransportSettingsAdContainer = "Transport Settings";

			// Token: 0x040000A0 RID: 160
			private const string InterceptorRulesAdContainer = "Interceptor Rules";

			// Token: 0x02000016 RID: 22
			private enum InterceptorRuleScope
			{
				// Token: 0x040000A5 RID: 165
				None,
				// Token: 0x040000A6 RID: 166
				Server,
				// Token: 0x040000A7 RID: 167
				Dag,
				// Token: 0x040000A8 RID: 168
				Site,
				// Token: 0x040000A9 RID: 169
				Forest
			}
		}
	}
}
