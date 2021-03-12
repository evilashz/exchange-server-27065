using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020000AB RID: 171
	internal static class PerformanceMonitor
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x000141E8 File Offset: 0x000123E8
		static PerformanceMonitor()
		{
			PerformanceMonitor.BuildGenericCounterMap();
			PerformanceMonitor.BuildCustomCounterMap();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001421C File Offset: 0x0001241C
		private static void BuildGenericCounterMap()
		{
			string[] names = Enum.GetNames(typeof(ResponseType));
			int num = names.Length;
			PerformanceMonitor.respTypeToRequestCountCounterMap = new Dictionary<ResponseType, ExPerformanceCounter>(num);
			PerformanceMonitor.respTypeToRequestSuccessCountCounterMap = new Dictionary<ResponseType, ExPerformanceCounter>(num);
			PerformanceMonitor.soapActionToLatencyCounterMap = new Dictionary<string, ExPerformanceCounter>(num * PerformanceMonitor.SchemaPatterns.Length);
			PerformanceMonitor.latencycounterToRunningAverageFloatMap = new Dictionary<string, RunningAverageFloat>(num + 2);
			PerformanceMonitor.missingCounters = new List<string>();
			foreach (string text in names)
			{
				ResponseType responseType = (ResponseType)Enum.Parse(typeof(ResponseType), text);
				string apiMethodName = text.Replace("ResponseMessage", string.Empty);
				PerformanceMonitor.AddGenericCounterForApiMethod(apiMethodName, responseType);
			}
			if (PerformanceMonitor.missingCounters.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("List of missing counters");
				stringBuilder.Append(Environment.NewLine);
				foreach (string value in PerformanceMonitor.missingCounters)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(Environment.NewLine);
				}
				string errorMessage = stringBuilder.ToString();
				PerformanceMonitor.LogFailure(errorMessage, true);
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00014360 File Offset: 0x00012560
		private static void AddGenericCounterForApiMethod(string apiMethodName, ResponseType responseType)
		{
			PerformanceMonitor.AddCounterToCollection<ResponseType>(apiMethodName, "{0} Requests", responseType, ref PerformanceMonitor.respTypeToRequestCountCounterMap);
			PerformanceMonitor.AddCounterToCollection<ResponseType>(apiMethodName, "{0} Successful Requests", responseType, ref PerformanceMonitor.respTypeToRequestSuccessCountCounterMap);
			foreach (string format in PerformanceMonitor.SchemaPatterns)
			{
				PerformanceMonitor.AddCounterToCollection<string>(apiMethodName, "{0} Average Response Time", string.Format(format, apiMethodName), ref PerformanceMonitor.soapActionToLatencyCounterMap);
			}
			PerformanceMonitor.latencycounterToRunningAverageFloatMap.Add(string.Format("{0} Average Response Time", apiMethodName), new RunningAverageFloat(25));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000143DC File Offset: 0x000125DC
		private static void AddCounterToCollection<T>(string apiMethodName, string counterTypePattern, T key, ref Dictionary<T, ExPerformanceCounter> targetCollection)
		{
			string text = string.Format(counterTypePattern, apiMethodName);
			ExPerformanceCounter counterFromAllCountersCollection = PerformanceMonitor.GetCounterFromAllCountersCollection(text);
			if (counterFromAllCountersCollection != null)
			{
				targetCollection.Add(key, counterFromAllCountersCollection);
				return;
			}
			PerformanceMonitor.missingCounters.Add(text);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00014410 File Offset: 0x00012610
		private static ExPerformanceCounter GetCounterFromAllCountersCollection(string counterName)
		{
			ExPerformanceCounter result = null;
			foreach (ExPerformanceCounter exPerformanceCounter in WsPerformanceCounters.AllCounters)
			{
				if (exPerformanceCounter.CounterName == counterName)
				{
					result = exPerformanceCounter;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001444C File Offset: 0x0001264C
		private static void BuildCustomCounterMap()
		{
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap = new Dictionary<ResponseType, ExPerformanceCounter>();
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.CreateFolderResponseMessage, WsPerformanceCounters.TotalFoldersCreated);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.CopyFolderResponseMessage, WsPerformanceCounters.TotalFoldersCopied);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.MoveFolderResponseMessage, WsPerformanceCounters.TotalFoldersMoved);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.UpdateFolderResponseMessage, WsPerformanceCounters.TotalFoldersUpdated);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.DeleteFolderResponseMessage, WsPerformanceCounters.TotalFoldersDeleted);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.SyncFolderHierarchyResponseMessage, WsPerformanceCounters.TotalFoldersSynced);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.GetFolderResponseMessage, WsPerformanceCounters.TotalFoldersRead);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.CreateItemResponseMessage, WsPerformanceCounters.TotalItemsCreated);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.UploadItemsResponseMessage, WsPerformanceCounters.TotalItemsCreated);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.CopyItemResponseMessage, WsPerformanceCounters.TotalItemsCopied);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.MoveItemResponseMessage, WsPerformanceCounters.TotalItemsMoved);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.UpdateItemResponseMessage, WsPerformanceCounters.TotalItemsUpdated);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.DeleteItemResponseMessage, WsPerformanceCounters.TotalItemsDeleted);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.SyncFolderItemsResponseMessage, WsPerformanceCounters.TotalItemsSynced);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.SendItemResponseMessage, WsPerformanceCounters.TotalItemsSent);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.GetItemResponseMessage, WsPerformanceCounters.TotalItemsRead);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.FindItemResponseMessage, WsPerformanceCounters.TotalItemsRead);
			PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.Add(ResponseType.ExportItemsResponseMessage, WsPerformanceCounters.TotalItemsRead);
			PerformanceMonitor.latencycounterToRunningAverageFloatMap.Add("Average Response Time", new RunningAverageFloat(25));
			PerformanceMonitor.latencycounterToRunningAverageFloatMap.Add("Proxy Average Response Time", new RunningAverageFloat(25));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000145BC File Offset: 0x000127BC
		public static void Initialize()
		{
			try
			{
				foreach (ExPerformanceCounter exPerformanceCounter in WsPerformanceCounters.AllCounters)
				{
					exPerformanceCounter.RawValue = 0L;
				}
				if (EWSSettings.IsWsPerformanceCountersEnabled)
				{
					foreach (ExPerformanceCounter exPerformanceCounter2 in WsDatacenterPerformanceCounters.AllCounters)
					{
						exPerformanceCounter2.RawValue = 0L;
					}
				}
				WsPerformanceCounters.PID.RawValue = (long)Process.GetCurrentProcess().Id;
				PerformanceMonitor.performanceCountersInitialized = true;
			}
			catch (InvalidOperationException exception)
			{
				PerformanceMonitor.performanceCountersInitialized = false;
				ServiceDiagnostics.LogExceptionWithTrace(ServicesEventLogConstants.Tuple_InitializePerformanceCountersFailed, null, ExTraceGlobals.PerformanceMonitorTracer, null, "Failed to initialize performance counters. Error: {0}.", exception);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001466C File Offset: 0x0001286C
		public static bool PerformanceCountersEnabled
		{
			get
			{
				return PerformanceMonitor.performanceCountersInitialized;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00014674 File Offset: 0x00012874
		public static void UpdateResponseTimePerformanceCounter(long latency)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					PerformanceMonitor.UpdateMovingAveragePerformanceCounter(WsPerformanceCounters.AverageResponseTime, latency);
					WsPerformanceCounters.TotalRequests.Increment();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update response time performance counter. Error: {0}.", arg);
				}
				PerformanceMonitor.UpdateResponseTimePerformanceCounterForSoapAction(latency);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000146CC File Offset: 0x000128CC
		public static void UpdateProxyResponseTimePerformanceCounter(long newValue)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					PerformanceMonitor.UpdateMovingAveragePerformanceCounter(WsPerformanceCounters.ProxyAverageResponseTime, newValue);
					WsPerformanceCounters.TotalProxyRequests.Increment();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update proxy response time performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00014720 File Offset: 0x00012920
		public static void UpdatePushStatusCounter(bool success)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					ExPerformanceCounter exPerformanceCounter = success ? WsPerformanceCounters.TotalPushNotificationSuccesses : WsPerformanceCounters.TotalPushNotificationFailures;
					exPerformanceCounter.Increment();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update push status performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00014774 File Offset: 0x00012974
		public static void UpdateUnsubscribeCounter()
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					WsPerformanceCounters.TotalUnsubscribeRequests.Increment();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update unsubscribe performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000147BC File Offset: 0x000129BC
		public static void UpdateFailedSubscriptionCounter()
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					ExPerformanceCounter totalFailedSubscriptions = WsPerformanceCounters.TotalFailedSubscriptions;
					totalFailedSubscriptions.Increment();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update failed subscription performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00014804 File Offset: 0x00012A04
		public static void UpdateStreamedEventsCounter(long eventCount)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					ExPerformanceCounter totalStreamedEvents = WsPerformanceCounters.TotalStreamedEvents;
					totalStreamedEvents.IncrementBy(eventCount);
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update streamedEvents performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00014850 File Offset: 0x00012A50
		public static void UpdateActiveSubscriptionsCounter(long count)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					WsPerformanceCounters.ActiveSubscriptions.RawValue = count;
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update active subscription performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00014898 File Offset: 0x00012A98
		public static void UpdateActiveStreamingConnectionsCounter(long count)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					WsPerformanceCounters.ActiveStreamingConnections.RawValue = count;
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update active streaming subscription connection performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000148E0 File Offset: 0x00012AE0
		public static void UpdateTotalProxyRequestBytesCount(long byteCount)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					WsPerformanceCounters.TotalProxyRequestBytes.IncrementBy(byteCount);
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update TotalProxyRequestBytes performance counter.  Error {0}", arg);
				}
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00014928 File Offset: 0x00012B28
		public static void UpdateTotalProxyResponseBytesCount(long byteCount)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					WsPerformanceCounters.TotalProxyResponseBytes.IncrementBy(byteCount);
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update TotalProxyResponseBytes performance counter.  Error {0}", arg);
				}
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00014970 File Offset: 0x00012B70
		public static void UpdateTotalProxyFailoversCount()
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					WsPerformanceCounters.TotalProxyFailovers.Increment();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update TotalProxyFailovers performance counter.  Error {0}", arg);
				}
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000149B8 File Offset: 0x00012BB8
		public static void UpdateTotalRequestRejectionsCount()
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					WsPerformanceCounters.TotalRequestRejections.Increment();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update TotalRequestRejections performance counter.  Error {0}", arg);
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00014A00 File Offset: 0x00012C00
		public static void UpdateTotalCompletedRequestsCount()
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					WsPerformanceCounters.TotalCompletedRequests.Increment();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update TotalCompletedRequests performance counter.  Error {0}", arg);
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00014A55 File Offset: 0x00012C55
		public static void UpdateTotalRequestsReceivedWithPartnerToken()
		{
			PerformanceMonitor.SafeUpdatePerfCounter("TotalRequestsReceivedWithPartnerToken", delegate
			{
				WsDatacenterPerformanceCounters.TotalRequestsReceivedWithPartnerToken.Increment();
			});
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00014A8B File Offset: 0x00012C8B
		public static void UpdateTotalUnauthorizedRequestsReceivedWithPartnerToken()
		{
			PerformanceMonitor.SafeUpdatePerfCounter("TotalUnauthorizedRequestsReceivedWithPartnerToken", delegate
			{
				WsDatacenterPerformanceCounters.TotalUnauthorizedRequestsReceivedWithPartnerToken.Increment();
			});
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00014AD0 File Offset: 0x00012CD0
		public static void UpdatePartnerTokenCacheEntries(int value)
		{
			PerformanceMonitor.SafeUpdatePerfCounter("PartnerTokenCacheEntries", delegate
			{
				WsDatacenterPerformanceCounters.PartnerTokenCacheEntries.RawValue = (long)value;
			});
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00014B00 File Offset: 0x00012D00
		private static void SafeUpdatePerfCounter(string counterName, Action updateAction)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					updateAction();
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<string, InvalidOperationException>(0L, "Failed to update {0} performance counter. Error {1}", counterName, arg);
				}
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00014B44 File Offset: 0x00012D44
		public static void UpdateResponseCounters(IExchangeWebMethodResponse response, int objectsChanged)
		{
			if (PerformanceMonitor.PerformanceCountersEnabled)
			{
				try
				{
					ResponseType responseType = response.GetResponseType();
					ExPerformanceCounter exPerformanceCounter;
					if (PerformanceMonitor.respTypeToRequestCountCounterMap.TryGetValue(responseType, out exPerformanceCounter))
					{
						exPerformanceCounter.Increment();
					}
					else
					{
						PerformanceMonitor.ReportAbsentCounter(responseType, "Request Count", true);
					}
					if (response.GetErrorCodeToLog() == ResponseCodeType.NoError)
					{
						ExPerformanceCounter exPerformanceCounter2;
						if (PerformanceMonitor.respTypeToRequestSuccessCountCounterMap.TryGetValue(responseType, out exPerformanceCounter2))
						{
							exPerformanceCounter2.Increment();
						}
						else
						{
							PerformanceMonitor.ReportAbsentCounter(responseType, "Request Success Count", true);
						}
					}
					ExPerformanceCounter exPerformanceCounter3;
					if (PerformanceMonitor.respTypeToRequestObjectsChangedCounterMap.TryGetValue(responseType, out exPerformanceCounter3))
					{
						if (objectsChanged > 0)
						{
							exPerformanceCounter3.IncrementBy((long)objectsChanged);
						}
					}
					else
					{
						PerformanceMonitor.ReportAbsentCounter(responseType, "Request Count", false);
					}
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update response performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00014C08 File Offset: 0x00012E08
		private static void UpdateResponseTimePerformanceCounterForSoapAction(long latency)
		{
			string soapAction = PerformanceMonitor.GetSoapAction();
			if (soapAction != null)
			{
				try
				{
					ExPerformanceCounter exPerformanceCounter;
					if (PerformanceMonitor.soapActionToLatencyCounterMap.TryGetValue(soapAction, out exPerformanceCounter))
					{
						exPerformanceCounter.Increment();
					}
					else
					{
						PerformanceMonitor.ReportAbsentCounter(soapAction, "Latency Counter", true);
					}
					PerformanceMonitor.UpdateMovingAveragePerformanceCounter(exPerformanceCounter, latency);
				}
				catch (InvalidOperationException arg)
				{
					ExTraceGlobals.PerformanceMonitorTracer.TraceError<InvalidOperationException>(0L, "Failed to update response time performance counter. Error: {0}.", arg);
				}
			}
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00014C70 File Offset: 0x00012E70
		private static void ReportAbsentCounter(ResponseType responseType, string counterType, bool failRequest)
		{
			string errorMessage = string.Format("Performance counter not found for response type : {0}", responseType);
			PerformanceMonitor.LogFailure(errorMessage, failRequest);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00014C98 File Offset: 0x00012E98
		private static void ReportAbsentCounter(string soapAction, string counterType, bool failRequest)
		{
			string errorMessage = string.Format("Performance counter not found for soap action : {0}", soapAction);
			PerformanceMonitor.LogFailure(errorMessage, failRequest);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00014CB8 File Offset: 0x00012EB8
		private static void ReportAbsentRunningAverageFloatInstance(string counterName)
		{
			string errorMessage = string.Format("RunningAverageFloat instance not found for counter : {0}", counterName);
			PerformanceMonitor.LogFailure(errorMessage, true);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00014CD8 File Offset: 0x00012ED8
		private static void LogFailure(string errorMessage, bool failRequest)
		{
			ExTraceGlobals.PerformanceMonitorTracer.TraceError(0L, errorMessage);
			if (failRequest)
			{
				throw new ArgumentException(errorMessage);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00014CF4 File Offset: 0x00012EF4
		private static string GetSoapAction()
		{
			CallContext callContext = HttpContext.Current.Items["CallContext"] as CallContext;
			if (callContext != null)
			{
				return callContext.SoapAction;
			}
			return null;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00014D28 File Offset: 0x00012F28
		private static void UpdateMovingAveragePerformanceCounter(ExPerformanceCounter performanceCounter, long newValue)
		{
			string counterName = performanceCounter.CounterName;
			lock (performanceCounter)
			{
				if (PerformanceMonitor.latencycounterToRunningAverageFloatMap.ContainsKey(counterName))
				{
					PerformanceMonitor.latencycounterToRunningAverageFloatMap[counterName].Update((float)newValue);
					performanceCounter.RawValue = (long)PerformanceMonitor.latencycounterToRunningAverageFloatMap[counterName].Value;
				}
				else
				{
					PerformanceMonitor.ReportAbsentRunningAverageFloatInstance(counterName);
				}
			}
		}

		// Token: 0x04000631 RID: 1585
		private const int LatencyCounterNumberOfSamples = 25;

		// Token: 0x04000632 RID: 1586
		private const string RequestCountCounterPattern = "{0} Requests";

		// Token: 0x04000633 RID: 1587
		private const string RequestSuccessCountCounterPattern = "{0} Successful Requests";

		// Token: 0x04000634 RID: 1588
		private const string LatencyCounterPattern = "{0} Average Response Time";

		// Token: 0x04000635 RID: 1589
		private static readonly string[] SchemaPatterns = new string[]
		{
			"http://schemas.microsoft.com/exchange/services/2006/messages/{0}",
			"http://schemas.microsoft.com/exchange/services/2006a/messages/{0}"
		};

		// Token: 0x04000636 RID: 1590
		private static bool performanceCountersInitialized;

		// Token: 0x04000637 RID: 1591
		private static Dictionary<ResponseType, ExPerformanceCounter> respTypeToRequestObjectsChangedCounterMap;

		// Token: 0x04000638 RID: 1592
		private static Dictionary<ResponseType, ExPerformanceCounter> respTypeToRequestCountCounterMap;

		// Token: 0x04000639 RID: 1593
		private static Dictionary<ResponseType, ExPerformanceCounter> respTypeToRequestSuccessCountCounterMap;

		// Token: 0x0400063A RID: 1594
		private static Dictionary<string, ExPerformanceCounter> soapActionToLatencyCounterMap;

		// Token: 0x0400063B RID: 1595
		private static Dictionary<string, RunningAverageFloat> latencycounterToRunningAverageFloatMap;

		// Token: 0x0400063C RID: 1596
		private static List<string> missingCounters;
	}
}
