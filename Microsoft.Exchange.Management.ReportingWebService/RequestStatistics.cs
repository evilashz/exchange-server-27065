using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000003 RID: 3
	public class RequestStatistics
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000021CD File Offset: 0x000003CD
		public RequestStatistics()
		{
			this.statisticsDataPoints = new Dictionary<RequestStatistics.RequestStatItem, RequestStatistics.StatisticsDataPoint>();
			this.extendedStatisticsDataPoints = new Dictionary<string, string>();
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021EB File Offset: 0x000003EB
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000021F3 File Offset: 0x000003F3
		public string RequestUrl { get; set; }

		// Token: 0x06000008 RID: 8 RVA: 0x000021FC File Offset: 0x000003FC
		public static RequestStatistics CreateRequestRequestStatistics(HttpContext httpContext)
		{
			RequestStatistics requestStatistics = null;
			if (httpContext.Items != null)
			{
				requestStatistics = (httpContext.Items[RequestStatistics.RequestStatsKey] as RequestStatistics);
				if (requestStatistics == null)
				{
					requestStatistics = new RequestStatistics();
					requestStatistics.RequestUrl = httpContext.Request.RawUrl;
					httpContext.Items[RequestStatistics.RequestStatsKey] = requestStatistics;
				}
			}
			return requestStatistics;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002255 File Offset: 0x00000455
		public void AddStatisticsDataPoint(RequestStatistics.RequestStatItem name, DateTime startTime, DateTime endTime)
		{
			if (this.statisticsDataPoints.ContainsKey(name))
			{
				this.statisticsDataPoints[name] = new RequestStatistics.StatisticsDataPoint(name, startTime, endTime);
				return;
			}
			this.statisticsDataPoints.Add(name, new RequestStatistics.StatisticsDataPoint(name, startTime, endTime));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000228E File Offset: 0x0000048E
		public void AddExtendedStatisticsDataPoint(string name, string value)
		{
			if (this.extendedStatisticsDataPoints.ContainsKey(name))
			{
				this.extendedStatisticsDataPoints[name] = value;
				return;
			}
			this.extendedStatisticsDataPoints.Add(name, value);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022BC File Offset: 0x000004BC
		public string GetStatisticsDataPointResult(RequestStatistics.RequestStatItem name)
		{
			if (this.statisticsDataPoints.ContainsKey(name))
			{
				RequestStatistics.StatisticsDataPoint statisticsDataPoint = this.statisticsDataPoints[name];
				return string.Format("{0}({1})", statisticsDataPoint.EndTime.Subtract(statisticsDataPoint.StartTime).TotalMilliseconds, statisticsDataPoint.StartTime.ToString("hh:mm:ss.ffftt"));
			}
			return string.Empty;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002328 File Offset: 0x00000528
		public string GetExtendedStatisticsDataPointResult()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this.extendedStatisticsDataPoints.Keys)
			{
				stringBuilder.AppendFormat("{0}:{1};", text, this.extendedStatisticsDataPoints[text]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000001 RID: 1
		public static readonly string RequestStatsKey = "RequestStatistics";

		// Token: 0x04000002 RID: 2
		private Dictionary<RequestStatistics.RequestStatItem, RequestStatistics.StatisticsDataPoint> statisticsDataPoints;

		// Token: 0x04000003 RID: 3
		private Dictionary<string, string> extendedStatisticsDataPoints;

		// Token: 0x02000004 RID: 4
		public enum RequestStatItem
		{
			// Token: 0x04000006 RID: 6
			RequestResponseTime,
			// Token: 0x04000007 RID: 7
			HttpHandlerProcessRequestLatency,
			// Token: 0x04000008 RID: 8
			RbacPrincipalAcquireLatency,
			// Token: 0x04000009 RID: 9
			NewExchangeRunspaceConfigurationSettingsLatency,
			// Token: 0x0400000A RID: 10
			GetReportingSchemaLatency,
			// Token: 0x0400000B RID: 11
			NewRwsExchangeRunspaceConfigurationLatency,
			// Token: 0x0400000C RID: 12
			NewRbacPrincipalLatency,
			// Token: 0x0400000D RID: 13
			GetMetadataProviderLatency,
			// Token: 0x0400000E RID: 14
			GetQueryProviderLatency,
			// Token: 0x0400000F RID: 15
			CreateGenericTypeListForResults,
			// Token: 0x04000010 RID: 16
			CmdletResponseTime,
			// Token: 0x04000011 RID: 17
			InvokeCmdletLatency,
			// Token: 0x04000012 RID: 18
			InvokeCmdletExcludeRunspaceCreationLatency,
			// Token: 0x04000013 RID: 19
			InvokeCmdletExclusiveLatency,
			// Token: 0x04000014 RID: 20
			PowerShellCreateRunspaceLatency,
			// Token: 0x04000015 RID: 21
			ActivateReportingWebServiceHostLatency,
			// Token: 0x04000016 RID: 22
			DeactivateReportingWebServiceHostLatency,
			// Token: 0x04000017 RID: 23
			CreatePSHostLatency,
			// Token: 0x04000018 RID: 24
			InitializeRunspaceLatency,
			// Token: 0x04000019 RID: 25
			GetInitialSessionStateLatency,
			// Token: 0x0400001A RID: 26
			ConfigureRunspaceLatency,
			// Token: 0x0400001B RID: 27
			CreateRunspaceServerSettingsLatency
		}

		// Token: 0x02000005 RID: 5
		private struct StatisticsDataPoint
		{
			// Token: 0x0600000E RID: 14 RVA: 0x000023AC File Offset: 0x000005AC
			public StatisticsDataPoint(RequestStatistics.RequestStatItem name, DateTime startTime, DateTime endTime)
			{
				this.Name = name;
				this.StartTime = startTime;
				this.EndTime = endTime;
			}

			// Token: 0x0400001C RID: 28
			public RequestStatistics.RequestStatItem Name;

			// Token: 0x0400001D RID: 29
			public DateTime StartTime;

			// Token: 0x0400001E RID: 30
			public DateTime EndTime;
		}
	}
}
