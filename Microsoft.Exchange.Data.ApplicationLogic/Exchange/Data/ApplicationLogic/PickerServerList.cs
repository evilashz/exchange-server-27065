using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x020001A9 RID: 425
	internal class PickerServerList : IDisposable
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x00042908 File Offset: 0x00040B08
		internal PickerServerList(ServerPickerManager context)
		{
			this.context = context;
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x0004295A File Offset: 0x00040B5A
		public PickerServer LocalServer
		{
			get
			{
				return this.localServer;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00042962 File Offset: 0x00040B62
		public string LocalServerLegacyDN
		{
			get
			{
				if (this.localServer == null)
				{
					return string.Empty;
				}
				return this.localServer.LegacyDN;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001044 RID: 4164 RVA: 0x0004297D File Offset: 0x00040B7D
		public string LocalServerFQDN
		{
			get
			{
				if (this.localServer == null)
				{
					return string.Empty;
				}
				return this.localServer.FQDN;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00042998 File Offset: 0x00040B98
		public string LocalSystemAttendantDN
		{
			get
			{
				if (this.localServer == null)
				{
					return string.Empty;
				}
				return this.localServer.SystemAttendantDN;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001046 RID: 4166 RVA: 0x000429B3 File Offset: 0x00040BB3
		public string LocalServerName
		{
			get
			{
				if (this.localServer == null)
				{
					return string.Empty;
				}
				return this.localServer.MachineName;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x000429CE File Offset: 0x00040BCE
		public bool SubjectLoggingEnabled
		{
			get
			{
				return this.localServer != null && this.localServer.MessageTrackingLogSubjectLoggingEnabled;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001048 RID: 4168 RVA: 0x000429E5 File Offset: 0x00040BE5
		public IPAddress LocalIP
		{
			get
			{
				if (this.localIP == null)
				{
					return IPAddress.None;
				}
				return this.localIP;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x000429FB File Offset: 0x00040BFB
		public byte[] LocalIPBytes
		{
			get
			{
				if (this.localIPBytes == null)
				{
					return IPAddress.None.GetAddressBytes();
				}
				return this.localIPBytes;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600104A RID: 4170 RVA: 0x00042A16 File Offset: 0x00040C16
		public Guid LocalServerGuid
		{
			get
			{
				if (this.localServer == null)
				{
					return Guid.Empty;
				}
				return this.localServer.ServerGuid;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00042A31 File Offset: 0x00040C31
		internal int RetryServerCount
		{
			get
			{
				return this.retryServerCount;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600104C RID: 4172 RVA: 0x00042A39 File Offset: 0x00040C39
		internal int Count
		{
			get
			{
				return this.serverList.Count;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600104D RID: 4173 RVA: 0x00042A46 File Offset: 0x00040C46
		internal Trace Tracer
		{
			get
			{
				return this.context.Tracer;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x00042A53 File Offset: 0x00040C53
		internal bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		// Token: 0x170003EE RID: 1006
		internal PickerServer this[int index]
		{
			get
			{
				PickerServer result;
				lock (this.stateLock)
				{
					if (index >= 0 && index < this.serverList.Count)
					{
						result = this.serverList[index];
					}
					else
					{
						result = null;
					}
				}
				return result;
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00042ABC File Offset: 0x00040CBC
		public PickerServer PickNextUsingRoundRobinPreferringLocal()
		{
			this.context.Tracer.TraceDebug(0L, "Picking next server using round robin preferring local");
			lock (this.stateLock)
			{
				if (this.localServer != null && (this.localServer.ServerRole & this.context.ServerRole) != ServerRole.None && this.IsEligibleForUse(this.localServer))
				{
					return this.localServer;
				}
			}
			return this.PickNextUsingRoundRobin();
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00042B50 File Offset: 0x00040D50
		public PickerServer PickNextUsingRoundRobinPreferringNonLocal()
		{
			return this.PickNextUsingRoundRobin(true);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00042B59 File Offset: 0x00040D59
		public PickerServer PickNextUsingRoundRobin()
		{
			return this.PickNextUsingRoundRobin(false);
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00042B64 File Offset: 0x00040D64
		public void PromoteServer(PickerServer server)
		{
			lock (this.stateLock)
			{
				int num = this.serverList.IndexOf(server);
				if (num != -1)
				{
					this.currentServerIndex = num;
				}
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x00042BB8 File Offset: 0x00040DB8
		private PickerServer PickNextUsingRoundRobin(bool preferNonLocal)
		{
			this.context.Tracer.TraceDebug<string>(0L, "Picking next server using round robin {0}", preferNonLocal ? "preferring non local" : string.Empty);
			if (this.serverList.Count == 0)
			{
				this.context.Tracer.TraceDebug<string>(0L, "No {0} servers found", this.context.ServerRole.ToString());
				return null;
			}
			lock (this.stateLock)
			{
				bool flag2 = false;
				for (int i = 0; i < this.serverList.Count; i++)
				{
					if (this.currentServerIndex >= this.serverList.Count)
					{
						this.currentServerIndex = 0;
					}
					PickerServer pickerServer = this.serverList[this.currentServerIndex++];
					if (preferNonLocal && pickerServer == this.localServer)
					{
						this.context.Tracer.TraceDebug(0L, "Skipping local server in hopes of finding another.");
						flag2 = true;
					}
					else if (pickerServer.IsEligibleForUse())
					{
						this.context.Tracer.TraceDebug<string, string>(0L, "Found {0} as the next {1} server", pickerServer.LegacyDN, this.context.ServerRole.ToString());
						return pickerServer;
					}
				}
				if (flag2 && this.localServer.IsEligibleForUse())
				{
					this.context.Tracer.TraceDebug(0L, "Returning local server.");
					return this.localServer;
				}
			}
			this.context.Tracer.TraceDebug<string>(0L, "No active {0} server available based on round robin", this.context.ServerRole.ToString());
			return null;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00042D84 File Offset: 0x00040F84
		public PickerServer PickServerByFqdn(string fqdn)
		{
			PickerServer result;
			if (this.fqdnServers.TryGetValue(fqdn, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00042DA4 File Offset: 0x00040FA4
		public void UpdateServerHealth(PickerServer server, bool? isHealthy)
		{
			lock (this.stateLock)
			{
				int num = this.serverList.IndexOf(server);
				if (num != -1)
				{
					server = this.serverList[num];
					server.InternalUpdateServerHealth(isHealthy);
				}
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00042E04 File Offset: 0x00041004
		public void ForceAllToActive()
		{
			this.TryForceAllToActive();
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x00042E10 File Offset: 0x00041010
		public bool TryForceAllToActive()
		{
			bool result;
			lock (this.stateLock)
			{
				if (DateTime.UtcNow - this.lastForceRetryToActiveTime > PickerServerList.ForceRetryToActiveInterval)
				{
					this.context.Tracer.TraceDebug<string>(0L, "Force all {0} servers to become active", this.context.ServerRole.ToString());
					foreach (PickerServer pickerServer in this.serverList)
					{
						pickerServer.InternalUpdateServerHealth(null);
					}
					this.lastForceRetryToActiveTime = DateTime.UtcNow;
					result = true;
				}
				else
				{
					this.context.Tracer.TraceDebug<DateTime>(0L, "Last force retry to active happened at {0}", this.lastForceRetryToActiveTime);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00042F10 File Offset: 0x00041110
		public void AddRef()
		{
			if (this.refCount <= 0)
			{
				throw new InvalidOperationException("Invalid reference management detected.");
			}
			Interlocked.Increment(ref this.refCount);
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00042F32 File Offset: 0x00041132
		public void Release()
		{
			if (this.refCount <= 0)
			{
				throw new InvalidOperationException("Invalid reference management detected.");
			}
			if (Interlocked.Decrement(ref this.refCount) == 0)
			{
				this.Dispose();
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00042F5C File Offset: 0x0004115C
		public void Dispose()
		{
			if (this.localServer != null)
			{
				this.localServer.Dispose();
				this.localServer = null;
			}
			foreach (PickerServer pickerServer in this.serverList)
			{
				pickerServer.Dispose();
			}
			this.serverList.Clear();
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00042FD4 File Offset: 0x000411D4
		public XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = new XElement("Servers");
			new XElement("PickerServerList", new object[]
			{
				new XElement("refCount", this.refCount),
				new XElement("serverListCount", this.serverList.Count),
				new XElement("lastForceRetryToActiveTime", this.lastForceRetryToActiveTime),
				xelement
			});
			lock (this.stateLock)
			{
				foreach (PickerServer pickerServer in this.serverList)
				{
					xelement.Add(pickerServer.GetDiagnosticInfo(argument));
				}
			}
			return xelement;
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x000430E4 File Offset: 0x000412E4
		internal bool IsEligibleForUse(PickerServer server)
		{
			int num = this.serverList.IndexOf(server);
			return num != -1 && this.serverList[num].IsEligibleForUse();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00043118 File Offset: 0x00041318
		internal bool IsChangeIgnorable(Server server)
		{
			PickerServer pickerServer = this.FindMatchingServer(server);
			if (pickerServer == null)
			{
				if ((server.CurrentServerRole & this.context.ServerRole) == ServerRole.None)
				{
					return true;
				}
			}
			else if ((server.CurrentServerRole & this.context.ServerRole) != ServerRole.None && ADObjectId.Equals(server.ServerSite, this.localSite) && server.VersionNumber >= Server.CurrentProductMinimumVersion && pickerServer.ArePropertiesEqual(server, this.context.ServerRole))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00043191 File Offset: 0x00041391
		internal void DecrementServersInRetryCount()
		{
			Interlocked.Decrement(ref this.retryServerCount);
			this.context.UpdateServersInRetryPerfmon(this);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000431AB File Offset: 0x000413AB
		internal void IncrementServersInRetryCount()
		{
			Interlocked.Increment(ref this.retryServerCount);
			this.context.UpdateServersInRetryPerfmon(this);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000431C8 File Offset: 0x000413C8
		internal void LoadFromAD(PickerServerList oldServers)
		{
			this.context.Tracer.TracePfd<int, string>(0L, "PFD EMS {0} Loading {1} servers From AD...", 30619, this.context.ServerRole.ToString());
			Server server = this.context.ConfigurationSession.FindLocalServer();
			if (server != null)
			{
				this.CheckForOverride(server);
				if (server.ServerSite == null)
				{
					this.context.Tracer.TraceError(0L, "Local server doesn't belong to a site");
					ExEventLog.EventTuple tuple = this.context.HasValidConfiguration ? ApplicationLogicEventLogConstants.Tuple_LocalServerNotInSiteWarning : ApplicationLogicEventLogConstants.Tuple_LocalServerNotInSite;
					this.context.LogEvent(tuple, null, new object[0]);
					return;
				}
				this.context.ServerPickerClient.LocalServerDiscovered(server);
				this.localSite = server.ServerSite;
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new BitMaskAndFilter(ServerSchema.CurrentServerRole, (ulong)((long)this.context.ServerRole)),
					new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ServerSite, server.ServerSite),
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ServerSchema.VersionNumber, Server.CurrentProductMinimumVersion),
					new ComparisonFilter(ComparisonOperator.LessThan, ServerSchema.VersionNumber, Server.NextProductMinimumVersion)
				});
				HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				HashSet<string> hashSet2 = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
				ADPagedReader<Server> adpagedReader = this.context.ConfigurationSession.FindPaged<Server>(null, QueryScope.SubTree, filter, null, 0);
				foreach (Server server2 in adpagedReader)
				{
					ServiceState serviceState = ServerComponentStates.ReadEffectiveComponentState(server2.Fqdn, server2.ComponentStates, ServerComponentStateSources.AD, ServerComponentStates.GetComponentId(this.context.Component), ServiceState.Active);
					if (this.context.Component != ServerComponentEnum.None && serviceState != ServiceState.Active)
					{
						this.context.Tracer.TraceDebug<string, string, ServiceState>(0L, "Component {0} is not active on server {1}. Current component state is {2}.", this.context.Component.ToString(), server2.DistinguishedName, serviceState);
					}
					else
					{
						bool flag = true;
						if (this.overrideList != null && this.overrideList.Count > 0)
						{
							if (this.overrideList.Contains(server2.DistinguishedName))
							{
								this.context.Tracer.TraceDebug<string, string>(0L, "Adding {0} Server from override list: {1} to active list", this.context.ServerRole.ToString(), server2.DistinguishedName);
							}
							else
							{
								flag = false;
							}
						}
						else
						{
							this.context.Tracer.TraceDebug<string, string, int>(0L, "Adding {0} Server: {1} Version: {2} to active list", this.context.ServerRole.ToString(), server2.DistinguishedName, server2.VersionNumber);
						}
						if (flag)
						{
							if (this.fqdnServers.ContainsKey(server2.Fqdn) || hashSet.Contains(server2.Name) || hashSet2.Contains(server2.ExchangeLegacyDN))
							{
								this.context.Tracer.TraceError<string, string, string>(0L, "Found more than one server with same FQDN {0} or LegacyDN {1} or MachineName {2}.", server2.Fqdn, server2.ExchangeLegacyDN, server2.Name);
								this.context.LogEvent(ApplicationLogicEventLogConstants.Tuple_MisconfiguredServer, server2.Fqdn, new object[]
								{
									server2.ExchangeLegacyDN,
									server2.Name
								});
							}
							else
							{
								PickerServer pickerServer = this.context.ServerPickerClient.CreatePickerServer(server2, this);
								if (oldServers != null)
								{
									PickerServer pickerServer2 = oldServers.PickServerByFqdn(pickerServer.FQDN);
									if (pickerServer2 != null)
									{
										pickerServer2.CopyStatusTo(pickerServer);
									}
								}
								this.serverList.Add(pickerServer);
								this.fqdnServers.Add(pickerServer.FQDN, pickerServer);
								hashSet.Add(pickerServer.MachineName);
								hashSet2.Add(pickerServer.LegacyDN);
							}
						}
					}
				}
				this.context.ServerPickerClient.ServerListUpdated(this.serverList);
				if ((server.CurrentServerRole & this.context.ServerRole) == ServerRole.None)
				{
					this.localServer = this.context.ServerPickerClient.CreatePickerServer(server, this);
				}
				else
				{
					PickerServer pickerServer3 = this.FindMatchingServer(server);
					if (pickerServer3 != null)
					{
						this.localServer = pickerServer3;
					}
					else
					{
						this.context.Tracer.TraceDebug<string>(0L, "Local server ({0}) meets criteria, but wasn't found in AD using list query; won't be preferred", server.Fqdn);
						this.localServer = this.context.ServerPickerClient.CreatePickerServer(server, this);
					}
				}
				try
				{
					this.localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
				}
				catch (SocketException ex)
				{
					this.context.Tracer.TraceError<string>(0L, "Can't get local IP address due to: {0}", ex.ToString());
					ExEventLog.EventTuple tuple2 = this.context.HasValidConfiguration ? ApplicationLogicEventLogConstants.Tuple_CantGetLocalIPWarning : ApplicationLogicEventLogConstants.Tuple_CantGetLocalIP;
					this.context.LogEvent(tuple2, ex.GetType().FullName, new object[]
					{
						ex
					});
					return;
				}
				this.localIPBytes = this.localIP.GetAddressBytes();
				this.context.Tracer.TracePfd<int, string, int>(0L, "PFD EMS {0} Finished finding {1} servers: {2} found.", 19355, this.context.ServerRole.ToString(), this.serverList.Count);
				this.isValid = true;
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00043718 File Offset: 0x00041918
		private void CheckForOverride(Server server)
		{
			if (this.context.ServerPickerClient.OverrideListPropertyDefinition == null)
			{
				return;
			}
			MultiValuedProperty<ADObjectId> multiValuedProperty = server[this.context.ServerPickerClient.OverrideListPropertyDefinition] as MultiValuedProperty<ADObjectId>;
			if (multiValuedProperty != null)
			{
				this.overrideList = new HashSet<string>();
				foreach (ADObjectId adobjectId in multiValuedProperty)
				{
					this.overrideList.Add(adobjectId.DistinguishedName);
				}
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x000437B0 File Offset: 0x000419B0
		private PickerServer FindMatchingServer(Server server)
		{
			foreach (PickerServer pickerServer in this.serverList)
			{
				if (string.Equals(pickerServer.LegacyDN, server.ExchangeLegacyDN, StringComparison.OrdinalIgnoreCase))
				{
					return pickerServer;
				}
			}
			return null;
		}

		// Token: 0x040008AF RID: 2223
		private static readonly TimeSpan ForceRetryToActiveInterval = TimeSpan.FromSeconds(30.0);

		// Token: 0x040008B0 RID: 2224
		private object stateLock = new object();

		// Token: 0x040008B1 RID: 2225
		private List<PickerServer> serverList = new List<PickerServer>();

		// Token: 0x040008B2 RID: 2226
		private int currentServerIndex;

		// Token: 0x040008B3 RID: 2227
		private DateTime lastForceRetryToActiveTime = DateTime.UtcNow;

		// Token: 0x040008B4 RID: 2228
		private int retryServerCount;

		// Token: 0x040008B5 RID: 2229
		private ServerPickerManager context;

		// Token: 0x040008B6 RID: 2230
		private ADObjectId localSite;

		// Token: 0x040008B7 RID: 2231
		private HashSet<string> overrideList;

		// Token: 0x040008B8 RID: 2232
		private PickerServer localServer;

		// Token: 0x040008B9 RID: 2233
		private IPAddress localIP;

		// Token: 0x040008BA RID: 2234
		private byte[] localIPBytes;

		// Token: 0x040008BB RID: 2235
		private bool isValid;

		// Token: 0x040008BC RID: 2236
		private int refCount = 1;

		// Token: 0x040008BD RID: 2237
		private Dictionary<string, PickerServer> fqdnServers = new Dictionary<string, PickerServer>(StringComparer.OrdinalIgnoreCase);
	}
}
