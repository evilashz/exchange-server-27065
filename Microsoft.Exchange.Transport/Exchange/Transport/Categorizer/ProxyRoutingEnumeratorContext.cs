using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000246 RID: 582
	internal class ProxyRoutingEnumeratorContext
	{
		// Token: 0x06001971 RID: 6513 RVA: 0x00066BD0 File Offset: 0x00064DD0
		public ProxyRoutingEnumeratorContext(ProxyRoutingContext proxyContext)
		{
			this.proxyContext = proxyContext;
			this.remainingServerCount = proxyContext.MaxTotalHubCount;
			this.remoteSiteRemainingServerCount = Math.Min(proxyContext.MaxRemoteSiteHubCount, this.remainingServerCount);
			this.localSiteRemainingServerCount = Math.Min(proxyContext.MaxLocalSiteHubCount, this.remainingServerCount);
			this.allServersUnhealthy = true;
			this.serverHealthCheckEnabled = true;
			this.allowedServerGuids = new HashSet<Guid>();
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x00066C3D File Offset: 0x00064E3D
		public bool AllServersUnhealthy
		{
			get
			{
				return this.allServersUnhealthy;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00066C45 File Offset: 0x00064E45
		public int RemainingServerCount
		{
			get
			{
				return this.remainingServerCount;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x00066C4D File Offset: 0x00064E4D
		public int RemoteSiteRemainingServerCount
		{
			get
			{
				return this.remoteSiteRemainingServerCount;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x00066C55 File Offset: 0x00064E55
		public int LocalSiteRemainingServerCount
		{
			get
			{
				return this.localSiteRemainingServerCount;
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00066C5D File Offset: 0x00064E5D
		public bool PreLoadbalanceFilter(RoutingServerInfo serverInfo)
		{
			if (!this.proxyContext.VerifyVersionRestriction(serverInfo))
			{
				RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Server {1} does not satisfy version requirements for being a proxy target", this.proxyContext.Timestamp, serverInfo.Fqdn);
				return false;
			}
			return true;
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x000671AC File Offset: 0x000653AC
		public IEnumerable<RoutingServerInfo> PostLoadbalanceFilter(IEnumerable<RoutingServerInfo> servers, bool? inRemoteSite)
		{
			foreach (RoutingServerInfo serverInfo in servers)
			{
				bool inRemoteSiteValue = (inRemoteSite != null) ? inRemoteSite.Value : (!this.proxyContext.RoutingTables.ServerMap.IsInLocalSite(serverInfo));
				if (this.remainingServerCount == 0 || (inRemoteSite != null && inRemoteSiteValue && this.remoteSiteRemainingServerCount == 0) || (inRemoteSite != null && !inRemoteSiteValue && this.localSiteRemainingServerCount == 0))
				{
					RoutingDiag.Tracer.TraceDebug<DateTime>((long)this.GetHashCode(), "[{0}] Proxy enumerator has reached one of the server count limits while more servers are available", this.proxyContext.Timestamp);
					break;
				}
				if (inRemoteSiteValue && this.remoteSiteRemainingServerCount == 0)
				{
					RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Server {1} is in a remote site and the limit for remote servers has been reached; skipping it", this.proxyContext.Timestamp, serverInfo.Fqdn);
				}
				else if (!inRemoteSiteValue && this.localSiteRemainingServerCount == 0)
				{
					RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Server {1} is in a local site and the limit for local servers has been reached; skipping it", this.proxyContext.Timestamp, serverInfo.Fqdn);
				}
				else if (this.allowedServerGuids.Contains(serverInfo.Id.ObjectGuid))
				{
					RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Server {1} has already been returned by this proxy enumerator; skipping it", this.proxyContext.Timestamp, serverInfo.Fqdn);
				}
				else
				{
					ushort port = 25;
					if (serverInfo.IsFrontendAndHubColocatedServer)
					{
						port = 2525;
					}
					if (this.serverHealthCheckEnabled && this.proxyContext.Core.Dependencies.IsUnhealthyFqdn(serverInfo.Fqdn, port))
					{
						if (this.allServersUnhealthy)
						{
							RoutingUtils.AddItemToLazyList<RoutingServerInfo>(serverInfo, ref this.allUnhealthyServerList);
						}
						RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Server FQDN {1} is Unhealthy and will be excluded from the proxy target list", this.proxyContext.Timestamp, serverInfo.Fqdn);
					}
					else
					{
						this.allServersUnhealthy = false;
						this.allUnhealthyServerList = null;
						this.remainingServerCount--;
						if (inRemoteSiteValue || this.remoteSiteRemainingServerCount > this.remainingServerCount)
						{
							this.remoteSiteRemainingServerCount--;
						}
						if (!inRemoteSiteValue || this.localSiteRemainingServerCount > this.remainingServerCount)
						{
							this.localSiteRemainingServerCount--;
						}
						RoutingDiag.Tracer.TraceDebug<DateTime, string>((long)this.GetHashCode(), "[{0}] Returning server {1} from proxy enumerator", this.proxyContext.Timestamp, serverInfo.Fqdn);
						this.allowedServerGuids.Add(serverInfo.Id.ObjectGuid);
						yield return serverInfo;
					}
				}
			}
			yield break;
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000671D8 File Offset: 0x000653D8
		public IEnumerable<RoutingServerInfo> GetUnhealthyServers()
		{
			if (!this.allServersUnhealthy || this.allUnhealthyServerList == null)
			{
				throw new InvalidOperationException("GetUnhealthyServers() must not be invoked when not all servers are unhealthy");
			}
			IList<RoutingServerInfo> servers = this.allUnhealthyServerList;
			this.allUnhealthyServerList = null;
			this.serverHealthCheckEnabled = false;
			return this.PostLoadbalanceFilter(servers, null);
		}

		// Token: 0x04000C1B RID: 3099
		private ProxyRoutingContext proxyContext;

		// Token: 0x04000C1C RID: 3100
		private int remainingServerCount;

		// Token: 0x04000C1D RID: 3101
		private int remoteSiteRemainingServerCount;

		// Token: 0x04000C1E RID: 3102
		private int localSiteRemainingServerCount;

		// Token: 0x04000C1F RID: 3103
		private HashSet<Guid> allowedServerGuids;

		// Token: 0x04000C20 RID: 3104
		private bool serverHealthCheckEnabled;

		// Token: 0x04000C21 RID: 3105
		private bool allServersUnhealthy;

		// Token: 0x04000C22 RID: 3106
		private List<RoutingServerInfo> allUnhealthyServerList;
	}
}
