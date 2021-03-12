using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000279 RID: 633
	internal class UnhealthyTargetFilter<K>
	{
		// Token: 0x06001B5B RID: 7003 RVA: 0x000700D4 File Offset: 0x0006E2D4
		public UnhealthyTargetFilter(UnhealthyTargetFilterConfiguration configuration, IEqualityComparer<K> comparer, Func<ExDateTime> currentTimeGetter)
		{
			this.tracer.TraceFunction(0L, "UnhealthyTargetFilter");
			if (configuration == null)
			{
				throw new ArgumentNullException("configuration");
			}
			this.healthyServerMap = new SynchronizedDictionary<K, UnhealthyTargetFilter<K>.HealthyTargetServerEntry>(100, comparer);
			this.unhealthyServerMap = new SynchronizedDictionary<K, UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry>(100, comparer);
			this.serverDelayMap = new SynchronizedDictionary<K, TimeSpan>(100, comparer);
			if (currentTimeGetter == null)
			{
				currentTimeGetter = (() => ExDateTime.UtcNow);
			}
			this.getCurrentTime = currentTimeGetter;
			this.configuration = configuration;
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0007016C File Offset: 0x0006E36C
		public bool Enabled
		{
			get
			{
				return this.configuration.Enabled;
			}
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000701A8 File Offset: 0x0006E3A8
		public void CleanupExpiredEntries()
		{
			this.tracer.TraceFunction(0L, "CleanupExpiredEntries");
			ExDateTime currTime = this.getCurrentTime();
			this.healthyServerMap.RemoveAll((UnhealthyTargetFilter<K>.HealthyTargetServerEntry entry) => entry.IsExpired(currTime));
			this.unhealthyServerMap.RemoveAll((UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry entry) => entry.IsExpired(currTime, this.configuration));
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0007021C File Offset: 0x0006E41C
		public bool TryMarkTargetUnhealthyIfNoConnectionOpen(K key, out ExDateTime nextRetryTime, out int currentConnectionCount, out int currentFailureCount)
		{
			this.tracer.TraceFunction<string>(0L, "TryMarkTargetUnhealthyIfNoConnectionOpen arg:{0}", (key != null) ? key.ToString() : "null");
			if (!this.configuration.Enabled)
			{
				currentConnectionCount = int.MinValue;
				nextRetryTime = ExDateTime.MinValue;
				currentFailureCount = 0;
				return false;
			}
			UnhealthyTargetFilter<K>.HealthyTargetServerEntry healthyTargetServerEntry;
			if (!this.healthyServerMap.TryGetValue(key, out healthyTargetServerEntry) || healthyTargetServerEntry.IsPlaceHolder)
			{
				UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry unhealthyTargetServerEntry = this.unhealthyServerMap.AddIfNotExists(key, (K target) => new UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry());
				unhealthyTargetServerEntry.IncrementFailureCount(this.getCurrentTime(), this.configuration);
				ExDateTime retryTime = unhealthyTargetServerEntry.GetRetryTime(this.configuration);
				this.tracer.TraceDebug<K, int, ExDateTime>(0L, "TryMarkTargetUnhealthyIfNoConnectionOpen key:{0} Failure Count:{1} Next Retry Time:{2}", key, unhealthyTargetServerEntry.FailureCount, retryTime);
				currentConnectionCount = 0;
				nextRetryTime = retryTime;
				currentFailureCount = unhealthyTargetServerEntry.FailureCount;
				return true;
			}
			UnhealthyTargetFilter<K>.HealthyTargetServerEntry healthyTargetServerEntry2 = healthyTargetServerEntry;
			currentConnectionCount = healthyTargetServerEntry2.ActiveConnectionsCount;
			currentFailureCount = 0;
			nextRetryTime = this.getCurrentTime();
			healthyTargetServerEntry.SetLastAccessed(nextRetryTime);
			this.tracer.Information<K, int>(0L, "TryMarkTargetUnhealthyIfNoConnectionOpen key:{0} is not marked as unhealthy because it has {1} connection opened", key, currentConnectionCount);
			return false;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x00070374 File Offset: 0x0006E574
		public bool TryMarkTargetInConnectingState(K key)
		{
			bool foundUnhealthyTarget = false;
			bool markedInConnectingState = false;
			this.unhealthyServerMap.ForKey(key, delegate(K targetkey, UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry unhealthyTargetServerEntry)
			{
				if (unhealthyTargetServerEntry != null)
				{
					foundUnhealthyTarget = true;
					markedInConnectingState = unhealthyTargetServerEntry.TryMarkTargetInConnectingState();
				}
			});
			return !foundUnhealthyTarget || markedInConnectingState;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000703C8 File Offset: 0x0006E5C8
		public void UnMarkTargetInConnectingState(K key)
		{
			this.unhealthyServerMap.ForKey(key, delegate(K targetkey, UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry unhealthyTargetServerEntry)
			{
				if (unhealthyTargetServerEntry != null)
				{
					unhealthyTargetServerEntry.UnMarkTargetInConnectingState();
				}
			});
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000703FC File Offset: 0x0006E5FC
		public void IncrementConnectionCountToTarget(K key)
		{
			this.tracer.TraceFunction<string>(0L, "IncrementConnectionCountToTarget arg:{0}", (key != null) ? key.ToString() : "null");
			if (!this.configuration.Enabled)
			{
				return;
			}
			UnhealthyTargetFilter<K>.HealthyTargetServerEntry healthyTargetServerEntry = this.healthyServerMap.AddIfNotExists(key, (K target) => new UnhealthyTargetFilter<K>.HealthyTargetServerEntry());
			healthyTargetServerEntry.IncrementActiveConnectionsCount(this.getCurrentTime());
			this.tracer.Information<K, int>(0L, "IncrementConnectionCountToTarget key:{0} New Connection Count {1}.", key, healthyTargetServerEntry.ActiveConnectionsCount);
			this.unhealthyServerMap.Remove(key);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000704A8 File Offset: 0x0006E6A8
		public void DecrementConnectionCountToTarget(K key)
		{
			this.tracer.TraceFunction<string>(0L, "DecrementConnectionCountToTarget arg:{0}", (key != null) ? key.ToString() : "null");
			if (!this.configuration.Enabled)
			{
				return;
			}
			UnhealthyTargetFilter<K>.HealthyTargetServerEntry healthyTargetServerEntry;
			if (!this.healthyServerMap.TryGetValue(key, out healthyTargetServerEntry) || healthyTargetServerEntry.IsPlaceHolder)
			{
				throw new InvalidOperationException("connection to healthy server can't be negative.");
			}
			UnhealthyTargetFilter<K>.HealthyTargetServerEntry healthyTargetServerEntry2 = healthyTargetServerEntry;
			healthyTargetServerEntry2.DecrementActiveConnectionsCount(this.getCurrentTime());
			this.tracer.Information<K, int>(0L, "DecrementConnectionCountToTarget key:{0} New Connection Count {1}.", key, healthyTargetServerEntry2.ActiveConnectionsCount);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00070544 File Offset: 0x0006E744
		public List<K> FilterOutUnhealthyTargets(List<K> listToBeFiltered, out bool allUnhealthyEntries)
		{
			return this.FilterOutUnhealthyTargets<K>(listToBeFiltered, (K key) => key, out allUnhealthyEntries);
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0007056C File Offset: 0x0006E76C
		public T[] FilterOutUnhealthyTargets<T>(T[] targetsToBeFiltered, Func<T, K> objectToKeyMapper)
		{
			if (targetsToBeFiltered == null || targetsToBeFiltered.Length < 1)
			{
				return targetsToBeFiltered;
			}
			List<T> list = new List<T>(targetsToBeFiltered);
			bool flag;
			List<T> list2 = this.FilterOutUnhealthyTargets<T>(list, objectToKeyMapper, out flag);
			if (list.Count == list2.Count)
			{
				return targetsToBeFiltered;
			}
			return list2.ToArray();
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00070614 File Offset: 0x0006E814
		public List<T> FilterOutUnhealthyTargets<T>(List<T> targetHostToBeFiltered, Func<T, K> objectToKeyMapper, out bool allUnhealthyEntries)
		{
			if (!this.configuration.Enabled || targetHostToBeFiltered == null || targetHostToBeFiltered.Count == 0)
			{
				allUnhealthyEntries = false;
				return targetHostToBeFiltered;
			}
			Dictionary<K, List<T>> keyToTargetHostMap = new Dictionary<K, List<T>>(targetHostToBeFiltered.Count);
			foreach (T t in targetHostToBeFiltered)
			{
				List<T> list = null;
				K key2 = objectToKeyMapper(t);
				if (!keyToTargetHostMap.TryGetValue(key2, out list))
				{
					list = new List<T>(1);
					keyToTargetHostMap.Add(key2, list);
				}
				list.Add(t);
			}
			List<T> healthyServers = null;
			ExDateTime currTime = this.getCurrentTime();
			this.unhealthyServerMap.ForEachKey(keyToTargetHostMap.Keys, null, delegate(K key, UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry unhealthyServerEntry)
			{
				if (unhealthyServerEntry == null || unhealthyServerEntry.ShouldRetryUnhealthyServer(currTime, this.configuration))
				{
					if (healthyServers == null)
					{
						healthyServers = new List<T>(targetHostToBeFiltered.Count);
					}
					healthyServers.AddRange(keyToTargetHostMap[key]);
				}
			});
			allUnhealthyEntries = (healthyServers == null);
			if (healthyServers == null || healthyServers.Count == targetHostToBeFiltered.Count)
			{
				healthyServers = targetHostToBeFiltered;
			}
			return healthyServers;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00070764 File Offset: 0x0006E964
		public bool IsUnhealthy(K target)
		{
			UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry unhealthyTargetServerEntry;
			return this.unhealthyServerMap.TryGetValue(target, out unhealthyTargetServerEntry) && !unhealthyTargetServerEntry.ShouldRetryUnhealthyServer(this.getCurrentTime(), this.configuration);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00070928 File Offset: 0x0006EB28
		internal void AddDiagnosticInfoTo(XElement unhealthyTargetFiltererElement, bool showVerbose)
		{
			this.tracer.TraceFunction<bool>(0L, "AddDiagnosticInfoTo verbose:{0}", showVerbose);
			if (unhealthyTargetFiltererElement == null)
			{
				throw new ArgumentNullException("unhealthyTargetFiltererElement");
			}
			unhealthyTargetFiltererElement.Add(new XElement("UnhealthyTargetsCount", this.unhealthyServerMap.Count));
			if (showVerbose)
			{
				XElement unhealthyTargetsElement = new XElement("UnhealthyTargets");
				ExDateTime currTime = this.getCurrentTime();
				this.unhealthyServerMap.ForEach(null, delegate(K server, UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry unhealthyServer)
				{
					XElement xelement = new XElement("Server", server.ToString());
					xelement.Add(new object[]
					{
						new XElement("FailureCount", unhealthyServer.FailureCount),
						new XElement("LastRetry", unhealthyServer.LastRetryTime),
						new XElement("Retryable", unhealthyServer.ShouldRetryUnhealthyServer(currTime, this.configuration)),
						new XElement("NextRetryScheduled", unhealthyServer.GetRetryTime(this.configuration))
					});
					unhealthyTargetsElement.Add(xelement);
				});
				XElement healthyTargetsElement = new XElement("HealthyTargets");
				this.healthyServerMap.ForEach(null, delegate(K server, UnhealthyTargetFilter<K>.HealthyTargetServerEntry healthyServer)
				{
					XElement xelement = new XElement("Server", server.ToString());
					xelement.Add(new XElement("ActiveConnectionsCount", healthyServer.ActiveConnectionsCount));
					healthyTargetsElement.Add(xelement);
				});
				this.serverDelayMap.ForEach(null, delegate(K server, TimeSpan delay)
				{
					XElement xelement = new XElement("Server", server.ToString());
					xelement.Add(new XElement("Delay", delay));
					healthyTargetsElement.Add(xelement);
				});
				unhealthyTargetFiltererElement.Add(healthyTargetsElement);
				unhealthyTargetFiltererElement.Add(unhealthyTargetsElement);
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00070A29 File Offset: 0x0006EC29
		public void UpdateServerLatency(K key, TimeSpan timeSpan)
		{
			this.serverDelayMap[key] = timeSpan;
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00070A38 File Offset: 0x0006EC38
		public TimeSpan GetServerLatency(K smtpTarget)
		{
			TimeSpan result;
			if (this.serverDelayMap.TryGetValue(smtpTarget, out result))
			{
				return result;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x04000CE6 RID: 3302
		private const int DictionaryInitialCapacity = 100;

		// Token: 0x04000CE7 RID: 3303
		private Func<ExDateTime> getCurrentTime;

		// Token: 0x04000CE8 RID: 3304
		private UnhealthyTargetFilterConfiguration configuration;

		// Token: 0x04000CE9 RID: 3305
		private SynchronizedDictionary<K, UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry> unhealthyServerMap;

		// Token: 0x04000CEA RID: 3306
		private SynchronizedDictionary<K, UnhealthyTargetFilter<K>.HealthyTargetServerEntry> healthyServerMap;

		// Token: 0x04000CEB RID: 3307
		private SynchronizedDictionary<K, TimeSpan> serverDelayMap;

		// Token: 0x04000CEC RID: 3308
		private Trace tracer = ExTraceGlobals.RoutingTracer;

		// Token: 0x0200027A RID: 634
		private sealed class UnhealthyTargetServerEntry
		{
			// Token: 0x1700073A RID: 1850
			// (get) Token: 0x06001B6F RID: 7023 RVA: 0x00070A5C File Offset: 0x0006EC5C
			public int FailureCount
			{
				get
				{
					return this.failureCount;
				}
			}

			// Token: 0x1700073B RID: 1851
			// (get) Token: 0x06001B70 RID: 7024 RVA: 0x00070A64 File Offset: 0x0006EC64
			public ExDateTime LastRetryTime
			{
				get
				{
					return this.lastRetryTime;
				}
			}

			// Token: 0x06001B71 RID: 7025 RVA: 0x00070A6C File Offset: 0x0006EC6C
			public bool IsExpired(ExDateTime currTime, UnhealthyTargetFilterConfiguration configuration)
			{
				return this.inConnectingState == 0 && this.GetRetryTime(configuration) + UnhealthyTargetFilter<K>.UnhealthyTargetServerEntry.ExpiryInterval <= currTime;
			}

			// Token: 0x06001B72 RID: 7026 RVA: 0x00070A90 File Offset: 0x0006EC90
			public void IncrementFailureCount(ExDateTime currTime, UnhealthyTargetFilterConfiguration configuration)
			{
				int num = this.failureCount + 1;
				ExDateTime retryTime = this.GetRetryTime(configuration);
				if (retryTime < currTime)
				{
					this.lastRetryTime = currTime;
					this.failureCount = num;
				}
				this.inConnectingState = 0;
			}

			// Token: 0x06001B73 RID: 7027 RVA: 0x00070ACC File Offset: 0x0006ECCC
			public bool ShouldRetryUnhealthyServer(ExDateTime currTime, UnhealthyTargetFilterConfiguration configuration)
			{
				return currTime >= this.GetRetryTime(configuration) && this.inConnectingState == 0;
			}

			// Token: 0x06001B74 RID: 7028 RVA: 0x00070AE8 File Offset: 0x0006ECE8
			public ExDateTime GetRetryTime(UnhealthyTargetFilterConfiguration configuration)
			{
				if (this.failureCount == 0)
				{
					return ExDateTime.MinValue;
				}
				TimeSpan t;
				if (this.failureCount <= configuration.GlitchRetryCount)
				{
					t = configuration.GlitchRetryInterval;
				}
				else if (this.failureCount <= configuration.GlitchRetryCount + configuration.TransientFailureRetryCount)
				{
					t = configuration.TransientFailureRetryInterval;
				}
				else
				{
					t = configuration.OutboundConnectionFailureRetryInterval;
				}
				return this.lastRetryTime + t;
			}

			// Token: 0x06001B75 RID: 7029 RVA: 0x00070B4D File Offset: 0x0006ED4D
			public bool TryMarkTargetInConnectingState()
			{
				return Interlocked.CompareExchange(ref this.inConnectingState, 1, 0) == 0;
			}

			// Token: 0x06001B76 RID: 7030 RVA: 0x00070B5F File Offset: 0x0006ED5F
			public void UnMarkTargetInConnectingState()
			{
				this.inConnectingState = 0;
			}

			// Token: 0x04000CF2 RID: 3314
			private static readonly TimeSpan ExpiryInterval = TimeSpan.FromMinutes(10.0);

			// Token: 0x04000CF3 RID: 3315
			private int failureCount;

			// Token: 0x04000CF4 RID: 3316
			private ExDateTime lastRetryTime;

			// Token: 0x04000CF5 RID: 3317
			private int inConnectingState;
		}

		// Token: 0x0200027B RID: 635
		private sealed class HealthyTargetServerEntry
		{
			// Token: 0x1700073C RID: 1852
			// (get) Token: 0x06001B79 RID: 7033 RVA: 0x00070B85 File Offset: 0x0006ED85
			public bool IsPlaceHolder
			{
				get
				{
					return this.ActiveConnectionsCount == 0;
				}
			}

			// Token: 0x1700073D RID: 1853
			// (get) Token: 0x06001B7A RID: 7034 RVA: 0x00070B90 File Offset: 0x0006ED90
			public int ActiveConnectionsCount
			{
				get
				{
					return this.activeConnectionsCount;
				}
			}

			// Token: 0x06001B7B RID: 7035 RVA: 0x00070B98 File Offset: 0x0006ED98
			public void SetLastAccessed(ExDateTime currTime)
			{
				this.lastAccessed = currTime;
			}

			// Token: 0x06001B7C RID: 7036 RVA: 0x00070BA1 File Offset: 0x0006EDA1
			public bool IsExpired(ExDateTime currTime)
			{
				return this.ActiveConnectionsCount == 0 && this.lastAccessed + UnhealthyTargetFilter<K>.HealthyTargetServerEntry.expiryInterval <= currTime;
			}

			// Token: 0x06001B7D RID: 7037 RVA: 0x00070BC3 File Offset: 0x0006EDC3
			public int IncrementActiveConnectionsCount(ExDateTime currTime)
			{
				this.lastAccessed = currTime;
				return Interlocked.Increment(ref this.activeConnectionsCount);
			}

			// Token: 0x06001B7E RID: 7038 RVA: 0x00070BD7 File Offset: 0x0006EDD7
			public int DecrementActiveConnectionsCount(ExDateTime currTime)
			{
				this.lastAccessed = currTime;
				return Interlocked.Decrement(ref this.activeConnectionsCount);
			}

			// Token: 0x04000CF6 RID: 3318
			private static readonly TimeSpan expiryInterval = TimeSpan.FromMinutes(1.0);

			// Token: 0x04000CF7 RID: 3319
			private int activeConnectionsCount;

			// Token: 0x04000CF8 RID: 3320
			private ExDateTime lastAccessed;
		}
	}
}
