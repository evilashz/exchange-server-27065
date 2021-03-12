using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.AsyncContexts;
using Microsoft.Exchange.Directory.TopologyService.Common;
using Microsoft.Exchange.Directory.TopologyService.Configuration;
using Microsoft.Exchange.Directory.TopologyService.Data;
using Microsoft.Exchange.Directory.TopologyService.EventLog;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x0200001A RID: 26
	[DebuggerDisplay("Topologies = {topologiesCache.Count}, Server Requests = {getRemoteServerPendingRequest.Count} , Topology Requests = {pendingRequest.Count}")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TopologyDiscoveryManager
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000690C File Offset: 0x00004B0C
		internal TopologyDiscoveryManager(TopologyDiscoveryWorkProvider workQueue, TopologyCache topologiesCache)
		{
			ArgumentValidator.ThrowIfNull("workQueue", workQueue);
			ArgumentValidator.ThrowIfNull("topologiesCache", topologiesCache);
			this.topologiesCache = topologiesCache;
			this.workQueue = workQueue;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006963 File Offset: 0x00004B63
		private TopologyDiscoveryManager() : this(new TopologyDiscoveryWorkProvider(), TopologyCache.Default)
		{
			TopologyDiscoveryWorker.Instance.TryRegisterWorkQueue(base.GetType().Name, this.workQueue);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00006991 File Offset: 0x00004B91
		public static TopologyDiscoveryManager Instance
		{
			get
			{
				return TopologyDiscoveryManager.instance;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006998 File Offset: 0x00004B98
		public void Start(string preferredCDCFqdn = null)
		{
			ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "Initializing Topology Discovery Manager");
			ConfigurationData.LogServiceStartingEvent("TopologyDiscoveryManager.Start() - worker initialized");
			ConfigurationData.Instance.ConfigurationChanged += this.OnNewConfigurationChange;
			this.TryStartForestDiscoveryOrRediscovery(TopologyProvider.LocalForestFqdn, true, preferredCDCFqdn);
			ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "Local forest discovery scheduling started");
			ConfigurationData.LogServiceStartingEvent("TopologyDiscoveryManager.Start() - Local forest discovery scheduling started");
			if (Globals.IsDatacenter)
			{
				this.StartAccountForestDiscovery();
				ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "Account forest discovery scheduling started");
				ConfigurationData.LogServiceStartingEvent("TopologyDiscoveryManager.Start() - Account forest discovery scheduling started");
			}
			ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "Topology Discovered Manager initialized");
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006A4C File Offset: 0x00004C4C
		public IAsyncResult BeginGetTopology(string partitionFqdn, AsyncCallback callback, object state)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(partitionFqdn, state, callback);
			TopologyDiscoveryInfo topologyDiscoveryInfo = null;
			this.topologiesCache.TryGetValue(partitionFqdn, out topologyDiscoveryInfo);
			ExTraceGlobals.TopologyTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "{0} - BeginGetTopology - TopologyInfo is {1}NULL. Topology is {2}NULL.", partitionFqdn, (topologyDiscoveryInfo == null) ? string.Empty : "NOT ", (topologyDiscoveryInfo != null && topologyDiscoveryInfo.Topology != null) ? "NOT " : string.Empty);
			if (topologyDiscoveryInfo != null && topologyDiscoveryInfo.Topology != null && topologyDiscoveryInfo.Topology.HasMinimalRequiredServers())
			{
				callback(lazyAsyncResult);
				this.StartForestDiscoveryOrRediscoveryIfRequired(partitionFqdn);
			}
			else
			{
				int num = (topologyDiscoveryInfo != null && topologyDiscoveryInfo.Topology != null) ? topologyDiscoveryInfo.TopologyVersion : -1;
				if (this.TryStartForestDiscoveryOrRediscovery(partitionFqdn, true, null))
				{
					if (this.topologiesCache.TryGetValue(partitionFqdn, out topologyDiscoveryInfo) && topologyDiscoveryInfo.Topology != null && num != topologyDiscoveryInfo.TopologyVersion)
					{
						callback(lazyAsyncResult);
					}
					else
					{
						ConcurrentQueue<LazyAsyncResult> orAdd = this.pendingRequest.GetOrAdd(partitionFqdn, new ConcurrentQueue<LazyAsyncResult>());
						orAdd.Enqueue(lazyAsyncResult);
						ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - BeginGetTopology - Request enqueued", partitionFqdn);
					}
				}
				else
				{
					callback(lazyAsyncResult);
				}
			}
			return lazyAsyncResult;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006B6C File Offset: 0x00004D6C
		public ADTopology EndGetTopology(IAsyncResult ar)
		{
			ArgumentValidator.ThrowIfNull("ar", ar);
			ArgumentValidator.ThrowIfTypeInvalid<LazyAsyncResult>("ar", ar);
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar;
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException("Async Result already completed.");
			}
			lazyAsyncResult.EndCalled = true;
			string text = lazyAsyncResult.AsyncObject as string;
			if (string.IsNullOrEmpty(text))
			{
				throw new NotSupportedException("Invalid Async Result. Context is invalid");
			}
			TopologyDiscoveryInfo topologyDiscoveryInfo = null;
			if (!this.topologiesCache.TryGetValue(text, out topologyDiscoveryInfo))
			{
				throw new TopologyServiceTransientException(new LocalizedString(string.Format("Unable to find topology info for forest {0}", text)));
			}
			ADTopology topology = topologyDiscoveryInfo.Topology;
			Exception lastDiscoveryException = topologyDiscoveryInfo.LastDiscoveryException;
			if (topology == null && lastDiscoveryException == null)
			{
				throw new TopologyServiceTransientException(new LocalizedString(string.Format("Unable to find topology or exception information for forest {0}", text)));
			}
			if (topology == null)
			{
				throw lastDiscoveryException;
			}
			return topology;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006C68 File Offset: 0x00004E68
		public IAsyncResult BeginGetServerFromDomain(ADObjectId domain, AsyncCallback callback, object asyncState)
		{
			ArgumentValidator.ThrowIfNull("domain", domain);
			ExTraceGlobals.TopologyTracer.TraceFunction<string>((long)this.GetHashCode(), "{0} - Entering BeginGetServerFromDomain", domain.DistinguishedName);
			DiscoverServerFromRemoteDomainContext discoverServerFromRemoteDomainContext = new DiscoverServerFromRemoteDomainContext(domain);
			LazyAsyncResult asyncResult = new LazyAsyncResult(discoverServerFromRemoteDomainContext, asyncState, callback);
			ServerInfo serverInfoResult = null;
			if (this.TryGetServerFromRemoteDomainFromTopologiesCache(domain, out serverInfoResult))
			{
				discoverServerFromRemoteDomainContext.ServerInfoResult = serverInfoResult;
				callback(asyncResult);
				return asyncResult;
			}
			ConcurrentQueue<LazyAsyncResult> concurrentQueue = new ConcurrentQueue<LazyAsyncResult>();
			concurrentQueue.Enqueue(asyncResult);
			ConcurrentQueue<LazyAsyncResult> obj = this.getRemoteServerPendingRequest.AddOrUpdate(domain.DistinguishedName, concurrentQueue, delegate(string unusedKey, ConcurrentQueue<LazyAsyncResult> currentQueue)
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - BeginGetServerFromDomain - Domain already scheduled for discovery", domain.DistinguishedName);
				currentQueue.Enqueue(asyncResult);
				return currentQueue;
			});
			if (concurrentQueue.Equals(obj))
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "{0} - BeginGetServerFromDomain - Domain is being scheduled for discovery", domain.DistinguishedName);
				RemoteDomainServerDiscovery workItem = new RemoteDomainServerDiscovery(domain);
				this.workQueue.AddUrgentWork(workItem, new Action<IWorkItemResult>(this.DiscoverServerFromRemoteDomainCallback));
			}
			ExTraceGlobals.TopologyTracer.TraceFunction<string>((long)this.GetHashCode(), "{0} - Exiting BeginGetServerFromDomain", domain.DistinguishedName);
			return asyncResult;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006DC0 File Offset: 0x00004FC0
		public ServerInfo EndGetServerFromDomain(IAsyncResult ar)
		{
			ArgumentValidator.ThrowIfNull("ar", ar);
			ArgumentValidator.ThrowIfTypeInvalid<LazyAsyncResult>("ar", ar);
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar;
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException("Async Result already completed.");
			}
			lazyAsyncResult.EndCalled = true;
			DiscoverServerFromRemoteDomainContext discoverServerFromRemoteDomainContext = lazyAsyncResult.AsyncObject as DiscoverServerFromRemoteDomainContext;
			if (discoverServerFromRemoteDomainContext == null)
			{
				throw new NotSupportedException("Invalid Async Result. Context is invalid");
			}
			if (discoverServerFromRemoteDomainContext.DiscoveryException == null && discoverServerFromRemoteDomainContext.ServerInfoResult == null)
			{
				throw new TopologyServiceTransientException(new LocalizedString(string.Format("Unable to find suitable DC in domain {0}", discoverServerFromRemoteDomainContext.DomainId.ToCanonicalName())));
			}
			if (discoverServerFromRemoteDomainContext.ServerInfoResult == null)
			{
				ExTraceGlobals.TopologyTracer.TraceError<string, Exception>((long)this.GetHashCode(), "{0} - EndGetServerForDomain Error {1}", discoverServerFromRemoteDomainContext.DomainId.ToString(), discoverServerFromRemoteDomainContext.DiscoveryException);
				throw new NoSuitableServerFoundException(new LocalizedString(string.Format("Wrapping Exception: Unable to find suitable DC in domain {0}", discoverServerFromRemoteDomainContext.DomainId.ToCanonicalName())), discoverServerFromRemoteDomainContext.DiscoveryException);
			}
			ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0} - EndGetServerForDomain returning {1}", discoverServerFromRemoteDomainContext.DomainId.ToString(), (discoverServerFromRemoteDomainContext.ServerInfoResult != null) ? discoverServerFromRemoteDomainContext.ServerInfoResult.ToString() : string.Empty);
			return discoverServerFromRemoteDomainContext.ServerInfoResult;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00007094 File Offset: 0x00005294
		public IEnumerable<ADTopology> GetAllTopologies()
		{
			foreach (KeyValuePair<string, TopologyDiscoveryInfo> topologyInfo in this.topologiesCache)
			{
				KeyValuePair<string, TopologyDiscoveryInfo> keyValuePair = topologyInfo;
				if (keyValuePair.Value.Topology != null)
				{
					KeyValuePair<string, TopologyDiscoveryInfo> keyValuePair2 = topologyInfo;
					yield return keyValuePair2.Value.Topology;
				}
			}
			yield break;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000070B4 File Offset: 0x000052B4
		public bool TryGetTopology(string partitionFqdn, out ADTopology topology)
		{
			topology = null;
			TopologyDiscoveryInfo topologyDiscoveryInfo = null;
			if (this.topologiesCache.TryGetValue(partitionFqdn, out topologyDiscoveryInfo))
			{
				topology = topologyDiscoveryInfo.Topology;
			}
			return null != topology;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000070E6 File Offset: 0x000052E6
		public void StartForestDiscoverOrRediscover(string forestFqdn, string cdcFqdn = null)
		{
			this.TryStartForestDiscoveryOrRediscovery(forestFqdn, true, cdcFqdn);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000070F4 File Offset: 0x000052F4
		public void StartForestDiscoveryOrRediscoveryIfRequired(string forestFqdn)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("forestFqdn", forestFqdn);
			TopologyDiscoveryInfo topologyDiscoveryInfo;
			if (this.topologiesCache.TryGetValue(forestFqdn, out topologyDiscoveryInfo) && topologyDiscoveryInfo.Topology != null)
			{
				ADTopology topology = topologyDiscoveryInfo.Topology;
				if (topology.NeedsRediscover())
				{
					this.TryStartForestDiscoveryOrRediscovery(forestFqdn, false, null);
				}
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007140 File Offset: 0x00005340
		private bool TryGetServerFromRemoteDomainFromTopologiesCache(ADObjectId domain, out ServerInfo serverInfo)
		{
			ArgumentValidator.ThrowIfNull("domain", domain);
			serverInfo = null;
			PartitionId partitionId = domain.GetPartitionId();
			string text = (null != partitionId) ? partitionId.ForestFQDN : string.Empty;
			ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "TryGetServerFromRemoteDomainFromTopologiesCache - Domain {0} ({1}).", domain.DistinguishedName, text);
			List<string> list = new List<string>(2);
			if (!string.IsNullOrEmpty(text))
			{
				list.Add(text);
			}
			if (!string.Equals(text, TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase))
			{
				list.Add(TopologyProvider.LocalForestFqdn);
			}
			if (Globals.IsDatacenter)
			{
				list.AddRange(this.topologiesCache.Keys);
			}
			TopologyDiscoveryInfo topologyDiscoveryInfo = null;
			foreach (string text2 in list)
			{
				if (this.topologiesCache.TryGetValue(text2, out topologyDiscoveryInfo) && topologyDiscoveryInfo.Topology != null)
				{
					List<ServerInfo> list2 = topologyDiscoveryInfo.Topology.FindServersForWritableNC(domain, 1);
					if (list2.Count > 0)
					{
						ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "TryGetServerFromRemoteDomainFromTopologiesCache - Domain {0} found cache (Forest Fqdn {1})", domain.DistinguishedName, text2);
						serverInfo = list2[0];
						break;
					}
				}
			}
			return null != serverInfo;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007280 File Offset: 0x00005480
		private void DiscoverServerFromRemoteDomainCallback(IWorkItemResult result)
		{
			ArgumentValidator.ThrowIfNull("result", result);
			ArgumentValidator.ThrowIfTypeInvalid<WorkItemResult<RemoteDomainServerDiscovery.RemoteDomainServerDiscoveryResult>>("result", result);
			WorkItemResult<RemoteDomainServerDiscovery.RemoteDomainServerDiscoveryResult> workItemResult = (WorkItemResult<RemoteDomainServerDiscovery.RemoteDomainServerDiscoveryResult>)result;
			ServerInfo serverInfoResult = null;
			Exception discoveryException = null;
			switch (workItemResult.ResultType)
			{
			case ResultType.Abandoned:
			case ResultType.TimedOut:
				discoveryException = (workItemResult.Exception ?? new NoSuitableServerFoundException(DirectoryStrings.ErrorNoSuitableDCInDomain(workItemResult.Data.DomainId.ToCanonicalName(), Strings.DiscoveryTimeoutOrCancelled)));
				break;
			case ResultType.Succeeded:
				serverInfoResult = workItemResult.Data.ServerInfo;
				break;
			case ResultType.Failed:
				discoveryException = workItemResult.Exception;
				break;
			}
			ConcurrentQueue<LazyAsyncResult> concurrentQueue;
			if (this.getRemoteServerPendingRequest.TryGetValue(workItemResult.Data.DomainId.DistinguishedName, out concurrentQueue))
			{
				while (!concurrentQueue.IsEmpty)
				{
					LazyAsyncResult lazyAsyncResult = null;
					if (concurrentQueue.TryDequeue(out lazyAsyncResult))
					{
						DiscoverServerFromRemoteDomainContext discoverServerFromRemoteDomainContext = (DiscoverServerFromRemoteDomainContext)lazyAsyncResult.AsyncObject;
						discoverServerFromRemoteDomainContext.ServerInfoResult = serverInfoResult;
						discoverServerFromRemoteDomainContext.DiscoveryException = discoveryException;
						lazyAsyncResult.InvokeCallback(workItemResult);
					}
				}
				this.getRemoteServerPendingRequest.TryRemove(workItemResult.Data.DomainId.DistinguishedName, out concurrentQueue);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00007394 File Offset: 0x00005594
		private void StartAccountForestDiscovery()
		{
			this.workQueue.ScheduleWork(new AccountForestDiscoveryMonitorWorkItem(), DateTime.UtcNow.Add(TimeSpan.FromMinutes(5.0)), new Action<IWorkItemResult>(this.AccountForestScanCallback));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000073D8 File Offset: 0x000055D8
		private void OnNewConfigurationChange()
		{
			ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "OnNewConfigurationChange.");
			this.TryStartForestDiscoveryOrRediscovery(TopologyProvider.LocalForestFqdn, true, null);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007400 File Offset: 0x00005600
		private void ForestDiscoveryCallback(IWorkItemResult result)
		{
			ArgumentValidator.ThrowIfNull("result", result);
			ArgumentValidator.ThrowIfTypeInvalid<WorkItemResult<ADTopologyDiscoveryResult>>("result", result);
			WorkItemResult<ADTopologyDiscoveryResult> workItemResult = (WorkItemResult<ADTopologyDiscoveryResult>)result;
			this.LogDiscoveryExceptionIfNecessary(workItemResult);
			TopologyDiscoveryInfo topologyDiscoveryInfo = null;
			this.topologiesCache.TryGetValue(workItemResult.Data.TopologyDiscoveryInfo.ForestFqdn, out topologyDiscoveryInfo);
			ExTraceGlobals.TopologyTracer.TraceDebug<string, ResultType>((long)this.GetHashCode(), "OnNewForestDiscovery - Forest '{0}'. Discovery Result Type {1}", topologyDiscoveryInfo.ForestFqdn, workItemResult.ResultType);
			topologyDiscoveryInfo.GetLock();
			try
			{
				switch (workItemResult.ResultType)
				{
				case ResultType.TimedOut:
				case ResultType.Failed:
				{
					Exception ex = workItemResult.Exception;
					if (ex == null)
					{
						ex = new TopologyDiscoveryException(Strings.ForestDiscoveryTimeout(topologyDiscoveryInfo.ForestFqdn, (topologyDiscoveryInfo.Topology == null) ? ConfigurationData.Instance.UrgentOrInitialTopologyTimeout.TotalSeconds : ConfigurationData.Instance.FullTopologyDiscoveryTimeout.TotalSeconds));
					}
					topologyDiscoveryInfo.SetLastDiscoveryException(ex);
					topologyDiscoveryInfo.ClearDiscoveryWI();
					break;
				}
				case ResultType.Succeeded:
				{
					TimeSpan timeSpan = workItemResult.WhenCompleted - workItemResult.WhenStarted;
					if (timeSpan < TimeSpan.Zero)
					{
						timeSpan = TimeSpan.Zero;
					}
					topologyDiscoveryInfo.SetADTopology(workItemResult.Data.Topology, timeSpan);
					topologyDiscoveryInfo.ClearDiscoveryWI();
					break;
				}
				}
			}
			finally
			{
				topologyDiscoveryInfo.ReleaseLock();
			}
			this.OnNewADTopology(topologyDiscoveryInfo);
			topologyDiscoveryInfo.GetLock();
			try
			{
				if (topologyDiscoveryInfo.IsDiscoveryInProgress() || topologyDiscoveryInfo.IsDiscoveryScheduled())
				{
					ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}. Forest discovery is in progress or scheduled by another thread. Skip.", topologyDiscoveryInfo.ForestFqdn);
				}
				else
				{
					DiscoveryFlags discoveryFlags = DiscoveryFlags.None;
					DateTime executeOn = DateTime.UtcNow;
					ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "{0}. Has Topology {1}. CanDiscover {2}. ResultType {3}. Discovery Flags {4}.", new object[]
					{
						topologyDiscoveryInfo.ForestFqdn,
						null == topologyDiscoveryInfo.Topology,
						topologyDiscoveryInfo.CanDiscover(),
						workItemResult.ResultType,
						(workItemResult.Data != null) ? DiscoveryFlags.None : workItemResult.Data.DiscoveryFlags
					});
					if (workItemResult.ResultType != ResultType.Succeeded && workItemResult.ResultType != ResultType.Abandoned && topologyDiscoveryInfo.CanDiscover())
					{
						discoveryFlags = workItemResult.Data.DiscoveryFlags;
						if (topologyDiscoveryInfo.Topology == null)
						{
							executeOn = executeOn.Add(ConfigurationData.Instance.DiscoveryFrequencyOnNoTopology);
							ExTraceGlobals.TopologyTracer.TraceInformation<string>(this.GetHashCode(), (long)topologyDiscoveryInfo.GetHashCode(), "{0}. Case 1.A", topologyDiscoveryInfo.ForestFqdn);
						}
						else
						{
							executeOn = executeOn.Add(ConfigurationData.Instance.DiscoveryFrequencyOnFailure);
							ExTraceGlobals.TopologyTracer.TraceInformation<string>(this.GetHashCode(), (long)topologyDiscoveryInfo.GetHashCode(), "{0}. Case 1.B", topologyDiscoveryInfo.ForestFqdn);
						}
					}
					else if (workItemResult.ResultType == ResultType.Succeeded)
					{
						if ((workItemResult.Data.DiscoveryFlags & DiscoveryFlags.FullDiscovery) == DiscoveryFlags.None)
						{
							discoveryFlags = DiscoveryFlags.FullDiscovery;
							executeOn = executeOn.Add(ConfigurationData.Instance.WaitTimeBetweenInitialAndFullDiscovery);
							ExTraceGlobals.TopologyTracer.TraceInformation<string>(this.GetHashCode(), (long)topologyDiscoveryInfo.GetHashCode(), "{0}. Case 2.A", topologyDiscoveryInfo.ForestFqdn);
						}
						else if (topologyDiscoveryInfo.Topology.NeedsRediscover())
						{
							discoveryFlags = DiscoveryFlags.FullDiscovery;
							executeOn = executeOn.Add(ConfigurationData.Instance.DiscoveryFrequencyOnMinPercentageDC);
							ExTraceGlobals.TopologyTracer.TraceInformation<string>(this.GetHashCode(), (long)topologyDiscoveryInfo.GetHashCode(), "{0}. Case 2.B", topologyDiscoveryInfo.ForestFqdn);
						}
						else if (topologyDiscoveryInfo.ForestFqdn.Equals(TopologyProvider.LocalForestFqdn, StringComparison.OrdinalIgnoreCase))
						{
							discoveryFlags = DiscoveryFlags.FullDiscovery;
							executeOn = executeOn.Add(ConfigurationData.Instance.DiscoveryFrequency);
							ExTraceGlobals.TopologyTracer.TraceInformation<string>(this.GetHashCode(), (long)topologyDiscoveryInfo.GetHashCode(), "{0}. Case 2.C", topologyDiscoveryInfo.ForestFqdn);
						}
					}
					ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "{0} {1} be discovered. Discovery Flags {2} Next Execution {3}", new object[]
					{
						topologyDiscoveryInfo.ForestFqdn,
						(discoveryFlags != DiscoveryFlags.None) ? "will" : "won't",
						discoveryFlags,
						executeOn.ToString()
					});
					if (discoveryFlags != DiscoveryFlags.None)
					{
						ADTopologyDiscovery adtopologyDiscovery = ADTopologyDiscovery.CreateTopologyDiscoveryWorkItem(topologyDiscoveryInfo, discoveryFlags, null);
						this.workQueue.ScheduleWork(adtopologyDiscovery, executeOn, new Action<IWorkItemResult>(this.ForestDiscoveryCallback));
						topologyDiscoveryInfo.SetDiscoveryWI(adtopologyDiscovery, QueueType.Timed);
					}
				}
			}
			finally
			{
				topologyDiscoveryInfo.ReleaseLock();
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00007860 File Offset: 0x00005A60
		private void AccountForestScanCallback(IWorkItemResult result)
		{
			ArgumentValidator.ThrowIfNull("result", result);
			ArgumentValidator.ThrowIfTypeInvalid<WorkItemResult<List<PartitionId>>>("result", result);
			WorkItemResult<List<PartitionId>> workItemResult = (WorkItemResult<List<PartitionId>>)result;
			DateTime dateTime = DateTime.UtcNow;
			List<PartitionId> list = new List<PartitionId>();
			switch (workItemResult.ResultType)
			{
			case ResultType.Abandoned:
				dateTime = DateTime.MaxValue;
				break;
			case ResultType.TimedOut:
			case ResultType.Failed:
				dateTime = dateTime.Add(ConfigurationData.Instance.ForestScanFrequencyOnFailure);
				break;
			case ResultType.Succeeded:
				list = workItemResult.Data;
				dateTime = dateTime.Add(ConfigurationData.Instance.ForestScanFrequency);
				break;
			}
			if (dateTime != DateTime.MaxValue)
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<DateTime>((long)this.GetHashCode(), "Account Forest Scan - Perform Account Forest scan on {0}", dateTime);
				this.workQueue.ScheduleWork(new AccountForestDiscoveryMonitorWorkItem(), dateTime, new Action<IWorkItemResult>(this.AccountForestScanCallback));
			}
			foreach (PartitionId partitionId in list)
			{
				this.TryStartForestDiscoveryOrRediscovery(partitionId.ForestFQDN, false, null);
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000797C File Offset: 0x00005B7C
		private void OnNewADTopology(TopologyDiscoveryInfo topologyInfo)
		{
			ConcurrentQueue<LazyAsyncResult> pendingRequests;
			if (this.pendingRequest.TryGetValue(topologyInfo.ForestFqdn, out pendingRequests))
			{
				this.DispatchQueuedRequests(pendingRequests);
				if (!TopologyProvider.LocalForestFqdn.Equals(topologyInfo.ForestFqdn, StringComparison.OrdinalIgnoreCase))
				{
					this.pendingRequest.TryRemove(topologyInfo.ForestFqdn, out pendingRequests);
				}
				this.DispatchQueuedRequests(pendingRequests);
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000079D4 File Offset: 0x00005BD4
		private void DispatchQueuedRequests(ConcurrentQueue<LazyAsyncResult> pendingRequests)
		{
			while (!pendingRequests.IsEmpty)
			{
				LazyAsyncResult lazyAsyncResult = null;
				if (pendingRequests.TryDequeue(out lazyAsyncResult))
				{
					lazyAsyncResult.InvokeCallback(lazyAsyncResult);
				}
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007A00 File Offset: 0x00005C00
		private bool TryStartForestDiscoveryOrRediscovery(string forestFqdn, bool isOnDemand, string cdcFqdn = null)
		{
			ExTraceGlobals.TopologyTracer.TraceFunction((long)this.GetHashCode(), "Entering TopologyDiscoveryManager.InternalStartForestDiscoveryOrRediscovery");
			ArgumentValidator.ThrowIfNullOrEmpty("forestFqdn", forestFqdn);
			ExTraceGlobals.TopologyTracer.Information<string, bool, string>((long)this.GetHashCode(), "InternalStartForestDiscoveryOrRediscovery. Forest {0} isOnDemand {1} CDCFqdn {2}", forestFqdn, isOnDemand, cdcFqdn ?? "<NULL>");
			TopologyDiscoveryInfo orAdd = this.topologiesCache.GetOrAdd(forestFqdn, new TopologyDiscoveryInfo(forestFqdn));
			ADTopology topology = orAdd.Topology;
			if (orAdd.IsDiscoveryInProgress())
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "InternalStartForestDiscoveryOrRediscovery. Discovery of Forest {0} already in progress", forestFqdn);
				return true;
			}
			if (!isOnDemand && !orAdd.CanDiscover())
			{
				ExTraceGlobals.TopologyTracer.TraceDebug<string, string>((long)this.GetHashCode(), "InternalStartForestDiscoveryOrRediscovery. Stopping attemp to discover forest {0}. Data {1}", forestFqdn, orAdd.ToString());
				return false;
			}
			DiscoveryFlags discoveryFlags = isOnDemand ? DiscoveryFlags.UrgentDiscovery : DiscoveryFlags.None;
			discoveryFlags |= ((orAdd.Topology == null) ? DiscoveryFlags.InitialDiscovery : DiscoveryFlags.FullDiscovery);
			ADTopologyDiscovery adtopologyDiscovery = ADTopologyDiscovery.CreateTopologyDiscoveryWorkItem(orAdd, discoveryFlags, cdcFqdn);
			orAdd.GetLock();
			bool result;
			try
			{
				if (orAdd.IsDiscoveryInProgress())
				{
					result = true;
				}
				else if (!object.Equals(topology, orAdd.Topology) && string.IsNullOrEmpty(cdcFqdn))
				{
					result = false;
				}
				else
				{
					if (isOnDemand)
					{
						if (!orAdd.IsDiscoveryScheduled() || QueueType.Urgent != orAdd.QueueType || cdcFqdn != null)
						{
							string workItemId = null;
							if (orAdd.TryGetDiscoveryWorkItemId(out workItemId))
							{
								this.workQueue.RemoveAllMatchingWorkItems(workItemId, orAdd.QueueType);
							}
							orAdd.ClearDiscoveryWI();
							this.workQueue.AddUrgentWork(adtopologyDiscovery, new Action<IWorkItemResult>(this.ForestDiscoveryCallback));
							orAdd.SetDiscoveryWI(adtopologyDiscovery, QueueType.Urgent);
						}
					}
					else if (!orAdd.IsDiscoveryScheduled())
					{
						orAdd.ClearDiscoveryWI();
						this.workQueue.AddWork(adtopologyDiscovery, new Action<IWorkItemResult>(this.ForestDiscoveryCallback));
						orAdd.SetDiscoveryWI(adtopologyDiscovery, QueueType.UnTimed);
					}
					ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "Topology Discovery Manager - Forest {0} Work Id '{1}' DiscoveryFlags [{2}] TopologyInfo {3}", new object[]
					{
						forestFqdn,
						adtopologyDiscovery.Id,
						discoveryFlags,
						orAdd.ToString()
					});
					result = true;
				}
			}
			finally
			{
				orAdd.ReleaseLock();
			}
			return result;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007C10 File Offset: 0x00005E10
		private void LogDiscoveryExceptionIfNecessary(WorkItemResult<ADTopologyDiscoveryResult> workItemResult)
		{
			ArgumentValidator.ThrowIfNull("workItemResult", workItemResult);
			if (ResultType.Succeeded == workItemResult.ResultType)
			{
				return;
			}
			string text = (workItemResult.Data == null) ? "Unknown forest" : workItemResult.Data.TopologyDiscoveryInfo.ForestFqdn;
			if (workItemResult.Exception == null)
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DISCOVERY_FAILED2, null, new object[]
				{
					text,
					workItemResult.ResultType.ToString()
				});
				return;
			}
			ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_DISCOVERY_FAILED2, null, new object[]
			{
				text,
				Globals.IsDatacenter ? workItemResult.Exception.ToString() : workItemResult.Exception.Message
			});
		}

		// Token: 0x04000061 RID: 97
		private static TopologyDiscoveryManager instance = new TopologyDiscoveryManager();

		// Token: 0x04000062 RID: 98
		private TopologyDiscoveryWorkProvider workQueue;

		// Token: 0x04000063 RID: 99
		private ConcurrentDictionary<string, ConcurrentQueue<LazyAsyncResult>> pendingRequest = new ConcurrentDictionary<string, ConcurrentQueue<LazyAsyncResult>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000064 RID: 100
		private ConcurrentDictionary<string, ConcurrentQueue<LazyAsyncResult>> getRemoteServerPendingRequest = new ConcurrentDictionary<string, ConcurrentQueue<LazyAsyncResult>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000065 RID: 101
		private TopologyCache topologiesCache;
	}
}
