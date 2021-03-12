using System;
using System.Web;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200000E RID: 14
	internal static class NativeProxyLogHelper
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002E0C File Offset: 0x0000100C
		public static void PublishNativeProxyStatistics(HttpContextBase httpContext)
		{
			RequestLogger logger = RequestLogger.GetLogger(httpContext);
			string value = httpContext.Response.Headers[NativeProxyLogHelper.NativeProxyStatusHeaders.BackEndHttpStatus];
			if (!string.IsNullOrEmpty(value))
			{
				logger.LogField(LogKey.BackendStatus, value);
			}
			string text = httpContext.Response.Headers[NativeProxyLogHelper.NativeProxyStatusHeaders.ProxyErrorHResult];
			string arg = httpContext.Response.Headers[NativeProxyLogHelper.NativeProxyStatusHeaders.ProxyErrorLabel];
			string arg2 = httpContext.Response.Headers[NativeProxyLogHelper.NativeProxyStatusHeaders.ProxyErrorMessage];
			if (!string.IsNullOrEmpty(text))
			{
				logger.LogField(LogKey.ErrorCode, text);
				string value2 = string.Format("[{0}] [{1}] {2}", text, arg, arg2);
				logger.AppendErrorInfo("ProxyError", value2);
			}
			HttpWorkerRequest httpWorkerRequest = (HttpWorkerRequest)((IServiceProvider)httpContext).GetService(typeof(HttpWorkerRequest));
			bool hasWinHttpQuery = NativeProxyLogHelper.PublishTimestamps(httpWorkerRequest, logger);
			NativeProxyLogHelper.PublishLatencies(httpWorkerRequest, logger, hasWinHttpQuery);
			NativeProxyLogHelper.PublishCounters(httpWorkerRequest, logger);
			NativeProxyLogHelper.PublishStreamStats(httpWorkerRequest, logger);
			NativeProxyLogHelper.PublishGenericStats(httpWorkerRequest, logger, NativeProxyLogHelper.NativeProxyStatisticsVariables.RequestBufferSizeFootprints);
			NativeProxyLogHelper.PublishGenericStats(httpWorkerRequest, logger, NativeProxyLogHelper.NativeProxyStatisticsVariables.ResponseBufferSizeFootprints);
			if (NativeProxyLogHelper.LogBufferCopyStats.Value)
			{
				NativeProxyLogHelper.PublishGenericStats(httpWorkerRequest, logger, NativeProxyLogHelper.NativeProxyStatisticsVariables.BufferCopyStatsClientUpload);
				NativeProxyLogHelper.PublishGenericStats(httpWorkerRequest, logger, NativeProxyLogHelper.NativeProxyStatisticsVariables.BufferCopyStatsServerQuery);
				NativeProxyLogHelper.PublishGenericStats(httpWorkerRequest, logger, NativeProxyLogHelper.NativeProxyStatisticsVariables.BufferCopyStatsServerDownload);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F40 File Offset: 0x00001140
		private static bool PublishTimestamps(HttpWorkerRequest httpWorkerRequest, RequestLogger logger)
		{
			bool result = false;
			string serverVariable = httpWorkerRequest.GetServerVariable(NativeProxyLogHelper.NativeProxyStatisticsVariables.Timestamps);
			if (!string.IsNullOrEmpty(serverVariable))
			{
				logger.AppendGenericInfo(NativeProxyLogHelper.NativeProxyStatisticsVariables.Timestamps, serverVariable);
				long[] array = NativeProxyLogHelper.ConvertStatisticsDataArray(serverVariable);
				logger.LogField(LogKey.ModuleToHandlerSwitchingLatency, array[1] - array[0]);
				long num = array[14] - array[3];
				logger.LogField(LogKey.BackendProcessingLatency, num);
				long num2 = num;
				if (array[22] >= 0L)
				{
					num2 = array[22] - array[3];
				}
				logger.LogField(LogKey.ProxyTime, num2);
				result = (array[15] >= 0L);
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002FD4 File Offset: 0x000011D4
		private static void PublishLatencies(HttpWorkerRequest httpWorkerRequest, RequestLogger logger, bool hasWinHttpQuery)
		{
			string serverVariable = httpWorkerRequest.GetServerVariable(NativeProxyLogHelper.NativeProxyStatisticsVariables.Latencies);
			if (!string.IsNullOrEmpty(serverVariable))
			{
				logger.AppendGenericInfo(NativeProxyLogHelper.NativeProxyStatisticsVariables.Latencies, serverVariable);
				long[] array = NativeProxyLogHelper.ConvertStatisticsDataArray(serverVariable);
				logger.LogField(LogKey.ClientReqStreamLatency, array[0]);
				logger.LogField(LogKey.ClientRespStreamLatency, array[1]);
				logger.LogField(LogKey.BackendReqStreamLatency, array[2]);
				if (hasWinHttpQuery)
				{
					logger.LogField(LogKey.BackendRespStreamLatency, array[3]);
					return;
				}
				logger.LogField(LogKey.BackendRespStreamLatency, array[4]);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000305C File Offset: 0x0000125C
		private static void PublishCounters(HttpWorkerRequest httpWorkerRequest, RequestLogger logger)
		{
			string serverVariable = httpWorkerRequest.GetServerVariable(NativeProxyLogHelper.NativeProxyStatisticsVariables.Counters);
			if (!string.IsNullOrEmpty(serverVariable))
			{
				logger.AppendGenericInfo(NativeProxyLogHelper.NativeProxyStatisticsVariables.Counters, serverVariable);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000308C File Offset: 0x0000128C
		private static void PublishStreamStats(HttpWorkerRequest httpWorkerRequest, RequestLogger logger)
		{
			string serverVariable = httpWorkerRequest.GetServerVariable(NativeProxyLogHelper.NativeProxyStatisticsVariables.StreamStats);
			if (!string.IsNullOrEmpty(serverVariable))
			{
				logger.AppendGenericInfo(NativeProxyLogHelper.NativeProxyStatisticsVariables.StreamStats, serverVariable);
				long[] array = NativeProxyLogHelper.ConvertStatisticsDataArray(serverVariable);
				logger.LogField(LogKey.RequestBytes, array[0]);
				logger.LogField(LogKey.ResponseBytes, array[2]);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030E0 File Offset: 0x000012E0
		private static void PublishGenericStats(HttpWorkerRequest httpWorkerRequest, RequestLogger logger, string statsDataName)
		{
			string serverVariable = httpWorkerRequest.GetServerVariable(statsDataName);
			if (!string.IsNullOrEmpty(serverVariable))
			{
				logger.AppendGenericInfo(statsDataName, serverVariable);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003110 File Offset: 0x00001310
		private static long[] ConvertStatisticsDataArray(string dataLine)
		{
			string[] array = dataLine.Split(new char[]
			{
				'/'
			}, StringSplitOptions.RemoveEmptyEntries);
			return Array.ConvertAll<string, long>(array, (string x) => long.Parse(x));
		}

		// Token: 0x04000067 RID: 103
		public static readonly BoolAppSettingsEntry LogBufferCopyStats = new BoolAppSettingsEntry("NativeHttpProxy.LogBufferCopyStats", false, null);

		// Token: 0x0200000F RID: 15
		private enum ProxyEventTimestamps
		{
			// Token: 0x0400006A RID: 106
			Timestamp_IIS_MapHandler,
			// Token: 0x0400006B RID: 107
			Timestamp_IIS_ExecuteHandler,
			// Token: 0x0400006C RID: 108
			Timestamp_WinHttp_Pre_SendRequest,
			// Token: 0x0400006D RID: 109
			Timestamp_WinHttp_Post_SendRequest,
			// Token: 0x0400006E RID: 110
			Timestamp_IIS_BeginRead_First,
			// Token: 0x0400006F RID: 111
			Timestamp_IIS_BeginRead_Last,
			// Token: 0x04000070 RID: 112
			Timestamp_IIS_ReadComplete_First,
			// Token: 0x04000071 RID: 113
			Timestamp_IIS_ReadComplete_Last,
			// Token: 0x04000072 RID: 114
			Timestamp_WinHttp_SendRequestComplete,
			// Token: 0x04000073 RID: 115
			Timestamp_WinHttp_BeginWrite_First,
			// Token: 0x04000074 RID: 116
			Timestamp_WinHttp_BeginWrite_Last,
			// Token: 0x04000075 RID: 117
			Timestamp_WinHttp_WriteComplete_First,
			// Token: 0x04000076 RID: 118
			Timestamp_WinHttp_WriteComplete_Last,
			// Token: 0x04000077 RID: 119
			Timestamp_WinHttp_ReceiveResponse,
			// Token: 0x04000078 RID: 120
			Timestamp_WinHttp_HeadersAvailable,
			// Token: 0x04000079 RID: 121
			Timestamp_WinHttp_QueryData_First,
			// Token: 0x0400007A RID: 122
			Timestamp_WinHttp_QueryData_Last,
			// Token: 0x0400007B RID: 123
			Timestamp_WinHttp_DataAvailable_First,
			// Token: 0x0400007C RID: 124
			Timestamp_WinHttp_DataAvailable_Last,
			// Token: 0x0400007D RID: 125
			Timestamp_WinHttp_BeginRead_First,
			// Token: 0x0400007E RID: 126
			Timestamp_WinHttp_BeginRead_Last,
			// Token: 0x0400007F RID: 127
			Timestamp_WinHttp_ReadComplete_First,
			// Token: 0x04000080 RID: 128
			Timestamp_WinHttp_ReadComplete_Last,
			// Token: 0x04000081 RID: 129
			Timestamp_IIS_BeginWrite_First,
			// Token: 0x04000082 RID: 130
			Timestamp_IIS_BeginWrite_Last,
			// Token: 0x04000083 RID: 131
			Timestamp_IIS_WriteComplete_First,
			// Token: 0x04000084 RID: 132
			Timestamp_IIS_WriteComplete_Last,
			// Token: 0x04000085 RID: 133
			Timestamp_WinHttp_RequestError,
			// Token: 0x04000086 RID: 134
			Timestamp_Proxy_CompleteRequest,
			// Token: 0x04000087 RID: 135
			Timestamp_IIS_CompleteRequest
		}

		// Token: 0x02000010 RID: 16
		private enum ProxyStreamingLatencies
		{
			// Token: 0x04000089 RID: 137
			Latency_IIS_Read,
			// Token: 0x0400008A RID: 138
			Latency_IIS_Write,
			// Token: 0x0400008B RID: 139
			Latency_WinHttp_Write,
			// Token: 0x0400008C RID: 140
			Latency_WinHttp_Query,
			// Token: 0x0400008D RID: 141
			Latency_WinHttp_Read
		}

		// Token: 0x02000011 RID: 17
		private enum ProxyEventCounters
		{
			// Token: 0x0400008F RID: 143
			Count_IIS_Read,
			// Token: 0x04000090 RID: 144
			Count_IIS_Write,
			// Token: 0x04000091 RID: 145
			Count_WinHttp_Write,
			// Token: 0x04000092 RID: 146
			Count_WinHttp_DataAvailable,
			// Token: 0x04000093 RID: 147
			Count_WinHttp_Read
		}

		// Token: 0x02000012 RID: 18
		private enum ProxyStreamStats
		{
			// Token: 0x04000095 RID: 149
			Request_BytesLength,
			// Token: 0x04000096 RID: 150
			Request_Chunked,
			// Token: 0x04000097 RID: 151
			Response_BytesLength,
			// Token: 0x04000098 RID: 152
			Response_Chunked
		}

		// Token: 0x02000013 RID: 19
		internal static class NativeProxyStatusHeaders
		{
			// Token: 0x04000099 RID: 153
			public static readonly string BackEndHttpStatus = "X-BackEndHttpStatus";

			// Token: 0x0400009A RID: 154
			public static readonly string ProxyErrorHResult = "X-ProxyErrorHResult";

			// Token: 0x0400009B RID: 155
			public static readonly string ProxyErrorLabel = "X-ProxyErrorLabel";

			// Token: 0x0400009C RID: 156
			public static readonly string ProxyErrorMessage = "X-ProxyErrorMessage";
		}

		// Token: 0x02000014 RID: 20
		internal static class NativeProxyStatisticsVariables
		{
			// Token: 0x0400009D RID: 157
			public static readonly string Timestamps = "ProxyStats_Event_Timestamps";

			// Token: 0x0400009E RID: 158
			public static readonly string Counters = "ProxyStats_Event_Counters";

			// Token: 0x0400009F RID: 159
			public static readonly string Latencies = "ProxyStats_Streaming_Latencies";

			// Token: 0x040000A0 RID: 160
			public static readonly string StreamStats = "ProxyStats_Stream_Stats";

			// Token: 0x040000A1 RID: 161
			public static readonly string RequestBufferSizeFootprints = "ProxyStats_BufferSizeFootprints_Request";

			// Token: 0x040000A2 RID: 162
			public static readonly string ResponseBufferSizeFootprints = "ProxyStats_BufferSizeFootprints_Response";

			// Token: 0x040000A3 RID: 163
			public static readonly string BufferCopyStatsClientUpload = "ProxyStats_BufferCopyStats_Client_Upload";

			// Token: 0x040000A4 RID: 164
			public static readonly string BufferCopyStatsServerQuery = "ProxyStats_BufferCopyStats_Server_Query";

			// Token: 0x040000A5 RID: 165
			public static readonly string BufferCopyStatsServerDownload = "ProxyStats_BufferCopyStats_Server_Download";
		}
	}
}
