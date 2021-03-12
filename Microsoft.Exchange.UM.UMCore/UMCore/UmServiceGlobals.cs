using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Mapi;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200022C RID: 556
	internal sealed class UmServiceGlobals
	{
		// Token: 0x06001017 RID: 4119 RVA: 0x00047DC4 File Offset: 0x00045FC4
		private UmServiceGlobals()
		{
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00047DCC File Offset: 0x00045FCC
		internal static TimeSpan ComponentStoptime
		{
			get
			{
				return UmServiceGlobals.componentStoptime;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00047DD3 File Offset: 0x00045FD3
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x00047DDA File Offset: 0x00045FDA
		internal static OnWorkerProcessRetiredEventHandler OnWorkerProcessRetired
		{
			get
			{
				return UmServiceGlobals.onWorkerProcessRetired;
			}
			set
			{
				UmServiceGlobals.onWorkerProcessRetired = value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00047DE2 File Offset: 0x00045FE2
		internal static bool WorkerProcessIsRetiring
		{
			get
			{
				return UmServiceGlobals.workerProcessIsRetiring;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00047DE9 File Offset: 0x00045FE9
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x00047DF0 File Offset: 0x00045FF0
		internal static UMStartupMode StartupMode
		{
			get
			{
				return UmServiceGlobals.startupMode;
			}
			set
			{
				UmServiceGlobals.startupMode = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00047DF8 File Offset: 0x00045FF8
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x00047DFF File Offset: 0x00045FFF
		internal static bool ArePerfCountersEnabled
		{
			get
			{
				return UmServiceGlobals.arePerfCountersEnabled;
			}
			set
			{
				UmServiceGlobals.arePerfCountersEnabled = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00047E07 File Offset: 0x00046007
		internal static BaseUMVoipPlatform VoipPlatform
		{
			get
			{
				return UmServiceGlobals.voipPlatform;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00047E0E File Offset: 0x0004600E
		internal static ADNotificationsManager ADNotifications
		{
			get
			{
				UmServiceGlobals.notifManager = ADNotificationsManager.Instance;
				return UmServiceGlobals.notifManager;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x00047E1F File Offset: 0x0004601F
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x00047E26 File Offset: 0x00046026
		internal static bool ShuttingDown
		{
			get
			{
				return UmServiceGlobals.shutDown;
			}
			set
			{
				UmServiceGlobals.shutDown = true;
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00047E30 File Offset: 0x00046030
		internal static void UmInitialize(int sipPort)
		{
			ProcessLog.WriteLine("UmServiceGlobals::UmInitialize", new object[0]);
			ProcessLog.WriteLine("UmInitialize: Initialize GlobalConfiguration", new object[0]);
			GlobCfg.Init();
			Directory.CreateDirectory(Utils.VoiceMailFilePath);
			Directory.CreateDirectory(Utils.UMBadMailFilePath);
			ProcessLog.WriteLine("UmInitialize: Initialize RecyclerConfiguration", new object[0]);
			UMRecyclerConfig.Init();
			ProcessLog.WriteLine("UmInitialize: Initialize State Machine", new object[0]);
			UmServiceGlobals.globManagerConfig = new GlobalActivityManager.ConfigClass();
			UmServiceGlobals.globManagerConfig.Load(GlobCfg.ConfigFile);
			ProcessLog.WriteLine("UmInitialize: Initialize RPC Server", new object[0]);
			ExRpcModule.Bind();
			ProcessLog.WriteLine("UmInitialize: Initialize Outdialing Diagnostics", new object[0]);
			OutdialingDiagnostics.ValidateProperties();
			ProcessLog.WriteLine("UmInitialize: Initialize SipPeerManager", new object[0]);
			SipPeerManager.Initialize(true, new UMServiceADSettings());
			ProcessLog.WriteLine("UmInitialize: Register for mailbox failures", new object[0]);
			MailboxSessionEstablisher.OnMailboxConnectionAttempted += UmServiceGlobals.MailboxSessionEstablisher_OnMailboxConnectionAttempted;
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Initializing VoIP.", new object[]
			{
				11834
			});
			UmServiceGlobals.voipPlatform = Platform.Builder.CreateVoipPlatform();
			ProcessLog.WriteLine("UmInitialize: Initialize VOIP Platform", new object[0]);
			UmServiceGlobals.voipPlatform.Initialize(sipPort, new UMCallSessionHandler<EventArgs>(UmServiceGlobals.CallHandler));
			ProcessLog.WriteLine("UmInitialize: Initialize SIP Peers", new object[0]);
			UmServiceGlobals.StartUMComponents(StartupStage.WPInitialization);
			ProcessLog.WriteLine("UmInitialize: Success", new object[0]);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00047FA2 File Offset: 0x000461A2
		private static void MailboxSessionEstablisher_OnMailboxConnectionAttempted(object sender, MailboxConnectionArgs e)
		{
			Util.IncrementCounter(AvailabilityCounters.PercentageFailedMailboxAccess_Base, 1L);
			if (!e.SuccessfulConnection)
			{
				Util.IncrementCounter(AvailabilityCounters.PercentageFailedMailboxAccess, 1L);
			}
			Util.SetCounter(AvailabilityCounters.RecentPercentageFailedMailboxAccess, (long)UmServiceGlobals.recentMailboxFailures.Update(e.SuccessfulConnection));
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00047FE0 File Offset: 0x000461E0
		internal static void UmUninitialize()
		{
			ProcessLog.WriteLine("UmServiceGlobals::UmUnInitialize", new object[0]);
			UmServiceGlobals.shutDown = true;
			List<AutoResetEvent> list = new List<AutoResetEvent>();
			for (int i = UmServiceGlobals.umComponents.Length - 1; i >= 0; i--)
			{
				IUMAsyncComponent iumasyncComponent = UmServiceGlobals.umComponents[i];
				if (iumasyncComponent.IsInitialized)
				{
					ProcessLog.WriteLine("Stopping Component : " + iumasyncComponent.Name, new object[0]);
					iumasyncComponent.StopAsync();
					list.Add(iumasyncComponent.StoppedEvent);
				}
			}
			bool flag = WaitHandle.WaitAll(list.ToArray(), UmServiceGlobals.ComponentStoptime, false);
			if (flag)
			{
				ProcessLog.WriteLine("All Components shutdown in properTime. Cleaning up and exiting", new object[0]);
				foreach (IUMAsyncComponent iumasyncComponent2 in UmServiceGlobals.umComponents)
				{
					ProcessLog.WriteLine("CleanupAfterStopped for Component : " + iumasyncComponent2.Name, new object[0]);
					iumasyncComponent2.CleanupAfterStopped();
				}
				return;
			}
			ProcessLog.WriteLine("Some Components didn't shutdown in properTime. Hence exiting the process", new object[0]);
			Utils.KillThisProcess();
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000480DC File Offset: 0x000462DC
		internal static void UmRetire(BaseUMVoipPlatform.FinalCallEndedDelegate finalCallEndedDelegate)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStopTracer, 0, "UMServiceGlobals::UmRetire", new object[0]);
			UmServiceGlobals.workerProcessIsRetiring = true;
			if (UmServiceGlobals.OnWorkerProcessRetired != null)
			{
				UmServiceGlobals.OnWorkerProcessRetired();
			}
			if (UmServiceGlobals.VoipPlatform != null)
			{
				UmServiceGlobals.VoipPlatform.Retire(finalCallEndedDelegate);
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x00048130 File Offset: 0x00046330
		internal static void InitializeCurrentCallsPerformanceCounters()
		{
			GeneralCounters.CurrentCalls.RawValue = 0L;
			GeneralCounters.CurrentUnauthenticatedPilotNumberCalls.RawValue = 0L;
			GeneralCounters.CurrentVoicemailCalls.RawValue = 0L;
			GeneralCounters.CurrentFaxCalls.RawValue = 0L;
			GeneralCounters.CurrentSubscriberAccessCalls.RawValue = 0L;
			GeneralCounters.CurrentAutoAttendantCalls.RawValue = 0L;
			GeneralCounters.CurrentPlayOnPhoneCalls.RawValue = 0L;
			GeneralCounters.CurrentPromptEditingCalls.RawValue = 0L;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000481A0 File Offset: 0x000463A0
		internal static void InitializeCallAnswerQueuedMessagesPerformanceCounters()
		{
			if (!UmServiceGlobals.ArePerfCountersEnabled)
			{
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(Utils.VoiceMailFilePath);
			FileInfo[] files = directoryInfo.GetFiles("*.txt");
			if (files != null)
			{
				AvailabilityCounters.TotalQueuedMessages.RawValue = (long)files.Length;
				return;
			}
			AvailabilityCounters.TotalQueuedMessages.RawValue = 0L;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000481EC File Offset: 0x000463EC
		internal static void InitializePerformanceCounters()
		{
			try
			{
				Utils.InitializePerformanceCounters(typeof(AvailabilityCounters));
				AvailabilityCounters.UMPipelineSLA.RawValue = 100L;
				Utils.InitializePerformanceCounters(typeof(CallAnswerCounters));
				Utils.InitializePerformanceCounters(typeof(FaxCounters));
				Utils.InitializePerformanceCounters(typeof(GeneralCounters));
				Utils.InitializePerformanceCounters(typeof(PerformanceCounters));
				Utils.InitializePerformanceCounters(typeof(SubscriberAccessCounters));
				UmServiceGlobals.ArePerfCountersEnabled = true;
				CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Perfcounters initialized successfully. ArePerfCountersEnabled value = {1}", new object[]
				{
					10810,
					UmServiceGlobals.ArePerfCountersEnabled
				});
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceStartTracer, 0, "Failed to initialize perfmon counters, perf data will not be available. Error: {0}", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x000482D8 File Offset: 0x000464D8
		internal static void StartUMComponents(StartupStage stage)
		{
			foreach (IUMAsyncComponent iumasyncComponent in UmServiceGlobals.umComponents)
			{
				ProcessLog.WriteLine(string.Concat(new object[]
				{
					"Stage:",
					stage,
					"  Starting Component : ",
					iumasyncComponent.Name
				}), new object[0]);
				iumasyncComponent.StartNow(stage);
			}
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00048340 File Offset: 0x00046540
		private static void CallHandler(BaseUMCallSession voiceObject, EventArgs e)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, 0, "Received call", new object[0]);
			ActivityManager activityManager = UmServiceGlobals.globManagerConfig.CreateActivityManager();
			activityManager.Start(voiceObject, null);
		}

		// Token: 0x04000B7C RID: 2940
		private static BaseUMVoipPlatform voipPlatform;

		// Token: 0x04000B7D RID: 2941
		private static PercentageBooleanSlidingCounter recentMailboxFailures = PercentageBooleanSlidingCounter.CreateFailureCounter(1000, TimeSpan.FromHours(1.0));

		// Token: 0x04000B7E RID: 2942
		private static bool shutDown;

		// Token: 0x04000B7F RID: 2943
		private static GlobalActivityManager.ConfigClass globManagerConfig;

		// Token: 0x04000B80 RID: 2944
		private static bool arePerfCountersEnabled;

		// Token: 0x04000B81 RID: 2945
		private static UMStartupMode startupMode = UMStartupMode.TCP;

		// Token: 0x04000B82 RID: 2946
		private static TimeSpan componentStoptime = new TimeSpan(0, 1, 30);

		// Token: 0x04000B83 RID: 2947
		private static ADNotificationsManager notifManager;

		// Token: 0x04000B84 RID: 2948
		private static bool workerProcessIsRetiring;

		// Token: 0x04000B85 RID: 2949
		private static OnWorkerProcessRetiredEventHandler onWorkerProcessRetired;

		// Token: 0x04000B86 RID: 2950
		private static IUMAsyncComponent[] umComponents = new IUMAsyncComponent[]
		{
			UmServiceGlobals.SyncComponents.Instance,
			PipelineDispatcher.Instance,
			UMServerRpcComponent.Instance,
			UMPlayOnPhoneRpcServerComponent.Instance,
			UMPartnerMessageRpcServerComponent.Instance,
			UMPromptPreviewRpcServerComponent.Instance,
			UMRecipientTasksServerComponent.Instance,
			UMServerPingRpcServerComponent.Instance,
			UMMwiDeliveryRpcServer.Instance,
			MobileSpeechRecoDispatcher.Instance,
			MobileSpeechRecoRpcServerComponent.Instance,
			CacheCleaner.Instance
		};

		// Token: 0x0200022D RID: 557
		internal class SyncComponents : IUMAsyncComponent
		{
			// Token: 0x170003F5 RID: 1013
			// (get) Token: 0x0600102E RID: 4142 RVA: 0x0004842B File Offset: 0x0004662B
			public AutoResetEvent StoppedEvent
			{
				get
				{
					return this.syncUMComponentStoppedEvent;
				}
			}

			// Token: 0x170003F6 RID: 1014
			// (get) Token: 0x0600102F RID: 4143 RVA: 0x00048433 File Offset: 0x00046633
			public bool IsInitialized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170003F7 RID: 1015
			// (get) Token: 0x06001030 RID: 4144 RVA: 0x00048436 File Offset: 0x00046636
			public string Name
			{
				get
				{
					return base.GetType().Name;
				}
			}

			// Token: 0x170003F8 RID: 1016
			// (get) Token: 0x06001031 RID: 4145 RVA: 0x00048443 File Offset: 0x00046643
			internal static UmServiceGlobals.SyncComponents Instance
			{
				get
				{
					return UmServiceGlobals.SyncComponents.instance;
				}
			}

			// Token: 0x06001032 RID: 4146 RVA: 0x0004844C File Offset: 0x0004664C
			public void StartNow(StartupStage stage)
			{
				if (stage == StartupStage.WPInitialization)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "{0} starting in stage {1}", new object[]
					{
						this.Name,
						stage
					});
					CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Registering Interface for Incoming Calls.", new object[]
					{
						13882
					});
					CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Attempting to Start Fax Job Manager.", new object[]
					{
						15930
					});
					ProcessLog.WriteLine("MediaMethods.InitializeACM()", new object[0]);
					MediaMethods.InitializeACM();
					CallStatisticsLogger.Instance.Init();
					PipelineStatisticsLogger.Instance.Init();
					MobileSpeechRequestStatisticsLogger.Instance.Init();
					CallPerformanceLogger.Instance.Init();
					CallRejectionLogger.Instance.Init();
					OffensiveWordsFilter.Init();
					ProcessLog.WriteLine("UmInitialize: Initialize Incoming Call Listener", new object[0]);
					UmServiceGlobals.voipPlatform.Start();
					TempFileFactory.StartCleanUpTimer();
					return;
				}
				if (stage == StartupStage.WPActivation)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "{0} starting in stage {1}", new object[]
					{
						this.Name,
						stage
					});
					UmServiceGlobals.InitializeCallAnswerQueuedMessagesPerformanceCounters();
					ProcessLog.WriteLine("Initialize: Initialized CA performance counters.", new object[0]);
					CurrentCallsCounterHelper.Instance.Init();
					ProcessLog.WriteLine("Initialize: Initialized call counter helper.", new object[0]);
				}
			}

			// Token: 0x06001033 RID: 4147 RVA: 0x000485B0 File Offset: 0x000467B0
			public void StopAsync()
			{
				this.syncUMComponentStoppedEvent.Reset();
				ProcessLog.WriteLine("UmUnInitialize: Uninitialize AD Notifications", new object[0]);
				if (UmServiceGlobals.notifManager != null)
				{
					UmServiceGlobals.notifManager.Dispose();
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStopTracer, 0, "in UmUninitialize: Shutting VoipPlatform", new object[0]);
				if (UmServiceGlobals.voipPlatform != null)
				{
					UmServiceGlobals.voipPlatform.Shutdown();
				}
				CallStatisticsLogger.Instance.Dispose();
				PipelineStatisticsLogger.Instance.Dispose();
				MobileSpeechRequestStatisticsLogger.Instance.Dispose();
				CallPerformanceLogger.Instance.Dispose();
				CallRejectionLogger.Instance.Dispose();
				ProcessLog.WriteLine("UmUnInitialize: Uninitialize Call counters", new object[0]);
				CurrentCallsCounterHelper.Instance.Shutdown();
				this.syncUMComponentStoppedEvent.Set();
				TempFileFactory.StopCleanUpTimer();
			}

			// Token: 0x06001034 RID: 4148 RVA: 0x00048673 File Offset: 0x00046873
			public void CleanupAfterStopped()
			{
				this.syncUMComponentStoppedEvent.Close();
			}

			// Token: 0x04000B87 RID: 2951
			private static UmServiceGlobals.SyncComponents instance = new UmServiceGlobals.SyncComponents();

			// Token: 0x04000B88 RID: 2952
			private AutoResetEvent syncUMComponentStoppedEvent = new AutoResetEvent(false);
		}
	}
}
