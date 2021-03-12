using System;
using System.Reflection;
using System.ServiceProcess;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Extensibility;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.RecipientAPI;
using Microsoft.Exchange.Transport.ResourceMonitoring;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.FrontEnd
{
	// Token: 0x02000004 RID: 4
	internal sealed class FrontEndTransportService : ExServiceBase
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002248 File Offset: 0x00000448
		public FrontEndTransportService()
		{
			base.ServiceName = "MSExchangeFrontEndTransport";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
			string componentId = ServerComponentStates.GetComponentId(ServerComponentEnum.FrontendTransport);
			this.components = new Components(componentId, true);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002290 File Offset: 0x00000490
		public static void Main(string[] args)
		{
			bool flag = false;
			bool flag2 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.Ordinal))
				{
					FrontEndTransportService.Usage();
					Environment.Exit(0);
				}
				else if (text.StartsWith("-console"))
				{
					flag = true;
				}
				else if (text.StartsWith("-wait"))
				{
					flag2 = true;
				}
			}
			FrontEndTransportService.runningAsService = !Environment.UserInteractive;
			if (!FrontEndTransportService.runningAsService)
			{
				if (!flag)
				{
					FrontEndTransportService.Usage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			Globals.InitializeSinglePerfCounterInstance();
			ExWatson.Register();
			string text2;
			if (!Components.TryLoadTransportAppConfig(out text2))
			{
				Environment.Exit(1);
			}
			SettingOverrideSync.Instance.Start(true);
			FrontEndTransportService.instance = new FrontEndTransportService();
			if (FrontEndTransportService.runningAsService)
			{
				ServiceBase.Run(FrontEndTransportService.instance);
				return;
			}
			ExServiceBase.RunAsConsole(FrontEndTransportService.instance);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002394 File Offset: 0x00000594
		protected override void OnStartInternal(string[] args)
		{
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Transport.ADExceptionHandling.Enabled)
			{
				FrontEndTransportService.ConstructComponentLoadTree();
			}
			else
			{
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(new ADOperation(FrontEndTransportService.ConstructComponentLoadTree), 3);
				if (!adoperationResult.Succeeded)
				{
					FrontEndTransportService.eventLog.LogEvent(TransportEventLogConstants.Tuple_FrontendTransportServiceInitializationFailure, null, new object[]
					{
						adoperationResult.Exception.ToString()
					});
					base.GracefullyAbortStartup();
				}
			}
			this.components.Start(new Components.StopServiceHandler(FrontEndTransportService.OnStopServiceBecauseOfFailure), false, false, true, true);
			this.components.Continue();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002436 File Offset: 0x00000636
		protected override void OnStopInternal()
		{
			ADNotificationListener.Stop();
			this.components.Stop();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002448 File Offset: 0x00000648
		protected override void OnCustomCommandInternal(int command)
		{
			switch (command)
			{
			case 201:
				this.components.ConfigUpdate();
				return;
			case 202:
			case 204:
				break;
			case 203:
				Components.Configuration.ClearCaches();
				return;
			case 205:
				Kerberos.FlushTicketCache();
				return;
			case 206:
				Components.SmtpInComponent.FlushProtocolLog();
				Components.SmtpOutConnectionHandler.FlushProtocolLog();
				ConnectionLog.FlushBuffer();
				MessageTrackingLog.FlushBuffer();
				break;
			default:
				return;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024B7 File Offset: 0x000006B7
		private static void OnStopServiceBecauseOfFailure(string reason, bool canRetry, bool retryAlways, bool failServiceWithException)
		{
			Environment.Exit(1);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024BF File Offset: 0x000006BF
		private static void Usage()
		{
			Console.WriteLine("MSExchangeFrontEndTransport.exe -console -wait -?");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024CC File Offset: 0x000006CC
		private static void ConstructComponentLoadTree()
		{
			TransportAppConfig.IsMemberOfResolverConfiguration transportIsMemberOfResolverConfig = Components.TransportAppConfig.TransportIsMemberOfResolverConfig;
			IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver adAdapter = new IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver(transportIsMemberOfResolverConfig.DisableDynamicGroups);
			Components.AgentComponent = new AgentComponent();
			Components.RoutingComponent = new RoutingComponent();
			Components.EnhancedDns = new EnhancedDns();
			Components.UnhealthyTargetFilterComponent = new UnhealthyTargetFilterComponent();
			Components.ProxyHubSelectorComponent = new ProxyHubSelectorComponent();
			Components.CertificateComponent = new CertificateComponent();
			Components.Configuration = new ConfigurationComponent(ProcessTransportRole.FrontEnd);
			Components.MessageThrottlingComponent = new MessageThrottlingComponent();
			Components.ResourceManagerComponent = new ResourceManagerComponent(ResourceManagerResources.PrivateBytes | ResourceManagerResources.TotalBytes);
			Components.SmtpInComponent = new SmtpInComponent(false);
			Components.SmtpOutConnectionHandler = new SmtpOutConnectionHandler();
			Components.SystemCheckComponent = new SystemCheckComponent();
			Components.TransportIsMemberOfResolverComponent = new IsMemberOfResolverComponent<RoutingAddress>("Transport", transportIsMemberOfResolverConfig, adAdapter);
			Components.TransportMailItemLoader item = new Components.TransportMailItemLoader();
			Components.Logging = new Components.LoggingComponent(false, false, false, false, true);
			StorageFactory.SchemaToUse = StorageFactory.Schema.NullSchema;
			Components.MessagingDatabase = new MessagingDatabaseComponent();
			Components.ResourceThrottlingComponent = new ResourceThrottlingComponent(new ResourceMeteringConfig(8000, null), new ResourceThrottlingConfig(null), new ComponentsWrapper(), Components.MessagingDatabase, null, Components.Configuration, ResourceManagerResources.PrivateBytes | ResourceManagerResources.TotalBytes, ResourceObservingComponents.EnhancedDns | ResourceObservingComponents.IsMemberOfResolver | ResourceObservingComponents.SmtpIn);
			Components.PerfCountersLoader perfCountersLoader = new Components.PerfCountersLoader(false);
			Components.PerfCountersLoaderComponent = perfCountersLoader;
			ParallelTransportComponent parallelTransportComponent = new ParallelTransportComponent("AD Configuration Readers");
			parallelTransportComponent.TransportComponents.Add(item);
			parallelTransportComponent.TransportComponents.Add(Components.TransportIsMemberOfResolverComponent);
			parallelTransportComponent.TransportComponents.Add(perfCountersLoader);
			parallelTransportComponent.TransportComponents.Add(Components.Logging);
			parallelTransportComponent.TransportComponents.Add(new Components.ServicePrincipalNameRegistrar());
			parallelTransportComponent.TransportComponents.Add(Components.MessageThrottlingComponent);
			parallelTransportComponent.TransportComponents.Add(new Components.DirectTrustLoader());
			SequentialTransportComponent sequentialTransportComponent = new SequentialTransportComponent("Resource Manager");
			sequentialTransportComponent.TransportComponents.Add(Components.ResourceManagerComponent);
			ParallelTransportComponent parallelTransportComponent2 = new ParallelTransportComponent("Configuration, Certificate, and Resource Manager components");
			parallelTransportComponent2.TransportComponents.Add(sequentialTransportComponent);
			parallelTransportComponent2.TransportComponents.Add(Components.CertificateComponent);
			SequentialTransportComponent sequentialTransportComponent2 = new SequentialTransportComponent("Root Component");
			sequentialTransportComponent2.TransportComponents.Add((ITransportComponent)Components.Configuration);
			sequentialTransportComponent2.TransportComponents.Add(Components.SystemCheckComponent);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent2);
			sequentialTransportComponent2.TransportComponents.Add((ITransportComponent)Components.AgentComponent);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.RoutingComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.EnhancedDns);
			sequentialTransportComponent2.TransportComponents.Add(Components.UnhealthyTargetFilterComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.ProxyHubSelectorComponent);
			ParallelTransportComponent parallelTransportComponent3 = new ParallelTransportComponent("SmtpIn and SmtpOut");
			parallelTransportComponent3.TransportComponents.Add(Components.SmtpInComponent);
			parallelTransportComponent3.TransportComponents.Add(Components.SmtpOutConnectionHandler);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent3);
			sequentialTransportComponent2.TransportComponents.Add(new FrontEndBackgroundProcessingThread(new FrontEndBackgroundProcessingThread.ServerComponentStateChangedHandler(FrontEndTransportService.instance.HandleServerComponentStateChanged)));
			sequentialTransportComponent2.TransportComponents.Add(Components.ResourceThrottlingComponent);
			Components.SetDatabaseComponents(sequentialTransportComponent);
			Components.SetRootComponent(sequentialTransportComponent2);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027CC File Offset: 0x000009CC
		private void HandleServerComponentStateChanged(ServiceState newState)
		{
			FrontEndTransportService.eventLog.LogEvent(TransportEventLogConstants.Tuple_FrontendTransportRestartOnServiceStateChange, null, new object[]
			{
				newState
			});
			Environment.Exit(0);
		}

		// Token: 0x04000007 RID: 7
		public const string FrontEndTransportServiceName = "MSExchangeFrontEndTransport";

		// Token: 0x04000008 RID: 8
		private const string HelpOption = "-?";

		// Token: 0x04000009 RID: 9
		private const string ConsoleOption = "-console";

		// Token: 0x0400000A RID: 10
		private const string WaitToContinueOption = "-wait";

		// Token: 0x0400000B RID: 11
		private static FrontEndTransportService instance;

		// Token: 0x0400000C RID: 12
		private static ExEventLog eventLog = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x0400000D RID: 13
		private static bool runningAsService;

		// Token: 0x0400000E RID: 14
		private Components components;
	}
}
