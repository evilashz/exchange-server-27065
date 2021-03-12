using System;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCore.Exceptions;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000267 RID: 615
	internal abstract class UMServiceBase : ExServiceBase, IDisposeTrackable, IDisposable
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00050534 File Offset: 0x0004E734
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x0005053B File Offset: 0x0004E73B
		public static string ServiceShortName { get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x00050543 File Offset: 0x0004E743
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x0005054A File Offset: 0x0004E74A
		public static LocalizedString ServiceNameForEventLogging { get; private set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00050552 File Offset: 0x0004E752
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x00050559 File Offset: 0x0004E759
		public static LocalizedString ServerNameForEventLogging { get; private set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00050561 File Offset: 0x0004E761
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x00050569 File Offset: 0x0004E769
		public UMADSettings ADSettings { get; protected set; }

		// Token: 0x06001236 RID: 4662 RVA: 0x00050574 File Offset: 0x0004E774
		public UMServiceBase()
		{
			ProcessLog.WriteLine("UMAbstractService::C'tor", new object[0]);
			base.ServiceName = UMServiceBase.ServiceShortName;
			base.CanStop = true;
			base.CanPauseAndContinue = false;
			base.AutoLog = false;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000505C4 File Offset: 0x0004E7C4
		public static void GlobalExceptionHandler(object sender, UnhandledExceptionEventArgs eventArgs)
		{
			try
			{
				Exception ex = (Exception)eventArgs.ExceptionObject;
				string text = CommonUtil.ToEventLogString(ex);
				if (ex is UMServiceBaseException || ex is UnableToInitializeResourceException || ex is ExchangeServerNotFoundException)
				{
					if (!UMServiceBase.hasServiceStarted)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStartFailure, null, new object[]
						{
							UMServiceBase.ServiceNameForEventLogging,
							text
						});
					}
					else
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceFatalException, null, new object[]
						{
							UMServiceBase.ServiceNameForEventLogging,
							text
						});
					}
				}
				else
				{
					if (!UMServiceBase.hasServiceStarted)
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStartFailure, null, new object[]
						{
							UMServiceBase.ServiceNameForEventLogging,
							text
						});
					}
					else
					{
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceUnhandledException, null, new object[]
						{
							UMServiceBase.ServiceNameForEventLogging,
							text
						});
					}
					lock (UMServiceBase.mutex)
					{
						CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "Unhandled Exception Received, Sending Watson Report. Exception = " + ex.ToString(), new object[0]);
						using (ITempFile tempFile = Breadcrumbs.GenerateDump())
						{
							using (ITempFile tempFile2 = ProcessLog.GenerateDump())
							{
								ExWatson.TryAddExtraFile(tempFile.FilePath);
								ExWatson.TryAddExtraFile(tempFile2.FilePath);
								ExWatson.HandleException(sender, eventArgs);
							}
						}
					}
				}
			}
			finally
			{
				Utils.KillThisProcess();
			}
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000507C8 File Offset: 0x0004E9C8
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UMServiceBase>(this);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x000507D0 File Offset: 0x0004E9D0
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000507E8 File Offset: 0x0004E9E8
		public X509Certificate2 GetCertificateFromThumbprint(string thumbprint)
		{
			X509Certificate2 x509Certificate;
			try
			{
				x509Certificate = CertificateUtils.FindCertByThumbprint(thumbprint);
			}
			catch (SecurityException innerException)
			{
				throw new UMServiceBaseException(Strings.UnableToFindCertificate(thumbprint, UMServiceBase.ServerNameForEventLogging), innerException);
			}
			catch (CryptographicException innerException2)
			{
				throw new UMServiceBaseException(Strings.UnableToFindCertificate(thumbprint, UMServiceBase.ServerNameForEventLogging), innerException2);
			}
			if (x509Certificate == null)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UMServiceBase.UpdateCertificate: Unable to find the cert in the store.", new object[0]);
				throw new UMServiceBaseException(Strings.UnableToFindCertificate(thumbprint, UMServiceBase.ServerNameForEventLogging));
			}
			if (CertificateUtils.IsExpired(x509Certificate))
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceTracer, 0, "UMServiceBase.UpdateCertificate: Expired Certificate.", new object[0]);
				throw new UMServiceBaseException(Strings.ExpiredCertificate(thumbprint, UMServiceBase.ServerNameForEventLogging));
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "UMServiceBase.UpdateCertificate: Certificate found in the store with Thumbprint '{0}' and Issuer '{1}'.", new object[]
			{
				x509Certificate.Thumbprint,
				x509Certificate.Issuer
			});
			return x509Certificate;
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x000508FC File Offset: 0x0004EAFC
		protected static void Initialize(string serviceShortName, LocalizedString serviceName, LocalizedString serverName)
		{
			ProcessLog.WriteLine("UMAbstractService::Initialize", new object[0]);
			UMServiceBase.ServiceShortName = serviceShortName;
			UMServiceBase.ServiceNameForEventLogging = serviceName;
			UMServiceBase.ServerNameForEventLogging = serverName;
			int num = Privileges.RemoveAllExcept(new string[]
			{
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeIncreaseQuotaPrivilege",
				"SeAssignPrimaryTokenPrivilege"
			});
			if (num != 0)
			{
				UMServiceBaseException exception = new UMServiceBaseException(Strings.UnableToRemovePermissions(UMServiceBase.ServiceNameForEventLogging, num));
				UMServiceBase.GlobalExceptionHandler(null, new UnhandledExceptionEventArgs(exception, true));
			}
			ProcessLog.WriteLine("UMAbstractService::Initialize: Removed unnecessary privileges", new object[0]);
			ExWatson.Init();
			ProcessLog.WriteLine("UMAbstractService::Initialize: Initialized Watson.", new object[0]);
			AppDomain.CurrentDomain.UnhandledException += UMServiceBase.GlobalExceptionHandler;
			ProcessLog.WriteLine("UMAbstractService::Initialize: Register for Unhandled Exceptions", new object[0]);
			Globals.InitializeSinglePerfCounterInstance();
			ProcessLog.WriteLine("Initialize: Initialized single performance counters.", new object[0]);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000509EE File Offset: 0x0004EBEE
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00050A14 File Offset: 0x0004EC14
		protected override void OnStopInternal()
		{
			ProcessLog.WriteLine("UMServiceBase::OnStopInternal", new object[0]);
			try
			{
				this.OnEventStartTime = ExDateTime.UtcNow;
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStopTracer, 0, "Stopping UMServiceBase {0}", new object[]
				{
					ExDateTime.UtcNow
				});
				ADNotificationsManager.Instance.Dispose();
				UmServiceGlobals.ShuttingDown = true;
				ProcessLog.WriteLine("OnStopInternal: Shutdown AD notifications.", new object[0]);
				lock (this)
				{
					this.DisposeModeChangedTimer();
				}
				this.InternalStop();
				if (this.certificateDiagnostics != null)
				{
					this.certificateDiagnostics.Dispose();
					this.certificateDiagnostics = null;
				}
				if (InstrumentationCollector.Stop())
				{
					ProcessLog.WriteLine("OnStopInternal: InstrumentationCollector Stopped", new object[0]);
				}
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStopSuccess, null, new object[]
				{
					UMServiceBase.ServiceNameForEventLogging
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStopTracer, 0, "Service stopped successfully", new object[0]);
				ProcessLog.WriteLine("OnStopInternal: Success.", new object[0]);
			}
			catch (Exception exception)
			{
				UMServiceBase.GlobalExceptionHandler(this, new UnhandledExceptionEventArgs(exception, true));
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00050B78 File Offset: 0x0004ED78
		protected override void OnStartInternal(string[] args)
		{
			ProcessLog.WriteLine("UMServiceBase::OnStartInternal", new object[0]);
			this.OnEventStartTime = ExDateTime.UtcNow;
			try
			{
				this.StartService();
				UMServiceBase.hasServiceStarted = true;
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMServiceStartSuccess, null, new object[]
				{
					UMServiceBase.ServiceNameForEventLogging
				});
				CallIdTracer.TraceDebug(ExTraceGlobals.ServiceStartTracer, 0, "UMAbstractService started successfully", new object[0]);
				ProcessLog.WriteLine("OnStartInternal: Service Started Sucessfully", new object[0]);
			}
			catch (Exception ex)
			{
				CallIdTracer.TraceError(ExTraceGlobals.ServiceStartTracer, 0, "UMServiceBase failed to start ", new object[]
				{
					ex.ToString()
				});
				if (ex is ADTransientException || ex is ADExternalException)
				{
					ex = new UMServiceBaseException(ex.Message, ex);
				}
				UMServiceBase.GlobalExceptionHandler(this, new UnhandledExceptionEventArgs(ex, true));
				throw;
			}
		}

		// Token: 0x0600123F RID: 4671
		protected abstract void InternalStart();

		// Token: 0x06001240 RID: 4672
		protected abstract void InternalStop();

		// Token: 0x06001241 RID: 4673
		protected abstract CertificateDiagnostics CreateCertificateDiagnostics();

		// Token: 0x06001242 RID: 4674
		protected abstract void HandleCertChange();

		// Token: 0x06001243 RID: 4675
		protected abstract void LoadServiceADSettings();

		// Token: 0x06001244 RID: 4676 RVA: 0x00050C64 File Offset: 0x0004EE64
		protected void ADServerUpdateCallback(ADNotificationEventArgs args)
		{
			lock (this)
			{
				if (args != null && args.Id != null && args.ChangeType == ADNotificationChangeType.ModifyOrAdd && this.ADSettings.Id.Equals(args.Id))
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "ADServerUpdateCallback is for the local configuration.", new object[0]);
					this.ADSettings = this.ADSettings.RefreshFromAD();
					if (UMRecyclerConfig.UMStartupType != this.ADSettings.UMStartupMode)
					{
						this.StartModeChangedTimer(this.ADSettings.UMStartupMode);
					}
					else
					{
						this.DisposeModeChangedTimer();
						this.UpdateCertificateIfNeeded(UMRecyclerConfig.UMStartupType, this.ADSettings.UMCertificateThumbprint);
					}
				}
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00050D3C File Offset: 0x0004EF3C
		private static void VerifyDatacenterMandatorySettings()
		{
			if (CommonConstants.UseDataCenterCallRouting)
			{
				if (SipPeerManager.Instance.SIPAccessService == null)
				{
					throw new UMServiceBaseException(Strings.SIPAccessServiceNotSet);
				}
				if (SipPeerManager.Instance.SBCService == null)
				{
					throw new UMServiceBaseException(Strings.SIPSessionBorderControllerNotSet);
				}
				if (SipPeerManager.Instance.AVAuthenticationService == null)
				{
					throw new UMServiceBaseException(Strings.AVAuthenticationServiceNotSet);
				}
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00050DA4 File Offset: 0x0004EFA4
		private void StartService()
		{
			ProcessLog.WriteLine("UMServiceBase::StartService", new object[0]);
			this.LogServiceStateChangeInfo("LoadServiceADSettings");
			this.LoadServiceADSettings();
			this.LogServiceStateChangeInfo("InstrumentationCollectorStart");
			if (InstrumentationCollector.Start(new SystemInstrumentationStrategy()))
			{
				ProcessLog.WriteLine("UMService:: InstrumentationCollector Started", new object[0]);
			}
			CallIdTracer.TracePfd(ExTraceGlobals.ServiceStartTracer, 0, "PFD UMS {0} - Starting UM Service {1}", new object[]
			{
				9210,
				ExDateTime.UtcNow
			});
			this.LogServiceStateChangeInfo("UMRecyclerConfigInit");
			UMRecyclerConfig.Init(this.ADSettings);
			ProcessLog.WriteLine("StartService: Initialized the UMRecycler config.", new object[0]);
			UmServiceGlobals.StartupMode = UMRecyclerConfig.UMStartupType;
			this.LogServiceStateChangeInfo("SipPeerManagerInitialize");
			SipPeerManager.Initialize(false, this.ADSettings);
			ProcessLog.WriteLine("StartService: Initialized SipPeerManager.", new object[0]);
			this.LogServiceStateChangeInfo("VerifyDatacenterMandatorySettings");
			UMServiceBase.VerifyDatacenterMandatorySettings();
			ProcessLog.WriteLine("StartService: Verified DatacenterMandatorySettings", new object[0]);
			this.InternalStart();
			this.LogServiceStateChangeInfo("InitializeCertificateDiagnosticsIfNecessary");
			this.InitializeCertificateDiagnosticsIfNecessary();
			this.LogServiceStateChangeInfo("InsertStartupNotification");
			StartupNotification.InsertStartupNotification(UMServiceBase.ServiceShortName);
			this.LogServiceStateChangeInfo("StartService completed");
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00050EDE File Offset: 0x0004F0DE
		private void DisposeModeChangedTimer()
		{
			if (this.modeChangedTimer != null)
			{
				this.modeChangedTimer.Dispose();
				this.modeChangedTimer = null;
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00050EFA File Offset: 0x0004F0FA
		private void StartModeChangedTimer(UMStartupMode startupMode)
		{
			if (this.modeChangedTimer == null)
			{
				this.modeChangedTimer = new Timer(new TimerCallback(this.LogModeChangedEvent), null, 0, UMRecyclerConfig.AlertIntervalAfterStartupModeChanged * 1000);
			}
			this.ChangedMode = startupMode;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00050F30 File Offset: 0x0004F130
		private void LogModeChangedEvent(object dummyObject)
		{
			lock (this)
			{
				if (this.modeChangedTimer != null)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_UMStartupModeChanged, null, new object[]
					{
						UMServiceBase.ServiceNameForEventLogging,
						LocalizedDescriptionAttribute.FromEnum(typeof(UMStartupMode), UMRecyclerConfig.UMStartupType),
						LocalizedDescriptionAttribute.FromEnum(typeof(UMStartupMode), this.ChangedMode)
					});
				}
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00050FCC File Offset: 0x0004F1CC
		private void UpdateCertificateIfNeeded(UMStartupMode currentMode, string newCert)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.ServiceTracer, 0, "UMServiceBase.UpdateCertificate: Current mode {0}, and New thumbprint {1}.", new object[]
			{
				currentMode,
				newCert
			});
			if (currentMode != UMStartupMode.TCP && !string.IsNullOrEmpty(newCert) && !newCert.Equals(CertificateUtils.UMCertificate.Thumbprint, StringComparison.OrdinalIgnoreCase))
			{
				CertificateUtils.UMCertificate = this.GetCertificateFromThumbprint(newCert);
				this.HandleCertChange();
				this.InitializeCertificateDiagnosticsIfNecessary();
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00051039 File Offset: 0x0004F239
		private void InitializeCertificateDiagnosticsIfNecessary()
		{
			if (this.certificateDiagnostics != null)
			{
				this.certificateDiagnostics.Dispose();
			}
			if (UmServiceGlobals.StartupMode != UMStartupMode.TCP)
			{
				this.certificateDiagnostics = this.CreateCertificateDiagnostics();
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00051061 File Offset: 0x0004F261
		protected void LogServiceStateChangeInfo(string info)
		{
			base.LogExWatsonTimeoutServiceStateChangeInfo(info + Environment.NewLine);
		}

		// Token: 0x04000C01 RID: 3073
		protected static volatile bool hasServiceStarted;

		// Token: 0x04000C02 RID: 3074
		protected static UMServiceBase umService;

		// Token: 0x04000C03 RID: 3075
		public ExDateTime OnEventStartTime;

		// Token: 0x04000C04 RID: 3076
		private static object mutex = new object();

		// Token: 0x04000C05 RID: 3077
		private DisposeTracker disposeTracker;

		// Token: 0x04000C06 RID: 3078
		private Timer modeChangedTimer;

		// Token: 0x04000C07 RID: 3079
		private CertificateDiagnostics certificateDiagnostics;

		// Token: 0x04000C08 RID: 3080
		public UMStartupMode ChangedMode;
	}
}
