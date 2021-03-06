using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000025 RID: 37
	internal class RequestDetailsLogger : RequestDetailsLoggerBase<RequestDetailsLogger>
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00008E86 File Offset: 0x00007086
		public RequestDetailsLogger() : this(Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\Ews"))
		{
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008E9D File Offset: 0x0000709D
		protected RequestDetailsLogger(string logFolderPath)
		{
			this.logFolderPath = logFolderPath;
			if (RequestDetailsLogger.ApplicationType == LoggerApplicationType.Owa || RequestDetailsLogger.ApplicationType == LoggerApplicationType.Oss)
			{
				base.SkipLogging = true;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008ED0 File Offset: 0x000070D0
		internal new static RequestDetailsLogger Current
		{
			get
			{
				CallContext callContext = CallContext.Current;
				if (callContext != null && callContext.ProtocolLog != null)
				{
					return callContext.ProtocolLog;
				}
				RequestDetailsLogger requestDetailsLogger = RequestDetailsLoggerBase<RequestDetailsLogger>.Current;
				return requestDetailsLogger ?? RequestDetailsLogger.LoggerInstanceForTest;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008F05 File Offset: 0x00007105
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00008F0C File Offset: 0x0000710C
		public static RequestDetailsLogger LoggerInstanceForTest
		{
			get
			{
				return RequestDetailsLogger.loggerInstanceForTest;
			}
			set
			{
				RequestDetailsLogger.loggerInstanceForTest = value;
				RequestDetailsLogger.loggerInstanceForTest.InitializeLogger();
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008F1E File Offset: 0x0000711E
		public static void LogEvent(RequestDetailsLogger logger, Enum eventName)
		{
			if (logger != null)
			{
				logger.LogEvent(eventName);
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008F2C File Offset: 0x0000712C
		public static void LogSubscriptionInfo(string correlationId, string organizationId, string action, IEnumerable<string> subscriptionIds, params string[] details)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in subscriptionIds)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(",");
			}
			RequestDetailsLogger.LogSubscriptionInfo(correlationId, organizationId, action, stringBuilder.ToString(), details);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008F98 File Offset: 0x00007198
		public static void LogSubscriptionInfo(string correlationId, string organizationId, string action, string subscriptionIds, params string[] details)
		{
			if (RequestDetailsLoggerBase<RequestDetailsLogger>.Log == null)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(RequestDetailsLoggerBase<RequestDetailsLogger>.LogSchema);
			if (!RequestDetailsLogger.logKeysInitialized)
			{
				lock (RequestDetailsLogger.logKeysInitLock)
				{
					if (!RequestDetailsLogger.logKeysInitialized)
					{
						RequestDetailsLogger.logKeysInitialized = true;
						if (!RequestDetailsLoggerBase<RequestDetailsLogger>.TryGetLogColumnIndexByEnum(ActivityStandardMetadata.Action, out RequestDetailsLogger.actionIndex) || !RequestDetailsLoggerBase<RequestDetailsLogger>.TryGetLogColumnIndexByEnum(ServiceCommonMetadata.GenericInfo, out RequestDetailsLogger.genericInfoIndex) || !RequestDetailsLoggerBase<RequestDetailsLogger>.TryGetLogColumnIndexByEnum(ActivityReadonlyMetadata.EndTime, out RequestDetailsLogger.timeStampIndex) || !RequestDetailsLoggerBase<RequestDetailsLogger>.TryGetLogColumnIndexByEnum(EwsMetadata.CorrelationGuid, out RequestDetailsLogger.correlationGuidIndex) || !RequestDetailsLoggerBase<RequestDetailsLogger>.TryGetLogColumnIndexByEnum(ActivityStandardMetadata.TenantId, out RequestDetailsLogger.organizationIndex) || !RequestDetailsLoggerBase<RequestDetailsLogger>.TryGetLogColumnIndexByEnum(ServiceCommonMetadata.ServerHostName, out RequestDetailsLogger.serverHostNameIndex))
						{
							RequestDetailsLogger.keysLookupFailed = true;
						}
					}
				}
			}
			if (RequestDetailsLogger.keysLookupFailed)
			{
				return;
			}
			logRowFormatter[RequestDetailsLogger.actionIndex] = "Sbsc_" + action;
			if (correlationId != null)
			{
				logRowFormatter[RequestDetailsLogger.correlationGuidIndex] = correlationId;
			}
			if (organizationId != null)
			{
				logRowFormatter[RequestDetailsLogger.organizationIndex] = organizationId;
			}
			if (RequestDetailsLogger.MachineName != null)
			{
				logRowFormatter[RequestDetailsLogger.serverHostNameIndex] = RequestDetailsLogger.MachineName;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("tid=");
			stringBuilder.Append(Thread.CurrentThread.ManagedThreadId);
			stringBuilder.Append(";");
			if (subscriptionIds != null)
			{
				stringBuilder.Append("ids=");
				stringBuilder.Append(subscriptionIds);
				stringBuilder.Append(";");
			}
			if (details != null)
			{
				stringBuilder.Append("dts=");
				foreach (string value in details)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(",");
				}
			}
			logRowFormatter[RequestDetailsLogger.genericInfoIndex] = stringBuilder.ToString();
			RequestDetailsLoggerBase<RequestDetailsLogger>.Log.Append(logRowFormatter, RequestDetailsLogger.timeStampIndex);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00009178 File Offset: 0x00007378
		public static string FormatSubscriptionLogDetails(string key, object value)
		{
			return string.Format("{0}:{1}", key, value);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009186 File Offset: 0x00007386
		internal void LogEvent(Enum eventName)
		{
			this.Set(eventName, this.stopWatch.ElapsedMilliseconds);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000091E4 File Offset: 0x000073E4
		public static void LogException(Exception exception, string keyPrefix, string eventId)
		{
			RequestDetailsLogger.RunWithSafeLogger(delegate(RequestDetailsLogger logger)
			{
				logger.LogExceptionToGenericError(exception, keyPrefix);
			}, delegate(RequestDetailsLogger newLogger)
			{
				newLogger.Set(ExtensibleLoggerMetadata.EventId, eventId);
				newLogger.LogExceptionToGenericError(exception, keyPrefix);
			});
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009229 File Offset: 0x00007429
		public void AppendAuthError(string key, string value)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendAuthenticationError(this, key, value);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009284 File Offset: 0x00007484
		public void SafeLogWithSeparator(Enum key, string value)
		{
			Action<RequestDetailsLogger> action = delegate(RequestDetailsLogger logger)
			{
				string text = logger.Get(key);
				if (!string.IsNullOrEmpty(text))
				{
					text += "|";
				}
				text += value;
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(logger, key, text);
			};
			RequestDetailsLogger.RunWithSafeLogger(action, action);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000092BC File Offset: 0x000074BC
		public override bool ShouldSendDebugResponseHeaders()
		{
			string value = this.httpContext.Request.Headers[WellKnownHeader.Probe];
			return (!string.IsNullOrEmpty(value) && (EWSSettings.IsPartnerHostedOnly || VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.DebugResponseHeaders.Enabled)) || Global.SendDebugResponseHeaders;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009313 File Offset: 0x00007513
		internal void AppendClientStatistic(string key, string value)
		{
			base.Append(EwsMetadata.ClientStatistics, key, value);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00009324 File Offset: 0x00007524
		protected override void SetPerLogLineSizeLimit()
		{
			if (RequestDetailsLogger.ApplicationType == LoggerApplicationType.Owa)
			{
				ActivityScopeImpl.MaxAppendableColumnLength = null;
				RequestDetailsLoggerBase<RequestDetailsLogger>.ErrorMessageLengthThreshold = null;
				RequestDetailsLoggerBase<RequestDetailsLogger>.ProcessExceptionMessage = false;
				base.SkipLogging = true;
				return;
			}
			base.SetPerLogLineSizeLimit();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000936C File Offset: 0x0000756C
		protected override void InitializeLogger()
		{
			ActivityContext.RegisterMetadata(typeof(ServiceCommonMetadata));
			ActivityContext.RegisterMetadata(typeof(ServiceLatencyMetadata));
			ActivityContext.RegisterMetadata(typeof(BudgetMetadata));
			ActivityContext.RegisterMetadata(typeof(EwsMetadata));
			ThreadPool.QueueUserWorkItem(new WaitCallback(RequestDetailsLogger.CommitLogLines));
			this.stopWatch = Stopwatch.StartNew();
			base.InitializeLogger();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000093D8 File Offset: 0x000075D8
		protected override RequestLoggerConfig GetRequestLoggerConfig()
		{
			List<KeyValuePair<string, Enum>> columns = new List<KeyValuePair<string, Enum>>
			{
				new KeyValuePair<string, Enum>("DateTime", ActivityReadonlyMetadata.EndTime),
				new KeyValuePair<string, Enum>("RequestId", ActivityReadonlyMetadata.ActivityId),
				new KeyValuePair<string, Enum>("MajorVersion", ServiceCommonMetadata.ServerVersionMajor),
				new KeyValuePair<string, Enum>("MinorVersion", ServiceCommonMetadata.ServerVersionMinor),
				new KeyValuePair<string, Enum>("BuildVersion", ServiceCommonMetadata.ServerVersionBuild),
				new KeyValuePair<string, Enum>("RevisionVersion", ServiceCommonMetadata.ServerVersionRevision),
				new KeyValuePair<string, Enum>("ClientRequestId", ActivityStandardMetadata.ClientRequestId),
				new KeyValuePair<string, Enum>("AuthenticationType", ActivityStandardMetadata.AuthenticationType),
				new KeyValuePair<string, Enum>("IsAuthenticated", ServiceCommonMetadata.IsAuthenticated),
				new KeyValuePair<string, Enum>("AuthenticatedUser", ServiceCommonMetadata.AuthenticatedUser),
				new KeyValuePair<string, Enum>("Organization", ActivityStandardMetadata.TenantId),
				new KeyValuePair<string, Enum>("UserAgent", ActivityStandardMetadata.ClientInfo),
				new KeyValuePair<string, Enum>("VersionInfo", EwsMetadata.VersionInfo),
				new KeyValuePair<string, Enum>("ClientIpAddress", ServiceCommonMetadata.ClientIpAddress),
				new KeyValuePair<string, Enum>("ServerHostName", ServiceCommonMetadata.ServerHostName),
				new KeyValuePair<string, Enum>("FrontEndServer", EwsMetadata.FrontEndServer),
				new KeyValuePair<string, Enum>("SoapAction", ActivityStandardMetadata.Action),
				new KeyValuePair<string, Enum>("HttpStatus", ServiceCommonMetadata.HttpStatus),
				new KeyValuePair<string, Enum>("RequestSize", ServiceCommonMetadata.RequestSize),
				new KeyValuePair<string, Enum>("ResponseSize", ServiceCommonMetadata.ResponseSize),
				new KeyValuePair<string, Enum>("ErrorCode", ServiceCommonMetadata.ErrorCode),
				new KeyValuePair<string, Enum>("ImpersonatedUser", ActivityStandardMetadata.UserEmail),
				new KeyValuePair<string, Enum>("ProxyAsUser", EwsMetadata.ProxyAsUser),
				new KeyValuePair<string, Enum>("ActAsUser", EwsMetadata.ActAsUser),
				new KeyValuePair<string, Enum>("Cookie", ServiceCommonMetadata.Cookie),
				new KeyValuePair<string, Enum>("CorrelationGuid", EwsMetadata.CorrelationGuid),
				new KeyValuePair<string, Enum>("PrimaryOrProxyServer", EwsMetadata.PrimaryOrProxyServer),
				new KeyValuePair<string, Enum>("TaskType", EwsMetadata.TaskType),
				new KeyValuePair<string, Enum>("RemoteBackendCount", EwsMetadata.RemoteBackendCount),
				new KeyValuePair<string, Enum>("LocalMailboxCount", EwsMetadata.LocalMailboxCount),
				new KeyValuePair<string, Enum>("RemoteMailboxCount", EwsMetadata.RemoteMailboxCount),
				new KeyValuePair<string, Enum>("LocalIdCount", EwsMetadata.LocalIdCount),
				new KeyValuePair<string, Enum>("RemoteIdCount", EwsMetadata.RemoteIdCount),
				new KeyValuePair<string, Enum>("BeginBudgetConnections", BudgetMetadata.BeginBudgetConnections),
				new KeyValuePair<string, Enum>("EndBudgetConnections", BudgetMetadata.EndBudgetConnections),
				new KeyValuePair<string, Enum>("BeginBudgetHangingConnections", BudgetMetadata.BeginBudgetHangingConnections),
				new KeyValuePair<string, Enum>("EndBudgetHangingConnections", BudgetMetadata.EndBudgetHangingConnections),
				new KeyValuePair<string, Enum>("BeginBudgetAD", BudgetMetadata.BeginBudgetAD),
				new KeyValuePair<string, Enum>("EndBudgetAD", BudgetMetadata.EndBudgetAD),
				new KeyValuePair<string, Enum>("BeginBudgetCAS", BudgetMetadata.BeginBudgetCAS),
				new KeyValuePair<string, Enum>("EndBudgetCAS", BudgetMetadata.EndBudgetCAS),
				new KeyValuePair<string, Enum>("BeginBudgetRPC", BudgetMetadata.BeginBudgetRPC),
				new KeyValuePair<string, Enum>("EndBudgetRPC", BudgetMetadata.EndBudgetRPC),
				new KeyValuePair<string, Enum>("BeginBudgetFindCount", BudgetMetadata.BeginBudgetFindCount),
				new KeyValuePair<string, Enum>("EndBudgetFindCount", BudgetMetadata.EndBudgetFindCount),
				new KeyValuePair<string, Enum>("BeginBudgetSubscriptions", BudgetMetadata.BeginBudgetSubscriptions),
				new KeyValuePair<string, Enum>("EndBudgetSubscriptions", BudgetMetadata.EndBudgetSubscriptions),
				new KeyValuePair<string, Enum>("MDBResource", BudgetMetadata.MDBResource),
				new KeyValuePair<string, Enum>("MDBHealth", BudgetMetadata.MDBHealth),
				new KeyValuePair<string, Enum>("MDBHistoricalLoad", BudgetMetadata.MDBHistoricalLoad),
				new KeyValuePair<string, Enum>("ThrottlingPolicy", BudgetMetadata.ThrottlingPolicy),
				new KeyValuePair<string, Enum>("ThrottlingDelay", BudgetMetadata.ThrottlingDelay),
				new KeyValuePair<string, Enum>("ThrottlingRequestType", BudgetMetadata.ThrottlingRequestType),
				new KeyValuePair<string, Enum>("TotalDCRequestCount", BudgetMetadata.TotalDCRequestCount),
				new KeyValuePair<string, Enum>("TotalDCRequestLatency", BudgetMetadata.TotalDCRequestLatency),
				new KeyValuePair<string, Enum>("TotalMBXRequestCount", BudgetMetadata.TotalMBXRequestCount),
				new KeyValuePair<string, Enum>("TotalMBXRequestLatency", BudgetMetadata.TotalMBXRequestLatency),
				new KeyValuePair<string, Enum>("RecipientLookupLatency", ServiceLatencyMetadata.RecipientLookupLatency),
				new KeyValuePair<string, Enum>("ExchangePrincipalLatency", ServiceLatencyMetadata.ExchangePrincipalLatency),
				new KeyValuePair<string, Enum>("HttpPipelineLatency", ServiceLatencyMetadata.HttpPipelineLatency),
				new KeyValuePair<string, Enum>("CheckAccessCoreLatency", ServiceLatencyMetadata.CheckAccessCoreLatency),
				new KeyValuePair<string, Enum>("AuthModuleLatency", ServiceLatencyMetadata.AuthModuleLatency),
				new KeyValuePair<string, Enum>("CallContextInitLatency", ServiceLatencyMetadata.CallContextInitLatency),
				new KeyValuePair<string, Enum>("PreExecutionLatency", ServiceLatencyMetadata.PreExecutionLatency),
				new KeyValuePair<string, Enum>("CoreExecutionLatency", ServiceLatencyMetadata.CoreExecutionLatency),
				new KeyValuePair<string, Enum>("TotalRequestTime", ActivityReadonlyMetadata.TotalMilliseconds),
				new KeyValuePair<string, Enum>("DetailedExchangePrincipalLatency", ServiceLatencyMetadata.DetailedExchangePrincipalLatency),
				new KeyValuePair<string, Enum>("ClientStatistics", EwsMetadata.ClientStatistics),
				new KeyValuePair<string, Enum>("GenericInfo", ServiceCommonMetadata.GenericInfo),
				new KeyValuePair<string, Enum>("AuthenticationErrors", ServiceCommonMetadata.AuthenticationErrors),
				new KeyValuePair<string, Enum>("GenericErrors", ServiceCommonMetadata.GenericErrors),
				new KeyValuePair<string, Enum>("Puid", ActivityStandardMetadata.Puid)
			};
			return new RequestLoggerConfig("EWS Logs", "Ews_", "EWSProtocolLogs", "RequestDetailsLogger.EWSProtocolLogFolder", this.logFolderPath, TimeSpan.FromDays((double)RequestDetailsLogger.MaxLogRetentionInDays.Value), (long)RequestDetailsLogger.MaxLogDirectorySizeInGB.Value * 1024L * 1024L * 1024L, (long)RequestDetailsLogger.MaxLogFileSizeInMB.Value * 1024L * 1024L, ServiceCommonMetadata.GenericInfo, columns, Enum.GetValues(typeof(ServiceLatencyMetadata)).Length);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009AC0 File Offset: 0x00007CC0
		public override void Commit()
		{
			BudgetMetadataPublisher.PublishMetadata();
			if (RequestDetailsLogger.ApplicationType == LoggerApplicationType.Owa)
			{
				base.Commit();
				return;
			}
			this.AsyncCommit();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009ADC File Offset: 0x00007CDC
		private void AsyncCommit()
		{
			if (!base.IsDisposed)
			{
				ServiceCommonMetadataPublisher.PublishMetadata();
				if (string.IsNullOrEmpty(this.Get(ServiceCommonMetadata.AuthenticatedUser)) && string.Equals("401", this.Get(ServiceCommonMetadata.HttpStatus), StringComparison.OrdinalIgnoreCase) && string.Equals(this.Get(ActivityStandardMetadata.AuthenticationType), "Anonymous", StringComparison.OrdinalIgnoreCase))
				{
					base.ExcludeLogEntry();
				}
				else
				{
					string value = this.Get(ServiceCommonMetadata.LiveIdBasicLog);
					string value2 = this.Get(ServiceCommonMetadata.LiveIdBasicError);
					string value3 = this.Get(ServiceCommonMetadata.LiveIdNegotiateError);
					if (!string.IsNullOrEmpty(value))
					{
						this.AppendAuthError("LiveIdBasicLog", value);
					}
					if (!string.IsNullOrEmpty(value2))
					{
						this.AppendAuthError("LiveIdBasicError", value2);
					}
					if (!string.IsNullOrEmpty(value3))
					{
						this.AppendAuthError("LiveIdNegotiateError", value3);
					}
					base.AppendGenericInfo("MailboxTypeCacheSize", MailboxTypeCache.Instance.CacheSize);
				}
				RequestDetailsLogger.fileIoQueue.Enqueue(this);
				RequestDetailsLogger.workerSignal.Set();
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009BDF File Offset: 0x00007DDF
		protected override bool IsIgnorableException(Exception ex)
		{
			return ex != null && ex is BailOutException;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00009BF0 File Offset: 0x00007DF0
		protected override bool LogFullException(Exception ex)
		{
			bool result = true;
			LocalizedException ex2 = ex as LocalizedException;
			if (ex2 != null)
			{
				result = this.IsAmbiguousError((ResponseCodeType)ex2.ErrorCode);
			}
			return result;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00009C18 File Offset: 0x00007E18
		private bool IsAmbiguousError(ResponseCodeType errorCode)
		{
			if (errorCode <= ResponseCodeType.ErrorInvalidOperation)
			{
				if (errorCode <= ResponseCodeType.ErrorCorruptData)
				{
					if (errorCode != ResponseCodeType.ErrorADOperation && errorCode != ResponseCodeType.ErrorCorruptData)
					{
						goto IL_74;
					}
				}
				else
				{
					switch (errorCode)
					{
					case ResponseCodeType.ErrorInsufficientResources:
					case ResponseCodeType.ErrorInternalServerError:
					case ResponseCodeType.ErrorInternalServerTransientError:
						break;
					default:
						if (errorCode != ResponseCodeType.ErrorInvalidOperation)
						{
							goto IL_74;
						}
						break;
					}
				}
			}
			else if (errorCode <= ResponseCodeType.ErrorMailboxStoreUnavailable)
			{
				if (errorCode != ResponseCodeType.ErrorInvalidPropertyRequest)
				{
					switch (errorCode)
					{
					case ResponseCodeType.ErrorMailboxMoveInProgress:
					case ResponseCodeType.ErrorMailboxStoreUnavailable:
						break;
					default:
						goto IL_74;
					}
				}
			}
			else if (errorCode != ResponseCodeType.ErrorServerBusy && errorCode != ResponseCodeType.ErrorExchangeConfigurationException)
			{
				goto IL_74;
			}
			return true;
			IL_74:
			return !Enum.IsDefined(typeof(ResponseCodeType), errorCode) || Global.WriteStackTraceToProtocolLogForErrorTypes.Contains(errorCode.ToString());
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009CC7 File Offset: 0x00007EC7
		internal static void FlushQueuedFileWrites()
		{
			RequestDetailsLogger.workerSignal.Set();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009CD4 File Offset: 0x00007ED4
		private static void CommitLogLines(object state)
		{
			for (;;)
			{
				if (RequestDetailsLogger.fileIoQueue.Count <= 0)
				{
					RequestDetailsLogger.workerSignal.WaitOne();
				}
				else
				{
					RequestDetailsLogger requestDetailsLogger = RequestDetailsLogger.fileIoQueue.Dequeue() as RequestDetailsLogger;
					if (requestDetailsLogger != null && !requestDetailsLogger.IsDisposed)
					{
						try
						{
							requestDetailsLogger.Dispose();
						}
						catch (Exception exception)
						{
							ExceptionHandlerBase.ReportException(requestDetailsLogger, exception);
						}
					}
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009D3C File Offset: 0x00007F3C
		private static void RunWithSafeLogger(Action<RequestDetailsLogger> runWithLogger, Action<RequestDetailsLogger> runWithTemporaryLogger)
		{
			RequestDetailsLogger requestDetailsLogger = RequestDetailsLogger.Current;
			if (requestDetailsLogger == null || requestDetailsLogger.IsDisposed)
			{
				using (RequestDetailsLogger requestDetailsLogger2 = RequestDetailsLoggerBase<RequestDetailsLogger>.InitializeRequestLogger())
				{
					runWithTemporaryLogger(requestDetailsLogger2);
					return;
				}
			}
			runWithLogger(requestDetailsLogger);
		}

		// Token: 0x040001A4 RID: 420
		private const string LogType = "EWS Logs";

		// Token: 0x040001A5 RID: 421
		private const string LogFilePrefix = "Ews_";

		// Token: 0x040001A6 RID: 422
		private const string LogComponent = "EWSProtocolLogs";

		// Token: 0x040001A7 RID: 423
		private const string CustomLogFolderPathAppSettingsKey = "RequestDetailsLogger.EWSProtocolLogFolder";

		// Token: 0x040001A8 RID: 424
		public static LoggerApplicationType ApplicationType = LoggerApplicationType.Ews;

		// Token: 0x040001A9 RID: 425
		private static Queue fileIoQueue = Queue.Synchronized(new Queue());

		// Token: 0x040001AA RID: 426
		private static AutoResetEvent workerSignal = new AutoResetEvent(false);

		// Token: 0x040001AB RID: 427
		private static readonly IntAppSettingsEntry MaxLogRetentionInDays = new IntAppSettingsEntry("MaxLogRetentionInDays", 30, ExTraceGlobals.CommonAlgorithmTracer);

		// Token: 0x040001AC RID: 428
		private static readonly IntAppSettingsEntry MaxLogDirectorySizeInGB = new IntAppSettingsEntry("MaxLogDirectorySizeInGB", 5, ExTraceGlobals.CommonAlgorithmTracer);

		// Token: 0x040001AD RID: 429
		private static readonly IntAppSettingsEntry MaxLogFileSizeInMB = new IntAppSettingsEntry("MaxLogFileSizeInMB", 10, ExTraceGlobals.CommonAlgorithmTracer);

		// Token: 0x040001AE RID: 430
		private static readonly string MachineName = Environment.MachineName;

		// Token: 0x040001AF RID: 431
		private readonly string logFolderPath;

		// Token: 0x040001B0 RID: 432
		private static RequestDetailsLogger loggerInstanceForTest;

		// Token: 0x040001B1 RID: 433
		private static readonly object logKeysInitLock = new object();

		// Token: 0x040001B2 RID: 434
		private static bool keysLookupFailed;

		// Token: 0x040001B3 RID: 435
		private static bool logKeysInitialized;

		// Token: 0x040001B4 RID: 436
		private static int actionIndex;

		// Token: 0x040001B5 RID: 437
		private static int genericInfoIndex;

		// Token: 0x040001B6 RID: 438
		private static int timeStampIndex;

		// Token: 0x040001B7 RID: 439
		private static int correlationGuidIndex;

		// Token: 0x040001B8 RID: 440
		private static int organizationIndex;

		// Token: 0x040001B9 RID: 441
		private static int serverHostNameIndex;

		// Token: 0x040001BA RID: 442
		private Stopwatch stopWatch = Stopwatch.StartNew();
	}
}
