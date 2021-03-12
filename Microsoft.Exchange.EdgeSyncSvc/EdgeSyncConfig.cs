using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Common.Internal;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x0200000E RID: 14
	internal class EdgeSyncConfig
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00005560 File Offset: 0x00003760
		public EdgeSyncConfig(ITopologyConfigurationSession session)
		{
			this.session = session;
			this.configReloadTimer = new Timer(new TimerCallback(this.ReLoadEdgeSyncConfig), null, -1, (int)this.reloadConfigFromADInterval.TotalMilliseconds);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600006A RID: 106 RVA: 0x000055E0 File Offset: 0x000037E0
		// (remove) Token: 0x0600006B RID: 107 RVA: 0x00005618 File Offset: 0x00003818
		public event EventHandler ConfigChanged;

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006C RID: 108 RVA: 0x0000564D File Offset: 0x0000384D
		public EdgeSyncServiceConfig ServiceConfig
		{
			get
			{
				return this.serviceConfig;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00005655 File Offset: 0x00003855
		public Dictionary<string, EdgeSyncConnector> Connectors
		{
			get
			{
				return this.connectors;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000056FC File Offset: 0x000038FC
		public bool Initialize(out Exception e)
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADSite localSite = this.session.GetLocalSite();
				if (localSite == null)
				{
					throw new TransientException(Strings.CannotGetLocalSite);
				}
				this.localSiteId = localSite.Id;
				this.cookies.Add(ADNotificationAdapter.RegisterChangeNotification<EdgeSyncServiceConfig>(this.localSiteId, new ADNotificationCallback(this.ConfigChangeNotificationDispatch)));
				this.cookies.Add(ADNotificationAdapter.RegisterChangeNotification<EdgeSyncConnector>(this.localSiteId, new ADNotificationCallback(this.ConfigChangeNotificationDispatch)));
				this.cookies.Add(ADNotificationAdapter.RegisterChangeNotification<ADSite>(this.localSiteId, new ADNotificationCallback(this.ConfigChangeNotificationDispatch)));
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				e = adoperationResult.Exception;
				ExTraceGlobals.TopologyTracer.TraceError<Exception>((long)this.GetHashCode(), "EdgeSyncConfig.Initialize.TryRunADOperation failed with {0}", e);
				return false;
			}
			return this.LoadEdgeSyncConfig(out e);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005750 File Offset: 0x00003950
		public void Shutdown()
		{
			ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "EdgeSyncConfig.Shutdown called");
			lock (this)
			{
				if (this.cookies != null)
				{
					foreach (ADNotificationRequestCookie request in this.cookies)
					{
						ADNotificationAdapter.UnregisterChangeNotification(request);
					}
				}
			}
			if (this.configReloadTimer != null)
			{
				this.configReloadTimer.Dispose();
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00005998 File Offset: 0x00003B98
		private bool LoadEdgeSyncConfig(out Exception e)
		{
			bool result;
			try
			{
				e = null;
				if (Interlocked.Increment(ref this.configLoadingCount) == 1)
				{
					EdgeSyncServiceConfig edgeSyncServiceConfig = null;
					ADObjectId edgeSyncServiceConfigId = null;
					ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
					{
						ADSite localSite = this.session.GetLocalSite();
						if (localSite == null)
						{
							throw new TransientException(Strings.CannotGetLocalSite);
						}
						ADObjectId id = localSite.Id;
						if (!this.localSiteId.Equals(id))
						{
							lock (this)
							{
								foreach (ADNotificationRequestCookie request in this.cookies)
								{
									ADNotificationAdapter.UnregisterChangeNotification(request);
								}
								this.cookies.Clear();
								this.cookies.Add(ADNotificationAdapter.RegisterChangeNotification<EdgeSyncServiceConfig>(id, new ADNotificationCallback(this.ConfigChangeNotificationDispatch)));
								this.cookies.Add(ADNotificationAdapter.RegisterChangeNotification<EdgeSyncConnector>(id, new ADNotificationCallback(this.ConfigChangeNotificationDispatch)));
							}
							this.localSiteId = id;
						}
						edgeSyncServiceConfigId = this.localSiteId.GetChildId("EdgeSyncService");
						edgeSyncServiceConfig = this.session.Read<EdgeSyncServiceConfig>(edgeSyncServiceConfigId);
					}, 3);
					if (!adoperationResult.Succeeded)
					{
						e = adoperationResult.Exception;
						EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EdgeTopologyException, null, new object[]
						{
							e
						});
						result = false;
					}
					else if (edgeSyncServiceConfig == null)
					{
						e = new EdgeSyncServiceConfigNotFoundException(this.localSiteId.Name, edgeSyncServiceConfigId.DistinguishedName);
						result = false;
					}
					else
					{
						Dictionary<string, EdgeSyncConnector> newConnectors = new Dictionary<string, EdgeSyncConnector>();
						if (!ADNotificationAdapter.TryReadConfigurationPaged<EdgeSyncConnector>(() => this.session.FindPaged<EdgeSyncConnector>(edgeSyncServiceConfig.Id, QueryScope.SubTree, null, null, 0), delegate(EdgeSyncConnector connector)
						{
							newConnectors.Add(connector.Name, connector);
						}, out adoperationResult))
						{
							e = adoperationResult.Exception;
							EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EdgeTopologyException, null, new object[]
							{
								e
							});
							result = false;
						}
						else
						{
							Interlocked.Exchange<EdgeSyncServiceConfig>(ref this.serviceConfig, edgeSyncServiceConfig);
							Interlocked.Exchange<Dictionary<string, EdgeSyncConnector>>(ref this.connectors, newConnectors);
							result = true;
						}
					}
				}
				else
				{
					result = true;
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.configLoadingCount);
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005AFC File Offset: 0x00003CFC
		private void ReLoadEdgeSyncConfig(object state)
		{
			Exception ex;
			this.LoadEdgeSyncConfig(out ex);
			this.OnConfigChanged();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005B18 File Offset: 0x00003D18
		private void OnConfigChanged()
		{
			if (this.ConfigChanged != null)
			{
				this.ConfigChanged(this, null);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005B30 File Offset: 0x00003D30
		private void ConfigChangeNotificationDispatch(ADNotificationEventArgs args)
		{
			try
			{
				if (Interlocked.Increment(ref this.notificationHandlerCount) == 1)
				{
					try
					{
						this.configReloadTimer.Change(this.notificationDelay, this.reloadConfigFromADInterval);
					}
					catch (ObjectDisposedException)
					{
					}
				}
			}
			finally
			{
				Interlocked.Decrement(ref this.notificationHandlerCount);
			}
		}

		// Token: 0x04000047 RID: 71
		private readonly TimeSpan reloadConfigFromADInterval = TimeSpan.FromHours(1.0);

		// Token: 0x04000048 RID: 72
		private readonly TimeSpan notificationDelay = TimeSpan.FromSeconds(10.0);

		// Token: 0x04000049 RID: 73
		private Timer configReloadTimer;

		// Token: 0x0400004A RID: 74
		private int configLoadingCount;

		// Token: 0x0400004B RID: 75
		private int notificationHandlerCount;

		// Token: 0x0400004C RID: 76
		private ITopologyConfigurationSession session;

		// Token: 0x0400004D RID: 77
		private List<ADNotificationRequestCookie> cookies = new List<ADNotificationRequestCookie>();

		// Token: 0x0400004E RID: 78
		private ADObjectId localSiteId;

		// Token: 0x0400004F RID: 79
		private EdgeSyncServiceConfig serviceConfig;

		// Token: 0x04000050 RID: 80
		private Dictionary<string, EdgeSyncConnector> connectors = new Dictionary<string, EdgeSyncConnector>();
	}
}
