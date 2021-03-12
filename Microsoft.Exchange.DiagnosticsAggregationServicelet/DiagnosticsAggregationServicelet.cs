using System;
using System.Globalization;
using System.Net.Security;
using System.ServiceModel;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DiagnosticsAggregation;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Exchange.Servicelets.DiagnosticsAggregation.Messages;
using Microsoft.Exchange.Transport.DiagnosticsAggregationService;

namespace Microsoft.Exchange.Servicelets.DiagnosticsAggregation
{
	// Token: 0x02000008 RID: 8
	public class DiagnosticsAggregationServicelet : Servicelet
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002FCC File Offset: 0x000011CC
		internal static ExEventLog EventLog
		{
			get
			{
				return DiagnosticsAggregationServicelet.eventLog;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002FD3 File Offset: 0x000011D3
		internal static DiagnosticsAggregationLog Log
		{
			get
			{
				return DiagnosticsAggregationServicelet.log;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002FDA File Offset: 0x000011DA
		internal static Server LocalServer
		{
			get
			{
				if (DiagnosticsAggregationServicelet.localServer == null)
				{
					throw new InvalidOperationException("LocalServer cannot be accessed before setting it");
				}
				return DiagnosticsAggregationServicelet.localServer;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002FF3 File Offset: 0x000011F3
		internal static DiagnosticsAggregationServiceletConfig Config
		{
			get
			{
				return DiagnosticsAggregationServicelet.config;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002FFA File Offset: 0x000011FA
		internal static TransportConfigContainer TransportSettings
		{
			get
			{
				if (DiagnosticsAggregationServicelet.transportSettings == null)
				{
					throw new InvalidOperationException("TransportSettings cannot be accessed before initializing it");
				}
				return DiagnosticsAggregationServicelet.transportSettings;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003014 File Offset: 0x00001214
		public override void Work()
		{
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceDebug(0L, "Starting DiagnosticsAggregationServicelet.");
			if (DiagnosticsAggregationServicelet.config == null)
			{
				DiagnosticsAggregationServicelet.config = new DiagnosticsAggregationServiceletConfig();
			}
			if (DiagnosticsAggregationServicelet.config.Enabled)
			{
				this.HostService();
				return;
			}
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceWarning(0L, "DiagnosticsAggregationServicelet is not enabled.");
			DiagnosticsAggregationServicelet.EventLog.LogEvent(MSExchangeDiagnosticsAggregationEventLogConstants.Tuple_DiagnosticsAggregationServiceletIsDisabled, null, new object[0]);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003080 File Offset: 0x00001280
		internal static NetTcpBinding GetTcpBinding()
		{
			return new NetTcpBinding
			{
				MaxReceivedMessageSize = (long)((int)ByteQuantifiedSize.FromMB(10UL).ToBytes()),
				Security = 
				{
					Transport = 
					{
						ProtectionLevel = ProtectionLevel.EncryptAndSign,
						ClientCredentialType = TcpClientCredentialType.Windows
					},
					Message = 
					{
						ClientCredentialType = MessageCredentialType.Windows
					}
				}
			};
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000030DF File Offset: 0x000012DF
		internal static LocalQueuesDataProvider GetLocalQueuesDataProvider()
		{
			if (DiagnosticsAggregationServicelet.localQueuesDataProvider == null)
			{
				throw new InvalidOperationException("localQueuesDataProvider has not been instantiated yet");
			}
			return DiagnosticsAggregationServicelet.localQueuesDataProvider;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000030F8 File Offset: 0x000012F8
		internal static GroupQueuesDataProvider GetGroupQueuesDataProvider()
		{
			if (DiagnosticsAggregationServicelet.groupQueuesDataProvider == null)
			{
				throw new InvalidOperationException("groupQueuesDataProvider has not been instantiated yet");
			}
			return DiagnosticsAggregationServicelet.groupQueuesDataProvider;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003111 File Offset: 0x00001311
		private static void OnLocalServerChanged(ADNotificationEventArgs args)
		{
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceDebug(0L, "LocalServer changed");
			DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.Information, "LocalServer changed", new object[0]);
			DiagnosticsAggregationServicelet.GetLocalServer();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003189 File Offset: 0x00001389
		private static ADOperationResult GetTransportSettings()
		{
			return ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 219, "GetTransportSettings", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\DiagnosticsAggregation\\Program\\DiagnosticsAggregationServicelet.cs");
				TransportConfigContainer transportConfigContainer = topologyConfigurationSession.FindSingletonConfigurationObject<TransportConfigContainer>();
				if (transportConfigContainer != null)
				{
					DiagnosticsAggregationServicelet.transportSettings = transportConfigContainer;
					return;
				}
				throw new TenantTransportSettingsNotFoundException("First Org");
			});
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000031E9 File Offset: 0x000013E9
		private static ADOperationResult GetLocalServer()
		{
			return ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 246, "GetLocalServer", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ServiceHost\\Servicelets\\DiagnosticsAggregation\\Program\\DiagnosticsAggregationServicelet.cs");
				DiagnosticsAggregationServicelet.localServer = topologyConfigurationSession.FindLocalServer();
			});
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003210 File Offset: 0x00001410
		private static bool TryReadAdObjects()
		{
			ADOperationResult adoperationResult = DiagnosticsAggregationServicelet.GetLocalServer();
			if (!adoperationResult.Succeeded)
			{
				Exception exception = adoperationResult.Exception;
				DiagnosticsAggregationServicelet.EventLog.LogEvent(MSExchangeDiagnosticsAggregationEventLogConstants.Tuple_DiagnosticsAggregationServiceletLoadFailed, null, new object[]
				{
					exception
				});
				DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.ServiceletError, "Getting Local server failed. Details {0}", new object[]
				{
					exception
				});
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<Exception>(0L, "Encountered an error while getting local server object. Details {0}.", exception);
				return false;
			}
			ADOperationResult adoperationResult2 = DiagnosticsAggregationServicelet.GetTransportSettings();
			if (!adoperationResult2.Succeeded)
			{
				Exception exception2 = adoperationResult2.Exception;
				DiagnosticsAggregationServicelet.EventLog.LogEvent(MSExchangeDiagnosticsAggregationEventLogConstants.Tuple_DiagnosticsAggregationServiceletLoadFailed, null, new object[]
				{
					exception2
				});
				DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.ServiceletError, "Getting transportsettings failed. Details {0}", new object[]
				{
					exception2
				});
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<Exception>(0L, "Encountered an error while getting TransportSettings configuration. Details {0}.", exception2);
				return false;
			}
			return true;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000032F0 File Offset: 0x000014F0
		private void HostService()
		{
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceDebug(0L, "DiagnosticsAggregationServicelet HostService started.");
			DiagnosticsAggregationServicelet.log.Start(DiagnosticsAggregationServicelet.config);
			if (!DiagnosticsAggregationServicelet.TryReadAdObjects())
			{
				return;
			}
			ServiceHost serviceHost = null;
			try
			{
				this.RegisterForChangeNotifications();
				if (!this.TryHostDiagnosticsWebService(false, out serviceHost))
				{
					return;
				}
				this.StartPeriodicDagAggregation();
				while (!base.StopEvent.WaitOne(TimeSpan.FromMinutes(1.0)))
				{
					if (DiagnosticsAggregationServicelet.transportSettingsChanged)
					{
						DiagnosticsAggregationServicelet.transportSettingsChanged = false;
						TransportConfigContainer transportConfigContainer = DiagnosticsAggregationServicelet.TransportSettings;
						if (DiagnosticsAggregationServicelet.GetTransportSettings() != ADOperationResult.Success)
						{
							DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.ServiceletError, "Fetching transport settings failed", new object[0]);
						}
						else if (transportConfigContainer.DiagnosticsAggregationServicePort != DiagnosticsAggregationServicelet.TransportSettings.DiagnosticsAggregationServicePort)
						{
							DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.Information, "Webservice port is changed from {0} to {1}. Hosting the webservice with the new bindings.", new object[]
							{
								transportConfigContainer.DiagnosticsAggregationServicePort,
								DiagnosticsAggregationServicelet.TransportSettings.DiagnosticsAggregationServicePort
							});
							ServiceHost serviceHost2 = null;
							if (this.TryHostDiagnosticsWebService(true, out serviceHost))
							{
								ServiceHost client = serviceHost;
								serviceHost = serviceHost2;
								WcfUtils.DisposeWcfClientGracefully(client, false);
							}
							else
							{
								DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.ServiceletError, "Hosting the webservice with new bindings did not succeed.", new object[0]);
							}
						}
						else
						{
							DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.Information, "Webservice port did not change.", new object[0]);
						}
					}
				}
			}
			finally
			{
				if (DiagnosticsAggregationServicelet.groupQueuesDataProvider != null)
				{
					DiagnosticsAggregationServicelet.groupQueuesDataProvider.Stop();
				}
				if (DiagnosticsAggregationServicelet.localQueuesDataProvider != null)
				{
					DiagnosticsAggregationServicelet.localQueuesDataProvider.Stop();
				}
				WcfUtils.DisposeWcfClientGracefully(serviceHost, false);
				this.UnregisterADNotifications();
				DiagnosticsAggregationServicelet.log.Stop();
			}
			ExTraceGlobals.DiagnosticsAggregationTracer.TraceDebug(0L, "HostService Stopped.");
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000034A4 File Offset: 0x000016A4
		private void StartPeriodicDagAggregation()
		{
			DiagnosticsAggregationServicelet.localQueuesDataProvider = new LocalQueuesDataProvider(DiagnosticsAggregationServicelet.log, DiagnosticsAggregationServicelet.LocalServer.Id);
			DiagnosticsAggregationServicelet.groupQueuesDataProvider = new GroupQueuesDataProvider(DiagnosticsAggregationServicelet.log);
			DiagnosticsAggregationServicelet.localQueuesDataProvider.Start();
			DiagnosticsAggregationServicelet.groupQueuesDataProvider.Start();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000034E4 File Offset: 0x000016E4
		private bool TryHostDiagnosticsWebService(bool rehosting, out ServiceHost host)
		{
			host = null;
			Exception ex = null;
			int diagnosticsAggregationServicePort = DiagnosticsAggregationServicelet.TransportSettings.DiagnosticsAggregationServicePort;
			try
			{
				host = new ServiceHost(typeof(DiagnosticsAggregationServiceImpl), new Uri[0]);
				string text = string.Format(CultureInfo.InvariantCulture, DiagnosticsAggregationHelper.DiagnosticsAggregationEndpointFormat, new object[]
				{
					"localhost",
					diagnosticsAggregationServicePort
				});
				host.AddServiceEndpoint(typeof(IDiagnosticsAggregationService), DiagnosticsAggregationServicelet.GetTcpBinding(), text);
				this.AddMetadataEndpointInDebugBuild(host);
				host.Open();
				DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.Information, string.Format(CultureInfo.InvariantCulture, "listening at {0}", new object[]
				{
					text
				}), new object[0]);
			}
			catch (InvalidOperationException ex2)
			{
				ex = ex2;
			}
			catch (CommunicationException ex3)
			{
				ex = ex3;
			}
			catch (TimeoutException ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex != null && host != null)
				{
					WcfUtils.DisposeWcfClientGracefully(host, false);
				}
			}
			if (ex != null)
			{
				ExTraceGlobals.DiagnosticsAggregationTracer.TraceError<Exception>(0L, "HostService Failed {0}.", ex);
				if (rehosting)
				{
					DiagnosticsAggregationServicelet.EventLog.LogEvent(MSExchangeDiagnosticsAggregationEventLogConstants.Tuple_DiagnosticsAggregationRehostingFailed, null, new object[]
					{
						diagnosticsAggregationServicePort,
						ex
					});
				}
				else
				{
					DiagnosticsAggregationServicelet.EventLog.LogEvent(MSExchangeDiagnosticsAggregationEventLogConstants.Tuple_DiagnosticsAggregationServiceletLoadFailed, null, new object[]
					{
						ex
					});
				}
				DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.ServiceletError, ex.ToString(), new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003678 File Offset: 0x00001878
		private void AddMetadataEndpointInDebugBuild(ServiceHost host)
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000367C File Offset: 0x0000187C
		private void RegisterForChangeNotifications()
		{
			if (DiagnosticsAggregationServicelet.serverConfigNotificationCookie != null)
			{
				throw new InvalidOperationException("Cannot register for transportserver AD change notifications twice");
			}
			ADNotificationAdapter.TryRegisterChangeNotification<Server>(DiagnosticsAggregationServicelet.localServer.Id, new ADNotificationCallback(DiagnosticsAggregationServicelet.OnLocalServerChanged), 5, out DiagnosticsAggregationServicelet.serverConfigNotificationCookie);
			if (DiagnosticsAggregationServicelet.transportSettingsNotificationCookie != null)
			{
				throw new InvalidOperationException("Cannot register for transportsettings AD change notifications twice");
			}
			ADNotificationAdapter.TryRegisterChangeNotification<TransportConfigContainer>(DiagnosticsAggregationServicelet.TransportSettings.Id, new ADNotificationCallback(this.OnTransportSettingsChanged), 5, out DiagnosticsAggregationServicelet.transportSettingsNotificationCookie);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000036F1 File Offset: 0x000018F1
		private void OnTransportSettingsChanged(ADNotificationEventArgs args)
		{
			DiagnosticsAggregationServicelet.Log.Log(DiagnosticsAggregationEvent.Information, "Transportsettings changed", new object[0]);
			DiagnosticsAggregationServicelet.transportSettingsChanged = true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003728 File Offset: 0x00001928
		private void UnregisterADNotifications()
		{
			if (DiagnosticsAggregationServicelet.serverConfigNotificationCookie != null)
			{
				ADNotificationAdapter.TryRunADOperation(delegate()
				{
					ADNotificationAdapter.UnregisterChangeNotification(DiagnosticsAggregationServicelet.serverConfigNotificationCookie);
				}, 0);
				DiagnosticsAggregationServicelet.serverConfigNotificationCookie = null;
			}
			if (DiagnosticsAggregationServicelet.transportSettingsNotificationCookie != null)
			{
				ADNotificationAdapter.TryRunADOperation(delegate()
				{
					ADNotificationAdapter.UnregisterChangeNotification(DiagnosticsAggregationServicelet.transportSettingsNotificationCookie);
				}, 0);
				DiagnosticsAggregationServicelet.transportSettingsNotificationCookie = null;
			}
		}

		// Token: 0x04000029 RID: 41
		private static readonly ExEventLog eventLog = new ExEventLog(ExTraceGlobals.DiagnosticsAggregationTracer.Category, "MSExchange DiagnosticsAggregation");

		// Token: 0x0400002A RID: 42
		private static readonly DiagnosticsAggregationLog log = new DiagnosticsAggregationLog();

		// Token: 0x0400002B RID: 43
		private static LocalQueuesDataProvider localQueuesDataProvider;

		// Token: 0x0400002C RID: 44
		private static GroupQueuesDataProvider groupQueuesDataProvider;

		// Token: 0x0400002D RID: 45
		private static Server localServer;

		// Token: 0x0400002E RID: 46
		private static TransportConfigContainer transportSettings;

		// Token: 0x0400002F RID: 47
		private static ADNotificationRequestCookie serverConfigNotificationCookie;

		// Token: 0x04000030 RID: 48
		private static DiagnosticsAggregationServiceletConfig config;

		// Token: 0x04000031 RID: 49
		private static ADNotificationRequestCookie transportSettingsNotificationCookie;

		// Token: 0x04000032 RID: 50
		private static bool transportSettingsChanged;
	}
}
