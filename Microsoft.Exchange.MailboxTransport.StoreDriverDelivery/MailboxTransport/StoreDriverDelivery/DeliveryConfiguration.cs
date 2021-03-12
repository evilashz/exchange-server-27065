using System;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Extensibility;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.RecipientAPI;
using Microsoft.Exchange.Transport.ResourceMonitoring;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000038 RID: 56
	internal class DeliveryConfiguration : IDeliveryConfiguration
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000C658 File Offset: 0x0000A858
		private DeliveryConfiguration()
		{
			DeliveryConfiguration.components = new Components(string.Empty, false);
			DeliveryConfiguration.app = new AppConfig();
			this.isInitialized = false;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000C681 File Offset: 0x0000A881
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000C699 File Offset: 0x0000A899
		public static IDeliveryConfiguration Instance
		{
			get
			{
				if (DeliveryConfiguration.configuration == null)
				{
					DeliveryConfiguration.configuration = new DeliveryConfiguration();
				}
				return DeliveryConfiguration.configuration;
			}
			set
			{
				DeliveryConfiguration.configuration = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000C6A1 File Offset: 0x0000A8A1
		public IAppConfiguration App
		{
			get
			{
				return DeliveryConfiguration.app;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public DeliveryPoisonHandler PoisonHandler
		{
			get
			{
				return DeliveryConfiguration.poisonHandler;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000C6AF File Offset: 0x0000A8AF
		public IThrottlingConfig Throttling
		{
			get
			{
				return DeliveryConfiguration.throttlingConfig;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		public void Load(IMbxDeliveryListener submitHandler)
		{
			if (!this.isInitialized)
			{
				DeliveryConfiguration.app.Load();
				this.submitHandler = submitHandler;
				this.ConstructComponentLoadTree();
				DeliveryConfiguration.components.Start(new Components.StopServiceHandler(DeliveryConfiguration.OnStopServiceBecauseOfFailure), false, false, true, true);
				DeliveryConfiguration.components.Continue();
				MessageTrackingLog.Configure(Components.Configuration.LocalServer.TransportServer);
				LatencyTracker.Start(Components.TransportAppConfig.LatencyTracker, ProcessTransportRole.MailboxDelivery);
				this.isInitialized = true;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C733 File Offset: 0x0000A933
		public void Unload()
		{
			if (this.isInitialized)
			{
				DeliveryConfiguration.components.Stop();
				this.isInitialized = false;
			}
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C74E File Offset: 0x0000A94E
		public void ConfigUpdate()
		{
			DeliveryConfiguration.components.ConfigUpdate();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C75A File Offset: 0x0000A95A
		private static void OnStopServiceBecauseOfFailure(string reason, bool canRetry, bool retryAlways, bool failServiceWithException)
		{
			Environment.Exit(1);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C764 File Offset: 0x0000A964
		private void ConstructComponentLoadTree()
		{
			TransportAppConfig.IsMemberOfResolverConfiguration transportIsMemberOfResolverConfig = Components.TransportAppConfig.TransportIsMemberOfResolverConfig;
			IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver adAdapter = new IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver(transportIsMemberOfResolverConfig.DisableDynamicGroups);
			TransportAppConfig.IsMemberOfResolverConfiguration mailboxRulesIsMemberOfResolverConfig = Components.TransportAppConfig.MailboxRulesIsMemberOfResolverConfig;
			IsMemberOfResolverADAdapter<string>.LegacyDNResolver adAdapter2 = new IsMemberOfResolverADAdapter<string>.LegacyDNResolver(mailboxRulesIsMemberOfResolverConfig.DisableDynamicGroups);
			Components.AgentComponent = new AgentComponent();
			Components.RoutingComponent = new RoutingComponent();
			Components.EnhancedDns = new EnhancedDns();
			Components.DsnGenerator = new DsnGenerator();
			Components.UnhealthyTargetFilterComponent = new UnhealthyTargetFilterComponent();
			Components.TransportIsMemberOfResolverComponent = new IsMemberOfResolverComponent<RoutingAddress>("Transport", transportIsMemberOfResolverConfig, adAdapter);
			Components.MailboxRulesIsMemberOfResolverComponent = new IsMemberOfResolverComponent<string>("MailboxRules", mailboxRulesIsMemberOfResolverConfig, adAdapter2);
			Components.StoreDriverDelivery = StoreDriverDelivery.CreateStoreDriver();
			Components.Categorizer = this.submitHandler;
			Components.CertificateComponent = new CertificateComponent();
			Components.Configuration = new ConfigurationComponent(ProcessTransportRole.MailboxDelivery);
			Components.MessageThrottlingComponent = new MessageThrottlingComponent();
			Components.ResourceManagerComponent = new ResourceManagerComponent(ResourceManagerResources.PrivateBytes | ResourceManagerResources.TotalBytes);
			Components.SmtpInComponent = new SmtpInComponent(this.IsModernSmtpStackEnabled());
			Components.SmtpOutConnectionHandler = new SmtpOutConnectionHandler();
			Components.SystemCheckComponent = new SystemCheckComponent();
			Components.TransportMailItemLoader item = new Components.TransportMailItemLoader();
			Components.ProxyHubSelectorComponent = new ProxyHubSelectorComponent();
			Components.PoisonMessageComponent = new PoisonMessage();
			Components.Logging = new Components.LoggingComponent(false, false, false, false, false);
			StorageFactory.SchemaToUse = StorageFactory.Schema.NullSchema;
			Components.MessagingDatabase = new MessagingDatabaseComponent();
			Components.PerfCountersLoader perfCountersLoader = new Components.PerfCountersLoader(false);
			Components.PerfCountersLoaderComponent = perfCountersLoader;
			Components.ResourceThrottlingComponent = new ResourceThrottlingComponent(new ResourceMeteringConfig(8000, null), new ResourceThrottlingConfig(null), new ComponentsWrapper(), Components.MessagingDatabase, null, Components.Configuration, ResourceManagerResources.PrivateBytes | ResourceManagerResources.TotalBytes, ResourceObservingComponents.EnhancedDns | ResourceObservingComponents.IsMemberOfResolver | ResourceObservingComponents.SmtpIn);
			ParallelTransportComponent parallelTransportComponent = new ParallelTransportComponent("Parallel Group 1");
			parallelTransportComponent.TransportComponents.Add(Components.ResourceManagerComponent);
			parallelTransportComponent.TransportComponents.Add(Components.CertificateComponent);
			parallelTransportComponent.TransportComponents.Add(Components.TransportIsMemberOfResolverComponent);
			parallelTransportComponent.TransportComponents.Add(Components.MailboxRulesIsMemberOfResolverComponent);
			ParallelTransportComponent parallelTransportComponent2 = new ParallelTransportComponent("Parallel Group 2");
			parallelTransportComponent2.TransportComponents.Add(item);
			parallelTransportComponent2.TransportComponents.Add(new Components.MicrosoftExchangeRecipientLoader());
			parallelTransportComponent2.TransportComponents.Add(perfCountersLoader);
			parallelTransportComponent2.TransportComponents.Add(Components.Logging);
			parallelTransportComponent2.TransportComponents.Add(Components.PoisonMessageComponent);
			parallelTransportComponent2.TransportComponents.Add(Components.MessageThrottlingComponent);
			parallelTransportComponent2.TransportComponents.Add(Components.StoreDriverDelivery);
			parallelTransportComponent2.TransportComponents.Add((ITransportComponent)Components.AgentComponent);
			ParallelTransportComponent parallelTransportComponent3 = new ParallelTransportComponent("Parallel Group 3");
			parallelTransportComponent3.TransportComponents.Add(Components.SmtpInComponent);
			parallelTransportComponent3.TransportComponents.Add(Components.SmtpOutConnectionHandler);
			parallelTransportComponent3.TransportComponents.Add(Components.RoutingComponent);
			parallelTransportComponent3.TransportComponents.Add(Components.UnhealthyTargetFilterComponent);
			parallelTransportComponent3.TransportComponents.Add(Components.DsnGenerator);
			parallelTransportComponent3.TransportComponents.Add(DeliveryConfiguration.poisonHandler);
			Components.SetRootComponent(new SequentialTransportComponent("Root Component")
			{
				TransportComponents = 
				{
					(ITransportComponent)Components.Configuration,
					Components.SystemCheckComponent,
					parallelTransportComponent,
					parallelTransportComponent2,
					parallelTransportComponent3,
					Components.ResourceThrottlingComponent,
					Components.ProxyHubSelectorComponent
				}
			});
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		private bool IsModernSmtpStackEnabled()
		{
			VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null);
			return snapshot != null && snapshot.MailboxTransport.MailboxTransportSmtpIn.Enabled;
		}

		// Token: 0x04000113 RID: 275
		private static Components components;

		// Token: 0x04000114 RID: 276
		private static IAppConfiguration app;

		// Token: 0x04000115 RID: 277
		private static IDeliveryConfiguration configuration;

		// Token: 0x04000116 RID: 278
		private static DeliveryPoisonHandler poisonHandler = new DeliveryPoisonHandler(Components.TransportAppConfig.PoisonMessage.CrashDetectionWindow, DeliveryConfiguration.Instance.App.PoisonRegistryEntryMaxCount);

		// Token: 0x04000117 RID: 279
		private static IThrottlingConfig throttlingConfig = ThrottlingConfigFactory.Create();

		// Token: 0x04000118 RID: 280
		private bool isInitialized;

		// Token: 0x04000119 RID: 281
		private IMbxDeliveryListener submitHandler;
	}
}
