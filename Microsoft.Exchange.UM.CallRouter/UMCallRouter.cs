using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.CallRouter.Exceptions;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.CallRouter
{
	// Token: 0x02000002 RID: 2
	internal sealed class UMCallRouter : UMServiceBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public UMCallRouter()
		{
			this.Diag = new DiagnosticHelper(this, ExTraceGlobals.UMCallRouterTracer);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E9 File Offset: 0x000002E9
		// (set) Token: 0x06000003 RID: 3 RVA: 0x000020F1 File Offset: 0x000002F1
		private DiagnosticHelper Diag { get; set; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public static void Main(string[] args)
		{
			ProcessLog.WriteLine("UMCallRouter::Main", new object[0]);
			UMServiceBase.Initialize("MSExchangeUMCR", Strings.ServiceName, Strings.Server);
			UMServiceBase.umService = new UMCallRouter();
			UMCallRouter.InitializePerfCounters();
			if (!Environment.UserInteractive)
			{
				ServiceBase.Run(UMServiceBase.umService);
				return;
			}
			ExServiceBase.RunAsConsole(UMServiceBase.umService);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002158 File Offset: 0x00000358
		protected override void HandleCertChange()
		{
			this.Diag.Trace("UMCallRouter : HandleCertChange", new object[0]);
			UMCallRouter.platform.ChangeCertificate();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000217A File Offset: 0x0000037A
		protected override void LoadServiceADSettings()
		{
			base.ADSettings = new UMCallRouterADSettings();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
		protected override void InternalStop()
		{
			this.Diag.Trace("UMCallRouter : InternalStop", new object[0]);
			lock (this)
			{
				if (this.pingTimer != null)
				{
					this.pingTimer.Dispose();
					this.pingTimer = null;
				}
				this.DisposeMaintenanceModeCheckTimer();
				if (UMCallRouter.platform != null)
				{
					UMCallRouter.platform.Dispose();
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002204 File Offset: 0x00000404
		protected override CertificateDiagnostics CreateCertificateDiagnostics()
		{
			return new UMCallRouter.UMCallRouterCertificateDiagnostics(CertificateUtils.UMCertificate);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002210 File Offset: 0x00000410
		protected override void InternalStart()
		{
			this.Diag.Trace("UMCallRouter : InternalStart", new object[0]);
			base.LogServiceStateChangeInfo("CreateCallRouterVoipPlatform");
			UMCallRouter.platform = Platform.Builder.CreateCallRouterVoipPlatform(UMServiceBase.ServiceNameForEventLogging, UMServiceBase.ServerNameForEventLogging, base.ADSettings);
			this.Diag.Trace("UMCallRouter: InternalStart : Initialized platform.", new object[0]);
			base.LogServiceStateChangeInfo("CafeCallStatisticsLoggerInit");
			CafeCallStatisticsLogger.Instance.Init();
			CallPerformanceLogger.Instance.Init();
			CallRejectionLogger.Instance.Init();
			base.LogServiceStateChangeInfo("StartPlatform");
			this.StartPlatform();
			this.maintenanceModeCheckTimer = new Timer(new TimerCallback(this.CheckMaintenanceModeBit), null, 0, 60000);
			this.Diag.Trace("UMCallRouter: InternalStart: Started SIP endpoint.", new object[0]);
			this.Diag.Trace("UMCallRouter: InternalStart:  Success.", new object[0]);
			base.LogServiceStateChangeInfo("RegisterADNotification");
			ADNotificationsManager.Instance.CallRouterSettings.ConfigChanged += base.ADServerUpdateCallback;
			this.Diag.Trace("StartService: Registered for AD notifications.", new object[0]);
			base.LogServiceStateChangeInfo("InternalStartCompleted");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002340 File Offset: 0x00000540
		private static void InitializePerfCounters()
		{
			try
			{
				Utils.InitializePerformanceCounters(typeof(CallRouterAvailabilityCounters));
				UmServiceGlobals.ArePerfCountersEnabled = true;
			}
			catch (InvalidOperationException ex)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.UMCallRouterTracer, null, "UMCallRouter : Failed to initialize perfmon counters, perf data will not be available. Error: {0} ArePerfCountersEnabled= {1}", new object[]
				{
					ex,
					UmServiceGlobals.ArePerfCountersEnabled
				});
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023A0 File Offset: 0x000005A0
		private static bool IsOnline()
		{
			return ServerComponentStateManager.IsOnline(ServerComponentEnum.UMCallRouter);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023AC File Offset: 0x000005AC
		private void HandleUMCallRouterComponentStatusChange()
		{
			this.Diag.Trace("UMCallRouter : HandleUMCallRouterComponentStatusChange Server Online = {0}", new object[]
			{
				this.isOnline
			});
			if (this.isOnline)
			{
				UMCallRouter.platform.StartListening();
				return;
			}
			UMCallRouter.platform.StopListening();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023FC File Offset: 0x000005FC
		private void DisposeMaintenanceModeCheckTimer()
		{
			if (this.maintenanceModeCheckTimer != null)
			{
				this.maintenanceModeCheckTimer.Dispose();
				this.maintenanceModeCheckTimer = null;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002418 File Offset: 0x00000618
		private void CheckMaintenanceModeBit(object dummyObject)
		{
			lock (this)
			{
				if (this.maintenanceModeCheckTimer != null)
				{
					bool flag2 = UMCallRouter.IsOnline();
					if (flag2 != this.isOnline)
					{
						this.isOnline = flag2;
						this.HandleUMCallRouterComponentStatusChange();
					}
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002474 File Offset: 0x00000674
		private void StartPlatform()
		{
			this.Diag.Trace("UMCallRouter: StartPlatform.", new object[0]);
			if (UmServiceGlobals.StartupMode != UMStartupMode.TCP)
			{
				base.LogServiceStateChangeInfo("GetCertificateFromThumbprint");
				CertificateUtils.UMCertificate = base.GetCertificateFromThumbprint(base.ADSettings.UMCertificateThumbprint);
			}
			if (UMCallRouter.IsOnline())
			{
				this.isOnline = true;
				base.LogServiceStateChangeInfo("StartListening");
				UMCallRouter.platform.StartListening();
				return;
			}
			this.isOnline = false;
			base.LogServiceStateChangeInfo("StopListening");
			UMCallRouter.platform.StopListening();
			this.Diag.Trace("UMCallRouter : Listening on only Loopback endpoint since server is not online", new object[0]);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMCallRouterSocketShutdown, null, new object[0]);
		}

		// Token: 0x04000001 RID: 1
		private const int MaintenanceModeCheckIntervalSeconds = 60;

		// Token: 0x04000002 RID: 2
		private static BaseCallRouterPlatform platform;

		// Token: 0x04000003 RID: 3
		private Timer pingTimer;

		// Token: 0x04000004 RID: 4
		private Timer maintenanceModeCheckTimer;

		// Token: 0x04000005 RID: 5
		private bool isOnline;

		// Token: 0x02000003 RID: 3
		private class UMCallRouterCertificateDiagnostics : CertificateDiagnostics
		{
			// Token: 0x06000010 RID: 16 RVA: 0x0000252C File Offset: 0x0000072C
			public UMCallRouterCertificateDiagnostics(X509Certificate2 certificate) : base(certificate)
			{
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000011 RID: 17 RVA: 0x00002535 File Offset: 0x00000735
			protected override ExEventLog.EventTuple CertificateDetailsEventTuple
			{
				get
				{
					return UMEventLogConstants.Tuple_CallRouterCertificateDetails;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000012 RID: 18 RVA: 0x0000253C File Offset: 0x0000073C
			protected override ExEventLog.EventTuple CertificateAboutToExpireEventTuple
			{
				get
				{
					return UMEventLogConstants.Tuple_CallRouterCertificateNearExpiry;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000013 RID: 19 RVA: 0x00002543 File Offset: 0x00000743
			protected override ExEventLog.EventTuple CertificateExpirationOkEventTuple
			{
				get
				{
					return UMEventLogConstants.Tuple_CallRouterCertificateExpiryIsGood;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000014 RID: 20 RVA: 0x0000254A File Offset: 0x0000074A
			protected override UMNotificationEvent CertificateNearExpiry
			{
				get
				{
					return UMNotificationEvent.CallRouterCertificateNearExpiry;
				}
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000015 RID: 21 RVA: 0x0000254D File Offset: 0x0000074D
			protected override Component UMExchangeComponent
			{
				get
				{
					return ExchangeComponent.UMCallRouter;
				}
			}

			// Token: 0x06000016 RID: 22 RVA: 0x00002554 File Offset: 0x00000754
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<UMCallRouter.UMCallRouterCertificateDiagnostics>(this);
			}
		}
	}
}
