using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.HA.SupportApi;

namespace Microsoft.Exchange.HA.Services
{
	// Token: 0x02000333 RID: 819
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServerLocatorManager : IServiceComponent
	{
		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x0009AAA4 File Offset: 0x00098CA4
		public static ServerLocatorManager Instance
		{
			get
			{
				if (ServerLocatorManager.instance == null)
				{
					lock (ServerLocatorManager.syncRoot)
					{
						if (ServerLocatorManager.instance == null)
						{
							ServerLocatorManager.instance = new ServerLocatorManager();
						}
					}
				}
				return ServerLocatorManager.instance;
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0009AB04 File Offset: 0x00098D04
		private static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ServerLocatorServiceTracer;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x0009AB0B File Offset: 0x00098D0B
		public string Name
		{
			get
			{
				return "ServerLocator";
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0009AB12 File Offset: 0x00098D12
		public FacilityEnum Facility
		{
			get
			{
				return FacilityEnum.ServerLocatorService;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x0009AB16 File Offset: 0x00098D16
		public bool IsCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0009AB19 File Offset: 0x00098D19
		public bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x0009AB1C File Offset: 0x00098D1C
		public bool IsRetriableOnError
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0009AB1F File Offset: 0x00098D1F
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public void Invoke(Action toInvoke)
		{
			toInvoke();
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x0009AB27 File Offset: 0x00098D27
		public bool IsRunning
		{
			get
			{
				return this.isRunning;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0009AB2F File Offset: 0x00098D2F
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x0009AB37 File Offset: 0x00098D37
		public IMonitoringADConfigProvider ADConfigProvider { get; private set; }

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0009AB40 File Offset: 0x00098D40
		// (set) Token: 0x06002161 RID: 8545 RVA: 0x0009AB48 File Offset: 0x00098D48
		public ICopyStatusClientLookup CopyStatusLookup { get; private set; }

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0009AB51 File Offset: 0x00098D51
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x0009AB59 File Offset: 0x00098D59
		public ServerLocatorPerfmonCounters Counters { get; private set; }

		// Token: 0x06002164 RID: 8548 RVA: 0x0009AB62 File Offset: 0x00098D62
		private ServerLocatorManager()
		{
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0009AB6A File Offset: 0x00098D6A
		private static ServerLocatorPerfmonCounters GetPerfCounters()
		{
			return new ServerLocatorPerfmonCounters();
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0009AB74 File Offset: 0x00098D74
		public bool Start()
		{
			Exception ex;
			return this.Start(out ex);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0009AC34 File Offset: 0x00098E34
		public bool Start(out Exception ex)
		{
			ex = null;
			if (this.isRunning)
			{
				ServerLocatorManager.Tracer.TraceDebug(0L, "Server Locator Manager is already started.");
				return true;
			}
			bool flag = false;
			bool result;
			try
			{
				object obj;
				Monitor.Enter(obj = ServerLocatorManager.syncRoot, ref flag);
				if (this.isRunning)
				{
					ServerLocatorManager.Tracer.TraceDebug(0L, "Server Locator Manager is already started.");
					result = true;
				}
				else
				{
					Exception serviceHostException = null;
					try
					{
						if (this.m_restartManager == null)
						{
							ServerLocatorManager.Tracer.TraceDebug(0L, "Creating restart manager that will constantly monitor service.");
							this.m_restartManager = new ServerLocatorManager.RestartServerLocator(this, ServerLocatorManager.restartInterval);
							this.m_restartManager.Start();
						}
						ServerLocatorManager.Tracer.TraceDebug(0L, "Creating caching active manager client for Server Locator Service.");
						this.ADConfigProvider = Dependencies.MonitoringADConfigProvider;
						this.CopyStatusLookup = Dependencies.MonitoringCopyStatusClientLookup;
						this.Counters = ServerLocatorManager.GetPerfCounters();
						ServerLocatorManager.Tracer.TraceDebug<string>(0L, "Starting Server Locator Service on {0}.", ServerLocatorManager.baseAddress.AbsoluteUri);
						this.serviceHost = new ServiceHost(typeof(ServerLocator), new Uri[]
						{
							ServerLocatorManager.baseAddress
						});
						this.serviceHost.Faulted += this.RestartServiceHost;
						NetTcpBinding netTcpBinding = new NetTcpBinding();
						netTcpBinding.MaxBufferPoolSize = 16777216L;
						netTcpBinding.MaxBufferSize = 16777216;
						netTcpBinding.MaxConnections = 200;
						netTcpBinding.MaxReceivedMessageSize = 16777216L;
						netTcpBinding.ReaderQuotas.MaxDepth = 128;
						netTcpBinding.ReaderQuotas.MaxArrayLength = int.MaxValue;
						netTcpBinding.ReaderQuotas.MaxBytesPerRead = int.MaxValue;
						netTcpBinding.ReaderQuotas.MaxNameTableCharCount = int.MaxValue;
						netTcpBinding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
						this.serviceHost.AddServiceEndpoint(typeof(IServerLocator), netTcpBinding, "ServerLocator");
						ServiceThrottlingBehavior serviceThrottlingBehavior = new ServiceThrottlingBehavior();
						serviceThrottlingBehavior.MaxConcurrentCalls = RegistryParameters.WcfMaxConcurrentCalls;
						serviceThrottlingBehavior.MaxConcurrentSessions = RegistryParameters.WcfMaxConcurrentSessions;
						serviceThrottlingBehavior.MaxConcurrentInstances = RegistryParameters.WcfMaxConcurrentInstances;
						ServiceThrottlingBehavior serviceThrottlingBehavior2 = this.serviceHost.Description.Behaviors.Find<ServiceThrottlingBehavior>();
						if (serviceThrottlingBehavior2 == null)
						{
							this.serviceHost.Description.Behaviors.Add(serviceThrottlingBehavior);
						}
						else
						{
							this.serviceHost.Description.Behaviors.Remove(serviceThrottlingBehavior2);
							this.serviceHost.Description.Behaviors.Add(serviceThrottlingBehavior);
						}
						ServiceMetadataBehavior item = new ServiceMetadataBehavior();
						this.serviceHost.Description.Behaviors.Add(item);
						if (RegistryParameters.WcfEnableMexEndpoint)
						{
							ServerLocatorManager.Tracer.TraceDebug(0L, "Creating Mex binding.");
							ExAssert.RetailAssert(RegistryParameters.HighAvailabilityWebServiceMexPort != RegistryParameters.HighAvailabilityWebServicePort, "Metadata Exchange port should be different from Server Locator web service port.");
							Binding binding = MetadataExchangeBindings.CreateMexTcpBinding();
							this.serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), binding, ServerLocatorManager.baseMetadataExchangeAddress);
						}
						InvokeWithTimeout.Invoke(delegate()
						{
							InvalidOperationException serviceHostException;
							try
							{
								this.serviceHost.Open();
							}
							catch (InvalidOperationException serviceHostException)
							{
								serviceHostException = serviceHostException;
							}
							catch (CommunicationException serviceHostException2)
							{
								serviceHostException = serviceHostException2;
							}
							catch (SocketException serviceHostException3)
							{
								serviceHostException = serviceHostException3;
							}
							catch (Win32Exception serviceHostException4)
							{
								serviceHostException = serviceHostException4;
							}
							if (serviceHostException != null)
							{
								ServerLocatorManager.Tracer.TraceError<string>(0L, "InokeWithTimeout() failed to start Server Locator Service communication channel. Error: {0}", serviceHostException.Message);
							}
						}, ServerLocatorManager.startTimeout);
						if (serviceHostException == null)
						{
							this.isRunning = true;
							ServerLocatorManager.Tracer.TraceDebug(0L, "Server Locator Service started.");
							ReplayEventLogConstants.Tuple_ServerLocatorServiceStarted.LogEvent(null, new object[]
							{
								ServerLocatorManager.baseAddress.AbsoluteUri
							});
							return true;
						}
					}
					catch (TimeoutException ex2)
					{
						ReplayEventLogConstants.Tuple_ServerLocatorServiceStartTimeout.LogEvent(null, new object[]
						{
							ServerLocatorManager.baseAddress.AbsoluteUri,
							this.GenerateDiagnosticException("Timeout during starting server locator WCF service from ServerLocatorManager.Start()", ex2).Message
						});
						ex = ex2;
					}
					if (serviceHostException != null)
					{
						ex = serviceHostException;
					}
					if (ex != null)
					{
						ServerLocatorManager.Tracer.TraceError<string>(0L, "Start() failed to start Server Locator Service communication channel. Error: {0}", ex.Message);
					}
					if (this.serviceHost != null)
					{
						ServerLocatorManager.Tracer.TraceDebug(0L, "Aborting server locator service communication channel from Start().");
						this.serviceHost.Abort();
					}
					ServerLocatorManager.Tracer.TraceError<string, string>(0L, "Failed to start Server Locator Service on {0}. Error: {1}", ServerLocatorManager.baseAddress.AbsoluteUri, ex.Message);
					ReplayEventLogConstants.Tuple_ServerLocatorServiceFailedToStart.LogEvent(null, new object[]
					{
						ServerLocatorManager.baseAddress.AbsoluteUri,
						ex.Message
					});
					this.m_restartManager.ChangeTimer(ServerLocatorManager.startNowInterval, ServerLocatorManager.restartInterval);
					result = false;
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
			return result;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0009B0B0 File Offset: 0x000992B0
		internal bool InitiateRestartSequence()
		{
			if (this.isRestartRequired)
			{
				this.isRestartRequired = false;
				ServerLocatorManager.Tracer.TraceDebug(0L, "Restart will be attmpted now.");
				ReplayEventLogConstants.Tuple_ServerLocatorServiceRestartScheduled.LogEvent(this.Name, new object[]
				{
					TimeSpan.Zero.TotalSeconds
				});
				return true;
			}
			return false;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0009B110 File Offset: 0x00099310
		internal void RestartServiceHost(object sender, EventArgs e)
		{
			if (sender is SupportApiService)
			{
				ServerLocatorManager.Tracer.TraceDebug(0L, "Server Locator restart called from support api service.");
				ReplayEventLogConstants.Tuple_ServerLocatorServiceCommunicationChannelFaulted.LogEvent(null, new object[]
				{
					"Triggered by Support API."
				});
			}
			else
			{
				ServerLocatorManager.Tracer.TraceError<CommunicationState>(0L, "Service Host communication channel is {0}. Because of this error we need to stop and re-start service host.", this.serviceHost.State);
				ReplayEventLogConstants.Tuple_ServerLocatorServiceCommunicationChannelFaulted.LogEvent(null, new object[]
				{
					this.serviceHost.State,
					this.GenerateDiagnosticException("Service host communication channel faulted event received").Message
				});
			}
			this.isRestartRequired = true;
			if (this.m_restartManager != null)
			{
				bool flag = false;
				try
				{
					Monitor.TryEnter(ServerLocatorManager.syncRoot, 1000, ref flag);
					if (flag)
					{
						if (this.m_restartManager != null)
						{
							ServerLocatorManager.Tracer.TraceDebug(0L, "Pinged server locator restart manager to restart now.");
							this.m_restartManager.ChangeTimer(ServerLocatorManager.startNowInterval, ServerLocatorManager.restartInterval);
						}
						else
						{
							ServerLocatorManager.Tracer.TraceError(0L, "Cannot ping server locator restart manager because it is not available.");
						}
					}
					else
					{
						ServerLocatorManager.Tracer.TraceError(0L, "Cannot get lock to ping server locator restart manager.");
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(ServerLocatorManager.syncRoot);
					}
				}
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0009B244 File Offset: 0x00099444
		public void Stop()
		{
			this.Stop(false);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0009B25C File Offset: 0x0009945C
		public void Stop(bool aborting)
		{
			lock (ServerLocatorManager.syncRoot)
			{
				ServerLocatorManager.Tracer.TraceDebug<bool>(0L, "Server Locator Service Stop({0}).", aborting);
				if (this.serviceHost != null)
				{
					ServerLocatorManager.Tracer.TraceDebug<CommunicationState, bool>(0L, "Server Locator Service communication channel is {0}. Aborting flag is: {1}.", this.serviceHost.State, aborting);
					if (aborting)
					{
						ServerLocatorManager.Tracer.TraceDebug(0L, "Aborting Server Locator Service communication channel.");
						this.serviceHost.Abort();
					}
					else
					{
						try
						{
							ServerLocatorManager.Tracer.TraceDebug(0L, "Closing Server Locator Service communication channel.");
							InvokeWithTimeout.Invoke(delegate()
							{
								this.serviceHost.Close();
							}, ServerLocatorManager.startTimeout);
						}
						catch (CommunicationException ex)
						{
							ServerLocatorManager.Tracer.TraceError<string>(0L, "Unable to close Server Locator Service communication channel gracefully due to: {0}", ex.Message);
							this.serviceHost.Abort();
						}
						catch (TimeoutException ex2)
						{
							ServerLocatorManager.Tracer.TraceError<string>(0L, "Unable to close Server Locator Service commuication channel gracefully due to: {0}", ex2.Message);
							this.serviceHost.Abort();
						}
					}
				}
				if (!aborting && this.m_restartManager != null)
				{
					ServerLocatorManager.Tracer.TraceDebug(0L, "Stopping restart manager.");
					this.m_restartManager.Stop();
					this.m_restartManager = null;
				}
				ServerLocatorManager.Tracer.TraceDebug(0L, "Server Locator Service stopped.");
				ReplayEventLogConstants.Tuple_ServerLocatorServiceStopped.LogEvent(null, new object[0]);
				this.isRunning = false;
			}
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0009B400 File Offset: 0x00099600
		private Exception GenerateDiagnosticException(string errorMessage)
		{
			return this.GenerateDiagnosticException(errorMessage, null);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0009B40C File Offset: 0x0009960C
		private Exception GenerateDiagnosticException(string errorMessage, Exception inner)
		{
			Exception ex = null;
			NativeMethods.SocketData openSocketByPort = NativeMethods.GetOpenSocketByPort(RegistryParameters.HighAvailabilityWebServicePort);
			if (openSocketByPort != null)
			{
				try
				{
					try
					{
						using (Process processById = Process.GetProcessById(openSocketByPort.OwnerPid))
						{
							using (Process currentProcess = Process.GetCurrentProcess())
							{
								ReplayEventLogConstants.Tuple_ServerLocatorServiceAnotherProcessUsingPort.LogEvent(this.Name, new object[]
								{
									RegistryParameters.HighAvailabilityWebServicePort,
									processById.ProcessName,
									openSocketByPort.OwnerPid,
									currentProcess.ProcessName,
									currentProcess.Id
								});
								ex = new Exception(string.Format("Server locator manager error: {0}. Process using port is: '{1}' (pid: {2}). Current process: '{3}' (pid: {4})", new object[]
								{
									errorMessage,
									processById.ProcessName,
									openSocketByPort.OwnerPid,
									currentProcess.ProcessName,
									currentProcess.Id
								}), inner);
							}
						}
					}
					catch (ArgumentException)
					{
					}
					catch (InvalidOperationException)
					{
					}
					catch (Win32Exception)
					{
					}
					return ex;
				}
				finally
				{
					if (ex == null)
					{
						try
						{
							using (Process currentProcess2 = Process.GetCurrentProcess())
							{
								ex = new Exception(string.Format("Server locator manager error: {0}. Process using port is: '<unknown>' (pid: {1}). Current process: {2} (pid: {3})", new object[]
								{
									errorMessage,
									openSocketByPort.OwnerPid,
									currentProcess2.ProcessName,
									currentProcess2.Id
								}), inner);
							}
						}
						catch (Win32Exception)
						{
							ex = new Exception(string.Format("Server locator manager error: {0}. Process using port is: '<unknown>' (pid: {1}).", errorMessage, openSocketByPort.OwnerPid), inner);
						}
					}
				}
			}
			try
			{
				using (Process currentProcess3 = Process.GetCurrentProcess())
				{
					ex = new Exception(string.Format("Server locator manager error: {0}. No other process using the port was found. Current process: {1} (pid: {2})", errorMessage, currentProcess3.ProcessName, currentProcess3.Id), inner);
				}
			}
			catch (Win32Exception)
			{
				ex = new Exception(string.Format("Server locator manager error: {0}. No other process using the port was found.", errorMessage), inner);
			}
			return ex;
		}

		// Token: 0x04000DA0 RID: 3488
		private static volatile ServerLocatorManager instance;

		// Token: 0x04000DA1 RID: 3489
		private static object syncRoot = new object();

		// Token: 0x04000DA2 RID: 3490
		private static Uri baseAddress = new Uri(string.Format("net.tcp://{0}:{1}/Exchange.HighAvailability", AmServerName.LocalComputerName.Fqdn, RegistryParameters.HighAvailabilityWebServicePort));

		// Token: 0x04000DA3 RID: 3491
		private static Uri baseMetadataExchangeAddress = new Uri(string.Format("net.tcp://{0}:{1}/Exchange.HighAvailability/mex", AmServerName.LocalComputerName.Fqdn, RegistryParameters.HighAvailabilityWebServiceMexPort));

		// Token: 0x04000DA4 RID: 3492
		private static TimeSpan restartInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000DA5 RID: 3493
		private static TimeSpan startNowInterval = TimeSpan.Zero;

		// Token: 0x04000DA6 RID: 3494
		private static TimeSpan startTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000DA7 RID: 3495
		private ServiceHost serviceHost;

		// Token: 0x04000DA8 RID: 3496
		private bool isRestartRequired;

		// Token: 0x04000DA9 RID: 3497
		private bool isRunning;

		// Token: 0x04000DAA RID: 3498
		private ServerLocatorManager.RestartServerLocator m_restartManager;

		// Token: 0x02000334 RID: 820
		[ClassAccessLevel(AccessLevel.Implementation)]
		private class RestartServerLocator : TimerComponent
		{
			// Token: 0x06002170 RID: 8560 RVA: 0x0009B6FF File Offset: 0x000998FF
			public RestartServerLocator(ServerLocatorManager serverLocatorManager, TimeSpan periodicStartInterval) : base(periodicStartInterval, periodicStartInterval, "RestartServerLocator")
			{
				this.m_serverLocatorManager = serverLocatorManager;
			}

			// Token: 0x06002171 RID: 8561 RVA: 0x0009B715 File Offset: 0x00099915
			protected override void TimerCallbackInternal()
			{
				if (this.m_serverLocatorManager.InitiateRestartSequence())
				{
					this.m_serverLocatorManager.Stop(true);
					this.m_serverLocatorManager.Start();
				}
			}

			// Token: 0x04000DAE RID: 3502
			private ServerLocatorManager m_serverLocatorManager;
		}
	}
}
