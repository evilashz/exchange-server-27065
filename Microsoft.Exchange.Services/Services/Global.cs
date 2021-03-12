using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.EventLogs;
using Microsoft.Exchange.Services.OData;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.UM.ClientAccess;
using Microsoft.Exchange.UM.UMCommon.MessageContent;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services
{
	// Token: 0x02000015 RID: 21
	public class Global : HttpApplication
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004390 File Offset: 0x00002590
		public static IUnifiedContactStoreConfiguration UnifiedContactStoreConfiguration
		{
			get
			{
				return Global.unifiedContactStoreConfiguration;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004397 File Offset: 0x00002597
		public static int FindCountLimit
		{
			get
			{
				return Global.findCountLimit;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000439E File Offset: 0x0000259E
		public static int HangingConnectionLimit
		{
			get
			{
				return Global.hangingConnectionLimit;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000043A5 File Offset: 0x000025A5
		public static bool? EnableSchemaValidationOverride
		{
			get
			{
				return Global.enableSchemaValidationOverride;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000043AC File Offset: 0x000025AC
		public static bool UseGcCollect
		{
			get
			{
				return Global.useGcCollect;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000043B3 File Offset: 0x000025B3
		public static int CreateItemRequestSizeThreshold
		{
			get
			{
				return Global.createItemRequestSizeThreshold;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000043BA File Offset: 0x000025BA
		public static int CollectIntervalInMilliseconds
		{
			get
			{
				return Global.collectIntervalInMilliseconds;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000043C1 File Offset: 0x000025C1
		public static int PrivateWorkingSetThreshold
		{
			get
			{
				return Global.privateWorkingSetThreshold;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000043C8 File Offset: 0x000025C8
		public static int AccessingPrincipalCacheSize
		{
			get
			{
				return Global.accessingPrincipalCacheSize;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000043CF File Offset: 0x000025CF
		public static bool AllowCommandOptimization(string name)
		{
			return !Global.disableAllCommandOptimizations && !Global.disableCommandOptimizationSet.Contains(name.ToLowerInvariant());
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000043ED File Offset: 0x000025ED
		public static int MaxAttachmentNestLevel
		{
			get
			{
				return Global.maxAttachmentNestLevel;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000043F4 File Offset: 0x000025F4
		public static bool EncodeStringProperties
		{
			get
			{
				return Global.GetAppSettingAsBool("EncodeStringProperties", false);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004404 File Offset: 0x00002604
		public static int SearchTimeoutInMilliseconds
		{
			get
			{
				int result = Global.searchTimeoutInMilliseconds;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(3200658749U, ref result);
				return result;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004429 File Offset: 0x00002629
		public static int MaximumTemporaryFilteredViewPerUser
		{
			get
			{
				return Global.maximumTemporaryFilteredViewPerUser;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004430 File Offset: 0x00002630
		internal static int OrganizationWideAccessPolicyTimeoutInSeconds
		{
			get
			{
				return Global.organizationWideAccessPolicyTimeoutInSeconds;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004437 File Offset: 0x00002637
		internal static int ExchangePrincipalCacheTimeoutInMinutes
		{
			get
			{
				return Global.exchangePrincipalCacheTimeoutInMinutes;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000443E File Offset: 0x0000263E
		internal static int ExchangePrincipalCacheTimeoutInSecondsOnError
		{
			get
			{
				return Global.exchangePrincipalCacheTimeoutInSecondsOnError;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004445 File Offset: 0x00002645
		public static bool WriteStackTraceOnISE
		{
			get
			{
				return Global.writeStackTraceOnISE;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000444C File Offset: 0x0000264C
		public static string JunkMailReportingAddress
		{
			get
			{
				return Global.junkMailReportingAddress;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004453 File Offset: 0x00002653
		public static string NotJunkMailReportingAddress
		{
			get
			{
				return Global.notJunkMailReportingAddress;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000445A File Offset: 0x0000265A
		public static bool SendXBEServerExceptionHeaderToCafe
		{
			get
			{
				return Global.sendXBEServerExceptionHeaderToCafe;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004461 File Offset: 0x00002661
		public static bool EnableMailboxLogger
		{
			get
			{
				return Global.enableMailboxLogger;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00004468 File Offset: 0x00002668
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000446F File Offset: 0x0000266F
		public static bool SendXBEServerExceptionHeaderToCafeOnFailover { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00004477 File Offset: 0x00002677
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x0000447E File Offset: 0x0000267E
		public static bool FastParticipantResolveEnabled { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004486 File Offset: 0x00002686
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x0000448D File Offset: 0x0000268D
		public static bool EnableFaultInjection { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004495 File Offset: 0x00002695
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x0000449C File Offset: 0x0000269C
		public static List<Dictionary<string, string>> FaultsList { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000044A4 File Offset: 0x000026A4
		public static HashSet<string> WriteStackTraceToProtocolLogForErrorTypes
		{
			get
			{
				return Global.writeStackTraceToProtocolLogForErrorTypes;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x000044AB File Offset: 0x000026AB
		public static bool WriteThrottlingDiagnostics
		{
			get
			{
				return Global.writeThrottlingDiagnostics;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CA RID: 202 RVA: 0x000044B4 File Offset: 0x000026B4
		public static int GetAttachmentSizeLimit
		{
			get
			{
				int num = Global.MaxMaxRequestSizeForEWS.Member;
				if (num == 0)
				{
					num = Global.getAttachmentSizeLimit;
				}
				int num2 = 0;
				ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(2659593533U, ref num2);
				if (num2 != 0)
				{
					num = num2;
				}
				return num;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000044EE File Offset: 0x000026EE
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000044F5 File Offset: 0x000026F5
		internal static IResponseShapeResolver ResponseShapeResolver
		{
			get
			{
				return Global.responseShapeResolver;
			}
			set
			{
				Global.responseShapeResolver = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000044FD File Offset: 0x000026FD
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004504 File Offset: 0x00002704
		internal static EwsClientMailboxSessionCloningHandler EwsClientMailboxSessionCloningHandler
		{
			get
			{
				return Global.ewsClientMailboxSessionCloningHandler;
			}
			set
			{
				Global.ewsClientMailboxSessionCloningHandler = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000450C File Offset: 0x0000270C
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004513 File Offset: 0x00002713
		internal static string DefaultMapiClientType
		{
			get
			{
				return Global.defaultMapiClientType;
			}
			set
			{
				Global.defaultMapiClientType = value;
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "Default MAPI client type set to {0}", Global.defaultMapiClientType);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004531 File Offset: 0x00002731
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004538 File Offset: 0x00002738
		internal static bool SafeHtmlLoaded
		{
			get
			{
				return Global.safeHtmlLoaded;
			}
			set
			{
				Global.safeHtmlLoaded = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004540 File Offset: 0x00002740
		internal static bool ODataStackTraceInErrorResponse
		{
			get
			{
				return false || Global.oDataStackTraceInErrorResponse;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000455C File Offset: 0x0000275C
		internal static int GetAppSettingAsInt(string key, int defaultValue)
		{
			string s = ConfigurationManager.AppSettings[key];
			int result;
			if (int.TryParse(s, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004584 File Offset: 0x00002784
		internal static bool GetAppSettingAsBool(string key, bool defaultValue)
		{
			string value = ConfigurationManager.AppSettings[key];
			bool result;
			if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000045B2 File Offset: 0x000027B2
		private static MailboxSession DefaultEwsClientMailboxSessionCloningHandler(Guid mailboxGuid, CultureInfo userCulture, LogonType logonType, string userContextKey, ExchangePrincipal mailboxToAccess, IADOrgPerson person, ClientSecurityContext callerSecurityContext, GenericIdentity genericIdentity, bool unifiedLogon)
		{
			return null;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000045B8 File Offset: 0x000027B8
		private static void InitializeThrottlingPerfCounters()
		{
			int appSettingAsInt = Global.GetAppSettingAsInt("MassUserOverBudgetPercent", 0);
			if (appSettingAsInt <= 0 || appSettingAsInt > 100)
			{
				ExTraceGlobals.ThrottlingTracer.TraceError<int>(0L, "[Global::InitializeThrottlingPerfCounters] Invalid MassUserOverBudgetPercent value in web.config: {0}.  Must be >0 and <=100.", appSettingAsInt);
			}
			ThrottlingPerfCounterWrapper.Initialize(BudgetType.Ews, (appSettingAsInt > 0 && appSettingAsInt <= 100) ? new int?(appSettingAsInt) : null);
			ResourceHealthMonitorManager.Initialize(ResourceHealthComponent.EWS);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004612 File Offset: 0x00002812
		private static void InitializeProxyTimeout()
		{
			Global.proxyTimeout = Global.GetAppSettingAsInt("ProxyTimeout", Global.defaultProxyTimeout);
			if (Global.proxyTimeout < Global.minimumProxyTimeout)
			{
				Global.proxyTimeout = Global.minimumProxyTimeout;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000463E File Offset: 0x0000283E
		private static void InitializeSearchTimeout()
		{
			Global.searchTimeoutInMilliseconds = Global.GetAppSettingAsInt("SearchTimeoutInMilliseconds", 60000);
			if (Global.searchTimeoutInMilliseconds < Global.minimumSearchTimeout && Global.searchTimeoutInMilliseconds != -1)
			{
				Global.searchTimeoutInMilliseconds = Global.minimumSearchTimeout;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004674 File Offset: 0x00002874
		private static void InitializeMaximumTemporaryFilteredViewPerUser()
		{
			int num = Global.GetAppSettingAsInt("MaximumTemporaryFilteredViewPerUser", 20);
			if (num < Global.minimumTemporaryFilteredViewPerUser)
			{
				num = Global.minimumTemporaryFilteredViewPerUser;
			}
			else if (num > 20)
			{
				num = 20;
			}
			Global.maximumTemporaryFilteredViewPerUser = num;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000046AC File Offset: 0x000028AC
		private static void InitializeEventQueuePollingInterval()
		{
			int num = Global.GetAppSettingAsInt("EventQueuePollingInterval", 5);
			if (num < 1)
			{
				ExTraceGlobals.ThrottlingTracer.TraceError<int, int>(0L, "[Global::InitializeEventQueuePollingInterval] Invalid EventQueuePollingInterval value in web.config: {0}.  Must be >= {1}.", num, 1);
				num = 1;
			}
			else if (num > 60)
			{
				ExTraceGlobals.ThrottlingTracer.TraceError<int, int>(0L, "[Global::InitializeEventQueuePollingInterval] Invalid EventQueuePollingInterval value in web.config: {0}.  Must be <= {1}.", num, 60);
				num = 60;
			}
			Global.eventQueuePollingInterval = num;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004702 File Offset: 0x00002902
		private static void InitializeAccessPolicyTimeout()
		{
			Global.organizationWideAccessPolicyTimeoutInSeconds = Global.GetAppSettingAsInt("OrganizationWideAccessPolicyTimeoutInSeconds", 10800);
			if (Global.organizationWideAccessPolicyTimeoutInSeconds < 0)
			{
				Global.organizationWideAccessPolicyTimeoutInSeconds = 0;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004728 File Offset: 0x00002928
		private static UserWorkloadManager CreateWorkloadManager()
		{
			int workloadManagerMaxThreadCount = Global.GetWorkloadManagerMaxThreadCount();
			int workloadManagerMaxTasksQueued = Global.GetWorkloadManagerMaxTasksQueued();
			int appSettingAsInt = Global.GetAppSettingAsInt("DelayTimeThreshold", 0);
			if (appSettingAsInt <= 0)
			{
				ExTraceGlobals.ThrottlingTracer.TraceError<int>(0L, "[Global::InitializeThrottlingPerfCounters] Invalid DelayTimeThreshold value in web.config :{0}.  Must be > 0.", appSettingAsInt);
			}
			UserWorkloadManager.Initialize(workloadManagerMaxThreadCount, workloadManagerMaxTasksQueued, workloadManagerMaxTasksQueued, TimeSpan.FromMinutes(5.0), (appSettingAsInt > 0) ? new int?(appSettingAsInt) : null);
			return UserWorkloadManager.Singleton;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004794 File Offset: 0x00002994
		private static int GetWorkloadManagerMaxThreadCount()
		{
			int appSettingAsInt = Global.GetAppSettingAsInt("MaxWorkerThreadsPerProcessor", Global.defaultMaxWorkerThreadsPerProcessor);
			int processorCount = Environment.ProcessorCount;
			if (appSettingAsInt < Global.minimumMaxWorkerThreadsPerProcessor)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<int, int>(0L, "[Global.GetWorkloadManagerMaxThreadCount Invalid MaxWorkerThreadsPerProcessor value in web.config: {0}.  Must be >= {1}.", appSettingAsInt, Global.minimumMaxWorkerThreadsPerProcessor);
				appSettingAsInt = Global.minimumMaxWorkerThreadsPerProcessor;
			}
			int num = appSettingAsInt * processorCount;
			int num2 = UserWorkloadManager.GetAvailableThreads() / 2;
			int num3 = Global.defaultMaxWorkerThreadsPerProcessor * processorCount;
			int num4 = Math.Max(num2, num3);
			if (num > num4)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<int, int, int>(0L, "Global.GetWorkloadManagerMaxThreadCount Invalid MaxWorkerThreadsPerProcessor value in web.config: {0}. Product of maximum worker threads and processor count ({1}) must be <= available threads ceiling ({2}).", appSettingAsInt, processorCount, num4);
				num = num4;
			}
			ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "maxThreadCount: {0}. availableThreads: {1}. defaultMaxThreadCount: {2}. processor count: {3}.", new object[]
			{
				num,
				num2,
				num3,
				processorCount
			});
			return num;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004860 File Offset: 0x00002A60
		private static int GetWorkloadManagerMaxTasksQueued()
		{
			int appSettingAsInt = Global.GetAppSettingAsInt("MaxTasksQueued", Global.defaultMaxTasksQueued);
			if (appSettingAsInt < Global.minimumMaxTasksQueued)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<int, int>(0L, "Global.GetWorkloadManagerMaxTasksQueued Invalid MaxTasksQueued value in web.config: {0}.  Must be >= {1}.", appSettingAsInt, Global.minimumMaxTasksQueued);
				appSettingAsInt = Global.minimumMaxTasksQueued;
			}
			else if (appSettingAsInt > Global.maximumMaxTasksQueued)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<int, int>(0L, "Global.GetWorkloadManagerMaxTasksQueued Invalid MaxTasksQueued value in web.config: {0}.  Must be <= {1}.", appSettingAsInt, Global.maximumMaxTasksQueued);
				appSettingAsInt = Global.maximumMaxTasksQueued;
			}
			return appSettingAsInt;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000048CC File Offset: 0x00002ACC
		private void Application_BeginRequest(object sender, EventArgs e)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.InitializeRequestLogger();
			RequestDetailsLogger.Current.ActivityScope.UpdateFromMessage(HttpContext.Current.Request);
			RequestDetailsLogger.Current.ActivityScope.SerializeTo(HttpContext.Current.Response);
			NameValueCollection headers = HttpContext.Current.Request.Headers;
			if (headers != null)
			{
				string text = headers.Get("X-EWS-TargetVersion");
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendColumn(RequestDetailsLogger.Current, EwsMetadata.VersionInfo, "Target", string.IsNullOrEmpty(text) ? "None" : text);
				if (headers.Get("SOAPAction") != null && headers.Get("Authorization") != null && headers.Get("Authorization").StartsWith("Negotiate") && headers.Get("SOAPAction").ToLowerInvariant().Contains("wssecurity") && HttpContext.Current.Request.Url.PathAndQuery.ToLowerInvariant().Contains("wssecurity"))
				{
					string userAgent = HttpContext.Current.Request.UserAgent;
					ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>(0L, "Bad request from {0}. Returning BadRequest to workaround WCF crash.", userAgent);
					Global.SetHttpResponse(HttpContext.Current, HttpStatusCode.BadRequest);
				}
			}
			string sourceCafeServer = CafeHelper.GetSourceCafeServer(HttpContext.Current.Request);
			if (!string.IsNullOrEmpty(sourceCafeServer))
			{
				RequestDetailsLogger.Current.Set(EwsMetadata.FrontEndServer, sourceCafeServer);
			}
			UserWorkloadManager userWorkloadManager = (UserWorkloadManager)base.Application["WS_WorkloadManagerKey"];
			if (UserWorkloadManager.Singleton.IsQueueFull)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError(0L, "Global.Application_BeginRequest WorkloadManager queue is full. Returning ServiceUnavailable.");
				PerformanceMonitor.UpdateTotalRequestRejectionsCount();
				Global.SetHttpResponse(HttpContext.Current, HttpStatusCode.ServiceUnavailable);
			}
			Stopwatch stopwatch = new Stopwatch();
			HttpContext.Current.Items["ServicesStopwatch"] = stopwatch;
			stopwatch.Start();
			if (Global.issueEwsCookie)
			{
				HttpResponse response = HttpContext.Current.Response;
				HttpRequest request = HttpContext.Current.Request;
				if (request.Cookies["exchangecookie"] == null)
				{
					HttpCookie httpCookie = new HttpCookie("exchangecookie");
					httpCookie.Expires = (DateTime)ExDateTime.Now.AddYears(1);
					httpCookie.HttpOnly = true;
					httpCookie.Path = "/";
					httpCookie.Value = Guid.NewGuid().ToString("N");
					response.Cookies.Add(httpCookie);
					return;
				}
				HttpCookie httpCookie2 = request.Cookies["exchangecookie"];
				if (httpCookie2.Value.Length == 32)
				{
					response.Cookies.Add(httpCookie2);
				}
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004B64 File Offset: 0x00002D64
		private void Application_EndRequest(object sender, EventArgs e)
		{
			try
			{
				if (base.Response.StatusCode == 404 && base.Response.SubStatusCode == 13)
				{
					base.Response.StatusCode = 507;
				}
				double wcfDispatchLatency = EWSSettings.WcfDispatchLatency;
				if (wcfDispatchLatency > 100.0)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "WcfLatency", wcfDispatchLatency);
				}
				Stopwatch stopwatch = HttpContext.Current.Items["ServicesStopwatch"] as Stopwatch;
				if (stopwatch != null)
				{
					stopwatch.Stop();
					long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
					PerformanceMonitor.UpdateResponseTimePerformanceCounter(elapsedMilliseconds);
				}
			}
			finally
			{
				Global.CleanUpRequestObjects();
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004C18 File Offset: 0x00002E18
		internal static void CleanUpRequestObjects()
		{
			RequestDetailsLogger requestDetailsLogger = RequestDetailsLoggerBase<RequestDetailsLogger>.Current;
			try
			{
				int num = (int)EwsCpuBasedSleeper.Singleton.GetDelayTime();
				if (num > 0)
				{
					requestDetailsLogger.AppendGenericInfo("Delay", num);
				}
				try
				{
					Global.DisposeServiceCommand();
					Global.DisposeCallContext();
					Global.DisposeRequestMessage();
				}
				finally
				{
					Global.ResetServerVersion();
				}
				try
				{
					if (requestDetailsLogger != null)
					{
						ActivityContext.SetThreadScope(requestDetailsLogger.ActivityScope);
					}
				}
				finally
				{
					if (requestDetailsLogger != null && !requestDetailsLogger.IsDisposed)
					{
						requestDetailsLogger.Commit();
					}
					if (num > 0 && HttpContext.Current != null && HttpContext.Current.Response != null && HttpContext.Current.Response.IsClientConnected)
					{
						Thread.Sleep(num);
					}
				}
			}
			finally
			{
				BufferedRegionStream bufferedRegionStream = EWSSettings.MessageStream as BufferedRegionStream;
				if (bufferedRegionStream != null)
				{
					bufferedRegionStream.Dispose();
					EWSSettings.MessageStream = null;
				}
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004CF8 File Offset: 0x00002EF8
		private static void DisposeRequestMessage()
		{
			Message message = HttpContext.Current.Items["EwsHttpContextMessage"] as Message;
			if (message != null)
			{
				message.Close();
				HttpContext.Current.Items.Remove("EwsHttpContextMessage");
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004D3C File Offset: 0x00002F3C
		internal static void DisposeCallContext()
		{
			CallContext callContext = HttpContext.Current.Items["CallContext"] as CallContext;
			if (callContext != null)
			{
				callContext.Dispose();
				HttpContext.Current.Items["CallContext"] = null;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D81 File Offset: 0x00002F81
		internal static void ResetServerVersion()
		{
			HttpContext.Current.Items["WS_ServerVersionKey"] = null;
			ExchangeVersion.Current = null;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004DA0 File Offset: 0x00002FA0
		internal static void DisposeServiceCommand()
		{
			ServiceCommandBase serviceCommandBase = HttpContext.Current.Items["WS_ServiceCommandKey"] as ServiceCommandBase;
			if (serviceCommandBase != null)
			{
				IDictionary items = HttpContext.Current.Items;
				if (items.Contains("WS_ServiceProviderRequestIdKey"))
				{
					Global.TraceCasStop(serviceCommandBase.GetType(), serviceCommandBase.CallContext, (Guid)items["WS_ServiceProviderRequestIdKey"]);
				}
				IDisposable disposable = serviceCommandBase as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				HttpContext.Current.Items["WS_ServiceCommandKey"] = null;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004E28 File Offset: 0x00003028
		private void InitializeIdentityCache()
		{
			int num = Global.GetAppSettingAsInt("IdentityCacheSizeLimit", 4000);
			if (num <= 0)
			{
				num = 4000;
			}
			int appSettingAsInt = Global.GetAppSettingAsInt("PerUserAccessPolicyTimeoutInSeconds", -1);
			if (appSettingAsInt < 0)
			{
				ADIdentityInformationCache.Initialize(num);
				return;
			}
			TimeSpan absoluteTimeout = TimeSpan.FromSeconds((double)appSettingAsInt);
			TimeSpan slidingTimeout = TimeSpan.FromSeconds((double)(appSettingAsInt / 2));
			ADIdentityInformationCache.Initialize(num, absoluteTimeout, slidingTimeout);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004E80 File Offset: 0x00003080
		private void Application_Start(object sender, EventArgs e)
		{
			string[] privilegesToKeep = new string[]
			{
				"SeAssignPrimaryTokenPrivilege",
				"SeAuditPrivilege",
				"SeChangeNotifyPrivilege",
				"SeCreateGlobalPrivilege",
				"SeImpersonatePrivilege",
				"SeIncreaseQuotaPrivilege",
				"SeTcbPrivilege"
			};
			int num = Privileges.RemoveAllExcept(privilegesToKeep, "MSExchangeServicesAppPool");
			if (num != 0)
			{
				Environment.Exit(num);
			}
			this.InitializeWatsonReporting();
			ODataConfig.Initialize();
			int appSettingAsInt = Global.GetAppSettingAsInt("AppSettingCertificateExpirationCheckerIntervalInMinutes", 4320);
			ExchangeCertificateChecker.Initialize(appSettingAsInt);
			this.InitializeIdentityCache();
			Global.InitializeEventQueuePollingInterval();
			Globals.InitializeMultiPerfCounterInstance("EWS");
			ExchangeDiagnosticsHelper.RegisterDiagnosticsComponents();
			Global.InitializeThrottlingPerfCounters();
			PerformanceMonitor.Initialize();
			Subscriptions.Initialize();
			base.Application["WS_APPWideMailboxCacheKey"] = new AppWideStoreSessionCache();
			base.Application["WS_AcceptedDomainCacheKey"] = new AcceptedDomainCache();
			UserWorkloadManager userWorkloadManager = Global.CreateWorkloadManager();
			base.Application["WS_WorkloadManagerKey"] = userWorkloadManager;
			ShutdownHandler.Singleton.Add(userWorkloadManager);
			Global.InitializeSettingsFromWebConfig();
			Global.InitializeProxyTimeout();
			Global.InitializeSearchTimeout();
			Global.InitializeAccessPolicyTimeout();
			Global.writeRequestDetailsToLog = Global.GetAppSettingAsBool("WriteRequestsToLog", true);
			ExRpcModule.Bind();
			StoreSession.AbandonNotificationsDuringShutdown(false);
			UMClientCommonBase.InitializePerformanceCounters(true);
			ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(FaultInjection.Callback));
			KillBitTimer.Singleton.Start();
			KillbitWatcher.TryWatch(new KillbitWatcher.ReadKillBitFromFileCallback(KillBitHelper.ReadKillBitFromFile));
			ServiceDiagnostics.LogEventWithTrace(ServicesEventLogConstants.Tuple_StartedSuccessfully, null, ExTraceGlobals.CommonAlgorithmTracer, this, "Application started successfully.", null);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005004 File Offset: 0x00003204
		internal static void InitializeSettingsFromWebConfig()
		{
			Global.proxyToSelf = Global.GetAppSettingAsBool("ProxyToSelf", false);
			Global.writeProxyHopHeaders = Global.GetAppSettingAsBool("WriteProxyHopHeaders", false);
			Global.writeFailoverTypeHeader = Global.GetAppSettingAsBool("WriteFailoverTypeHeader", false);
			Global.writeStackTraceOnISE = Global.GetAppSettingAsBool("WriteStackTraceOnISE", false);
			Global.writeStackTraceToProtocolLogForErrorTypes = Global.GetAppSettingAsHashSet("WriteStackTraceToProtocolLogForErrorTypes", null);
			Global.sendDebugResponseHeaders = Global.GetAppSettingAsBool("SendDebugResponseHeaders", false);
			Global.writeThrottlingDiagnostics = Global.GetAppSettingAsBool("WriteThrottlingDiagnostics", false);
			Global.issueEwsCookie = Global.GetAppSettingAsBool("IssueEwsCookie", true);
			Global.chargePreExecuteToBudgetEnabled = Global.GetAppSettingAsBool("ChargePreExecuteToBudgetEnabled", true);
			Global.accessingPrincipalCacheSize = Global.GetAppSettingAsInt("AccessingPrincipalCacheSize", 4);
			Global.maxAttachmentNestLevel = Global.GetAppSettingAsInt("MaxAttachmentNestingDepth", 10);
			Global.findCountLimit = Global.GetAppSettingAsInt("FindCountLimit", 1000);
			Global.hangingConnectionLimit = Global.GetAppSettingAsInt("HangingConnectionLimit", 10);
			Global.createItemRequestSizeThreshold = Global.GetAppSettingAsInt("CreateItemRequestSizeThreshold", 5120000);
			Global.privateWorkingSetThreshold = Global.GetAppSettingAsInt("PrivateWorkingSetThreshold", 716800000);
			Global.collectIntervalInMilliseconds = Global.GetAppSettingAsInt("CollectIntervalInMilliseconds", 300000);
			Global.sendXBEServerExceptionHeaderToCafe = Global.GetAppSettingAsBool("SendXBEServerExceptionHeaderToCafe", true);
			Global.enableMailboxLogger = Global.GetAppSettingAsBool("EnableMailboxLogger", false);
			Global.FastParticipantResolveEnabled = Global.GetAppSettingAsBool("FastParticipantResolveEnabled", true);
			Global.enableSchemaValidationOverride = null;
			Global.useGcCollect = Global.GetAppSettingAsBool("UseGcCollect", false);
			Global.exchangePrincipalCacheTimeoutInMinutes = Global.GetAppSettingAsInt("ExchangePrincipalCacheTimeoutInMinutes", 5);
			Global.exchangePrincipalCacheTimeoutInSecondsOnError = Global.GetAppSettingAsInt("ExchangePrincipalCacheTimeoutInSecondsOnError", 30);
			string value = ConfigurationManager.AppSettings["EnableSchemaValidationOverride"];
			bool value2;
			if (!string.IsNullOrEmpty(value) && bool.TryParse(value, out value2))
			{
				Global.enableSchemaValidationOverride = new bool?(value2);
			}
			Global.bulkOperationThrottlingEnabled = Global.GetAppSettingAsBool("BulkOperationThrottlingEnabled", Global.bulkOperationThrottlingEnabled);
			Global.bulkOperationConcurrencyCap = Global.GetAppSettingAsInt("BulkOperationConcurrencyCap", Global.bulkOperationConcurrencyCap);
			Global.bulkOperationMethods = Global.GetAppSettingAsHashSet("BulkOperationMethods", Global.bulkOperationMethods);
			SingleComponentThrottlingPolicy.BulkOperationConcurrencyCap = Convert.ToUInt32(Global.bulkOperationConcurrencyCap);
			Global.nonInteractiveOperationThrottlingEnabled = Global.GetAppSettingAsBool("NonInteractiveOperationThrottlingEnabled", Global.nonInteractiveOperationThrottlingEnabled);
			Global.nonInteractiveOperationConcurrencyCap = Global.GetAppSettingAsInt("NonInteractiveOperationConcurrencyCap", Global.nonInteractiveOperationConcurrencyCap);
			Global.nonInteractiveOperationMethods = Global.GetAppSettingAsHashSet("NonInteractiveOperationMethods", null);
			SingleComponentThrottlingPolicy.NonInteractiveOperationConcurrencyCap = Convert.ToUInt32(Global.nonInteractiveOperationConcurrencyCap);
			Global.backgroundLoadedTasksEnabled = Global.GetAppSettingAsBool("BackgroundLoadedTasksEnabled", Global.backgroundLoadedTasksEnabled);
			Global.backgroundLoadedTasks = Global.GetAppSettingAsHashSet("BackgroundLoadedTasks", Global.backgroundLoadedTasks);
			Global.backgroundSyncTasksForWellKnownClientsEnabled = Global.GetAppSettingAsBool("BackgroundSyncTasksForWellKnownClientsEnabled", Global.backgroundSyncTasksForWellKnownClientsEnabled);
			Global.backgroundSyncTasksForWellKnownClients = Global.GetAppSettingAsHashSet("BackgroundSyncTasksForWellKnownClients", Global.backgroundSyncTasksForWellKnownClients);
			Global.wellKnownClientsForBackgroundSync = Global.GetAppSettingAsHashSet("WellKnownClientsForBackgroundSync", Global.wellKnownClientsForBackgroundSync);
			Global.disableCommandOptimizationSet = Global.GetAppSettingAsHashSet("DisableCommandOptimizations", null);
			Global.disableAllCommandOptimizations = Global.disableCommandOptimizationSet.Contains("all");
			Global.SendXBEServerExceptionHeaderToCafeOnFailover = Global.GetAppSettingAsBool("SendXBEServerExceptionHeaderToCafeOnFailover", true);
			Global.EnableFaultInjection = Global.GetAppSettingAsBool("EnableFaultInjection", false);
			Global.httpHandlerDisabledMethods = Global.GetAppSettingAsHashSet("HttpHandlerDisabledMethods", Global.httpHandlerDisabledMethods);
			Global.oDataStackTraceInErrorResponse = Global.GetAppSettingAsBool("OData.StackTraceInErrorResponse", false);
			Global.InitializeMaximumTemporaryFilteredViewPerUser();
			if (Global.EnableFaultInjection)
			{
				Global.GetFaultInjectionConfig();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000532B File Offset: 0x0000352B
		public static bool ProxyToSelf
		{
			get
			{
				return Global.proxyToSelf;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00005332 File Offset: 0x00003532
		public static int EventQueuePollingInterval
		{
			get
			{
				return Global.eventQueuePollingInterval;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005339 File Offset: 0x00003539
		public static bool ShowDebugInformation
		{
			get
			{
				return Global.GetAppSettingAsBool("ShowDebugInformation", false);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00005346 File Offset: 0x00003546
		public static bool ChargePreExecuteToBudgetEnabled
		{
			get
			{
				return Global.chargePreExecuteToBudgetEnabled;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005350 File Offset: 0x00003550
		internal static void TraceCasStop(object serviceProviderOperation, CallContext callContext, Guid serviceRequestId)
		{
			if (ETWTrace.ShouldTraceCasStop(serviceRequestId))
			{
				object obj = callContext.EffectiveCaller.PrimarySmtpAddress;
				obj = (obj ?? callContext.EffectiveCallerSid);
				int bytesIn = 0;
				HttpContext httpContext = HttpContext.Current;
				string serverAddress = string.Empty;
				if (httpContext != null)
				{
					HttpRequest request = httpContext.Request;
					if (request != null)
					{
						bytesIn = request.TotalBytes;
						Uri url = request.Url;
						serverAddress = ((url != null) ? url.Host : string.Empty);
					}
				}
				Microsoft.Exchange.Diagnostics.Trace.TraceCasStop(CasTraceEventType.Ews, serviceRequestId, bytesIn, 0, serverAddress, obj, serviceProviderOperation, string.Empty, string.Empty);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000053DA File Offset: 0x000035DA
		public static int ProxyTimeout
		{
			get
			{
				return Global.proxyTimeout;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000053E1 File Offset: 0x000035E1
		public static bool WriteProxyHopHeaders
		{
			get
			{
				return Global.writeProxyHopHeaders;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000053E8 File Offset: 0x000035E8
		public static bool WriteFailoverTypeHeader
		{
			get
			{
				return Global.writeFailoverTypeHeader;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000053EF File Offset: 0x000035EF
		public static HashSet<string> BulkOperationMethods
		{
			get
			{
				return Global.bulkOperationMethods;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000053F6 File Offset: 0x000035F6
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x000053FD File Offset: 0x000035FD
		public static HashSet<string> NonInteractiveOperationMethods
		{
			get
			{
				return Global.nonInteractiveOperationMethods;
			}
			set
			{
				Global.nonInteractiveOperationMethods = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005405 File Offset: 0x00003605
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000540C File Offset: 0x0000360C
		public static HashSet<string> BackgroundLoadedTasks
		{
			get
			{
				return Global.backgroundLoadedTasks;
			}
			set
			{
				Global.backgroundLoadedTasks = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005414 File Offset: 0x00003614
		public static HashSet<string> LongRunningScenarioTasks
		{
			get
			{
				return Global.longRunningScenarioTasks;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000541B File Offset: 0x0000361B
		public static HashSet<string> LongRunningScenarioNonBackgroundTasks
		{
			get
			{
				return Global.longRunningScenarioNonBackgroundTasks;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005422 File Offset: 0x00003622
		public static HashSet<RoleType> LongRunningScenarioEnabledRoleTypes
		{
			get
			{
				return Global.longRunningScenarioEnabledRoleTypes;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005429 File Offset: 0x00003629
		public static Regex LongRunningScenarioEnabledUserAgents
		{
			get
			{
				return Global.longRunningScenarioEnabledUserAgents;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005430 File Offset: 0x00003630
		public static bool SendDebugResponseHeaders
		{
			get
			{
				return Global.sendDebugResponseHeaders;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005437 File Offset: 0x00003637
		public static bool WriteRequestDetailsToLog
		{
			get
			{
				return Global.writeRequestDetailsToLog;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000543E File Offset: 0x0000363E
		public static HashSet<string> HttpHandleDisabledMethods
		{
			get
			{
				return Global.httpHandlerDisabledMethods;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005445 File Offset: 0x00003645
		public static bool BulkOperationThrottlingEnabled
		{
			get
			{
				return Global.bulkOperationThrottlingEnabled;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000544C File Offset: 0x0000364C
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005453 File Offset: 0x00003653
		public static bool NonInteractiveThrottlingEnabled
		{
			get
			{
				return Global.nonInteractiveOperationThrottlingEnabled;
			}
			set
			{
				Global.nonInteractiveOperationThrottlingEnabled = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000545B File Offset: 0x0000365B
		public static bool BackgroundLoadedTasksEnabled
		{
			get
			{
				return Global.backgroundLoadedTasksEnabled;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005462 File Offset: 0x00003662
		public static bool BackgroundSyncTasksForWellKnownClientsEnabled
		{
			get
			{
				return Global.backgroundSyncTasksForWellKnownClientsEnabled;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005469 File Offset: 0x00003669
		public static HashSet<string> BackgroundSyncTasksForWellKnownClients
		{
			get
			{
				return Global.backgroundSyncTasksForWellKnownClients;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005470 File Offset: 0x00003670
		public static HashSet<string> WellKnownClientsForBackgroundSync
		{
			get
			{
				return Global.wellKnownClientsForBackgroundSync;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005478 File Offset: 0x00003678
		internal static FileVersionInfo BuildVersionInfo
		{
			get
			{
				if (Global.buildVersionInfo == null)
				{
					FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
					Interlocked.Exchange<FileVersionInfo>(ref Global.buildVersionInfo, versionInfo);
				}
				return Global.buildVersionInfo;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000054AD File Offset: 0x000036AD
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000054B4 File Offset: 0x000036B4
		internal static BudgetType BudgetType
		{
			get
			{
				return Global.budgetType;
			}
			set
			{
				Global.budgetType = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000054BC File Offset: 0x000036BC
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000054C3 File Offset: 0x000036C3
		internal static BudgetType BulkOperationBudgetType
		{
			get
			{
				return Global.bulkOperationBudgetType;
			}
			set
			{
				Global.bulkOperationBudgetType = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000054CB File Offset: 0x000036CB
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000054D2 File Offset: 0x000036D2
		internal static BudgetType NonInteractiveOperationBudgetType
		{
			get
			{
				return Global.nonInteractiveOperationBudgetType;
			}
			set
			{
				Global.nonInteractiveOperationBudgetType = value;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000054DA File Offset: 0x000036DA
		internal static void SetHttpResponse(HttpContext context, HttpStatusCode statusCode)
		{
			context.Response.StatusCode = (int)statusCode;
			context.Response.SuppressContent = true;
			context.ApplicationInstance.CompleteRequest();
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005500 File Offset: 0x00003700
		internal static HashSet<string> GetAppSettingAsHashSet(string key, HashSet<string> existingHashSet = null)
		{
			string text = ConfigurationManager.AppSettings[key];
			HashSet<string> hashSet = (existingHashSet != null) ? existingHashSet : new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
			if (!string.IsNullOrEmpty(text))
			{
				foreach (string text2 in text.Split(new char[]
				{
					',',
					';'
				}))
				{
					hashSet.Add(text2.ToLowerInvariant());
				}
			}
			return hashSet;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005574 File Offset: 0x00003774
		private void InitializeWatsonReporting()
		{
			bool appSettingAsBool = Global.GetAppSettingAsBool("SendWatsonReport", true);
			bool appSettingAsBool2 = Global.GetAppSettingAsBool("FilterExceptionsFromWatsonReport", true);
			ServiceDiagnostics.InitializeWatsonReporting(appSettingAsBool, appSettingAsBool2);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000055A0 File Offset: 0x000037A0
		private void Application_Error(object sender, EventArgs e)
		{
			HttpApplication httpApplication = (HttpApplication)sender;
			Exception lastError = httpApplication.Server.GetLastError();
			if (Global.IsKnownHttpException(lastError) || Global.IsKnownWcfException(lastError))
			{
				return;
			}
			if (Global.IsDocumentationError(lastError, httpApplication.Context, "/ews/UM2007Legacy.asmx"))
			{
				httpApplication.Server.ClearError();
				HttpContext.Current.Response.StatusCode = 200;
				HttpContext.Current.Response.Write(string.Format("<HTML><HEAD><TITLE>{0}</TITLE></HEAD><BODY><B>{0}</B></BODY></HTML>", Strings.UMWebServicePage));
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.Close();
				return;
			}
			if (Global.IsDocumentationError(lastError, httpApplication.Context, "/ews/exchange.asmx"))
			{
				httpApplication.Server.ClearError();
				HttpContext.Current.Response.Redirect("Services.wsdl", false);
				HttpContext.Current.Response.Flush();
				HttpContext.Current.Response.Close();
				return;
			}
			ServiceDiagnostics.ReportException(lastError, ServicesEventLogConstants.Tuple_InternalServerError, null, "Exception from Application_Error event: {0}");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000056A8 File Offset: 0x000038A8
		internal static bool IsDocumentationError(Exception exception, HttpContext httpContext, string path)
		{
			if (!(exception is InvalidOperationException))
			{
				return false;
			}
			if (httpContext == null)
			{
				return false;
			}
			HttpRequest request = httpContext.Request;
			return !(request.RequestType != "GET") && string.Equals(request.Path, path, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000056F1 File Offset: 0x000038F1
		internal static bool IsKnownWcfException(Exception exception)
		{
			return exception is ServiceActivationException;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005700 File Offset: 0x00003900
		internal static bool IsKnownHttpException(Exception exception)
		{
			bool result = false;
			HttpException ex = exception as HttpException;
			if (ex != null)
			{
				int httpCode = ex.GetHttpCode();
				if (httpCode >= 400 && httpCode <= 599 && httpCode != 500)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000573C File Offset: 0x0000393C
		private void Application_End(object sender, EventArgs e)
		{
			RequestDetailsLogger.FlushQueuedFileWrites();
			StoreSession.AbandonNotificationsDuringShutdown(true);
			if (Subscriptions.Singleton != null)
			{
				Subscriptions.Singleton.Dispose();
			}
			ExchangeCertificateChecker.Terminate();
			this.DisposeApplicationObject("WS_APPWideMailboxCacheKey");
			this.DisposeApplicationObject("WS_AcceptedDomainCacheKey");
			ExchangeDiagnosticsHelper.UnRegisterDiagnosticsComponents();
			ServiceDiagnostics.LogEventWithTrace(ServicesEventLogConstants.Tuple_StoppedSuccessfully, null, ExTraceGlobals.CommonAlgorithmTracer, this, "Application stopped successfully.", null);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000057A0 File Offset: 0x000039A0
		private void DisposeApplicationObject(string applicationCacheKey)
		{
			using (base.Application[applicationCacheKey] as IDisposable)
			{
				base.Application.Remove(applicationCacheKey);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000057F0 File Offset: 0x000039F0
		private static void GetFaultInjectionConfig()
		{
			Configuration configuration = WebConfigurationManager.OpenWebConfiguration("/EWS");
			AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
			string rawXml = appSettingsSection.SectionInformation.GetRawXml();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(rawXml);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//appSettings/add[@key = 'FaultInjectionFault']/@value");
			if (xmlNodeList == null)
			{
				Global.EnableFaultInjection = false;
				return;
			}
			List<string> list = (from XmlNode node in xmlNodeList
			select node.Value).ToList<string>();
			Global.FaultsList = new List<Dictionary<string, string>>();
			foreach (string text in list)
			{
				string[] fault = text.Split(new char[]
				{
					';'
				});
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string valueForKey = Global.GetValueForKey("HttpStatus", fault);
				string valueForKey2 = Global.GetValueForKey("ErrorCode", fault);
				if ((string.IsNullOrEmpty(valueForKey) || valueForKey == "200") && string.IsNullOrEmpty(valueForKey2))
				{
					Global.EnableFaultInjection = false;
					break;
				}
				dictionary.Add("HttpStatus", valueForKey);
				dictionary.Add("ErrorCode", valueForKey2);
				dictionary.Add("SoapAction", Global.GetValueForKey("SoapAction", fault));
				string text2 = Global.GetValueForKey("UserName", fault);
				if (!string.IsNullOrEmpty(text2))
				{
					text2 = text2.ToLower();
				}
				dictionary.Add("UserName", text2);
				Global.FaultsList.Add(dictionary);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000599C File Offset: 0x00003B9C
		public static string GetValueForKey(string key, string[] fault)
		{
			foreach (string text in fault)
			{
				if (text.StartsWith(key))
				{
					return text.Substring(key.Length + 1);
				}
			}
			return string.Empty;
		}

		// Token: 0x04000033 RID: 51
		internal const string ServiceCommandKey = "WS_ServiceCommandKey";

		// Token: 0x04000034 RID: 52
		internal const string ServiceProviderRequestIdKey = "WS_ServiceProviderRequestIdKey";

		// Token: 0x04000035 RID: 53
		internal const string ServerVersionKey = "WS_ServerVersionKey";

		// Token: 0x04000036 RID: 54
		internal const string AnchorMailboxHintKey = "AnchorMailboxHintKey";

		// Token: 0x04000037 RID: 55
		internal const CasTraceEventType EwsTraceEventType = CasTraceEventType.Ews;

		// Token: 0x04000038 RID: 56
		private const string AppSettingEncodeStringProperties = "EncodeStringProperties";

		// Token: 0x04000039 RID: 57
		private const int DefaultFindCountLimit = 1000;

		// Token: 0x0400003A RID: 58
		private const int DefaultSearchTimeoutInMilliseconds = 60000;

		// Token: 0x0400003B RID: 59
		private const int DefaultMaximumTemporaryFilteredViewPerUser = 20;

		// Token: 0x0400003C RID: 60
		private const int DefaultHangingConnectionLimit = 10;

		// Token: 0x0400003D RID: 61
		private const string DefaultJunkMailReportingAddress = "junk@office365.microsoft.com";

		// Token: 0x0400003E RID: 62
		private const string DefaultNotJunkMailReportingAddress = "not_junk@office365.microsoft.com";

		// Token: 0x0400003F RID: 63
		private const int DefaultCreateItemRequestSizeThreshold = 5120000;

		// Token: 0x04000040 RID: 64
		private const int DefaultPrivateWorkingSetThreshold = 716800000;

		// Token: 0x04000041 RID: 65
		private const int DefaultCollectIntervalInMilliseconds = 300000;

		// Token: 0x04000042 RID: 66
		private const int DefaultOrganizationWideAccessPolicyTimeoutInSeconds = 10800;

		// Token: 0x04000043 RID: 67
		internal const string TargetVersionHeaderName = "X-EWS-TargetVersion";

		// Token: 0x04000044 RID: 68
		internal const string AppWideMailboxCacheKey = "WS_APPWideMailboxCacheKey";

		// Token: 0x04000045 RID: 69
		internal const string AcceptedDomainCacheKey = "WS_AcceptedDomainCacheKey";

		// Token: 0x04000046 RID: 70
		internal const string WorkloadManagerKey = "WS_WorkloadManagerKey";

		// Token: 0x04000047 RID: 71
		internal const string ExchangeUM12LegacyAsmx = "/ews/UM2007Legacy.asmx";

		// Token: 0x04000048 RID: 72
		internal const string ServicesRequestDetailsLoggerKey = "WS_RequestDetailsLoggerKey";

		// Token: 0x04000049 RID: 73
		private const string HttpGet = "GET";

		// Token: 0x0400004A RID: 74
		private const string ExchangeAsmx = "/ews/exchange.asmx";

		// Token: 0x0400004B RID: 75
		private const string ServicesWsdl = "Services.wsdl";

		// Token: 0x0400004C RID: 76
		internal const string CertificateValidationComponentId = "EWS";

		// Token: 0x0400004D RID: 77
		internal const string ProxyCertificateValidationComponentId = "EwsProxy";

		// Token: 0x0400004E RID: 78
		private const string ServicesStopwatch = "ServicesStopwatch";

		// Token: 0x0400004F RID: 79
		private const string AppSettingSendWatsonReport = "SendWatsonReport";

		// Token: 0x04000050 RID: 80
		private const string AppSettingFilterExceptionsFromWatsonReport = "FilterExceptionsFromWatsonReport";

		// Token: 0x04000051 RID: 81
		private const string AppSettingProxyToSelf = "ProxyToSelf";

		// Token: 0x04000052 RID: 82
		private const string AppSettingWriteProxyHopHeaders = "WriteProxyHopHeaders";

		// Token: 0x04000053 RID: 83
		private const string AppSettingProxyTimeout = "ProxyTimeout";

		// Token: 0x04000054 RID: 84
		private const string AppSettingWriteRequestsToLog = "WriteRequestsToLog";

		// Token: 0x04000055 RID: 85
		private const string AppSettingWriteFailoverTypeHeader = "WriteFailoverTypeHeader";

		// Token: 0x04000056 RID: 86
		private const string AppSettingWriteStackTraceOnISE = "WriteStackTraceOnISE";

		// Token: 0x04000057 RID: 87
		private const string AppSettingWriteStackTraceToProtocolLogForErrorTypes = "WriteStackTraceToProtocolLogForErrorTypes";

		// Token: 0x04000058 RID: 88
		private const string AppSettingSendDebugResponseHeaders = "SendDebugResponseHeaders";

		// Token: 0x04000059 RID: 89
		private const string AppSettingSearchTimeout = "SearchTimeoutInMilliseconds";

		// Token: 0x0400005A RID: 90
		private const string AppSettingEDiscoverySearchTimeout = "EDiscoverySearchTimeoutInMilliseconds";

		// Token: 0x0400005B RID: 91
		private const string AppSettingMassUserOverBudgetPercent = "MassUserOverBudgetPercent";

		// Token: 0x0400005C RID: 92
		private const string AppSettingDelayTimeThreshold = "DelayTimeThreshold";

		// Token: 0x0400005D RID: 93
		private const string AppSettingIssueEwsCookie = "IssueEwsCookie";

		// Token: 0x0400005E RID: 94
		private const string AppSettingAccessingPrincipalCacheSize = "AccessingPrincipalCacheSize";

		// Token: 0x0400005F RID: 95
		private const string AppSettingMaxWorkerThreadsPerProcessor = "MaxWorkerThreadsPerProcessor";

		// Token: 0x04000060 RID: 96
		private const string AppSettingMaxTasksQueued = "MaxTasksQueued";

		// Token: 0x04000061 RID: 97
		private const string AppSettingEventQueuePollingInterval = "EventQueuePollingInterval";

		// Token: 0x04000062 RID: 98
		private const string AppSettingWriteThrottlingDiagnostics = "WriteThrottlingDiagnostics";

		// Token: 0x04000063 RID: 99
		private const string AppSettingPerUserAccessPolicyTimeoutInSeconds = "PerUserAccessPolicyTimeoutInSeconds";

		// Token: 0x04000064 RID: 100
		private const string AppSettingOrganizationWideAccessPolicyTimeoutInSeconds = "OrganizationWideAccessPolicyTimeoutInSeconds";

		// Token: 0x04000065 RID: 101
		private const string AppSettingCertificateExpirationCheckerIntervalInMinutes = "AppSettingCertificateExpirationCheckerIntervalInMinutes";

		// Token: 0x04000066 RID: 102
		private const string AppSettingDisableCommandOptimizations = "DisableCommandOptimizations";

		// Token: 0x04000067 RID: 103
		private const string AppSettingIdentityCacheSizeLimit = "IdentityCacheSizeLimit";

		// Token: 0x04000068 RID: 104
		private const string AppSettingMaxAttachmentNestingDepth = "MaxAttachmentNestingDepth";

		// Token: 0x04000069 RID: 105
		private const string AppSettingFindCountLimit = "FindCountLimit";

		// Token: 0x0400006A RID: 106
		private const string AppSettingShowDebugInformation = "ShowDebugInformation";

		// Token: 0x0400006B RID: 107
		private const string AppSettingMaximumTemporaryFilteredViewPerUser = "MaximumTemporaryFilteredViewPerUser";

		// Token: 0x0400006C RID: 108
		private const string AppSettingBulkOperationThrottlingEnabled = "BulkOperationThrottlingEnabled";

		// Token: 0x0400006D RID: 109
		private const string AppSettingBulkOperationMethods = "BulkOperationMethods";

		// Token: 0x0400006E RID: 110
		private const string AppSettingBulkOperationConcurrencyCap = "BulkOperationConcurrencyCap";

		// Token: 0x0400006F RID: 111
		private const string AppSettingNonInteractiveOperationThrottlingEnabled = "NonInteractiveOperationThrottlingEnabled";

		// Token: 0x04000070 RID: 112
		private const string AppSettingNonInteractiveOperationMethods = "NonInteractiveOperationMethods";

		// Token: 0x04000071 RID: 113
		private const string AppSettingNonInteractiveOperationConcurrencyCap = "NonInteractiveOperationConcurrencyCap";

		// Token: 0x04000072 RID: 114
		private const string AppSettingBackgroundLoadedTasksEnabled = "BackgroundLoadedTasksEnabled";

		// Token: 0x04000073 RID: 115
		private const string AppSettingBackgroundLoadedTasks = "BackgroundLoadedTasks";

		// Token: 0x04000074 RID: 116
		private const string AppSettingBackgroundSyncTasksForWellKnownClientsEnabled = "BackgroundSyncTasksForWellKnownClientsEnabled";

		// Token: 0x04000075 RID: 117
		private const string AppSettingBackgroundSyncTasksForWellKnownClients = "BackgroundSyncTasksForWellKnownClients";

		// Token: 0x04000076 RID: 118
		private const string AppSettingWellKnownClientsForBackgroundSync = "WellKnownClientsForBackgroundSync";

		// Token: 0x04000077 RID: 119
		private const string AppSettingChargePreExecuteToBudgetEnabled = "ChargePreExecuteToBudgetEnabled";

		// Token: 0x04000078 RID: 120
		private const string AppSettingHangingConnectionLimit = "HangingConnectionLimit";

		// Token: 0x04000079 RID: 121
		private const string AppSettingEnableSchemaValidationOverride = "EnableSchemaValidationOverride";

		// Token: 0x0400007A RID: 122
		private const string AppSettingUseGcCollect = "UseGcCollect";

		// Token: 0x0400007B RID: 123
		private const string AppSettingCreateItemRequestSizeThreshold = "CreateItemRequestSizeThreshold";

		// Token: 0x0400007C RID: 124
		private const string AppSettingPrivateWorkingSetThreshold = "PrivateWorkingSetThreshold";

		// Token: 0x0400007D RID: 125
		private const string AppSettingCollectIntervalInMilliseconds = "CollectIntervalInMilliseconds";

		// Token: 0x0400007E RID: 126
		private const string AppSettingSendXBEServerExceptionHeaderToCafe = "SendXBEServerExceptionHeaderToCafe";

		// Token: 0x0400007F RID: 127
		private const string AppSettingEnableMailboxLogger = "EnableMailboxLogger";

		// Token: 0x04000080 RID: 128
		private const string AppSettingSendXBEServerExceptionHeaderToCafeOnFailover = "SendXBEServerExceptionHeaderToCafeOnFailover";

		// Token: 0x04000081 RID: 129
		private const string AppSettingExchangePrincipalCacheTimeoutInMinutes = "ExchangePrincipalCacheTimeoutInMinutes";

		// Token: 0x04000082 RID: 130
		private const string AppSettingFastParticipantResolveEnabled = "FastParticipantResolveEnabled";

		// Token: 0x04000083 RID: 131
		private const string AppSettingExchangePrincipalCacheTimeoutInSecondsOnError = "ExchangePrincipalCacheTimeoutInSecondsOnError";

		// Token: 0x04000084 RID: 132
		private const string AppSettingHttpHandlerDisabledMethods = "HttpHandlerDisabledMethods";

		// Token: 0x04000085 RID: 133
		private const int DefaultMassUserOverBudgetPercent = 0;

		// Token: 0x04000086 RID: 134
		private const int DefaultDelayTimeThresholdInMilliseconds = 0;

		// Token: 0x04000087 RID: 135
		private const int DefaultIdentityCacheSize = 4000;

		// Token: 0x04000088 RID: 136
		private const string AppSettingEnableFaultInjection = "EnableFaultInjection";

		// Token: 0x04000089 RID: 137
		private const string AppSettingFaultInjectionFaults = "FaultInjectionFault";

		// Token: 0x0400008A RID: 138
		private const string AppSettingODataStackTraceInErrorResponse = "OData.StackTraceInErrorResponse";

		// Token: 0x0400008B RID: 139
		private const string TrackingCookieName = "exchangecookie";

		// Token: 0x0400008C RID: 140
		private const int DefaultEventQueuePollingInterval = 5;

		// Token: 0x0400008D RID: 141
		private const int MinEventQueuePollingInterval = 1;

		// Token: 0x0400008E RID: 142
		private const int MaxEventQueuePollingInterval = 60;

		// Token: 0x0400008F RID: 143
		private const int DefaultCertificateCheckerTimerIntervalInMinutes = 4320;

		// Token: 0x04000090 RID: 144
		private const int MinimumPerUserAccessPolicyTimeoutInSeconds = 0;

		// Token: 0x04000091 RID: 145
		private const int MinimumOrganizationWideAccessPolicyTimeoutInSeconds = 0;

		// Token: 0x04000092 RID: 146
		internal static readonly string BooleanTrue = true.ToString();

		// Token: 0x04000093 RID: 147
		private static readonly UnifiedContactStoreConfiguration unifiedContactStoreConfiguration = new UnifiedContactStoreConfiguration();

		// Token: 0x04000094 RID: 148
		private static int maxAttachmentNestLevel = 10;

		// Token: 0x04000095 RID: 149
		private static int findCountLimit = 1000;

		// Token: 0x04000096 RID: 150
		private static int searchTimeoutInMilliseconds = 60000;

		// Token: 0x04000097 RID: 151
		private static int maximumTemporaryFilteredViewPerUser = 20;

		// Token: 0x04000098 RID: 152
		private static int hangingConnectionLimit = 10;

		// Token: 0x04000099 RID: 153
		private static bool? enableSchemaValidationOverride = null;

		// Token: 0x0400009A RID: 154
		private static bool useGcCollect = false;

		// Token: 0x0400009B RID: 155
		private static int createItemRequestSizeThreshold = 5120000;

		// Token: 0x0400009C RID: 156
		private static int privateWorkingSetThreshold = 716800000;

		// Token: 0x0400009D RID: 157
		private static int collectIntervalInMilliseconds = 300000;

		// Token: 0x0400009E RID: 158
		private static int organizationWideAccessPolicyTimeoutInSeconds = 10800;

		// Token: 0x0400009F RID: 159
		private static int exchangePrincipalCacheTimeoutInMinutes = 5;

		// Token: 0x040000A0 RID: 160
		private static int exchangePrincipalCacheTimeoutInSecondsOnError = 30;

		// Token: 0x040000A1 RID: 161
		private static bool writeStackTraceOnISE = false;

		// Token: 0x040000A2 RID: 162
		private static HashSet<string> writeStackTraceToProtocolLogForErrorTypes = new HashSet<string>();

		// Token: 0x040000A3 RID: 163
		private static bool writeThrottlingDiagnostics = false;

		// Token: 0x040000A4 RID: 164
		private static string junkMailReportingAddress = "junk@office365.microsoft.com";

		// Token: 0x040000A5 RID: 165
		private static string notJunkMailReportingAddress = "not_junk@office365.microsoft.com";

		// Token: 0x040000A6 RID: 166
		private static bool sendXBEServerExceptionHeaderToCafe = true;

		// Token: 0x040000A7 RID: 167
		private static bool enableMailboxLogger = false;

		// Token: 0x040000A8 RID: 168
		private static int accessingPrincipalCacheSize = 4;

		// Token: 0x040000A9 RID: 169
		private static HashSet<string> disableCommandOptimizationSet = new HashSet<string>();

		// Token: 0x040000AA RID: 170
		private static bool disableAllCommandOptimizations = false;

		// Token: 0x040000AB RID: 171
		private static bool oDataStackTraceInErrorResponse = false;

		// Token: 0x040000AC RID: 172
		private static IResponseShapeResolver responseShapeResolver = new DefaultResponseShapeResolver();

		// Token: 0x040000AD RID: 173
		private static EwsClientMailboxSessionCloningHandler ewsClientMailboxSessionCloningHandler = new EwsClientMailboxSessionCloningHandler(Global.DefaultEwsClientMailboxSessionCloningHandler);

		// Token: 0x040000AE RID: 174
		private static bool safeHtmlLoaded;

		// Token: 0x040000AF RID: 175
		private static string defaultMapiClientType = "Client=WebServices";

		// Token: 0x040000B0 RID: 176
		private static int getAttachmentSizeLimit = 38797312;

		// Token: 0x040000B1 RID: 177
		internal static readonly RemoteCertificateValidationCallback RemoteCertificateValidationCallback = (object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => EWSSettings.AllowInternalUntrustedCerts || errors == SslPolicyErrors.None;

		// Token: 0x040000B2 RID: 178
		private static readonly int defaultProxyTimeout = 59000;

		// Token: 0x040000B3 RID: 179
		private static readonly int minimumProxyTimeout = 10000;

		// Token: 0x040000B4 RID: 180
		private static readonly int minimumSearchTimeout = 10;

		// Token: 0x040000B5 RID: 181
		private static readonly int minimumTemporaryFilteredViewPerUser = 5;

		// Token: 0x040000B6 RID: 182
		private static readonly int defaultMaxWorkerThreadsPerProcessor = 10;

		// Token: 0x040000B7 RID: 183
		private static readonly int minimumMaxWorkerThreadsPerProcessor = 1;

		// Token: 0x040000B8 RID: 184
		private static readonly int defaultMaxTasksQueued = 500;

		// Token: 0x040000B9 RID: 185
		private static readonly int minimumMaxTasksQueued = 50;

		// Token: 0x040000BA RID: 186
		private static readonly int maximumMaxTasksQueued = 750;

		// Token: 0x040000BB RID: 187
		private static bool proxyToSelf = false;

		// Token: 0x040000BC RID: 188
		private static bool writeProxyHopHeaders = false;

		// Token: 0x040000BD RID: 189
		private static bool writeFailoverTypeHeader = false;

		// Token: 0x040000BE RID: 190
		private static bool writeRequestDetailsToLog = true;

		// Token: 0x040000BF RID: 191
		private static int proxyTimeout = Global.defaultProxyTimeout;

		// Token: 0x040000C0 RID: 192
		private static bool sendDebugResponseHeaders = false;

		// Token: 0x040000C1 RID: 193
		private static bool issueEwsCookie = true;

		// Token: 0x040000C2 RID: 194
		private static bool chargePreExecuteToBudgetEnabled = true;

		// Token: 0x040000C3 RID: 195
		private static bool bulkOperationThrottlingEnabled = true;

		// Token: 0x040000C4 RID: 196
		private static int bulkOperationConcurrencyCap = 2;

		// Token: 0x040000C5 RID: 197
		private static HashSet<string> bulkOperationMethods = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
		{
			typeof(MarkAllItemsAsRead).Name,
			typeof(EmptyFolder).Name
		};

		// Token: 0x040000C6 RID: 198
		private static HashSet<string> httpHandlerDisabledMethods = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x040000C7 RID: 199
		private static bool nonInteractiveOperationThrottlingEnabled = false;

		// Token: 0x040000C8 RID: 200
		private static int nonInteractiveOperationConcurrencyCap = 10;

		// Token: 0x040000C9 RID: 201
		private static HashSet<string> nonInteractiveOperationMethods = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x040000CA RID: 202
		private static bool backgroundLoadedTasksEnabled = true;

		// Token: 0x040000CB RID: 203
		private static HashSet<string> backgroundLoadedTasks = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
		{
			typeof(ExportItems).Name,
			typeof(GetImItemList).Name,
			typeof(UploadItems).Name
		};

		// Token: 0x040000CC RID: 204
		private static bool backgroundSyncTasksForWellKnownClientsEnabled = false;

		// Token: 0x040000CD RID: 205
		private static HashSet<string> backgroundSyncTasksForWellKnownClients = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase)
		{
			typeof(GetItem).Name,
			typeof(GetAttachment).Name
		};

		// Token: 0x040000CE RID: 206
		private static HashSet<string> wellKnownClientsForBackgroundSync = new HashSet<string>(StringComparer.InvariantCulture)
		{
			"MacOutlook".ToLowerInvariant()
		};

		// Token: 0x040000CF RID: 207
		private static Regex longRunningScenarioEnabledUserAgents = new Regex("EDiscovery", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

		// Token: 0x040000D0 RID: 208
		private static HashSet<RoleType> longRunningScenarioEnabledRoleTypes = new HashSet<RoleType>
		{
			RoleType.MailboxSearch
		};

		// Token: 0x040000D1 RID: 209
		private static HashSet<string> longRunningScenarioTasks = new HashSet<string>
		{
			typeof(SearchMailboxes).Name,
			typeof(GetNonIndexableItemStatistics).Name,
			typeof(GetNonIndexableItemDetails).Name,
			typeof(GetDiscoverySearchConfiguration).Name,
			typeof(GetFolder).Name,
			typeof(FindFolder).Name,
			typeof(ExportItems).Name,
			typeof(UploadItems).Name,
			typeof(GetItem).Name,
			typeof(CreateFolder).Name,
			typeof(DeleteFolder).Name,
			typeof(MoveFolder).Name,
			typeof(FindItem).Name,
			typeof(CreateItem).Name,
			typeof(UpdateItem).Name,
			typeof(DeleteItem).Name,
			typeof(GetAttachment).Name,
			typeof(CreateAttachment).Name,
			typeof(DeleteAttachment).Name
		};

		// Token: 0x040000D2 RID: 210
		private static HashSet<string> longRunningScenarioNonBackgroundTasks = new HashSet<string>
		{
			typeof(SearchMailboxes).Name
		};

		// Token: 0x040000D3 RID: 211
		private static int eventQueuePollingInterval = 5;

		// Token: 0x040000D4 RID: 212
		private static BudgetType budgetType = BudgetType.Ews;

		// Token: 0x040000D5 RID: 213
		private static BudgetType bulkOperationBudgetType = BudgetType.EwsBulkOperation;

		// Token: 0x040000D6 RID: 214
		private static BudgetType nonInteractiveOperationBudgetType = BudgetType.EwsBulkOperation;

		// Token: 0x040000D7 RID: 215
		private static FileVersionInfo buildVersionInfo;

		// Token: 0x040000D8 RID: 216
		internal static LazyMember<int> MaxMaxRequestSizeForEWS = new LazyMember<int>(delegate()
		{
			int num = 0;
			try
			{
				List<CustomBindingElement> list = new List<CustomBindingElement>(MessageEncoderWithXmlDeclaration.EwsBindingNames.Length);
				Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~/web.config");
				BindingsSection bindingsSection = (BindingsSection)configuration.GetSection("system.serviceModel/bindings");
				foreach (string text in MessageEncoderWithXmlDeclaration.EwsBindingNames)
				{
					if (bindingsSection.CustomBinding.Bindings.ContainsKey(text))
					{
						list.Add(bindingsSection.CustomBinding.Bindings[text]);
					}
					else
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "Binding {0} was not found in web.config file.", text);
					}
				}
				foreach (CustomBindingElement customBindingElement in list)
				{
					TransportElement transportElement = (TransportElement)customBindingElement[typeof(HttpsTransportElement)];
					if (transportElement == null)
					{
						transportElement = (TransportElement)customBindingElement[typeof(HttpTransportElement)];
					}
					if (transportElement != null)
					{
						if (transportElement.MaxReceivedMessageSize > 0L)
						{
							num = Math.Max(num, (int)transportElement.MaxReceivedMessageSize);
						}
					}
					else
					{
						ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "No transport element found for binding {0} in web.config file.", customBindingElement.Name);
					}
				}
			}
			catch (Exception)
			{
				num = 0;
			}
			return num;
		});

		// Token: 0x040000D9 RID: 217
		internal static LazyMember<bool> UseBufferRequestChannelListener = new LazyMember<bool>(delegate()
		{
			bool result = false;
			List<CustomBindingElement> list = new List<CustomBindingElement>(MessageEncoderWithXmlDeclaration.EwsBindingNames.Length);
			Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~/web.config");
			BindingsSection bindingsSection = (BindingsSection)configuration.GetSection("system.serviceModel/bindings");
			foreach (string text in MessageEncoderWithXmlDeclaration.EwsBindingNames)
			{
				if (bindingsSection.CustomBinding.Bindings.ContainsKey(text))
				{
					list.Add(bindingsSection.CustomBinding.Bindings[text]);
				}
				else
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug<string>(0L, "Binding {0} was not found in web.config file.", text);
				}
			}
			foreach (CustomBindingElement customBindingElement in list)
			{
				HttpsTransportElement httpsTransportElement = (HttpsTransportElement)customBindingElement[typeof(HttpsTransportElement)];
				if (httpsTransportElement != null)
				{
					if (httpsTransportElement.TransferMode == TransferMode.Streamed)
					{
						result = true;
					}
				}
				else
				{
					HttpTransportElement httpTransportElement = (HttpTransportElement)customBindingElement[typeof(HttpTransportElement)];
					if (httpTransportElement != null && httpTransportElement.TransferMode == TransferMode.Streamed)
					{
						result = true;
					}
				}
			}
			return result;
		});
	}
}
