using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000106 RID: 262
	internal class QueryLogData
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x0001EBE8 File Offset: 0x0001CDE8
		public QueryLogData()
		{
			this.succeededQueries = new Dictionary<string, List<FreeBusyQuery>>();
			this.failedQueries = new Dictionary<string, List<FreeBusyQuery>>();
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001EC08 File Offset: 0x0001CE08
		public void Add(FreeBusyQuery query, bool isFailed)
		{
			string text = query.Target;
			text = ((text == null) ? "NULL" : text);
			Dictionary<string, List<FreeBusyQuery>> dictionary = isFailed ? this.failedQueries : this.succeededQueries;
			List<FreeBusyQuery> list;
			if (dictionary.TryGetValue(text, out list))
			{
				list.Add(query);
				return;
			}
			list = new List<FreeBusyQuery>();
			list.Add(query);
			dictionary[text] = list;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001EC64 File Offset: 0x0001CE64
		public string GetSucceededQueryLogData()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			foreach (KeyValuePair<string, List<FreeBusyQuery>> keyValuePair in this.succeededQueries)
			{
				string key = keyValuePair.Key;
				List<FreeBusyQuery> value = keyValuePair.Value;
				stringBuilder.AppendFormat("{0}-{1}|", "Target", key);
				foreach (KeyValuePair<string, string> keyValuePair2 in value[0].LogData)
				{
					stringBuilder.AppendFormat("{0}-{1}|", keyValuePair2.Key, keyValuePair2.Value);
				}
				string text = (value[0].Type != null) ? value[0].Type.Value.ToString() : "NULL";
				stringBuilder.AppendFormat("{0}-{1}|{2}-{3}|", new object[]
				{
					"QT",
					text,
					"Cnt",
					value.Count
				});
				foreach (FreeBusyQuery query in value)
				{
					string intraForestLatency = QueryLogData.GetIntraForestLatency(query);
					if (!string.IsNullOrEmpty(intraForestLatency))
					{
						stringBuilder.AppendFormat("[{0}]", intraForestLatency);
					}
				}
				stringBuilder.Append(";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001EE4C File Offset: 0x0001D04C
		public List<string> GetFailedQueryLogData()
		{
			List<string> list = new List<string>(this.failedQueries.Count);
			foreach (KeyValuePair<string, List<FreeBusyQuery>> keyValuePair in this.failedQueries)
			{
				string key = keyValuePair.Key;
				List<FreeBusyQuery> value = keyValuePair.Value;
				StringBuilder stringBuilder = new StringBuilder(1024);
				stringBuilder.AppendFormat("{0}-{1}|", "Target", key);
				foreach (KeyValuePair<string, string> keyValuePair2 in value[0].LogData)
				{
					stringBuilder.AppendFormat("{0}-{1}|", keyValuePair2.Key, keyValuePair2.Value);
				}
				if (value[0].Type != null)
				{
					stringBuilder.AppendFormat("{0}-{1}|{2}-{3}|", new object[]
					{
						"QT",
						value[0].Type.Value,
						"Cnt",
						value.Count
					});
				}
				else
				{
					stringBuilder.AppendFormat("{0}-{1}|", "Cnt", value.Count);
				}
				HashSet<Type> hashSet = new HashSet<Type>();
				foreach (FreeBusyQuery freeBusyQuery in value)
				{
					Exception exceptionInfo = freeBusyQuery.Result.ExceptionInfo;
					Exception ex = exceptionInfo;
					WebException ex2 = exceptionInfo as WebException;
					while (ex.InnerException != null)
					{
						ex = ex.InnerException;
						if (ex2 == null)
						{
							ex2 = (ex as WebException);
						}
					}
					if (!hashSet.Contains(ex.GetType()))
					{
						stringBuilder.Append("[");
						AvailabilityException ex3 = exceptionInfo as AvailabilityException;
						if (ex3 != null)
						{
							stringBuilder.AppendFormat("{0}-{1}|", "EXPS", ex3.ServerName);
							if (!string.IsNullOrEmpty(ex3.LocationIdentifier))
							{
								stringBuilder.AppendFormat("{0}-{1}|", "LID", ex3.LocationIdentifier);
							}
						}
						if (freeBusyQuery.Email != null)
						{
							stringBuilder.AppendFormat("{0}-{1}|", "AT", freeBusyQuery.Email.Address);
						}
						stringBuilder.AppendFormat("{0}-{1}|", "EXPM", exceptionInfo.GetType().Name + ":" + exceptionInfo.Message);
						if (exceptionInfo.GetType() != ex.GetType())
						{
							stringBuilder.AppendFormat("{0}-{1}|", "IEXPM", ex.GetType().Name + ":" + ex.Message);
						}
						hashSet.Add(ex.GetType());
						if (ex2 != null)
						{
							HttpWebResponse httpWebResponse = ex2.Response as HttpWebResponse;
							if (httpWebResponse != null)
							{
								string text = httpWebResponse.Headers.Get(MSDiagnosticsHeader.HeaderName);
								if (text != null)
								{
									stringBuilder.AppendFormat("{0}-{1}|", "DI", text);
								}
								string text2 = httpWebResponse.Headers.Get("request-id");
								if (text2 != null)
								{
									string text3 = httpWebResponse.Headers.Get(WellKnownHeader.XFEServer);
									string text4 = httpWebResponse.Headers.Get(WellKnownHeader.XCalculatedBETarget);
									stringBuilder.AppendFormat("{0}-{1}|{2}-{3}|{4}-{5}", new object[]
									{
										"RID",
										text2,
										"TFE",
										text3,
										"TBE",
										text4
									});
								}
							}
						}
						stringBuilder.Append(QueryLogData.GetIntraForestLatency(freeBusyQuery));
						stringBuilder.Append(']');
					}
				}
				list.Add(stringBuilder.ToString());
			}
			return list;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001F274 File Offset: 0x0001D474
		private static string GetIntraForestLatency(BaseQuery query)
		{
			string text = string.Empty;
			if (query.Type == RequestType.Local || query.Type == RequestType.IntraSite || query.Type == RequestType.CrossSite)
			{
				if (query.ExchangePrincipalLatency > 0L)
				{
					text = string.Format("{0}-{1}|", "EPL", query.ExchangePrincipalLatency);
				}
				if (query.ServiceDiscoveryLatency > 0L)
				{
					text = string.Format("{0}{1}-{2}|", text, "SDL", query.ServiceDiscoveryLatency);
				}
			}
			return text;
		}

		// Token: 0x0400042A RID: 1066
		public const string EmptyTarget = "NULL";

		// Token: 0x0400042B RID: 1067
		public const string AutoDInfo = "AutoDInfo";

		// Token: 0x0400042C RID: 1068
		private const string Target = "Target";

		// Token: 0x0400042D RID: 1069
		private const string Count = "Cnt";

		// Token: 0x0400042E RID: 1070
		private const string Attendee = "AT";

		// Token: 0x0400042F RID: 1071
		private const string QueryType = "QT";

		// Token: 0x04000430 RID: 1072
		private const string ExchangePrincipalLatency = "EPL";

		// Token: 0x04000431 RID: 1073
		private const string ServiceDiscoveryLatency = "SDL";

		// Token: 0x04000432 RID: 1074
		private const string ExceptionMessage = "EXPM";

		// Token: 0x04000433 RID: 1075
		private const string InnerExceptionMessage = "IEXPM";

		// Token: 0x04000434 RID: 1076
		private const string ExceptionServer = "EXPS";

		// Token: 0x04000435 RID: 1077
		private const string LocationIdentifierKey = "LID";

		// Token: 0x04000436 RID: 1078
		private const string RequestId = "RID";

		// Token: 0x04000437 RID: 1079
		private const string TargetBE = "TBE";

		// Token: 0x04000438 RID: 1080
		private const string TargetFE = "TFE";

		// Token: 0x04000439 RID: 1081
		private const string DiagnosticInfo = "DI";

		// Token: 0x0400043A RID: 1082
		private const string LogFormat = "{0}-{1}|";

		// Token: 0x0400043B RID: 1083
		private const int DefaultLogCapacity = 1024;

		// Token: 0x0400043C RID: 1084
		private Dictionary<string, List<FreeBusyQuery>> succeededQueries;

		// Token: 0x0400043D RID: 1085
		private Dictionary<string, List<FreeBusyQuery>> failedQueries;
	}
}
