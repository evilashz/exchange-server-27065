using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.EventLogs;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000027 RID: 39
	internal sealed class DownLevelServerPingManager : DisposeTrackableBase
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00006F50 File Offset: 0x00005150
		public DownLevelServerPingManager(Func<Dictionary<string, List<DownLevelServerStatusEntry>>> downLevelServerMapGetter)
		{
			if (downLevelServerMapGetter == null)
			{
				throw new ArgumentNullException("downLevelServerMapGetter");
			}
			ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[DownLevelServerPingManager::Ctor]: Instantiating.");
			this.downLevelServerMapGetter = downLevelServerMapGetter;
			this.workerTimer = new Timer(delegate(object o)
			{
				this.RefreshServerStatus();
			}, null, TimeSpan.Zero, DownLevelServerPingManager.DownLevelServerPingInterval.Value);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006FF0 File Offset: 0x000051F0
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.workerTimer != null)
			{
				this.workerTimer.Dispose();
				this.workerTimer = null;
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000700F File Offset: 0x0000520F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DownLevelServerPingManager>(this);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007018 File Offset: 0x00005218
		private void RefreshServerStatus()
		{
			try
			{
				if (this.workerSignal.WaitOne(0))
				{
					this.workerSignal.Reset();
					try
					{
						ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[DownLevelServerPingManager::RefreshServerStatus]: Refreshing server map.");
						Diagnostics.Logger.LogEvent(FrontEndHttpProxyEventLogConstants.Tuple_RefreshingDownLevelServerStatus, null, new object[]
						{
							HttpProxyGlobals.ProtocolType.ToString()
						});
						this.InternalRefresh();
						PerfCounters.HttpProxyCountersInstance.DownLevelServersLastPing.RawValue = Stopwatch.GetTimestamp();
					}
					finally
					{
						this.workerSignal.Set();
					}
				}
			}
			catch (Exception exception)
			{
				Diagnostics.ReportException(exception, FrontEndHttpProxyEventLogConstants.Tuple_InternalServerError, null, "Exception from RefreshServerStatus: {0}");
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000070E0 File Offset: 0x000052E0
		private void InternalRefresh()
		{
			Dictionary<string, List<DownLevelServerStatusEntry>> dictionary = this.downLevelServerMapGetter();
			int num = 0;
			int num2 = 0;
			ServiceTopology serviceTopology = null;
			try
			{
				serviceTopology = ServiceTopology.GetCurrentServiceTopology(DownLevelServerPingManager.DownLevelServerPingServiceTopologyTimeout.Value, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerPingManager.cs", "InternalRefresh", 202);
			}
			catch (ReadTopologyTimeoutException)
			{
			}
			foreach (List<DownLevelServerStatusEntry> list in dictionary.Values)
			{
				foreach (DownLevelServerStatusEntry downLevelServerStatusEntry in list)
				{
					if (serviceTopology != null && serviceTopology.IsServerOutOfService(downLevelServerStatusEntry.BackEndServer.Fqdn, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\DownLevelServerManager\\DownLevelServerPingManager.cs", "InternalRefresh", 213))
					{
						ExTraceGlobals.VerboseTracer.TraceWarning<BackEndServer>((long)this.GetHashCode(), "[DownLevelServerPingManager::InternalRefresh]: Skipping server {0} because it's marked as OutOfService.", downLevelServerStatusEntry.BackEndServer);
						downLevelServerStatusEntry.IsHealthy = false;
					}
					else
					{
						Uri uri = this.pingStrategy.Member.BuildUrl(downLevelServerStatusEntry.BackEndServer.Fqdn);
						Diagnostics.Logger.LogEvent(FrontEndHttpProxyEventLogConstants.Tuple_PingingDownLevelServer, downLevelServerStatusEntry.BackEndServer.Fqdn, new object[]
						{
							HttpProxyGlobals.ProtocolType.ToString(),
							downLevelServerStatusEntry.BackEndServer.Fqdn,
							uri
						});
						Exception ex = this.pingStrategy.Member.Ping(uri);
						if (ex != null)
						{
							ex = this.pingStrategy.Member.Ping(uri);
						}
						ExTraceGlobals.VerboseTracer.TraceDebug<Uri, Exception>((long)this.GetHashCode(), "[DownLevelServerPingManager::InternalRefresh]: Tested endpoint {0} with result {1}.", uri, ex);
						if (ex != null)
						{
							downLevelServerStatusEntry.IsHealthy = false;
							Diagnostics.Logger.LogEvent(FrontEndHttpProxyEventLogConstants.Tuple_MarkingDownLevelServerUnhealthy, downLevelServerStatusEntry.BackEndServer.Fqdn, new object[]
							{
								HttpProxyGlobals.ProtocolType.ToString(),
								downLevelServerStatusEntry.BackEndServer.Fqdn,
								uri,
								ex.ToString()
							});
						}
						else
						{
							num2++;
							downLevelServerStatusEntry.IsHealthy = true;
						}
					}
					num++;
				}
			}
			PerfCounters.HttpProxyCountersInstance.DownLevelTotalServers.RawValue = (long)num;
			PerfCounters.HttpProxyCountersInstance.DownLevelHealthyServers.RawValue = (long)num2;
		}

		// Token: 0x04000066 RID: 102
		private static readonly TimeSpanAppSettingsEntry DownLevelServerPingInterval = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("DownLevelServerPingInterval"), TimeSpanUnit.Seconds, TimeSpan.FromSeconds(60.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000067 RID: 103
		private static readonly TimeSpanAppSettingsEntry DownLevelServerPingServiceTopologyTimeout = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("DownLevelServerPingServiceTopologyTimeout"), TimeSpanUnit.Seconds, TimeSpan.FromSeconds(180.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000068 RID: 104
		private static readonly Dictionary<ProtocolType, ProtocolPingStrategyBase> CustomStrategies = new Dictionary<ProtocolType, ProtocolPingStrategyBase>
		{
			{
				ProtocolType.Owa,
				new OwaPingStrategy()
			},
			{
				ProtocolType.RpcHttp,
				new RpcHttpPingStrategy()
			}
		};

		// Token: 0x04000069 RID: 105
		private Func<Dictionary<string, List<DownLevelServerStatusEntry>>> downLevelServerMapGetter;

		// Token: 0x0400006A RID: 106
		private Timer workerTimer;

		// Token: 0x0400006B RID: 107
		private ManualResetEvent workerSignal = new ManualResetEvent(true);

		// Token: 0x0400006C RID: 108
		private LazyMember<ProtocolPingStrategyBase> pingStrategy = new LazyMember<ProtocolPingStrategyBase>(delegate()
		{
			ProtocolPingStrategyBase result = null;
			if (DownLevelServerPingManager.CustomStrategies.TryGetValue(HttpProxyGlobals.ProtocolType, out result))
			{
				return result;
			}
			return new DefaultPingStrategy();
		});
	}
}
