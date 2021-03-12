using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Extensibility;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.RecipientAPI;
using Microsoft.Exchange.Transport.ResourceMonitoring;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x02000003 RID: 3
	internal sealed class MailboxTransportSubmissionService : ExServiceBase, IDiagnosable
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002257 File Offset: 0x00000457
		public MailboxTransportSubmissionService()
		{
			base.ServiceName = "Microsoft Exchange Mailbox Transport Submission";
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
			MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances = new HashSet<MailboxTransportSubmissionAssistant>(100);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000228B File Offset: 0x0000048B
		public static IEventNotificationItem MonitoringEventNotificationItem
		{
			get
			{
				return MailboxTransportSubmissionService.monitoringEventNotificationItem;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002292 File Offset: 0x00000492
		public static IStoreDriverTracer StoreDriverTracer
		{
			get
			{
				return MailboxTransportSubmissionService.storeDriverTracer;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002299 File Offset: 0x00000499
		public static QuarantineHandler QuarantineHandler
		{
			get
			{
				return MailboxTransportSubmissionService.quarantineHandler;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000022A0 File Offset: 0x000004A0
		public static SubmissionPoisonHandler SubmissionPoisonHandler
		{
			get
			{
				return MailboxTransportSubmissionService.submissionPoisonHandler;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022A7 File Offset: 0x000004A7
		internal static SlidingPercentageCounter PercentPermanentFailures
		{
			get
			{
				return MailboxTransportSubmissionService.percentPermanentFailures;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000022AE File Offset: 0x000004AE
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000022B5 File Offset: 0x000004B5
		internal static HashSet<MailboxTransportSubmissionAssistant> MailboxTransportSubmissionAssistantInstances
		{
			get
			{
				return MailboxTransportSubmissionService.mailboxTransportSubmissionAssistantInstances;
			}
			private set
			{
				MailboxTransportSubmissionService.mailboxTransportSubmissionAssistantInstances = value;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C0 File Offset: 0x000004C0
		public static void Main(string[] args)
		{
			CommonDiagnosticsLog.Initialize(HostId.MailboxTransportSubmissionService);
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StartProcess);
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege"
			});
			if (num != 0)
			{
				Environment.Exit(num);
			}
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.RegisterWatson);
			ExWatson.Register();
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.RegisterWatsonAction);
			ExWatson.RegisterReportAction(new WatsonRegKeyReportAction(MailboxTransportSubmissionService.watsonRegKeyReportActionString), WatsonActionScope.Process);
			MailboxTransportSubmissionService.runningAsService = !Environment.UserInteractive;
			bool flag = false;
			bool flag2 = false;
			foreach (string text in args)
			{
				if (text.StartsWith("-?", StringComparison.Ordinal))
				{
					MailboxTransportSubmissionService.Usage();
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
			if (!MailboxTransportSubmissionService.runningAsService)
			{
				if (!flag)
				{
					MailboxTransportSubmissionService.Usage();
					Environment.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.InitializePerformanceCounterInstance);
			Globals.InitializeSinglePerfCounterInstance();
			SettingOverrideSync.Instance.Start(true);
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.LoadLatencyTrackerConfiguration);
			try
			{
				LatencyTracker.Configuration = TransportAppConfig.LatencyTrackerConfig.Load();
			}
			catch (ConfigurationErrorsException)
			{
			}
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.LoadTransportAppConfig);
			string text2;
			if (!Components.TryLoadTransportAppConfig(out text2))
			{
				MailboxTransportSubmissionEventLogger.LogEvent(MSExchangeSubmissionEventLogConstants.Tuple_SubmissionServiceStartFailure, null, new object[]
				{
					text2
				});
				Environment.Exit(1);
			}
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.CreateService);
			MailboxTransportSubmissionService.mailboxTransportSubmissionService = new MailboxTransportSubmissionService();
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.RunService);
			if (!MailboxTransportSubmissionService.runningAsService)
			{
				ExServiceBase.RunAsConsole(MailboxTransportSubmissionService.mailboxTransportSubmissionService);
				return;
			}
			ServiceBase.Run(MailboxTransportSubmissionService.mailboxTransportSubmissionService);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000247C File Offset: 0x0000067C
		string IDiagnosable.GetDiagnosticComponentName()
		{
			return "MailboxTransportSubmission";
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002484 File Offset: 0x00000684
		XElement IDiagnosable.GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			KeyValuePair<string, object>[] eventData = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("DiagnosticsRequest", parameters.Argument)
			};
			CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.MailboxTransportSubmissionService, eventData);
			XElement xelement = new XElement("MailboxTransportSubmission", new object[]
			{
				new XElement("runningAsService", MailboxTransportSubmissionService.runningAsService),
				new XElement("maxThreads", this.maxThreads),
				new XElement("maxConcurrentSubmissions", MailboxTransportSubmissionAssistant.MaxConcurrentSubmissions),
				new XElement("stage", MailboxTransportSubmissionService.stage)
			});
			lock (MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances)
			{
				XElement xelement2 = new XElement("MailboxTransportSubmissionAssistantInstances");
				xelement.Add(xelement2);
				foreach (MailboxTransportSubmissionAssistant mailboxTransportSubmissionAssistant in MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances)
				{
					xelement2.Add(mailboxTransportSubmissionAssistant.GetDiagnosticInfo(parameters.Argument));
				}
			}
			return xelement;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025E8 File Offset: 0x000007E8
		internal static void DetectSubmissionHang()
		{
			lock (MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances)
			{
				foreach (MailboxTransportSubmissionAssistant mailboxTransportSubmissionAssistant in MailboxTransportSubmissionService.MailboxTransportSubmissionAssistantInstances)
				{
					if (mailboxTransportSubmissionAssistant.DetectSubmissionHang())
					{
						break;
					}
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002664 File Offset: 0x00000864
		protected override void OnStartInternal(string[] args)
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(new ADOperation(this.OnStartInternalHelper));
			if (!adoperationResult.Succeeded)
			{
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, 0L, ("AD Operation Failed. The Mailbox Transport Submission Service will be stopped." + adoperationResult.Exception == null) ? string.Empty : adoperationResult.Exception.Message);
				MailboxTransportSubmissionEventLogger.LogEvent(MSExchangeSubmissionEventLogConstants.Tuple_SubmissionServiceStartFailure, null, new object[]
				{
					adoperationResult.Exception
				});
				base.Stop();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void OnStopInternal()
		{
			this.OnStopInternalHelper();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000026F8 File Offset: 0x000008F8
		protected override void OnCustomCommandInternal(int command)
		{
			if (command != 201)
			{
				return;
			}
			SubmissionConfiguration.Instance.ConfigUpdate();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000271A File Offset: 0x0000091A
		private static void Usage()
		{
			Console.WriteLine(Strings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000273C File Offset: 0x0000093C
		private static void LogStage(MailboxTransportSubmissionService.Stage stage)
		{
			MailboxTransportSubmissionService.stage = stage;
			MailboxTransportSubmissionService.stageTimes.Add(new KeyValuePair<string, string>(stage.ToString(), DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)));
			KeyValuePair<string, object>[] eventData = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Stage", stage.ToString())
			};
			CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.MailboxTransportSubmissionService, eventData);
			ExWatson.AddExtraData(string.Format("{0:u}: Stage={1}", DateTime.UtcNow, stage));
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000027D0 File Offset: 0x000009D0
		private static void InitializePerfMon()
		{
			try
			{
				MailboxTransportSubmissionService.percentPermanentFailures = new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0), true);
				MSExchangeSubmission.PendingSubmissions.Reset();
			}
			catch (InvalidOperationException ex)
			{
				MailboxTransportSubmissionEventLogger.LogEvent(MSExchangeSubmissionEventLogConstants.Tuple_SubmissionServiceStartFailure, null, new object[]
				{
					ex
				});
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000283C File Offset: 0x00000A3C
		private void OnStartInternalHelper()
		{
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StartService);
			MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePfdPass<int, DateTime>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, 0L, "PFD EMS {0} Starting MailboxTransportSubmissionService ({1})", 24475, DateTime.UtcNow);
			bool flag = false;
			bool flag2 = false;
			string text = null;
			try
			{
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.RegisterPamComponent);
				ProcessAccessManager.RegisterComponent(this);
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.InitializePerformanceMonitoring);
				MailboxTransportSubmissionService.InitializePerfMon();
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePfdPass<int, DateTime>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, 0L, "PFD EMS {0} Finished Loading Perfmon ({1})", 32667, DateTime.UtcNow);
				TransportADNotificationAdapter.Instance.RegisterForSubmissionServiceEvents();
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.LoadConfiguration);
				StorageExceptionHandler.Init();
				this.ConstructComponentLoadTree();
				SubmissionConfiguration.Instance.Load();
				int num;
				ThreadPool.GetMaxThreads(out this.maxThreads, out num);
				ITimeBasedAssistantType[] timeBasedAssistantTypeArray = null;
				IEventBasedAssistantType[] eventBasedAssistantTypeArray = new IEventBasedAssistantType[]
				{
					new MailboxTransportSubmissionAssistantType()
				};
				this.databaseManager = new DatabaseManager("Microsoft Exchange Mailbox Transport Submission", MailboxTransportSubmissionAssistant.MaxConcurrentSubmissions, eventBasedAssistantTypeArray, timeBasedAssistantTypeArray, false);
				MailboxTransportSubmissionEventLogger.LogEvent(MSExchangeSubmissionEventLogConstants.Tuple_SubmissionServiceStartSuccess, null, new object[0]);
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.CreateBackgroundThread);
				MailboxTransportSubmissionService.backgroundThread = new BackgroundProcessingThread();
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StartBackgroundThread);
				MailboxTransportSubmissionService.backgroundThread.Start(false, ServiceState.Active);
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StartDatabaseManager);
				this.databaseManager.Start();
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.ServiceStarted);
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePfdPass<int>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, 0L, "PFD EMS {0} MailTransportSubmissionService Started", 26523);
				flag = true;
			}
			catch (ConfigurationErrorsException ex)
			{
				text = ex.Message;
				flag2 = true;
			}
			catch (HandlerParseException ex2)
			{
				text = ex2.Message;
				flag2 = true;
			}
			finally
			{
				if (!flag)
				{
					MailboxTransportSubmissionEventLogger.LogEvent(MSExchangeSubmissionEventLogConstants.Tuple_SubmissionServiceStartFailure, null, new object[]
					{
						text
					});
					MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePfdPass(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, 0L, "Failed to start MailboxTransportSubmissionService");
					base.ExRequestAdditionalTime(60000);
					if (flag2)
					{
						base.Stop();
					}
				}
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002A5C File Offset: 0x00000C5C
		private void OnStopInternalHelper()
		{
			MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StopService);
			ADNotificationListener.Stop();
			MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePass<DateTime>(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, 0L, "Stopping MailboxTransportSubmissionService ({0})", DateTime.UtcNow);
			bool flag = false;
			try
			{
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StopBackgroundThread);
				if (MailboxTransportSubmissionService.backgroundThread != null)
				{
					MailboxTransportSubmissionService.backgroundThread.Stop();
				}
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StopDatabaseManager);
				if (this.databaseManager != null)
				{
					this.databaseManager.Stop();
					this.databaseManager.Dispose();
				}
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StopMessageTracking);
				MessageTrackingLog.Stop();
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.StopConfiguration);
				SubmissionConfiguration.Instance.Unload();
				MailboxTransportSubmissionEventLogger.LogEvent(MSExchangeSubmissionEventLogConstants.Tuple_SubmissionServiceStopSuccess, null, new object[0]);
				MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TracePass(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, 0L, "Stopped MailboxTransportSubmissionService");
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.UngregisterPamComponent);
				ProcessAccessManager.UnregisterComponent(this);
				MailboxTransportSubmissionService.LogStage(MailboxTransportSubmissionService.Stage.ServiceStopped);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					MailboxTransportSubmissionEventLogger.LogEvent(MSExchangeSubmissionEventLogConstants.Tuple_SubmissionServiceStopFailure, null, new object[0]);
					MailboxTransportSubmissionService.StoreDriverTracer.ServiceTracer.TraceFail(MailboxTransportSubmissionService.StoreDriverTracer.MessageProbeActivityId, 0L, "Failed to stop MailboxTransportSubmissionService");
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002B88 File Offset: 0x00000D88
		private void ConstructComponentLoadTree()
		{
			TransportAppConfig.IsMemberOfResolverConfiguration transportIsMemberOfResolverConfig = Components.TransportAppConfig.TransportIsMemberOfResolverConfig;
			IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver adAdapter = new IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver(transportIsMemberOfResolverConfig.DisableDynamicGroups);
			Components.TransportIsMemberOfResolverComponent = new IsMemberOfResolverComponent<RoutingAddress>("Transport", transportIsMemberOfResolverConfig, adAdapter);
			Components.StoreDriverSubmission = MailboxTransportSubmissionService.storeDriverSubmission;
			Components.StoreDriverSubmission.Start(false, ServiceState.Active);
			Components.DsnGenerator = new DsnGenerator();
			Components.AgentComponent = new AgentComponent();
			Components.RoutingComponent = new RoutingComponent();
			Components.EnhancedDns = new EnhancedDns();
			Components.UnhealthyTargetFilterComponent = new UnhealthyTargetFilterComponent();
			Components.ProxyHubSelectorComponent = new ProxyHubSelectorComponent();
			Components.CertificateComponent = new CertificateComponent();
			Components.Configuration = new ConfigurationComponent(ProcessTransportRole.MailboxSubmission);
			Components.MessageThrottlingComponent = new MessageThrottlingComponent();
			Components.ResourceManagerComponent = new ResourceManagerComponent(ResourceManagerResources.PrivateBytes | ResourceManagerResources.TotalBytes);
			Components.SmtpInComponent = new SmtpInComponent(false);
			Components.SmtpOutConnectionHandler = new SmtpOutConnectionHandler();
			Components.SystemCheckComponent = new SystemCheckComponent();
			Components.TransportIsMemberOfResolverComponent = new IsMemberOfResolverComponent<RoutingAddress>("Transport", transportIsMemberOfResolverConfig, adAdapter);
			Components.TransportMailItemLoader item = new Components.TransportMailItemLoader();
			Components.Logging = new Components.LoggingComponent(true, false, false, false, false, "MSGTRKMS");
			StorageFactory.SchemaToUse = StorageFactory.Schema.NullSchema;
			Components.MessagingDatabase = new MessagingDatabaseComponent();
			Components.PerfCountersLoader perfCountersLoader = new Components.PerfCountersLoader(false);
			Components.PerfCountersLoaderComponent = perfCountersLoader;
			Components.ResourceThrottlingComponent = new ResourceThrottlingComponent(new ResourceMeteringConfig(8000, null), new ResourceThrottlingConfig(null), new ComponentsWrapper(), Components.MessagingDatabase, null, Components.Configuration, ResourceManagerResources.PrivateBytes | ResourceManagerResources.TotalBytes, ResourceObservingComponents.EnhancedDns | ResourceObservingComponents.IsMemberOfResolver | ResourceObservingComponents.SmtpIn);
			ParallelTransportComponent parallelTransportComponent = new ParallelTransportComponent("Parallel Components");
			parallelTransportComponent.TransportComponents.Add(item);
			parallelTransportComponent.TransportComponents.Add(Components.TransportIsMemberOfResolverComponent);
			parallelTransportComponent.TransportComponents.Add(perfCountersLoader);
			parallelTransportComponent.TransportComponents.Add(Components.Logging);
			parallelTransportComponent.TransportComponents.Add(new Components.MicrosoftExchangeRecipientLoader());
			parallelTransportComponent.TransportComponents.Add(Components.DsnGenerator);
			parallelTransportComponent.TransportComponents.Add(Components.MessageThrottlingComponent);
			parallelTransportComponent.TransportComponents.Add(Components.StoreDriverSubmission);
			SequentialTransportComponent sequentialTransportComponent = new SequentialTransportComponent("Database and dependents");
			sequentialTransportComponent.TransportComponents.Add(Components.ResourceManagerComponent);
			ParallelTransportComponent parallelTransportComponent2 = new ParallelTransportComponent("Configuration Certificate and Database components");
			parallelTransportComponent2.TransportComponents.Add(sequentialTransportComponent);
			parallelTransportComponent2.TransportComponents.Add(Components.CertificateComponent);
			SequentialTransportComponent sequentialTransportComponent2 = new SequentialTransportComponent("Root Component");
			sequentialTransportComponent2.TransportComponents.Add(MailboxTransportSubmissionService.quarantineHandler);
			sequentialTransportComponent2.TransportComponents.Add(MailboxTransportSubmissionService.submissionPoisonHandler);
			sequentialTransportComponent2.TransportComponents.Add((ITransportComponent)Components.Configuration);
			sequentialTransportComponent2.TransportComponents.Add(Components.SystemCheckComponent);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent2);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.RoutingComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.EnhancedDns);
			sequentialTransportComponent2.TransportComponents.Add(Components.UnhealthyTargetFilterComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.ProxyHubSelectorComponent);
			ParallelTransportComponent parallelTransportComponent3 = new ParallelTransportComponent("SmtpIn and SmtpOut");
			parallelTransportComponent3.TransportComponents.Add(Components.SmtpInComponent);
			parallelTransportComponent3.TransportComponents.Add(Components.SmtpOutConnectionHandler);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent3);
			sequentialTransportComponent2.TransportComponents.Add(Components.ResourceThrottlingComponent);
			Components.SetDatabaseComponents(sequentialTransportComponent);
			Components.SetRootComponent(sequentialTransportComponent2);
		}

		// Token: 0x04000009 RID: 9
		public const string MailboxTransportSubmissionServiceName = "Microsoft Exchange Mailbox Transport Submission";

		// Token: 0x0400000A RID: 10
		private const string ProcessAccessManagerComponentName = "MailboxTransportSubmission";

		// Token: 0x0400000B RID: 11
		private const string HelpOption = "-?";

		// Token: 0x0400000C RID: 12
		private const string ConsoleOption = "-console";

		// Token: 0x0400000D RID: 13
		private const string WaitToContinueOption = "-wait";

		// Token: 0x0400000E RID: 14
		public static readonly Guid MailboxTransportSubmissionServiceComponentGuid = new Guid("{0739bfa0-d929-496d-a13a-0f7e1a64d042}");

		// Token: 0x0400000F RID: 15
		private static readonly IEventNotificationItem monitoringEventNotificationItem = new EventNotificationItemWrapper();

		// Token: 0x04000010 RID: 16
		private static readonly IStoreDriverTracer storeDriverTracer = new StoreDriverTracer();

		// Token: 0x04000011 RID: 17
		private static readonly ICrashRepository crashRepository = new RegistryCrashRepository(SubmissionConfiguration.Instance.App.PoisonRegistryEntryLocation, MailboxTransportSubmissionService.storeDriverTracer);

		// Token: 0x04000012 RID: 18
		private static readonly QuarantineHandler quarantineHandler = new QuarantineHandler(SubmissionConfiguration.Instance.App.MailboxQuarantineCrashCountWindow, SubmissionConfiguration.Instance.App.MailboxQuarantineSpan, SubmissionConfiguration.Instance.App.MailboxQuarantineCrashCountThreshold, MailboxTransportSubmissionService.crashRepository, MailboxTransportSubmissionService.monitoringEventNotificationItem);

		// Token: 0x04000013 RID: 19
		private static readonly SubmissionPoisonHandler submissionPoisonHandler = new SubmissionPoisonHandler(SubmissionConfiguration.Instance.App.PoisonRegistryEntryExpiryWindow, SubmissionConfiguration.Instance.App.PoisonRegistryEntryMaxCount, MailboxTransportSubmissionService.quarantineHandler, MailboxTransportSubmissionService.crashRepository, MailboxTransportSubmissionService.storeDriverTracer);

		// Token: 0x04000014 RID: 20
		private static StoreDriverSubmission storeDriverSubmission = new StoreDriverSubmission(MailboxTransportSubmissionService.storeDriverTracer);

		// Token: 0x04000015 RID: 21
		private static SlidingPercentageCounter percentPermanentFailures;

		// Token: 0x04000016 RID: 22
		private static MailboxTransportSubmissionService.Stage stage;

		// Token: 0x04000017 RID: 23
		private static List<KeyValuePair<string, string>> stageTimes = new List<KeyValuePair<string, string>>(32);

		// Token: 0x04000018 RID: 24
		private static bool runningAsService;

		// Token: 0x04000019 RID: 25
		private static MailboxTransportSubmissionService mailboxTransportSubmissionService;

		// Token: 0x0400001A RID: 26
		private static string watsonRegKeyReportActionString = "HKLM\\SYSTEM\\CurrentControlSet\\Services\\MSExchangeIS\\ImagePath";

		// Token: 0x0400001B RID: 27
		private static BackgroundProcessingThread backgroundThread;

		// Token: 0x0400001C RID: 28
		private static HashSet<MailboxTransportSubmissionAssistant> mailboxTransportSubmissionAssistantInstances;

		// Token: 0x0400001D RID: 29
		private DatabaseManager databaseManager;

		// Token: 0x0400001E RID: 30
		private int maxThreads;

		// Token: 0x02000004 RID: 4
		internal enum Stage
		{
			// Token: 0x04000020 RID: 32
			NotStarted,
			// Token: 0x04000021 RID: 33
			StartProcess,
			// Token: 0x04000022 RID: 34
			RegisterWatson,
			// Token: 0x04000023 RID: 35
			RegisterWatsonAction,
			// Token: 0x04000024 RID: 36
			InitializePerformanceCounterInstance,
			// Token: 0x04000025 RID: 37
			LoadLatencyTrackerConfiguration,
			// Token: 0x04000026 RID: 38
			LoadTransportAppConfig,
			// Token: 0x04000027 RID: 39
			CreateService,
			// Token: 0x04000028 RID: 40
			RunService,
			// Token: 0x04000029 RID: 41
			StartService,
			// Token: 0x0400002A RID: 42
			RegisterPamComponent,
			// Token: 0x0400002B RID: 43
			InitializePerformanceMonitoring,
			// Token: 0x0400002C RID: 44
			StartMessageTracking,
			// Token: 0x0400002D RID: 45
			LoadConfiguration,
			// Token: 0x0400002E RID: 46
			CreateDatabaseManager,
			// Token: 0x0400002F RID: 47
			StartDatabaseManager,
			// Token: 0x04000030 RID: 48
			CreateBackgroundThread,
			// Token: 0x04000031 RID: 49
			StartBackgroundThread,
			// Token: 0x04000032 RID: 50
			ServiceStarted,
			// Token: 0x04000033 RID: 51
			StopService,
			// Token: 0x04000034 RID: 52
			StopBackgroundThread,
			// Token: 0x04000035 RID: 53
			StopDatabaseManager,
			// Token: 0x04000036 RID: 54
			StopConfiguration,
			// Token: 0x04000037 RID: 55
			UngregisterPamComponent,
			// Token: 0x04000038 RID: 56
			StopMessageTracking,
			// Token: 0x04000039 RID: 57
			ShutdownConnectionCache,
			// Token: 0x0400003A RID: 58
			ServiceStopped
		}
	}
}
