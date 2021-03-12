using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;
using Microsoft.Exchange.Data.Storage.OfflineRms;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MessageSecurity;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.IPFilter;
using Microsoft.Exchange.Rpc.MailSubmission;
using Microsoft.Exchange.Rpc.OfflineRms;
using Microsoft.Exchange.Rpc.QueueViewer;
using Microsoft.Exchange.SecureMail;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport.Admin.IPFiltering;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Delivery;
using Microsoft.Exchange.Transport.Extensibility;
using Microsoft.Exchange.Transport.Logging;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Logging.WorkloadManagement;
using Microsoft.Exchange.Transport.MessageResubmission;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.Pickup;
using Microsoft.Exchange.Transport.QueueViewer;
using Microsoft.Exchange.Transport.RecipientAPI;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Exchange.Transport.Storage;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.Win32;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000057 RID: 87
	internal sealed class Components
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000B26C File Offset: 0x0000946C
		static Components()
		{
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(Components.FaultInjectionCallback));
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B2DE File Offset: 0x000094DE
		public Components(string hostServiceName, bool stateManagementEnabled)
		{
			this.hostServiceName = hostServiceName;
			this.stateManagementEnabled = stateManagementEnabled;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600023D RID: 573 RVA: 0x0000B2F4 File Offset: 0x000094F4
		// (remove) Token: 0x0600023E RID: 574 RVA: 0x0000B328 File Offset: 0x00009528
		public static event EventHandler ConfigChanged;

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000B35B File Offset: 0x0000955B
		public static object SyncRoot
		{
			get
			{
				return Components.syncRoot;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000B362 File Offset: 0x00009562
		// (set) Token: 0x06000241 RID: 577 RVA: 0x0000B37B File Offset: 0x0000957B
		public static IStoreDriver StoreDriver
		{
			get
			{
				if (Components.storeDriver == null)
				{
					throw new Components.ComponentNotAvailableException("StoreDriver");
				}
				return Components.storeDriver;
			}
			set
			{
				Components.storeDriver = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000B383 File Offset: 0x00009583
		// (set) Token: 0x06000243 RID: 579 RVA: 0x0000B399 File Offset: 0x00009599
		public static IStoreDriverSubmission StoreDriverSubmission
		{
			get
			{
				Components.ThrowIfNullComponent("StoreDriverSubmission", Components.storeDriverSubmission);
				return Components.storeDriverSubmission;
			}
			internal set
			{
				Components.storeDriverSubmission = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000B3A1 File Offset: 0x000095A1
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000B3B7 File Offset: 0x000095B7
		public static IStoreDriverDelivery StoreDriverDelivery
		{
			get
			{
				Components.ThrowIfNullComponent("StoreDriverDelivery", Components.storeDriverDelivery);
				return Components.storeDriverDelivery;
			}
			internal set
			{
				Components.storeDriverDelivery = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000B3BF File Offset: 0x000095BF
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000B3D5 File Offset: 0x000095D5
		public static IStartableTransportComponent StoreDriverLoaderComponent
		{
			get
			{
				Components.ThrowIfNullComponent("StoreDriverLoaderComponent", Components.storeDriverLoaderComponent);
				return Components.storeDriverLoaderComponent;
			}
			set
			{
				Components.storeDriverLoaderComponent = (Components.StoreDriverLoader)value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000B3E2 File Offset: 0x000095E2
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public static IStartableTransportComponent Aggregator
		{
			get
			{
				Components.ThrowIfNullComponent("Aggregator", Components.aggregator);
				return Components.aggregator;
			}
			set
			{
				Components.aggregator = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000B400 File Offset: 0x00009600
		public static TransportAppConfig TransportAppConfig
		{
			get
			{
				if (Components.transportAppConfig == null)
				{
					Components.transportAppConfig = TransportAppConfig.Load();
				}
				return Components.transportAppConfig;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000B418 File Offset: 0x00009618
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000B42E File Offset: 0x0000962E
		public static MessagingDatabaseComponent MessagingDatabase
		{
			get
			{
				Components.ThrowIfNullComponent("MessagingDatabase", Components.messagingDatabaseComponent);
				return Components.messagingDatabaseComponent;
			}
			set
			{
				Components.messagingDatabaseComponent = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000B436 File Offset: 0x00009636
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000B44C File Offset: 0x0000964C
		public static ISmtpInComponent SmtpInComponent
		{
			get
			{
				Components.ThrowIfNullComponent("SmtpInComponent", Components.smtpInComponent);
				return Components.smtpInComponent;
			}
			set
			{
				Components.smtpInComponent = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000B454 File Offset: 0x00009654
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000B46A File Offset: 0x0000966A
		public static SmtpOutConnectionHandler SmtpOutConnectionHandler
		{
			get
			{
				Components.ThrowIfNullComponent("SmtpOutConnectionHandler", Components.smtpOutConnectionHandler);
				return Components.smtpOutConnectionHandler;
			}
			set
			{
				Components.smtpOutConnectionHandler = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000B472 File Offset: 0x00009672
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000B488 File Offset: 0x00009688
		public static PickupComponent PickupComponent
		{
			get
			{
				Components.ThrowIfNullComponent("PickupComponent", Components.pickupComponent);
				return Components.pickupComponent;
			}
			set
			{
				Components.pickupComponent = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000B490 File Offset: 0x00009690
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000B4A6 File Offset: 0x000096A6
		public static IBootLoader BootScanner
		{
			get
			{
				Components.ThrowIfNullComponent("BootScanner", Components.bootScanner);
				return Components.bootScanner;
			}
			set
			{
				Components.bootScanner = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000B4AE File Offset: 0x000096AE
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000B4C4 File Offset: 0x000096C4
		public static CategorizerComponent CategorizerComponent
		{
			get
			{
				Components.ThrowIfNullComponent("CategorizerComponent", Components.categorizerComponent);
				return Components.categorizerComponent;
			}
			set
			{
				Components.categorizerComponent = value;
				Components.categorizer = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000B4D2 File Offset: 0x000096D2
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000B4E8 File Offset: 0x000096E8
		public static CategorizerAdapterComponent CategorizerAdapterComponent
		{
			get
			{
				Components.ThrowIfNullComponent("CategorizerAdapterComponent", Components.categorizerAdapterComponent);
				return Components.categorizerAdapterComponent;
			}
			set
			{
				Components.categorizerAdapterComponent = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000B4F0 File Offset: 0x000096F0
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000B506 File Offset: 0x00009706
		public static SchedulerAdapterComponent SchedulerAdapterComponent
		{
			get
			{
				Components.ThrowIfNullComponent("SchedulerAdapterComponent", Components.schedulerAdapterComponent);
				return Components.schedulerAdapterComponent;
			}
			set
			{
				Components.schedulerAdapterComponent = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000B50E File Offset: 0x0000970E
		public static ICategorizer Categorizer
		{
			set
			{
				Components.categorizer = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000B516 File Offset: 0x00009716
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000B52C File Offset: 0x0000972C
		public static IRoutingComponent RoutingComponent
		{
			get
			{
				Components.ThrowIfNullComponent("RoutingComponent", Components.routingComponent);
				return Components.routingComponent;
			}
			set
			{
				Components.routingComponent = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000B534 File Offset: 0x00009734
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000B54A File Offset: 0x0000974A
		public static UnhealthyTargetFilterComponent UnhealthyTargetFilterComponent
		{
			get
			{
				Components.ThrowIfNullComponent("UnhealthyTargetFilterComponent", Components.unhealthyTargetFilterComponent);
				return Components.unhealthyTargetFilterComponent;
			}
			set
			{
				Components.unhealthyTargetFilterComponent = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000B552 File Offset: 0x00009752
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000B568 File Offset: 0x00009768
		public static EnhancedDns EnhancedDns
		{
			get
			{
				Components.ThrowIfNullComponent("EnhancedDnsComponent", Components.enhancedDnsComponent);
				return Components.enhancedDnsComponent;
			}
			set
			{
				Components.enhancedDnsComponent = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000B570 File Offset: 0x00009770
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000B586 File Offset: 0x00009786
		public static IProxyHubSelectorComponent ProxyHubSelectorComponent
		{
			get
			{
				Components.ThrowIfNullComponent("ProxyHubSelectorComponent", Components.proxyHubSelectorComponent);
				return Components.proxyHubSelectorComponent;
			}
			set
			{
				Components.proxyHubSelectorComponent = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000B58E File Offset: 0x0000978E
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000B5A7 File Offset: 0x000097A7
		public static ITransportConfiguration Configuration
		{
			get
			{
				if (Components.configurationComponent == null)
				{
					throw new Components.ComponentNotAvailableException("Configuration");
				}
				return Components.configurationComponent;
			}
			set
			{
				Components.configurationComponent = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000B5AF File Offset: 0x000097AF
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000B5C5 File Offset: 0x000097C5
		public static IAgentRuntime AgentComponent
		{
			get
			{
				Components.ThrowIfNullComponent("AgentComponent", Components.agentComponent);
				return Components.agentComponent;
			}
			set
			{
				Components.agentComponent = (AgentComponent)value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000B5D2 File Offset: 0x000097D2
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000B5E8 File Offset: 0x000097E8
		public static RemoteDeliveryComponent RemoteDeliveryComponent
		{
			get
			{
				Components.ThrowIfNullComponent("RemoteDeliveryComponent", Components.remoteDeliveryComponent);
				return Components.remoteDeliveryComponent;
			}
			set
			{
				Components.remoteDeliveryComponent = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000B5F0 File Offset: 0x000097F0
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000B606 File Offset: 0x00009806
		public static IQueueQuotaComponent QueueQuotaComponent
		{
			get
			{
				Components.ThrowIfNullComponent("QueueQuotaComponent", Components.queueQuotaComponent);
				return Components.queueQuotaComponent;
			}
			set
			{
				Components.queueQuotaComponent = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000B60E File Offset: 0x0000980E
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000B624 File Offset: 0x00009824
		public static IMeteringComponent Metering
		{
			get
			{
				Components.ThrowIfNullComponent("MeteringComponent", Components.meteringComponent);
				return Components.meteringComponent;
			}
			set
			{
				Components.meteringComponent = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000B62C File Offset: 0x0000982C
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000B642 File Offset: 0x00009842
		public static IProcessingQuotaComponent ProcessingQuotaComponent
		{
			get
			{
				Components.ThrowIfNullComponent("ProcessingQuotaComponent", Components.processingQuotaComponent);
				return Components.processingQuotaComponent;
			}
			set
			{
				Components.processingQuotaComponent = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000B64A File Offset: 0x0000984A
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000B660 File Offset: 0x00009860
		public static DeliveryAgentConnectionHandler DeliveryAgentConnectionHandler
		{
			get
			{
				Components.ThrowIfNullComponent("DeliveryAgentConnectionHandler", Components.deliveryAgentConnectionHandler);
				return Components.deliveryAgentConnectionHandler;
			}
			set
			{
				Components.deliveryAgentConnectionHandler = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000B668 File Offset: 0x00009868
		public static int InstanceId
		{
			get
			{
				return Components.instanceId;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000B66F File Offset: 0x0000986F
		public static Dns Dns
		{
			get
			{
				return Components.enhancedDnsComponent;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000B676 File Offset: 0x00009876
		public static bool IsActive
		{
			get
			{
				return Components.active;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000B67D File Offset: 0x0000987D
		public static bool IsPaused
		{
			get
			{
				return Components.paused;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B684 File Offset: 0x00009884
		public static bool ShuttingDown
		{
			get
			{
				return Components.busyExit == 1;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000B68E File Offset: 0x0000988E
		public static bool IsDatabaseShuttingDown
		{
			get
			{
				return Components.databaseComponents.IsUnloading;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000B69A File Offset: 0x0000989A
		public static bool ServiceControlled
		{
			get
			{
				return Components.serviceControlled;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000B6A1 File Offset: 0x000098A1
		public static ExEventLog EventLogger
		{
			get
			{
				return Components.eventLogger;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000B6A8 File Offset: 0x000098A8
		internal static ResourceManager ResourceManager
		{
			get
			{
				Components.ThrowIfNullComponent("ResourceManager", Components.resourceManagerComponent);
				return Components.ResourceManagerComponent.ResourceManager;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000B6C3 File Offset: 0x000098C3
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000B6D9 File Offset: 0x000098D9
		internal static ResourceManagerComponent ResourceManagerComponent
		{
			get
			{
				Components.ThrowIfNullComponent("ResourceManagerComponent", Components.resourceManagerComponent);
				return Components.resourceManagerComponent;
			}
			set
			{
				Components.resourceManagerComponent = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000B6E1 File Offset: 0x000098E1
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000B6F7 File Offset: 0x000098F7
		internal static ResourceThrottlingComponent ResourceThrottlingComponent
		{
			get
			{
				Components.ThrowIfNullComponent("ResourceThrottlingComponent", Components.resourceThrottlingComponent);
				return Components.resourceThrottlingComponent;
			}
			set
			{
				Components.resourceThrottlingComponent = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000B6FF File Offset: 0x000098FF
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000B715 File Offset: 0x00009915
		internal static RmsClientManagerComponent RmsClientManagerComponent
		{
			get
			{
				Components.ThrowIfNullComponent("RmsClientManagerComponent", Components.rmsClientManagerComponent);
				return Components.rmsClientManagerComponent;
			}
			set
			{
				Components.rmsClientManagerComponent = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000B71D File Offset: 0x0000991D
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000B733 File Offset: 0x00009933
		internal static QueueManager QueueManager
		{
			get
			{
				Components.ThrowIfNullComponent("QueueManager", Components.queueManager);
				return Components.queueManager;
			}
			set
			{
				Components.queueManager = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000B73B File Offset: 0x0000993B
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000B751 File Offset: 0x00009951
		internal static IProcessingSchedulerComponent ProcessingSchedulerComponent
		{
			get
			{
				Components.ThrowIfNullComponent("ProcessignSchedulerComponent", Components.processingSchedulerComponent);
				return Components.processingSchedulerComponent;
			}
			set
			{
				Components.processingSchedulerComponent = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B759 File Offset: 0x00009959
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000B76F File Offset: 0x0000996F
		internal static IMessageDepotComponent MessageDepotComponent
		{
			get
			{
				Components.ThrowIfNullComponent("MessageDepotComponent", Components.messageDepotComponent);
				return Components.messageDepotComponent;
			}
			set
			{
				Components.messageDepotComponent = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000B777 File Offset: 0x00009977
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000B78D File Offset: 0x0000998D
		internal static IMessageDepotQueueViewerComponent MessageDepotQueueViewerComponent
		{
			get
			{
				Components.ThrowIfNullComponent("MessageDepotQueueViewerComponent", Components.messageDepotQueueViewerComponent);
				return Components.messageDepotQueueViewerComponent;
			}
			set
			{
				Components.messageDepotQueueViewerComponent = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000B795 File Offset: 0x00009995
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000B7AE File Offset: 0x000099AE
		internal static DsnSchedulerComponent DsnSchedulerComponent
		{
			get
			{
				if (Components.dsnSchedulerComponent == null)
				{
					throw new Components.ComponentNotAvailableException("dsnSchedulerComponent");
				}
				return Components.dsnSchedulerComponent;
			}
			set
			{
				Components.dsnSchedulerComponent = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000B7B6 File Offset: 0x000099B6
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000B7CC File Offset: 0x000099CC
		internal static OrarGenerator OrarGenerator
		{
			get
			{
				Components.ThrowIfNullComponent("OrarGenerator", Components.orarGenerator);
				return Components.orarGenerator;
			}
			set
			{
				Components.orarGenerator = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000B7D4 File Offset: 0x000099D4
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000B7EA File Offset: 0x000099EA
		internal static DsnGenerator DsnGenerator
		{
			get
			{
				Components.ThrowIfNullComponent("DsnGenerator", Components.dsnGenerator);
				return Components.dsnGenerator;
			}
			set
			{
				Components.dsnGenerator = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000B7F2 File Offset: 0x000099F2
		internal static ClassificationConfig ClassificationConfig
		{
			get
			{
				return Components.classificationConfig;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000B7F9 File Offset: 0x000099F9
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000B80F File Offset: 0x00009A0F
		internal static MessageResubmissionComponent MessageResubmissionComponent
		{
			get
			{
				Components.ThrowIfNullComponent("MessageResubmissionComponent", Components.messageResubmissionComponent);
				return Components.messageResubmissionComponent;
			}
			set
			{
				Components.messageResubmissionComponent = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000B817 File Offset: 0x00009A17
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000B82D File Offset: 0x00009A2D
		internal static ShadowRedundancyComponent ShadowRedundancyComponent
		{
			get
			{
				Components.ThrowIfNullComponent("ShadowRedundancyComponent", Components.shadowRedundancyComponent);
				return Components.shadowRedundancyComponent;
			}
			set
			{
				Components.shadowRedundancyComponent = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000B835 File Offset: 0x00009A35
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000B84B File Offset: 0x00009A4B
		internal static Components.LoggingComponent Logging
		{
			get
			{
				Components.ThrowIfNullComponent("LoggingComponent", Components.loggingComponent);
				return Components.loggingComponent;
			}
			set
			{
				Components.loggingComponent = value;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000B853 File Offset: 0x00009A53
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000B869 File Offset: 0x00009A69
		internal static MessageThrottlingComponent MessageThrottlingComponent
		{
			get
			{
				Components.ThrowIfNullComponent("MessageThrottlingComponent", Components.messageThrottlingComponent);
				return Components.messageThrottlingComponent;
			}
			set
			{
				Components.messageThrottlingComponent = value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B871 File Offset: 0x00009A71
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000B887 File Offset: 0x00009A87
		internal static PoisonMessage PoisonMessageComponent
		{
			get
			{
				Components.ThrowIfNullComponent("PoisonMessageComponent", Components.poisonMessageComponent);
				return Components.poisonMessageComponent;
			}
			set
			{
				Components.poisonMessageComponent = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B88F File Offset: 0x00009A8F
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000B8A5 File Offset: 0x00009AA5
		internal static IsMemberOfResolverComponent<RoutingAddress> TransportIsMemberOfResolverComponent
		{
			get
			{
				Components.ThrowIfNullComponent("TransportIsMemberOfResolverComponent", Components.transportIsMemberOfResolverComponent);
				return Components.transportIsMemberOfResolverComponent;
			}
			set
			{
				Components.transportIsMemberOfResolverComponent = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000B8AD File Offset: 0x00009AAD
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000B8C3 File Offset: 0x00009AC3
		internal static IsMemberOfResolverComponent<string> MailboxRulesIsMemberOfResolverComponent
		{
			get
			{
				Components.ThrowIfNullComponent("MailboxRulesIsMemberOfResolverComponent", Components.mailboxRulesIsMemberOfResolverComponent);
				return Components.mailboxRulesIsMemberOfResolverComponent;
			}
			set
			{
				Components.mailboxRulesIsMemberOfResolverComponent = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000B8CB File Offset: 0x00009ACB
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000B8E1 File Offset: 0x00009AE1
		internal static SystemCheckComponent SystemCheckComponent
		{
			get
			{
				Components.ThrowIfNullComponent("SystemCheckComponent", Components.systemCheckComponent);
				return Components.systemCheckComponent;
			}
			set
			{
				Components.systemCheckComponent = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000B8E9 File Offset: 0x00009AE9
		internal static bool IsBridgehead
		{
			get
			{
				return Components.Configuration.LocalServer.IsBridgehead;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000B8FA File Offset: 0x00009AFA
		internal static bool IsMailboxProcess
		{
			get
			{
				return Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxSubmission || Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxDelivery;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000B918 File Offset: 0x00009B18
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000B92E File Offset: 0x00009B2E
		internal static CertificateComponent CertificateComponent
		{
			get
			{
				Components.ThrowIfNullComponent("CertificateComponent", Components.certificateComponent);
				return Components.certificateComponent;
			}
			set
			{
				Components.certificateComponent = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000B938 File Offset: 0x00009B38
		private ServiceState ServiceState
		{
			get
			{
				ServiceState serviceState = ServiceState.Active;
				if (this.stateManagementEnabled)
				{
					serviceState = ServiceStateHelper.GetServiceState(Components.Configuration, this.hostServiceName);
					Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_RetrieveServiceState, null, new object[]
					{
						this.hostServiceName,
						serviceState
					});
				}
				return serviceState;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B98C File Offset: 0x00009B8C
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000B9A5 File Offset: 0x00009BA5
		public static IPerfCountersLoader PerfCountersLoaderComponent
		{
			get
			{
				if (Components.perfCountersLoader == null)
				{
					throw new Components.ComponentNotAvailableException("perfCountersLoader");
				}
				return Components.perfCountersLoader;
			}
			set
			{
				Components.perfCountersLoader = value;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B9AD File Offset: 0x00009BAD
		private static void ThrowIfNullComponent(string componentName, ITransportComponent transportComponent)
		{
			if (transportComponent == null)
			{
				throw new Components.ComponentNotAvailableException(componentName);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B9B9 File Offset: 0x00009BB9
		public static bool TryGetStoreDriver(out IStoreDriver component)
		{
			component = Components.storeDriver;
			return Components.storeDriver != null;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B9CD File Offset: 0x00009BCD
		public static bool TryGetStoreDriverDelivery(out IStoreDriverDelivery component)
		{
			component = Components.storeDriverDelivery;
			return Components.storeDriverDelivery != null;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B9E1 File Offset: 0x00009BE1
		public static bool TryGetStoreDriverSubmission(out IStoreDriverSubmission component)
		{
			component = Components.storeDriverSubmission;
			return Components.storeDriverSubmission != null;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B9F5 File Offset: 0x00009BF5
		public static bool TryGetRemoteDeliveryComponent(out RemoteDeliveryComponent component)
		{
			component = Components.remoteDeliveryComponent;
			return Components.remoteDeliveryComponent != null;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000BA09 File Offset: 0x00009C09
		public static bool TryGetTransportIsMemberOfResolverComponent(out IsMemberOfResolverComponent<RoutingAddress> component)
		{
			component = Components.transportIsMemberOfResolverComponent;
			return Components.transportIsMemberOfResolverComponent != null;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000BA1D File Offset: 0x00009C1D
		public static bool TryGetSmtpInComponent(out ISmtpInComponent component)
		{
			component = Components.smtpInComponent;
			return Components.smtpInComponent != null;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000BA31 File Offset: 0x00009C31
		public static bool TryGetAggregator(out IStartableTransportComponent component)
		{
			component = Components.aggregator;
			return Components.aggregator != null;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000BA45 File Offset: 0x00009C45
		public static bool TryGetConfigurationComponent(out ITransportConfiguration component)
		{
			component = Components.configurationComponent;
			return Components.configurationComponent != null;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000BA59 File Offset: 0x00009C59
		public static bool TryGetMessagingDatabaseComponent(out MessagingDatabaseComponent component)
		{
			component = Components.messagingDatabaseComponent;
			return Components.messagingDatabaseComponent != null;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000BA6D File Offset: 0x00009C6D
		public static bool TryGetPickupComponent(out PickupComponent component)
		{
			component = Components.pickupComponent;
			return Components.pickupComponent != null;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000BA81 File Offset: 0x00009C81
		public static bool TryGetStoreDriverLoaderComponent(out IStartableTransportComponent component)
		{
			component = Components.storeDriverLoaderComponent;
			return Components.storeDriverLoaderComponent != null;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000BA95 File Offset: 0x00009C95
		public static bool TryGetBootScanner(out IStartableTransportComponent component)
		{
			component = Components.bootScanner;
			return Components.bootScanner != null;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000BAA9 File Offset: 0x00009CA9
		public static bool TryGetShadowRedundancyComponent(out ShadowRedundancyComponent component)
		{
			component = Components.shadowRedundancyComponent;
			return Components.shadowRedundancyComponent != null;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000BABD File Offset: 0x00009CBD
		public static bool TryGetMessageResubmissionComponent(out MessageResubmissionComponent component)
		{
			component = Components.messageResubmissionComponent;
			return Components.messageResubmissionComponent != null;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000BAD1 File Offset: 0x00009CD1
		public static bool TryGetEnhancedDnsComponent(out EnhancedDns component)
		{
			component = Components.enhancedDnsComponent;
			return component != null;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000BAE2 File Offset: 0x00009CE2
		public static bool TryGetMeteringComponent(out IMeteringComponent component)
		{
			component = Components.meteringComponent;
			return Components.meteringComponent != null;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000BAF6 File Offset: 0x00009CF6
		public static bool TryGetQueueQuotaComponent(out IQueueQuotaComponent component)
		{
			component = Components.queueQuotaComponent;
			return Components.queueQuotaComponent != null;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000BB0A File Offset: 0x00009D0A
		public static bool TryGetProcessingQuotaComponent(out IProcessingQuotaComponent component)
		{
			component = Components.processingQuotaComponent;
			return Components.processingQuotaComponent != null;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000BB1E File Offset: 0x00009D1E
		public static void SetRootComponent(SequentialTransportComponent rootComponent)
		{
			Components.rootComponent = rootComponent;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000BB26 File Offset: 0x00009D26
		public static void SetDatabaseComponents(SequentialTransportComponent databaseComponents)
		{
			Components.databaseComponents = databaseComponents;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000BB30 File Offset: 0x00009D30
		public static bool TryLoadTransportAppConfig(out string exceptionMessage)
		{
			exceptionMessage = null;
			bool result;
			try
			{
				Components.transportAppConfig = TransportAppConfig.Load();
				result = true;
			}
			catch (ConfigurationErrorsException ex)
			{
				result = false;
				exceptionMessage = ex.Message;
				ExTraceGlobals.GeneralTracer.TraceError(0L, string.Format("Failed to load configuration file. {0}.", ex.Message));
			}
			return result;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000BB88 File Offset: 0x00009D88
		public static void StopService(string reason, bool canRetry, bool retryAlways, bool failServiceWithException)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Components StopService");
			Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_StopService, null, new object[]
			{
				reason
			});
			if (Components.stopServiceHandler != null)
			{
				Components.stopServiceHandler(reason, canRetry, retryAlways, failServiceWithException);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		public static string OnUnhandledException(Exception e)
		{
			if (Components.rootComponent != null)
			{
				return Components.rootComponent.OnUnhandledException(e);
			}
			return null;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		public static string CurrentStateRepresentation()
		{
			string result;
			if (Components.ShuttingDown)
			{
				result = string.Format("Transport is shutting down: Following is the current state of the components: {0}", Components.rootComponent.CurrentState);
			}
			else
			{
				result = string.Format("Transport: Active = {0}, Paused={1}", Components.IsActive, Components.IsPaused);
			}
			return result;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BC3B File Offset: 0x00009E3B
		public static void HandleConnection(Socket clientConnection)
		{
			if (Components.smtpInComponent != null)
			{
				Components.SmtpInComponent.HandleConnection(clientConnection);
				return;
			}
			clientConnection.Close();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000BC58 File Offset: 0x00009E58
		public void Start(Components.StopServiceHandler stopServiceHandler, bool passiveRole, bool serviceControlled, bool selfListening, bool paused)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Start called");
			Components.serviceControlled = serviceControlled;
			if (Components.smtpInComponent != null)
			{
				Components.smtpInComponent.SelfListening = selfListening;
			}
			Components.stopServiceHandler = stopServiceHandler;
			Components.paused = paused;
			int availableMemoryInMBytes = Components.GetAvailableMemoryInMBytes();
			this.RegisterForDeletedObjectNotificationsInDatacenter();
			if (availableMemoryInMBytes < Components.TransportAppConfig.WorkerProcess.FreeMemoryRequiredToStartInMbytes)
			{
				string text = string.Format("TotalMemoryAvailableInMB:{0} MemoryRequiredToStartServiceInMB:{1} {2}", availableMemoryInMBytes, Components.TransportAppConfig.WorkerProcess.FreeMemoryRequiredToStartInMbytes, Components.GetProcessMemoryUsage());
				ExWatson.SendGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, "15.00.1497.010", Assembly.GetExecutingAssembly().GetName().Name, "OutOfMemoryException", "Startup", "Startup", "Component.Start", text);
				Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_NotEnoughMemoryToStartService, null, new object[]
				{
					text
				});
				Components.StopService(string.Format("Memory Available {0}MB less than required {1}MB", availableMemoryInMBytes, Components.TransportAppConfig.WorkerProcess.FreeMemoryRequiredToStartInMbytes), true, true, false);
			}
			if (!passiveRole)
			{
				this.ProtectedActivate();
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000BD80 File Offset: 0x00009F80
		public void Stop()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug<int>((long)this.GetHashCode(), "Stop {0}: initiated", Environment.TickCount);
			if (Interlocked.Exchange(ref Components.busyExit, 1) != 0)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Already working on exit procedure");
				return;
			}
			DisposeTracker.Suppressed = true;
			lock (Components.SyncRoot)
			{
				if (Components.IsActive)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<int>((long)this.GetHashCode(), "Stop {0}: signal background", Environment.TickCount);
					if (Components.bootScanner != null)
					{
						Components.BootScanner.Pause();
					}
					TransportFacades.Stop();
					Components.rootComponent.Stop();
					Components.rootComponent.Unload();
				}
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000BE4C File Offset: 0x0000A04C
		public void Pause()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Pause called");
			lock (Components.SyncRoot)
			{
				Components.paused = true;
				if (Components.resourceManagerComponent != null)
				{
					Components.ResourceManagerComponent.ResourceManager.RefreshComponentsState();
				}
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public void Continue()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Continue called");
			lock (Components.SyncRoot)
			{
				Components.paused = false;
				if (Components.IsActive && !Components.ShuttingDown)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Initiate Continue");
					if (Components.resourceManagerComponent != null)
					{
						Components.ResourceManagerComponent.ResourceManager.RefreshComponentsState();
					}
				}
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000BF48 File Offset: 0x0000A148
		public void Retire()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Retire called");
			if (Interlocked.Exchange(ref Components.busyExit, 1) != 0)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Already working on exit procedure");
				return;
			}
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Exit procedure started");
			lock (Components.SyncRoot)
			{
				if (Components.IsActive)
				{
					if (Components.bootScanner != null)
					{
						Components.BootScanner.Pause();
					}
					if (Components.smtpInComponent != null)
					{
						Components.SmtpInComponent.RejectCommands();
					}
					if (Components.pickupComponent != null)
					{
						Components.PickupComponent.Stop();
					}
					if (Components.storeDriver != null)
					{
						Components.StoreDriver.Retire();
					}
					if (Components.storeDriverDelivery != null)
					{
						Components.StoreDriverDelivery.Retire();
					}
					if (Components.storeDriverSubmission != null)
					{
						Components.StoreDriverSubmission.Retire();
					}
					if (Components.categorizerComponent != null)
					{
						Components.CategorizerComponent.Retire();
					}
					Components.retireTimer = new Timer(new TimerCallback(this.RetireCompleted), null, 15000, -1);
				}
				else
				{
					ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Retire signalled, components are not activated.");
					Environment.Exit(0);
				}
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C090 File Offset: 0x0000A290
		public void ProtectedActivate()
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(new ADOperation(this.Activate), 0);
			if (!adoperationResult.Succeeded)
			{
				string text = "Failed to read configuration. The Transport Service will be stopped.";
				ExTraceGlobals.ConfigurationTracer.TraceError(0L, text);
				Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ActivationFailed, null, new object[]
				{
					adoperationResult.Exception.ToString()
				});
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, text, ResultSeverityLevel.Warning, false);
				Components.StopService(Strings.ActivationFailed, adoperationResult.ErrorCode == ADOperationErrorCode.RetryableError, false, false);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000C158 File Offset: 0x0000A358
		public void Activate()
		{
			ICategorizerComponentFacade categorizerComponentFacade2;
			if (Components.categorizerComponent == null)
			{
				ICategorizerComponentFacade categorizerComponentFacade = new NullCategorizer();
				categorizerComponentFacade2 = categorizerComponentFacade;
			}
			else
			{
				categorizerComponentFacade2 = Components.CategorizerComponent;
			}
			ICategorizerComponentFacade categorizerComponentFacade3 = categorizerComponentFacade2;
			TransportFacades.Initialize(Components.Dns, categorizerComponentFacade3, Components.shadowRedundancyComponent, Components.ConfigChanged, new NewMailItemDelegate(TransportMailItem.NewAgentMailItem), new EnsureSecurityAttributesDelegate(MultilevelAuth.EnsureSecurityAttributesByAgent), new TrackReceiveByAgentDelegate(MessageTrackingLog.TrackReceiveByAgent), new TrackRecipientAddByAgentDelegate(MessageTrackingLog.TrackRecipientAddByAgent), new ReadHistoryFromMailItemByAgentDelegate(History.ReadFromMailItemByAgent), new ReadHistoryFromRecipientByAgentDelegate(History.ReadFromRecipientByAgent), new CreateAndSubmitApprovalInitiationForTransportRulesDelegate(ApprovalInitiation.CreateAndSubmitApprovalInitiationForTransportRules));
			lock (Components.SyncRoot)
			{
				Stopwatch stopwatch = new Stopwatch();
				Stopwatch stopwatch2 = new Stopwatch();
				TimerCallback callback = delegate(object state)
				{
					Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ActivationSlow, null, new object[]
					{
						Components.rootComponent.CurrentState
					});
				};
				Timer timer = new Timer(callback, null, Components.maxActivateSecondsToLog, TimeSpan.FromMilliseconds(-1.0));
				try
				{
					Components.SetLoadTimeDependencies();
					stopwatch2.Start();
					Components.rootComponent.Load();
					stopwatch2.Stop();
					Components.SetRunTimeDependencies();
					stopwatch.Start();
					Components.rootComponent.Start(true, this.ServiceState);
					stopwatch.Stop();
					Components.active = true;
					if (Components.categorizerComponent != null)
					{
						Components.CategorizerComponent.DataAvail();
					}
					if (!Components.IsPaused)
					{
						this.Continue();
					}
				}
				catch (TransportComponentLoadFailedException ex)
				{
					ExTraceGlobals.ConfigurationTracer.TraceError(0L, "Failed to load components. The Transport Service will be stopped.");
					bool canRetry = Components.HasTransientException(ex);
					Components.StopService(ex.Message, canRetry, false, false);
				}
				finally
				{
					timer.Dispose();
					if (stopwatch2.Elapsed + stopwatch.Elapsed > Components.maxActivateSecondsToLog)
					{
						Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ActivationTiming, null, new object[]
						{
							stopwatch2.Elapsed.ToString(),
							stopwatch.Elapsed.ToString(),
							string.Format("\n{0}\n{1}", Components.rootComponent.LoadTimings, Components.rootComponent.StartTimings)
						});
					}
				}
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public void ConfigUpdate()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Components ConfigUpdate");
			lock (Components.SyncRoot)
			{
				if (!Components.ShuttingDown && Components.IsActive)
				{
					EventHandler configChanged = Components.ConfigChanged;
					if (configChanged != null)
					{
						configChanged(null, null);
					}
					Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ConfigUpdateOccurred, null, new object[0]);
				}
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000C44C File Offset: 0x0000A64C
		internal static void InstantiateComponentsForTest()
		{
			TransportAppConfig.IsMemberOfResolverConfiguration transportIsMemberOfResolverConfig = Components.TransportAppConfig.TransportIsMemberOfResolverConfig;
			TransportAppConfig.IsMemberOfResolverConfiguration mailboxRulesIsMemberOfResolverConfig = Components.TransportAppConfig.MailboxRulesIsMemberOfResolverConfig;
			IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver adAdapter = new IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver(transportIsMemberOfResolverConfig.DisableDynamicGroups);
			IsMemberOfResolverADAdapter<string>.LegacyDNResolver adAdapter2 = new IsMemberOfResolverADAdapter<string>.LegacyDNResolver(mailboxRulesIsMemberOfResolverConfig.DisableDynamicGroups);
			StorageFactory.SchemaToUse = StorageFactory.Schema.NullSchema;
			Components.agentComponent = (Components.agentComponent ?? new AgentComponent());
			Components.messagingDatabaseComponent = (Components.messagingDatabaseComponent ?? new MessagingDatabaseComponent());
			Components.bootScanner = (Components.bootScanner ?? Components.messagingDatabaseComponent.CreateBootScanner());
			Components.routingComponent = (Components.routingComponent ?? new RoutingComponent());
			Components.unhealthyTargetFilterComponent = (Components.unhealthyTargetFilterComponent ?? new UnhealthyTargetFilterComponent());
			Components.enhancedDnsComponent = (Components.enhancedDnsComponent ?? new EnhancedDns());
			Components.categorizerComponent = (Components.categorizerComponent ?? new CategorizerComponent());
			Components.proxyHubSelectorComponent = (Components.proxyHubSelectorComponent ?? new ProxyHubSelectorComponent());
			Components.certificateComponent = (Components.certificateComponent ?? new CertificateComponent());
			Components.configurationComponent = (Components.configurationComponent ?? new ConfigurationComponent());
			Components.deliveryAgentConnectionHandler = (Components.deliveryAgentConnectionHandler ?? new DeliveryAgentConnectionHandler());
			Components.dsnGenerator = (Components.dsnGenerator ?? new DsnGenerator());
			Components.messageResubmissionComponent = (Components.messageResubmissionComponent ?? new MessageResubmissionComponent());
			Components.mailboxRulesIsMemberOfResolverComponent = (Components.mailboxRulesIsMemberOfResolverComponent ?? new IsMemberOfResolverComponent<string>("MailboxRules", mailboxRulesIsMemberOfResolverConfig, adAdapter2));
			Components.messageThrottlingComponent = (Components.messageThrottlingComponent ?? new MessageThrottlingComponent());
			Components.orarGenerator = (Components.orarGenerator ?? new OrarGenerator());
			Components.pickupComponent = (Components.pickupComponent ?? new PickupComponent(new Components.TransportPickupSubmissionHandler()));
			Components.queueManager = (Components.queueManager ?? new QueueManager());
			Components.messageDepotComponent = (Components.messageDepotComponent ?? new MessageDepotComponent());
			Components.messageDepotQueueViewerComponent = (Components.messageDepotQueueViewerComponent ?? new MessageDepotQueueViewerComponent());
			Components.dsnSchedulerComponent = (Components.dsnSchedulerComponent ?? new DsnSchedulerComponent());
			Components.remoteDeliveryComponent = (Components.remoteDeliveryComponent ?? new RemoteDeliveryComponent());
			Components.resourceManagerComponent = (Components.resourceManagerComponent ?? new ResourceManagerComponent(ResourceManagerResources.All));
			Components.rmsClientManagerComponent = (Components.rmsClientManagerComponent ?? new RmsClientManagerComponent());
			Components.shadowRedundancyComponent = (Components.shadowRedundancyComponent ?? new ShadowRedundancyComponent());
			Components.smtpInComponent = (Components.smtpInComponent ?? new SmtpInComponent(false));
			Components.smtpOutConnectionHandler = (Components.smtpOutConnectionHandler ?? new SmtpOutConnectionHandler());
			Components.storeDriverLoaderComponent = (Components.storeDriverLoaderComponent ?? new Components.StoreDriverLoader());
			Components.systemCheckComponent = (Components.systemCheckComponent ?? new SystemCheckComponent());
			Components.transportIsMemberOfResolverComponent = (Components.transportIsMemberOfResolverComponent ?? new IsMemberOfResolverComponent<RoutingAddress>("Transport", transportIsMemberOfResolverConfig, adAdapter));
			Components.poisonMessageComponent = (Components.poisonMessageComponent ?? new PoisonMessage());
			Components.loggingComponent = (Components.loggingComponent ?? new Components.LoggingComponent(false, false, false, false, false));
			Components.queueQuotaComponent = (Components.queueQuotaComponent ?? new QueueQuotaComponent());
			Components.meteringComponent = (Components.meteringComponent ?? new MeteringComponent());
			Components.processingQuotaComponent = (Components.processingQuotaComponent ?? new ProcessingQuotaComponent());
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000C74C File Offset: 0x0000A94C
		private static void SetLoadTimeDependencies()
		{
			if (Components.systemCheckComponent != null)
			{
				Components.SystemCheckComponent.SetLoadTimeDependencies(new SystemCheckConfig(null), Components.TransportAppConfig, Components.Configuration);
			}
			if (Components.smtpInComponent != null)
			{
				Components.SmtpInComponent.SetLoadTimeDependencies(Components.TransportAppConfig, Components.Configuration);
			}
			if (Components.smtpOutConnectionHandler != null)
			{
				Components.SmtpOutConnectionHandler.SetLoadTimeDependencies(Components.RoutingComponent.MailRouter, Components.TransportAppConfig, Components.Configuration, Components.SmtpInComponent, Components.Logging);
			}
			if (Components.routingComponent != null)
			{
				Components.routingComponent.SetLoadTimeDependencies(Components.TransportAppConfig, Components.Configuration);
			}
			if (Components.proxyHubSelectorComponent != null)
			{
				Components.proxyHubSelectorComponent.SetLoadTimeDependencies(Components.RoutingComponent.MailRouter, Components.Configuration);
			}
			if (Components.transportAppConfig != null)
			{
				Components.TransportAppConfig.QueueDatabase.SetLoadTimeDependencies(Components.Configuration);
			}
			if (Components.messagingDatabaseComponent != null)
			{
				Components.messagingDatabaseComponent.SetLoadTimeDependencies(Components.TransportAppConfig.QueueDatabase);
			}
			if (Components.shadowRedundancyComponent != null)
			{
				Components.shadowRedundancyComponent.SetLoadTimeDependencies(Components.MessagingDatabase.Database, Components.bootScanner);
			}
			if (Components.messageDepotComponent != null)
			{
				Components.messageDepotComponent.SetLoadTimeDependencies(new MessageDepotConfig(null));
			}
			if (Components.messageDepotQueueViewerComponent != null)
			{
				Components.messageDepotQueueViewerComponent.SetLoadTimeDependencies(Components.messageDepotComponent, Components.TransportAppConfig.QueueConfiguration);
			}
			if (Components.processingSchedulerComponent != null)
			{
				Components.processingSchedulerComponent.SetLoadTimeDependencies(Components.Configuration, Components.messageDepotComponent, Components.categorizerAdapterComponent, Components.messagingDatabaseComponent);
			}
			if (Components.bootScanner != null)
			{
				Components.bootScanner.SetLoadTimeDependencies(Components.EventLogger, Components.MessagingDatabase.Database, Components.ShadowRedundancyComponent, Components.PoisonMessageComponent, Components.CategorizerComponent, Components.QueueManager, Components.TransportAppConfig.BootLoader);
			}
			if (Components.messageResubmissionComponent != null && Components.messagingDatabaseComponent != null && Components.shadowRedundancyComponent != null)
			{
				Components.messageResubmissionComponent.SetLoadTimeDependencies(Components.TransportAppConfig.MessageResubmission, Components.messagingDatabaseComponent.Database);
			}
			if (Components.processingQuotaComponent != null || Components.messageDepotComponent != null)
			{
				Components.categorizer.SetLoadTimeDependencies(Components.ProcessingQuotaComponent, Components.messageDepotComponent);
				Components.processingQuotaComponent.SetLoadTimeDependencies(Components.TransportAppConfig.ProcessingQuota);
			}
			if (Components.dsnSchedulerComponent != null)
			{
				Components.dsnSchedulerComponent.SetLoadTimeDependencies(Components.messageDepotComponent, Components.DsnGenerator, Components.orarGenerator, new MessageTrackingLogWrapper(), Components.configurationComponent);
			}
			if (Components.meteringComponent != null)
			{
				Components.meteringComponent.SetLoadtimeDependencies(new MeteringConfig(), ExTraceGlobals.QueuingTracer);
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		private static void SetRunTimeDependencies()
		{
			ICategorizer categorizer = (Components.categorizer != null) ? Components.categorizer : new NullCategorizer();
			if (Components.queueQuotaComponent != null)
			{
				ICountTracker<MeteredEntity, MeteredCount> metering = null;
				IMeteringComponent meteringComponent;
				if (Components.TryGetMeteringComponent(out meteringComponent))
				{
					metering = meteringComponent.Metering;
				}
				QueueQuotaConfig queueQuotaConfig = new QueueQuotaConfig(Components.TransportAppConfig.FlowControlLog, Components.TransportAppConfig.QueueConfiguration);
				Components.queueQuotaComponent.SetRunTimeDependencies(queueQuotaConfig, Components.loggingComponent.FlowControlLog, new QueueQuotaComponentPerformanceCountersWrapper(queueQuotaConfig.RecentPerfCounterTrackingInterval, queueQuotaConfig.RecentPerfCounterTrackingBucketSize), Components.processingQuotaComponent, SubmitMessageQueue.Instance, Components.RemoteDeliveryComponent, metering);
			}
			if (Components.smtpInComponent != null)
			{
				Components.SmtpInComponent.SetRunTimeDependencies(Components.AgentComponent, Components.RoutingComponent.MailRouter, (Components.proxyHubSelectorComponent != null) ? Components.proxyHubSelectorComponent.ProxyHubSelector : null, Components.EnhancedDns, categorizer, Components.CertificateComponent.Cache, Components.CertificateComponent.Validator, Components.TransportIsMemberOfResolverComponent.IsMemberOfResolver, Components.MessagingDatabase.Database, Components.MessageThrottlingComponent.MessageThrottlingManager, (Components.shadowRedundancyComponent != null) ? Components.shadowRedundancyComponent.ShadowRedundancyManager : null, Components.SmtpOutConnectionHandler, Components.queueQuotaComponent);
			}
			if (Components.smtpOutConnectionHandler != null)
			{
				Components.SmtpOutConnectionHandler.SetRunTimeDependencies(Components.EnhancedDns, Components.UnhealthyTargetFilterComponent, Components.CertificateComponent.Cache, Components.CertificateComponent.Validator, (Components.shadowRedundancyComponent != null) ? Components.shadowRedundancyComponent.ShadowRedundancyManager : null);
			}
			if (Components.routingComponent != null)
			{
				Components.routingComponent.SetRunTimeDependencies(Components.shadowRedundancyComponent, Components.UnhealthyTargetFilterComponent, Components.categorizerComponent);
			}
			if (Components.enhancedDnsComponent != null)
			{
				Components.enhancedDnsComponent.SetRunTimeDependencies(Components.RoutingComponent.MailRouter);
			}
			if (Components.messageResubmissionComponent != null && Components.shadowRedundancyComponent != null && Components.shadowRedundancyComponent.ShadowRedundancyManager != null)
			{
				Components.messageResubmissionComponent.SetRunTimeDependencies(Components.shadowRedundancyComponent.ShadowRedundancyManager.PrimaryServerInfos);
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000CB68 File Offset: 0x0000AD68
		private static Exception FaultInjectionCallback(string exceptionType)
		{
			Exception ex = null;
			LocalizedString value = new LocalizedString("Fault injection.");
			if (exceptionType != null && exceptionType.Equals("System.ArgumentException"))
			{
				ex = new ArgumentException("Test Fault Exception");
			}
			if (ex == null)
			{
				ex = new Exception(value);
			}
			return ex;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		private static bool HasTransientException(Exception exception)
		{
			while (exception != null)
			{
				if (exception is TransientException)
				{
					return true;
				}
				exception = exception.InnerException;
			}
			return false;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000CBC8 File Offset: 0x0000ADC8
		private static void LoadStoreDriverAssembly()
		{
			AssemblyName name = Assembly.GetExecutingAssembly().GetName();
			string assemblyString = name.FullName.Replace(name.Name, "Microsoft.Exchange.StoreDriver");
			Components.storeDriverAssembly = Assembly.Load(assemblyString);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		public static string GetProcessMemoryUsage()
		{
			try
			{
				IEnumerable<string> instanceNames = new PerformanceCounterCategory("Process").GetInstanceNames();
				List<KeyValuePair<string, float>> list = new List<KeyValuePair<string, float>>();
				foreach (string text in instanceNames)
				{
					using (PerformanceCounter performanceCounter = new PerformanceCounter("Process", "Working Set", text, true))
					{
						list.Add(new KeyValuePair<string, float>(text, performanceCounter.NextValue()));
					}
				}
				return string.Format("TopProcesses @{0} :\n {1}", DateTime.UtcNow, string.Join("\n", from t in (from k in list
				orderby k.Value descending
				select k).Take(21)
				select string.Format("{0}:{1:N0}", t.Key, t.Value)));
			}
			catch (InvalidOperationException)
			{
			}
			catch (Win32Exception)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return "Unable to query performance counters to get the top processes";
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000CD68 File Offset: 0x0000AF68
		private static int GetAvailableMemoryInMBytes()
		{
			try
			{
				NativeMethods.MemoryStatusEx memoryStatusEx;
				if (NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx))
				{
					ulong num = memoryStatusEx.AvailPhys / 1048576UL;
					return (int)num;
				}
			}
			catch (Win32Exception)
			{
			}
			return int.MaxValue;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000CDB0 File Offset: 0x0000AFB0
		private void RetireCompleted(object obj)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Retire completed");
			if (Components.smtpInComponent != null)
			{
				Components.SmtpInComponent.RejectSubmits();
			}
			Components.rootComponent.Stop();
			Components.rootComponent.Unload();
			ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Exit");
			Environment.Exit(0);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000CE14 File Offset: 0x0000B014
		private void RegisterForDeletedObjectNotificationsInDatacenter()
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(new ADOperation(TransportADNotificationAdapter.Instance.RegisterForEdgeTransportEvents), 0);
			if (!adoperationResult.Succeeded)
			{
				string text = "Failed to read configuration. The Transport Service will be stopped.";
				ExTraceGlobals.ConfigurationTracer.TraceError(0L, text);
				Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ActivationFailed, null, new object[]
				{
					adoperationResult.Exception.ToString()
				});
				EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, text, ResultSeverityLevel.Warning, false);
				Components.StopService(Strings.ActivationFailed, adoperationResult.ErrorCode == ADOperationErrorCode.RetryableError, false, false);
			}
		}

		// Token: 0x0400016D RID: 365
		public const string SmtpPrincipalName = "SMTP";

		// Token: 0x0400016E RID: 366
		private const int RetirePeriodSec = 15;

		// Token: 0x0400016F RID: 367
		private static TransportAppConfig transportAppConfig;

		// Token: 0x04000170 RID: 368
		private static TimeSpan maxActivateSecondsToLog = TimeSpan.FromSeconds(15.0);

		// Token: 0x04000171 RID: 369
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000172 RID: 370
		private static SequentialTransportComponent rootComponent;

		// Token: 0x04000173 RID: 371
		private static SequentialTransportComponent databaseComponents;

		// Token: 0x04000174 RID: 372
		private static SmtpOutConnectionHandler smtpOutConnectionHandler;

		// Token: 0x04000175 RID: 373
		private static ISmtpInComponent smtpInComponent;

		// Token: 0x04000176 RID: 374
		private static MessagingDatabaseComponent messagingDatabaseComponent;

		// Token: 0x04000177 RID: 375
		private static CategorizerComponent categorizerComponent;

		// Token: 0x04000178 RID: 376
		private static CategorizerAdapterComponent categorizerAdapterComponent;

		// Token: 0x04000179 RID: 377
		private static SchedulerAdapterComponent schedulerAdapterComponent;

		// Token: 0x0400017A RID: 378
		private static Components.LoggingComponent loggingComponent;

		// Token: 0x0400017B RID: 379
		private static ICategorizer categorizer;

		// Token: 0x0400017C RID: 380
		private static IRoutingComponent routingComponent;

		// Token: 0x0400017D RID: 381
		private static UnhealthyTargetFilterComponent unhealthyTargetFilterComponent;

		// Token: 0x0400017E RID: 382
		private static EnhancedDns enhancedDnsComponent;

		// Token: 0x0400017F RID: 383
		private static IProxyHubSelectorComponent proxyHubSelectorComponent;

		// Token: 0x04000180 RID: 384
		private static ITransportConfiguration configurationComponent;

		// Token: 0x04000181 RID: 385
		private static AgentComponent agentComponent;

		// Token: 0x04000182 RID: 386
		private static OrarGenerator orarGenerator;

		// Token: 0x04000183 RID: 387
		private static DsnGenerator dsnGenerator;

		// Token: 0x04000184 RID: 388
		private static ClassificationConfig classificationConfig = new ClassificationConfig();

		// Token: 0x04000185 RID: 389
		private static PickupComponent pickupComponent;

		// Token: 0x04000186 RID: 390
		private static ResourceManagerComponent resourceManagerComponent;

		// Token: 0x04000187 RID: 391
		private static ResourceThrottlingComponent resourceThrottlingComponent;

		// Token: 0x04000188 RID: 392
		private static RmsClientManagerComponent rmsClientManagerComponent;

		// Token: 0x04000189 RID: 393
		private static RemoteDeliveryComponent remoteDeliveryComponent;

		// Token: 0x0400018A RID: 394
		private static DeliveryAgentConnectionHandler deliveryAgentConnectionHandler;

		// Token: 0x0400018B RID: 395
		private static QueueManager queueManager;

		// Token: 0x0400018C RID: 396
		private static IProcessingSchedulerComponent processingSchedulerComponent;

		// Token: 0x0400018D RID: 397
		private static IMessageDepotComponent messageDepotComponent;

		// Token: 0x0400018E RID: 398
		private static IMessageDepotQueueViewerComponent messageDepotQueueViewerComponent;

		// Token: 0x0400018F RID: 399
		private static DsnSchedulerComponent dsnSchedulerComponent;

		// Token: 0x04000190 RID: 400
		private static MessageResubmissionComponent messageResubmissionComponent;

		// Token: 0x04000191 RID: 401
		private static ShadowRedundancyComponent shadowRedundancyComponent;

		// Token: 0x04000192 RID: 402
		private static MessageThrottlingComponent messageThrottlingComponent;

		// Token: 0x04000193 RID: 403
		private static PoisonMessage poisonMessageComponent;

		// Token: 0x04000194 RID: 404
		private static IsMemberOfResolverComponent<RoutingAddress> transportIsMemberOfResolverComponent;

		// Token: 0x04000195 RID: 405
		private static IsMemberOfResolverComponent<string> mailboxRulesIsMemberOfResolverComponent;

		// Token: 0x04000196 RID: 406
		private static IStoreDriver storeDriver;

		// Token: 0x04000197 RID: 407
		private static IStoreDriverDelivery storeDriverDelivery;

		// Token: 0x04000198 RID: 408
		private static IStoreDriverSubmission storeDriverSubmission;

		// Token: 0x04000199 RID: 409
		private static Components.StoreDriverLoader storeDriverLoaderComponent;

		// Token: 0x0400019A RID: 410
		private static IStartableTransportComponent aggregator;

		// Token: 0x0400019B RID: 411
		private static IQueueQuotaComponent queueQuotaComponent;

		// Token: 0x0400019C RID: 412
		private static IMeteringComponent meteringComponent;

		// Token: 0x0400019D RID: 413
		private static IProcessingQuotaComponent processingQuotaComponent;

		// Token: 0x0400019E RID: 414
		private static Assembly storeDriverAssembly;

		// Token: 0x0400019F RID: 415
		private static IBootLoader bootScanner;

		// Token: 0x040001A0 RID: 416
		private static IPFilterAdminServer adminIPFilterServer;

		// Token: 0x040001A1 RID: 417
		private static QueueViewerServer queueViewerServer;

		// Token: 0x040001A2 RID: 418
		private static RpcServerWrapper offlineRmsServer;

		// Token: 0x040001A3 RID: 419
		private static MessageResubmissionRpcServerImpl resubmissionServer;

		// Token: 0x040001A4 RID: 420
		private static CertificateComponent certificateComponent;

		// Token: 0x040001A5 RID: 421
		private static IPerfCountersLoader perfCountersLoader;

		// Token: 0x040001A6 RID: 422
		private static SystemCheckComponent systemCheckComponent;

		// Token: 0x040001A7 RID: 423
		private static int instanceId = Process.GetCurrentProcess().Id;

		// Token: 0x040001A8 RID: 424
		private static int busyExit;

		// Token: 0x040001A9 RID: 425
		private static bool active;

		// Token: 0x040001AA RID: 426
		private static bool paused;

		// Token: 0x040001AB RID: 427
		private static Timer retireTimer;

		// Token: 0x040001AC RID: 428
		private static bool serviceControlled;

		// Token: 0x040001AD RID: 429
		private static Components.StopServiceHandler stopServiceHandler;

		// Token: 0x040001AE RID: 430
		private static object syncRoot = new object();

		// Token: 0x040001AF RID: 431
		private readonly string hostServiceName;

		// Token: 0x040001B0 RID: 432
		private readonly bool stateManagementEnabled;

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x060002D7 RID: 727
		public delegate void StopServiceHandler(string reason, bool canRetry, bool retryAlways, bool failServiceWithException);

		// Token: 0x02000059 RID: 89
		internal class ServicePrincipalNameRegistrar : ITransportComponent
		{
			// Token: 0x060002DA RID: 730 RVA: 0x0000CEAC File Offset: 0x0000B0AC
			public void Load()
			{
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					if (!current.User.IsWellKnown(WellKnownSidType.NetworkServiceSid) && !current.User.IsWellKnown(WellKnownSidType.LocalSystemSid))
					{
						return;
					}
				}
				bool flag;
				int num = LsaPolicy.GetDomainMembershipStatus(out flag);
				if (num != 0)
				{
					flag = true;
				}
				if (flag)
				{
					num = ServicePrincipalName.RegisterServiceClass("SmtpSvc");
					if (num != 0)
					{
						Win32Exception ex = new Win32Exception(num);
						Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SpnRegisterFailure, null, new object[]
						{
							"SmtpSvc",
							ex.Message
						});
					}
					num = ServicePrincipalName.RegisterServiceClass("SMTP");
					if (num != 0)
					{
						Win32Exception ex2 = new Win32Exception(num);
						Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SpnRegisterFailure, null, new object[]
						{
							"SMTP",
							ex2.Message
						});
					}
					return;
				}
			}

			// Token: 0x060002DB RID: 731 RVA: 0x0000CF94 File Offset: 0x0000B194
			public void Unload()
			{
			}

			// Token: 0x060002DC RID: 732 RVA: 0x0000CF96 File Offset: 0x0000B196
			public string OnUnhandledException(Exception e)
			{
				return null;
			}
		}

		// Token: 0x0200005A RID: 90
		internal class RpcServerComponent : IStartableTransportComponent, ITransportComponent
		{
			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060002DE RID: 734 RVA: 0x0000CFA1 File Offset: 0x0000B1A1
			public string CurrentState
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060002DF RID: 735 RVA: 0x0000CFA4 File Offset: 0x0000B1A4
			public void Load()
			{
			}

			// Token: 0x060002E0 RID: 736 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
			public void Start(bool initiallyPaused, ServiceState targetRunningState)
			{
				bool isLocalOnly = !Components.IsBridgehead;
				Components.adminIPFilterServer = (IPFilterAdminServer)RpcServerBase.RegisterServer(typeof(IPFilterAdminServer), Components.Configuration.LocalServer.TransportServerSecurity, 131112, isLocalOnly);
				Components.queueViewerServer = (QueueViewerServer)RpcServerBase.RegisterServer(typeof(QueueViewerServer), Components.Configuration.LocalServer.TransportServerSecurity, 131220, isLocalOnly);
				if (Components.IsBridgehead)
				{
					Components.offlineRmsServer = (RpcServerWrapper)RpcServerBase.RegisterServer(typeof(RpcServerWrapper), Components.Configuration.LocalServer.TransportServerSecurity, 131220, false);
					ObjectSecurity localSystemSecurity = this.GetLocalSystemSecurity();
					ObjectSecurity serverAdminSecurity = this.GetServerAdminSecurity();
					Components.resubmissionServer = (MessageResubmissionRpcServerImpl)RpcServerBase.RegisterServer(typeof(MessageResubmissionRpcServerImpl), serverAdminSecurity, 1, false, (uint)Components.Configuration.LocalServer.MaxConcurrentMailboxSubmissions);
					Components.resubmissionServer.LocalSystemSecurityDescriptor = localSystemSecurity;
				}
				Components.Configuration.LocalServerChanged += Components.RpcServerComponent.ConfigUpdate;
			}

			// Token: 0x060002E1 RID: 737 RVA: 0x0000D0AC File Offset: 0x0000B2AC
			public void Stop()
			{
				Components.Configuration.LocalServerChanged -= Components.RpcServerComponent.ConfigUpdate;
				RpcServerBase.StopServer(IPFilterRpcServer.RpcIntfHandle);
				RpcServerBase.StopServer(QueueViewerRpcServer.RpcIntfHandle);
				if (Components.IsBridgehead)
				{
					RpcServerBase.StopServer(OfflineRmsRpcServer.RpcIntfHandle);
					RpcServerBase.StopServer(MailSubmissionServiceRpcServer.RpcIntfHandle);
				}
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x0000D0FE File Offset: 0x0000B2FE
			public void Pause()
			{
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x0000D100 File Offset: 0x0000B300
			public void Continue()
			{
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x0000D102 File Offset: 0x0000B302
			public void Unload()
			{
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000D104 File Offset: 0x0000B304
			public string OnUnhandledException(Exception e)
			{
				return null;
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000D107 File Offset: 0x0000B307
			private static void ConfigUpdate(TransportServerConfiguration server)
			{
				Components.adminIPFilterServer.SecurityDescriptor = server.TransportServerSecurity;
				Components.queueViewerServer.SecurityDescriptor = server.TransportServerSecurity;
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x0000D12C File Offset: 0x0000B32C
			private ObjectSecurity GetServerAdminSecurity()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2901, "GetServerAdminSecurity", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Core\\start.cs");
				Server server = null;
				try
				{
					server = topologyConfigurationSession.FindLocalServer();
				}
				catch (LocalServerNotFoundException arg)
				{
					ExTraceGlobals.GeneralTracer.TraceError<LocalServerNotFoundException>(0L, "FindLocalServer failed: {0}", arg);
					return null;
				}
				RawSecurityDescriptor rawSecurityDescriptor = server.ReadSecurityDescriptor();
				if (rawSecurityDescriptor == null)
				{
					return null;
				}
				FileSecurity fileSecurity = new FileSecurity();
				byte[] array = new byte[rawSecurityDescriptor.BinaryLength];
				rawSecurityDescriptor.GetBinaryForm(array, 0);
				fileSecurity.SetSecurityDescriptorBinaryForm(array);
				IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2929, "GetServerAdminSecurity", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Core\\start.cs");
				SecurityIdentifier exchangeServersUsgSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
				FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(exchangeServersUsgSid, FileSystemRights.ReadData, AccessControlType.Allow);
				fileSecurity.SetAccessRule(fileSystemAccessRule);
				SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
				fileSystemAccessRule = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
				fileSecurity.AddAccessRule(fileSystemAccessRule);
				return fileSecurity;
			}

			// Token: 0x060002E8 RID: 744 RVA: 0x0000D224 File Offset: 0x0000B424
			private ObjectSecurity GetLocalSystemSecurity()
			{
				FileSecurity fileSecurity = new FileSecurity();
				IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(null, null, CultureInfo.CurrentCulture.LCID, true, ConsistencyMode.IgnoreInvalid, null, ADSessionSettings.FromRootOrgScopeSet(), 2957, "GetLocalSystemSecurity", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Core\\start.cs");
				SecurityIdentifier exchangeServersUsgSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
				FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(exchangeServersUsgSid, FileSystemRights.ReadData, AccessControlType.Allow);
				fileSecurity.SetAccessRule(fileSystemAccessRule);
				SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
				fileSystemAccessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
				fileSecurity.AddAccessRule(fileSystemAccessRule);
				fileSecurity.SetOwner(securityIdentifier);
				return fileSecurity;
			}
		}

		// Token: 0x0200005B RID: 91
		internal class LoggingComponent : ITransportComponent
		{
			// Token: 0x060002EA RID: 746 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
			public LoggingComponent(bool startMessageTrackingLog, bool startQueueLogging, bool startTransportWlmLog, bool startFlowControlLog, bool startDnsLog) : this(startMessageTrackingLog, startQueueLogging, startTransportWlmLog, startFlowControlLog, startDnsLog, null)
			{
			}

			// Token: 0x060002EB RID: 747 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
			public LoggingComponent(bool startMessageTrackingLog, bool startQueueLogging, bool startTransportWlmLog, bool startFlowControlLog, bool startDnsLog, string messageTrackingLogPrefix)
			{
				this.startMessageTrackingLog = startMessageTrackingLog;
				this.startQueueLogging = startQueueLogging;
				this.messageTrackingLogPrefix = messageTrackingLogPrefix;
				this.startTransportWlmLog = startTransportWlmLog;
				this.startFlowControlLog = startFlowControlLog;
				this.startDnsLog = startDnsLog;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060002EC RID: 748 RVA: 0x0000D343 File Offset: 0x0000B543
			public ProtocolLog SmtpSendLog
			{
				get
				{
					return this.smtpSendLog;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060002ED RID: 749 RVA: 0x0000D34B File Offset: 0x0000B54B
			public FlowControlLog FlowControlLog
			{
				get
				{
					return this.flowControlLog;
				}
			}

			// Token: 0x060002EE RID: 750 RVA: 0x0000D354 File Offset: 0x0000B554
			public void Load()
			{
				if (this.startMessageTrackingLog)
				{
					if (string.IsNullOrEmpty(this.messageTrackingLogPrefix))
					{
						MessageTrackingLog.Start();
					}
					else
					{
						MessageTrackingLog.Start(this.messageTrackingLogPrefix);
					}
					MessageTrackingLog.Configure(Components.Configuration.LocalServer.TransportServer);
				}
				this.smtpSendLog.Configure(Components.Configuration.LocalServer.TransportServer.SendProtocolLogPath, Components.Configuration.LocalServer.TransportServer.SendProtocolLogMaxAge, Components.Configuration.LocalServer.TransportServer.SendProtocolLogMaxDirectorySize, Components.Configuration.LocalServer.TransportServer.SendProtocolLogMaxFileSize, Components.Configuration.AppConfig.Logging.SmtpSendLogBufferSize, Components.Configuration.AppConfig.Logging.SmtpSendLogFlushInterval, Components.Configuration.AppConfig.Logging.SmtpSendLogAsyncInterval);
				Components.Configuration.LocalServerChanged += this.ConfigUpdate;
				ConnectionLog.Start();
				ConnectionLog.TransportStart(Components.Configuration.LocalServer.MaxConcurrentMailboxSubmissions, Components.Configuration.LocalServer.MaxConcurrentMailboxDeliveries, Components.Configuration.LocalServer.TransportServer.MaxOutboundConnections.ToString());
				if (this.systemProbeLoggingEnabled)
				{
					Components.LoggingComponent.StartSystemProbe();
				}
				if (this.startQueueLogging)
				{
					QueueLog.Start();
				}
				if (this.startTransportWlmLog)
				{
					TransportWlmLog.Start();
				}
				if (this.startFlowControlLog)
				{
					this.flowControlLog = new FlowControlLog();
					Server transportServer = Components.Configuration.LocalServer.TransportServer;
					if (!transportServer.FlowControlLogEnabled && (transportServer.FlowControlLogPath == null || string.IsNullOrEmpty(transportServer.FlowControlLogPath.PathName)))
					{
						ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Flow Control Log path was set to null, Flow Control Log is disabled");
						Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_FlowControlLogPathIsNull, null, new object[0]);
					}
					else
					{
						this.flowControlLog.Configure(Components.TransportAppConfig.FlowControlLog, Components.Configuration.LocalServer.TransportServer);
					}
				}
				if (this.startDnsLog && Components.Configuration.LocalServer.TransportServer.DnsLogEnabled)
				{
					string dnsLogPath = (Components.Configuration.LocalServer.TransportServer.DnsLogPath == null) ? null : Components.Configuration.LocalServer.TransportServer.DnsLogPath.PathName;
					DnsLog.Start(dnsLogPath, Components.Configuration.LocalServer.TransportServer.DnsLogMaxAge, (long)Components.Configuration.LocalServer.TransportServer.DnsLogMaxDirectorySize.Value.ToBytes(), (long)Components.Configuration.LocalServer.TransportServer.DnsLogMaxFileSize.Value.ToBytes());
				}
			}

			// Token: 0x060002EF RID: 751 RVA: 0x0000D624 File Offset: 0x0000B824
			public void Unload()
			{
				Components.Configuration.LocalServerChanged -= this.ConfigUpdate;
				if (this.startMessageTrackingLog)
				{
					MessageTrackingLog.Stop();
				}
				if (this.systemProbeLoggingEnabled)
				{
					SystemProbe.Stop();
				}
				this.smtpSendLog.Close();
				ConnectionLog.TransportStop();
				ConnectionLog.Stop();
				QueueLog.Stop();
				if (this.startTransportWlmLog)
				{
					TransportWlmLog.Stop();
				}
				if (this.startFlowControlLog)
				{
					this.flowControlLog.Stop();
				}
				DnsLog.Stop();
			}

			// Token: 0x060002F0 RID: 752 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
			public string OnUnhandledException(Exception e)
			{
				ConnectionLog.FlushBuffer();
				TransportWlmLog.FlushBuffer();
				return null;
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
			private void ConfigUpdate(TransportServerConfiguration server)
			{
				if (this.startMessageTrackingLog)
				{
					MessageTrackingLog.Configure(server.TransportServer);
				}
				this.smtpSendLog.Configure(server.TransportServer.SendProtocolLogPath, server.TransportServer.SendProtocolLogMaxAge, server.TransportServer.SendProtocolLogMaxDirectorySize, server.TransportServer.SendProtocolLogMaxFileSize, Components.Configuration.AppConfig.Logging.SmtpSendLogBufferSize, Components.Configuration.AppConfig.Logging.SmtpSendLogFlushInterval, Components.Configuration.AppConfig.Logging.SmtpSendLogAsyncInterval);
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x0000D748 File Offset: 0x0000B948
			private static void StartSystemProbe()
			{
				try
				{
					SystemProbe.Start("SYSPRB", Components.configurationComponent.ProcessTransportRole.ToString());
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "System probe started successfully.");
					SystemProbe.ActivityId = CombGuidGenerator.NewGuid();
					SystemProbe.TracePass("Transport", "System probe started successfully.", new object[0]);
					SystemProbe.ActivityId = Guid.Empty;
				}
				catch (LogException ex)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Failed to initialize system probe. {0}", ex.Message);
				}
			}

			// Token: 0x040001B5 RID: 437
			private readonly bool startMessageTrackingLog;

			// Token: 0x040001B6 RID: 438
			private readonly bool startQueueLogging;

			// Token: 0x040001B7 RID: 439
			private readonly string messageTrackingLogPrefix;

			// Token: 0x040001B8 RID: 440
			private readonly bool startTransportWlmLog;

			// Token: 0x040001B9 RID: 441
			private readonly bool startFlowControlLog;

			// Token: 0x040001BA RID: 442
			private readonly bool startDnsLog;

			// Token: 0x040001BB RID: 443
			private readonly bool systemProbeLoggingEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Transport.SystemProbeLogging.Enabled;

			// Token: 0x040001BC RID: 444
			private ProtocolLog smtpSendLog = new ProtocolLog("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version, "SMTP Send Protocol Log", "SEND", "SmtpSendProtocolLogs");

			// Token: 0x040001BD RID: 445
			private FlowControlLog flowControlLog;
		}

		// Token: 0x0200005D RID: 93
		internal class PerfCountersLoader : ITransportComponent, IDiagnosable, IPerfCountersLoader
		{
			// Token: 0x060002F4 RID: 756 RVA: 0x0000D7DC File Offset: 0x0000B9DC
			public PerfCountersLoader(bool initSecureMailPerfCounters)
			{
				this.initSecureMailPerfCounters = initSecureMailPerfCounters;
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x0000D7F6 File Offset: 0x0000B9F6
			public void Load()
			{
				Components.PerfCountersLoader.InitializeADRecipientCachePerfCounters();
				if (this.initSecureMailPerfCounters)
				{
					Utils.InitPerfCounters();
				}
				LatencyTracker.Start(Components.TransportAppConfig.LatencyTracker, Components.Configuration.ProcessTransportRole);
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x0000D824 File Offset: 0x0000BA24
			public void Unload()
			{
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Logging the number of messages left in the queues");
				ProcessTransportRole processTransportRole = Components.configurationComponent.ProcessTransportRole;
				QueuingPerfCountersInstance queuingPerfCountersInstance = (processTransportRole == ProcessTransportRole.FrontEnd) ? null : QueueManager.GetTotalPerfCounters();
				bool flag = queuingPerfCountersInstance != null && (processTransportRole == ProcessTransportRole.Hub || processTransportRole == ProcessTransportRole.Edge);
				bool flag2 = ShadowRedundancyManager.PerfCounters != null && ShadowRedundancyManager.PerfCounters.IsValid(ShadowRedundancyCounterId.RedundantMessageDiscardEvents);
				if (flag || flag2)
				{
					Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_QueuingStatusAtShutdown, null, new object[]
					{
						flag ? queuingPerfCountersInstance.SubmissionQueueLength.RawValue.ToString() : string.Empty,
						flag ? queuingPerfCountersInstance.InternalAggregateDeliveryQueueLength.RawValue.ToString() : string.Empty,
						flag ? queuingPerfCountersInstance.ExternalAggregateDeliveryQueueLength.RawValue.ToString() : string.Empty,
						flag ? queuingPerfCountersInstance.UnreachableQueueLength.RawValue.ToString() : string.Empty,
						flag ? queuingPerfCountersInstance.PoisonQueueLength.RawValue.ToString() : string.Empty,
						flag ? queuingPerfCountersInstance.AggregateShadowQueueLength.RawValue.ToString() : string.Empty,
						flag2 ? ShadowRedundancyManager.PerfCounters.RedundantMessageDiscardEvents.ToString() : string.Empty
					});
				}
				ExTraceGlobals.GeneralTracer.TraceDebug((long)this.GetHashCode(), "Stopping latency tracking");
				LatencyTracker.Stop();
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x0000D9B2 File Offset: 0x0000BBB2
			public string OnUnhandledException(Exception e)
			{
				return null;
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
			private static void InitializeADRecipientCachePerfCounters()
			{
				if (!Components.IsBridgehead)
				{
					return;
				}
				try
				{
					ADRecipientCache<TransportMiniRecipient>.InitializePerfCounters(Process.GetCurrentProcess().ProcessName);
				}
				catch (InvalidOperationException ex)
				{
					string text = string.Format("Failed to initialize AD recipient cache performance counters: {0}", ex);
					ExTraceGlobals.GeneralTracer.TraceError(0L, text);
					Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ADRecipientCachePerfCountersLoadFailure, null, new object[]
					{
						ex.ToString()
					});
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, text, ResultSeverityLevel.Error, false);
				}
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000DA44 File Offset: 0x0000BC44
			public void AddCounterToGetExchangeDiagnostics(Type counterType, string counterName)
			{
				this.otherCounters[counterType] = counterName;
			}

			// Token: 0x060002FA RID: 762 RVA: 0x0000DA54 File Offset: 0x0000BC54
			private bool GetPerfCounterInfoUsingReflection(string argument, Type classType, XElement perfCountersElement, bool showVerbose, string counterName)
			{
				bool result = false;
				string text = string.IsNullOrEmpty(counterName) ? classType.Name : counterName;
				XElement xelement = new XElement(text);
				perfCountersElement.Add(xelement);
				if (showVerbose || argument.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1)
				{
					MethodInfo method = classType.GetMethod("GetPerfCounterInfo");
					if (method != null)
					{
						method.Invoke(classType, new object[]
						{
							xelement
						});
						result = true;
					}
					else
					{
						xelement.Add(string.Format("{0} does not have GetPerfCounterInfo method defined", classType.Name));
					}
				}
				return result;
			}

			// Token: 0x060002FB RID: 763 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
			public string GetDiagnosticComponentName()
			{
				return "PerfCounters";
			}

			// Token: 0x060002FC RID: 764 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
			public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
			{
				XElement xelement = new XElement(this.GetDiagnosticComponentName());
				bool showVerbose = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
				bool flag = parameters.Argument.IndexOf("help", StringComparison.OrdinalIgnoreCase) != -1;
				bool flag2 = false;
				foreach (Type classType in Components.PerfCountersLoader.TransportPerfCounters)
				{
					flag2 |= this.GetPerfCounterInfoUsingReflection(parameters.Argument, classType, xelement, showVerbose, null);
				}
				foreach (KeyValuePair<Type, string> keyValuePair in this.otherCounters)
				{
					flag2 |= this.GetPerfCounterInfoUsingReflection(parameters.Argument, keyValuePair.Key, xelement, showVerbose, keyValuePair.Value);
				}
				if (!flag2 || flag)
				{
					XElement xelement2 = new XElement("Example1");
					xelement2.SetValue("$xml=[xml](Get-ExchangeDiagnosticInfo -Process edgetransport -component perfcounters -Argument verbose -server ht001);$xml.Diagnostics.Components.PerfCounters");
					XElement xelement3 = new XElement("Example2");
					xelement3.SetValue("$xml=[xml](Get-ExchangeDiagnosticInfo -Process edgetransport -component perfcounters -argument QueuingPerfCounters,SmtpReceivePerfCounters -server ht001);$xml.Diagnostics.Components.PerfCounters.QueuingPerfCounters");
					xelement.Add(xelement2);
					xelement.Add(xelement3);
				}
				return xelement;
			}

			// Token: 0x060002FD RID: 765 RVA: 0x0000DC24 File Offset: 0x0000BE24
			private static Type[] GetTransportPerfCounters()
			{
				return new Type[]
				{
					typeof(CertificateValidationResultCachePerfCounters),
					typeof(ConfigurationCachePerfCounters),
					typeof(DatabasePerfCounters),
					typeof(DeliveryAgentPerfCounters),
					typeof(DeliveryFailurePerfCounters),
					typeof(DsnGeneratorPerfCounters),
					typeof(LatencyTrackerEndToEndPerfCounters),
					typeof(LatencyTrackerPerfCounters),
					typeof(MessageResubmissionPerformanceCounters),
					typeof(OutboundIPPoolPerfCounters),
					typeof(PickupPerfCounters),
					typeof(SubmitHelperPerfCounters),
					typeof(ProxyHubSelectorPerfCounters),
					typeof(QueuingPerfCounters),
					typeof(ResolverPerfCounters),
					typeof(RoutingPerfCounters),
					typeof(SecureMailTransportPerfCounters),
					typeof(ShadowRedundancyPerfCounters),
					typeof(ShadowRedundancyInstancePerfCounters),
					typeof(SmtpAvailabilityPerfCounters),
					typeof(SmtpConnectionCachePerfCounters),
					typeof(SmtpProxyPerfCounters),
					typeof(SmtpReceiveFrontendPerfCounters),
					typeof(SmtpReceivePerfCounters),
					typeof(SmtpResponseSubCodePerfCounters),
					typeof(SmtpSendPerfCounters),
					typeof(MExCounters),
					typeof(MSExchangeADRecipientCache),
					typeof(IsMemberOfResolverPerfCounters),
					typeof(QueueQuotaComponentPerfCounters)
				};
			}

			// Token: 0x040001BE RID: 446
			private readonly bool initSecureMailPerfCounters;

			// Token: 0x040001BF RID: 447
			private static Type[] TransportPerfCounters = Components.PerfCountersLoader.GetTransportPerfCounters();

			// Token: 0x040001C0 RID: 448
			private Dictionary<Type, string> otherCounters = new Dictionary<Type, string>();
		}

		// Token: 0x0200005E RID: 94
		internal class StoreDriverLoader : IStartableTransportComponent, ITransportComponent
		{
			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060002FF RID: 767 RVA: 0x0000DDE1 File Offset: 0x0000BFE1
			public string CurrentState
			{
				get
				{
					if (Components.storeDriver != null)
					{
						return Components.storeDriver.CurrentState;
					}
					return null;
				}
			}

			// Token: 0x06000300 RID: 768 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
			public void Load()
			{
				if (!Components.IsBridgehead)
				{
					return;
				}
				Components.LoadStoreDriverAssembly();
				Type type = Components.storeDriverAssembly.GetType("Microsoft.Exchange.MailboxTransport.StoreDriver.StoreDriver", true);
				MethodInfo method = type.GetMethod("CreateStoreDriver", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (null == method)
				{
					throw new InvalidOperationException("CreateStoreDriver method was not found.");
				}
				object obj = method.Invoke(null, null);
				if (obj == null)
				{
					ExTraceGlobals.GeneralTracer.TraceError((long)this.GetHashCode(), "Failed to create the store driver object; Local delivery is disabled");
					return;
				}
				Components.storeDriver = (obj as IStoreDriver);
				this.mexEventsType = Components.storeDriverAssembly.GetType("Microsoft.Exchange.MailboxTransport.StoreDriver.MExEvents", true);
				MethodInfo method2 = this.mexEventsType.GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (null == method2)
				{
					throw new InvalidOperationException("MExEvents.Initialize method was not found.");
				}
				try
				{
					method2.Invoke(null, new object[]
					{
						Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\agents.config")
					});
				}
				catch (TargetInvocationException ex)
				{
					ExchangeConfigurationException ex2 = ex.InnerException as ExchangeConfigurationException;
					if (ex2 == null)
					{
						throw;
					}
					string text = "StoreDriver.MExEvents.Initialize threw ExchangeConfigurationException: shutting down service.";
					ExTraceGlobals.SchedulerTracer.TraceError((long)this.GetHashCode(), text);
					Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_CannotStartAgents, null, new object[]
					{
						ex2.LocalizedString,
						ex2
					});
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, text, ResultSeverityLevel.Error, false);
					bool canRetry = Components.HasTransientException(ex2);
					Components.StopService(ex2.Message, canRetry, false, false);
				}
			}

			// Token: 0x06000301 RID: 769 RVA: 0x0000DF84 File Offset: 0x0000C184
			public void Unload()
			{
				if (null != this.mexEventsType)
				{
					MethodInfo method = this.mexEventsType.GetMethod("Shutdown", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					if (null == method)
					{
						throw new InvalidOperationException("MExEvents.Shutdown method was not found.");
					}
					method.Invoke(null, null);
				}
			}

			// Token: 0x06000302 RID: 770 RVA: 0x0000DFCF File Offset: 0x0000C1CF
			public string OnUnhandledException(Exception e)
			{
				if (Components.storeDriver != null)
				{
					Components.storeDriver.Pause();
				}
				return null;
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000DFE3 File Offset: 0x0000C1E3
			public void Start(bool initiallyPaused, ServiceState targetRunningState)
			{
				this.targetRunningState = targetRunningState;
				if (Components.storeDriver != null)
				{
					Components.storeDriver.Start(initiallyPaused || !this.ShouldExecute());
				}
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000E00C File Offset: 0x0000C20C
			public void Stop()
			{
				if (Components.storeDriver != null)
				{
					Components.storeDriver.Stop();
				}
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000E01F File Offset: 0x0000C21F
			public void Pause()
			{
				if (Components.storeDriver != null)
				{
					Components.storeDriver.Pause();
				}
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000E032 File Offset: 0x0000C232
			public void Continue()
			{
				if (Components.storeDriver != null && this.ShouldExecute())
				{
					Components.storeDriver.Continue();
				}
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000E04D File Offset: 0x0000C24D
			private bool ShouldExecute()
			{
				return this.targetRunningState == ServiceState.Active || this.targetRunningState == ServiceState.Draining;
			}

			// Token: 0x040001C1 RID: 449
			private Type mexEventsType;

			// Token: 0x040001C2 RID: 450
			private ServiceState targetRunningState;
		}

		// Token: 0x0200005F RID: 95
		internal class AggregatorLoader : IStartableTransportComponent, ITransportComponent
		{
			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x06000309 RID: 777 RVA: 0x0000E06B File Offset: 0x0000C26B
			public string CurrentState
			{
				get
				{
					if (Components.aggregator != null)
					{
						return Components.aggregator.CurrentState;
					}
					return null;
				}
			}

			// Token: 0x0600030A RID: 778 RVA: 0x0000E080 File Offset: 0x0000C280
			public void Load()
			{
				if (!Components.IsBridgehead)
				{
					return;
				}
				this.LoadTransportSyncAssemblies();
				Type type = Components.AggregatorLoader.transportSyncWorkerAssembly.GetType("Microsoft.Exchange.MailboxTransport.ContentAggregation.AggregationComponent", true);
				MethodInfo method = type.GetMethod("CreateAggregationComponent", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				if (method == null)
				{
					throw new InvalidOperationException("CreateAggregationComponent method not found");
				}
				object obj = method.Invoke(null, null);
				if (obj == null)
				{
					ExTraceGlobals.GeneralTracer.TraceError((long)this.GetHashCode(), "Failed to create the aggregation component, no aggregator on this hub");
					return;
				}
				Components.aggregator = (obj as IStartableTransportComponent);
				if (Components.aggregator != null)
				{
					Components.aggregator.Load();
				}
			}

			// Token: 0x0600030B RID: 779 RVA: 0x0000E10D File Offset: 0x0000C30D
			public void Unload()
			{
				if (Components.aggregator != null)
				{
					Components.aggregator.Unload();
				}
			}

			// Token: 0x0600030C RID: 780 RVA: 0x0000E120 File Offset: 0x0000C320
			public string OnUnhandledException(Exception e)
			{
				if (Components.aggregator != null)
				{
					return Components.aggregator.OnUnhandledException(e);
				}
				return null;
			}

			// Token: 0x0600030D RID: 781 RVA: 0x0000E136 File Offset: 0x0000C336
			public void Start(bool initiallyPaused, ServiceState targetRunningState)
			{
				if (Components.aggregator != null)
				{
					Components.aggregator.Start(initiallyPaused, targetRunningState);
				}
			}

			// Token: 0x0600030E RID: 782 RVA: 0x0000E14B File Offset: 0x0000C34B
			public void Stop()
			{
				if (Components.aggregator != null)
				{
					Components.aggregator.Stop();
				}
			}

			// Token: 0x0600030F RID: 783 RVA: 0x0000E15E File Offset: 0x0000C35E
			public void Pause()
			{
				if (Components.aggregator != null)
				{
					Components.aggregator.Pause();
				}
			}

			// Token: 0x06000310 RID: 784 RVA: 0x0000E171 File Offset: 0x0000C371
			public void Continue()
			{
				if (Components.aggregator != null)
				{
					Components.aggregator.Continue();
				}
			}

			// Token: 0x06000311 RID: 785 RVA: 0x0000E184 File Offset: 0x0000C384
			private void LoadTransportSyncAssemblies()
			{
				AssemblyName assemblyName = null;
				if (Components.AggregatorLoader.transportSyncCommonAssembly == null)
				{
					lock (Components.AggregatorLoader.syncRoot)
					{
						if (Components.AggregatorLoader.transportSyncCommonAssembly == null)
						{
							assemblyName = Assembly.GetExecutingAssembly().GetName();
							string assemblyString = assemblyName.FullName.Replace(assemblyName.Name, "Microsoft.Exchange.Transport.Sync.Common");
							Components.AggregatorLoader.transportSyncCommonAssembly = Assembly.Load(assemblyString);
						}
					}
				}
				if (Components.AggregatorLoader.transportSyncWorkerAssembly == null)
				{
					lock (Components.AggregatorLoader.syncRoot)
					{
						if (Components.AggregatorLoader.transportSyncWorkerAssembly == null)
						{
							if (assemblyName == null)
							{
								assemblyName = Assembly.GetExecutingAssembly().GetName();
							}
							string assemblyString2 = assemblyName.FullName.Replace(assemblyName.Name, "Microsoft.Exchange.Transport.Sync.Worker");
							Components.AggregatorLoader.transportSyncWorkerAssembly = Assembly.Load(assemblyString2);
						}
					}
				}
			}

			// Token: 0x040001C3 RID: 451
			private static object syncRoot = new object();

			// Token: 0x040001C4 RID: 452
			private static Assembly transportSyncCommonAssembly;

			// Token: 0x040001C5 RID: 453
			private static Assembly transportSyncWorkerAssembly;
		}

		// Token: 0x02000060 RID: 96
		internal class CategorizerMExRuntimeLoader : ITransportComponent, IDiagnosable
		{
			// Token: 0x06000314 RID: 788 RVA: 0x0000E294 File Offset: 0x0000C494
			public void Load()
			{
				try
				{
					MExEvents.Initialize(Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\agents.config"));
				}
				catch (ExchangeConfigurationException ex)
				{
					string text = "StoreDriver.MExEvents.Initialize threw ExchangeConfigurationException: shutting down service.";
					ExTraceGlobals.SchedulerTracer.TraceError((long)this.GetHashCode(), text);
					Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_CannotStartAgents, null, new object[]
					{
						ex.LocalizedString,
						ex
					});
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, text, ResultSeverityLevel.Error, false);
					bool canRetry = Components.HasTransientException(ex);
					Components.StopService(ex.Message, canRetry, false, false);
				}
			}

			// Token: 0x06000315 RID: 789 RVA: 0x0000E33C File Offset: 0x0000C53C
			public void Unload()
			{
				MExEvents.Shutdown();
			}

			// Token: 0x06000316 RID: 790 RVA: 0x0000E343 File Offset: 0x0000C543
			public string OnUnhandledException(Exception e)
			{
				return null;
			}

			// Token: 0x06000317 RID: 791 RVA: 0x0000E346 File Offset: 0x0000C546
			string IDiagnosable.GetDiagnosticComponentName()
			{
				return "RoutingAgents";
			}

			// Token: 0x06000318 RID: 792 RVA: 0x0000E350 File Offset: 0x0000C550
			XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
			{
				XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
				xelement.Add(MExEvents.GetDiagnosticInfo(parameters));
				return xelement;
			}
		}

		// Token: 0x02000061 RID: 97
		internal class BootScannerMExRuntimeLoader : ITransportComponent, IDiagnosable
		{
			// Token: 0x0600031A RID: 794 RVA: 0x0000E384 File Offset: 0x0000C584
			public void Load()
			{
				try
				{
					StorageAgentMExEvents.Initialize(Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\Shared\\agents.config"));
				}
				catch (ExchangeConfigurationException ex)
				{
					string text = "StorageAgentMExEvents.Initialize threw ExchangeConfigurationException: shutting down service.";
					ExTraceGlobals.SchedulerTracer.TraceError((long)this.GetHashCode(), text);
					Components.eventLogger.LogEvent(TransportEventLogConstants.Tuple_CannotStartAgents, null, new object[]
					{
						ex.LocalizedString,
						ex
					});
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, text, ResultSeverityLevel.Error, false);
					bool canRetry = Components.HasTransientException(ex);
					Components.StopService(ex.Message, canRetry, false, false);
				}
			}

			// Token: 0x0600031B RID: 795 RVA: 0x0000E42C File Offset: 0x0000C62C
			public void Unload()
			{
				StorageAgentMExEvents.Shutdown();
			}

			// Token: 0x0600031C RID: 796 RVA: 0x0000E433 File Offset: 0x0000C633
			public string OnUnhandledException(Exception e)
			{
				return null;
			}

			// Token: 0x0600031D RID: 797 RVA: 0x0000E436 File Offset: 0x0000C636
			string IDiagnosable.GetDiagnosticComponentName()
			{
				return "StorageAgents";
			}

			// Token: 0x0600031E RID: 798 RVA: 0x0000E440 File Offset: 0x0000C640
			XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
			{
				XElement xelement = new XElement(((IDiagnosable)this).GetDiagnosticComponentName());
				xelement.Add(StorageAgentMExEvents.GetDiagnosticInfo(parameters));
				return xelement;
			}
		}

		// Token: 0x02000062 RID: 98
		internal class TransportMailItemLoader : ITransportComponent
		{
			// Token: 0x06000320 RID: 800 RVA: 0x0000E473 File Offset: 0x0000C673
			public void Load()
			{
				TransportMailItem.SetComponents(Components.Configuration, Components.ResourceManagerComponent.ResourceManager, Components.shadowRedundancyComponent, Components.MessagingDatabase.Database);
			}

			// Token: 0x06000321 RID: 801 RVA: 0x0000E498 File Offset: 0x0000C698
			public void Unload()
			{
			}

			// Token: 0x06000322 RID: 802 RVA: 0x0000E49A File Offset: 0x0000C69A
			public string OnUnhandledException(Exception e)
			{
				return null;
			}
		}

		// Token: 0x02000063 RID: 99
		internal class MicrosoftExchangeRecipientLoader : ITransportComponent
		{
			// Token: 0x06000324 RID: 804 RVA: 0x0000E4A5 File Offset: 0x0000C6A5
			public void Load()
			{
				GlobalConfigurationBase<MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>.Start();
			}

			// Token: 0x06000325 RID: 805 RVA: 0x0000E4AC File Offset: 0x0000C6AC
			public void Unload()
			{
				GlobalConfigurationBase<MicrosoftExchangeRecipient, MicrosoftExchangeRecipientConfiguration>.Stop();
			}

			// Token: 0x06000326 RID: 806 RVA: 0x0000E4B3 File Offset: 0x0000C6B3
			public string OnUnhandledException(Exception e)
			{
				return null;
			}
		}

		// Token: 0x02000064 RID: 100
		internal class DirectTrustLoader : ITransportComponent
		{
			// Token: 0x06000328 RID: 808 RVA: 0x0000E4C0 File Offset: 0x0000C6C0
			public void Load()
			{
				if (VariantConfiguration.InvariantNoFlightingSnapshot.Transport.DirectTrustCache.Enabled)
				{
					DirectTrust.Load();
				}
			}

			// Token: 0x06000329 RID: 809 RVA: 0x0000E4EB File Offset: 0x0000C6EB
			public void Unload()
			{
				DirectTrust.Unload();
			}

			// Token: 0x0600032A RID: 810 RVA: 0x0000E4F2 File Offset: 0x0000C6F2
			public string OnUnhandledException(Exception e)
			{
				return null;
			}
		}

		// Token: 0x02000066 RID: 102
		internal class TransportPickupSubmissionHandler : IPickupSubmitHandler
		{
			// Token: 0x0600032D RID: 813 RVA: 0x0000E4FD File Offset: 0x0000C6FD
			public void OnSubmit(TransportMailItem item, MailDirectionality direction, PickupType pickupType)
			{
				if (direction != MailDirectionality.Undefined)
				{
					item.Directionality = direction;
				}
				item.CommitImmediate();
				Components.CategorizerComponent.EnqueueSubmittedMessage(item);
			}
		}

		// Token: 0x02000067 RID: 103
		private class ComponentNotAvailableException : Exception
		{
			// Token: 0x0600032F RID: 815 RVA: 0x0000E523 File Offset: 0x0000C723
			internal ComponentNotAvailableException(string componentName) : base("component " + componentName + " is not available")
			{
			}
		}
	}
}
