using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x02000554 RID: 1364
	internal static class SmtpResponseSubCodePerfCounters
	{
		// Token: 0x06003EDD RID: 16093 RVA: 0x0010D744 File Offset: 0x0010B944
		public static void GetPerfCounterInfo(XElement element)
		{
			if (SmtpResponseSubCodePerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in SmtpResponseSubCodePerfCounters.AllCounters)
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

		// Token: 0x040022FD RID: 8957
		public const string CategoryName = "MSExchangeTransport Queuing Errors";

		// Token: 0x040022FE RID: 8958
		public static readonly ExPerformanceCounter Zero = new ExPerformanceCounter("MSExchangeTransport Queuing Errors", "X.0.X Other or Undefined Status", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040022FF RID: 8959
		public static readonly ExPerformanceCounter One = new ExPerformanceCounter("MSExchangeTransport Queuing Errors", "X.1.X Addressing Status", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04002300 RID: 8960
		public static readonly ExPerformanceCounter Two = new ExPerformanceCounter("MSExchangeTransport Queuing Errors", "X.2.X Mailbox Status", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04002301 RID: 8961
		public static readonly ExPerformanceCounter Three = new ExPerformanceCounter("MSExchangeTransport Queuing Errors", "X.3.X Mail System Status", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04002302 RID: 8962
		public static readonly ExPerformanceCounter Four = new ExPerformanceCounter("MSExchangeTransport Queuing Errors", "X.4.X Network and Routing Status", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04002303 RID: 8963
		public static readonly ExPerformanceCounter Five = new ExPerformanceCounter("MSExchangeTransport Queuing Errors", "X.5.X Mail Delivery Protocol Status", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04002304 RID: 8964
		public static readonly ExPerformanceCounter Six = new ExPerformanceCounter("MSExchangeTransport Queuing Errors", "X.6.X Message Content or Media Status", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04002305 RID: 8965
		public static readonly ExPerformanceCounter Seven = new ExPerformanceCounter("MSExchangeTransport Queuing Errors", "X.7.X Security or Policy Status", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04002306 RID: 8966
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			SmtpResponseSubCodePerfCounters.Zero,
			SmtpResponseSubCodePerfCounters.One,
			SmtpResponseSubCodePerfCounters.Two,
			SmtpResponseSubCodePerfCounters.Three,
			SmtpResponseSubCodePerfCounters.Four,
			SmtpResponseSubCodePerfCounters.Five,
			SmtpResponseSubCodePerfCounters.Six,
			SmtpResponseSubCodePerfCounters.Seven
		};
	}
}
