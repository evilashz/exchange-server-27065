using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000021 RID: 33
	internal sealed class HttpProxyCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00005710 File Offset: 0x00003910
		internal HttpProxyCountersInstance(string instanceName, HttpProxyCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange HttpProxy")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Requests/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalRequests = new ExPerformanceCounter(base.CategoryName, "Total Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalRequests);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Bytes Out/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalBytesOut = new ExPerformanceCounter(base.CategoryName, "Total Bytes Out", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalBytesOut);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Bytes In/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalBytesIn = new ExPerformanceCounter(base.CategoryName, "Total Bytes In", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalBytesIn);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Proxy Requests/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TotalProxyRequests = new ExPerformanceCounter(base.CategoryName, "Total Proxy Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TotalProxyRequests);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Calls/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.MailboxServerLocatorCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Calls", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.MailboxServerLocatorCalls);
				this.MailboxServerLocatorFailedCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Failed Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorFailedCalls);
				this.MailboxServerLocatorRetriedCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Retried Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorRetriedCalls);
				this.MailboxServerLocatorLatency = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Last Call Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorLatency);
				this.MailboxServerLocatorAverageLatency = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorAverageLatency);
				this.MailboxServerLocatorAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Average Latency Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorAverageLatencyBase);
				this.DownLevelTotalServers = new ExPerformanceCounter(base.CategoryName, "ClientAccess 2010 Total Servers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DownLevelTotalServers);
				this.DownLevelHealthyServers = new ExPerformanceCounter(base.CategoryName, "ClientAccess 2010 Healthy Servers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DownLevelHealthyServers);
				this.DownLevelServersLastPing = new ExPerformanceCounter(base.CategoryName, "ClientAccess 2010 Servers Last Ping", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DownLevelServersLastPing);
				this.LoadBalancerHealthChecks = new ExPerformanceCounter(base.CategoryName, "Load Balancer Health Checks", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LoadBalancerHealthChecks);
				this.MovingAverageCasLatency = new ExPerformanceCounter(base.CategoryName, "Average ClientAccess Server Processing Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageCasLatency);
				this.MovingAverageTenantLookupLatency = new ExPerformanceCounter(base.CategoryName, "Average Tenant Lookup Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageTenantLookupLatency);
				this.MovingAverageAuthenticationLatency = new ExPerformanceCounter(base.CategoryName, "Average Authentication Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageAuthenticationLatency);
				this.MovingAverageMailboxServerLocatorLatency = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Average Latency (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageMailboxServerLocatorLatency);
				this.MovingAverageSharedCacheLatency = new ExPerformanceCounter(base.CategoryName, "Shared Cache Client Average Latency (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageSharedCacheLatency);
				this.MovingPercentageMailboxServerFailure = new ExPerformanceCounter(base.CategoryName, "Mailbox Server Proxy Failure Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageMailboxServerFailure);
				this.MovingPercentageMailboxServerLocatorRetriedCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Retried Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageMailboxServerLocatorRetriedCalls);
				this.MovingPercentageMailboxServerLocatorFailedCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Failure Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageMailboxServerLocatorFailedCalls);
				this.MovingPercentageNewProxyConnectionCreation = new ExPerformanceCounter(base.CategoryName, "Proxy Connection Creation Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageNewProxyConnectionCreation);
				this.TotalRetryOnError = new ExPerformanceCounter(base.CategoryName, "Total RetryOnError", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalRetryOnError);
				this.FailedRetryOnError = new ExPerformanceCounter(base.CategoryName, "Failed RetryOnError", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedRetryOnError);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Accepted Connection Count Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.AcceptedConnectionCount = new ExPerformanceCounter(base.CategoryName, "Accepted Connection Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.AcceptedConnectionCount);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Rejected Connection Count Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.RejectedConnectionCount = new ExPerformanceCounter(base.CategoryName, "Rejected Connection Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.RejectedConnectionCount);
				this.OutstandingProxyRequests = new ExPerformanceCounter(base.CategoryName, "Outstanding Proxy Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingProxyRequests);
				this.OutstandingProxyRequestsToForest = new ExPerformanceCounter(base.CategoryName, "Outstanding Proxy Requests to Target Forest", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingProxyRequestsToForest);
				this.OutstandingSharedCacheRequests = new ExPerformanceCounter(base.CategoryName, "Outstanding Shared Cache Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingSharedCacheRequests);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Redirect By Sender Mailbox Count Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.RedirectBySenderMailboxCount = new ExPerformanceCounter(base.CategoryName, "Redirect By Sender Mailbox Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.RedirectBySenderMailboxCount);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "Redirect By Tenant Mailbox Count Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.RedirectByTenantMailboxCount = new ExPerformanceCounter(base.CategoryName, "Redirect By Tenant Mailbox Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.RedirectByTenantMailboxCount);
				long num = this.TotalRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter10 in list)
					{
						exPerformanceCounter10.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00005EA0 File Offset: 0x000040A0
		internal HttpProxyCountersInstance(string instanceName) : base(instanceName, "MSExchange HttpProxy")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Requests/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalRequests = new ExPerformanceCounter(base.CategoryName, "Total Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalRequests);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Bytes Out/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalBytesOut = new ExPerformanceCounter(base.CategoryName, "Total Bytes Out", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalBytesOut);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Bytes In/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalBytesIn = new ExPerformanceCounter(base.CategoryName, "Total Bytes In", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalBytesIn);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Proxy Requests/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TotalProxyRequests = new ExPerformanceCounter(base.CategoryName, "Total Proxy Requests", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TotalProxyRequests);
				ExPerformanceCounter exPerformanceCounter5 = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Calls/Sec", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter5);
				this.MailboxServerLocatorCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Calls", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter5
				});
				list.Add(this.MailboxServerLocatorCalls);
				this.MailboxServerLocatorFailedCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Failed Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorFailedCalls);
				this.MailboxServerLocatorRetriedCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Retried Calls", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorRetriedCalls);
				this.MailboxServerLocatorLatency = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Last Call Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorLatency);
				this.MailboxServerLocatorAverageLatency = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Average Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorAverageLatency);
				this.MailboxServerLocatorAverageLatencyBase = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Average Latency Base", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MailboxServerLocatorAverageLatencyBase);
				this.DownLevelTotalServers = new ExPerformanceCounter(base.CategoryName, "ClientAccess 2010 Total Servers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DownLevelTotalServers);
				this.DownLevelHealthyServers = new ExPerformanceCounter(base.CategoryName, "ClientAccess 2010 Healthy Servers", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DownLevelHealthyServers);
				this.DownLevelServersLastPing = new ExPerformanceCounter(base.CategoryName, "ClientAccess 2010 Servers Last Ping", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.DownLevelServersLastPing);
				this.LoadBalancerHealthChecks = new ExPerformanceCounter(base.CategoryName, "Load Balancer Health Checks", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.LoadBalancerHealthChecks);
				this.MovingAverageCasLatency = new ExPerformanceCounter(base.CategoryName, "Average ClientAccess Server Processing Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageCasLatency);
				this.MovingAverageTenantLookupLatency = new ExPerformanceCounter(base.CategoryName, "Average Tenant Lookup Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageTenantLookupLatency);
				this.MovingAverageAuthenticationLatency = new ExPerformanceCounter(base.CategoryName, "Average Authentication Latency", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageAuthenticationLatency);
				this.MovingAverageMailboxServerLocatorLatency = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Average Latency (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageMailboxServerLocatorLatency);
				this.MovingAverageSharedCacheLatency = new ExPerformanceCounter(base.CategoryName, "Shared Cache Client Average Latency (Moving Average)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingAverageSharedCacheLatency);
				this.MovingPercentageMailboxServerFailure = new ExPerformanceCounter(base.CategoryName, "Mailbox Server Proxy Failure Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageMailboxServerFailure);
				this.MovingPercentageMailboxServerLocatorRetriedCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Retried Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageMailboxServerLocatorRetriedCalls);
				this.MovingPercentageMailboxServerLocatorFailedCalls = new ExPerformanceCounter(base.CategoryName, "MailboxServerLocator Failure Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageMailboxServerLocatorFailedCalls);
				this.MovingPercentageNewProxyConnectionCreation = new ExPerformanceCounter(base.CategoryName, "Proxy Connection Creation Rate", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.MovingPercentageNewProxyConnectionCreation);
				this.TotalRetryOnError = new ExPerformanceCounter(base.CategoryName, "Total RetryOnError", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalRetryOnError);
				this.FailedRetryOnError = new ExPerformanceCounter(base.CategoryName, "Failed RetryOnError", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.FailedRetryOnError);
				ExPerformanceCounter exPerformanceCounter6 = new ExPerformanceCounter(base.CategoryName, "Accepted Connection Count Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter6);
				this.AcceptedConnectionCount = new ExPerformanceCounter(base.CategoryName, "Accepted Connection Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter6
				});
				list.Add(this.AcceptedConnectionCount);
				ExPerformanceCounter exPerformanceCounter7 = new ExPerformanceCounter(base.CategoryName, "Rejected Connection Count Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter7);
				this.RejectedConnectionCount = new ExPerformanceCounter(base.CategoryName, "Rejected Connection Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter7
				});
				list.Add(this.RejectedConnectionCount);
				this.OutstandingProxyRequests = new ExPerformanceCounter(base.CategoryName, "Outstanding Proxy Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingProxyRequests);
				this.OutstandingProxyRequestsToForest = new ExPerformanceCounter(base.CategoryName, "Outstanding Proxy Requests to Target Forest", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingProxyRequestsToForest);
				this.OutstandingSharedCacheRequests = new ExPerformanceCounter(base.CategoryName, "Outstanding Shared Cache Requests", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.OutstandingSharedCacheRequests);
				ExPerformanceCounter exPerformanceCounter8 = new ExPerformanceCounter(base.CategoryName, "Redirect By Sender Mailbox Count Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter8);
				this.RedirectBySenderMailboxCount = new ExPerformanceCounter(base.CategoryName, "Redirect By Sender Mailbox Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter8
				});
				list.Add(this.RedirectBySenderMailboxCount);
				ExPerformanceCounter exPerformanceCounter9 = new ExPerformanceCounter(base.CategoryName, "Redirect By Tenant Mailbox Count Per Second", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter9);
				this.RedirectByTenantMailboxCount = new ExPerformanceCounter(base.CategoryName, "Redirect By Tenant Mailbox Count", instanceName, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter9
				});
				list.Add(this.RedirectByTenantMailboxCount);
				long num = this.TotalRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter10 in list)
					{
						exPerformanceCounter10.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006630 File Offset: 0x00004830
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

		// Token: 0x040000F7 RID: 247
		public readonly ExPerformanceCounter TotalRequests;

		// Token: 0x040000F8 RID: 248
		public readonly ExPerformanceCounter TotalBytesOut;

		// Token: 0x040000F9 RID: 249
		public readonly ExPerformanceCounter TotalBytesIn;

		// Token: 0x040000FA RID: 250
		public readonly ExPerformanceCounter TotalProxyRequests;

		// Token: 0x040000FB RID: 251
		public readonly ExPerformanceCounter MailboxServerLocatorCalls;

		// Token: 0x040000FC RID: 252
		public readonly ExPerformanceCounter MailboxServerLocatorFailedCalls;

		// Token: 0x040000FD RID: 253
		public readonly ExPerformanceCounter MailboxServerLocatorRetriedCalls;

		// Token: 0x040000FE RID: 254
		public readonly ExPerformanceCounter MailboxServerLocatorLatency;

		// Token: 0x040000FF RID: 255
		public readonly ExPerformanceCounter MailboxServerLocatorAverageLatency;

		// Token: 0x04000100 RID: 256
		public readonly ExPerformanceCounter MailboxServerLocatorAverageLatencyBase;

		// Token: 0x04000101 RID: 257
		public readonly ExPerformanceCounter DownLevelTotalServers;

		// Token: 0x04000102 RID: 258
		public readonly ExPerformanceCounter DownLevelHealthyServers;

		// Token: 0x04000103 RID: 259
		public readonly ExPerformanceCounter DownLevelServersLastPing;

		// Token: 0x04000104 RID: 260
		public readonly ExPerformanceCounter LoadBalancerHealthChecks;

		// Token: 0x04000105 RID: 261
		public readonly ExPerformanceCounter MovingAverageCasLatency;

		// Token: 0x04000106 RID: 262
		public readonly ExPerformanceCounter MovingAverageTenantLookupLatency;

		// Token: 0x04000107 RID: 263
		public readonly ExPerformanceCounter MovingAverageAuthenticationLatency;

		// Token: 0x04000108 RID: 264
		public readonly ExPerformanceCounter MovingAverageMailboxServerLocatorLatency;

		// Token: 0x04000109 RID: 265
		public readonly ExPerformanceCounter MovingAverageSharedCacheLatency;

		// Token: 0x0400010A RID: 266
		public readonly ExPerformanceCounter MovingPercentageMailboxServerFailure;

		// Token: 0x0400010B RID: 267
		public readonly ExPerformanceCounter MovingPercentageMailboxServerLocatorRetriedCalls;

		// Token: 0x0400010C RID: 268
		public readonly ExPerformanceCounter MovingPercentageMailboxServerLocatorFailedCalls;

		// Token: 0x0400010D RID: 269
		public readonly ExPerformanceCounter MovingPercentageNewProxyConnectionCreation;

		// Token: 0x0400010E RID: 270
		public readonly ExPerformanceCounter TotalRetryOnError;

		// Token: 0x0400010F RID: 271
		public readonly ExPerformanceCounter FailedRetryOnError;

		// Token: 0x04000110 RID: 272
		public readonly ExPerformanceCounter AcceptedConnectionCount;

		// Token: 0x04000111 RID: 273
		public readonly ExPerformanceCounter RejectedConnectionCount;

		// Token: 0x04000112 RID: 274
		public readonly ExPerformanceCounter OutstandingProxyRequests;

		// Token: 0x04000113 RID: 275
		public readonly ExPerformanceCounter OutstandingProxyRequestsToForest;

		// Token: 0x04000114 RID: 276
		public readonly ExPerformanceCounter OutstandingSharedCacheRequests;

		// Token: 0x04000115 RID: 277
		public readonly ExPerformanceCounter RedirectBySenderMailboxCount;

		// Token: 0x04000116 RID: 278
		public readonly ExPerformanceCounter RedirectByTenantMailboxCount;
	}
}
