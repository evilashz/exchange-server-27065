using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Exchange.UM.UMService.Exceptions;
using Microsoft.Exchange.Win32;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMService
{
	// Token: 0x0200000D RID: 13
	internal sealed class UMService : UMServiceBase
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004748 File Offset: 0x00002948
		public static TimeSpan TimeoutLeft
		{
			get
			{
				TimeSpan t = new TimeSpan(0, 1, 0);
				TimeSpan result = default(TimeSpan);
				if (!UMServiceBase.hasServiceStarted)
				{
					result = ((UMService)UMServiceBase.umService).StartTimeout - (ExDateTime.UtcNow - ((UMService)UMServiceBase.umService).OnEventStartTime) - t;
				}
				else
				{
					result = ((UMService)UMServiceBase.umService).StopTimeout + UmServiceGlobals.ComponentStoptime - (ExDateTime.UtcNow - ((UMService)UMServiceBase.umService).OnEventStartTime) - t;
				}
				if (result.TotalMilliseconds < 0.0)
				{
					result = TimeSpan.Zero;
				}
				return result;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000047FD File Offset: 0x000029FD
		public static void Main(string[] args)
		{
			ProcessLog.WriteLine("UMService::Main", new object[0]);
			UMServiceBase.Initialize("MSExchangeUM", Strings.ServiceName, Strings.Server);
			UMService.InstantiateService();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004828 File Offset: 0x00002A28
		public bool CanRedirect(bool isCallSecured, out int port)
		{
			if (this.workerProcessManager == null)
			{
				port = 0;
				return false;
			}
			return this.workerProcessManager.CanRedirect(isCallSecured, out port);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004844 File Offset: 0x00002A44
		public bool RecycleWPWithNewCert()
		{
			return this.workerProcessManager.RecycleWPToChangeCerts();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004854 File Offset: 0x00002A54
		protected override void OnCommandTimeout()
		{
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_WatsoningDueToTimeout, null, new object[0]);
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.WatsonWorkerProcessDueToTimeout();
			}
			else
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.ServiceTracer, this.GetHashCode(), "UMService::OnCommandTimeout, WorkerProcessManager is null, workerprocess cannot be signalled to watson", new object[0]);
			}
			ExceptionHandling.SendWatsonWithExtraData(new WatsoningDueToTimeout(), false);
			base.OnCommandTimeout();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000048C0 File Offset: 0x00002AC0
		protected override void OnCustomCommandInternal(int command)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, this.GetHashCode(), "UMService::OnCustomCommandStartInternal() command = {0}", new object[]
			{
				command
			});
			this.workerProcessManager.RecycleWorkerProcess();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004904 File Offset: 0x00002B04
		protected override void HandleCertChange()
		{
			UMService.umConnMgr.RestartEndPoint();
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "UMService.UpdateCertificate: UMConnectionManager restarted successfully. Startup mode is {0}", new object[]
			{
				UMRecyclerConfig.UMStartupType
			});
			if (this.RecycleWPWithNewCert())
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "UMService.UpdateCertificate: Restarted the WP as well, returning.", new object[0]);
				return;
			}
			CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UMService.UpdateCertificate: Unable to recycle the Worker process.", new object[0]);
			throw new UMServiceBaseException(Strings.WorkerProcessRestartFailure);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004993 File Offset: 0x00002B93
		protected override void LoadServiceADSettings()
		{
			base.ADSettings = new UMServiceADSettings();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000049A0 File Offset: 0x00002BA0
		protected override void InternalStop()
		{
			lock (this)
			{
				if (this.pingTimer != null)
				{
					this.pingTimer.Dispose();
					this.pingTimer = null;
				}
				ProcessLog.WriteLine("OnStopInternal: Stopped ping timer.", new object[0]);
				if (UMService.umConnMgr != null)
				{
					UMService.umConnMgr.StopEndPoint();
				}
				ProcessLog.WriteLine("OnStopInternal: Stopped connection manager.", new object[0]);
			}
			CallPerformanceLogger.Instance.Dispose();
			CallRejectionLogger.Instance.Dispose();
			if (this.workerProcessManager != null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Requesting extra startup Time for " + (int)UMService.TimeoutLeft.TotalMilliseconds + "millisecs", new object[0]);
				try
				{
					UMServiceBase.umService.ExRequestAdditionalTime((int)UMService.TimeoutLeft.TotalMilliseconds);
					ProcessLog.WriteLine("OnStopInternal: Requested additional time of '{0}'.", new object[]
					{
						(int)UMService.TimeoutLeft.TotalMilliseconds
					});
				}
				catch (InvalidOperationException ex)
				{
					CallIdTracer.TraceError(ExTraceGlobals.ServiceStartTracer, 0, "Couldnt Request extra startup Time. Error = " + ex.ToString(), new object[0]);
				}
				this.workerProcessManager.Stop(UMService.TimeoutLeft);
				ProcessLog.WriteLine("OnStopInternal: Stopped worker process manager.", new object[0]);
			}
			AACounters.RemoveAllInstances();
			ProcessLog.WriteLine("OnStopInternal: Removed AA counter instances.", new object[0]);
			TranscriptionCounters.RemoveAllInstances();
			ProcessLog.WriteLine("OnStopInternal: Removed Transcription counter instances.", new object[0]);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004B40 File Offset: 0x00002D40
		protected override void InternalStart()
		{
			ProcessLog.WriteLine("UMCallRouter::StartService", new object[0]);
			base.LogServiceStateChangeInfo("KillOrphanedUMWorkerProcess");
			UMService.KillOrphanedUMWorkerProcess();
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Starting UM Service {1}", new object[]
			{
				9210,
				ExDateTime.UtcNow
			});
			base.LogServiceStateChangeInfo("FlushTicketCache");
			Kerberos.FlushTicketCache();
			ProcessLog.WriteLine("StartService: Flused Kerberos ticket cache.", new object[0]);
			base.LogServiceStateChangeInfo("RegisterServiceClass");
			int num = ServicePrincipalName.RegisterServiceClass("SmtpSvc");
			if (num != 0)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceStartTracer, 0, "Failed to register SMTP SPN, status = {0}", new object[]
				{
					num
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_SmtpSpnRegistrationFailure, null, new object[]
				{
					num
				});
			}
			ProcessLog.WriteLine("StartService: Registered transport SPN.", new object[0]);
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Worker SIP ports: {0} & {1}", new object[]
			{
				UMRecyclerConfig.Worker1SipPortNumber,
				UMRecyclerConfig.Worker2SipPortNumber
			});
			try
			{
				Directory.CreateDirectory(Utils.VoiceMailFilePath);
			}
			catch (IOException innerException)
			{
				throw new UMServiceBaseException(Strings.FailedToCreateVoiceMailFilePath(Utils.VoiceMailFilePath), innerException);
			}
			ProcessLog.WriteLine("StartService: Initialized voicemail directory.", new object[0]);
			CallPerformanceLogger.Instance.Init();
			CallRejectionLogger.Instance.Init();
			base.LogServiceStateChangeInfo("InitializeConnectionManager");
			UMService.InitializeConnectionManager();
			ProcessLog.WriteLine("StartService: Initialized connection manager.", new object[0]);
			string path = Path.Combine(Utils.GetExchangeDirectory(), "bin");
			string workerPath = Path.Combine(path, "UMworkerprocess.exe");
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - UMService: Initializing the UM Worker Process Manager", new object[]
			{
				15418
			});
			base.LogServiceStateChangeInfo("SetupWorkerProcessManager");
			this.SetupWorkerProcessManager(workerPath);
			ProcessLog.WriteLine("StartService: Set up the worker process manager.", new object[0]);
			base.LogServiceStateChangeInfo("StartSIPEndpoint");
			this.StartSIPEndpoint();
			ProcessLog.WriteLine("StartService: Started SIP endpoint.", new object[0]);
			if (UMRecyclerConfig.HeartBeatInterval != 0)
			{
				base.LogServiceStateChangeInfo("StartHeartbeatThread");
				CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - UMService: Starting HeartbeatTimer", new object[]
				{
					13370
				});
				this.workerProcessManager.HeartbeatTimer = new Timer(new TimerCallback(this.workerProcessManager.MonitorHeartBeat), null, UMRecyclerConfig.HeartBeatInterval * 1000, UMRecyclerConfig.HeartBeatInterval * 1000);
				ProcessLog.WriteLine("StartService: Initialized heartbeat timer.", new object[0]);
			}
			if (UMRecyclerConfig.ResourceMonitorInterval != 0)
			{
				base.LogServiceStateChangeInfo("StartResourceMonitor");
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - UMService: Starting ResourceMonitorTimer", new object[]
				{
					9274
				});
				this.workerProcessManager.ResourceTimer = new Timer(new TimerCallback(this.workerProcessManager.MonitorResources), null, UMRecyclerConfig.ResourceMonitorInterval * 1000, UMRecyclerConfig.ResourceMonitorInterval * 1000);
				ProcessLog.WriteLine("StartService: Initialized resource timer.", new object[0]);
			}
			if (AppConfig.Instance.Service.PipelineStallCheckThreshold > TimeSpan.Zero)
			{
				base.LogServiceStateChangeInfo("StartPipelineMonitor");
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Starting PipelineMonitor.  PipelineStallThreshold {0}", new object[]
				{
					AppConfig.Instance.Service.PipelineStallCheckThreshold
				});
				TimeSpan timeSpan = TimeSpan.FromSeconds(AppConfig.Instance.Service.PipelineStallCheckThreshold.TotalSeconds / 4.0);
				this.workerProcessManager.PipelinHealthCheckTimer = new Timer(new TimerCallback(this.workerProcessManager.DropPipelineHealthCheck), null, timeSpan, timeSpan);
				ProcessLog.WriteLine("StartService: Initialized PipelineMonitor timer to {0}.", new object[]
				{
					timeSpan
				});
			}
			if (Utils.RunningInTestMode)
			{
				UMService.testModeCanary = File.Create("UMTEST-C798DBA2-1B87-11DC-9A33-5A6656D89593.bin", 1, FileOptions.DeleteOnClose);
			}
			ManualResetEvent[] waitHandles = new ManualResetEvent[]
			{
				this.workerProcessManager.WpStarted,
				this.workerProcessManager.WpFatalError
			};
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Requesting extra startup Time for " + (int)UMService.TimeoutLeft.TotalMilliseconds + "millisecs", new object[0]);
			try
			{
				UMServiceBase.umService.ExRequestAdditionalTime((int)UMService.TimeoutLeft.TotalMilliseconds);
				ProcessLog.WriteLine("StartService: Requested extra time '{0}'.", new object[]
				{
					UMService.TimeoutLeft.TotalMilliseconds
				});
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceStartTracer, 0, "Couldnt Request extra startup Time. Error = " + ex.ToString(), new object[0]);
			}
			base.LogServiceStateChangeInfo("WaitForWorkerProcess");
			ProcessLog.WriteLine("StartService: Waiting for worker process.", new object[0]);
			int num2 = WaitHandle.WaitAny(waitHandles, UMService.TimeoutLeft, false);
			if (num2 == 258)
			{
				throw new UMServiceBaseException(Strings.WorkerProcessStartTimeout);
			}
			if (num2 == 0)
			{
				ProcessLog.WriteLine("StartService: Success.", new object[0]);
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Worker Process started successfully", new object[0]);
				ADNotificationsManager.Instance.Server.ConfigChanged += base.ADServerUpdateCallback;
				ProcessLog.WriteLine("StartService: Registered for AD notifications.", new object[0]);
			}
			else if (num2 == 1)
			{
				ProcessLog.WriteLine("StartService: Failed to start worker process.", new object[0]);
				throw new UMServiceBaseException(Strings.WPFatalError);
			}
			base.LogServiceStateChangeInfo("InternalStartCompleted");
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00005128 File Offset: 0x00003328
		protected override CertificateDiagnostics CreateCertificateDiagnostics()
		{
			return new UMService.UMServiceCertificateDiagnostics(CertificateUtils.UMCertificate);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00005134 File Offset: 0x00003334
		private static void InstantiateService()
		{
			UMServiceBase.umService = new UMService();
			if (Environment.UserInteractive)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "To run in interactive mode, compile service with RUN_SERVICE_IN_CONSOLE enabled", new object[0]);
				Utils.KillThisProcess();
			}
			UMService.RunServiceInSCM();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000517C File Offset: 0x0000337C
		private static void Initialize(bool init)
		{
			ProcessLog.WriteLine("UMService::Initialize", new object[0]);
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - UMService: Preinitializing Enviornment.", new object[]
			{
				12858
			});
			if (init)
			{
				UMServiceBase.umService = new UMService();
			}
			UmServiceGlobals.InitializePerformanceCounters();
			ProcessLog.WriteLine("Initialize: Initialized performance counters.", new object[0]);
			((UMService)UMServiceBase.umService).CreateJobObject("Microsoft Exchange UM Job");
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000051FC File Offset: 0x000033FC
		private static void KillOrphanedUMWorkerProcess()
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension("UMworkerprocess.exe");
			Process[] processesByName = Process.GetProcessesByName(fileNameWithoutExtension);
			if (processesByName.Length > 0)
			{
				ProcessLog.WriteLine("KillOrphanedUMWorkerProcess: Found '{0}' orphaned UMWorkerProcesses.", new object[]
				{
					processesByName.Length
				});
				foreach (Process process in processesByName)
				{
					Utils.KillProcess(process);
					ProcessLog.WriteLine("KillOrphanedUMWorkerProcess: Killed Process with Id '{0}'.", new object[]
					{
						process.Id
					});
				}
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00005284 File Offset: 0x00003484
		private static void InitializeConnectionManager()
		{
			LocalizedString localizedString;
			switch (UMRecyclerConfig.UMStartupType)
			{
			case UMStartupMode.TCP:
				localizedString = Strings.TCPOnly;
				break;
			case UMStartupMode.TLS:
				localizedString = Strings.TLSOnly;
				break;
			case UMStartupMode.Dual:
				localizedString = Strings.TCPnTLS;
				break;
			default:
				throw new InvalidOperationException(string.Format("InitializeConnectionManager: unexpected startup type mode - {0}", UMRecyclerConfig.UMStartupType));
			}
			UMService.umConnMgr = UMConnectionManagerHelper.CreateConnectionManager(UMRecyclerConfig.UMStartupType, (UMService)UMServiceBase.umService);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_StartingMode, null, new object[]
			{
				UMServiceBase.ServiceNameForEventLogging,
				localizedString
			});
			UMService.umConnMgr.Initialize();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000532F File Offset: 0x0000352F
		private static void RunServiceInSCM()
		{
			UMService.Initialize(false);
			ServiceBase.Run(UMServiceBase.umService);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00005341 File Offset: 0x00003541
		private void SetupWorkerProcessManager(string workerPath)
		{
			this.workerProcessManager = this.CreateAndStartWorkerProcessManager(workerPath);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00005350 File Offset: 0x00003550
		private void CreateJobObject(string jobObjectName)
		{
			this.jobObject = NativeMethods.CreateJobObject(IntPtr.Zero, jobObjectName);
			if (this.jobObject == null || this.jobObject.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Win32Exception ex = new Win32Exception(lastWin32Error);
				CallIdTracer.TraceError(ExTraceGlobals.ServiceStartTracer, 0, "CreateJobObject Failed, error={0}, for name ={1}", new object[]
				{
					jobObjectName,
					ex.Message
				});
				throw new UMServiceBaseException(Strings.UMServiceSetJobObjectFailed(jobObjectName, ex.Message));
			}
			NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION extendedLimits = default(NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION);
			extendedLimits.ProcessMemoryLimit = UIntPtr.Zero;
			extendedLimits.JobMemoryLimit = UIntPtr.Zero;
			extendedLimits.PeakProcessMemoryUsed = UIntPtr.Zero;
			extendedLimits.PeakJobMemoryUsed = UIntPtr.Zero;
			extendedLimits.BasicLimitInformation.LimitFlags = 8192U;
			try
			{
				this.jobObject.SetExtendedLimits(extendedLimits);
			}
			catch (Win32Exception ex2)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceStartTracer, 0, "SetInformationJobObject Failed, error={0}, for name={1}", new object[]
				{
					ex2.Message,
					jobObjectName
				});
				this.jobObject.Close();
				throw new UMServiceBaseException(Strings.UMServiceSetJobObjectFailed(jobObjectName, ex2.Message));
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "Job Object Successfully created with name={0}", new object[]
			{
				jobObjectName
			});
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000054B4 File Offset: 0x000036B4
		private void StartSIPEndpoint()
		{
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Intializing SIP Endpoint", new object[]
			{
				11322
			});
			UMService.umConnMgr.EnsureListeningForCalls();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000054F8 File Offset: 0x000036F8
		private WorkerProcessManager CreateAndStartWorkerProcessManager(string workerPath)
		{
			WorkerProcessManager workerProcessManager = null;
			WorkerProcessManager.Create(workerPath, this.jobObject, out workerProcessManager);
			workerProcessManager.Start();
			return workerProcessManager;
		}

		// Token: 0x04000026 RID: 38
		private static FileStream testModeCanary;

		// Token: 0x04000027 RID: 39
		private static UMConnectionManagerHelper.UMConnectionManager umConnMgr;

		// Token: 0x04000028 RID: 40
		private WorkerProcessManager workerProcessManager;

		// Token: 0x04000029 RID: 41
		private SafeJobHandle jobObject;

		// Token: 0x0400002A RID: 42
		private Timer pingTimer;

		// Token: 0x0200000E RID: 14
		public enum GatewayHealth
		{
			// Token: 0x0400002C RID: 44
			Good,
			// Token: 0x0400002D RID: 45
			LateResponse,
			// Token: 0x0400002E RID: 46
			Error
		}

		// Token: 0x0200000F RID: 15
		private class UMServiceCertificateDiagnostics : CertificateDiagnostics
		{
			// Token: 0x0600007D RID: 125 RVA: 0x00005524 File Offset: 0x00003724
			public UMServiceCertificateDiagnostics(X509Certificate2 certificate) : base(certificate)
			{
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600007E RID: 126 RVA: 0x0000552D File Offset: 0x0000372D
			protected override ExEventLog.EventTuple CertificateDetailsEventTuple
			{
				get
				{
					return UMEventLogConstants.Tuple_ServiceCertificateDetails;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x0600007F RID: 127 RVA: 0x00005534 File Offset: 0x00003734
			protected override ExEventLog.EventTuple CertificateAboutToExpireEventTuple
			{
				get
				{
					return UMEventLogConstants.Tuple_CertificateNearExpiry;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000080 RID: 128 RVA: 0x0000553B File Offset: 0x0000373B
			protected override ExEventLog.EventTuple CertificateExpirationOkEventTuple
			{
				get
				{
					return UMEventLogConstants.Tuple_CertificateExpiryIsGood;
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000081 RID: 129 RVA: 0x00005542 File Offset: 0x00003742
			protected override UMNotificationEvent CertificateNearExpiry
			{
				get
				{
					return UMNotificationEvent.CertificateNearExpiry;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000082 RID: 130 RVA: 0x00005545 File Offset: 0x00003745
			protected override Microsoft.Office.Datacenter.ActiveMonitoring.Component UMExchangeComponent
			{
				get
				{
					return ExchangeComponent.UMProtocol;
				}
			}

			// Token: 0x06000083 RID: 131 RVA: 0x0000554C File Offset: 0x0000374C
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UMService.UMServiceCertificateDiagnostics>(this);
			}
		}
	}
}
