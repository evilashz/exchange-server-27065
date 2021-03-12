using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x0200002B RID: 43
	internal abstract class VirtualServer : ExchangeDiagnosableWrapper<SessionsInfo>, IDisposable
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000985C File Offset: 0x00007A5C
		public VirtualServer(ProtocolBaseServices server, PopImapAdConfiguration configuration)
		{
			ProtocolBaseServices.Assert(server != null, "server is null", new object[0]);
			ProtocolBaseServices.Assert(configuration != null, "configuration is null", new object[0]);
			this.server = server;
			if (ResponseFactory.AppendServerNameInBannerEnabled)
			{
				string s = string.Format("{0}.{1}", configuration.Server.Name, configuration.Server.DomainId);
				string str = Convert.ToBase64String(Encoding.Unicode.GetBytes(s));
				this.banner = configuration.Banner + " [" + str + "]";
			}
			else
			{
				this.banner = configuration.Banner;
			}
			this.sslCertificateName = new string[]
			{
				configuration.X509CertificateName
			};
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe)
			{
				this.sslPorts = new List<IPEndPoint>(configuration.SSLBindings.Count);
				foreach (IPBinding item in configuration.SSLBindings)
				{
					this.sslPorts.Add(item);
				}
				this.nonSslPorts = new List<IPEndPoint>(configuration.UnencryptedOrTLSBindings.Count);
				using (MultiValuedProperty<IPBinding>.Enumerator enumerator2 = configuration.UnencryptedOrTLSBindings.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						IPBinding item2 = enumerator2.Current;
						this.nonSslPorts.Add(item2);
					}
					goto IL_210;
				}
			}
			ProtocolBaseServices.ServerTracer.TraceDebug<int>(0L, "Initializing BE virtual server . Proxytargetport:{0}.", configuration.ProxyTargetPort);
			this.sslPorts = new List<IPEndPoint>(4);
			this.sslPorts.Add(new IPEndPoint(IPAddress.Any, configuration.ProxyTargetPort));
			this.sslPorts.Add(new IPEndPoint(IPAddress.IPv6Any, configuration.ProxyTargetPort));
			this.nonSslPorts = new List<IPEndPoint>(4);
			this.nonSslPorts.Add(new IPEndPoint(IPAddress.Any, configuration.ProxyTargetPort));
			this.nonSslPorts.Add(new IPEndPoint(IPAddress.IPv6Any, configuration.ProxyTargetPort));
			IL_210:
			this.sessions = new List<ProtocolSession>();
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public ProtocolBaseServices Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public int ConnectionCount
		{
			get
			{
				return this.sessions.Count;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009AB8 File Offset: 0x00007CB8
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificateCache.Find(this.sslCertificateName, CertificateSelectionOption.WildcardAllowed | CertificateSelectionOption.PreferedNonSelfSigned);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00009AD9 File Offset: 0x00007CD9
		public string Banner
		{
			get
			{
				return this.banner;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000287 RID: 647
		public abstract ExPerformanceCounter Connections_Current { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000288 RID: 648
		public abstract ExPerformanceCounter Connections_Failed { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000289 RID: 649
		public abstract ExPerformanceCounter Connections_Rejected { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600028A RID: 650
		public abstract ExPerformanceCounter Connections_Total { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600028B RID: 651
		public abstract ExPerformanceCounter UnAuth_Connections { get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600028C RID: 652
		public abstract ExPerformanceCounter SSLConnections_Current { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600028D RID: 653
		public abstract ExPerformanceCounter SSLConnections_Total { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600028E RID: 654
		public abstract ExPerformanceCounter InvalidCommands { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600028F RID: 655
		public abstract ExPerformanceCounter AverageCommandProcessingTime { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000290 RID: 656
		public abstract ExPerformanceCounter Proxy_Connections_Current { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000291 RID: 657
		public abstract ExPerformanceCounter Proxy_Connections_Failed { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000292 RID: 658
		public abstract ExPerformanceCounter Proxy_Connections_Total { get; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000293 RID: 659
		public abstract ExPerformanceCounter Requests_Total { get; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000294 RID: 660
		public abstract ExPerformanceCounter Requests_Failure { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000295 RID: 661
		public abstract int RpcLatencyCounterIndex { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000296 RID: 662
		public abstract int LdapLatencyCounterIndex { get; }

		// Token: 0x06000297 RID: 663 RVA: 0x00009AE1 File Offset: 0x00007CE1
		public static VirtualServer GetInstance()
		{
			return ProtocolBaseServices.VirtualServer;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009AE8 File Offset: 0x00007CE8
		public bool Initialize()
		{
			if (!string.IsNullOrEmpty(this.sslCertificateName[0]))
			{
				this.certificateCache.Open(OpenFlags.ReadOnly);
				if (this.Certificate == null)
				{
					ProtocolBaseServices.ServerTracer.TraceError<string>(0L, "Unable to find certificate \"{0}\".", this.sslCertificateName[0]);
					ProtocolBaseServices.LogEvent(this.server.SslCertificateNotFoundEventTuple, this.sslCertificateName[0], new string[]
					{
						this.sslCertificateName[0]
					});
				}
			}
			else
			{
				ProtocolBaseServices.ServerTracer.TraceError(0L, "Configuration information for the SSL certificate is null or empty.");
				ProtocolBaseServices.LogEvent(this.server.SslCertificateNotFoundEventTuple, string.Empty, new string[]
				{
					string.Empty
				});
			}
			return true;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009B9C File Offset: 0x00007D9C
		public void AcceptClientConnection(Socket socket)
		{
			ProtocolSession protocolSession = null;
			if (this.stopping)
			{
				VirtualServer.ShutdownSocket(socket);
				return;
			}
			IPAddress address = ((IPEndPoint)socket.RemoteEndPoint).Address;
			if (!this.server.IsOnline() && !address.Equals(IPAddress.Loopback) && !address.Equals(IPAddress.IPv6Loopback))
			{
				ProtocolBaseServices.ServerTracer.TraceError(0L, "Rejecting connection because Protocol is set to Offline.");
				VirtualServer.ShutdownSocket(socket);
				return;
			}
			try
			{
				lock (this.sessions)
				{
					if (this.stopping)
					{
						ProtocolBaseServices.ServerTracer.TraceDebug(0L, "AcceptClientConnection is called when service is stopping, connection rejected.");
						VirtualServer.ShutdownSocket(socket);
						return;
					}
					this.Connections_Total.Increment();
					this.Connections_Current.Increment();
					NetworkConnection connection = new NetworkConnection(socket, 4096);
					protocolSession = this.CreateNewSession(connection);
					ProtocolBaseServices.ServerTracer.TraceDebug<long, IPEndPoint, IPEndPoint>(0L, "New Tcp connection {0} detected from {1} to {2}", protocolSession.SessionId, protocolSession.RemoteEndPoint, protocolSession.LocalEndPoint);
					if (this.sessions.Count >= this.server.MaxConnectionCount)
					{
						ProtocolBaseServices.ServerTracer.TraceWarning<int>(0L, "Maximum connections exceeded {0}, session disconnected.", this.server.MaxConnectionCount);
						ProtocolBaseServices.LogEvent(this.server.MaxConnectionCountExceededEventTuple, this.ToString(), new string[0]);
						protocolSession.BeginShutdown(this.server.MaxConnectionsError);
						this.Connections_Rejected.Increment();
						return;
					}
					this.sessions.Add(protocolSession);
				}
				bool flag2 = false;
				if (!this.sslPorts.Contains(protocolSession.LocalEndPoint))
				{
					if (!this.nonSslPorts.Contains(protocolSession.LocalEndPoint))
					{
						IPEndPoint item = new IPEndPoint(IPAddress.Any, protocolSession.LocalEndPoint.Port);
						if (this.sslPorts.Contains(item))
						{
							this.sslPorts.Add(protocolSession.LocalEndPoint);
							flag2 = true;
						}
						else if (this.nonSslPorts.Contains(item))
						{
							this.nonSslPorts.Add(protocolSession.LocalEndPoint);
						}
					}
				}
				else
				{
					flag2 = true;
				}
				if (flag2 && this.Certificate == null)
				{
					ProtocolBaseServices.ServerTracer.TraceWarning(0L, "Unable to start SSL connection without a certificate.");
					ProtocolBaseServices.LogEvent(this.server.SslConnectionNotStartedEventTuple, this.ToString(), new string[0]);
					protocolSession.BeginShutdown(this.server.NoSslCertificateError);
					this.Connections_Rejected.Increment();
				}
				else
				{
					protocolSession.StartSession(flag2);
				}
			}
			finally
			{
				ProtocolBaseServices.InMemoryTraceOperationCompleted(0L);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009E44 File Offset: 0x00008044
		public void RemoveSession(ProtocolSession protocolSession)
		{
			if (this.stopping)
			{
				return;
			}
			lock (this.sessions)
			{
				if (!this.stopping)
				{
					this.sessions.Remove(protocolSession);
					this.Connections_Current.Decrement();
					if (protocolSession.IsTls)
					{
						this.SSLConnections_Current.Decrement();
					}
				}
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009EC0 File Offset: 0x000080C0
		public void SessionStopped()
		{
			if (Interlocked.Decrement(ref this.sessionsToStop) == 0)
			{
				this.allStopped.Set();
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009EDC File Offset: 0x000080DC
		public int DisposeExpiredSessions(string connectionId)
		{
			ProtocolBaseServices.ServerTracer.TraceDebug<string>(0L, "DisposeExpiredSessions for {0}", connectionId);
			int num = 0;
			if (this.stopping)
			{
				return 0;
			}
			List<ProtocolSession> list = new List<ProtocolSession>();
			lock (this.sessions)
			{
				if (this.stopping)
				{
					return 0;
				}
				ExDateTime t = ExDateTime.UtcNow.AddSeconds((double)(-(double)this.Server.ConnectionTimeout));
				foreach (ProtocolSession protocolSession in this.sessions)
				{
					if (!protocolSession.Disposed)
					{
						if (Monitor.TryEnter(protocolSession))
						{
							try
							{
								if (protocolSession.Disposed)
								{
									continue;
								}
								if (protocolSession.ResponseFactory != null && protocolSession.ResponseFactory.ProtocolUser != null && string.Equals(protocolSession.ResponseFactory.ProtocolUser.ConnectionIdentity, connectionId))
								{
									if (protocolSession.Disconnected || protocolSession.Connection == null || protocolSession.LastActivityTime < t)
									{
										ProtocolBaseServices.SessionTracer.TraceDebug<ProtocolSession>(protocolSession.SessionId, "Expired session found: {0}", protocolSession);
										protocolSession.ResponseFactory.ProtocolUser.ConnectionIdentity = null;
										list.Add(protocolSession);
									}
									else
									{
										num++;
									}
								}
								continue;
							}
							finally
							{
								Monitor.Exit(protocolSession);
							}
						}
						ProtocolBaseServices.SessionTracer.TraceDebug(protocolSession.SessionId, "session already locked by another thread.");
					}
				}
			}
			foreach (ProtocolSession protocolSession2 in list)
			{
				protocolSession2.Dispose();
			}
			return num;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000A0EC File Offset: 0x000082EC
		public void Stop()
		{
			lock (this.sessions)
			{
				this.stopping = true;
			}
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Stop all sessions.");
			BaseSession.ConnectionShutdownDelegate connectionShutdown = new BaseSession.ConnectionShutdownDelegate(this.SessionStopped);
			this.allStopped = new AutoResetEvent(this.sessions.Count == 0);
			this.sessionsToStop = this.sessions.Count;
			foreach (ProtocolSession protocolSession in this.sessions)
			{
				if (!protocolSession.Disposed)
				{
					lock (protocolSession)
					{
						if (!protocolSession.Disposed)
						{
							ProtocolBaseServices.SessionTracer.TraceDebug(protocolSession.SessionId, "Try to stop the session.");
							protocolSession.BeginShutdown(this.server.ServerShutdownMessage, connectionShutdown);
						}
					}
				}
			}
			this.allStopped.WaitOne(30000, true);
			this.certificateCache.Close();
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Close performance counters");
			this.ClosePerfCounters();
		}

		// Token: 0x0600029E RID: 670
		public abstract ProtocolSession CreateNewSession(NetworkConnection connection);

		// Token: 0x0600029F RID: 671
		public abstract void ClosePerfCounters();

		// Token: 0x060002A0 RID: 672 RVA: 0x0000A248 File Offset: 0x00008448
		void IDisposable.Dispose()
		{
			this.ClosePerfCounters();
			if (this.allStopped != null)
			{
				this.allStopped.Close();
				this.allStopped = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000A270 File Offset: 0x00008470
		protected override string UsageText
		{
			get
			{
				return "Returns active session and user info.";
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000A277 File Offset: 0x00008477
		protected override string UsageSample
		{
			get
			{
				return "Get-ExchangeDiagnosticInfo -Server <TargetServer> -Process <ProcessName> -Component SessionsInfo [-Argument Detailed]";
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000A27E File Offset: 0x0000847E
		protected override string ComponentName
		{
			get
			{
				return "SessionsInfo";
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000A288 File Offset: 0x00008488
		internal override SessionsInfo GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			SessionsInfo sessionsInfo = new SessionsInfo();
			sessionsInfo.Count = this.sessions.Count;
			sessionsInfo.Stopping = this.stopping;
			if (string.Equals("Detailed", argument.Argument, StringComparison.InvariantCultureIgnoreCase))
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>(ResponseFactory.ConnectionsPerUser.Counters.Count);
				lock (this.sessions)
				{
					sessionsInfo.Count = this.sessions.Count;
					sessionsInfo.Stopping = this.stopping;
					sessionsInfo.Sessions = new SessionInfo[sessionsInfo.Count];
					int num = 0;
					foreach (ProtocolSession protocolSession in this.sessions)
					{
						try
						{
							SessionInfo sessionInfo = new SessionInfo(protocolSession);
							sessionsInfo.Sessions[num++] = sessionInfo;
							if (!string.IsNullOrEmpty(sessionInfo.ConnectionId))
							{
								dictionary[sessionInfo.ConnectionId] = sessionInfo.User;
							}
						}
						catch (Exception arg)
						{
							ProtocolBaseServices.ServerTracer.TraceDebug<ProtocolSession, Exception>(0L, "Exception while creating SessionInfo for session {0}\r\n{1}", protocolSession, arg);
						}
					}
				}
				lock (ResponseFactory.ConnectionsPerUser)
				{
					sessionsInfo.Users = new UserInfo[ResponseFactory.ConnectionsPerUser.Counters.Count];
					int num2 = 0;
					foreach (string text in ResponseFactory.ConnectionsPerUser.Counters.Keys)
					{
						UserInfo userInfo = new UserInfo();
						string name;
						if (dictionary.TryGetValue(text, out name))
						{
							userInfo.Name = name;
						}
						else
						{
							userInfo.Name = text;
						}
						userInfo.SessionCount = ResponseFactory.ConnectionsPerUser.Counters[text];
						sessionsInfo.Users[num2++] = userInfo;
					}
				}
			}
			return sessionsInfo;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000A4C0 File Offset: 0x000086C0
		private static void ShutdownSocket(Socket socket)
		{
			try
			{
				socket.Shutdown(SocketShutdown.Both);
				socket.Close();
			}
			catch (SocketException arg)
			{
				ProtocolBaseServices.ServerTracer.TraceDebug<SocketException>(0L, "Exception while shutting down socket: {0}", arg);
			}
		}

		// Token: 0x04000161 RID: 353
		private ProtocolBaseServices server;

		// Token: 0x04000162 RID: 354
		private string banner;

		// Token: 0x04000163 RID: 355
		private string[] sslCertificateName;

		// Token: 0x04000164 RID: 356
		private TlsCertificateCache certificateCache = new TlsCertificateCache();

		// Token: 0x04000165 RID: 357
		private List<IPEndPoint> sslPorts;

		// Token: 0x04000166 RID: 358
		private List<IPEndPoint> nonSslPorts;

		// Token: 0x04000167 RID: 359
		private bool stopping;

		// Token: 0x04000168 RID: 360
		private List<ProtocolSession> sessions;

		// Token: 0x04000169 RID: 361
		private AutoResetEvent allStopped;

		// Token: 0x0400016A RID: 362
		private int sessionsToStop;
	}
}
