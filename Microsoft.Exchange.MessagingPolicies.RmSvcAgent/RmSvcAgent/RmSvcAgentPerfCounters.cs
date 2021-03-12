using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.RmSvcAgent
{
	// Token: 0x0200000E RID: 14
	internal static class RmSvcAgentPerfCounters
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003B88 File Offset: 0x00001D88
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RmSvcAgentPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in RmSvcAgentPerfCounters.AllCounters)
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

		// Token: 0x04000063 RID: 99
		public const string CategoryName = "MSExchange RMS Agents";

		// Token: 0x04000064 RID: 100
		public static readonly ExPerformanceCounter CurrentActiveAgents = new ExPerformanceCounter("MSExchange RMS Agents", "Active RMS Licensing Agents", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000065 RID: 101
		private static readonly ExPerformanceCounter RateOfSuccessfulActiveRequests = new ExPerformanceCounter("MSExchange RMS Agents", "Active RMS Licensing Agents/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000066 RID: 102
		public static readonly ExPerformanceCounter TotalSuccessfulActiveRequests = new ExPerformanceCounter("MSExchange RMS Agents", "Total active RMS Licensing Agents", string.Empty, null, new ExPerformanceCounter[]
		{
			RmSvcAgentPerfCounters.RateOfSuccessfulActiveRequests
		});

		// Token: 0x04000067 RID: 103
		private static readonly ExPerformanceCounter RateOfUnsuccessfulActiveRequests = new ExPerformanceCounter("MSExchange RMS Agents", "RMS Licensing Agents Failed to Process/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000068 RID: 104
		public static readonly ExPerformanceCounter TotalUnsuccessfulActiveRequests = new ExPerformanceCounter("MSExchange RMS Agents", "Total RMS Licensing Agents Failed to Process", string.Empty, null, new ExPerformanceCounter[]
		{
			RmSvcAgentPerfCounters.RateOfUnsuccessfulActiveRequests
		});

		// Token: 0x04000069 RID: 105
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			RmSvcAgentPerfCounters.CurrentActiveAgents,
			RmSvcAgentPerfCounters.TotalSuccessfulActiveRequests,
			RmSvcAgentPerfCounters.TotalUnsuccessfulActiveRequests
		};
	}
}
