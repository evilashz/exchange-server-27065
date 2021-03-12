using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ExchangeTopology;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.AsyncContexts;
using Microsoft.Exchange.Directory.TopologyService.Data;
using Microsoft.Exchange.Directory.TopologyService.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Directory.TopologyService
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	internal class TopologyService : ITopologyService, ITopologyService
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00008C23 File Offset: 0x00006E23
		[PrincipalPermission(SecurityAction.Demand, Role = "ReadOnlyAdmin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Role = "UserService")]
		public ServiceVersion GetServiceVersion()
		{
			return ServiceVersion.Current;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00008C2C File Offset: 0x00006E2C
		[PrincipalPermission(SecurityAction.Demand, Role = "ReadOnlyAdmin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Role = "UserService")]
		public byte[][] GetExchangeTopology(DateTime currentTopologyTimestamp, ExchangeTopologyScope topologyScope, bool forceRefresh)
		{
			ExchangeTopologyCache exchangeTopologyCache = ExchangeTopologyCache.Get(topologyScope);
			if (exchangeTopologyCache == null)
			{
				return null;
			}
			if (forceRefresh)
			{
				exchangeTopologyCache.Refresh();
			}
			ExchangeTopologyCache.SerializedTopology topology = exchangeTopologyCache.Topology;
			if (topology == null)
			{
				return null;
			}
			if (topology.Discovery.DiscoveryStarted == currentTopologyTimestamp)
			{
				return null;
			}
			return topology.Get(topologyScope);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00008C94 File Offset: 0x00006E94
		[PrincipalPermission(SecurityAction.Demand, Role = "ReadOnlyAdmin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Role = "UserService")]
		public List<TopologyVersion> GetAllTopologyVersions()
		{
			List<TopologyVersion> versions = null;
			this.ExecuteServiceCall(delegate
			{
				versions = this.InternalGetAllTopologyVersions();
			});
			return versions;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00008CF0 File Offset: 0x00006EF0
		[PrincipalPermission(SecurityAction.Demand, Role = "ReadOnlyAdmin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Role = "UserService")]
		public List<TopologyVersion> GetTopologyVersions(List<string> partitionFqdns)
		{
			List<TopologyVersion> versions = null;
			this.ExecuteServiceCall(delegate
			{
				versions = this.InternalGetTopologyVersions(partitionFqdns);
			});
			return versions;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008D80 File Offset: 0x00006F80
		[FaultContract(typeof(ArgumentException))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(TopologyServiceFault))]
		[AsyncPerfForestInspector]
		[FaultContract(typeof(InvalidOperationException))]
		[PrincipalPermission(SecurityAction.Demand, Role = "ReadOnlyAdmin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Role = "UserService")]
		public IAsyncResult BeginGetServersForRole(string partitionFqdn, List<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested, AsyncCallback callback, object asyncState)
		{
			IAsyncResult result = null;
			this.ExecuteServiceCall(delegate
			{
				result = this.InternalBeginGetServersForRole(partitionFqdn, currentlyUsedServers, role, serversRequested, forestWideAffinityRequested, callback, asyncState);
			});
			return result;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00008E10 File Offset: 0x00007010
		public List<ServerInfo> EndGetServersForRole(IAsyncResult result)
		{
			List<ServerInfo> servers = null;
			this.ExecuteServiceCall(delegate
			{
				servers = this.InternalEndGetServersForRole(result);
			});
			return servers;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008E80 File Offset: 0x00007080
		[FaultContract(typeof(TopologyServiceFault))]
		[FaultContract(typeof(ArgumentNullException))]
		[FaultContract(typeof(InvalidOperationException))]
		[FaultContract(typeof(ArgumentException))]
		[PrincipalPermission(SecurityAction.Demand, Role = "ReadOnlyAdmin")]
		[PrincipalPermission(SecurityAction.Demand, Role = "LocalAdministrators")]
		[PrincipalPermission(SecurityAction.Demand, Role = "UserService")]
		public IAsyncResult BeginGetServerFromDomainDN(string domainDN, AsyncCallback callback, object asyncState)
		{
			IAsyncResult result = null;
			this.ExecuteServiceCall(delegate
			{
				result = this.InternalBeginGetServerFromDomainDN(domainDN, callback, asyncState);
			});
			return result;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008EF0 File Offset: 0x000070F0
		public ServerInfo EndGetServerFromDomainDN(IAsyncResult result)
		{
			ServerInfo info = null;
			this.ExecuteServiceCall(delegate
			{
				info = TopologyDiscoveryManager.Instance.EndGetServerFromDomain(result);
			});
			return info;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00008F5C File Offset: 0x0000715C
		[FaultContract(typeof(TopologyServiceFault))]
		[PrincipalPermission(SecurityAction.Demand, Role = "ReadOnlyAdmin+LocalCall")]
		[PrincipalPermission(SecurityAction.Demand, Role = "LocalAdministrators+LocalCall")]
		[PrincipalPermission(SecurityAction.Demand, Role = "UserService+LocalCall")]
		public IAsyncResult BeginSetConfigDC(string partitionFqdn, string serverName, AsyncCallback callback, object asyncState)
		{
			IAsyncResult result = null;
			this.ExecuteServiceCall(delegate
			{
				result = this.InternalBeginSetConfigDC(partitionFqdn, serverName, callback, asyncState);
			});
			return result;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00008FD0 File Offset: 0x000071D0
		public void EndSetConfigDC(IAsyncResult result)
		{
			this.ExecuteServiceCall(delegate
			{
				this.InternalEndSetConfigDC(result);
			});
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000902C File Offset: 0x0000722C
		[PrincipalPermission(SecurityAction.Demand, Role = "ReadOnlyAdmin+LocalCall")]
		[PrincipalPermission(SecurityAction.Demand, Role = "LocalAdministrators+LocalCall")]
		[PrincipalPermission(SecurityAction.Demand, Role = "UserService+LocalCall")]
		public void ReportServerDown(string partitionFqdn, string serverName, ADServerRole role)
		{
			this.ExecuteServiceCall(delegate
			{
				this.InternalReportServerDown(partitionFqdn, serverName, role);
			});
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009070 File Offset: 0x00007270
		private List<TopologyVersion> InternalGetAllTopologyVersions()
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Entering TopologyService.GetTopologyVersion().");
			List<TopologyVersion> list = new List<TopologyVersion>();
			foreach (ADTopology adtopology in TopologyDiscoveryManager.Instance.GetAllTopologies())
			{
				ExTraceGlobals.WCFServiceEndpointTracer.Information<string, int>((long)this.GetHashCode(), "{0} - Version {1}.", adtopology.ForestFqdn, adtopology.Version);
				list.Add(new TopologyVersion(adtopology.ForestFqdn, adtopology.Version));
			}
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction<int>((long)this.GetHashCode(), "Exiting TopologyService.GetTopologyVersion() - returning topology version of total {0} partitions", list.Count);
			return list;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000912C File Offset: 0x0000732C
		private List<TopologyVersion> InternalGetTopologyVersions(List<string> partitionFqdns)
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction<int>((long)this.GetHashCode(), "TopologyService.GetTopologyVersion() - Entering Get Topology Version for {0} partition(s).", (partitionFqdns == null) ? 1 : partitionFqdns.Count);
			if (partitionFqdns == null || partitionFqdns.Count<string>() == 0)
			{
				ExTraceGlobals.WCFServiceEndpointTracer.TraceError((long)this.GetHashCode(), "Partition Fqdns is null or empty");
				throw new ArgumentNullException("Partition Fqdns is null or empty");
			}
			List<TopologyVersion> list = new List<TopologyVersion>(partitionFqdns.Count);
			foreach (string text in partitionFqdns)
			{
				ADTopology adtopology;
				if (TopologyDiscoveryManager.Instance.TryGetTopology(text, out adtopology))
				{
					ExTraceGlobals.WCFServiceEndpointTracer.Information<int, string>((long)this.GetHashCode(), "Returning Topology Version {0} For Partition {1}.", adtopology.Version, text);
					list.Add(new TopologyVersion(adtopology.ForestFqdn, adtopology.Version));
				}
				else
				{
					ExTraceGlobals.WCFServiceEndpointTracer.TraceWarning<string>((long)this.GetHashCode(), "Topology Version not found for Partition {0}.", text);
					list.Add(new TopologyVersion(text, -1));
				}
			}
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction<int>((long)this.GetHashCode(), "TopologyService.GetTopologyVersion() - returning Topology Version for {0} partition(s).", list.Count);
			return list;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009288 File Offset: 0x00007488
		private IAsyncResult InternalBeginGetServersForRole(string partitionFqdn, List<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested, AsyncCallback callback, object asyncState)
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Entering TopologyService.BeginGetServersForRole()");
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ArgumentValidator.ThrowIfOutOfRange<int>("serversRequested", serversRequested, 1, int.MaxValue);
			if (role == ADServerRole.None)
			{
				throw new ArgumentException("Invalid AD Server Role");
			}
			if (ExTraceGlobals.WCFServiceEndpointTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.WCFServiceEndpointTracer.Information((long)this.GetHashCode(), "BeginGetServersForRole. Partition Fqdn {0}, Currently Used Servers {1}, Role {2}, Servers Requested {3}， Prefer same server ｛4｝", new object[]
				{
					partitionFqdn.ToString(),
					string.Join(",", currentlyUsedServers),
					role,
					serversRequested,
					forestWideAffinityRequested
				});
			}
			ServerRequestContext worker = new ServerRequestContext(partitionFqdn, currentlyUsedServers, role, serversRequested, forestWideAffinityRequested);
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(worker, asyncState, callback);
			TopologyDiscoveryManager.Instance.BeginGetTopology(partitionFqdn, delegate(IAsyncResult ar)
			{
				LazyAsyncResult lazyAsyncResult2 = (LazyAsyncResult)ar.AsyncState;
				ServerRequestContext serverRequestContext = (ServerRequestContext)lazyAsyncResult2.AsyncObject;
				serverRequestContext.AsyncResult = ar;
				lazyAsyncResult2.InvokeCallback(lazyAsyncResult2);
			}, lazyAsyncResult);
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Exiting TopologyService.BeginGetServersForRole()");
			return lazyAsyncResult;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009390 File Offset: 0x00007590
		private List<ServerInfo> InternalEndGetServersForRole(IAsyncResult result)
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Entering TopologyService.EndGetServersForRole()");
			ArgumentValidator.ThrowIfNull("result", result);
			ArgumentValidator.ThrowIfTypeInvalid<LazyAsyncResult>("result", result);
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			if (lazyAsyncResult.EndCalled)
			{
				ExTraceGlobals.WCFServiceEndpointTracer.TraceError((long)this.GetHashCode(), "End was already called. Invalid Begin/End");
				throw new InvalidOperationException("End was already called. Invalid Begin/End");
			}
			lazyAsyncResult.EndCalled = true;
			ServerRequestContext serverRequestContext = (ServerRequestContext)lazyAsyncResult.AsyncObject;
			if (serverRequestContext.AsyncResult == null)
			{
				throw new ArgumentException("Topology Not Found");
			}
			ADTopology adtopology = TopologyDiscoveryManager.Instance.EndGetTopology(serverRequestContext.AsyncResult);
			List<ServerInfo> serversForRole = adtopology.GetServersForRole(serverRequestContext.CurrentlyUsedServers.ToList<string>(), serverRequestContext.Role, serverRequestContext.ServersRequested, serverRequestContext.ForestWideAffinityRequested);
			if (ExTraceGlobals.WCFServiceEndpointTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.WCFServiceEndpointTracer.Information((long)this.GetHashCode(), "EndGetServersForRole. Role {0}. Number Of Servers Requested {1}. Original Servers {2}. Returned Servers {3}. ForestWideAffinityRequested {4}", new object[]
				{
					serverRequestContext.Role,
					serverRequestContext.ServersRequested,
					string.Join(",", serverRequestContext.CurrentlyUsedServers),
					string.Join<ServerInfo>(",", serversForRole),
					serverRequestContext.ForestWideAffinityRequested
				});
			}
			return serversForRole;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000094D4 File Offset: 0x000076D4
		private IAsyncResult InternalBeginGetServerFromDomainDN(string domainDN, AsyncCallback callback, object asyncState)
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Entering TopologyService.BeginGetServerFromDomainDN()");
			ArgumentValidator.ThrowIfNullOrEmpty("domainDN", domainDN);
			ADObjectId adobjectId = null;
			if (!ADObjectId.TryParseDnOrGuid(domainDN, out adobjectId))
			{
				throw new ArgumentException("Invalid domainDN");
			}
			ADObjectId domainId = adobjectId.DomainId;
			ExTraceGlobals.WCFServiceEndpointTracer.TraceDebug<string>((long)this.GetHashCode(), "GetServerFromDomainDN {0}", domainDN);
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Exiting TopologyService.BeginGetServerFromDomainDN()");
			return TopologyDiscoveryManager.Instance.BeginGetServerFromDomain(domainId, callback, asyncState);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00009590 File Offset: 0x00007790
		private IAsyncResult InternalBeginSetConfigDC(string partitionFqdn, string serverName, AsyncCallback callback, object asyncState)
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Entering TopologyService.BeginSetConfigDC()");
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ArgumentValidator.ThrowIfNullOrEmpty("serverName", serverName);
			ExTraceGlobals.WCFServiceEndpointTracer.Information<string, string>((long)this.GetHashCode(), "Set ConfigDC. PartitionFqdn {0}, ServerName {1}", partitionFqdn, serverName);
			if (!Globals.IsDatacenter && !TopologyProvider.LocalForestFqdn.Equals(partitionFqdn, StringComparison.OrdinalIgnoreCase))
			{
				throw new TopologyServiceTransientException(Strings.ForestNotFoundOrNotDiscovered(partitionFqdn));
			}
			SingleServerOperationContext worker = new SingleServerOperationContext(partitionFqdn, serverName, "SetConfigDC");
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(worker, asyncState, callback);
			TopologyDiscoveryManager.Instance.BeginGetTopology(partitionFqdn, delegate(IAsyncResult ar)
			{
				LazyAsyncResult lazyAsyncResult2 = (LazyAsyncResult)ar.AsyncState;
				SingleServerOperationContext singleServerOperationContext = (SingleServerOperationContext)lazyAsyncResult2.AsyncObject;
				singleServerOperationContext.AsyncResult = ar;
				lazyAsyncResult2.InvokeCallback(lazyAsyncResult2);
			}, lazyAsyncResult);
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Exiting TopologyService.BeginSetConfigDC()");
			return lazyAsyncResult;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000965C File Offset: 0x0000785C
		private void InternalEndSetConfigDC(IAsyncResult result)
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Entering TopologyService.EndSetConfigDC()");
			ArgumentValidator.ThrowIfNull("result", result);
			ArgumentValidator.ThrowIfTypeInvalid<LazyAsyncResult>("result", result);
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)result;
			if (lazyAsyncResult.EndCalled)
			{
				ExTraceGlobals.WCFServiceEndpointTracer.TraceError((long)this.GetHashCode(), "End was already called. Invalid Begin/End");
				throw new InvalidOperationException("End was already called. Invalid Begin/End");
			}
			lazyAsyncResult.EndCalled = true;
			SingleServerOperationContext singleServerOperationContext = (SingleServerOperationContext)lazyAsyncResult.AsyncObject;
			if (singleServerOperationContext.AsyncResult == null)
			{
				throw new ArgumentException("Topology Not Found");
			}
			ADTopology adtopology = TopologyDiscoveryManager.Instance.EndGetTopology(singleServerOperationContext.AsyncResult);
			bool flag = adtopology.TrySetConfigDC(singleServerOperationContext.ServerFqdn);
			ExTraceGlobals.WCFServiceEndpointTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "Config DC for Partition FQDN {0} {1}changed to {1}", singleServerOperationContext.PartitionFqdn, flag ? string.Empty : "DID NOT", singleServerOperationContext.ServerFqdn);
			if (!flag)
			{
				ExTraceGlobals.WCFServiceEndpointTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Starting a forest Rediscovery for Partition FQDN {0} setting preferred CDC as {1}", singleServerOperationContext.PartitionFqdn, singleServerOperationContext.ServerFqdn);
				TopologyDiscoveryManager.Instance.StartForestDiscoverOrRediscover(singleServerOperationContext.PartitionFqdn, singleServerOperationContext.ServerFqdn);
			}
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Exiting TopologyService.EndSetConfigDC()");
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000978C File Offset: 0x0000798C
		private void InternalReportServerDown(string partitionFqdn, string serverName, ADServerRole role)
		{
			ExTraceGlobals.WCFServiceEndpointTracer.TraceFunction((long)this.GetHashCode(), "Entering TopologyService.ReportServerDown()");
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ArgumentValidator.ThrowIfNullOrEmpty("serverName", serverName);
			if (role == ADServerRole.None)
			{
				ExTraceGlobals.WCFServiceEndpointTracer.TraceError((long)this.GetHashCode(), "Invalid Server Role");
				throw new ArgumentException("Invalid Server Role");
			}
			ExTraceGlobals.WCFServiceEndpointTracer.Information<string, string, ADServerRole>((long)this.GetHashCode(), "Report Server Down. PartitionFqdn {0}, ServerName {1}, Role {2}", partitionFqdn, serverName, role);
			ADTopology adtopology = null;
			if (!TopologyDiscoveryManager.Instance.TryGetTopology(partitionFqdn, out adtopology))
			{
				ExTraceGlobals.WCFServiceEndpointTracer.TraceError<string>((long)this.GetHashCode(), "Topology not found for Partition FQDN {0}", partitionFqdn);
				return;
			}
			adtopology.ReportServerDown(serverName, role);
			TopologyDiscoveryManager.Instance.StartForestDiscoveryOrRediscoveryIfRequired(partitionFqdn);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00009840 File Offset: 0x00007A40
		private void ExecuteServiceCall(Action action)
		{
			try
			{
				action();
			}
			catch (ArgumentNullException ex)
			{
				throw new FaultException<ArgumentNullException>(ex, new FaultReason(ex.Message));
			}
			catch (ArgumentException ex2)
			{
				throw new FaultException<ArgumentException>(ex2, new FaultReason(ex2.Message));
			}
			catch (InvalidOperationException ex3)
			{
				throw new FaultException<InvalidOperationException>(ex3, new FaultReason(ex3.Message));
			}
			catch (Exception ex4)
			{
				ServiceDebugBehavior serviceDebugBehavior = OperationContext.Current.Host.Description.Behaviors.Find<ServiceDebugBehavior>();
				Exception ex5 = (ex4 is NoSuitableServerFoundException && ex4.StackTrace.Contains("EndGetServerFromDomain") && ex4.Message.Contains("Wrapping Exception") && ex4.InnerException != null) ? ex4.InnerException : ex4;
				throw new FaultException<TopologyServiceFault>(TopologyServiceFault.Create(ex5, serviceDebugBehavior != null && serviceDebugBehavior.IncludeExceptionDetailInFaults), new FaultReason(ex5.Message));
			}
		}

		// Token: 0x0400007F RID: 127
		internal const uint LidWcfInjectDelay = 2466655549U;

		// Token: 0x04000080 RID: 128
		private const string SetConfigDCStr = "SetConfigDC";
	}
}
