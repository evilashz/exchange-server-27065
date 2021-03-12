using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Principal;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000276 RID: 630
	internal class TcpServerChannel : TcpChannel
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00064EAC File Offset: 0x000630AC
		public string NetworkName
		{
			get
			{
				return this.m_networkName;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00064EB4 File Offset: 0x000630B4
		public string ClientNodeName
		{
			get
			{
				return this.m_clientNodeName;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00064EBC File Offset: 0x000630BC
		public bool ClientIsDagMember
		{
			get
			{
				return this.m_networkName != null;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00064ECA File Offset: 0x000630CA
		public ExchangeNetworkPerfmonCounters PerfCounters
		{
			get
			{
				return this.m_networkPerfCounters;
			}
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x00064ED4 File Offset: 0x000630D4
		private TcpServerChannel(Socket socket, NegotiateStream stream, int ioTimeoutInMSec, TimeSpan idleLimit) : base(socket, stream, ioTimeoutInMSec, idleLimit)
		{
			IPEndPoint ipendPoint = (IPEndPoint)socket.RemoteEndPoint;
			NetworkEndPoint networkEndPoint;
			ExchangeNetwork exchangeNetwork = NetworkManager.LookupEndPoint(ipendPoint.Address, out networkEndPoint);
			if (exchangeNetwork != null)
			{
				this.m_networkName = exchangeNetwork.Name;
				this.m_clientNodeName = networkEndPoint.NodeName;
				this.m_networkPerfCounters = exchangeNetwork.PerfCounters;
				base.PartnerNodeName = this.m_clientNodeName;
				ExTraceGlobals.TcpChannelTracer.TraceDebug<string, string, EndPoint>((long)this.GetHashCode(), "Opening server channel with DAG member {0} on network {1} from ip {2}", this.m_clientNodeName, this.m_networkName, socket.RemoteEndPoint);
			}
			else
			{
				base.PartnerNodeName = socket.RemoteEndPoint.ToString();
				ExTraceGlobals.TcpChannelTracer.TraceDebug<EndPoint>((long)this.GetHashCode(), "Opening server channel with unknown client on ip {0}", socket.RemoteEndPoint);
			}
			ReplayCrimsonEvents.ServerNetworkConnectionOpen.Log<string, string, string>(base.PartnerNodeName, base.RemoteEndpointString, base.LocalEndpointString);
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00064FAC File Offset: 0x000631AC
		public static TcpServerChannel AuthenticateAsServer(TcpListener listener, Socket connection)
		{
			TcpServerChannel tcpServerChannel = null;
			int iotimeoutInMSec = listener.ListenerConfig.IOTimeoutInMSec;
			NegotiateStream negotiateStream = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				NetworkStream networkStream = new NetworkStream(connection, false);
				disposeGuard.Add<NetworkStream>(networkStream);
				negotiateStream = new NegotiateStream(networkStream, false);
				disposeGuard.Add<NegotiateStream>(negotiateStream);
				negotiateStream.WriteTimeout = iotimeoutInMSec;
				negotiateStream.ReadTimeout = iotimeoutInMSec;
				negotiateStream.AuthenticateAsServer(CredentialCache.DefaultNetworkCredentials, ProtectionLevel.None, TokenImpersonationLevel.Identification);
				if (!negotiateStream.IsAuthenticated)
				{
					string text = "Authentication failed";
					ExTraceGlobals.TcpServerTracer.TraceError((long)connection.GetHashCode(), text);
					ReplayCrimsonEvents.ServerSideConnectionFailure.LogPeriodic<string, string, string>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, connection.RemoteEndPoint.ToString(), connection.LocalEndPoint.ToString(), text);
				}
				if (!negotiateStream.IsMutuallyAuthenticated)
				{
					ExTraceGlobals.TcpServerTracer.TraceError((long)connection.GetHashCode(), "Mutual Authentication failed");
				}
				WindowsIdentity wid = negotiateStream.RemoteIdentity as WindowsIdentity;
				string text2 = null;
				try
				{
					text2 = negotiateStream.RemoteIdentity.Name;
				}
				catch (SystemException ex)
				{
					string text3 = string.Format("RemoteIdentity.Name failed: {0}", ex.ToString());
					ExTraceGlobals.TcpServerTracer.TraceError((long)connection.GetHashCode(), text3);
					ReplayCrimsonEvents.ServerSideConnectionFailure.LogPeriodic<string, string, string>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, connection.RemoteEndPoint.ToString(), connection.LocalEndPoint.ToString(), text3);
				}
				if (!RemoteDataProvider.AuthorizeRequest(wid))
				{
					ExTraceGlobals.TcpServerTracer.TraceError<string, string>((long)connection.GetHashCode(), "Authorization failed. ClientMachine={0}, User={1}", connection.RemoteEndPoint.ToString(), text2);
					ReplayCrimsonEvents.ServerSideConnectionFailure.LogPeriodic<string, string, string>(Environment.MachineName, DiagCore.DefaultEventSuppressionInterval, connection.RemoteEndPoint.ToString(), connection.LocalEndPoint.ToString(), string.Format("AuthorizeRequest failed. User={0}", text2));
					return null;
				}
				tcpServerChannel = new TcpServerChannel(connection, negotiateStream, listener.ListenerConfig.IOTimeoutInMSec, listener.ListenerConfig.IdleLimit);
				ExTraceGlobals.TcpServerTracer.TraceDebug<string, bool, bool>((long)tcpServerChannel.GetHashCode(), "Connection authenticated as {0}. Encrypted={1} Signed={2}", text2, negotiateStream.IsEncrypted, negotiateStream.IsSigned);
				if (tcpServerChannel != null)
				{
					disposeGuard.Success();
				}
			}
			return tcpServerChannel;
		}

		// Token: 0x040009C3 RID: 2499
		private string m_networkName;

		// Token: 0x040009C4 RID: 2500
		private string m_clientNodeName;

		// Token: 0x040009C5 RID: 2501
		private ExchangeNetworkPerfmonCounters m_networkPerfCounters;
	}
}
