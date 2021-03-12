using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000262 RID: 610
	internal class RoutingTablesLoader
	{
		// Token: 0x06001A5F RID: 6751 RVA: 0x0006C370 File Offset: 0x0006A570
		public RoutingTablesLoader(IMailRouter parentRouter)
		{
			RoutingUtils.ThrowIfNull(parentRouter, "parentRouter");
			this.parentRouter = parentRouter;
			this.syncObject = new object();
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06001A60 RID: 6752 RVA: 0x0006C398 File Offset: 0x0006A598
		// (remove) Token: 0x06001A61 RID: 6753 RVA: 0x0006C3D0 File Offset: 0x0006A5D0
		public event RoutingTablesChangedHandler RoutingTablesChanged;

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x0006C405 File Offset: 0x0006A605
		public RoutingTables RoutingTables
		{
			get
			{
				return this.routingTables;
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0006C410 File Offset: 0x0006A610
		public bool LoadAndSubscribe(RoutingContextCore context)
		{
			RoutingUtils.ThrowIfNull(context, "context");
			this.context = context;
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Loading configuration information for routing and subscribing");
			this.context.Dependencies.RegisterForLocalServerChanges(new ConfigurationUpdateHandler<TransportServerConfiguration>(this.HandleLocalServerConfigChange));
			if (this.context.IsEdgeMode)
			{
				this.context.EdgeDependencies.RegisterForAcceptedDomainChanges(new ConfigurationUpdateHandler<AcceptedDomainTable>(this.HandleAcceptedDomainChange));
			}
			this.routingTables = null;
			this.databaseLoader = new DatabaseLoader(this.context);
			if (this.context.Settings.DagSelectorEnabled)
			{
				this.tenantDagQuota = new TenantDagQuota(this.context.Settings.TenantDagQuotaDagsPerTenant, this.context.Settings.TenantDagQuotaMessagesPerDag, this.context.Settings.TenantDagQuotaPastWeight, this.context.Settings.DagSelectorLogDiagnosticInfo);
			}
			IList<ADNotificationRequestCookie> list = null;
			try
			{
				int num = 0;
				while (this.routingTables == null)
				{
					ADOperationResult adoperationResult = ADOperationResult.Success;
					RoutingTopologyBase routingTopologyBase = this.CreateTopologyConfig();
					if (list == null)
					{
						adoperationResult = routingTopologyBase.TryRegisterForADNotifications(new ADNotificationCallback(this.HandleADChangeNotification), out list);
					}
					if (adoperationResult.Succeeded)
					{
						adoperationResult = this.TryLoadRoutingTablesAndNotify(routingTopologyBase, false);
					}
					if (!adoperationResult.Succeeded && !this.HandleFailedInitialLoadAttempt(adoperationResult, num))
					{
						RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Leaving LoadAndSubscribe() because the process is shutting down");
						return false;
					}
					num++;
				}
				lock (this.syncObject)
				{
					if (this.context.Dependencies.IsProcessShuttingDown)
					{
						RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Leaving LoadAndSubscribe() because the process is shutting down (II)");
						return false;
					}
					Interlocked.Exchange(ref this.lastUpdateTimeTicks, DateTime.UtcNow.Ticks);
					RoutingDiag.Tracer.TraceDebug(0L, "Creating reload timer");
					this.reloadTimer = new GuardedTimer(new TimerCallback(this.ReloadRoutingTables), null, this.context.Settings.ConfigReloadInterval, this.context.Settings.ConfigReloadInterval);
					this.context.Dependencies.RegisterForServiceControlConfigUpdates(new EventHandler(this.ForceReloadRoutingTables));
					this.notificationCookies = list;
					list = null;
				}
			}
			finally
			{
				if (list != null)
				{
					RoutingTopologyBase.UnregisterFromADNotifications(list);
				}
			}
			if (this.routingTableReloadRequired)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ReloadRoutingTables));
			}
			return true;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0006C6B0 File Offset: 0x0006A8B0
		public void Unsubscribe()
		{
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Unsubscribing from all configuration change notifications");
			GuardedTimer guardedTimer = null;
			IList<ADNotificationRequestCookie> list = null;
			lock (this.syncObject)
			{
				guardedTimer = this.reloadTimer;
				this.reloadTimer = null;
				list = this.notificationCookies;
				this.notificationCookies = null;
			}
			if (list != null)
			{
				RoutingTopologyBase.UnregisterFromADNotifications(list);
			}
			if (guardedTimer != null)
			{
				guardedTimer.Dispose(true);
				this.context.Dependencies.UnregisterFromServiceControlConfigUpdates(new EventHandler(this.ForceReloadRoutingTables));
			}
			if (this.context.IsEdgeMode)
			{
				this.context.EdgeDependencies.UnregisterFromAcceptedDomainChanges(new ConfigurationUpdateHandler<AcceptedDomainTable>(this.HandleAcceptedDomainChange));
			}
			this.context.Dependencies.UnregisterFromLocalServerChanges(new ConfigurationUpdateHandler<TransportServerConfiguration>(this.HandleLocalServerConfigChange));
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0006C794 File Offset: 0x0006A994
		public bool TryGetDiagnosticInfo(bool verbose, DiagnosableParameters parameters, out XElement diagnosticInfo)
		{
			if (this.tenantDagQuota == null)
			{
				diagnosticInfo = null;
				return false;
			}
			return this.tenantDagQuota.TryGetDiagnosticInfo(verbose, parameters, out diagnosticInfo);
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0006C7B4 File Offset: 0x0006A9B4
		private bool HandleFailedInitialLoadAttempt(ADOperationResult result, int attemptNumber)
		{
			if (attemptNumber >= 5)
			{
				RoutingDiag.Tracer.TraceError<int>((long)this.GetHashCode(), "Max number of retries ({0}) to load routing config data reached. Stopping the service.", 5);
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingMaxConfigLoadRetriesReached, null, new object[0]);
				throw new TransportComponentLoadFailedException(Strings.RoutingMaxConfigLoadRetriesReached, result.Exception);
			}
			RoutingDiag.Tracer.TraceError<int>((long)this.GetHashCode(), "Failed to load configuration information for routing; will block the process and retry in {0} seconds", 10);
			RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingWillRetryLoad, null, new object[]
			{
				10
			});
			for (int i = 0; i < 10; i++)
			{
				if (this.context.Dependencies.IsProcessShuttingDown)
				{
					return false;
				}
				Thread.Sleep(1000);
			}
			return true;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0006C8B4 File Offset: 0x0006AAB4
		private ADOperationResult TryLoadRoutingTablesAndNotify(RoutingTopologyBase topologyConfig, bool forcedReload)
		{
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Loading routing tables");
			RoutingTables newTables = null;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				topologyConfig.PreLoad();
				newTables = new RoutingTables(topologyConfig, this.context, this.tenantDagQuota, forcedReload);
			}, 0);
			if (!adoperationResult.Succeeded)
			{
				TransientRoutingException ex = adoperationResult.Exception as TransientRoutingException;
				if (ex != null)
				{
					RoutingDiag.Tracer.TraceError<DateTime, TransientRoutingException>((long)this.GetHashCode(), "[{0}] RoutingTables load failed with transient error. Exception details: {1}", topologyConfig.WhenCreated, ex);
					RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingTransientConfigError, null, new object[]
					{
						ex.LocalizedString,
						ex
					});
				}
				else
				{
					RoutingDiag.Tracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to load Routing tables; AD Exception occurred: {0}", adoperationResult.Exception);
					RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_RoutingAdUnavailable, null, new object[0]);
				}
				return adoperationResult;
			}
			this.context.PerfCounters.IncrementRoutingTablesCalculated();
			bool flag;
			bool flag2;
			this.MatchAndReplaceRoutingTables(newTables, out flag, out flag2);
			if (flag2)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.LogRoutingTables), Tuple.Create<RoutingTables, RoutingTopologyBase>(newTables, topologyConfig));
				if (!flag)
				{
					this.context.PerfCounters.IncrementRoutingTablesChanged();
				}
				RoutingTablesChangedHandler routingTablesChanged = this.RoutingTablesChanged;
				if (routingTablesChanged != null)
				{
					routingTablesChanged(this.parentRouter, newTables.WhenCreated, !flag);
				}
			}
			return adoperationResult;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0006CA3C File Offset: 0x0006AC3C
		private void MatchAndReplaceRoutingTables(RoutingTables newTables, out bool matched, out bool replaced)
		{
			replaced = false;
			matched = false;
			RoutingTables routingTables;
			lock (this.syncObject)
			{
				routingTables = this.routingTables;
				if (this.context.Dependencies.IsProcessShuttingDown)
				{
					if (routingTables == null)
					{
						this.routingTables = newTables;
					}
					return;
				}
				if (routingTables == null || newTables.WhenCreated > routingTables.WhenCreated)
				{
					if (routingTables != null)
					{
						matched = routingTables.Match(newTables);
					}
					this.routingTables = newTables;
					replaced = true;
				}
			}
			if (replaced)
			{
				RoutingDiag.Tracer.TraceDebug<object, DateTime>((long)this.GetHashCode(), "Replaced routing tables [{0}] with [{1}]", (routingTables == null) ? "null" : routingTables.WhenCreated, newTables.WhenCreated);
				if (this.context.ServerRoutingSupported && routingTables != null)
				{
					routingTables.DatabaseMap.LogDagSelectorInfo();
					return;
				}
			}
			else
			{
				RoutingDiag.Tracer.TraceDebug<object, DateTime>((long)this.GetHashCode(), "Routing tables [{0}] are newer than [{1}] and were not replaced", (routingTables == null) ? "null" : routingTables.WhenCreated, newTables.WhenCreated);
			}
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0006CB54 File Offset: 0x0006AD54
		private RoutingTopologyBase CreateTopologyConfig()
		{
			if (!this.context.IsEdgeMode)
			{
				return new RoutingTopology(this.databaseLoader, this.context);
			}
			return new EdgeRoutingTopology();
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x0006CB7A File Offset: 0x0006AD7A
		private void HandleLocalServerConfigChange(TransportServerConfiguration transportServerConfiguration)
		{
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "HandleLocalServerConfigChange() callback invoked");
			RoutingTableLogFileManager.HandleTransportServerConfigChange(transportServerConfiguration);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0006CB98 File Offset: 0x0006AD98
		private void HandleAcceptedDomainChange(AcceptedDomainTable acceptedDomainTable)
		{
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "HandleLocalAcceptedDomainChange() callback invoked");
			this.HandleConfigurationChange();
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0006CBB8 File Offset: 0x0006ADB8
		private void HandleADChangeNotification(ADNotificationEventArgs args)
		{
			RoutingDiag.Tracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "HandleAdChangeNotification() callback invoked due to {0}", args.Id);
			if (this.context.IsEdgeMode && this.context.EdgeDependencies.IsLocalServerId(args.Id))
			{
				return;
			}
			this.HandleConfigurationChange();
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x0006CC0D File Offset: 0x0006AE0D
		private void ForceReloadRoutingTables(object source, EventArgs args)
		{
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "ForceReloadRoutingTables() callback invoked");
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ReloadRoutingTables), true);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x0006CC40 File Offset: 0x0006AE40
		private void ReloadRoutingTables(object state)
		{
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "ReloadRoutingTables() callback invoked");
			if (this.context.Dependencies.IsProcessShuttingDown)
			{
				RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "Bailing out of ReloadRoutingTables() because the process is shutting down");
				return;
			}
			Interlocked.Exchange(ref this.deferredNotificationCount, 0);
			Interlocked.Exchange(ref this.lastUpdateTimeTicks, DateTime.UtcNow.Ticks);
			this.TryLoadRoutingTablesAndNotify(this.CreateTopologyConfig(), state != null && (bool)state);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0006CCCC File Offset: 0x0006AECC
		private void HandleConfigurationChange()
		{
			try
			{
				if (Interlocked.Increment(ref this.notificationHandlerCount) == 1 && Interlocked.Increment(ref this.deferredNotificationCount) <= this.context.Settings.MaxDeferredNotifications)
				{
					GuardedTimer guardedTimer = this.reloadTimer;
					if (guardedTimer != null)
					{
						TimeSpan timeSpan = this.CalculateDelayForNextUpdate();
						RoutingDiag.Tracer.TraceDebug<TimeSpan>((long)this.GetHashCode(), "Scheduling config reload to happen after {0}", timeSpan);
						guardedTimer.Change(timeSpan, this.context.Settings.ConfigReloadInterval);
					}
					else
					{
						this.routingTableReloadRequired = true;
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.notificationHandlerCount);
			}
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0006CD6C File Offset: 0x0006AF6C
		private TimeSpan CalculateDelayForNextUpdate()
		{
			TimeSpan deferredReloadInterval = this.context.Settings.DeferredReloadInterval;
			TimeSpan minConfigReloadInterval = this.context.Settings.MinConfigReloadInterval;
			long num = Interlocked.Read(ref this.lastUpdateTimeTicks);
			TimeSpan timeSpan = (num != 0L) ? new TimeSpan(num + minConfigReloadInterval.Ticks - DateTime.UtcNow.Ticks) : TimeSpan.Zero;
			if (!(deferredReloadInterval > timeSpan))
			{
				return timeSpan;
			}
			return deferredReloadInterval;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0006CDE0 File Offset: 0x0006AFE0
		private void LogRoutingTables(object routingInfoObj)
		{
			Tuple<RoutingTables, RoutingTopologyBase> tuple = (Tuple<RoutingTables, RoutingTopologyBase>)routingInfoObj;
			RoutingTableLogger.LogRoutingTables(tuple.Item1, tuple.Item2, this.context);
		}

		// Token: 0x04000C8B RID: 3211
		private const int MaxLoadRetryCount = 5;

		// Token: 0x04000C8C RID: 3212
		private const int LoadRetryIntervalSeconds = 10;

		// Token: 0x04000C8D RID: 3213
		private IMailRouter parentRouter;

		// Token: 0x04000C8E RID: 3214
		private RoutingContextCore context;

		// Token: 0x04000C8F RID: 3215
		private RoutingTables routingTables;

		// Token: 0x04000C90 RID: 3216
		private DatabaseLoader databaseLoader;

		// Token: 0x04000C91 RID: 3217
		private TenantDagQuota tenantDagQuota;

		// Token: 0x04000C92 RID: 3218
		private IList<ADNotificationRequestCookie> notificationCookies;

		// Token: 0x04000C93 RID: 3219
		private GuardedTimer reloadTimer;

		// Token: 0x04000C94 RID: 3220
		private int deferredNotificationCount;

		// Token: 0x04000C95 RID: 3221
		private int notificationHandlerCount;

		// Token: 0x04000C96 RID: 3222
		private long lastUpdateTimeTicks;

		// Token: 0x04000C97 RID: 3223
		private bool routingTableReloadRequired;

		// Token: 0x04000C98 RID: 3224
		private object syncObject;
	}
}
