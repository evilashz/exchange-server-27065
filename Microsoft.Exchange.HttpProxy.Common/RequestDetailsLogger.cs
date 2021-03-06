using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.HttpProxy.EventLogs;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000019 RID: 25
	internal class RequestDetailsLogger : RequestDetailsLoggerBase<RequestDetailsLogger>
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004194 File Offset: 0x00002394
		private static string DefaultLogFolderPath
		{
			get
			{
				string result;
				try
				{
					result = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\HttpProxy");
				}
				catch (SetupVersionInformationCorruptException)
				{
					result = "C:\\Program Files\\Microsoft\\Exchange Server\\V15";
				}
				return result;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000041D0 File Offset: 0x000023D0
		public static RoutingType DetectRoutingType(string localMachineForest, Uri targetBEServerUrl)
		{
			if (string.IsNullOrEmpty(localMachineForest))
			{
				throw new ArgumentException("localMachineForest is null or empty:");
			}
			if (targetBEServerUrl == null)
			{
				throw new ArgumentNullException("targetBEServerUrl");
			}
			string text;
			if (localMachineForest.Equals("prod.exchangelabs.com", StringComparison.OrdinalIgnoreCase))
			{
				text = "nam";
			}
			else
			{
				text = localMachineForest.Substring(0, 3);
			}
			string text2 = targetBEServerUrl.Host.ToLower();
			int num = text2.IndexOf('.');
			if (num > -1)
			{
				text2 = text2.Substring(num + 1, text2.Length - num - 1);
			}
			string value;
			if (text2.Equals("prod.exchangelabs.com", StringComparison.OrdinalIgnoreCase))
			{
				value = "nam";
			}
			else
			{
				value = text2.Substring(0, 3);
			}
			if (localMachineForest.Equals(text2, StringComparison.OrdinalIgnoreCase))
			{
				return RoutingType.IntraForest;
			}
			if (text2.Length > 2 && text.Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				return RoutingType.CrossForest;
			}
			return RoutingType.CrossRegion;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004294 File Offset: 0x00002494
		public static void PublishBackendDataToLog(RequestDetailsLogger logger, HttpWebResponse backendRespone)
		{
			if (backendRespone != null)
			{
				foreach (object obj in backendRespone.Headers.Keys)
				{
					string text = (string)obj;
					if (text.StartsWith(Constants.XBackendHeaderPrefix))
					{
						RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendBackEndGenericInfo(logger, text, backendRespone.Headers[text]);
					}
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004310 File Offset: 0x00002510
		public static void LastChanceExceptionHandler(Exception ex)
		{
			RequestDetailsLogger requestDetailsLogger = RequestDetailsLoggerBase<RequestDetailsLogger>.Current;
			if (requestDetailsLogger != null && !requestDetailsLogger.IsDisposed)
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeLogRequestException(requestDetailsLogger, ex, "OnUnhandledException");
				requestDetailsLogger.AsyncCommit(true);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004341 File Offset: 0x00002541
		public void AppendAuthError(string key, string value)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendAuthenticationError(this, key, value);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000434C File Offset: 0x0000254C
		public void LogCurrentTime(string label)
		{
			base.AppendGenericInfo(label, DateTime.UtcNow.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffZ", DateTimeFormatInfo.InvariantInfo));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004378 File Offset: 0x00002578
		public long GetLatency(Enum field, long defaultVal = 0L)
		{
			long result = 0L;
			if (base.TryGetLatency(field, out result))
			{
				return result;
			}
			return defaultVal;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004396 File Offset: 0x00002596
		public void SetRoutingType(Uri targetBEServerUrl)
		{
			this.Set(HttpProxyMetadata.RoutingType, RequestDetailsLogger.DetectRoutingType(HttpProxyGlobals.LocalMachineForest.Member, targetBEServerUrl));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000043BC File Offset: 0x000025BC
		public override bool ShouldSendDebugResponseHeaders()
		{
			string value = null;
			if (this.httpContext != null)
			{
				value = this.httpContext.Request.Headers[Constants.ProbeHeaderName];
			}
			return (!string.IsNullOrEmpty(value) && (HttpProxyGlobals.IsPartnerHostedOnly || VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.DebugResponseHeaders.Enabled)) || RequestDetailsLogger.SendDebugResponseHeaders.Value;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004422 File Offset: 0x00002622
		internal static void FlushQueuedFileWrites()
		{
			RequestDetailsLogger.workerSignal.Set();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004430 File Offset: 0x00002630
		internal void AsyncCommit(bool forceSync)
		{
			if (!base.IsDisposed)
			{
				ServiceCommonMetadataPublisher.PublishMetadata();
				if (string.IsNullOrEmpty(this.Get(ServiceCommonMetadata.AuthenticatedUser)) && string.Compare(this.Get(ServiceCommonMetadata.HttpStatus), "401") == 0 && string.Equals(this.Get(ActivityStandardMetadata.AuthenticationType), "Anonymous", StringComparison.OrdinalIgnoreCase))
				{
					base.ExcludeLogEntry();
				}
				if (!forceSync)
				{
					RequestDetailsLogger.fileIoQueue.Enqueue(this);
					RequestDetailsLogger.workerSignal.Set();
					return;
				}
				this.Dispose();
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000044B4 File Offset: 0x000026B4
		internal void AppendString(Enum key, string valueToAppend)
		{
			string text = this.Get(key) ?? string.Empty;
			text += valueToAppend;
			this.Set(key, text);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000044E4 File Offset: 0x000026E4
		internal void SafeLogUriData(Uri requestUrl)
		{
			if (requestUrl.IsAbsoluteUri)
			{
				this.Set(HttpProxyMetadata.UrlHost, requestUrl.DnsSafeHost);
				this.Set(HttpProxyMetadata.UrlStem, requestUrl.LocalPath);
				this.Set(HttpProxyMetadata.UrlQuery, requestUrl.Query);
				return;
			}
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(this, "InvalidRelativeUri", requestUrl.ToString());
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004547 File Offset: 0x00002747
		protected override bool LogFullException(Exception ex)
		{
			return true;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000454C File Offset: 0x0000274C
		protected override void InitializeLogger()
		{
			ActivityContext.RegisterMetadata(typeof(HttpProxyMetadata));
			ActivityContext.RegisterMetadata(typeof(ServiceCommonMetadata));
			ActivityContext.RegisterMetadata(typeof(ServiceLatencyMetadata));
			ActivityContext.InitialMetadataCapacity = new int?(Enum.GetNames(typeof(ActivityStandardMetadata)).Length + Enum.GetNames(typeof(ActivityReadonlyMetadata)).Length + Enum.GetNames(typeof(HttpProxyMetadata)).Length + Enum.GetNames(typeof(ServiceCommonMetadata)).Length + Enum.GetNames(typeof(ServiceLatencyMetadata)).Length);
			ThreadPool.QueueUserWorkItem(new WaitCallback(RequestDetailsLogger.CommitLogLines));
			base.InitializeLogger();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004604 File Offset: 0x00002804
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
				new KeyValuePair<string, Enum>("Protocol", HttpProxyMetadata.Protocol),
				new KeyValuePair<string, Enum>("UrlHost", HttpProxyMetadata.UrlHost),
				new KeyValuePair<string, Enum>("UrlStem", HttpProxyMetadata.UrlStem),
				new KeyValuePair<string, Enum>("ProtocolAction", HttpProxyMetadata.ProtocolAction),
				new KeyValuePair<string, Enum>("AuthenticationType", ActivityStandardMetadata.AuthenticationType),
				new KeyValuePair<string, Enum>("IsAuthenticated", ServiceCommonMetadata.IsAuthenticated),
				new KeyValuePair<string, Enum>("AuthenticatedUser", ServiceCommonMetadata.AuthenticatedUser),
				new KeyValuePair<string, Enum>("Organization", ActivityStandardMetadata.TenantId),
				new KeyValuePair<string, Enum>("AnchorMailbox", HttpProxyMetadata.AnchorMailbox),
				new KeyValuePair<string, Enum>("UserAgent", ActivityStandardMetadata.ClientInfo),
				new KeyValuePair<string, Enum>("ClientIpAddress", ServiceCommonMetadata.ClientIpAddress),
				new KeyValuePair<string, Enum>("ServerHostName", ServiceCommonMetadata.ServerHostName),
				new KeyValuePair<string, Enum>("HttpStatus", ServiceCommonMetadata.HttpStatus),
				new KeyValuePair<string, Enum>("BackEndStatus", HttpProxyMetadata.BackEndStatus),
				new KeyValuePair<string, Enum>("ErrorCode", ServiceCommonMetadata.ErrorCode),
				new KeyValuePair<string, Enum>("Method", ServiceCommonMetadata.HttpMethod),
				new KeyValuePair<string, Enum>("ProxyAction", HttpProxyMetadata.ProxyAction),
				new KeyValuePair<string, Enum>("TargetServer", HttpProxyMetadata.TargetServer),
				new KeyValuePair<string, Enum>("TargetServerVersion", HttpProxyMetadata.TargetServerVersion),
				new KeyValuePair<string, Enum>("RoutingType", HttpProxyMetadata.RoutingType),
				new KeyValuePair<string, Enum>("RoutingHint", HttpProxyMetadata.RoutingHint),
				new KeyValuePair<string, Enum>("BackEndCookie", HttpProxyMetadata.BackEndCookie),
				new KeyValuePair<string, Enum>("ServerLocatorHost", HttpProxyMetadata.ServerLocatorHost),
				new KeyValuePair<string, Enum>("ServerLocatorLatency", HttpProxyMetadata.ServerLocatorLatency),
				new KeyValuePair<string, Enum>("RequestBytes", ServiceCommonMetadata.RequestSize),
				new KeyValuePair<string, Enum>("ResponseBytes", ServiceCommonMetadata.ResponseSize),
				new KeyValuePair<string, Enum>("TargetOutstandingRequests", HttpProxyMetadata.TargetOutstandingRequests),
				new KeyValuePair<string, Enum>("AuthModulePerfContext", HttpProxyMetadata.AuthModulePerfContext),
				new KeyValuePair<string, Enum>("HttpPipelineLatency", ServiceLatencyMetadata.HttpPipelineLatency),
				new KeyValuePair<string, Enum>("CalculateTargetBackEndLatency", HttpProxyMetadata.CalculateTargetBackendLatency),
				new KeyValuePair<string, Enum>("GlsLatencyBreakup", HttpProxyMetadata.GlsLatencyBreakup),
				new KeyValuePair<string, Enum>("TotalGlsLatency", HttpProxyMetadata.TotalGlsLatency),
				new KeyValuePair<string, Enum>("AccountForestLatencyBreakup", HttpProxyMetadata.AccountForestLatencyBreakup),
				new KeyValuePair<string, Enum>("TotalAccountForestLatency", HttpProxyMetadata.TotalAccountForestLatency),
				new KeyValuePair<string, Enum>("ResourceForestLatencyBreakup", HttpProxyMetadata.ResourceForestLatencyBreakup),
				new KeyValuePair<string, Enum>("TotalResourceForestLatency", HttpProxyMetadata.TotalResourceForestLatency),
				new KeyValuePair<string, Enum>("ADLatency", ServiceLatencyMetadata.CallerADLatency),
				new KeyValuePair<string, Enum>("SharedCacheLatencyBreakup", HttpProxyMetadata.SharedCacheLatencyBreakup),
				new KeyValuePair<string, Enum>("TotalSharedCacheLatency", HttpProxyMetadata.TotalSharedCacheLatency),
				new KeyValuePair<string, Enum>("ActivityContextLifeTime", ActivityReadonlyMetadata.TotalMilliseconds),
				new KeyValuePair<string, Enum>("ModuleToHandlerSwitchingLatency", HttpProxyMetadata.ModuleToHandlerSwitchingLatency),
				new KeyValuePair<string, Enum>("ClientReqStreamLatency", HttpProxyMetadata.ClientRequestStreamingLatency),
				new KeyValuePair<string, Enum>("BackendReqInitLatency", HttpProxyMetadata.BackendRequestInitLatency),
				new KeyValuePair<string, Enum>("BackendReqStreamLatency", HttpProxyMetadata.BackendRequestStreamingLatency),
				new KeyValuePair<string, Enum>("BackendProcessingLatency", HttpProxyMetadata.BackendProcessingLatency),
				new KeyValuePair<string, Enum>("BackendRespInitLatency", HttpProxyMetadata.BackendResponseInitLatency),
				new KeyValuePair<string, Enum>("BackendRespStreamLatency", HttpProxyMetadata.BackendResponseStreamingLatency),
				new KeyValuePair<string, Enum>("ClientRespStreamLatency", HttpProxyMetadata.ClientResponseStreamingLatency),
				new KeyValuePair<string, Enum>("KerberosAuthHeaderLatency", HttpProxyMetadata.KerberosAuthHeaderLatency),
				new KeyValuePair<string, Enum>("HandlerCompletionLatency", HttpProxyMetadata.HandlerCompletionLatency),
				new KeyValuePair<string, Enum>("RequestHandlerLatency", HttpProxyMetadata.RequestHandlerLatency),
				new KeyValuePair<string, Enum>("HandlerToModuleSwitchingLatency", HttpProxyMetadata.HandlerToModuleSwitchingLatency),
				new KeyValuePair<string, Enum>("ProxyTime", HttpProxyMetadata.TotalProxyingLatency),
				new KeyValuePair<string, Enum>("CoreLatency", HttpProxyMetadata.CoreLatency),
				new KeyValuePair<string, Enum>("RoutingLatency", HttpProxyMetadata.RoutingLatency),
				new KeyValuePair<string, Enum>("HttpProxyOverhead", HttpProxyMetadata.HttpProxyOverhead),
				new KeyValuePair<string, Enum>("TotalRequestTime", HttpProxyMetadata.TotalRequestTime),
				new KeyValuePair<string, Enum>("RouteRefresherLatency", HttpProxyMetadata.RouteRefresherLatency),
				new KeyValuePair<string, Enum>("UrlQuery", HttpProxyMetadata.UrlQuery),
				new KeyValuePair<string, Enum>("BackEndGenericInfo", ServiceCommonMetadata.BackEndGenericInfo),
				new KeyValuePair<string, Enum>("GenericInfo", ServiceCommonMetadata.GenericInfo),
				new KeyValuePair<string, Enum>("GenericErrors", ServiceCommonMetadata.GenericErrors)
			};
			string fallbackLogFolderPath = Path.Combine(RequestDetailsLogger.DefaultLogFolderPath, HttpProxyGlobals.ProtocolType.ToString());
			return new RequestLoggerConfig("HttpProxy Logs", "HttpProxy_", "HttpProxyProtocolLogs", "RequestDetailsLogger.HttpProxyProtocolLogFolder", fallbackLogFolderPath, TimeSpan.FromDays((double)RequestDetailsLogger.MaxLogRetentionInDays.Value), (long)(RequestDetailsLogger.MaxLogDirectorySizeInGB.Value * 1034 * 1024 * 1024), (long)(RequestDetailsLogger.MaxLogFileSizeInMB.Value * 1024 * 1024), ServiceCommonMetadata.GenericInfo, columns, Enum.GetValues(typeof(ServiceLatencyMetadata)).Length);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004CC8 File Offset: 0x00002EC8
		protected override string GetDebugHeaderSource()
		{
			return "FE";
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004CD0 File Offset: 0x00002ED0
		private static void CommitLogLines(object state)
		{
			for (;;)
			{
				try
				{
					while (RequestDetailsLogger.fileIoQueue.Count > 0)
					{
						RequestDetailsLogger requestDetailsLogger = RequestDetailsLogger.fileIoQueue.Dequeue() as RequestDetailsLogger;
						if (requestDetailsLogger != null && !requestDetailsLogger.IsDisposed)
						{
							requestDetailsLogger.Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					Diagnostics.ReportException(exception, FrontEndHttpProxyEventLogConstants.Tuple_InternalServerError, null, "Exception from RequestDetailsLogger.CommitLogLines : {0}");
				}
				RequestDetailsLogger.workerSignal.WaitOne();
			}
		}

		// Token: 0x040000B6 RID: 182
		public const string LogType = "HttpProxy Logs";

		// Token: 0x040000B7 RID: 183
		public const string LogFilePrefix = "HttpProxy_";

		// Token: 0x040000B8 RID: 184
		public const string LogComponent = "HttpProxyProtocolLogs";

		// Token: 0x040000B9 RID: 185
		public const string CustomLogFolderPathAppSettingsKey = "RequestDetailsLogger.HttpProxyProtocolLogFolder";

		// Token: 0x040000BA RID: 186
		public static readonly HttpProxyMetadata[] PreservedHttpProxyMetadata = new HttpProxyMetadata[]
		{
			HttpProxyMetadata.AnchorMailbox,
			HttpProxyMetadata.RoutingType,
			HttpProxyMetadata.RoutingHint,
			HttpProxyMetadata.BackEndCookie,
			HttpProxyMetadata.CalculateTargetBackendLatency,
			HttpProxyMetadata.TargetServer,
			HttpProxyMetadata.TargetServerVersion,
			HttpProxyMetadata.BackendProcessingLatency,
			HttpProxyMetadata.ServerLocatorLatency,
			HttpProxyMetadata.ServerLocatorHost,
			HttpProxyMetadata.BackEndStatus
		};

		// Token: 0x040000BB RID: 187
		private static readonly IntAppSettingsEntry MaxLogRetentionInDays = new IntAppSettingsEntry(HttpProxySettings.Prefix("MaxLogRetentionInDays"), 30, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BC RID: 188
		private static readonly IntAppSettingsEntry MaxLogDirectorySizeInGB = new IntAppSettingsEntry(HttpProxySettings.Prefix("MaxLogDirectorySizeInGB"), 1, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BD RID: 189
		private static readonly IntAppSettingsEntry MaxLogFileSizeInMB = new IntAppSettingsEntry(HttpProxySettings.Prefix("MaxLogFileSizeInMB"), 10, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BE RID: 190
		private static readonly BoolAppSettingsEntry SendDebugResponseHeaders = new BoolAppSettingsEntry(HttpProxySettings.Prefix("SendDebugResponseHeaders"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x040000BF RID: 191
		private static Queue fileIoQueue = Queue.Synchronized(new Queue());

		// Token: 0x040000C0 RID: 192
		private static AutoResetEvent workerSignal = new AutoResetEvent(false);
	}
}
