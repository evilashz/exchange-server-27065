using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000DAE RID: 3502
	internal static class HttpHeaderProcessor
	{
		// Token: 0x060058FA RID: 22778 RVA: 0x00115798 File Offset: 0x00113998
		internal static List<ClientStatistics> ProcessClientStatisticsHttpHeader(HttpContext httpContext)
		{
			string text = httpContext.Request.Headers[HttpHeaderProcessor.RequestStatisticsKey];
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string[] array = text.Split(HttpHeaderProcessor.RequestDelimiter, StringSplitOptions.RemoveEmptyEntries);
			if (array == null || array.Length <= 0 || array.Length > 200)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[ProcessClientStatisticsHttpHeader] No requests received.");
				return null;
			}
			int num = (array.Length > 200) ? 200 : array.Length;
			List<ClientStatistics> list = new List<ClientStatistics>(num);
			for (int i = 0; i < num; i++)
			{
				string request = array[i];
				ClientStatistics clientStatistics = HttpHeaderProcessor.ParseRequestEntries(request, i);
				if (clientStatistics != null && clientStatistics.IsValid())
				{
					list.Add(clientStatistics);
				}
				else
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[ProcessClientStatisticsHttpHeader] Client request information is invalid, skipping.");
				}
			}
			return list;
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x00115860 File Offset: 0x00113A60
		private static ClientStatistics ParseRequestEntries(string request, int requestIndex)
		{
			string[] array = request.Split(HttpHeaderProcessor.EntryDelimiter, StringSplitOptions.RemoveEmptyEntries);
			if (array == null || array.Length <= 0)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[ProcessClientStatisticsHttpHeader] Got an empty entry");
				return null;
			}
			ClientStatistics clientStatistics = new ClientStatistics();
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(HttpHeaderProcessor.ValueDelimiter, StringSplitOptions.RemoveEmptyEntries);
				if (array2 == null || array2.Length != 2)
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[ProcessClientStatisticsHttpHeader] Invalid Part");
					return null;
				}
				if (string.IsNullOrEmpty(array2[0]) || string.IsNullOrEmpty(array2[1]))
				{
					ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[ProcessClientStatisticsHttpHeader] Got empty parts");
					return null;
				}
				string a;
				if ((a = array2[0].Trim()) != null)
				{
					if (!(a == "MessageId"))
					{
						if (!(a == "ResponseTime"))
						{
							if (!(a == "RequestTime"))
							{
								if (!(a == "ResponseSize"))
								{
									if (!(a == "HttpResponseCode"))
									{
										if (a == "ErrorCode")
										{
											int num2;
											if (!int.TryParse(array2[1], out num2))
											{
												ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[ProcessClientStatisticsHttpHeader] Unable to parse ErrorCode part");
												return null;
											}
											if (clientStatistics.ErrorCode == null)
											{
												clientStatistics.ErrorCode = new int[20];
											}
											if (num < 20)
											{
												clientStatistics.ErrorCode[num] = num2;
												num++;
											}
										}
									}
									else
									{
										int httpResponseCode;
										if (!HttpHeaderProcessor.ValidateAndParseClientRequestHeaderPart("HttpResponseCode", clientStatistics.HttpResponseCode, array2[1], out httpResponseCode))
										{
											return null;
										}
										clientStatistics.HttpResponseCode = httpResponseCode;
									}
								}
								else
								{
									int responseSize;
									if (!HttpHeaderProcessor.ValidateAndParseClientRequestHeaderPart("ResponseSize", clientStatistics.ResponseSize, array2[1], out responseSize))
									{
										return null;
									}
									clientStatistics.ResponseSize = responseSize;
								}
							}
							else
							{
								DateTime requestTime = clientStatistics.RequestTime;
								DateTime requestTime2;
								if (!DateTime.TryParse(array2[1], out requestTime2))
								{
									return null;
								}
								clientStatistics.RequestTime = requestTime2;
							}
						}
						else
						{
							int responseTime;
							if (!HttpHeaderProcessor.ValidateAndParseClientRequestHeaderPart("ResponseTime", clientStatistics.ResponseTime, array2[1], out responseTime))
							{
								return null;
							}
							clientStatistics.ResponseTime = responseTime;
						}
					}
					else
					{
						if (!string.IsNullOrEmpty(clientStatistics.MessageId))
						{
							ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[ProcessClientStatisticsHttpHeader] Got a duplicate MessageId");
							return null;
						}
						clientStatistics.MessageId = array2[1].Trim();
					}
				}
				RequestDetailsLogger.Current.AppendClientStatistic(array2[0] + '_' + requestIndex, array2[1]);
			}
			return clientStatistics;
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x00115AB2 File Offset: 0x00113CB2
		private static bool ValidateAndParseClientRequestHeaderPart(string partKey, int initialPartValue, string part, out int parsedPartValue)
		{
			parsedPartValue = -1;
			if (initialPartValue >= 0)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceDebug(0L, "[ProcessClientStatisticsHttpHeader] Got a duplicate " + partKey);
				return false;
			}
			return int.TryParse(part, out parsedPartValue);
		}

		// Token: 0x04003153 RID: 12627
		private const string MessageId = "MessageId";

		// Token: 0x04003154 RID: 12628
		private const string RequestTime = "RequestTime";

		// Token: 0x04003155 RID: 12629
		private const string ResponseTime = "ResponseTime";

		// Token: 0x04003156 RID: 12630
		private const string ResponseSize = "ResponseSize";

		// Token: 0x04003157 RID: 12631
		private const string HttpResponseCode = "HttpResponseCode";

		// Token: 0x04003158 RID: 12632
		private const string PartialErrorCode = "ErrorCode";

		// Token: 0x04003159 RID: 12633
		private static readonly string RequestStatisticsKey = "X-ClientStatistics";

		// Token: 0x0400315A RID: 12634
		private static readonly char[] RequestDelimiter = new char[]
		{
			';'
		};

		// Token: 0x0400315B RID: 12635
		private static readonly char[] EntryDelimiter = new char[]
		{
			','
		};

		// Token: 0x0400315C RID: 12636
		private static readonly char[] ValueDelimiter = new char[]
		{
			'='
		};
	}
}
