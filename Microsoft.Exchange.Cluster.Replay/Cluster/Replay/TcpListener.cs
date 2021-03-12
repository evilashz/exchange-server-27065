using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Principal;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000271 RID: 625
	internal class TcpListener
	{
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x00064790 File Offset: 0x00062990
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x00064798 File Offset: 0x00062998
		public TcpListener.Config ListenerConfig { get; private set; }

		// Token: 0x06001867 RID: 6247 RVA: 0x000647A4 File Offset: 0x000629A4
		public Exception StartListening(TcpListener.Config cfg)
		{
			bool flag = false;
			Exception ex = null;
			if (this.m_started)
			{
				throw new ArgumentException("TcpListener is one time only");
			}
			try
			{
				this.ListenerConfig = cfg;
				this.m_started = true;
				this.m_listenSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
				this.m_listenSocket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, 0);
				this.m_listenSocket.ReceiveBufferSize = cfg.ReceiveBufferSize;
				this.m_listenSocket.SendBufferSize = cfg.SendBufferSize;
				IPEndPoint localEP = new IPEndPoint(IPAddress.IPv6Any, cfg.ListenPort);
				this.m_listenSocket.Bind(localEP);
				int maxValue = int.MaxValue;
				this.m_listenSocket.Listen(maxValue);
				this.m_listenSocket.BeginAccept(new AsyncCallback(TcpListener.AcceptCallback), this);
				this.m_idleTimer = TcpListener.IdleTimer.CreateTimer(this);
				this.m_idleTimer.Start();
				flag = true;
				TcpListener.Tracer.TraceDebug<int>((long)this.GetHashCode(), "Listening on port {0}", cfg.ListenPort);
				ReplayCrimsonEvents.TcpListenerStarted.Log<int>(cfg.ListenPort);
			}
			catch (NetworkTransportException ex2)
			{
				ex = ex2;
			}
			catch (SocketException ex3)
			{
				ex = ex3;
			}
			finally
			{
				if (!flag)
				{
					TcpListener.Tracer.TraceError<Exception>((long)this.GetHashCode(), "StartListening failed: {0}", ex);
					this.m_listenSocket.Close();
					this.m_listenSocket = null;
				}
			}
			return ex;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x0006490C File Offset: 0x00062B0C
		public void Stop()
		{
			this.m_stopping = true;
			if (this.m_idleTimer != null)
			{
				this.m_idleTimer.Stop();
				this.m_idleTimer = null;
			}
			if (this.m_listenSocket != null)
			{
				this.m_listenSocket.Close();
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00064944 File Offset: 0x00062B44
		private static void AcceptCallback(IAsyncResult ar)
		{
			Socket socket = null;
			TcpListener tcpListener = (TcpListener)ar.AsyncState;
			try
			{
				socket = tcpListener.m_listenSocket.EndAccept(ar);
			}
			catch (SocketException ex)
			{
				ExTraceGlobals.TcpServerTracer.TraceDebug<string>(0L, "TCP listener callback got exception: {0}", ex.Message);
			}
			catch (ObjectDisposedException ex2)
			{
				if (!tcpListener.m_stopping)
				{
					tcpListener.HandleUnexpectedException("TcpListener.ConnectCallback.EndAccept", ex2);
				}
			}
			if (!tcpListener.m_stopping)
			{
				Exception ex3 = null;
				try
				{
					tcpListener.m_listenSocket.BeginAccept(new AsyncCallback(TcpListener.AcceptCallback), tcpListener);
				}
				catch (SocketException ex4)
				{
					ex3 = ex4;
				}
				catch (ObjectDisposedException ex5)
				{
					ex3 = ex5;
				}
				if (ex3 != null)
				{
					ExTraceGlobals.TcpServerTracer.TraceError<Exception>(0L, "TCP listener. BeginAccept threw: {0}", ex3);
					if (socket != null)
					{
						socket.Close();
					}
					if (!tcpListener.m_stopping)
					{
						tcpListener.HandleUnexpectedException("TcpListener.ConnectCallback.BeginAccept", ex3);
					}
					return;
				}
			}
			if (socket != null)
			{
				tcpListener.ProcessConnection(socket);
			}
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x00064A4C File Offset: 0x00062C4C
		private void HandleUnexpectedException(string context, Exception ex)
		{
			string text = string.Format("{0}:{1}", context, ex.ToString());
			ExTraceGlobals.TcpServerTracer.TraceError((long)this.GetHashCode(), text);
			ReplayCrimsonEvents.OperationGeneratedUnhandledException.Log<string>(text);
			throw ex;
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00064A8C File Offset: 0x00062C8C
		private void ProcessConnection(Socket connection)
		{
			if (this.ListenerConfig.SocketConnectioHandOff != null)
			{
				this.ListenerConfig.SocketConnectioHandOff(connection);
				return;
			}
			TcpServerChannel tcpServerChannel = null;
			bool flag = false;
			Exception ex = null;
			try
			{
				ExTraceGlobals.TcpServerTracer.TraceDebug<EndPoint>((long)connection.GetHashCode(), "Connection received from {0}", connection.RemoteEndPoint);
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2577804605U);
				tcpServerChannel = TcpServerChannel.AuthenticateAsServer(this, connection);
				if (tcpServerChannel != null)
				{
					this.ListenerConfig.AuthConnectionHandOff(tcpServerChannel, this);
					flag = true;
				}
			}
			catch (SocketException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (AuthenticationException ex4)
			{
				ex = ex4;
			}
			catch (IdentityNotMappedException ex5)
			{
				ex = ex5;
			}
			catch (NetworkTransportException ex6)
			{
				ex = ex6;
			}
			finally
			{
				if (ex != null)
				{
					ExTraceGlobals.TcpServerTracer.TraceError<Exception>((long)this.GetHashCode(), "TCP ProcessConnection fails: {0}", ex);
				}
				if (tcpServerChannel == null)
				{
					connection.Close();
				}
				else if (!flag)
				{
					tcpServerChannel.Close();
				}
			}
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00064BA8 File Offset: 0x00062DA8
		public void AddActiveChannel(NetworkChannel channel)
		{
			lock (this.m_activeClientConnections)
			{
				this.m_activeClientConnections.Add(channel);
			}
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00064BF0 File Offset: 0x00062DF0
		public void RemoveActiveChannel(NetworkChannel channel)
		{
			lock (this.m_activeClientConnections)
			{
				this.m_activeClientConnections.Remove(channel);
			}
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x00064C38 File Offset: 0x00062E38
		public NetworkChannel FindSeedingChannel(MonitoredDatabase db)
		{
			lock (this.m_activeClientConnections)
			{
				foreach (NetworkChannel networkChannel in this.m_activeClientConnections)
				{
					if (networkChannel.IsSeeding && object.ReferenceEquals(networkChannel.MonitoredDatabase, db))
					{
						return networkChannel;
					}
				}
			}
			return null;
		}

		// Token: 0x040009B2 RID: 2482
		private static readonly Trace Tracer = ExTraceGlobals.TcpServerTracer;

		// Token: 0x040009B3 RID: 2483
		private Socket m_listenSocket;

		// Token: 0x040009B4 RID: 2484
		private bool m_stopping;

		// Token: 0x040009B5 RID: 2485
		private List<NetworkChannel> m_activeClientConnections = new List<NetworkChannel>();

		// Token: 0x040009B6 RID: 2486
		private TcpListener.IdleTimer m_idleTimer;

		// Token: 0x040009B7 RID: 2487
		private bool m_started;

		// Token: 0x02000272 RID: 626
		// (Invoke) Token: 0x06001872 RID: 6258
		internal delegate void AuthenticatedConnectionHandler(TcpServerChannel tcpChannel, TcpListener listener);

		// Token: 0x02000273 RID: 627
		// (Invoke) Token: 0x06001876 RID: 6262
		internal delegate void SocketConnectionHandler(Socket socket);

		// Token: 0x02000274 RID: 628
		internal class Config
		{
			// Token: 0x170006BD RID: 1725
			// (get) Token: 0x06001879 RID: 6265 RVA: 0x00064CEB File Offset: 0x00062EEB
			// (set) Token: 0x0600187A RID: 6266 RVA: 0x00064CF3 File Offset: 0x00062EF3
			public int ListenPort { get; set; }

			// Token: 0x170006BE RID: 1726
			// (get) Token: 0x0600187B RID: 6267 RVA: 0x00064CFC File Offset: 0x00062EFC
			// (set) Token: 0x0600187C RID: 6268 RVA: 0x00064D04 File Offset: 0x00062F04
			public string LocalNodeName { get; set; }

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x0600187D RID: 6269 RVA: 0x00064D0D File Offset: 0x00062F0D
			// (set) Token: 0x0600187E RID: 6270 RVA: 0x00064D15 File Offset: 0x00062F15
			public int ReceiveBufferSize { get; set; }

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x0600187F RID: 6271 RVA: 0x00064D1E File Offset: 0x00062F1E
			// (set) Token: 0x06001880 RID: 6272 RVA: 0x00064D26 File Offset: 0x00062F26
			public int SendBufferSize { get; set; }

			// Token: 0x170006C1 RID: 1729
			// (get) Token: 0x06001881 RID: 6273 RVA: 0x00064D2F File Offset: 0x00062F2F
			// (set) Token: 0x06001882 RID: 6274 RVA: 0x00064D37 File Offset: 0x00062F37
			public int IOTimeoutInMSec { get; set; }

			// Token: 0x170006C2 RID: 1730
			// (get) Token: 0x06001883 RID: 6275 RVA: 0x00064D40 File Offset: 0x00062F40
			// (set) Token: 0x06001884 RID: 6276 RVA: 0x00064D48 File Offset: 0x00062F48
			public TimeSpan IdleLimit { get; set; }

			// Token: 0x170006C3 RID: 1731
			// (get) Token: 0x06001885 RID: 6277 RVA: 0x00064D51 File Offset: 0x00062F51
			// (set) Token: 0x06001886 RID: 6278 RVA: 0x00064D59 File Offset: 0x00062F59
			public TcpListener.AuthenticatedConnectionHandler AuthConnectionHandOff { get; set; }

			// Token: 0x170006C4 RID: 1732
			// (get) Token: 0x06001887 RID: 6279 RVA: 0x00064D62 File Offset: 0x00062F62
			// (set) Token: 0x06001888 RID: 6280 RVA: 0x00064D6A File Offset: 0x00062F6A
			public TcpListener.SocketConnectionHandler SocketConnectioHandOff { get; set; }

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x06001889 RID: 6281 RVA: 0x00064D73 File Offset: 0x00062F73
			// (set) Token: 0x0600188A RID: 6282 RVA: 0x00064D7B File Offset: 0x00062F7B
			public bool KeepServerOpenByDefault { get; set; }

			// Token: 0x0600188B RID: 6283 RVA: 0x00064D84 File Offset: 0x00062F84
			public Config()
			{
				this.LocalNodeName = Environment.MachineName;
				this.ReceiveBufferSize = RegistryParameters.LogCopyNetworkTransferSize;
				this.SendBufferSize = RegistryParameters.LogCopyNetworkTransferSize;
				this.IOTimeoutInMSec = TcpChannel.GetDefaultTimeoutInMs();
				this.IdleLimit = TimeSpan.FromSeconds((double)RegistryParameters.TcpChannelIdleLimitInSec);
			}
		}

		// Token: 0x02000275 RID: 629
		private class IdleTimer : TimerComponent
		{
			// Token: 0x0600188C RID: 6284 RVA: 0x00064DD4 File Offset: 0x00062FD4
			public static TcpListener.IdleTimer CreateTimer(TcpListener listener)
			{
				TimeSpan period = TimeSpan.FromSeconds((double)Math.Min((int)listener.ListenerConfig.IdleLimit.TotalSeconds, 120));
				return new TcpListener.IdleTimer(listener, period);
			}

			// Token: 0x0600188D RID: 6285 RVA: 0x00064E0A File Offset: 0x0006300A
			private IdleTimer(TcpListener listener, TimeSpan period) : base(period, period, "TcpServerIdleTimer")
			{
				this.m_listener = listener;
			}

			// Token: 0x0600188E RID: 6286 RVA: 0x00064E20 File Offset: 0x00063020
			protected override void TimerCallbackInternal()
			{
				TcpListener.Tracer.TraceDebug(0L, "TcpServerIdleTimer running");
				NetworkChannel[] array;
				lock (this.m_listener.m_activeClientConnections)
				{
					array = this.m_listener.m_activeClientConnections.ToArray();
				}
				foreach (NetworkChannel networkChannel in array)
				{
					networkChannel.TcpChannel.CancelIfIdleTooLong();
				}
			}

			// Token: 0x040009C2 RID: 2498
			private TcpListener m_listener;
		}
	}
}
