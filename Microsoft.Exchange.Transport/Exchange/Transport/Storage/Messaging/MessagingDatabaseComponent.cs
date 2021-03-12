using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.ResourceMonitoring;

namespace Microsoft.Exchange.Transport.Storage.Messaging
{
	// Token: 0x020000F8 RID: 248
	internal class MessagingDatabaseComponent : IMessagingDatabaseComponent, IStartableTransportComponent, ITransportComponent, IDiagnosable
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00024684 File Offset: 0x00022884
		public IMessagingDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002468C File Offset: 0x0002288C
		public string CurrentState
		{
			get
			{
				return this.database.CurrentState;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00024699 File Offset: 0x00022899
		public IEnumerable<RoutedQueueBase> Queues
		{
			get
			{
				return this.queuesByNextHopSolutionKey.Values;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000246A8 File Offset: 0x000228A8
		public RoutedQueueBase GetQueue(NextHopSolutionKey queueNextHopSolutionKey)
		{
			RoutedQueueBase result;
			if (!this.TryGetQueue(queueNextHopSolutionKey, out result))
			{
				throw new KeyNotFoundException(string.Format("Unable to find queue with key={0}", queueNextHopSolutionKey));
			}
			return result;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000246D8 File Offset: 0x000228D8
		public RoutedQueueBase GetOrAddQueue(NextHopSolutionKey queueNextHopSolutionKey)
		{
			RoutedQueueBase result;
			if (!this.TryGetQueue(queueNextHopSolutionKey, out result))
			{
				result = this.CreateQueue(queueNextHopSolutionKey, false);
			}
			return result;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000246FA File Offset: 0x000228FA
		public bool TryGetQueue(NextHopSolutionKey queueNextHopSolutionKey, out RoutedQueueBase queue)
		{
			return this.queuesByNextHopSolutionKey.TryGetValue(queueNextHopSolutionKey, out queue);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002470C File Offset: 0x0002290C
		public RoutedQueueBase CreateQueue(NextHopSolutionKey key, bool suspendQueue)
		{
			long id = Interlocked.Increment(ref this.currentQueueId);
			RoutedQueueBase routedQueueBase = new RoutedQueueBase(id, key)
			{
				Suspended = suspendQueue
			};
			if (this.queuesByNextHopSolutionKey.TryAdd(key, routedQueueBase))
			{
				routedQueueBase.Commit();
			}
			else
			{
				routedQueueBase = this.queuesByNextHopSolutionKey[key];
			}
			return routedQueueBase;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002475C File Offset: 0x0002295C
		public void Load()
		{
			this.database.Attach(this.config);
			this.meterableDataSource = MeterableJetDataSourceFactory.CreateMeterableDataSource(this.database.DataSource);
			this.perfCounters = DatabasePerfCounters.GetInstance("other");
			this.statisticsTimer = new GuardedTimer(new TimerCallback(this.UpdateStatistics), null, this.config.StatisticsUpdateInterval);
			this.ScanQueueTable();
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000247C9 File Offset: 0x000229C9
		public void Unload()
		{
			if (this.statisticsTimer != null)
			{
				this.statisticsTimer.Dispose(true);
			}
			this.meterableDataSource = null;
			this.database.Detach();
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000247F1 File Offset: 0x000229F1
		public string OnUnhandledException(Exception e)
		{
			if (this.Database.DataSource != null)
			{
				return "Messaging Database flush result: " + (this.Database.DataSource.TryForceFlush() ?? "success.");
			}
			return null;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00024825 File Offset: 0x00022A25
		public void SetLoadTimeDependencies(IMessagingDatabaseConfig config)
		{
			ArgumentValidator.ThrowIfNull("config", config);
			this.config = config;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00024839 File Offset: 0x00022A39
		public IBootLoader CreateBootScanner()
		{
			return StorageFactory.CreateBootScanner();
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00024840 File Offset: 0x00022A40
		public string GetDiagnosticComponentName()
		{
			return "MessagingDatabase";
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00024848 File Offset: 0x00022A48
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			XElement xelement = new XElement("MessagingDatabase");
			if (parameters.Argument.Equals("config", StringComparison.InvariantCultureIgnoreCase))
			{
				xelement.Add(TransportAppConfig.GetDiagnosticInfoForType(this.config));
			}
			else
			{
				xelement.Add(this.Database.GetDiagnosticInfo(parameters));
			}
			return xelement;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0002489F File Offset: 0x00022A9F
		public void Start(bool initiallyPaused, ServiceState intendedState)
		{
			this.database.Start();
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000248AC File Offset: 0x00022AAC
		public void Stop()
		{
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000248AE File Offset: 0x00022AAE
		public void Pause()
		{
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000248B0 File Offset: 0x00022AB0
		public void Continue()
		{
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000248B4 File Offset: 0x00022AB4
		private void UpdateStatistics(object state)
		{
			if (this.perfCounters == null || this.meterableDataSource == null)
			{
				return;
			}
			this.perfCounters.TransportQueueDatabaseFileSize.RawValue = (long)ByteQuantifiedSize.FromBytes((ulong)this.meterableDataSource.GetDatabaseFileSize()).ToMB();
			this.perfCounters.TransportQueueDatabaseInternalFreeSpace.RawValue = (long)ByteQuantifiedSize.FromBytes((ulong)this.meterableDataSource.GetAvailableFreeSpace()).ToMB();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00024924 File Offset: 0x00022B24
		private void ScanQueueTable()
		{
			QueueTable queueTable = this.database.QueueTable;
			this.currentQueueId = 0L;
			using (DataTableCursor cursor = queueTable.GetCursor())
			{
				using (cursor.BeginTransaction())
				{
					bool flag = cursor.TryMoveFirst();
					while (flag)
					{
						RoutedQueueBase routedQueueBase = RoutedQueueBase.LoadFromRow(cursor);
						NextHopSolutionKey key = new NextHopSolutionKey(routedQueueBase.NextHopType.DeliveryType, routedQueueBase.NextHopDomain, routedQueueBase.NextHopConnector, routedQueueBase.NextHopTlsDomain);
						this.queuesByNextHopSolutionKey.TryAdd(key, routedQueueBase);
						if (routedQueueBase.Id > this.currentQueueId)
						{
							this.currentQueueId = routedQueueBase.Id;
						}
						flag = cursor.TryMoveNext(false);
					}
				}
			}
		}

		// Token: 0x04000454 RID: 1108
		private readonly IMessagingDatabase database = StorageFactory.GetNewDatabaseInstance();

		// Token: 0x04000455 RID: 1109
		private readonly ConcurrentDictionary<NextHopSolutionKey, RoutedQueueBase> queuesByNextHopSolutionKey = new ConcurrentDictionary<NextHopSolutionKey, RoutedQueueBase>();

		// Token: 0x04000456 RID: 1110
		private IMessagingDatabaseConfig config;

		// Token: 0x04000457 RID: 1111
		private long currentQueueId;

		// Token: 0x04000458 RID: 1112
		private DatabasePerfCountersInstance perfCounters;

		// Token: 0x04000459 RID: 1113
		private IMeterableJetDataSource meterableDataSource;

		// Token: 0x0400045A RID: 1114
		private GuardedTimer statisticsTimer;
	}
}
