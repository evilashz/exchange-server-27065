using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.EseRepl;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200026F RID: 623
	internal class TcpConnector : ITcpConnector
	{
		// Token: 0x0600185A RID: 6234 RVA: 0x00064430 File Offset: 0x00062630
		public TcpClientChannel TryConnect(NetworkPath netPath, out NetworkTransportException failureEx)
		{
			return this.TryConnect(netPath, DagNetEnvironment.ConnectTimeoutInSec * 1000, out failureEx);
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x00064448 File Offset: 0x00062648
		public TcpClientChannel TryConnect(NetworkPath netPath, int timeoutInMsec, out NetworkTransportException failureEx)
		{
			TcpClientChannel result = null;
			TcpClientChannel.TryOpenChannel(netPath, timeoutInMsec, out result, out failureEx);
			return result;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00064464 File Offset: 0x00062664
		public NetworkPath BuildDnsNetworkPath(string targetServer, int replicationPort)
		{
			try
			{
				IPAddress ipaddress = NetworkManager.ChooseAddressFromDNS(targetServer);
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
				throw new NetworkTransportException(ReplayStrings.NetworkAddressResolutionFailed(targetServer, ex.Message), ex);
			}
			throw new NetworkTransportException(ReplayStrings.NetworkAddressResolutionFailedNoDnsEntry(targetServer));
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x000644D0 File Offset: 0x000626D0
		public NetworkPath ChooseDagNetworkPath(string targetName, string networkName, NetworkPath.ConnectionPurpose purpose)
		{
			return NetworkManager.InternalChooseDagNetworkPath(targetName, networkName, purpose);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x000644DC File Offset: 0x000626DC
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
				netPath = this.BuildDnsNetworkPath(targetServerName, (int)NetworkManager.GetReplicationPort());
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

		// Token: 0x0600185F RID: 6239 RVA: 0x000645A6 File Offset: 0x000627A6
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

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x000645C8 File Offset: 0x000627C8
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x000645D0 File Offset: 0x000627D0
		public bool ForceSocketStream { get; set; }
	}
}
