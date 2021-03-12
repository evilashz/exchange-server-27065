using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.EdgeSync;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Common.Internal;
using Microsoft.Exchange.EdgeSync.Logging;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x0200000D RID: 13
	internal class Topology
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00004E2C File Offset: 0x0000302C
		public Topology(ITopologyConfigurationSession session)
		{
			this.session = session;
			this.configReloadTimer = new Timer(new TimerCallback(this.ReLoadExchangeTopology), null, -1, (int)Topology.updateInterval.TotalMilliseconds);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600005A RID: 90 RVA: 0x00004E94 File Offset: 0x00003094
		// (remove) Token: 0x0600005B RID: 91 RVA: 0x00004ECC File Offset: 0x000030CC
		public event EventHandler TopologyChanged;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004F01 File Offset: 0x00003101
		public string AdOrganizationAbsolutePath
		{
			get
			{
				return DistinguishedName.RemoveLeafRelativeDistinguishedNames(this.localServer.DistinguishedName, 4);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00004F14 File Offset: 0x00003114
		public ActiveDirectorySecurity ServerSecurity
		{
			get
			{
				return this.serverSecurity;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004F1C File Offset: 0x0000311C
		public Server LocalServer
		{
			get
			{
				return this.localServer;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004F24 File Offset: 0x00003124
		public Dictionary<string, Server> SiteEdgeServers
		{
			get
			{
				return this.siteEdgeServers;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004F2C File Offset: 0x0000312C
		public Dictionary<string, Server> SiteBridgeheadServers
		{
			get
			{
				return this.siteBridgeheadServers;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004F34 File Offset: 0x00003134
		public HashSet<string> SiteBridgeheadDistinguishedNames
		{
			get
			{
				return this.siteBridgeheadDistinguishedNames;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004FC8 File Offset: 0x000031C8
		public bool Initialize(EdgeSyncLogSession logSession, out Exception e)
		{
			Server server = null;
			ActiveDirectorySecurity newServerSecurity = null;
			ADObjectId rootId = null;
			this.logSession = logSession;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				server = this.session.FindLocalServer();
				newServerSecurity = this.GetServerObjectSecurity(server);
				rootId = this.session.GetOrgContainerId().GetChildId("Administrative Groups");
				this.cookie = ADNotificationAdapter.RegisterChangeNotification<Server>(rootId, new ADNotificationCallback(this.TopoloyChangeNotificationDispatch));
			}, 3);
			if (!adoperationResult.Succeeded)
			{
				e = adoperationResult.Exception;
				ExTraceGlobals.TopologyTracer.TraceError<Exception>((long)this.GetHashCode(), "Topology.Initialize.TryRunADOperation failed with {0}", e);
				return false;
			}
			if (newServerSecurity == null)
			{
				ExTraceGlobals.TopologyTracer.TraceError((long)this.GetHashCode(), "serverSecurity is null");
				e = new ADOperationException(Strings.UnableToLoadSD);
				return false;
			}
			this.localServer = server;
			this.serverSecurity = newServerSecurity;
			bool flag;
			return this.LoadExchangeTopology(out flag, out e);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005088 File Offset: 0x00003288
		public void Shutdown()
		{
			ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "Topology.Shutdown called");
			if (this.cookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(this.cookie);
			}
			if (this.configReloadTimer != null)
			{
				this.configReloadTimer.Dispose();
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000522C File Offset: 0x0000342C
		private bool LoadExchangeTopology(out bool topologyChanged, out Exception e)
		{
			bool result;
			try
			{
				e = null;
				topologyChanged = false;
				if (Interlocked.Increment(ref this.topologyLoadingCount) == 1)
				{
					Dictionary<string, Server> bridgeheadServers = new Dictionary<string, Server>();
					Dictionary<string, Server> edgeServers = new Dictionary<string, Server>();
					HashSet<string> bridgeheadDistinguishedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
					if (this.localServer.ServerSite == null)
					{
						e = new LocalizedException(Strings.NoSiteAttributeFound(this.localServer.Name));
						result = false;
					}
					else
					{
						QueryFilter filter = Util.BuildServerFilterForSite(this.localServer.ServerSite);
						bool match = true;
						ADOperationResult adoperationResult;
						if (!ADNotificationAdapter.TryReadConfigurationPaged<Server>(() => this.session.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0), delegate(Server server)
						{
							if (server.IsEdgeServer)
							{
								if (!this.siteEdgeServers.ContainsKey(server.Fqdn))
								{
									match = false;
								}
								if (!edgeServers.ContainsKey(server.Fqdn))
								{
									ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Add {0} to siteEdgeServers", server.Fqdn);
									edgeServers.Add(server.Fqdn, server);
								}
							}
							if (server.IsHubTransportServer)
							{
								if (!this.siteBridgeheadServers.ContainsKey(server.Fqdn))
								{
									match = false;
								}
								if (server.Id.Equals(this.localServer.Id))
								{
									if (server.EdgeSyncCredentials.Changed)
									{
										match = false;
									}
									Interlocked.Exchange<Server>(ref this.localServer, server);
								}
								if (!bridgeheadServers.ContainsKey(server.Fqdn))
								{
									ExTraceGlobals.TopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Add {0} to siteBridgeheadServers", server.Fqdn);
									bridgeheadServers.Add(server.Fqdn, server);
									bridgeheadDistinguishedNames.Add(server.DistinguishedName);
								}
							}
						}, out adoperationResult))
						{
							e = adoperationResult.Exception;
							this.logSession.LogException(EdgeSyncLoggingLevel.Low, EdgeSyncEvent.Topology, e, "Unable to load topology.");
							EdgeSyncEvents.Log.LogEvent(EdgeSyncEventLogConstants.Tuple_EdgeTopologyException, null, new object[]
							{
								e
							});
							result = false;
						}
						else if (match && this.siteEdgeServers.Count == edgeServers.Count && this.siteBridgeheadServers.Count == bridgeheadServers.Count)
						{
							ExTraceGlobals.TopologyTracer.TraceDebug((long)this.GetHashCode(), "Topology not changed");
							result = true;
						}
						else
						{
							EdgeSyncTopologyPerfCounters.Servers.RawValue = (long)(edgeServers.Count + bridgeheadServers.Count);
							EdgeSyncTopologyPerfCounters.Edges.RawValue = (long)edgeServers.Count;
							EdgeSyncTopologyPerfCounters.Bridgheads.RawValue = (long)bridgeheadServers.Count;
							topologyChanged = true;
							EdgeSyncTopologyPerfCounters.Updates.Increment();
							Interlocked.Exchange<Dictionary<string, Server>>(ref this.siteEdgeServers, edgeServers);
							Interlocked.Exchange<Dictionary<string, Server>>(ref this.siteBridgeheadServers, bridgeheadServers);
							Interlocked.Exchange<HashSet<string>>(ref this.siteBridgeheadDistinguishedNames, bridgeheadDistinguishedNames);
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
				Interlocked.Decrement(ref this.topologyLoadingCount);
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000544C File Offset: 0x0000364C
		private ActiveDirectorySecurity GetServerObjectSecurity(Server server)
		{
			RawSecurityDescriptor rawSecurityDescriptor = server.ReadSecurityDescriptor();
			if (rawSecurityDescriptor == null)
			{
				return null;
			}
			ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
			byte[] array = new byte[rawSecurityDescriptor.BinaryLength];
			rawSecurityDescriptor.GetBinaryForm(array, 0);
			activeDirectorySecurity.SetSecurityDescriptorBinaryForm(array);
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			ActiveDirectoryAccessRule rule = new ActiveDirectoryAccessRule(identity, ActiveDirectoryRights.GenericRead, AccessControlType.Allow);
			activeDirectorySecurity.AddAccessRule(rule);
			return activeDirectorySecurity;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000054A8 File Offset: 0x000036A8
		private void ReLoadExchangeTopology(object state)
		{
			bool flag;
			Exception ex;
			if (this.LoadExchangeTopology(out flag, out ex) && flag)
			{
				this.TopologyChanged(this, null);
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000054D4 File Offset: 0x000036D4
		private void TopoloyChangeNotificationDispatch(ADNotificationEventArgs args)
		{
			try
			{
				if (Interlocked.Increment(ref this.notificationHandlerCount) == 1)
				{
					try
					{
						this.configReloadTimer.Change(Topology.notificationDelay, Topology.updateInterval);
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

		// Token: 0x04000039 RID: 57
		private static readonly TimeSpan notificationDelay = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400003A RID: 58
		private static readonly TimeSpan updateInterval = TimeSpan.FromHours(1.0);

		// Token: 0x0400003B RID: 59
		private Timer configReloadTimer;

		// Token: 0x0400003C RID: 60
		private int topologyLoadingCount;

		// Token: 0x0400003D RID: 61
		private int notificationHandlerCount;

		// Token: 0x0400003E RID: 62
		private ITopologyConfigurationSession session;

		// Token: 0x0400003F RID: 63
		private ADNotificationRequestCookie cookie;

		// Token: 0x04000040 RID: 64
		private Server localServer;

		// Token: 0x04000041 RID: 65
		private ActiveDirectorySecurity serverSecurity;

		// Token: 0x04000042 RID: 66
		private Dictionary<string, Server> siteEdgeServers = new Dictionary<string, Server>();

		// Token: 0x04000043 RID: 67
		private Dictionary<string, Server> siteBridgeheadServers = new Dictionary<string, Server>();

		// Token: 0x04000044 RID: 68
		private HashSet<string> siteBridgeheadDistinguishedNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000045 RID: 69
		private EdgeSyncLogSession logSession;
	}
}
