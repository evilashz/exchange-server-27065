using System;
using System.Net.Security;
using System.ServiceModel;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Win32;
using www.outlook.com.highavailability.ServerLocator.v1;

namespace Microsoft.Exchange.Data.Storage.ServerLocator
{
	// Token: 0x02000D42 RID: 3394
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServerLocatorServiceClient : IDisposeTrackable, IDisposable
	{
		// Token: 0x17001F87 RID: 8071
		// (get) Token: 0x060075A4 RID: 30116 RVA: 0x00208B78 File Offset: 0x00206D78
		private static int HighAvailabilityWebServicePort
		{
			get
			{
				if (ServerLocatorServiceClient.highAvailabilityWebServicePort == 0)
				{
					lock (ServerLocatorServiceClient.syncRoot)
					{
						if (ServerLocatorServiceClient.highAvailabilityWebServicePort == 0)
						{
							ServerLocatorServiceClient.Tracer.TraceDebug(0L, "Using registry to get HighAvailabilityWebServicePort");
							using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters"))
							{
								if (registryKey != null)
								{
									ServerLocatorServiceClient.highAvailabilityWebServicePort = (int)registryKey.GetValue("HighAvailabilityWebServicePort", 64337);
									ServerLocatorServiceClient.Tracer.TraceDebug<string, int>(0L, "Registry key {0} found. Value: {1}.", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", ServerLocatorServiceClient.highAvailabilityWebServicePort);
								}
								else
								{
									ServerLocatorServiceClient.highAvailabilityWebServicePort = 64337;
									ServerLocatorServiceClient.Tracer.TraceDebug<string, int>(0L, "Registry key {0} not found. Using default value: {1}.", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", 64337);
								}
							}
						}
					}
				}
				return ServerLocatorServiceClient.highAvailabilityWebServicePort;
			}
		}

		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x060075A5 RID: 30117 RVA: 0x00208C68 File Offset: 0x00206E68
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ServerLocatorServiceClientTracer;
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x060075A6 RID: 30118 RVA: 0x00208C70 File Offset: 0x00206E70
		public bool IsUsable
		{
			get
			{
				bool result = false;
				if (this.m_client != null && this.m_client.State == CommunicationState.Opened)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x060075A7 RID: 30119 RVA: 0x00208C98 File Offset: 0x00206E98
		public static ServerLocatorServiceClient Create(string serverName)
		{
			return new ServerLocatorServiceClient(serverName);
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x00208CA0 File Offset: 0x00206EA0
		public static ServerLocatorServiceClient Create(string serverName, TimeSpan closeTimeout, TimeSpan openTimeout, TimeSpan receiveTimeout, TimeSpan sendTimeout)
		{
			return new ServerLocatorServiceClient(serverName, closeTimeout, openTimeout, receiveTimeout, sendTimeout);
		}

		// Token: 0x060075A9 RID: 30121 RVA: 0x00208CB0 File Offset: 0x00206EB0
		private ServerLocatorServiceClient(string serverName, TimeSpan closeTimeout, TimeSpan openTimeout, TimeSpan receiveTimeout, TimeSpan sendTimeout)
		{
			ServerLocatorServiceClient.Tracer.TraceDebug<string>(0L, "Constructing ServerLocatorClient for Server Locator Service on {0}.", serverName);
			this.m_disposeTracker = this.GetDisposeTracker();
			NetTcpBinding netTcpBinding = new NetTcpBinding();
			netTcpBinding.Name = "NetTcpBinding_ServerLocator";
			netTcpBinding.CloseTimeout = closeTimeout;
			netTcpBinding.OpenTimeout = openTimeout;
			netTcpBinding.ReceiveTimeout = receiveTimeout;
			netTcpBinding.SendTimeout = sendTimeout;
			netTcpBinding.TransactionFlow = false;
			netTcpBinding.TransferMode = TransferMode.Buffered;
			netTcpBinding.TransactionProtocol = TransactionProtocol.OleTransactions;
			netTcpBinding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
			netTcpBinding.ListenBacklog = 10;
			netTcpBinding.MaxBufferPoolSize = 16777216L;
			netTcpBinding.MaxBufferSize = 16777216;
			netTcpBinding.MaxReceivedMessageSize = 16777216L;
			netTcpBinding.MaxConnections = 10;
			netTcpBinding.ReaderQuotas.MaxDepth = 32;
			netTcpBinding.ReaderQuotas.MaxStringContentLength = 8192;
			netTcpBinding.ReaderQuotas.MaxArrayLength = 16384;
			netTcpBinding.ReaderQuotas.MaxBytesPerRead = 4096;
			netTcpBinding.ReaderQuotas.MaxNameTableCharCount = 16384;
			netTcpBinding.ReliableSession.Ordered = true;
			netTcpBinding.ReliableSession.InactivityTimeout = TimeSpan.FromMinutes(10.0);
			netTcpBinding.ReliableSession.Enabled = false;
			netTcpBinding.Security.Mode = SecurityMode.Transport;
			netTcpBinding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows;
			netTcpBinding.Security.Transport.ProtectionLevel = ProtectionLevel.EncryptAndSign;
			netTcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
			EndpointAddress endpointAddress = new EndpointAddress(string.Format("net.tcp://{0}:{1}/Exchange.HighAvailability/ServerLocator", serverName, ServerLocatorServiceClient.HighAvailabilityWebServicePort));
			try
			{
				this.m_client = new ServerLocatorClient(netTcpBinding, endpointAddress);
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			ServerLocatorServiceClient.Tracer.TraceDebug<string>(0L, "ServerLocatorClient for Server Locator Service on  {0} succesfully created.", endpointAddress.Uri.AbsoluteUri);
		}

		// Token: 0x060075AA RID: 30122 RVA: 0x00208E80 File Offset: 0x00207080
		private ServerLocatorServiceClient(string serverName) : this(serverName, ServerLocatorServiceClient.defaultCloseTimeout, ServerLocatorServiceClient.defaultOpenTimeout, ServerLocatorServiceClient.defaultReceiveTimeout, ServerLocatorServiceClient.defaultSendTimeout)
		{
		}

		// Token: 0x060075AB RID: 30123 RVA: 0x00208EA0 File Offset: 0x002070A0
		~ServerLocatorServiceClient()
		{
			this.Dispose(false);
		}

		// Token: 0x060075AC RID: 30124 RVA: 0x00208ED0 File Offset: 0x002070D0
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ServerLocatorServiceClient>(this);
		}

		// Token: 0x060075AD RID: 30125 RVA: 0x00208ED8 File Offset: 0x002070D8
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x060075AE RID: 30126 RVA: 0x00208EED File Offset: 0x002070ED
		public void Dispose()
		{
			if (!this.m_fDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060075AF RID: 30127 RVA: 0x00208F04 File Offset: 0x00207104
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_fDisposed)
				{
					if (disposing)
					{
						if (this.m_client != null)
						{
							if (this.m_client.State != CommunicationState.Opened)
							{
								this.m_client.Abort();
							}
							else
							{
								try
								{
									this.m_client.Close();
								}
								catch (CommunicationException)
								{
									this.m_client.Abort();
								}
								catch (TimeoutException)
								{
									this.m_client.Abort();
								}
							}
							this.m_client = null;
						}
						if (this.m_disposeTracker != null)
						{
							this.m_disposeTracker.Dispose();
						}
						this.m_disposeTracker = null;
					}
					this.m_fDisposed = true;
				}
			}
		}

		// Token: 0x060075B0 RID: 30128 RVA: 0x00208FD0 File Offset: 0x002071D0
		public IAsyncResult BeginGetServerForDatabase(Guid databaseGuid, AsyncCallback callback, object asyncState)
		{
			IAsyncResult result;
			try
			{
				result = this.m_client.BeginGetServerForDatabase(new DatabaseServerInformation
				{
					DatabaseGuid = databaseGuid,
					RequestSentUtc = DateTime.UtcNow
				}, callback, asyncState);
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return result;
		}

		// Token: 0x060075B1 RID: 30129 RVA: 0x00209020 File Offset: 0x00207220
		public DatabaseServerInformation EndGetServerForDatabase(IAsyncResult result)
		{
			DatabaseServerInformation result2;
			try
			{
				result2 = this.m_client.EndGetServerForDatabase(result);
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return result2;
		}

		// Token: 0x060075B2 RID: 30130 RVA: 0x00209058 File Offset: 0x00207258
		public DatabaseServerInformation GetServerForDatabase(Guid databaseGuid)
		{
			DatabaseServerInformation serverForDatabase;
			try
			{
				serverForDatabase = this.m_client.GetServerForDatabase(new DatabaseServerInformation
				{
					DatabaseGuid = databaseGuid,
					RequestSentUtc = DateTime.UtcNow
				});
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return serverForDatabase;
		}

		// Token: 0x060075B3 RID: 30131 RVA: 0x002090A8 File Offset: 0x002072A8
		public IAsyncResult BeginGetActiveCopiesForDatabaseAvailabilityGroup(AsyncCallback callback, object asyncState)
		{
			IAsyncResult result;
			try
			{
				result = this.m_client.BeginGetActiveCopiesForDatabaseAvailabilityGroup(callback, asyncState);
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return result;
		}

		// Token: 0x060075B4 RID: 30132 RVA: 0x002090E0 File Offset: 0x002072E0
		public DatabaseServerInformation[] EndGetActiveCopiesForDatabaseAvailabilityGroup(IAsyncResult result)
		{
			DatabaseServerInformation[] result2;
			try
			{
				result2 = this.m_client.EndGetActiveCopiesForDatabaseAvailabilityGroup(result);
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return result2;
		}

		// Token: 0x060075B5 RID: 30133 RVA: 0x00209118 File Offset: 0x00207318
		public DatabaseServerInformation[] GetActiveCopiesForDatabaseAvailabilityGroup()
		{
			DatabaseServerInformation[] activeCopiesForDatabaseAvailabilityGroup;
			try
			{
				activeCopiesForDatabaseAvailabilityGroup = this.m_client.GetActiveCopiesForDatabaseAvailabilityGroup();
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return activeCopiesForDatabaseAvailabilityGroup;
		}

		// Token: 0x060075B6 RID: 30134 RVA: 0x00209150 File Offset: 0x00207350
		public IAsyncResult BeginGetActiveCopiesForDatabaseAvailabilityGroupExtended(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters, AsyncCallback callback, object asyncState)
		{
			IAsyncResult result;
			try
			{
				result = this.m_client.BeginGetActiveCopiesForDatabaseAvailabilityGroupExtended(parameters, callback, asyncState);
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return result;
		}

		// Token: 0x060075B7 RID: 30135 RVA: 0x00209188 File Offset: 0x00207388
		public DatabaseServerInformation[] EndGetActiveCopiesForDatabaseAvailabilityGroupExtended(IAsyncResult result)
		{
			DatabaseServerInformation[] result2;
			try
			{
				result2 = this.m_client.EndGetActiveCopiesForDatabaseAvailabilityGroupExtended(result);
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return result2;
		}

		// Token: 0x060075B8 RID: 30136 RVA: 0x002091C0 File Offset: 0x002073C0
		public DatabaseServerInformation[] GetActiveCopiesForDatabaseAvailabilityGroupExtended(GetActiveCopiesForDatabaseAvailabilityGroupParameters parameters)
		{
			DatabaseServerInformation[] activeCopiesForDatabaseAvailabilityGroupExtended;
			try
			{
				activeCopiesForDatabaseAvailabilityGroupExtended = this.m_client.GetActiveCopiesForDatabaseAvailabilityGroupExtended(parameters);
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return activeCopiesForDatabaseAvailabilityGroupExtended;
		}

		// Token: 0x060075B9 RID: 30137 RVA: 0x002091F8 File Offset: 0x002073F8
		public ServiceVersion GetVersion()
		{
			ServiceVersion version;
			try
			{
				version = this.m_client.GetVersion();
			}
			catch (Exception ex)
			{
				throw this.HandleAndTranslateKnownException(ex);
			}
			return version;
		}

		// Token: 0x060075BA RID: 30138 RVA: 0x00209230 File Offset: 0x00207430
		private Exception HandleAndTranslateKnownException(Exception ex)
		{
			if (ex is FaultException<DatabaseServerInformationFault>)
			{
				if (((FaultException<DatabaseServerInformationFault>)ex).Detail.ErrorCode == DatabaseServerInformationType.PermanentError)
				{
					return new ServerLocatorClientException(ServerStrings.ServerLocatorServicePermanentFault, ex);
				}
				return new ServerLocatorClientTransientException(ServerStrings.ServerLocatorServiceTransientFault, ex);
			}
			else
			{
				if (ex is TimeoutException)
				{
					this.m_client.Abort();
					return new ServerLocatorClientTransientException(ServerStrings.ServerLocatorClientWCFCallTimeout, ex);
				}
				if (ex is CommunicationException)
				{
					this.m_client.Abort();
					return new ServerLocatorClientTransientException(ServerStrings.ServerLocatorClientWCFCallCommunicationError, ex);
				}
				if (ex is EndpointNotFoundException)
				{
					this.m_client.Abort();
					return new ServerLocatorClientTransientException(ServerStrings.ServerLocatorClientEndpointNotFoundException, ex);
				}
				return ex;
			}
		}

		// Token: 0x040051AC RID: 20908
		private const string WcfEndpointFormat = "net.tcp://{0}:{1}/Exchange.HighAvailability/ServerLocator";

		// Token: 0x040051AD RID: 20909
		private const int DefaultHighAvailabilityWebServicePort = 64337;

		// Token: 0x040051AE RID: 20910
		private const string HighAvailabilityWebServicePortKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters";

		// Token: 0x040051AF RID: 20911
		private static TimeSpan defaultCloseTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x040051B0 RID: 20912
		private static TimeSpan defaultOpenTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x040051B1 RID: 20913
		private static TimeSpan defaultReceiveTimeout = TimeSpan.FromMinutes(10.0);

		// Token: 0x040051B2 RID: 20914
		private static TimeSpan defaultSendTimeout = TimeSpan.FromMinutes(1.0);

		// Token: 0x040051B3 RID: 20915
		private static object syncRoot = new object();

		// Token: 0x040051B4 RID: 20916
		private static int highAvailabilityWebServicePort = 0;

		// Token: 0x040051B5 RID: 20917
		private bool m_fDisposed;

		// Token: 0x040051B6 RID: 20918
		private DisposeTracker m_disposeTracker;

		// Token: 0x040051B7 RID: 20919
		private ServerLocatorClient m_client;
	}
}
