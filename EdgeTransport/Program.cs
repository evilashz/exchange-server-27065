using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.IsMemberOfProvider;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Transport.Categorizer;
using Microsoft.Exchange.Transport.Delivery;
using Microsoft.Exchange.Transport.Extensibility;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.MessageResubmission;
using Microsoft.Exchange.Transport.MessageThrottling;
using Microsoft.Exchange.Transport.Pickup;
using Microsoft.Exchange.Transport.RecipientAPI;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.ResourceMonitoring;
using Microsoft.Exchange.Transport.ShadowRedundancy;
using Microsoft.Exchange.Transport.Storage.IPFiltering;
using Microsoft.Exchange.Transport.Storage.Messaging;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Exchange.Win32;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.Transport.Main
{
	// Token: 0x02000002 RID: 2
	internal sealed class Program : ControlObject.TransportWorker, IWorkerProcess
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void Main(string[] args)
		{
			List<string> list = new List<string>();
			list.Add("SeAuditPrivilege");
			list.Add("SeChangeNotifyPrivilege");
			list.Add("SeCreateGlobalPrivilege");
			list.Add("SeImpersonatePrivilege");
			bool isSystem;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				isSystem = current.IsSystem;
			}
			if (isSystem)
			{
				list.Add("SeIncreaseQuotaPrivilege");
				list.Add("SeAssignPrimaryTokenPrivilege");
			}
			int num = Privileges.RemoveAllExcept(list.ToArray());
			if (num != 0)
			{
				Program.main.StopService(Strings.PrivilegeRemovalFailure(num), true, false, false);
			}
			ExWatson.Init();
			AppDomain.CurrentDomain.UnhandledException += Program.MainUnhandledExceptionHandler;
			if (!Program.AddTokenReadPermissionForAdminGroup())
			{
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Failed to update the process security descriptor with TOKEN_READ. The result is the managed debugger may fail to attach.");
			}
			Program.main.Run(args);
			Program.Exit(Program.main.immediateRetryAfterExit ? 200 : 0);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021E8 File Offset: 0x000003E8
		public static bool AddTokenReadPermissionForAdminGroup()
		{
			return Privileges.UpdateProcessDacl(Process.GetCurrentProcess(), delegate(DiscretionaryAcl dacl)
			{
				dacl.AddAccess(AccessControlType.Allow, new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), 131080, InheritanceFlags.None, PropagationFlags.None);
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002211 File Offset: 0x00000411
		public void Retire()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Program.Retire()");
			if (this.transportComponents != null)
			{
				this.transportComponents.Retire();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002238 File Offset: 0x00000438
		public void Stop()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Program.Stop()");
			try
			{
				this.stopHandle.Release();
			}
			catch (SemaphoreFullException)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "StopHandle already signaled. The process is already stopping");
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002288 File Offset: 0x00000488
		public void Activate()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Program.Activate()");
			if (this.transportComponents != null)
			{
				this.transportComponents.ProtectedActivate();
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022AE File Offset: 0x000004AE
		public void Pause()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Program.Pause()");
			if (this.transportComponents != null)
			{
				this.transportComponents.Pause();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022D4 File Offset: 0x000004D4
		public void Continue()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Program.Continue()");
			if (this.transportComponents != null)
			{
				this.transportComponents.Continue();
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022FA File Offset: 0x000004FA
		public void ConfigUpdate()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Program.ConfigUpdate()");
			if (this.transportComponents != null)
			{
				this.transportComponents.ConfigUpdate();
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002320 File Offset: 0x00000520
		public void HandleMemoryPressure()
		{
			NativeMethods.MemoryStatusEx memoryStatusEx;
			bool flag = NativeMethods.GlobalMemoryStatusEx(out memoryStatusEx);
			if (Components.ResourceManager.CurrentPrivateBytesUses <= ResourceUses.Normal)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Ignoring service command to handle memory pressure because the worker process is stable.");
				this.topMemoryUsers = string.Format("PhysicalMemorySize:{0} {1}", flag ? memoryStatusEx.TotalPhys : 0UL, Components.GetProcessMemoryUsage());
				Program.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SystemLowOnMemory, this.topMemoryUsers, new object[0]);
				return;
			}
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Crashing the worker process to get out of memory pressure as indicated by service command.");
			if (flag)
			{
				throw new MemoryPressureException(memoryStatusEx.MemoryLoad, memoryStatusEx.TotalPhys);
			}
			throw new MemoryPressureException();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023C5 File Offset: 0x000005C5
		public void HandleConnection(Socket clientConnection)
		{
			if (this.transportComponents != null)
			{
				Components.HandleConnection(clientConnection);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023D5 File Offset: 0x000005D5
		public void ClearConfigCache()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Program.ClearConfigCache()");
			if (Components.Configuration != null)
			{
				Components.Configuration.ClearCaches();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023FC File Offset: 0x000005FC
		public void HandleBlockedSubmissionQueue()
		{
			if (Components.RemoteDeliveryComponent.MessagesCompletingCategorization != null && Components.RemoteDeliveryComponent.MessagesCompletingCategorization.Value == 0)
			{
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Crashing the worker process to get handle blocked submission queue as indicated by service command.");
				throw new SubmissionQueueBlockedException();
			}
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Ignoring service command to handle blocked submission queue because the worker process is stable.");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002459 File Offset: 0x00000659
		public void HandleForceCrash()
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Crashing the worker process to generate a crash dump.");
			throw new IgnorableForcedCrashException();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002471 File Offset: 0x00000671
		public void HandleLogFlush()
		{
			Components.SmtpInComponent.FlushProtocolLog();
			Components.SmtpOutConnectionHandler.FlushProtocolLog();
			ConnectionLog.FlushBuffer();
			MessageTrackingLog.FlushBuffer();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002494 File Offset: 0x00000694
		private static void MainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "MainUnhandledExceptionHandler");
			if (Program.main.isExiting)
			{
				Environment.Exit(0);
			}
			int num = Interlocked.Exchange(ref Program.main.busyUnhandledException, 1);
			if (num == 1)
			{
				return;
			}
			string text = null;
			if (Program.main.transportComponents != null)
			{
				text = Components.OnUnhandledException((Exception)eventArgs.ExceptionObject);
			}
			if (Components.IsActive)
			{
				PoisonMessage.SavePoisonContext((Exception)eventArgs.ExceptionObject);
			}
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Process unhandled exception");
			WatsonReportAction action = new WatsonExtraDataReportAction(text);
			ExWatson.RegisterReportAction(action, WatsonActionScope.Thread);
			try
			{
				ExWatson.HandleException(sender, eventArgs);
			}
			finally
			{
				ExWatson.UnregisterReportAction(action, WatsonActionScope.Thread);
			}
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Done processing unhandled exception. Return to CLR.");
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002564 File Offset: 0x00000764
		private static void Exit(int exitCode)
		{
			Environment.Exit(exitCode);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000256C File Offset: 0x0000076C
		private static void ConstructComponentLoadTree()
		{
			TransportAppConfig.IsMemberOfResolverConfiguration transportIsMemberOfResolverConfig = Components.TransportAppConfig.TransportIsMemberOfResolverConfig;
			TransportAppConfig.IsMemberOfResolverConfiguration mailboxRulesIsMemberOfResolverConfig = Components.TransportAppConfig.MailboxRulesIsMemberOfResolverConfig;
			IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver adAdapter = new IsMemberOfResolverADAdapter<RoutingAddress>.RoutingAddressResolver(transportIsMemberOfResolverConfig.DisableDynamicGroups);
			IsMemberOfResolverADAdapter<string>.LegacyDNResolver adAdapter2 = new IsMemberOfResolverADAdapter<string>.LegacyDNResolver(mailboxRulesIsMemberOfResolverConfig.DisableDynamicGroups);
			Components.AgentComponent = new AgentComponent();
			Components.MessagingDatabase = new MessagingDatabaseComponent();
			Components.BootScanner = Components.MessagingDatabase.CreateBootScanner();
			Components.RoutingComponent = new RoutingComponent();
			Components.EnhancedDns = new EnhancedDns();
			Components.UnhealthyTargetFilterComponent = new UnhealthyTargetFilterComponent();
			Components.CategorizerComponent = new CategorizerComponent();
			Components.CertificateComponent = new CertificateComponent();
			Components.Configuration = new ConfigurationComponent();
			Components.DeliveryAgentConnectionHandler = new DeliveryAgentConnectionHandler();
			Components.DsnGenerator = new DsnGenerator();
			Components.MessageResubmissionComponent = new MessageResubmissionComponent();
			Components.MailboxRulesIsMemberOfResolverComponent = new IsMemberOfResolverComponent<string>("MailboxRules", mailboxRulesIsMemberOfResolverConfig, adAdapter2);
			Components.MessageThrottlingComponent = new MessageThrottlingComponent();
			Components.OrarGenerator = new OrarGenerator();
			Components.PickupComponent = new PickupComponent(new Program.TransportPickupSubmissionHandler());
			Components.QueueManager = new QueueManager();
			Components.MessageDepotComponent = new MessageDepotComponent();
			Components.MessageDepotQueueViewerComponent = new MessageDepotQueueViewerComponent();
			Components.CategorizerAdapterComponent = new CategorizerAdapterComponent(Components.CategorizerComponent, Components.MessageDepotComponent);
			Components.ProcessingSchedulerComponent = new ProcessingSchedulerComponent();
			Components.SchedulerAdapterComponent = new SchedulerAdapterComponent(Components.MessageDepotComponent, Components.ProcessingSchedulerComponent);
			Components.DsnSchedulerComponent = new DsnSchedulerComponent();
			Components.RemoteDeliveryComponent = new RemoteDeliveryComponent();
			Components.ResourceManagerComponent = new ResourceManagerComponent(ResourceManagerResources.All);
			Components.RmsClientManagerComponent = new RmsClientManagerComponent();
			Components.ShadowRedundancyComponent = new ShadowRedundancyComponent();
			Components.SmtpInComponent = new SmtpInComponent(false);
			Components.SmtpOutConnectionHandler = new SmtpOutConnectionHandler();
			Components.StoreDriverLoaderComponent = new Components.StoreDriverLoader();
			Components.SystemCheckComponent = new SystemCheckComponent();
			Components.TransportIsMemberOfResolverComponent = new IsMemberOfResolverComponent<RoutingAddress>("Transport", transportIsMemberOfResolverConfig, adAdapter);
			Components.PoisonMessageComponent = new PoisonMessage();
			Components.Logging = new Components.LoggingComponent(true, true, true, true, true);
			Components.Metering = (new MeteringConfig().Enabled ? new MeteringComponent() : null);
			IMeteringComponent meteringComponent;
			Components.TryGetMeteringComponent(out meteringComponent);
			IQueueQuotaComponent queueQuotaComponent;
			if (!QueueQuotaConfig.IsQueueQuotaEnabled())
			{
				queueQuotaComponent = null;
			}
			else if (!QueueQuotaConfig.IsQueueQuotaWithMeteringEnabled() || meteringComponent == null)
			{
				IQueueQuotaComponent queueQuotaComponent2 = new QueueQuotaComponent();
				queueQuotaComponent = queueQuotaComponent2;
			}
			else
			{
				queueQuotaComponent = new QueueQuotaComponentWithMetering();
			}
			Components.QueueQuotaComponent = queueQuotaComponent;
			Components.ProcessingQuotaComponent = new ProcessingQuotaComponent();
			Components.PerfCountersLoader perfCountersLoader = new Components.PerfCountersLoader(true);
			Components.PerfCountersLoaderComponent = perfCountersLoader;
			Components.ResourceThrottlingComponent = new ResourceThrottlingComponent(new ResourceMeteringConfig(8000, null), new ResourceThrottlingConfig(null), new ComponentsWrapper(), Components.MessagingDatabase, Components.CategorizerComponent, Components.Configuration, ResourceManagerResources.All, ResourceObservingComponents.All);
			ParallelTransportComponent parallelTransportComponent = new ParallelTransportComponent("AD Configuration Readers");
			parallelTransportComponent.TransportComponents.Add(new Components.TransportMailItemLoader());
			parallelTransportComponent.TransportComponents.Add(Components.TransportIsMemberOfResolverComponent);
			parallelTransportComponent.TransportComponents.Add(Components.MailboxRulesIsMemberOfResolverComponent);
			parallelTransportComponent.TransportComponents.Add(Components.OrarGenerator);
			parallelTransportComponent.TransportComponents.Add(Components.DsnGenerator);
			parallelTransportComponent.TransportComponents.Add(perfCountersLoader);
			parallelTransportComponent.TransportComponents.Add(Components.Logging);
			parallelTransportComponent.TransportComponents.Add(Components.PoisonMessageComponent);
			parallelTransportComponent.TransportComponents.Add(new Components.DirectTrustLoader());
			parallelTransportComponent.TransportComponents.Add(new Components.MicrosoftExchangeRecipientLoader());
			parallelTransportComponent.TransportComponents.Add(new Components.ServicePrincipalNameRegistrar());
			parallelTransportComponent.TransportComponents.Add(new Components.CategorizerMExRuntimeLoader());
			parallelTransportComponent.TransportComponents.Add(new Components.BootScannerMExRuntimeLoader());
			parallelTransportComponent.TransportComponents.Add(Components.MessageThrottlingComponent);
			parallelTransportComponent.TransportComponents.Add(Components.RmsClientManagerComponent);
			parallelTransportComponent.TransportComponents.Add(Components.ShadowRedundancyComponent);
			parallelTransportComponent.TransportComponents.Add(Components.MessageResubmissionComponent);
			SequentialTransportComponent sequentialTransportComponent = new SequentialTransportComponent("Database and dependents");
			sequentialTransportComponent.TransportComponents.Add(Components.MessagingDatabase);
			sequentialTransportComponent.TransportComponents.Add(Components.ResourceManagerComponent);
			sequentialTransportComponent.TransportComponents.Add(Components.MessageDepotComponent);
			sequentialTransportComponent.TransportComponents.Add(Components.MessageDepotQueueViewerComponent);
			sequentialTransportComponent.TransportComponents.Add(Components.DsnSchedulerComponent);
			sequentialTransportComponent.TransportComponents.Add(new Database());
			ParallelTransportComponent parallelTransportComponent2 = new ParallelTransportComponent("Certificate, RemoteDelivery and Database components");
			parallelTransportComponent2.TransportComponents.Add(sequentialTransportComponent);
			parallelTransportComponent2.TransportComponents.Add(Components.CertificateComponent);
			IQueueQuotaComponent item;
			if (Components.TryGetQueueQuotaComponent(out item))
			{
				parallelTransportComponent2.TransportComponents.Add(item);
			}
			parallelTransportComponent2.TransportComponents.Add(Components.RemoteDeliveryComponent);
			if (meteringComponent != null)
			{
				parallelTransportComponent2.TransportComponents.Add(Components.Metering);
			}
			SequentialTransportComponent sequentialTransportComponent2 = new SequentialTransportComponent("Root Component");
			sequentialTransportComponent2.TransportComponents.Add((ITransportComponent)Components.Configuration);
			sequentialTransportComponent2.TransportComponents.Add(Components.SystemCheckComponent);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent2);
			sequentialTransportComponent2.TransportComponents.Add((ITransportComponent)Components.AgentComponent);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent);
			ParallelTransportComponent parallelTransportComponent3 = new ParallelTransportComponent("AD/Database Dependent Components");
			parallelTransportComponent3.TransportComponents.Add(Components.QueueManager);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent3);
			sequentialTransportComponent2.TransportComponents.Add(Components.RoutingComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.EnhancedDns);
			sequentialTransportComponent2.TransportComponents.Add(Components.UnhealthyTargetFilterComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.CategorizerComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.ProcessingSchedulerComponent);
			sequentialTransportComponent2.TransportComponents.Add(Components.SchedulerAdapterComponent);
			ParallelTransportComponent parallelTransportComponent4 = new ParallelTransportComponent("Categorizer Dependent Components");
			parallelTransportComponent4.TransportComponents.Add(new Components.RpcServerComponent());
			parallelTransportComponent4.TransportComponents.Add(Components.SmtpInComponent);
			parallelTransportComponent4.TransportComponents.Add(Components.StoreDriverLoaderComponent);
			parallelTransportComponent4.TransportComponents.Add(Components.PickupComponent);
			parallelTransportComponent4.TransportComponents.Add(Components.BootScanner);
			parallelTransportComponent4.TransportComponents.Add(Components.SmtpOutConnectionHandler);
			parallelTransportComponent4.TransportComponents.Add(new NonSmtpGatewayConnectionHandler());
			parallelTransportComponent4.TransportComponents.Add(Components.DeliveryAgentConnectionHandler);
			parallelTransportComponent4.TransportComponents.Add(new Components.AggregatorLoader());
			parallelTransportComponent4.TransportComponents.Add(Components.ProcessingQuotaComponent);
			sequentialTransportComponent2.TransportComponents.Add(parallelTransportComponent4);
			sequentialTransportComponent2.TransportComponents.Add(Components.ResourceThrottlingComponent);
			sequentialTransportComponent2.TransportComponents.Add(new BackgroundProcessingThread(new BackgroundProcessingThread.ServerComponentStateChangedHandler(Program.main.HandleServerComponentStateChanged)));
			Components.SetDatabaseComponents(sequentialTransportComponent);
			Components.SetRootComponent(sequentialTransportComponent2);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002BD0 File Offset: 0x00000DD0
		private static Semaphore OpenSemaphore(string name, string semaphoreLabel)
		{
			Semaphore semaphore = null;
			if (!string.IsNullOrEmpty(name))
			{
				try
				{
					semaphore = Semaphore.OpenExisting(name);
				}
				catch (WaitHandleCannotBeOpenedException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
				if (semaphore == null)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug<string, string>(0L, "Failed to open the {0} semaphore (name={1}). Exiting.", semaphoreLabel, name);
					Environment.Exit(1);
				}
			}
			return semaphore;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002C30 File Offset: 0x00000E30
		private static void UsageAndExit()
		{
			Console.WriteLine(Strings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
			Program.Exit(0);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002C58 File Offset: 0x00000E58
		private void WaitForHangSignal()
		{
			this.hangHandle.WaitOne();
			bool flag = this.transportComponents != null && Components.IsDatabaseShuttingDown;
			ExTraceGlobals.GeneralTracer.TraceDebug<bool, bool>(0L, "WaitForHangSignal got signaled. (isDatabaseShuttingDown = {0}, crashOnStopTimeout = {1})", flag, Components.TransportAppConfig.WorkerProcess.CrashOnStopTimeout);
			if (!flag)
			{
				Program.eventLogger.LogEvent(TransportEventLogConstants.Tuple_ProcessNotResponding, null, new object[]
				{
					Components.InstanceId
				});
				string text = "Process not responding. State: " + Components.CurrentStateRepresentation();
				ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "WaitForHangSignal: state representation = {0}", text);
				if (Components.TransportAppConfig.WorkerProcess.CrashOnStopTimeout)
				{
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "WaitForHangSignal: Throwing an exception.");
					throw new TimeoutException(text);
				}
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "WaitForHangSignal: Calling Exit(1).");
				Program.Exit(1);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002D34 File Offset: 0x00000F34
		private void Run(string[] args)
		{
			bool flag = false;
			bool passiveRole = false;
			bool flag2 = false;
			bool flag3 = false;
			bool selfListening = false;
			string text = null;
			string name = null;
			string name2 = null;
			string s = null;
			string fullName = Assembly.GetExecutingAssembly().FullName;
			ExTraceGlobals.GeneralTracer.TraceDebug<string>(0L, "Process {0} starting", fullName);
			foreach (string text2 in args)
			{
				if (text2.StartsWith("-?", StringComparison.Ordinal))
				{
					Program.UsageAndExit();
				}
				else if (text2.StartsWith("-console", StringComparison.Ordinal))
				{
					flag2 = true;
				}
				else if (text2.StartsWith("-stopkey:", StringComparison.Ordinal))
				{
					text = text2.Remove(0, "-stopkey:".Length);
				}
				else if (text2.StartsWith("-hangkey:", StringComparison.Ordinal))
				{
					name = text2.Remove(0, "-hangkey:".Length);
				}
				else if (text2.StartsWith("-resetkey:", StringComparison.Ordinal))
				{
					text2.Remove(0, "-resetkey:".Length);
				}
				else if (text2.StartsWith("-readykey:", StringComparison.Ordinal))
				{
					name2 = text2.Remove(0, "-readykey:".Length);
				}
				else if (text2.StartsWith("-pipe:", StringComparison.Ordinal))
				{
					s = text2.Remove(0, "-pipe:".Length);
				}
				else if (text2.StartsWith("-passive", StringComparison.Ordinal))
				{
					passiveRole = true;
				}
				else if (text2.StartsWith("-paused", StringComparison.Ordinal))
				{
					flag = true;
				}
				else if (text2.StartsWith("-wait", StringComparison.Ordinal))
				{
					flag3 = true;
				}
				else if (text2.StartsWith("-workerListening", StringComparison.Ordinal))
				{
					selfListening = true;
				}
			}
			bool flag4 = !string.IsNullOrEmpty(text);
			if (!flag4)
			{
				if (!flag2)
				{
					Program.UsageAndExit();
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag3)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			string error;
			if (!Components.TryLoadTransportAppConfig(out error))
			{
				this.StopService(Strings.AppConfigLoadFailure(error), false, false, false);
				return;
			}
			this.SetThreadPoolLimits(Components.TransportAppConfig);
			this.stopHandle = Program.OpenSemaphore(text, "stop");
			this.hangHandle = Program.OpenSemaphore(name, "hang");
			this.readyHandle = Program.OpenSemaphore(name2, "ready");
			if (this.hangHandle != null)
			{
				Thread thread = new Thread(new ThreadStart(this.WaitForHangSignal));
				thread.Start();
			}
			Globals.InitializeMultiPerfCounterInstance("Transport");
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Program.Run() starting components.");
			SettingOverrideSync.Instance.Start(true);
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Transport.ADExceptionHandling.Enabled)
			{
				Program.ConstructComponentLoadTree();
			}
			else
			{
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(new ADOperation(Program.ConstructComponentLoadTree), 3);
				if (!adoperationResult.Succeeded)
				{
					ExTraceGlobals.GeneralTracer.TraceError<Exception>(0L, "Construct components load tree failed {0}.", adoperationResult.Exception);
					Program.eventLogger.LogEvent(TransportEventLogConstants.Tuple_EdgeTransportInitializationFailure, null, new object[]
					{
						adoperationResult.Exception.ToString()
					});
					bool canRetry = adoperationResult.ErrorCode == ADOperationErrorCode.RetryableError;
					this.StopService(Strings.ADOperationFailure(adoperationResult.Exception.ToString()), canRetry, false, true);
					return;
				}
			}
			string componentId = ServerComponentStates.GetComponentId(this.CalculateServerComponentName());
			this.transportComponents = new Components(componentId, true);
			this.transportComponents.Start(new Components.StopServiceHandler(this.StopService), passiveRole, flag4, selfListening, true);
			if (flag4)
			{
				SafeFileHandle handle = new SafeFileHandle(new IntPtr(long.Parse(s)), true);
				this.listenPipeStream = new PipeStream(handle, FileAccess.Read, true);
				this.controlObject = new ControlObject(this.listenPipeStream, this);
				if (this.controlObject.Initialize())
				{
					if (this.readyHandle != null)
					{
						ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Signal the process is ready");
						this.readyHandle.Release();
						this.readyHandle.Close();
						this.readyHandle = null;
					}
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Start processing stored messages.");
					if (!flag)
					{
						this.transportComponents.Continue();
					}
					ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Waiting for shutdown signal to exit.");
					this.stopHandle.WaitOne();
				}
			}
			else
			{
				ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Service connected. Start processing messages.");
				if (!flag)
				{
					this.transportComponents.Continue();
				}
				Console.WriteLine("Press ENTER to exit ");
				bool flag5 = false;
				while (!flag5)
				{
					string text3 = Console.ReadLine();
					if (string.IsNullOrEmpty(text3))
					{
						Console.WriteLine("Exiting.");
						flag5 = true;
					}
					else if (text3.Equals("p"))
					{
						Console.WriteLine("Pause.");
						this.transportComponents.Pause();
					}
					else if (text3.Equals("c"))
					{
						Console.WriteLine("Continue.");
						this.transportComponents.Continue();
					}
					else
					{
						Console.WriteLine("Unknown command.");
					}
				}
			}
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Received a signal to shut down.  Closing control pipe.");
			this.CloseControlPipeStream();
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Stopping components.");
			ADNotificationListener.Stop();
			this.transportComponents.Stop();
			ExTraceGlobals.GeneralTracer.TraceDebug(0L, "Components stopped.");
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000326C File Offset: 0x0000146C
		private ServerComponentEnum CalculateServerComponentName()
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole";
			string text;
			try
			{
				text = (string)Registry.GetValue(keyName, "ConfiguredVersion", null);
			}
			catch (SecurityException)
			{
				text = null;
			}
			catch (IOException)
			{
				text = null;
			}
			catch (ArgumentException)
			{
				text = null;
			}
			if (text != null)
			{
				return ServerComponentEnum.EdgeTransport;
			}
			return ServerComponentEnum.HubTransport;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000032D4 File Offset: 0x000014D4
		private void CloseControlPipeStream()
		{
			PipeStream pipeStream = this.listenPipeStream;
			this.listenPipeStream = null;
			if (pipeStream != null)
			{
				try
				{
					pipeStream.Close();
				}
				catch (IOException)
				{
				}
				catch (ObjectDisposedException)
				{
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000331C File Offset: 0x0000151C
		private void HandleServerComponentStateChanged()
		{
			this.immediateRetryAfterExit = true;
			this.Stop();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000332C File Offset: 0x0000152C
		private void StopService(string reason, bool canRetry, bool retryAlways, bool failServiceWithException)
		{
			this.isExiting = true;
			this.CloseControlPipeStream();
			if (failServiceWithException)
			{
				Program.Exit(196);
			}
			if (!canRetry)
			{
				Program.Exit(199);
				return;
			}
			if (retryAlways)
			{
				Program.Exit(197);
				return;
			}
			Program.Exit(198);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000337C File Offset: 0x0000157C
		private void SetThreadPoolLimits(TransportAppConfig transportAppConfig)
		{
			this.minIoThreads = transportAppConfig.WorkerProcess.MinIOThreads;
			this.minWorkerThreads = transportAppConfig.WorkerProcess.MinWorkerThreads;
			ThreadPool.SetMinThreads(this.minWorkerThreads, this.minIoThreads);
			this.maxIoThreads = transportAppConfig.WorkerProcess.MaxIOThreads;
			this.maxWorkerThreads = transportAppConfig.WorkerProcess.MaxWorkerThreads;
			ThreadPool.SetMaxThreads(this.maxWorkerThreads, this.maxIoThreads);
		}

		// Token: 0x04000001 RID: 1
		private const string HelpOption = "-?";

		// Token: 0x04000002 RID: 2
		private const string ConsoleOption = "-console";

		// Token: 0x04000003 RID: 3
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000004 RID: 4
		private const string StopKeyOption = "-stopkey:";

		// Token: 0x04000005 RID: 5
		private const string HangKeyOption = "-hangkey:";

		// Token: 0x04000006 RID: 6
		private const string ResetKeyOption = "-resetkey:";

		// Token: 0x04000007 RID: 7
		private const string ReadyKeyOption = "-readykey:";

		// Token: 0x04000008 RID: 8
		private const string PipeOption = "-pipe:";

		// Token: 0x04000009 RID: 9
		private const string PassiveOption = "-passive";

		// Token: 0x0400000A RID: 10
		private const string PausedOption = "-paused";

		// Token: 0x0400000B RID: 11
		private const string SelfListeningKeyOption = "-workerListening";

		// Token: 0x0400000C RID: 12
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x0400000D RID: 13
		private static Program main = new Program();

		// Token: 0x0400000E RID: 14
		private bool isExiting;

		// Token: 0x0400000F RID: 15
		private Components transportComponents;

		// Token: 0x04000010 RID: 16
		private int busyUnhandledException;

		// Token: 0x04000011 RID: 17
		private Semaphore stopHandle;

		// Token: 0x04000012 RID: 18
		private Semaphore hangHandle;

		// Token: 0x04000013 RID: 19
		private Semaphore readyHandle;

		// Token: 0x04000014 RID: 20
		private ControlObject controlObject;

		// Token: 0x04000015 RID: 21
		private int maxIoThreads;

		// Token: 0x04000016 RID: 22
		private int maxWorkerThreads;

		// Token: 0x04000017 RID: 23
		private int minIoThreads;

		// Token: 0x04000018 RID: 24
		private int minWorkerThreads;

		// Token: 0x04000019 RID: 25
		private PipeStream listenPipeStream;

		// Token: 0x0400001A RID: 26
		private string topMemoryUsers;

		// Token: 0x0400001B RID: 27
		private bool immediateRetryAfterExit;

		// Token: 0x02000003 RID: 3
		internal class TransportPickupSubmissionHandler : IPickupSubmitHandler
		{
			// Token: 0x0600001E RID: 30 RVA: 0x0000341E File Offset: 0x0000161E
			public void OnSubmit(TransportMailItem item, MailDirectionality directionality, PickupType pickupType)
			{
				if (directionality != MailDirectionality.Undefined)
				{
					item.Directionality = directionality;
				}
				item.CommitImmediate();
				Components.CategorizerComponent.EnqueueSubmittedMessage(item);
			}
		}
	}
}
