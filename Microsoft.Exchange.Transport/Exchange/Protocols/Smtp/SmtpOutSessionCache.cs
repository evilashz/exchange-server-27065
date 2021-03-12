using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000516 RID: 1302
	internal class SmtpOutSessionCache : IDisposable
	{
		// Token: 0x06003CF9 RID: 15609 RVA: 0x000FE628 File Offset: 0x000FC828
		public SmtpOutSessionCache(int maxEntriesForOutboundProxy, int maxEntriesForNonOutboundProxy, TimeSpan smtpConnectionTimeout, TimeSpan smtpConnectionInactivityTimeout, ICachePerformanceCounters perfCountersForOutboundProxy, ICachePerformanceCounters perfCountersForNonOutboundProxy)
		{
			ArgumentValidator.ThrowIfNegative("maxEntriesForOutboundProxy", maxEntriesForOutboundProxy);
			ArgumentValidator.ThrowIfNegative("maxEntriesForNonOutboundProxy", maxEntriesForNonOutboundProxy);
			if (smtpConnectionTimeout.TotalSeconds < 5.0)
			{
				throw new ArgumentOutOfRangeException("smtpConnectionTimeOut", smtpConnectionTimeout, "SMTP Connection Timeout must be greater than or equal to 5 seconds");
			}
			if (smtpConnectionInactivityTimeout.TotalSeconds < 5.0)
			{
				throw new ArgumentOutOfRangeException("smtpConnectionInactivityTimeOut", smtpConnectionInactivityTimeout, "SMTP Connection Inactivity Timeout must be greater than or equal to 5 seconds");
			}
			this.cachePerfCountersForOutboundProxy = perfCountersForOutboundProxy;
			this.cachePerfCountersForNonOutboundProxy = perfCountersForNonOutboundProxy;
			this.maxCacheEntriesForOutboundProxy = maxEntriesForOutboundProxy;
			this.maxCacheEntriesForNonOutboundProxy = maxEntriesForNonOutboundProxy;
			this.currentCacheEntriesForNonOutboundProxy = 0;
			this.currentCacheEntriesForOutboundProxy = 0;
			this.mruListForNonOutboundProxy = new LinkedList<SmtpOutSessionCache.CacheItemWrapper>();
			this.mruListForOutboundProxy = new LinkedList<SmtpOutSessionCache.CacheItemWrapper>();
			this.connectionTimeout = smtpConnectionTimeout;
			this.connectionInactivityTimeout = smtpConnectionInactivityTimeout;
			this.cacheExpirationCheckInterval = ((smtpConnectionInactivityTimeout < smtpConnectionTimeout) ? smtpConnectionInactivityTimeout : smtpConnectionTimeout);
			this.sessionCache = new Dictionary<MultiValueKey, LinkedList<SmtpOutSessionCache.CacheItemWrapper>>();
			this.expiryTimer = new GuardedTimer(new TimerCallback(this.HandleExpiry), null, this.cacheExpirationCheckInterval, this.cacheExpirationCheckInterval);
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x000FE744 File Offset: 0x000FC944
		private int GetMaxEntriesByNextHop(NextHopSolutionKey nextHopKey)
		{
			if (SmtpOutSessionCache.OutboundFrontendCacheKey.Equals(nextHopKey))
			{
				return this.maxCacheEntriesForOutboundProxy;
			}
			return this.maxCacheEntriesForNonOutboundProxy;
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x000FE770 File Offset: 0x000FC970
		private ICachePerformanceCounters GetCachePerfCountersByNextHop(NextHopSolutionKey nextHopKey)
		{
			if (SmtpOutSessionCache.OutboundFrontendCacheKey.Equals(nextHopKey))
			{
				return this.cachePerfCountersForOutboundProxy;
			}
			return this.cachePerfCountersForNonOutboundProxy;
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000FE79C File Offset: 0x000FC99C
		private int GetCurrentEntriesByNextHop(NextHopSolutionKey nextHopKey)
		{
			if (SmtpOutSessionCache.OutboundFrontendCacheKey.Equals(nextHopKey))
			{
				return this.currentCacheEntriesForOutboundProxy;
			}
			return this.currentCacheEntriesForNonOutboundProxy;
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000FE7C8 File Offset: 0x000FC9C8
		private void SetCurrentEntriesByNextHop(NextHopSolutionKey nextHopKey, int currentEntries)
		{
			if (SmtpOutSessionCache.OutboundFrontendCacheKey.Equals(nextHopKey))
			{
				this.currentCacheEntriesForOutboundProxy = currentEntries;
				this.cachePerfCountersForOutboundProxy.SizeUpdated((long)currentEntries);
				return;
			}
			this.currentCacheEntriesForNonOutboundProxy = currentEntries;
			this.cachePerfCountersForNonOutboundProxy.SizeUpdated((long)currentEntries);
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000FE810 File Offset: 0x000FCA10
		private void AddToMRUList(SmtpOutSessionCache.CacheItemWrapper cacheItem)
		{
			NextHopSolutionKey other = (NextHopSolutionKey)cacheItem.ItemKey.GetKey(0);
			if (SmtpOutSessionCache.OutboundFrontendCacheKey.Equals(other))
			{
				this.mruListForOutboundProxy.AddFirst(cacheItem);
				return;
			}
			this.mruListForNonOutboundProxy.AddFirst(cacheItem);
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000FE85C File Offset: 0x000FCA5C
		private void RemoveFromMRUList(SmtpOutSessionCache.CacheItemWrapper cacheItem)
		{
			NextHopSolutionKey other = (NextHopSolutionKey)cacheItem.ItemKey.GetKey(0);
			if (SmtpOutSessionCache.OutboundFrontendCacheKey.Equals(other))
			{
				this.mruListForOutboundProxy.Remove(cacheItem);
				return;
			}
			this.mruListForNonOutboundProxy.Remove(cacheItem);
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000FE8A8 File Offset: 0x000FCAA8
		private SmtpOutSessionCache.CacheItemWrapper GetLastNodeFromMRUList(NextHopSolutionKey nextHopKey)
		{
			if (SmtpOutSessionCache.OutboundFrontendCacheKey.Equals(nextHopKey))
			{
				return this.mruListForOutboundProxy.Last.Value;
			}
			return this.mruListForNonOutboundProxy.Last.Value;
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x000FE8E8 File Offset: 0x000FCAE8
		public bool TryAdd(NextHopSolutionKey nextHopKey, IPEndPoint remoteEndPoint, SmtpOutSession connection)
		{
			if (nextHopKey.IsEmpty)
			{
				throw new ArgumentException("NextHopKey is Empty", "remoteEndPoint");
			}
			ArgumentValidator.ThrowIfNull("remoteEndPoint", remoteEndPoint);
			ArgumentValidator.ThrowIfNull("connection", connection);
			int maxEntriesByNextHop = this.GetMaxEntriesByNextHop(nextHopKey);
			if (maxEntriesByNextHop == 0)
			{
				return false;
			}
			TimeSpan t = DateTime.UtcNow - connection.SessionStartTime;
			if (t >= this.connectionTimeout || connection.Disconnected)
			{
				return false;
			}
			lock (this.syncObject)
			{
				MultiValueKey multiValueKey = new MultiValueKey(new object[]
				{
					nextHopKey,
					remoteEndPoint
				});
				LinkedListNode<SmtpOutSessionCache.CacheItemWrapper> linkedListNode = new LinkedListNode<SmtpOutSessionCache.CacheItemWrapper>(new SmtpOutSessionCache.CacheItemWrapper(multiValueKey, connection));
				int currentEntriesByNextHop = this.GetCurrentEntriesByNextHop(nextHopKey);
				if (currentEntriesByNextHop + 1 > maxEntriesByNextHop)
				{
					SmtpOutSessionCache.CacheItemWrapper lastNodeFromMRUList = this.GetLastNodeFromMRUList(nextHopKey);
					this.Remove(lastNodeFromMRUList, CacheItemRemovedReason.Scavenged);
				}
				LinkedList<SmtpOutSessionCache.CacheItemWrapper> linkedList;
				if (!this.sessionCache.TryGetValue(multiValueKey, out linkedList))
				{
					linkedList = new LinkedList<SmtpOutSessionCache.CacheItemWrapper>();
					this.sessionCache.Add(multiValueKey, linkedList);
				}
				linkedList.AddFirst(linkedListNode);
				this.SetCurrentEntriesByNextHop(nextHopKey, this.GetCurrentEntriesByNextHop(nextHopKey) + 1);
				this.AddToMRUList(linkedListNode.Value);
				connection.SetNextStateForCachedSessionAndLogInfo(this.GetCurrentEntriesByNextHop(nextHopKey));
			}
			return true;
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000FEA30 File Offset: 0x000FCC30
		public bool TryGetValue(NextHopSolutionKey nextHopKey, IPEndPoint remoteEndPoint, out SmtpOutSession cachedConnection, out string logMessage)
		{
			cachedConnection = null;
			logMessage = null;
			int maxEntriesByNextHop = this.GetMaxEntriesByNextHop(nextHopKey);
			ICachePerformanceCounters cachePerfCountersByNextHop = this.GetCachePerfCountersByNextHop(nextHopKey);
			if (maxEntriesByNextHop == 0)
			{
				return false;
			}
			bool result;
			lock (this.syncObject)
			{
				MultiValueKey key = new MultiValueKey(new object[]
				{
					nextHopKey,
					remoteEndPoint
				});
				LinkedList<SmtpOutSessionCache.CacheItemWrapper> linkedList;
				if (!this.sessionCache.TryGetValue(key, out linkedList) || linkedList == null || linkedList.Count == 0)
				{
					cachePerfCountersByNextHop.Accessed(AccessStatus.Miss);
					logMessage = string.Format("Cache Miss. Current Cache Size {0}", this.GetCurrentEntriesByNextHop(nextHopKey));
					result = false;
				}
				else
				{
					SmtpOutSessionCache.CacheItemWrapper cacheItemWrapper = null;
					foreach (SmtpOutSessionCache.CacheItemWrapper cacheItemWrapper2 in linkedList)
					{
						TimeSpan t = DateTime.UtcNow - cacheItemWrapper2.CacheItem.SessionStartTime;
						TimeSpan t2 = DateTime.UtcNow - cacheItemWrapper2.TimeCached;
						if (!cacheItemWrapper2.CacheItem.Disconnected && t < this.connectionTimeout && t2 < this.connectionInactivityTimeout)
						{
							cacheItemWrapper = cacheItemWrapper2;
							break;
						}
					}
					if (cacheItemWrapper != null)
					{
						this.Remove(cacheItemWrapper, CacheItemRemovedReason.Removed);
						logMessage = string.Format("Cache Hit. Current Cache Size {0}", this.GetCurrentEntriesByNextHop(nextHopKey));
						cachedConnection = cacheItemWrapper.CacheItem;
						cachePerfCountersByNextHop.Accessed(AccessStatus.Hit);
						result = true;
					}
					else
					{
						logMessage = string.Format("Cache Miss. Current Cache Size {0}", this.GetCurrentEntriesByNextHop(nextHopKey));
						cachePerfCountersByNextHop.Accessed(AccessStatus.Miss);
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x000FEBFC File Offset: 0x000FCDFC
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.expiryTimer.Dispose(true);
				this.disposed = true;
			}
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x000FEC1C File Offset: 0x000FCE1C
		public void RemoveAll(ConnectionCacheRemovalType removeType)
		{
			if (this.maxCacheEntriesForNonOutboundProxy == 0 && this.maxCacheEntriesForOutboundProxy == 0)
			{
				return;
			}
			Dictionary<MultiValueKey, LinkedList<SmtpOutSessionCache.CacheItemWrapper>> dictionary;
			lock (this.syncObject)
			{
				dictionary = this.sessionCache;
				this.sessionCache = new Dictionary<MultiValueKey, LinkedList<SmtpOutSessionCache.CacheItemWrapper>>();
				this.mruListForNonOutboundProxy = new LinkedList<SmtpOutSessionCache.CacheItemWrapper>();
				if (removeType == ConnectionCacheRemovalType.ConfigChange)
				{
					foreach (KeyValuePair<MultiValueKey, LinkedList<SmtpOutSessionCache.CacheItemWrapper>> keyValuePair in dictionary)
					{
						object key = keyValuePair.Key.GetKey(0);
						if (((NextHopSolutionKey)key).Equals(SmtpOutSessionCache.OutboundFrontendCacheKey) && !this.sessionCache.ContainsKey(keyValuePair.Key))
						{
							this.sessionCache.Add(keyValuePair.Key, keyValuePair.Value);
						}
					}
					using (Dictionary<MultiValueKey, LinkedList<SmtpOutSessionCache.CacheItemWrapper>>.Enumerator enumerator2 = this.sessionCache.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<MultiValueKey, LinkedList<SmtpOutSessionCache.CacheItemWrapper>> keyValuePair2 = enumerator2.Current;
							dictionary.Remove(keyValuePair2.Key);
						}
						goto IL_121;
					}
				}
				this.currentCacheEntriesForOutboundProxy = 0;
				this.cachePerfCountersForOutboundProxy.SizeUpdated(0L);
				this.mruListForOutboundProxy = new LinkedList<SmtpOutSessionCache.CacheItemWrapper>();
				IL_121:
				this.currentCacheEntriesForNonOutboundProxy = 0;
				this.cachePerfCountersForNonOutboundProxy.SizeUpdated(0L);
			}
			foreach (LinkedList<SmtpOutSessionCache.CacheItemWrapper> linkedList in dictionary.Values)
			{
				foreach (SmtpOutSessionCache.CacheItemWrapper cacheItemWrapper in linkedList)
				{
					if (removeType == ConnectionCacheRemovalType.ConfigChange)
					{
						cacheItemWrapper.CacheItem.SetNextStateToQuit();
					}
					else
					{
						cacheItemWrapper.CacheItem.RemoveConnection();
					}
				}
			}
			dictionary.Clear();
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x000FEE6C File Offset: 0x000FD06C
		private void Remove(SmtpOutSessionCache.CacheItemWrapper itemTobeRemoved, CacheItemRemovedReason reason)
		{
			LinkedList<SmtpOutSessionCache.CacheItemWrapper> connectionList;
			if (this.sessionCache.TryGetValue(itemTobeRemoved.ItemKey, out connectionList))
			{
				this.Remove(connectionList, itemTobeRemoved, reason);
			}
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x000FEE98 File Offset: 0x000FD098
		private void Remove(LinkedList<SmtpOutSessionCache.CacheItemWrapper> connectionList, SmtpOutSessionCache.CacheItemWrapper itemTobeRemoved, CacheItemRemovedReason reason)
		{
			if (reason == CacheItemRemovedReason.Scavenged || reason == CacheItemRemovedReason.Expired)
			{
				if (itemTobeRemoved.CacheItem.LogSession != null)
				{
					itemTobeRemoved.CacheItem.LogSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Connection being removed from Cache. Reason : {0}", new object[]
					{
						reason
					});
				}
				itemTobeRemoved.CacheItem.SetNextStateToQuit();
			}
			connectionList.Remove(itemTobeRemoved);
			if (connectionList.Count == 0)
			{
				this.sessionCache.Remove(itemTobeRemoved.ItemKey);
			}
			this.RemoveFromMRUList(itemTobeRemoved);
			this.SetCurrentEntriesByNextHop((NextHopSolutionKey)itemTobeRemoved.ItemKey.GetKey(0), this.GetCurrentEntriesByNextHop((NextHopSolutionKey)itemTobeRemoved.ItemKey.GetKey(0)) - 1);
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x000FEF48 File Offset: 0x000FD148
		private void HandleExpiry(object state)
		{
			DateTime utcNow = DateTime.UtcNow;
			lock (this.syncObject)
			{
				List<SmtpOutSessionCache.CacheItemWrapper> list = new List<SmtpOutSessionCache.CacheItemWrapper>();
				foreach (LinkedList<SmtpOutSessionCache.CacheItemWrapper> linkedList in this.sessionCache.Values)
				{
					foreach (SmtpOutSessionCache.CacheItemWrapper cacheItemWrapper in linkedList)
					{
						TimeSpan t = utcNow - cacheItemWrapper.CacheItem.SessionStartTime;
						TimeSpan t2 = utcNow - cacheItemWrapper.TimeCached;
						if (t >= this.connectionTimeout || t2 >= this.connectionInactivityTimeout || cacheItemWrapper.CacheItem.Disconnected)
						{
							list.Add(cacheItemWrapper);
						}
					}
				}
				if (list != null && list.Count != 0)
				{
					foreach (SmtpOutSessionCache.CacheItemWrapper cacheItemWrapper2 in list)
					{
						if (!cacheItemWrapper2.CacheItem.Disconnected)
						{
							this.Remove(cacheItemWrapper2, CacheItemRemovedReason.Expired);
						}
						else
						{
							this.Remove(cacheItemWrapper2, CacheItemRemovedReason.Removed);
						}
					}
					list.Clear();
				}
			}
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x000FF100 File Offset: 0x000FD300
		public void AddDiagnosticInfoTo(XElement cacheElement, bool verbose)
		{
			cacheElement.SetAttributeValue("currentCacheEntriesForOutboundProxy", this.currentCacheEntriesForOutboundProxy);
			cacheElement.SetAttributeValue("currentCacheEntriesForNonOutboundProxy", this.currentCacheEntriesForNonOutboundProxy);
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000FF138 File Offset: 0x000FD338
		// Note: this type is marked as 'beforefieldinit'.
		static SmtpOutSessionCache()
		{
			byte[] outboundFrontendIPAddressCacheKey = new byte[4];
			SmtpOutSessionCache.OutboundFrontendIPAddressCacheKey = outboundFrontendIPAddressCacheKey;
			SmtpOutSessionCache.OutboundFrontendPortCacheKey = 25;
			SmtpOutSessionCache.OutboundFrontendIPEndpointCacheKey = new IPEndPoint(new IPAddress(SmtpOutSessionCache.OutboundFrontendIPAddressCacheKey), SmtpOutSessionCache.OutboundFrontendPortCacheKey);
		}

		// Token: 0x04001EE5 RID: 7909
		public static readonly NextHopSolutionKey OutboundFrontendCacheKey = new NextHopSolutionKey(NextHopType.Empty, "Cached:Frontend", Guid.Empty);

		// Token: 0x04001EE6 RID: 7910
		private static readonly byte[] OutboundFrontendIPAddressCacheKey;

		// Token: 0x04001EE7 RID: 7911
		private static readonly int OutboundFrontendPortCacheKey;

		// Token: 0x04001EE8 RID: 7912
		public static readonly IPEndPoint OutboundFrontendIPEndpointCacheKey;

		// Token: 0x04001EE9 RID: 7913
		private readonly TimeSpan cacheExpirationCheckInterval;

		// Token: 0x04001EEA RID: 7914
		private readonly TimeSpan connectionTimeout;

		// Token: 0x04001EEB RID: 7915
		private readonly TimeSpan connectionInactivityTimeout;

		// Token: 0x04001EEC RID: 7916
		private readonly ICachePerformanceCounters cachePerfCountersForOutboundProxy;

		// Token: 0x04001EED RID: 7917
		private readonly ICachePerformanceCounters cachePerfCountersForNonOutboundProxy;

		// Token: 0x04001EEE RID: 7918
		private readonly int maxCacheEntriesForNonOutboundProxy;

		// Token: 0x04001EEF RID: 7919
		private readonly int maxCacheEntriesForOutboundProxy;

		// Token: 0x04001EF0 RID: 7920
		private int currentCacheEntriesForNonOutboundProxy;

		// Token: 0x04001EF1 RID: 7921
		private int currentCacheEntriesForOutboundProxy;

		// Token: 0x04001EF2 RID: 7922
		private GuardedTimer expiryTimer;

		// Token: 0x04001EF3 RID: 7923
		private bool disposed;

		// Token: 0x04001EF4 RID: 7924
		private object syncObject = new object();

		// Token: 0x04001EF5 RID: 7925
		private Dictionary<MultiValueKey, LinkedList<SmtpOutSessionCache.CacheItemWrapper>> sessionCache;

		// Token: 0x04001EF6 RID: 7926
		private LinkedList<SmtpOutSessionCache.CacheItemWrapper> mruListForNonOutboundProxy;

		// Token: 0x04001EF7 RID: 7927
		private LinkedList<SmtpOutSessionCache.CacheItemWrapper> mruListForOutboundProxy;

		// Token: 0x02000517 RID: 1303
		private class CacheItemWrapper
		{
			// Token: 0x06003D0A RID: 15626 RVA: 0x000FF18B File Offset: 0x000FD38B
			public CacheItemWrapper(MultiValueKey itemKey, SmtpOutSession cacheItem)
			{
				this.cacheItem = cacheItem;
				this.itemKey = itemKey;
				this.timeCached = DateTime.UtcNow;
			}

			// Token: 0x170012AE RID: 4782
			// (get) Token: 0x06003D0B RID: 15627 RVA: 0x000FF1AC File Offset: 0x000FD3AC
			public MultiValueKey ItemKey
			{
				get
				{
					return this.itemKey;
				}
			}

			// Token: 0x170012AF RID: 4783
			// (get) Token: 0x06003D0C RID: 15628 RVA: 0x000FF1B4 File Offset: 0x000FD3B4
			public SmtpOutSession CacheItem
			{
				get
				{
					return this.cacheItem;
				}
			}

			// Token: 0x170012B0 RID: 4784
			// (get) Token: 0x06003D0D RID: 15629 RVA: 0x000FF1BC File Offset: 0x000FD3BC
			public DateTime TimeCached
			{
				get
				{
					return this.timeCached;
				}
			}

			// Token: 0x04001EF8 RID: 7928
			private readonly DateTime timeCached;

			// Token: 0x04001EF9 RID: 7929
			private SmtpOutSession cacheItem;

			// Token: 0x04001EFA RID: 7930
			private MultiValueKey itemKey;
		}

		// Token: 0x02000518 RID: 1304
		internal class ConnectionCachePerfCounters : DefaultCachePerformanceCounters
		{
			// Token: 0x06003D0E RID: 15630 RVA: 0x000FF1C4 File Offset: 0x000FD3C4
			public ConnectionCachePerfCounters(ProcessTransportRole transportRole, string cachePerfCounterInstance)
			{
				ArgumentValidator.ThrowIfNull("cachePerfCounterInstance", cachePerfCounterInstance);
				if (!ProcessTransportRole.Edge.Equals(transportRole) && !ProcessTransportRole.Hub.Equals(transportRole) && !ProcessTransportRole.FrontEnd.Equals(transportRole) && !ProcessTransportRole.MailboxDelivery.Equals(transportRole) && !ProcessTransportRole.MailboxSubmission.Equals(transportRole))
				{
					throw new ArgumentNotSupportedException("transportRole", "Supplied Transport Role is not supported for these performance counters. [" + transportRole.ToString() + "]");
				}
				try
				{
					SmtpConnectionCachePerfCounters.SetCategoryName(SmtpOutSessionCache.ConnectionCachePerfCounters.perfCounterCategoryMap[transportRole]);
					this.perfCounters = SmtpConnectionCachePerfCounters.GetInstance(cachePerfCounterInstance);
					if (this.perfCounters != null)
					{
						base.InitializeCounters(this.perfCounters.Requests, this.perfCounters.HitRatio, this.perfCounters.HitRatio_Base, this.perfCounters.CacheSize);
					}
				}
				catch (InvalidOperationException ex)
				{
					ExTraceGlobals.GeneralTracer.TraceError<string, InvalidOperationException>(0L, "Failed to initialize performance counters for component '{0}': {1}", cachePerfCounterInstance, ex);
					SmtpCommand.EventLogger.LogEvent(TransportEventLogConstants.Tuple_PerfCountersLoadFailure, null, new object[]
					{
						"SmtpOutSessionCache",
						cachePerfCounterInstance,
						ex.ToString()
					});
					this.perfCounters = null;
				}
			}

			// Token: 0x04001EFB RID: 7931
			private const string EventTag = "SmtpOutSessionCache";

			// Token: 0x04001EFC RID: 7932
			private static readonly IDictionary<ProcessTransportRole, string> perfCounterCategoryMap = new Dictionary<ProcessTransportRole, string>
			{
				{
					ProcessTransportRole.Edge,
					"MSExchangeTransport Smtp Connection Cache"
				},
				{
					ProcessTransportRole.Hub,
					"MSExchangeTransport Smtp Connection Cache"
				},
				{
					ProcessTransportRole.FrontEnd,
					"MSExchangeFrontEndTransport Smtp Connection Cache"
				},
				{
					ProcessTransportRole.MailboxDelivery,
					"MSExchange Delivery Smtp Connection Cache"
				},
				{
					ProcessTransportRole.MailboxSubmission,
					"MSExchange Submission Smtp Connection Cache"
				}
			};

			// Token: 0x04001EFD RID: 7933
			private SmtpConnectionCachePerfCountersInstance perfCounters;
		}
	}
}
