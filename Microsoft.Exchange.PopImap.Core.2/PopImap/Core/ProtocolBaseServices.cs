using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000002 RID: 2
	internal abstract class ProtocolBaseServices : ControlObject.TransportWorker, IWorkerProcess, IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public ProtocolBaseServices()
		{
			ProtocolBaseServices.serverName = ComputerInformation.DnsFullyQualifiedDomainName;
			ProtocolBaseServices.serverVersion = "15.00.1497.010";
			ProtocolBaseServices.isMultiTenancyEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;
			this.LoadFlightingState();
			this.IsPartnerHostedOnly = DatacenterRegistry.IsPartnerHostedOnly();
			this.disposed = false;
			if (ProtocolBaseServices.service != null)
			{
				throw new ApplicationException("The service object already assigned!");
			}
			ProtocolBaseServices.service = this;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002143 File Offset: 0x00000343
		// (set) Token: 0x06000003 RID: 3 RVA: 0x0000214A File Offset: 0x0000034A
		public static Microsoft.Exchange.Diagnostics.Trace ServerTracer
		{
			get
			{
				return ProtocolBaseServices.serverTracer;
			}
			set
			{
				ProtocolBaseServices.serverTracer = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002152 File Offset: 0x00000352
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002159 File Offset: 0x00000359
		public static Microsoft.Exchange.Diagnostics.Trace SessionTracer
		{
			get
			{
				return ProtocolBaseServices.sessionTracer;
			}
			set
			{
				ProtocolBaseServices.sessionTracer = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002161 File Offset: 0x00000361
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ProtocolBaseServices.faultInjectionTracer == null)
				{
					ProtocolBaseServices.faultInjectionTracer = new FaultInjectionTrace(ProtocolBaseServices.popImapCoreComponentGuid, 0);
				}
				return ProtocolBaseServices.faultInjectionTracer;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000217F File Offset: 0x0000037F
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002186 File Offset: 0x00000386
		public static TroubleshootingContext TroubleshootingContext
		{
			get
			{
				return ProtocolBaseServices.troubleshootingContext;
			}
			set
			{
				ProtocolBaseServices.troubleshootingContext = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000218E File Offset: 0x0000038E
		public static string ServerName
		{
			get
			{
				return ProtocolBaseServices.serverName;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002195 File Offset: 0x00000395
		public static string ServerVersion
		{
			get
			{
				return ProtocolBaseServices.serverVersion;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000219C File Offset: 0x0000039C
		public static string ExchangeSetupLocation
		{
			get
			{
				string result;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
				{
					if (registryKey == null)
					{
						throw new ApplicationException("ExchangeSetupLocation can't be found!");
					}
					result = (string)registryKey.GetValue("MsiInstallPath", null);
				}
				return result;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021F8 File Offset: 0x000003F8
		public static VirtualServer VirtualServer
		{
			get
			{
				return ProtocolBaseServices.service.virtualServer;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002204 File Offset: 0x00000404
		public static ProtocolBaseServices Service
		{
			get
			{
				return ProtocolBaseServices.service;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000220B File Offset: 0x0000040B
		public static bool IsMultiTenancyEnabled
		{
			get
			{
				return ProtocolBaseServices.isMultiTenancyEnabled;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002212 File Offset: 0x00000412
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002219 File Offset: 0x00000419
		public static bool StoredSecretKeysValid { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002221 File Offset: 0x00000421
		public static bool GCCEnabledWithKeys
		{
			get
			{
				return ResponseFactory.GlobalCriminalComplianceEnabled && ProtocolBaseServices.StoredSecretKeysValid;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002231 File Offset: 0x00000431
		public static ServerServiceRole ServerRoleService
		{
			get
			{
				return ProtocolBaseServices.serverRoleService;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002238 File Offset: 0x00000438
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000223F File Offset: 0x0000043F
		public static ServerComponentEnum TargetProtocol
		{
			get
			{
				return ProtocolBaseServices.targetProtocol;
			}
			set
			{
				ProtocolBaseServices.targetProtocol = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002247 File Offset: 0x00000447
		public bool EnableExactRFC822Size
		{
			get
			{
				return this.configuration.EnableExactRFC822Size;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002254 File Offset: 0x00000454
		public int ConnectionCount
		{
			get
			{
				return this.virtualServer.ConnectionCount;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23
		public abstract string MaxConnectionsError { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24
		public abstract string NoSslCertificateError { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000019 RID: 25
		public abstract string TimeoutError { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001A RID: 26
		public abstract string ServerShutdownMessage { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001B RID: 27
		public abstract ExEventLog.EventTuple ServerStartEventTuple { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001C RID: 28
		public abstract ExEventLog.EventTuple ServerStopEventTuple { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001D RID: 29
		public abstract ExEventLog.EventTuple NoConfigurationFoundEventTuple { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001E RID: 30
		public abstract ExEventLog.EventTuple BasicOverPlainTextEventTuple { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001F RID: 31
		public abstract ExEventLog.EventTuple MaxConnectionCountExceededEventTuple { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000020 RID: 32
		public abstract ExEventLog.EventTuple MaxConnectionsFromSingleIpExceededEventTuple { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000021 RID: 33
		public abstract ExEventLog.EventTuple SslConnectionNotStartedEventTuple { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000022 RID: 34
		public abstract ExEventLog.EventTuple PortBusyEventTuple { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000023 RID: 35
		public abstract ExEventLog.EventTuple SslCertificateNotFoundEventTuple { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000024 RID: 36
		public abstract ExEventLog.EventTuple ProcessNotRespondingEventTuple { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000025 RID: 37
		public abstract ExEventLog.EventTuple ControlAddressInUseEventTuple { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000026 RID: 38
		public abstract ExEventLog.EventTuple ControlAddressDeniedEventTuple { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000027 RID: 39
		public abstract ExEventLog.EventTuple SpnRegisterFailureEventTuple { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000028 RID: 40
		public abstract ExEventLog.EventTuple CreateMailboxLoggerFailedEventTuple { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000029 RID: 41
		public abstract ExEventLog.EventTuple NoPerfCounterTimerEventTuple { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600002A RID: 42
		public abstract ExEventLog.EventTuple OnlineValueChangedEventTuple { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600002B RID: 43
		public abstract ExEventLog.EventTuple BadPasswordCodePageEventTuple { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600002C RID: 44
		public abstract ExEventLog.EventTuple LrsListErrorEventTuple { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600002D RID: 45
		public abstract ExEventLog.EventTuple LrsPartnerResolutionWarningEventTuple { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002261 File Offset: 0x00000461
		public LoginOptions LoginType
		{
			get
			{
				return this.configuration.LoginType;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000226E File Offset: 0x0000046E
		public bool LiveIdBasicAuthReplacement
		{
			get
			{
				return this.configuration.LiveIdBasicAuthReplacement;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000227B File Offset: 0x0000047B
		public bool SuppressReadReceipt
		{
			get
			{
				return this.configuration.SuppressReadReceipt;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002288 File Offset: 0x00000488
		public int ConnectionTimeout
		{
			get
			{
				return (int)this.configuration.AuthenticatedConnectionTimeout.TotalSeconds;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000022A9 File Offset: 0x000004A9
		public virtual CalendarItemRetrievalOptions CalendarItemRetrievalOption
		{
			get
			{
				return this.configuration.CalendarItemRetrievalOption;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000022B6 File Offset: 0x000004B6
		public virtual string OwaServer
		{
			get
			{
				return this.configuration.OwaServerUrl.AbsoluteUri;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000022C8 File Offset: 0x000004C8
		public int PreAuthConnectionTimeout
		{
			get
			{
				return (int)this.configuration.PreAuthenticatedConnectionTimeout.TotalSeconds;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000022E9 File Offset: 0x000004E9
		public int MaxCommandLength
		{
			get
			{
				return this.configuration.MaxCommandSize;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000022F6 File Offset: 0x000004F6
		public int MaxConnectionCount
		{
			get
			{
				return this.configuration.MaxConnections;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002303 File Offset: 0x00000503
		public int MaxConcurrentConnectionsFromSingleIp
		{
			get
			{
				return this.configuration.MaxConnectionFromSingleIP;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002310 File Offset: 0x00000510
		public int MaxConnectionsPerUser
		{
			get
			{
				return this.configuration.MaxConnectionsPerUser;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000231D File Offset: 0x0000051D
		public MimeTextFormat MessagesRetrievalMimeTextFormat
		{
			get
			{
				return this.configuration.MessageRetrievalMimeFormat;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000232A File Offset: 0x0000052A
		public int ProxyPort
		{
			get
			{
				return this.configuration.ProxyTargetPort;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002337 File Offset: 0x00000537
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000233E File Offset: 0x0000053E
		internal static string ServiceName
		{
			get
			{
				return ProtocolBaseServices.serviceName;
			}
			set
			{
				ProtocolBaseServices.serviceName = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002346 File Offset: 0x00000546
		// (set) Token: 0x0600003E RID: 62 RVA: 0x0000234D File Offset: 0x0000054D
		internal static string ShortServiceName { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002355 File Offset: 0x00000555
		// (set) Token: 0x06000040 RID: 64 RVA: 0x0000235C File Offset: 0x0000055C
		internal static bool EnforceCertificateErrors { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002364 File Offset: 0x00000564
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000236B File Offset: 0x0000056B
		internal static bool LrsLogEnabled { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002374 File Offset: 0x00000574
		internal static List<OrganizationId> LrsPartners
		{
			get
			{
				if (!ProtocolBaseServices.LrsLogEnabled)
				{
					return null;
				}
				lock (ProtocolBaseServices.service)
				{
					if (!ProtocolBaseServices.LrsLogEnabled)
					{
						return null;
					}
					if (ProtocolBaseServices.service.lrsPartners == null && !ProtocolBaseServices.service.LoadLrsPartnerList())
					{
						ProtocolBaseServices.LrsLogEnabled = false;
						return null;
					}
				}
				return ProtocolBaseServices.service.lrsPartners;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000023F0 File Offset: 0x000005F0
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000023F7 File Offset: 0x000005F7
		internal static LrsLog LrsLog { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000023FF File Offset: 0x000005FF
		internal LightWeightLog LightLog
		{
			get
			{
				return this.lightLog;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002407 File Offset: 0x00000607
		internal ProtocolLoggingLevel ProtocolLoggingLevel
		{
			get
			{
				if (!this.lightLogEnabled)
				{
					return ProtocolLoggingLevel.None;
				}
				return ProtocolLoggingLevel.Verbose;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002414 File Offset: 0x00000614
		internal int MaxFailedLoginAttempts
		{
			get
			{
				return this.maxFailedLoginAttempts;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000241C File Offset: 0x0000061C
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002424 File Offset: 0x00000624
		internal ExtendedProtectionConfig ExtendedProtectionConfig { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000242D File Offset: 0x0000062D
		internal bool GSSAPIAndNTLMAuthDisabled
		{
			get
			{
				return !this.configuration.EnableGSSAPIAndNTLMAuth || !ResponseFactory.KerberosAuthEnabled;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002446 File Offset: 0x00000646
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000244E File Offset: 0x0000064E
		internal bool IsPartnerHostedOnly { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002457 File Offset: 0x00000657
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000245F File Offset: 0x0000065F
		internal ProtocolConnectionSettings ExternalProxySettings { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002468 File Offset: 0x00000668
		internal int PasswordCodePage
		{
			get
			{
				return this.passwordCodePage;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002477 File Offset: 0x00000677
		protected static ExEventLog Logger
		{
			get
			{
				return ProtocolBaseServices.logger;
			}
			set
			{
				ProtocolBaseServices.logger = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000247F File Offset: 0x0000067F
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002487 File Offset: 0x00000687
		protected PopImapAdConfiguration Configuration
		{
			get
			{
				return this.configuration;
			}
			set
			{
				this.configuration = value;
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002490 File Offset: 0x00000690
		public static void Exit(int exitCode)
		{
			ProtocolBaseServices.ServerTracer.TraceDebug<int>(0L, "Exit {0} was called", exitCode);
			ProtocolBaseServices.InMemoryTraceOperationCompleted(0L);
			Environment.Exit(exitCode);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000024B4 File Offset: 0x000006B4
		public static void SendWatsonForUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Exception ex = e.ExceptionObject as Exception;
			if (ex != null)
			{
				ProtocolBaseServices.SendWatson(ex, true);
				return;
			}
			ExWatson.HandleException(sender, e);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000024DF File Offset: 0x000006DF
		public static void InMemoryTraceOperationCompleted(long id)
		{
			ProtocolBaseServices.TroubleshootingContext.TraceOperationCompletedAndUpdateContext(id);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000024EC File Offset: 0x000006EC
		public static bool AuthErrorReportEnabled(string username)
		{
			if (ProtocolBaseServices.authErrorReportEnabled || ResponseFactory.UseClientIpTestMocks)
			{
				return true;
			}
			if (string.IsNullOrEmpty(username))
			{
				return false;
			}
			username = username.ToLower();
			return username.StartsWith("healthmailbox") || username.EndsWith("exchangemon.net");
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002538 File Offset: 0x00000738
		public static void SendWatson(Exception exception, bool terminating)
		{
			if (ExTraceConfiguration.Instance.InMemoryTracingEnabled)
			{
				ProtocolBaseServices.ServerTracer.TraceError<Exception>(0L, "Exception caught: {0}", exception);
				ProtocolBaseServices.TroubleshootingContext.SendExceptionReportWithTraces(exception, terminating, true);
				return;
			}
			if (exception != TroubleshootingContext.FaultInjectionInvalidOperationException)
			{
				ExWatson.SendReport(exception, terminating ? ReportOptions.ReportTerminateAfterSend : ReportOptions.None, null);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002588 File Offset: 0x00000788
		public static bool IsCriticalException(Exception exception)
		{
			return exception is OutOfMemoryException || exception is StackOverflowException || exception is AccessViolationException || exception is InvalidProgramException || exception is CannotUnloadAppDomainException || exception is BadImageFormatException || exception is TypeInitializationException || exception is TypeLoadException;
		}

		// Token: 0x0600005B RID: 91
		public abstract void LoadFlightingState();

		// Token: 0x0600005C RID: 92 RVA: 0x000025D8 File Offset: 0x000007D8
		void IDisposable.Dispose()
		{
			if (this.resetHandle != null)
			{
				((IDisposable)this.resetHandle).Dispose();
				this.resetHandle = null;
			}
			if (this.stopHandle != null)
			{
				((IDisposable)this.stopHandle).Dispose();
				this.stopHandle = null;
			}
			if (this.readyHandle != null)
			{
				((IDisposable)this.readyHandle).Dispose();
				this.readyHandle = null;
			}
			if (!this.disposed)
			{
				this.disposed = true;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600005D RID: 93
		public abstract PopImapAdConfiguration GetConfiguration(ITopologyConfigurationSession session);

		// Token: 0x0600005E RID: 94 RVA: 0x00002648 File Offset: 0x00000848
		public void Retire()
		{
			try
			{
				this.stopHandle.Release();
			}
			catch (SemaphoreFullException)
			{
				ProtocolBaseServices.ServerTracer.TraceError(0L, "Stop handle already signaled!");
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002688 File Offset: 0x00000888
		public void Stop()
		{
			this.Retire();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002690 File Offset: 0x00000890
		public void Pause()
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002692 File Offset: 0x00000892
		public void Continue()
		{
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002694 File Offset: 0x00000894
		public void Activate()
		{
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002696 File Offset: 0x00000896
		public void ConfigUpdate()
		{
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002698 File Offset: 0x00000898
		public void HandleMemoryPressure()
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000269A File Offset: 0x0000089A
		public void ClearConfigCache()
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000269C File Offset: 0x0000089C
		public void HandleBlockedSubmissionQueue()
		{
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000269E File Offset: 0x0000089E
		public void HandleLogFlush()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000026A0 File Offset: 0x000008A0
		public void HandleForceCrash()
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000026A2 File Offset: 0x000008A2
		public void HandleConnection(Socket clientConnection)
		{
			this.virtualServer.AcceptClientConnection(clientConnection);
		}

		// Token: 0x0600006A RID: 106
		public abstract VirtualServer NewVirtualServer(ProtocolBaseServices server, PopImapAdConfiguration configuration);

		// Token: 0x0600006B RID: 107 RVA: 0x000026B0 File Offset: 0x000008B0
		public void Start(PopImapAdConfiguration configuration, bool selfListening)
		{
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Start");
			if (configuration == null)
			{
				ProtocolBaseServices.ServerTracer.TraceError(0L, "No configuration found.");
				ProtocolBaseServices.Exit(1);
				return;
			}
			ObjectSchema instance = ObjectSchema.GetInstance<PopImapConditionalHandlerSchema>();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("default", "SmtpAddress,TenantName,DisplayName,MailboxServer,Cmd,Parameters,Response,LightLogContext,Exception");
			BaseConditionalRegistration.Initialize(ProtocolBaseServices.ServiceName, instance, instance, dictionary);
			this.configuration = configuration;
			Kerberos.FlushTicketCache();
			if (ProtocolBaseServices.ServerTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ProtocolBaseServices.ServerTracer.Information<string>(0L, "Configuration of the server: {0}", this.configuration.DisplayString());
			}
			this.virtualServer = this.NewVirtualServer(this, this.configuration);
			if (!this.virtualServer.Initialize())
			{
				ProtocolBaseServices.ServerTracer.TraceError(0L, "Could not initialize virtual server.");
				ProtocolBaseServices.Exit(1);
				return;
			}
			StoreSession.UseRPCContextPool = true;
			if (this.LiveIdBasicAuthReplacement)
			{
				int num;
				if (!int.TryParse(ConfigurationManager.AppSettings["PodSiteStartRange"], out num))
				{
					ProtocolBaseServices.ServerTracer.TraceError(0L, "No pod site start range specified in exe.config.");
					ProtocolBaseServices.Exit(1);
					return;
				}
				ResponseFactory.PodSiteStartRange = num;
				if (!int.TryParse(ConfigurationManager.AppSettings["PodSiteEndRange"], out num))
				{
					ProtocolBaseServices.ServerTracer.TraceError(0L, "No pod site end range specified in exe.config.");
					ProtocolBaseServices.Exit(1);
					return;
				}
				ResponseFactory.PodSiteEndRange = num;
				ResponseFactory.PodRedirectTemplate = ConfigurationManager.AppSettings["PodRedirectTemplate"];
				ProtocolBaseServices.StoredSecretKeysValid = GccUtils.AreStoredSecretKeysValid();
			}
			if (!int.TryParse(ConfigurationManager.AppSettings["OfflineTimerCheckSeconds"], out ProtocolBaseServices.onlineCacheThreshold))
			{
				ProtocolBaseServices.onlineCacheThreshold = 60;
			}
			if (!bool.TryParse(ConfigurationManager.AppSettings["AuthErrorReportEnabled"], out ProtocolBaseServices.authErrorReportEnabled))
			{
				ProtocolBaseServices.authErrorReportEnabled = false;
			}
			int num2;
			if (!int.TryParse(ConfigurationManager.AppSettings["MaxIoThreadsPerCPU"], out num2))
			{
				num2 = 0;
			}
			if (num2 < 1 || num2 > 1000)
			{
				num2 = 0;
			}
			int num3;
			int arg;
			if (num2 != 0)
			{
				ThreadPool.GetMaxThreads(out num3, out arg);
				ThreadPool.SetMaxThreads(num3, Environment.ProcessorCount * num2);
			}
			ThreadPool.GetMaxThreads(out num3, out arg);
			ProtocolBaseServices.ServerTracer.Information<int, int>(0L, "Maximum worker threads: {0}, Maximum completion port threads: {1}", num3, arg);
			if (configuration.LoginType == LoginOptions.PlainTextLogin && this.configuration.UnencryptedOrTLSBindings.Count > 0)
			{
				ProtocolBaseServices.ServerTracer.TraceWarning(0L, "Basic authentication is available over plain text connections!");
				ProtocolBaseServices.LogEvent(this.BasicOverPlainTextEventTuple, null, new string[0]);
			}
			if (selfListening)
			{
				List<IPBinding> list = new List<IPBinding>();
				if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox)
				{
					list.Add(new IPBinding(IPAddress.Any, this.configuration.ProxyTargetPort));
					list.Add(new IPBinding(IPAddress.IPv6Any, this.configuration.ProxyTargetPort));
				}
				else
				{
					list = this.configuration.GetBindings();
				}
				this.connectionPools = new List<ConnectionPool>(list.Count);
				foreach (IPBinding ipbinding in list)
				{
					try
					{
						this.connectionPools.Add(new ConnectionPool(ipbinding, new ConnectionPool.ConnectionAcceptedDelegate(this.virtualServer.AcceptClientConnection)));
					}
					catch (SocketException arg2)
					{
						ProtocolBaseServices.ServerTracer.TraceError<IPBinding, SocketException>(0L, "Exception caught while opening port {0}:\r\n{1}", ipbinding, arg2);
						ProtocolBaseServices.LogEvent(this.PortBusyEventTuple, null, new string[]
						{
							ipbinding.ToString()
						});
						ProtocolBaseServices.Exit(1);
					}
				}
			}
			this.ConfigureExtendedProtection();
			ProtocolBaseServices.LogEvent(this.ServerStartEventTuple, null, new string[0]);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002A38 File Offset: 0x00000C38
		public virtual void TerminateAllSessions()
		{
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "TerminateAllSessions");
			this.virtualServer.Stop();
			ProtocolBaseServices.LogEvent(this.ServerStopEventTuple, null, new string[0]);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002A6C File Offset: 0x00000C6C
		public void WaitForHangSignal()
		{
			this.hangHandle.WaitOne();
			ProtocolBaseServices.LogEvent(this.ProcessNotRespondingEventTuple, null, new string[0]);
			string message = string.Format(ProtocolBaseStrings.ProcessNotResponding, "Hang during stop");
			Exception exception = new Exception(message);
			ProtocolBaseServices.SendWatson(exception, true);
			Environment.Exit(1);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002ABC File Offset: 0x00000CBC
		internal static void Assert(bool condition, string formatString, params object[] parameters)
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002ABE File Offset: 0x00000CBE
		internal static bool LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params string[] messageArgs)
		{
			return ProtocolBaseServices.logger.LogEvent(tuple, periodicKey, messageArgs);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002AD0 File Offset: 0x00000CD0
		protected static Exception FaultInjectionCallback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null && exceptionType.Equals("System.InvalidOperationException", StringComparison.OrdinalIgnoreCase))
			{
				result = TroubleshootingContext.FaultInjectionInvalidOperationException;
			}
			return result;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002AF8 File Offset: 0x00000CF8
		protected static void DetermineServiceRole()
		{
			string fileName;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				fileName = currentProcess.MainModule.FileName;
			}
			string directoryName = Path.GetDirectoryName(fileName);
			string directoryName2 = Path.GetDirectoryName(directoryName);
			int num = directoryName2.LastIndexOf("FrontEnd", StringComparison.OrdinalIgnoreCase);
			int num2 = directoryName2.LastIndexOf("ClientAccess", StringComparison.OrdinalIgnoreCase);
			if (num == directoryName2.Length - "FrontEnd".Length)
			{
				ProtocolBaseServices.serverRoleService = ServerServiceRole.cafe;
				return;
			}
			if (num2 == directoryName2.Length - "ClientAccess".Length)
			{
				ProtocolBaseServices.serverRoleService = ServerServiceRole.mailbox;
				return;
			}
			throw new FileLoadException("Installation error: Pop Imap executables not found under ClientAccess or FrontEnd subdirectory: {0}", fileName);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002BA0 File Offset: 0x00000DA0
		protected void ProcessGetConfigurationException(Exception ex)
		{
			if (ex != null)
			{
				ProtocolBaseServices.ServerTracer.TraceError<Exception>(0L, "{0}", ex);
				ProtocolBaseServices.LogEvent(this.NoConfigurationFoundEventTuple, null, new string[]
				{
					ex.Message
				});
				return;
			}
			ProtocolBaseServices.ServerTracer.TraceError(0L, "No configuration found.");
			ProtocolBaseServices.LogEvent(this.NoConfigurationFoundEventTuple, null, new string[]
			{
				string.Empty
			});
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002C10 File Offset: 0x00000E10
		protected void Run(string[] args)
		{
			long value = 0L;
			bool flag = false;
			bool flag2 = false;
			string text = null;
			string name = null;
			string name2 = null;
			string name3 = null;
			ProtocolBaseServices.ServerTracer.TraceDebug<string>(0L, "Process {0} starting", ProtocolBaseServices.ServiceName);
			foreach (string text2 in args)
			{
				if (text2.StartsWith("-?", StringComparison.OrdinalIgnoreCase))
				{
					ProtocolBaseServices.Usage();
					ProtocolBaseServices.Exit(0);
				}
				if (text2.StartsWith("-stopkey:", StringComparison.OrdinalIgnoreCase))
				{
					text = text2.Remove(0, "-stopkey:".Length);
				}
				else if (text2.StartsWith("-hangkey:", StringComparison.OrdinalIgnoreCase))
				{
					name = text2.Remove(0, "-hangkey:".Length);
				}
				else if (text2.StartsWith("-resetkey:", StringComparison.OrdinalIgnoreCase))
				{
					name2 = text2.Remove(0, "-resetkey:".Length);
				}
				else if (text2.StartsWith("-readykey:", StringComparison.OrdinalIgnoreCase))
				{
					name3 = text2.Remove(0, "-readykey:".Length);
				}
				else if (text2.StartsWith("-pipe:", StringComparison.OrdinalIgnoreCase))
				{
					value = long.Parse(text2.Remove(0, "-pipe:".Length));
				}
				else if (text2.StartsWith("-wait", StringComparison.OrdinalIgnoreCase))
				{
					flag2 = true;
				}
				else if (text2.StartsWith("-console", StringComparison.OrdinalIgnoreCase))
				{
					ProtocolBaseServices.runningFromConsole = true;
				}
			}
			flag = !string.IsNullOrEmpty(text);
			if (!flag)
			{
				if (!ProtocolBaseServices.runningFromConsole)
				{
					ProtocolBaseServices.Usage();
					ProtocolBaseServices.Exit(0);
				}
				Console.WriteLine("Starting {0}, running in console mode.", ProtocolBaseServices.ServiceName);
				if (flag2)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			this.resetHandle = ProtocolBaseServices.OpenSemaphore(name2, "reset");
			this.stopHandle = ProtocolBaseServices.OpenSemaphore(text, "stop");
			this.hangHandle = ProtocolBaseServices.OpenSemaphore(name, "hang");
			this.readyHandle = ProtocolBaseServices.OpenSemaphore(name3, "ready");
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Protocolbase Service::Run - Parsed Arguments & Called OpenSemaphore");
			if (this.hangHandle != null)
			{
				Thread thread = new Thread(new ThreadStart(this.WaitForHangSignal));
				thread.Start();
			}
			if (!int.TryParse(ConfigurationManager.AppSettings["MaxFailedLoginAttempts"], out this.maxFailedLoginAttempts))
			{
				this.maxFailedLoginAttempts = 4;
			}
			if (!int.TryParse(ConfigurationManager.AppSettings["PasswordCodePage"], out this.passwordCodePage))
			{
				this.passwordCodePage = 1252;
			}
			if (!this.TestPasswordDecoder(this.passwordCodePage))
			{
				ProtocolBaseServices.Exit(0);
			}
			this.RegisterSpn();
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Protocolbase Service::Run - RegisterSpn called successfully.");
			Globals.InitializeMultiPerfCounterInstance("PopImap");
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Protocolbase Service::Run - Initialized MultiPerf Counter instance for PopImap.");
			string text3 = ConfigurationManager.AppSettings["LogFileNameTemplate"];
			if (string.IsNullOrEmpty(text3))
			{
				if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.cafe)
				{
					text3 = ProtocolBaseServices.serviceName;
				}
				else
				{
					text3 = ProtocolBaseServices.serviceName + "BE";
				}
			}
			if (this.IsPartnerHostedOnly)
			{
				string text4 = ConfigurationManager.AppSettings["ExternalProxy"];
				if (!string.IsNullOrEmpty(text4))
				{
					try
					{
						this.ExternalProxySettings = ProtocolConnectionSettings.Parse(text4);
					}
					catch (FormatException ex)
					{
						ProtocolBaseServices.ServerTracer.TraceError(0L, "ExternalProxy value is invalid.\r\n" + ex.Message);
					}
				}
			}
			PopImapAdConfiguration popImapAdConfiguration = this.GetConfiguration();
			ProtocolBaseServices.ServerTracer.TraceDebug<string>(0L, "Reading configuration from AD was successful. ConfigurationValues:{0}", (popImapAdConfiguration == null) ? "<null>" : popImapAdConfiguration.ToString());
			if (popImapAdConfiguration == null)
			{
				ProtocolBaseServices.Exit(0);
			}
			this.lightLogEnabled = popImapAdConfiguration.ProtocolLogEnabled;
			ProtocolBaseServices.EnforceCertificateErrors = popImapAdConfiguration.EnforceCertificateErrors;
			if (this.lightLogEnabled)
			{
				int num;
				if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DurationToKeepLogsFor"]) || !int.TryParse(ConfigurationManager.AppSettings["DurationToKeepLogsFor"], out num))
				{
					num = 7;
				}
				TimeSpan timeSpan = (num > 0 && ResponseFactory.EnforceLogsRetentionPolicyEnabled) ? TimeSpan.FromDays((double)num) : TimeSpan.MaxValue;
				this.lightLog = new LightWeightLog("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version, ProtocolBaseServices.serviceName + " Log", text3, ProtocolBaseServices.serviceName + "Logs");
				if (!popImapAdConfiguration.LogPerFileSizeQuota.IsUnlimited && popImapAdConfiguration.LogPerFileSizeQuota.Value == ByteQuantifiedSize.Zero)
				{
					this.lightLog.Configure(popImapAdConfiguration.LogFileLocation, popImapAdConfiguration.LogFileRollOverSettings, timeSpan);
				}
				else
				{
					this.lightLog.Configure(popImapAdConfiguration.LogFileLocation, timeSpan, long.MaxValue, (long)(popImapAdConfiguration.LogPerFileSizeQuota.IsUnlimited ? 9223372036854775807UL : popImapAdConfiguration.LogPerFileSizeQuota.Value.ToBytes()));
				}
				ProtocolBaseServices.LrsLogEnabled = (ProtocolBaseServices.IsMultiTenancyEnabled && ResponseFactory.LrsLoggingEnabled && ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox);
				if (ProtocolBaseServices.LrsLogEnabled)
				{
					string text5 = ConfigurationManager.AppSettings["LogFileNameTemplate"];
					if (string.IsNullOrEmpty(text5))
					{
						text5 = ProtocolBaseServices.serviceName;
					}
					text5 += "LRS";
					ProtocolBaseServices.LrsLog = new LrsLog("Microsoft Exchange Server", Assembly.GetExecutingAssembly().GetName().Version, ProtocolBaseServices.serviceName + " Lrs Log", text5, ProtocolBaseServices.serviceName + "LrsLogs");
					if (!popImapAdConfiguration.LogPerFileSizeQuota.IsUnlimited && popImapAdConfiguration.LogPerFileSizeQuota.Value == ByteQuantifiedSize.Zero)
					{
						ProtocolBaseServices.LrsLog.Configure(popImapAdConfiguration.LogFileLocation, popImapAdConfiguration.LogFileRollOverSettings, timeSpan);
					}
					else
					{
						ProtocolBaseServices.LrsLog.Configure(popImapAdConfiguration.LogFileLocation, timeSpan, long.MaxValue, (long)(popImapAdConfiguration.LogPerFileSizeQuota.IsUnlimited ? 9223372036854775807UL : popImapAdConfiguration.LogPerFileSizeQuota.Value.ToBytes()));
					}
				}
			}
			this.Start(popImapAdConfiguration, !flag);
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Register Diagnostics components.");
			ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents();
			PipeStream pipeStream = null;
			if (flag)
			{
				SafeFileHandle handle = new SafeFileHandle(new IntPtr(value), true);
				pipeStream = new PipeStream(handle, FileAccess.Read, true);
				this.controlObject = new ControlObject(pipeStream, this);
				if (this.controlObject.Initialize())
				{
					if (this.readyHandle != null)
					{
						ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Signal the process is ready");
						this.readyHandle.Release();
						this.readyHandle.Close();
						this.readyHandle = null;
					}
					ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Wait for shutdown signal to exit");
					this.stopHandle.WaitOne();
				}
			}
			else
			{
				Console.WriteLine("Press ENTER to exit ");
				Console.ReadLine();
			}
			ProtocolBaseServices.ServerTracer.TraceDebug(0L, "Received a signal to shutdown");
			if (this.connectionPools != null)
			{
				foreach (ConnectionPool connectionPool in this.connectionPools)
				{
					connectionPool.Shutdown();
				}
			}
			this.TerminateAllSessions();
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
			if (this.lightLog != null)
			{
				this.lightLog.Close();
			}
			if (ProtocolBaseServices.LrsLog != null)
			{
				ProtocolBaseServices.LrsLog.Close();
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003384 File Offset: 0x00001584
		internal bool TestPasswordDecoder(int codePage)
		{
			Decoder decoder = null;
			bool result = true;
			try
			{
				decoder = Encoding.GetEncoding(codePage).GetDecoder();
			}
			catch (ArgumentException)
			{
				ProtocolBaseServices.ServerTracer.TraceError<int>(0L, "ArgumentException thrown while testing PasswordCodePage {0}.", codePage);
				ProtocolBaseServices.LogEvent(this.BadPasswordCodePageEventTuple, null, new string[]
				{
					"ArgumentException thrown while testing PasswordCodePage."
				});
				result = false;
			}
			catch (NotSupportedException)
			{
				ProtocolBaseServices.ServerTracer.TraceError<int>(0L, "NotSupportedException thrown while testing PasswordCodePage {0}.", codePage);
				ProtocolBaseServices.LogEvent(this.BadPasswordCodePageEventTuple, null, new string[]
				{
					"NotSupportedException thrown while testing PasswordCodePage."
				});
				result = false;
			}
			if (decoder == null)
			{
				decoder = Encoding.GetEncoding(20127).GetDecoder();
				if (decoder == null)
				{
					ProtocolBaseServices.ServerTracer.TraceError<int>(0L, "No Ascii decoder created while testing PasswordCodePage {0}.", codePage);
					ProtocolBaseServices.LogEvent(this.BadPasswordCodePageEventTuple, null, new string[]
					{
						"No Ascii decoder created while testing PasswordCodePage."
					});
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003474 File Offset: 0x00001674
		internal bool IsOnline()
		{
			if (ProtocolBaseServices.ServerRoleService == ServerServiceRole.mailbox)
			{
				return true;
			}
			if (ExDateTime.UtcNow < ProtocolBaseServices.onlineLastCheckTime.AddSeconds((double)ProtocolBaseServices.onlineCacheThreshold))
			{
				return ProtocolBaseServices.isOnline;
			}
			bool flag = ProtocolBaseServices.isOnline;
			ProtocolBaseServices.isOnline = ServerComponentStateManager.IsOnline(ProtocolBaseServices.TargetProtocol);
			ProtocolBaseServices.onlineLastCheckTime = ExDateTime.UtcNow;
			if (flag != ProtocolBaseServices.isOnline)
			{
				ProtocolBaseServices.LogEvent(this.OnlineValueChangedEventTuple, null, new string[]
				{
					ProtocolBaseServices.isOnline.ToString()
				});
			}
			return ProtocolBaseServices.isOnline;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000034FC File Offset: 0x000016FC
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
					semaphore = null;
				}
				catch (UnauthorizedAccessException)
				{
					semaphore = null;
				}
				if (semaphore == null)
				{
					ProtocolBaseServices.ServerTracer.TraceError<string, string>(0L, "Failed to open the {0} semaphore (name={1}). Exiting.", semaphoreLabel, name);
					Environment.Exit(1);
				}
			}
			return semaphore;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003560 File Offset: 0x00001760
		private static void Usage()
		{
			Console.WriteLine(ProtocolBaseStrings.UsageText, Assembly.GetExecutingAssembly().GetName().Name, ProtocolBaseServices.serviceName);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003580 File Offset: 0x00001780
		private PopImapAdConfiguration GetConfiguration()
		{
			PopImapAdConfiguration result;
			try
			{
				ITopologyConfigurationSession session = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 2108, "GetConfiguration", "f:\\15.00.1497\\sources\\dev\\PopImap\\src\\Core\\ProtocolBaseServices.cs");
				result = this.GetConfiguration(session);
			}
			catch (LocalizedException ex)
			{
				this.ProcessGetConfigurationException(ex);
				result = null;
			}
			return result;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000035D4 File Offset: 0x000017D4
		private void RegisterSpn()
		{
			this.RegisterSpn(ProtocolBaseServices.ServiceName);
			this.RegisterSpn(ProtocolBaseServices.ShortServiceName);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000035EC File Offset: 0x000017EC
		private void RegisterSpn(string name)
		{
			using (WindowsIdentity current = WindowsIdentity.GetCurrent())
			{
				if (!current.User.IsWellKnown(WellKnownSidType.NetworkServiceSid) && !current.User.IsWellKnown(WellKnownSidType.LocalSystemSid))
				{
					return;
				}
			}
			int num = ServicePrincipalName.RegisterServiceClass(name);
			if (num != 0)
			{
				ProtocolBaseServices.ServerTracer.TraceError<int>(0L, "RegisterServiceClass returned {0}", num);
				ProtocolBaseServices.LogEvent(this.SpnRegisterFailureEventTuple, null, new string[]
				{
					num.ToString()
				});
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003678 File Offset: 0x00001878
		private void ConfigureExtendedProtection()
		{
			if (this.configuration.ExtendedProtectionPolicy != ExtendedProtectionTokenCheckingMode.None)
			{
				this.ExtendedProtectionConfig = new ExtendedProtectionConfig((int)this.configuration.ExtendedProtectionPolicy, null, false);
				return;
			}
			this.ExtendedProtectionConfig = ExtendedProtectionConfig.NoExtendedProtection;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000036AB File Offset: 0x000018AB
		private bool LoadLrsPartnerList()
		{
			return true;
		}

		// Token: 0x04000001 RID: 1
		private const string StopKeyOption = "-stopkey:";

		// Token: 0x04000002 RID: 2
		private const string HangKeyOption = "-hangkey:";

		// Token: 0x04000003 RID: 3
		private const string ResetKeyOption = "-resetkey:";

		// Token: 0x04000004 RID: 4
		private const string ReadyKeyOption = "-readykey:";

		// Token: 0x04000005 RID: 5
		private const string ControlPipeOption = "-pipe:";

		// Token: 0x04000006 RID: 6
		private const string HelpOption = "-?";

		// Token: 0x04000007 RID: 7
		private const string WaitToContinue = "-wait";

		// Token: 0x04000008 RID: 8
		private const string ConsoleOption = "-console";

		// Token: 0x04000009 RID: 9
		private const string ServerRoleCafe = "FrontEnd";

		// Token: 0x0400000A RID: 10
		private const string ServerRoleMbx = "ClientAccess";

		// Token: 0x0400000B RID: 11
		private const string DefaultPropertyGroup = "SmtpAddress,TenantName,DisplayName,MailboxServer,Cmd,Parameters,Response,LightLogContext,Exception";

		// Token: 0x0400000C RID: 12
		private const int DefaultMaxIoThreadsValue = 0;

		// Token: 0x0400000D RID: 13
		private const int MaximumMaxIoThreadsValue = 1000;

		// Token: 0x0400000E RID: 14
		private const int MinimumMaxIoThreadsValue = 1;

		// Token: 0x0400000F RID: 15
		private const int TagFaultInjection = 0;

		// Token: 0x04000010 RID: 16
		private const int MaintenanceModeCheckIntervalSeconds = 60;

		// Token: 0x04000011 RID: 17
		private const int DefaultDurationToKeepLogsFor = 7;

		// Token: 0x04000012 RID: 18
		private const string ExchangeSetupLocationKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";

		// Token: 0x04000013 RID: 19
		private const string ExchangeInstallPathValue = "MsiInstallPath";

		// Token: 0x04000014 RID: 20
		private static Guid popImapCoreComponentGuid = new Guid("EFEA6219-825A-4d56-9B26-7393EF24D917");

		// Token: 0x04000015 RID: 21
		private static bool runningFromConsole;

		// Token: 0x04000016 RID: 22
		private static bool isMultiTenancyEnabled;

		// Token: 0x04000017 RID: 23
		private static ExEventLog logger;

		// Token: 0x04000018 RID: 24
		private static string serverName;

		// Token: 0x04000019 RID: 25
		private static string serverVersion;

		// Token: 0x0400001A RID: 26
		private static string serviceName;

		// Token: 0x0400001B RID: 27
		private static Microsoft.Exchange.Diagnostics.Trace serverTracer;

		// Token: 0x0400001C RID: 28
		private static Microsoft.Exchange.Diagnostics.Trace sessionTracer;

		// Token: 0x0400001D RID: 29
		private static FaultInjectionTrace faultInjectionTracer;

		// Token: 0x0400001E RID: 30
		private static TroubleshootingContext troubleshootingContext;

		// Token: 0x0400001F RID: 31
		private static ProtocolBaseServices service;

		// Token: 0x04000020 RID: 32
		private static ServerServiceRole serverRoleService = ServerServiceRole.unknown;

		// Token: 0x04000021 RID: 33
		private static ServerComponentEnum targetProtocol;

		// Token: 0x04000022 RID: 34
		private static bool isOnline = true;

		// Token: 0x04000023 RID: 35
		private static ExDateTime onlineLastCheckTime = ExDateTime.MinValue;

		// Token: 0x04000024 RID: 36
		private static int onlineCacheThreshold;

		// Token: 0x04000025 RID: 37
		private static bool authErrorReportEnabled;

		// Token: 0x04000026 RID: 38
		private Semaphore resetHandle;

		// Token: 0x04000027 RID: 39
		private Semaphore stopHandle;

		// Token: 0x04000028 RID: 40
		private Semaphore hangHandle;

		// Token: 0x04000029 RID: 41
		private Semaphore readyHandle;

		// Token: 0x0400002A RID: 42
		private PopImapAdConfiguration configuration;

		// Token: 0x0400002B RID: 43
		private ControlObject controlObject;

		// Token: 0x0400002C RID: 44
		private bool lightLogEnabled;

		// Token: 0x0400002D RID: 45
		private LightWeightLog lightLog;

		// Token: 0x0400002E RID: 46
		private int maxFailedLoginAttempts;

		// Token: 0x0400002F RID: 47
		private VirtualServer virtualServer;

		// Token: 0x04000030 RID: 48
		private List<ConnectionPool> connectionPools;

		// Token: 0x04000031 RID: 49
		private int passwordCodePage;

		// Token: 0x04000032 RID: 50
		private bool disposed;

		// Token: 0x04000033 RID: 51
		private List<OrganizationId> lrsPartners;
	}
}
