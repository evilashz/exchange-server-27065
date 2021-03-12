using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200003F RID: 63
	internal class TcpConnector : ITcpConnector
	{
		// Token: 0x06000217 RID: 535 RVA: 0x000084AC File Offset: 0x000066AC
		public TcpClientChannel TryConnect(NetworkPath netPath, out NetworkTransportException failureEx)
		{
			return this.TryConnect(netPath, DagNetEnvironment.ConnectTimeoutInSec * 1000, out failureEx);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000084C4 File Offset: 0x000066C4
		public TcpClientChannel TryConnect(NetworkPath netPath, int timeoutInMsec, out NetworkTransportException failureEx)
		{
			TcpClientChannel result = null;
			TcpClientChannel.TryOpenChannel(netPath, timeoutInMsec, out result, out failureEx);
			return result;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000084E0 File Offset: 0x000066E0
		public NetworkPath BuildDnsNetworkPath(string targetServer, int replicationPort)
		{
			try
			{
				IPAddress ipaddress = TcpConnector.ChooseAddressFromDNS(targetServer);
				if (ipaddress != null)
				{
					return new NetworkPath(targetServer, ipaddress, replicationPort, null)
					{
						NetworkChoiceIsMandatory = true
					};
				}
			}
			catch (SocketException ex)
			{
				throw new NetworkTransportException(Strings.NetworkAddressResolutionFailed(targetServer, ex.Message), ex);
			}
			throw new NetworkTransportException(Strings.NetworkAddressResolutionFailedNoDnsEntry(targetServer));
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000854C File Offset: 0x0000674C
		public NetworkPath ChooseDagNetworkPath(string targetServerName, string networkName, NetworkPath.ConnectionPurpose purpose)
		{
			DagNetConfig dagConfig;
			DagNetRoute[] array = DagNetEnvironment.NetChooser.BuildRoutes(targetServerName, false, networkName, out dagConfig);
			NetworkPath networkPath = null;
			if (array != null)
			{
				foreach (DagNetRoute dagNetRoute in array)
				{
					networkPath = new NetworkPath(targetServerName, dagNetRoute.TargetIPAddr, dagNetRoute.TargetPort, dagNetRoute.SourceIPAddr);
					networkPath.CrossSubnet = dagNetRoute.IsCrossSubnet;
					networkPath.ApplyNetworkPolicy(dagConfig);
				}
			}
			return networkPath;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000085B8 File Offset: 0x000067B8
		public TcpClientChannel OpenChannel(string targetServerName, ISimpleBufferPool socketStreamBufferPool, IPool<SocketStreamAsyncArgs> socketStreamAsyncArgPool, SocketStream.ISocketStreamPerfCounters perfCtrs, out NetworkPath netPath)
		{
			DagNetConfig dagConfig;
			DagNetRoute[] array = DagNetChooser.ProposeRoutes(targetServerName, out dagConfig);
			TcpClientChannel tcpClientChannel = null;
			netPath = null;
			NetworkTransportException ex = null;
			if (array != null)
			{
				foreach (DagNetRoute dagNetRoute in array)
				{
					netPath = new NetworkPath(targetServerName, dagNetRoute.TargetIPAddr, dagNetRoute.TargetPort, dagNetRoute.SourceIPAddr);
					netPath.CrossSubnet = dagNetRoute.IsCrossSubnet;
					this.ApplySocketStreamArgs(netPath, socketStreamBufferPool, socketStreamAsyncArgPool, perfCtrs);
					netPath.ApplyNetworkPolicy(dagConfig);
					tcpClientChannel = this.TryConnect(netPath, out ex);
					if (tcpClientChannel != null)
					{
						break;
					}
				}
			}
			if (tcpClientChannel == null)
			{
				netPath = this.BuildDnsNetworkPath(targetServerName, this.GetCurrentReplicationPort());
				this.ApplySocketStreamArgs(netPath, socketStreamBufferPool, socketStreamAsyncArgPool, perfCtrs);
				netPath.ApplyNetworkPolicy(dagConfig);
				tcpClientChannel = this.TryConnect(netPath, out ex);
				if (tcpClientChannel == null)
				{
					throw ex;
				}
			}
			return tcpClientChannel;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00008683 File Offset: 0x00006883
		private void ApplySocketStreamArgs(NetworkPath netPath, ISimpleBufferPool socketStreamBufferPool, IPool<SocketStreamAsyncArgs> socketStreamAsyncArgPool, SocketStream.ISocketStreamPerfCounters perfCtrs)
		{
			if (socketStreamBufferPool != null)
			{
				netPath.SocketStreamBufferPool = socketStreamBufferPool;
				netPath.SocketStreamAsyncArgPool = socketStreamAsyncArgPool;
				netPath.SocketStreamPerfCounters = perfCtrs;
				netPath.UseSocketStream = true;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000086A8 File Offset: 0x000068A8
		private static NetworkPath BuildDnsNetworkPath(string targetName, ushort replicationPort)
		{
			try
			{
				IPAddress ipaddress = TcpConnector.ChooseAddressFromDNS(targetName);
				if (ipaddress != null)
				{
					return new NetworkPath(targetName, ipaddress, (int)replicationPort, null)
					{
						NetworkChoiceIsMandatory = true
					};
				}
			}
			catch (SocketException ex)
			{
				throw new NetworkTransportException(Strings.NetworkAddressResolutionFailed(targetName, ex.Message), ex);
			}
			throw new NetworkTransportException(Strings.NetworkAddressResolutionFailedNoDnsEntry(targetName));
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00008714 File Offset: 0x00006914
		public static IPAddress ChooseAddressFromDNS(string targetName)
		{
			Exception ex;
			IPAddress[] dnsAddresses = NetworkUtil.GetDnsAddresses(targetName, ref ex);
			foreach (IPAddress ipaddress in dnsAddresses)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					return ipaddress;
				}
			}
			foreach (IPAddress ipaddress2 in dnsAddresses)
			{
				if (ipaddress2.AddressFamily == AddressFamily.InterNetworkV6)
				{
					return ipaddress2;
				}
			}
			return null;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008784 File Offset: 0x00006984
		private int GetCurrentReplicationPort()
		{
			DagNetConfig dagNetConfig = DagNetEnvironment.FetchLastKnownNetConfig();
			return dagNetConfig.ReplicationPort;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000879D File Offset: 0x0000699D
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000087A5 File Offset: 0x000069A5
		public bool ForceSocketStream { get; set; }

		// Token: 0x04000136 RID: 310
		private static readonly Trace Tracer = ExTraceGlobals.NetPathTracer;
	}
}
