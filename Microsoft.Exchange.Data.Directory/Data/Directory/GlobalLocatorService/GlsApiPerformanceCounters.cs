using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000A44 RID: 2628
	internal static class GlsApiPerformanceCounters
	{
		// Token: 0x0600784C RID: 30796 RVA: 0x0018DAE8 File Offset: 0x0018BCE8
		public static void GetPerfCounterInfo(XElement element)
		{
			if (GlsApiPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in GlsApiPerformanceCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04004F24 RID: 20260
		public const string CategoryName = "MSExchange Global Locator APIs";

		// Token: 0x04004F25 RID: 20261
		public static readonly ExPerformanceCounter FindDomainAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "FindDomain Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F26 RID: 20262
		public static readonly ExPerformanceCounter FindDomainAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for FindDomain Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F27 RID: 20263
		public static readonly ExPerformanceCounter FindTenantAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "FindTenant Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F28 RID: 20264
		public static readonly ExPerformanceCounter FindTenantAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for FindTenant Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F29 RID: 20265
		public static readonly ExPerformanceCounter FindUserAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "FindUser Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F2A RID: 20266
		public static readonly ExPerformanceCounter FindUserAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for FindUser Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F2B RID: 20267
		public static readonly ExPerformanceCounter SaveDomainAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "SaveDomain Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F2C RID: 20268
		public static readonly ExPerformanceCounter SaveDomainAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for SaveDomain Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F2D RID: 20269
		public static readonly ExPerformanceCounter SaveTenantAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "SaveTenant Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F2E RID: 20270
		public static readonly ExPerformanceCounter SaveTenantAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for SaveTenant Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F2F RID: 20271
		public static readonly ExPerformanceCounter SaveUserAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "SaveUser Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F30 RID: 20272
		public static readonly ExPerformanceCounter SaveUserAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for SaveUser Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F31 RID: 20273
		public static readonly ExPerformanceCounter DeleteDomainAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "DeleteDomain Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F32 RID: 20274
		public static readonly ExPerformanceCounter DeleteDomainAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for DeleteDomain Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F33 RID: 20275
		public static readonly ExPerformanceCounter DeleteTenantAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "DeleteTenant Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F34 RID: 20276
		public static readonly ExPerformanceCounter DeleteTenantAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for DeleteTenant Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F35 RID: 20277
		public static readonly ExPerformanceCounter DeleteUserAverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator APIs", "DeleteUser Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F36 RID: 20278
		public static readonly ExPerformanceCounter DeleteUserAverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator APIs", "Base for DeleteUser Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F37 RID: 20279
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			GlsApiPerformanceCounters.FindDomainAverageOverallLatency,
			GlsApiPerformanceCounters.FindDomainAverageOverallLatencyBase,
			GlsApiPerformanceCounters.FindTenantAverageOverallLatency,
			GlsApiPerformanceCounters.FindTenantAverageOverallLatencyBase,
			GlsApiPerformanceCounters.FindUserAverageOverallLatency,
			GlsApiPerformanceCounters.FindUserAverageOverallLatencyBase,
			GlsApiPerformanceCounters.SaveDomainAverageOverallLatency,
			GlsApiPerformanceCounters.SaveDomainAverageOverallLatencyBase,
			GlsApiPerformanceCounters.SaveTenantAverageOverallLatency,
			GlsApiPerformanceCounters.SaveTenantAverageOverallLatencyBase,
			GlsApiPerformanceCounters.SaveUserAverageOverallLatency,
			GlsApiPerformanceCounters.SaveUserAverageOverallLatencyBase,
			GlsApiPerformanceCounters.DeleteDomainAverageOverallLatency,
			GlsApiPerformanceCounters.DeleteDomainAverageOverallLatencyBase,
			GlsApiPerformanceCounters.DeleteTenantAverageOverallLatency,
			GlsApiPerformanceCounters.DeleteTenantAverageOverallLatencyBase,
			GlsApiPerformanceCounters.DeleteUserAverageOverallLatency,
			GlsApiPerformanceCounters.DeleteUserAverageOverallLatencyBase
		};
	}
}
