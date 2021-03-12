using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x0200000A RID: 10
	internal sealed class OAuthCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00005284 File Offset: 0x00003484
		internal OAuthCountersInstance(string instanceName, OAuthCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange OAuth")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Inbound: Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfAuthRequests = new ExPerformanceCounter(base.CategoryName, "Inbound: Total Auth Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfAuthRequests);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Inbound: Failed Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.NumberOfFailedAuthRequests = new ExPerformanceCounter(base.CategoryName, "Inbound: Failed Auth Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.NumberOfFailedAuthRequests);
				this.AverageResponseTime = new ExPerformanceCounter(base.CategoryName, "Inbound: Average Auth Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResponseTime);
				this.AverageAuthServerResponseTime = new ExPerformanceCounter(base.CategoryName, "Outbound: Average AuthServer Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAuthServerResponseTime);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Outbound: Token Requests to AuthServer/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.NumberOfAuthServerTokenRequests = new ExPerformanceCounter(base.CategoryName, "Outbound: Total Token Requests to AuthServer", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfAuthServerTokenRequests);
				this.NumberOfPendingAuthServerRequests = new ExPerformanceCounter(base.CategoryName, "Outbound: Number of Pending AuthServer Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfPendingAuthServerRequests);
				this.AuthServerTokenCacheSize = new ExPerformanceCounter(base.CategoryName, "Outbound: AuthServer Token Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AuthServerTokenCacheSize);
				this.NumberOfAuthServerTimeoutTokenRequests = new ExPerformanceCounter(base.CategoryName, "Outbound: Total Timeout Token Requests to AuthServer", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfAuthServerTimeoutTokenRequests);
				long num = this.NumberOfAuthRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter4 in list)
					{
						exPerformanceCounter4.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000550C File Offset: 0x0000370C
		internal OAuthCountersInstance(string instanceName) : base(instanceName, "MSExchange OAuth")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Inbound: Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.NumberOfAuthRequests = new ExPerformanceCounter(base.CategoryName, "Inbound: Total Auth Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.NumberOfAuthRequests);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Inbound: Failed Auth Requests/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.NumberOfFailedAuthRequests = new ExPerformanceCounter(base.CategoryName, "Inbound: Failed Auth Requests Total", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.NumberOfFailedAuthRequests);
				this.AverageResponseTime = new ExPerformanceCounter(base.CategoryName, "Inbound: Average Auth Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageResponseTime);
				this.AverageAuthServerResponseTime = new ExPerformanceCounter(base.CategoryName, "Outbound: Average AuthServer Response Time", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageAuthServerResponseTime);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Outbound: Token Requests to AuthServer/sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.NumberOfAuthServerTokenRequests = new ExPerformanceCounter(base.CategoryName, "Outbound: Total Token Requests to AuthServer", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.NumberOfAuthServerTokenRequests);
				this.NumberOfPendingAuthServerRequests = new ExPerformanceCounter(base.CategoryName, "Outbound: Number of Pending AuthServer Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfPendingAuthServerRequests);
				this.AuthServerTokenCacheSize = new ExPerformanceCounter(base.CategoryName, "Outbound: AuthServer Token Cache Size", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.AuthServerTokenCacheSize);
				this.NumberOfAuthServerTimeoutTokenRequests = new ExPerformanceCounter(base.CategoryName, "Outbound: Total Timeout Token Requests to AuthServer", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfAuthServerTimeoutTokenRequests);
				long num = this.NumberOfAuthRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter4 in list)
					{
						exPerformanceCounter4.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00005794 File Offset: 0x00003994
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

		// Token: 0x040000E6 RID: 230
		public readonly ExPerformanceCounter NumberOfAuthRequests;

		// Token: 0x040000E7 RID: 231
		public readonly ExPerformanceCounter NumberOfFailedAuthRequests;

		// Token: 0x040000E8 RID: 232
		public readonly ExPerformanceCounter AverageResponseTime;

		// Token: 0x040000E9 RID: 233
		public readonly ExPerformanceCounter AverageAuthServerResponseTime;

		// Token: 0x040000EA RID: 234
		public readonly ExPerformanceCounter NumberOfAuthServerTokenRequests;

		// Token: 0x040000EB RID: 235
		public readonly ExPerformanceCounter NumberOfPendingAuthServerRequests;

		// Token: 0x040000EC RID: 236
		public readonly ExPerformanceCounter AuthServerTokenCacheSize;

		// Token: 0x040000ED RID: 237
		public readonly ExPerformanceCounter NumberOfAuthServerTimeoutTokenRequests;
	}
}
