using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Common;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003B2 RID: 946
	internal class RemoteDeliveryComponent : IStartableTransportComponent, ITransportComponent, IDiagnosable, IQueueQuotaObservableComponent
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x000A82B0 File Offset: 0x000A64B0
		public RemoteDeliveryComponent()
		{
			this.connectionManager = new ConnectionManager();
			if (MultiTenantTransport.MultiTenancyEnabled)
			{
				this.healthTracker = new RemoteDeliveryHealthTracker(Components.TransportAppConfig.RemoteDelivery.RefreshIntervalToUpdateHealth, Components.TransportAppConfig.RemoteDelivery.MessageThresholdToUpdateHealthCounters, new RemoteDeliveryHealthPerformanceCountersWrapper());
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06002A52 RID: 10834 RVA: 0x000A833C File Offset: 0x000A653C
		// (remove) Token: 0x06002A53 RID: 10835 RVA: 0x000A8374 File Offset: 0x000A6574
		public event Action<TransportMailItem> OnAcquire;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06002A54 RID: 10836 RVA: 0x000A83AC File Offset: 0x000A65AC
		// (remove) Token: 0x06002A55 RID: 10837 RVA: 0x000A83E4 File Offset: 0x000A65E4
		public event Action<TransportMailItem> OnRelease;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06002A56 RID: 10838 RVA: 0x000A841C File Offset: 0x000A661C
		// (remove) Token: 0x06002A57 RID: 10839 RVA: 0x000A8454 File Offset: 0x000A6654
		public event Action<RoutedMailItem> OnAcquireRoutedMailItem;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06002A58 RID: 10840 RVA: 0x000A848C File Offset: 0x000A668C
		// (remove) Token: 0x06002A59 RID: 10841 RVA: 0x000A84C4 File Offset: 0x000A66C4
		public event Action<RoutedMailItem> OnReleaseRoutedMailItem;

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x000A84F9 File Offset: 0x000A66F9
		public static UnreachableMessageQueue UnreachableMessageQueue
		{
			get
			{
				return UnreachableMessageQueue.Instance;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x000A8500 File Offset: 0x000A6700
		public string CurrentState
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x000A8507 File Offset: 0x000A6707
		public ConnectionManager ConnectionManager
		{
			get
			{
				return this.connectionManager;
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x000A850F File Offset: 0x000A670F
		public WaitConditionManager ConditionManager
		{
			get
			{
				return this.conditionManager;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000A8518 File Offset: 0x000A6718
		public int TotalQueuedMessages
		{
			get
			{
				int count;
				lock (this.allMailItems)
				{
					count = this.allMailItems.Count;
				}
				return count;
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x000A8560 File Offset: 0x000A6760
		public int? MessagesCompletingCategorization
		{
			get
			{
				if (this.totalPerfCountersInstance == null)
				{
					return null;
				}
				return new int?((int)this.totalPerfCountersInstance.MessagesCompletingCategorization.RawValue);
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x000A8595 File Offset: 0x000A6795
		public object SyncQueues
		{
			get
			{
				return this.syncObject;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x000A859D File Offset: 0x000A679D
		public bool IsPaused
		{
			get
			{
				return this.paused;
			}
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x000A85A8 File Offset: 0x000A67A8
		public void Load()
		{
			if (Components.IsBridgehead && !Components.Configuration.LocalServer.TransportServer.MaxOutboundConnections.IsUnlimited && (Components.TransportAppConfig.ThrottlingConfig.DeliveryTenantThrottlingEnabled || Components.TransportAppConfig.ThrottlingConfig.DeliverySenderThrottlingEnabled))
			{
				this.conditionManager = new MultiQueueWaitConditionManager(Components.Configuration.LocalServer.TransportServer.MaxOutboundConnections.Value, Components.TransportAppConfig.ThrottlingConfig.GetConfig(false), new CostFactory(), null, null, ExTraceGlobals.QueuingTracer, new GetQueueDelegate(this.GetQueue));
			}
			this.endToEndLatencyBuckets = new E2ELatencyBucketsPerfCountersWrapper();
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Loaded remote delivery component.");
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000A866A File Offset: 0x000A686A
		public void Unload()
		{
			this.endToEndLatencyBuckets.Reset();
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Unloaded remote delivery component.");
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000A8688 File Offset: 0x000A6888
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000A868B File Offset: 0x000A688B
		public void Start(bool initiallyPaused, ServiceState targetRunningState)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Starting remote delivery component.");
			if (initiallyPaused)
			{
				this.Pause();
			}
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Started remote delivery component.");
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000A86B8 File Offset: 0x000A68B8
		public void Stop()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Stoping remote delivery component.");
			this.Pause();
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Stopped remote delivery component.");
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000A86E2 File Offset: 0x000A68E2
		public virtual void Pause()
		{
			this.paused = true;
			ExTraceGlobals.PickupTracer.TraceDebug(0L, "Paused remote delivery component.");
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x000A86FC File Offset: 0x000A68FC
		public virtual void Continue()
		{
			this.paused = false;
			ExTraceGlobals.PickupTracer.TraceDebug(0L, "Resumed remote delivery component.");
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x000A8716 File Offset: 0x000A6916
		public void SetPerfCounters(QueuingPerfCountersInstance totalCounters)
		{
			this.totalPerfCountersInstance = totalCounters;
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000A8720 File Offset: 0x000A6920
		public RoutedMessageQueue GetQueue(NextHopSolutionKey key)
		{
			RoutedMessageQueue result;
			if (!this.rmq.TryGetValue(key, out result))
			{
				ExTraceGlobals.QueuingTracer.TraceDebug<Guid, string>(0L, "Queue {0} ({1}) not found", key.NextHopConnector, key.NextHopDomain);
				return null;
			}
			return result;
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x000A8760 File Offset: 0x000A6960
		public TransportMailItem GetMailItem(long mailItemId)
		{
			TransportMailItem result;
			lock (this.allMailItems)
			{
				TransportMailItem transportMailItem;
				result = ((!this.allMailItems.TryGetValue(mailItemId, out transportMailItem)) ? null : transportMailItem);
			}
			return result;
		}

		// Token: 0x06002A6C RID: 10860 RVA: 0x000A87B0 File Offset: 0x000A69B0
		public RoutedMessageQueue[] GetQueueArray()
		{
			RoutedMessageQueue[] result;
			lock (this.SyncQueues)
			{
				RoutedMessageQueue[] array = new RoutedMessageQueue[this.rmq.Count];
				this.rmq.Values.CopyTo(array, 0);
				result = array;
			}
			return result;
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x000A8810 File Offset: 0x000A6A10
		public E2ELatencyBucketsPerfCountersWrapper GetEndToEndLatencyBuckets()
		{
			if (this.endToEndLatencyBuckets == null)
			{
				throw new InvalidOperationException("RemoteDeliveryComponent has not been loaded yet");
			}
			return this.endToEndLatencyBuckets;
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x000A882C File Offset: 0x000A6A2C
		public void VisitMailItems(Func<TransportMailItem, bool> visitor)
		{
			if (visitor == null)
			{
				throw new ArgumentNullException("visitor");
			}
			lock (this.allMailItems)
			{
				foreach (TransportMailItem arg in this.allMailItems.Values)
				{
					if (!visitor(arg))
					{
						break;
					}
				}
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x000A88D0 File Offset: 0x000A6AD0
		public void UpdateQueues()
		{
			RemoteDeliveryComponent.UnreachableMessageQueue.TimedUpdate();
			IEnumerable<RoutedMessageQueue> source = this.UpdateRoutedMessageQueues();
			foreach (RoutedMessageQueue routedMessageQueue in from queue in source
			orderby queue.TotalConnections, queue.LastDeliveryTime
			select queue)
			{
				this.connectionManager.CreateConnectionIfNecessary(routedMessageQueue);
			}
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x000A8984 File Offset: 0x000A6B84
		public void QueueMessageForNextHop(TransportMailItem transportMailItem)
		{
			this.AcquireMailItem(transportMailItem);
			bool flag = true;
			Dictionary<NextHopSolutionKey, NextHopSolution> nextHopSolutions = transportMailItem.NextHopSolutions;
			this.UpdateCounters(transportMailItem, delegate(QueuingPerfCountersInstance c)
			{
				c.ItemsQueuedForDeliveryTotal.Increment();
			});
			foreach (KeyValuePair<NextHopSolutionKey, NextHopSolution> keyValuePair in nextHopSolutions)
			{
				if (keyValuePair.Key.NextHopType.DeliveryType != DeliveryType.ShadowRedundancy)
				{
					RoutedMailItem routedMailItem = new RoutedMailItem(transportMailItem, keyValuePair.Key);
					LatencyTracker.BeginTrackLatency(LatencyTracker.GetDeliveryQueueLatencyComponent(keyValuePair.Key.NextHopType.DeliveryType), routedMailItem.LatencyTracker);
					if (keyValuePair.Key.NextHopType.DeliveryType == DeliveryType.Unreachable)
					{
						Components.ShadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemPreEnqueuing(transportMailItem, RemoteDeliveryComponent.UnreachableMessageQueue);
						RemoteDeliveryComponent.UnreachableMessageQueue.Enqueue(routedMailItem);
						flag = true;
						ExTraceGlobals.QueuingTracer.TraceDebug<long>(0L, "Message {0} enqueued in the unreachable queue", transportMailItem.RecordId);
					}
					else
					{
						RoutedMessageQueue routedMessageQueue;
						lock (this.SyncQueues)
						{
							if (!this.rmq.TryGetValue(keyValuePair.Key, out routedMessageQueue))
							{
								routedMessageQueue = RoutedMessageQueue.NewQueue(keyValuePair.Key, this.conditionManager, string.IsNullOrEmpty(keyValuePair.Key.OverrideSource) ? null : routedMailItem.OrganizationId);
								this.AddToQueueAddRemoveTrace("[CreateQueue] Thread={0}, QueueId={1}, Key={2}, NextHopDomain={3}", new object[]
								{
									Thread.CurrentThread.ManagedThreadId,
									routedMessageQueue.Id,
									routedMessageQueue.Key,
									routedMessageQueue.NextHopDomain
								});
								this.SendWatsonOnDuplicate(routedMessageQueue);
								this.rmq[keyValuePair.Key] = routedMessageQueue;
								this.ids[routedMessageQueue.Id] = routedMessageQueue;
								routedMessageQueue.OnAcquire += this.OnAcquireInternal;
								routedMessageQueue.OnRelease += this.OnReleaseInternal;
							}
							routedMessageQueue.AddReference();
						}
						Components.ShadowRedundancyComponent.ShadowRedundancyManager.NotifyMailItemPreEnqueuing(transportMailItem, routedMessageQueue);
						routedMessageQueue.Enqueue(routedMailItem);
						routedMailItem.UpdateE2ELatencyBucketsOnEnqueue();
						routedMessageQueue.ReleaseReference();
						bool flag3;
						if (!QueueManager.ShouldDehydrateMessage(routedMessageQueue, routedMailItem, out flag3))
						{
							flag = false;
						}
						if (routedMessageQueue.TotalConnections == 0 && flag3)
						{
							this.ConnectionManager.CreateConnectionIfNecessary(routedMessageQueue, ((IQueueItem)transportMailItem).Priority);
						}
					}
				}
			}
			if (flag)
			{
				try
				{
					transportMailItem.CommitLazyAndDehydrateMessageIfPossible(Breadcrumb.DehydrateOnRoutingDone);
				}
				catch (EsentErrorException arg)
				{
					ExTraceGlobals.QueuingTracer.TraceError<int, EsentErrorException>(0L, "Dehydration attempt for {0} failed with {1}", transportMailItem.GetHashCode(), arg);
				}
			}
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x000A8C98 File Offset: 0x000A6E98
		private void SendWatsonOnDuplicate(RoutedMessageQueue routedMessageQueue)
		{
			if (this.ids.ContainsKey(routedMessageQueue.Id))
			{
				this.AddToQueueAddRemoveTrace("[Duplicate] Thread={0}, QueueId={1}, Key={2}, NextHopDomain={3}", new object[]
				{
					Thread.CurrentThread.ManagedThreadId,
					routedMessageQueue.Id,
					routedMessageQueue.Key,
					routedMessageQueue.NextHopDomain
				});
				foreach (RoutedMessageQueue routedMessageQueue2 in this.ids.Values)
				{
					this.AddToQueueAddRemoveTrace("[LookById] QueueId={0}, Key={1}, NextHopDomain={2}", new object[]
					{
						routedMessageQueue2.Id,
						routedMessageQueue2.Key,
						routedMessageQueue2.NextHopDomain
					});
				}
				foreach (RoutedMessageQueue routedMessageQueue3 in this.rmq.Values)
				{
					this.AddToQueueAddRemoveTrace("[LookByNextHop] QueueId={0}, Key={1}, NextHopDomain={2}", new object[]
					{
						routedMessageQueue3.Id,
						routedMessageQueue3.Key,
						routedMessageQueue3.NextHopDomain
					});
				}
				string stackTrace = Environment.StackTrace;
				ExWatson.SendGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, "System.InvalidOperationException", stackTrace, stackTrace.GetHashCode().ToString(), "RemoteDeliveryComponent.SendWatsonOnDuplicate", string.Format("Duplicate queue found. More info:{0}{1}", Environment.NewLine, this.queueAddRemoveTrace));
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x000A8E68 File Offset: 0x000A7068
		private void AddToQueueAddRemoveTrace(string format, params object[] args)
		{
			this.queueAddRemoveTrace.AppendLine(string.Format("[{0}]{1}", DateTime.UtcNow, string.Format(format, args)));
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000A8E94 File Offset: 0x000A7094
		public void AcquireMailItem(TransportMailItem mailItem)
		{
			if (this.OnAcquire != null)
			{
				this.OnAcquire(mailItem);
			}
			mailItem.SetQueuedForDelivery(true);
			lock (this.allMailItems)
			{
				TransportHelpers.AttemptAddToDictionary<long, TransportMailItem>(this.allMailItems, mailItem.RecordId, mailItem, null);
			}
			this.UpdateCountersForAcquireMailItem(mailItem);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000A8F04 File Offset: 0x000A7104
		public void ReleaseMailItem(TransportMailItem mailItem)
		{
			lock (this.allMailItems)
			{
				this.allMailItems.Remove(mailItem.RecordId);
			}
			mailItem.SetQueuedForDelivery(false);
			if (this.OnRelease != null)
			{
				this.OnRelease(mailItem);
			}
			this.UpdateCountersForReleasedMailItem(mailItem);
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000A8F74 File Offset: 0x000A7174
		private void UpdateCountersForAcquireMailItem(TransportMailItem mailItem)
		{
			if (this.totalPerfCountersInstance != null)
			{
				this.totalPerfCountersInstance.MessagesQueuedForDelivery.Increment();
				this.totalPerfCountersInstance.MessagesQueuedForDeliveryTotal.Increment();
			}
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000A8FA0 File Offset: 0x000A71A0
		private void UpdateCountersForReleasedMailItem(TransportMailItem mailItem)
		{
			if (this.totalPerfCountersInstance != null)
			{
				this.totalPerfCountersInstance.MessagesQueuedForDelivery.Decrement();
				this.totalPerfCountersInstance.MessagesCompletedDeliveryTotal.Increment();
			}
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000A8FCC File Offset: 0x000A71CC
		private void UpdateCounters(TransportMailItem item, Action<QueuingPerfCountersInstance> updateCounter)
		{
			if (updateCounter == null)
			{
				throw new InvalidOperationException("No update action provided");
			}
			foreach (KeyValuePair<NextHopSolutionKey, NextHopSolution> keyValuePair in item.NextHopSolutions)
			{
				Components.QueueManager.UpateInstanceCounter(keyValuePair.Key.RiskLevel, item.Priority, updateCounter);
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000A9056 File Offset: 0x000A7256
		public virtual void CommitLazyAndDehydrateMessages()
		{
			this.VisitMailItems(delegate(TransportMailItem mailItem)
			{
				mailItem.CommitLazyAndDehydrateMessageIfPossible(Breadcrumb.DehydrateOnBackPressure);
				return true;
			});
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000A907C File Offset: 0x000A727C
		public List<RoutedMessageQueue> FindByQueueIdentity(QueueIdentity queueIdentity)
		{
			if (!queueIdentity.IsLocal || queueIdentity.Type != QueueType.Delivery)
			{
				return new List<RoutedMessageQueue>(0);
			}
			if (queueIdentity.RowId > 0L)
			{
				List<RoutedMessageQueue> list = new List<RoutedMessageQueue>(1);
				RoutedMessageQueue routedMessageQueue = this.FindById(queueIdentity.RowId);
				if (routedMessageQueue != null && routedMessageQueue.IsAdminVisible)
				{
					list.Add(routedMessageQueue);
				}
				return list;
			}
			return this.FindByDomain(queueIdentity.NextHopDomain);
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000A90E0 File Offset: 0x000A72E0
		public void LoadQueue(RoutedQueueBase queueStorage)
		{
			RoutedMessageQueue routedMessageQueue = RoutedMessageQueue.LoadQueue(queueStorage, this.conditionManager);
			if (routedMessageQueue != null)
			{
				routedMessageQueue.OnAcquire += this.OnAcquireInternal;
				routedMessageQueue.OnRelease += this.OnReleaseInternal;
				this.AddToQueueAddRemoveTrace("[LoadQueue] Thread={0}, QueueId={1}, Key={2}, NextHopDomain={3}", new object[]
				{
					Thread.CurrentThread.ManagedThreadId,
					routedMessageQueue.Id,
					routedMessageQueue.Key,
					routedMessageQueue.NextHopDomain
				});
				this.SendWatsonOnDuplicate(routedMessageQueue);
				this.ids[routedMessageQueue.Id] = routedMessageQueue;
				this.rmq[routedMessageQueue.Key] = routedMessageQueue;
			}
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000A9199 File Offset: 0x000A7399
		private void OnAcquireInternal(RoutedMailItem routedMailItem)
		{
			if (this.OnAcquireRoutedMailItem != null)
			{
				this.OnAcquireRoutedMailItem(routedMailItem);
			}
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000A91AF File Offset: 0x000A73AF
		private void OnReleaseInternal(RoutedMailItem routedMailItem)
		{
			if (this.OnReleaseRoutedMailItem != null)
			{
				this.OnReleaseRoutedMailItem(routedMailItem);
			}
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x000A91C8 File Offset: 0x000A73C8
		private RoutedMessageQueue FindById(long id)
		{
			RoutedMessageQueue result;
			lock (this.SyncQueues)
			{
				RoutedMessageQueue routedMessageQueue;
				result = (this.ids.TryGetValue(id, out routedMessageQueue) ? routedMessageQueue : null);
			}
			return result;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000A9218 File Offset: 0x000A7418
		private List<RoutedMessageQueue> FindByDomain(string domain)
		{
			List<RoutedMessageQueue> list = new List<RoutedMessageQueue>();
			lock (this.SyncQueues)
			{
				foreach (RoutedMessageQueue routedMessageQueue in this.ids.Values)
				{
					if (string.Equals(domain, routedMessageQueue.NextHopDomain, StringComparison.OrdinalIgnoreCase) && routedMessageQueue.IsAdminVisible)
					{
						list.Add(routedMessageQueue);
					}
				}
			}
			return list;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000AA094 File Offset: 0x000A8294
		private IEnumerable<RoutedMessageQueue> UpdateRoutedMessageQueues()
		{
			RoutedMessageQueue[] queues = this.GetQueueArray();
			int[] totalExternalActiveRemoteDelivery = new int[QueueManager.InstanceCountersLength];
			int[] totalInternalActiveRemoteDelivery = new int[QueueManager.InstanceCountersLength];
			int[] totalExternalRetryRemoteDelivery = new int[QueueManager.InstanceCountersLength];
			int[] totalInternalRetryRemoteDelivery = new int[QueueManager.InstanceCountersLength];
			int[] totalActiveMailboxDelivery = new int[QueueManager.InstanceCountersLength];
			int[] totalRetryMailboxDelivery = new int[QueueManager.InstanceCountersLength];
			int[] totalActiveNonSmtpDelivery = new int[QueueManager.InstanceCountersLength];
			int[] totalRetryNonSmtpDelivery = new int[QueueManager.InstanceCountersLength];
			int[] maxInternalQueueLength = new int[QueueManager.InstanceCountersLength];
			int[] maxExternalQueueLength = new int[QueueManager.InstanceCountersLength];
			int maxTotalInternalQueueLength = 0;
			int maxTotalInternalUnlockedQueueLength = 0;
			int maxTotalExternalQueueLength = 0;
			int maxTotalExternalUnlockedQueueLength = 0;
			bool healthUpdateNeeded = this.healthTracker != null && this.healthTracker.StartRefresh();
			for (int i = 0; i < queues.Length; i++)
			{
				queues[i].UpdateQueue();
				int[] activeCountForPerfCounter;
				int[] retryCountForPerfCounter;
				queues[i].GetQueueCounts(out activeCountForPerfCounter, out retryCountForPerfCounter);
				if (NextHopType.IsMailboxDeliveryType(queues[i].Key.NextHopType.DeliveryType))
				{
					totalActiveMailboxDelivery = totalActiveMailboxDelivery.Zip(activeCountForPerfCounter, (int a, int b) => a + b).ToArray<int>();
					totalRetryMailboxDelivery = totalRetryMailboxDelivery.Zip(retryCountForPerfCounter, (int a, int b) => a + b).ToArray<int>();
				}
				else if (queues[i].Key.NextHopType.DeliveryType == DeliveryType.NonSmtpGatewayDelivery)
				{
					totalActiveNonSmtpDelivery = totalActiveNonSmtpDelivery.Zip(activeCountForPerfCounter, (int a, int b) => a + b).ToArray<int>();
					totalRetryNonSmtpDelivery = totalRetryNonSmtpDelivery.Zip(retryCountForPerfCounter, (int a, int b) => a + b).ToArray<int>();
				}
				else if (queues[i].Key.NextHopType.DeliveryType != DeliveryType.Heartbeat)
				{
					if (queues[i].Key.NextHopType.NextHopCategory == NextHopCategory.External)
					{
						totalExternalActiveRemoteDelivery = totalExternalActiveRemoteDelivery.Zip(activeCountForPerfCounter, (int a, int b) => a + b).ToArray<int>();
						totalExternalRetryRemoteDelivery = totalExternalRetryRemoteDelivery.Zip(retryCountForPerfCounter, (int a, int b) => a + b).ToArray<int>();
					}
					else
					{
						totalInternalActiveRemoteDelivery = totalInternalActiveRemoteDelivery.Zip(activeCountForPerfCounter, (int a, int b) => a + b).ToArray<int>();
						totalInternalRetryRemoteDelivery = totalInternalRetryRemoteDelivery.Zip(retryCountForPerfCounter, (int a, int b) => a + b).ToArray<int>();
					}
				}
				if (queues[i].Key.NextHopType.NextHopCategory == NextHopCategory.External)
				{
					maxTotalExternalQueueLength = Math.Max(maxTotalExternalQueueLength, Components.QueueManager.GetTotalFromInstance(activeCountForPerfCounter) + Components.QueueManager.GetTotalFromInstance(retryCountForPerfCounter));
					maxTotalExternalUnlockedQueueLength = Math.Max(maxTotalExternalUnlockedQueueLength, queues[i].ActiveCount);
					maxExternalQueueLength = maxExternalQueueLength.Zip(activeCountForPerfCounter.Zip(retryCountForPerfCounter, (int a, int b) => a + b), (int a, int b) => Math.Max(a, b)).ToArray<int>();
				}
				else
				{
					maxTotalInternalQueueLength = Math.Max(maxTotalInternalQueueLength, Components.QueueManager.GetTotalFromInstance(activeCountForPerfCounter) + Components.QueueManager.GetTotalFromInstance(retryCountForPerfCounter));
					maxTotalInternalUnlockedQueueLength = Math.Max(maxTotalInternalUnlockedQueueLength, queues[i].ActiveCount);
					maxInternalQueueLength = maxInternalQueueLength.Zip(activeCountForPerfCounter.Zip(retryCountForPerfCounter, (int a, int b) => a + b), (int a, int b) => Math.Max(a, b)).ToArray<int>();
				}
				if (healthUpdateNeeded)
				{
					this.healthTracker.UpdateHealthUsingQueueData(queues[i]);
				}
				if (queues[i].CanResubmit(Components.TransportAppConfig.RemoteDelivery.MaxIdleTimeBeforeResubmit))
				{
					queues[i].ResetScheduledCallback();
					queues[i].Resubmit(ResubmitReason.Inactivity, null);
				}
				else if (queues[i].ActiveQueueLength > 0)
				{
					yield return queues[i];
				}
				else if (queues[i].CanBeDeleted(Components.Configuration.LocalServer.TransportServer.QueueMaxIdleTime))
				{
					lock (this.SyncQueues)
					{
						if (queues[i].CanBeDeleted(Components.Configuration.LocalServer.TransportServer.QueueMaxIdleTime))
						{
							this.AddToQueueAddRemoveTrace("[RemoveQueue_NextHop] Thread={0}, QueueId={1}, Key={2}, NextHopDomain={3}", new object[]
							{
								Thread.CurrentThread.ManagedThreadId,
								queues[i].Id,
								queues[i].Key,
								queues[i].NextHopDomain
							});
							this.rmq.Remove(queues[i].Key);
							queues[i].OnAcquire -= this.OnAcquireInternal;
							queues[i].OnRelease -= this.OnReleaseInternal;
							queues[i].ResetScheduledCallback();
							queues[i].Delete();
							this.AddToQueueAddRemoveTrace("[RemoveQueue_Ids] Thread={0}, QueueId={1}, Key={2}, NextHopDomain={3}", new object[]
							{
								Thread.CurrentThread.ManagedThreadId,
								queues[i].Id,
								queues[i].Key,
								queues[i].NextHopDomain
							});
							this.ids.Remove(queues[i].Id);
						}
					}
				}
			}
			if (healthUpdateNeeded)
			{
				this.healthTracker.CompleteRefresh();
			}
			if (this.totalPerfCountersInstance != null)
			{
				Components.QueueManager.UpdateAllInstanceCounters(totalActiveMailboxDelivery, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.ActiveMailboxDeliveryQueueLength.RawValue = (long)v;
				});
				Components.QueueManager.UpdateAllInstanceCounters(totalRetryMailboxDelivery, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.RetryMailboxDeliveryQueueLength.RawValue = (long)v;
				});
				Components.QueueManager.UpdateAllInstanceCounters(totalActiveNonSmtpDelivery, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.ActiveNonSmtpDeliveryQueueLength.RawValue = (long)v;
				});
				Components.QueueManager.UpdateAllInstanceCounters(totalRetryNonSmtpDelivery, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.RetryNonSmtpDeliveryQueueLength.RawValue = (long)v;
				});
				Components.QueueManager.UpdateAllInstanceCounters(totalInternalActiveRemoteDelivery, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.InternalActiveRemoteDeliveryQueueLength.RawValue = (long)v;
				});
				Components.QueueManager.UpdateAllInstanceCounters(totalExternalActiveRemoteDelivery, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.ExternalActiveRemoteDeliveryQueueLength.RawValue = (long)v;
				});
				Components.QueueManager.UpdateAllInstanceCounters(totalExternalRetryRemoteDelivery, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.ExternalRetryRemoteDeliveryQueueLength.RawValue = (long)v;
				});
				Components.QueueManager.UpdateAllInstanceCounters(totalInternalRetryRemoteDelivery, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.InternalRetryRemoteDeliveryQueueLength.RawValue = (long)v;
				});
				Components.QueueManager.UpdateAllInstanceCounters(totalActiveMailboxDelivery.Zip(totalRetryMailboxDelivery, (int a, int b) => a + b).Zip(totalActiveNonSmtpDelivery, (int a, int b) => a + b).Zip(totalRetryNonSmtpDelivery, (int a, int b) => a + b).Zip(totalInternalActiveRemoteDelivery, (int a, int b) => a + b).Zip(totalInternalRetryRemoteDelivery, (int a, int b) => a + b).ToArray<int>(), delegate(QueuingPerfCountersInstance c, int v)
				{
					c.InternalAggregateDeliveryQueueLength.RawValue = (long)v;
				}, true);
				Components.QueueManager.UpdateAllInstanceCounters(totalExternalActiveRemoteDelivery.Zip(totalExternalRetryRemoteDelivery, (int a, int b) => a + b).ToArray<int>(), delegate(QueuingPerfCountersInstance c, int v)
				{
					c.ExternalAggregateDeliveryQueueLength.RawValue = (long)v;
				}, true);
				Components.QueueManager.UpdateAllInstanceCounters(maxExternalQueueLength, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.ExternalLargestDeliveryQueueLength.RawValue = (long)v;
				}, false);
				Components.QueueManager.UpdateAllInstanceCounters(maxInternalQueueLength, delegate(QueuingPerfCountersInstance c, int v)
				{
					c.InternalLargestDeliveryQueueLength.RawValue = (long)v;
				}, false);
				this.totalPerfCountersInstance.InternalLargestDeliveryQueueLength.RawValue = (long)maxTotalInternalQueueLength;
				this.totalPerfCountersInstance.ExternalLargestDeliveryQueueLength.RawValue = (long)maxTotalExternalQueueLength;
				this.totalPerfCountersInstance.InternalLargestUnlockedDeliveryQueueLength.RawValue = (long)maxTotalInternalUnlockedQueueLength;
				this.totalPerfCountersInstance.ExternalLargestUnlockedDeliveryQueueLength.RawValue = (long)maxTotalExternalUnlockedQueueLength;
			}
			yield break;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x000AA0B1 File Offset: 0x000A82B1
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "RemoteDelivery";
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x000AA0B8 File Offset: 0x000A82B8
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
			bool flag = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("conditionalQueuing", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag3 = parameters.Argument.IndexOf("config", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag4 = parameters.Argument.IndexOf("diversity", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag5 = (!flag3 && !flag4) || parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
			if (flag3)
			{
				XElement xelement2 = new XElement("config");
				xelement2.Add(TransportAppConfig.GetDiagnosticInfoForType(Components.TransportAppConfig.RemoteDelivery));
				xelement2.Add(TransportAppConfig.GetDiagnosticInfoForType(Components.TransportAppConfig.DeliveryQueuePrioritizationConfiguration));
				xelement2.Add(TransportAppConfig.GetDiagnosticInfoForType(Components.TransportAppConfig.ThrottlingConfig));
				xelement.Add(xelement2);
			}
			if (flag2 && this.conditionManager != null)
			{
				xelement.Add(this.conditionManager.GetDiagnosticInfo(flag));
			}
			if (flag)
			{
				RoutedMessageQueue[] queueArray = this.GetQueueArray();
				XElement xelement3 = new XElement("queues");
				foreach (RoutedMessageQueue routedMessageQueue in queueArray)
				{
					xelement3.Add(routedMessageQueue.GetDiagnosticInfo(true, flag2));
				}
				xelement.Add(xelement3);
				if (this.healthTracker != null)
				{
					xelement.Add(this.healthTracker.GetDiagnosticInfo());
				}
			}
			if (flag5)
			{
				xelement.Add(new XElement("help", "Supported arguments: config, conditionalQueuing, verbose, diversity:" + QueueDiversity.UsageString));
			}
			if (flag4)
			{
				string requestArgument = parameters.Argument.Substring(parameters.Argument.IndexOf("diversity", StringComparison.OrdinalIgnoreCase) + "diversity".Length);
				this.GetDiversityDiagnosticInfo(xelement, requestArgument);
			}
			return xelement;
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x000AA2A8 File Offset: 0x000A84A8
		private void GetDiversityDiagnosticInfo(XElement deliveryElement, string requestArgument)
		{
			QueueDiversity queueDiversity;
			string text;
			if (QueueDiversity.TryParse(requestArgument, false, out queueDiversity, out text))
			{
				QueueType type = queueDiversity.QueueId.Type;
				if (type != QueueType.Delivery)
				{
					if (type == QueueType.Unreachable)
					{
						deliveryElement.Add(queueDiversity.GetDiagnosticInfo(UnreachableMessageQueue.Instance));
					}
					else
					{
						deliveryElement.Add(queueDiversity.GetComponentAdvice());
					}
				}
				else if (this.ids.ContainsKey(queueDiversity.QueueId.RowId))
				{
					deliveryElement.Add(queueDiversity.GetDiagnosticInfo(this.ids[queueDiversity.QueueId.RowId]));
				}
				else
				{
					text = string.Format("Remote Queue doesn't have queue id = '{0}'", queueDiversity.QueueId.RowId);
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				deliveryElement.Add(new XElement("Error", text));
			}
		}

		// Token: 0x0400156E RID: 5486
		private readonly Dictionary<NextHopSolutionKey, RoutedMessageQueue> rmq = new Dictionary<NextHopSolutionKey, RoutedMessageQueue>();

		// Token: 0x0400156F RID: 5487
		private readonly Dictionary<long, RoutedMessageQueue> ids = new Dictionary<long, RoutedMessageQueue>();

		// Token: 0x04001570 RID: 5488
		private readonly object syncObject = new object();

		// Token: 0x04001571 RID: 5489
		private readonly StringBuilder queueAddRemoveTrace = new StringBuilder();

		// Token: 0x04001572 RID: 5490
		private ConnectionManager connectionManager;

		// Token: 0x04001573 RID: 5491
		private MultiQueueWaitConditionManager conditionManager;

		// Token: 0x04001574 RID: 5492
		private QueuingPerfCountersInstance totalPerfCountersInstance;

		// Token: 0x04001575 RID: 5493
		private E2ELatencyBucketsPerfCountersWrapper endToEndLatencyBuckets;

		// Token: 0x04001576 RID: 5494
		private RemoteDeliveryHealthTracker healthTracker;

		// Token: 0x04001577 RID: 5495
		private Dictionary<long, TransportMailItem> allMailItems = new Dictionary<long, TransportMailItem>();

		// Token: 0x04001578 RID: 5496
		private bool paused;
	}
}
