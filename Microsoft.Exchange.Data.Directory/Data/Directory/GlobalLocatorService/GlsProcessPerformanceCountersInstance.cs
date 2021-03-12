using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000A46 RID: 2630
	internal sealed class GlsProcessPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x0600785A RID: 30810 RVA: 0x0018DF2C File Offset: 0x0018C12C
		internal GlsProcessPerformanceCountersInstance(string instanceName, GlsProcessPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Global Locator Processes")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NinetyFifthPercentileLatency = new ExPerformanceCounter(base.CategoryName, "95th Percentile Overall Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NinetyFifthPercentileLatency, new ExPerformanceCounter[0]);
				list.Add(this.NinetyFifthPercentileLatency);
				this.NinetyFifthPercentileLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for 95th Percentile Overall Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NinetyFifthPercentileLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.NinetyFifthPercentileLatencyBase);
				this.NinetyNinthPercentileLatency = new ExPerformanceCounter(base.CategoryName, "99th Percentile Overall Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NinetyNinthPercentileLatency, new ExPerformanceCounter[0]);
				list.Add(this.NinetyNinthPercentileLatency);
				this.NinetyNinthPercentileLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for 99th Percentile Overall Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NinetyNinthPercentileLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.NinetyNinthPercentileLatencyBase);
				this.AverageOverallLatency = new ExPerformanceCounter(base.CategoryName, "Average Overall Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageOverallLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageOverallLatency);
				this.AverageOverallLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Overall Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageOverallLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageOverallLatencyBase);
				this.AverageReadLatency = new ExPerformanceCounter(base.CategoryName, "Average Read Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageReadLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageReadLatency);
				this.AverageReadLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Read Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageReadLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageReadLatencyBase);
				this.AverageWriteLatency = new ExPerformanceCounter(base.CategoryName, "Average Write Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteLatency, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteLatency);
				this.AverageWriteLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Write Latency", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AverageWriteLatencyBase, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteLatencyBase);
				this.SuccessfulCallsPerMinute = new ExPerformanceCounter(base.CategoryName, "Successful calls over last minute", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.SuccessfulCallsPerMinute, new ExPerformanceCounter[0]);
				list.Add(this.SuccessfulCallsPerMinute);
				this.FailedCallsPerMinute = new ExPerformanceCounter(base.CategoryName, "Failed calls over last minute", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.FailedCallsPerMinute, new ExPerformanceCounter[0]);
				list.Add(this.FailedCallsPerMinute);
				this.NotFoundCallsPerMinute = new ExPerformanceCounter(base.CategoryName, "Not Found calls over last minute", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.NotFoundCallsPerMinute, new ExPerformanceCounter[0]);
				list.Add(this.NotFoundCallsPerMinute);
				this.CacheHitsRatioPerMinute = new ExPerformanceCounter(base.CategoryName, "Percentage of GLS cache hits for last minute", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.CacheHitsRatioPerMinute, new ExPerformanceCounter[0]);
				list.Add(this.CacheHitsRatioPerMinute);
				this.AcceptedDomainLookupCacheHitsRatioPerMinute = new ExPerformanceCounter(base.CategoryName, "Percentage of domain Cache hits for last minute", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AcceptedDomainLookupCacheHitsRatioPerMinute, new ExPerformanceCounter[0]);
				list.Add(this.AcceptedDomainLookupCacheHitsRatioPerMinute);
				this.ExternalDirectoryOrganizationIdCacheHitsRatioPerMinute = new ExPerformanceCounter(base.CategoryName, "Percentage of ExternalDirectoryOrganizationId Cache hits for last minute", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.ExternalDirectoryOrganizationIdCacheHitsRatioPerMinute, new ExPerformanceCounter[0]);
				list.Add(this.ExternalDirectoryOrganizationIdCacheHitsRatioPerMinute);
				this.MSAUserNetIdCacheHitsRatioPerMinute = new ExPerformanceCounter(base.CategoryName, "Percentage of MSAUserNetID Cache hits for last minute", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.MSAUserNetIdCacheHitsRatioPerMinute, new ExPerformanceCounter[0]);
				list.Add(this.MSAUserNetIdCacheHitsRatioPerMinute);
				long num = this.NinetyFifthPercentileLatency.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600785B RID: 30811 RVA: 0x0018E370 File Offset: 0x0018C570
		internal GlsProcessPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchange Global Locator Processes")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.NinetyFifthPercentileLatency = new ExPerformanceCounter(base.CategoryName, "95th Percentile Overall Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NinetyFifthPercentileLatency);
				this.NinetyFifthPercentileLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for 95th Percentile Overall Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NinetyFifthPercentileLatencyBase);
				this.NinetyNinthPercentileLatency = new ExPerformanceCounter(base.CategoryName, "99th Percentile Overall Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NinetyNinthPercentileLatency);
				this.NinetyNinthPercentileLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for 99th Percentile Overall Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NinetyNinthPercentileLatencyBase);
				this.AverageOverallLatency = new ExPerformanceCounter(base.CategoryName, "Average Overall Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageOverallLatency);
				this.AverageOverallLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Overall Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageOverallLatencyBase);
				this.AverageReadLatency = new ExPerformanceCounter(base.CategoryName, "Average Read Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageReadLatency);
				this.AverageReadLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Read Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageReadLatencyBase);
				this.AverageWriteLatency = new ExPerformanceCounter(base.CategoryName, "Average Write Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteLatency);
				this.AverageWriteLatencyBase = new ExPerformanceCounter(base.CategoryName, "Base for Average Write Latency", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageWriteLatencyBase);
				this.SuccessfulCallsPerMinute = new ExPerformanceCounter(base.CategoryName, "Successful calls over last minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.SuccessfulCallsPerMinute);
				this.FailedCallsPerMinute = new ExPerformanceCounter(base.CategoryName, "Failed calls over last minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedCallsPerMinute);
				this.NotFoundCallsPerMinute = new ExPerformanceCounter(base.CategoryName, "Not Found calls over last minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NotFoundCallsPerMinute);
				this.CacheHitsRatioPerMinute = new ExPerformanceCounter(base.CategoryName, "Percentage of GLS cache hits for last minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.CacheHitsRatioPerMinute);
				this.AcceptedDomainLookupCacheHitsRatioPerMinute = new ExPerformanceCounter(base.CategoryName, "Percentage of domain Cache hits for last minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AcceptedDomainLookupCacheHitsRatioPerMinute);
				this.ExternalDirectoryOrganizationIdCacheHitsRatioPerMinute = new ExPerformanceCounter(base.CategoryName, "Percentage of ExternalDirectoryOrganizationId Cache hits for last minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExternalDirectoryOrganizationIdCacheHitsRatioPerMinute);
				this.MSAUserNetIdCacheHitsRatioPerMinute = new ExPerformanceCounter(base.CategoryName, "Percentage of MSAUserNetID Cache hits for last minute", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.MSAUserNetIdCacheHitsRatioPerMinute);
				long num = this.NinetyFifthPercentileLatency.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter in list)
					{
						exPerformanceCounter.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600785C RID: 30812 RVA: 0x0018E6F8 File Offset: 0x0018C8F8
		public override void GetPerfCounterDiagnosticsInfo(XElement topElement)
		{
			XElement xelement = null;
			foreach (ExPerformanceCounter exPerformanceCounter in this.counters)
			{
				try
				{
					if (xelement == null)
					{
						xelement = new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.InstanceName));
						topElement.Add(xelement);
					}
					xelement.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					topElement.Add(content);
				}
			}
		}

		// Token: 0x04004F3A RID: 20282
		public readonly ExPerformanceCounter NinetyFifthPercentileLatency;

		// Token: 0x04004F3B RID: 20283
		public readonly ExPerformanceCounter NinetyFifthPercentileLatencyBase;

		// Token: 0x04004F3C RID: 20284
		public readonly ExPerformanceCounter NinetyNinthPercentileLatency;

		// Token: 0x04004F3D RID: 20285
		public readonly ExPerformanceCounter NinetyNinthPercentileLatencyBase;

		// Token: 0x04004F3E RID: 20286
		public readonly ExPerformanceCounter AverageOverallLatency;

		// Token: 0x04004F3F RID: 20287
		public readonly ExPerformanceCounter AverageOverallLatencyBase;

		// Token: 0x04004F40 RID: 20288
		public readonly ExPerformanceCounter AverageReadLatency;

		// Token: 0x04004F41 RID: 20289
		public readonly ExPerformanceCounter AverageReadLatencyBase;

		// Token: 0x04004F42 RID: 20290
		public readonly ExPerformanceCounter AverageWriteLatency;

		// Token: 0x04004F43 RID: 20291
		public readonly ExPerformanceCounter AverageWriteLatencyBase;

		// Token: 0x04004F44 RID: 20292
		public readonly ExPerformanceCounter SuccessfulCallsPerMinute;

		// Token: 0x04004F45 RID: 20293
		public readonly ExPerformanceCounter FailedCallsPerMinute;

		// Token: 0x04004F46 RID: 20294
		public readonly ExPerformanceCounter NotFoundCallsPerMinute;

		// Token: 0x04004F47 RID: 20295
		public readonly ExPerformanceCounter CacheHitsRatioPerMinute;

		// Token: 0x04004F48 RID: 20296
		public readonly ExPerformanceCounter AcceptedDomainLookupCacheHitsRatioPerMinute;

		// Token: 0x04004F49 RID: 20297
		public readonly ExPerformanceCounter ExternalDirectoryOrganizationIdCacheHitsRatioPerMinute;

		// Token: 0x04004F4A RID: 20298
		public readonly ExPerformanceCounter MSAUserNetIdCacheHitsRatioPerMinute;
	}
}
