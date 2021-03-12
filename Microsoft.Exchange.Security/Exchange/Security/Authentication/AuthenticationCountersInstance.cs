using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200000C RID: 12
	internal sealed class AuthenticationCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000058E4 File Offset: 0x00003AE4
		internal AuthenticationCountersInstance(string instanceName, AuthenticationCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Authentication")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.OutstandingAuthenticationRequests = new ExPerformanceCounter(base.CategoryName, "Outstanding Authentication Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingAuthenticationRequests);
				this.TotalAuthenticationRequests = new ExPerformanceCounter(base.CategoryName, "Total Authentication Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAuthenticationRequests);
				this.RejectedAuthenticationRequests = new ExPerformanceCounter(base.CategoryName, "Rejected Authentication Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RejectedAuthenticationRequests);
				this.AuthenticationLatency = new ExPerformanceCounter(base.CategoryName, "Authentication Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AuthenticationLatency);
				long num = this.OutstandingAuthenticationRequests.RawValue;
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

		// Token: 0x06000038 RID: 56 RVA: 0x00005A20 File Offset: 0x00003C20
		internal AuthenticationCountersInstance(string instanceName) : base(instanceName, "MSExchange Authentication")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.OutstandingAuthenticationRequests = new ExPerformanceCounter(base.CategoryName, "Outstanding Authentication Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingAuthenticationRequests);
				this.TotalAuthenticationRequests = new ExPerformanceCounter(base.CategoryName, "Total Authentication Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalAuthenticationRequests);
				this.RejectedAuthenticationRequests = new ExPerformanceCounter(base.CategoryName, "Rejected Authentication Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.RejectedAuthenticationRequests);
				this.AuthenticationLatency = new ExPerformanceCounter(base.CategoryName, "Authentication Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AuthenticationLatency);
				long num = this.OutstandingAuthenticationRequests.RawValue;
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

		// Token: 0x06000039 RID: 57 RVA: 0x00005B5C File Offset: 0x00003D5C
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

		// Token: 0x040000F0 RID: 240
		public readonly ExPerformanceCounter OutstandingAuthenticationRequests;

		// Token: 0x040000F1 RID: 241
		public readonly ExPerformanceCounter TotalAuthenticationRequests;

		// Token: 0x040000F2 RID: 242
		public readonly ExPerformanceCounter RejectedAuthenticationRequests;

		// Token: 0x040000F3 RID: 243
		public readonly ExPerformanceCounter AuthenticationLatency;
	}
}
