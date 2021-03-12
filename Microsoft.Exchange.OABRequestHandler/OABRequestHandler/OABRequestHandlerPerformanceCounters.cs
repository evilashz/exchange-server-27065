using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OABRequestHandler
{
	// Token: 0x0200000D RID: 13
	internal static class OABRequestHandlerPerformanceCounters
	{
		// Token: 0x0600003E RID: 62 RVA: 0x000054E0 File Offset: 0x000036E0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (OABRequestHandlerPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in OABRequestHandlerPerformanceCounters.AllCounters)
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

		// Token: 0x04000092 RID: 146
		public const string CategoryName = "MSExchangeOABRequestHandler";

		// Token: 0x04000093 RID: 147
		public static readonly ExPerformanceCounter RequestHandlingAverageTime = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Request Handling Average Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000094 RID: 148
		public static readonly ExPerformanceCounter RequestHandlingAverageTimeBase = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Request Handling Average Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000095 RID: 149
		private static readonly ExPerformanceCounter AccessDeniedFailuresPerSecond = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Access Denied Failures/Sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000096 RID: 150
		public static readonly ExPerformanceCounter AccessDeniedFailures = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Access Denied Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			OABRequestHandlerPerformanceCounters.AccessDeniedFailuresPerSecond
		});

		// Token: 0x04000097 RID: 151
		private static readonly ExPerformanceCounter InvalidRequestFailuresPerSecond = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Invalid Request Failures/Sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000098 RID: 152
		public static readonly ExPerformanceCounter InvalidRequestFailures = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Invalid Request Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			OABRequestHandlerPerformanceCounters.InvalidRequestFailuresPerSecond
		});

		// Token: 0x04000099 RID: 153
		private static readonly ExPerformanceCounter FileNotAvailableFailuresPerSecond = new ExPerformanceCounter("MSExchangeOABRequestHandler", "File Not Available Failures/Sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009A RID: 154
		public static readonly ExPerformanceCounter FileNotAvailableFailures = new ExPerformanceCounter("MSExchangeOABRequestHandler", "File Not Available Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			OABRequestHandlerPerformanceCounters.FileNotAvailableFailuresPerSecond
		});

		// Token: 0x0400009B RID: 155
		public static readonly ExPerformanceCounter CurrentNumberRequestsInCache = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Current Number Requests In Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009C RID: 156
		private static readonly ExPerformanceCounter DirectoryFailuresPerSecond = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Directory Failures/Sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009D RID: 157
		public static readonly ExPerformanceCounter DirectoryFailures = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Directory Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			OABRequestHandlerPerformanceCounters.DirectoryFailuresPerSecond
		});

		// Token: 0x0400009E RID: 158
		private static readonly ExPerformanceCounter UnknownFailuresPerSecond = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Unknown Failures/Sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400009F RID: 159
		public static readonly ExPerformanceCounter UnknownFailures = new ExPerformanceCounter("MSExchangeOABRequestHandler", "Unknown Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			OABRequestHandlerPerformanceCounters.UnknownFailuresPerSecond
		});

		// Token: 0x040000A0 RID: 160
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			OABRequestHandlerPerformanceCounters.RequestHandlingAverageTime,
			OABRequestHandlerPerformanceCounters.RequestHandlingAverageTimeBase,
			OABRequestHandlerPerformanceCounters.AccessDeniedFailures,
			OABRequestHandlerPerformanceCounters.InvalidRequestFailures,
			OABRequestHandlerPerformanceCounters.FileNotAvailableFailures,
			OABRequestHandlerPerformanceCounters.CurrentNumberRequestsInCache,
			OABRequestHandlerPerformanceCounters.DirectoryFailures,
			OABRequestHandlerPerformanceCounters.UnknownFailures
		};
	}
}
