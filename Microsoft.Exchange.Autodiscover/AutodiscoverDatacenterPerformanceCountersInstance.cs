using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x0200000F RID: 15
	internal sealed class AutodiscoverDatacenterPerformanceCountersInstance : PerformanceCounterInstance
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000396C File Offset: 0x00001B6C
		internal AutodiscoverDatacenterPerformanceCountersInstance(string instanceName, AutodiscoverDatacenterPerformanceCountersInstance autoUpdateTotalInstance) : base(instanceName, "MSExchangeAutodiscover:Datacenter")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Partner Token Requests/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalPartnerTokenRequests = new ExPerformanceCounter(base.CategoryName, "Total Partner Token Requests", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalPartnerTokenRequests, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalPartnerTokenRequests);
				this.TotalPartnerTokenRequestsPerTimeWindow = new ExPerformanceCounter(base.CategoryName, "Total Partner Token Requests/Time Window", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalPartnerTokenRequestsPerTimeWindow, new ExPerformanceCounter[0]);
				list.Add(this.TotalPartnerTokenRequestsPerTimeWindow);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Partner Token Requests Failed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalPartnerTokenRequestsFailed = new ExPerformanceCounter(base.CategoryName, "Total Partner Token Requests Failed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalPartnerTokenRequestsFailed, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalPartnerTokenRequestsFailed);
				this.TotalPartnerTokenRequestsFailedPerTimeWindow = new ExPerformanceCounter(base.CategoryName, "Total Partner Token Requests Failed/Time Window", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalPartnerTokenRequestsFailedPerTimeWindow, new ExPerformanceCounter[0]);
				list.Add(this.TotalPartnerTokenRequestsFailedPerTimeWindow);
				this.TotalCertAuthRequestsFailed = new ExPerformanceCounter(base.CategoryName, "Total Certificate Authentication Requests Failed", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalCertAuthRequestsFailed, new ExPerformanceCounter[0]);
				list.Add(this.TotalCertAuthRequestsFailed);
				this.TotalCertAuthRequestsFailedPerTimeWindow = new ExPerformanceCounter(base.CategoryName, "Total Certificate Authentication Requests Failed/Time Window", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalCertAuthRequestsFailedPerTimeWindow, new ExPerformanceCounter[0]);
				list.Add(this.TotalCertAuthRequestsFailedPerTimeWindow);
				this.AveragePartnerInfoQueryTime = new ExPerformanceCounter(base.CategoryName, "Average Partner Info Query Time", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.AveragePartnerInfoQueryTime, new ExPerformanceCounter[0]);
				list.Add(this.AveragePartnerInfoQueryTime);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Requests Received with Partner Token/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalRequestsReceivedWithPartnerToken = new ExPerformanceCounter(base.CategoryName, "Total Requests Received with Partner Token", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalRequestsReceivedWithPartnerToken, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalRequestsReceivedWithPartnerToken);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Unauthorized Requests Received with Partner Token/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TotalUnauthorizedRequestsReceivedWithPartnerToken = new ExPerformanceCounter(base.CategoryName, "Total Unauthorized Requests Received with Partner Token", instanceName, true, (autoUpdateTotalInstance == null) ? null : autoUpdateTotalInstance.TotalUnauthorizedRequestsReceivedWithPartnerToken, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TotalUnauthorizedRequestsReceivedWithPartnerToken);
				long num = this.TotalPartnerTokenRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter5 in list)
					{
						exPerformanceCounter5.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003CBC File Offset: 0x00001EBC
		internal AutodiscoverDatacenterPerformanceCountersInstance(string instanceName) : base(instanceName, "MSExchangeAutodiscover:Datacenter")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Partner Token Requests/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.TotalPartnerTokenRequests = new ExPerformanceCounter(base.CategoryName, "Total Partner Token Requests", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.TotalPartnerTokenRequests);
				this.TotalPartnerTokenRequestsPerTimeWindow = new ExPerformanceCounter(base.CategoryName, "Total Partner Token Requests/Time Window", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPartnerTokenRequestsPerTimeWindow);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Partner Token Requests Failed/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.TotalPartnerTokenRequestsFailed = new ExPerformanceCounter(base.CategoryName, "Total Partner Token Requests Failed", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.TotalPartnerTokenRequestsFailed);
				this.TotalPartnerTokenRequestsFailedPerTimeWindow = new ExPerformanceCounter(base.CategoryName, "Total Partner Token Requests Failed/Time Window", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalPartnerTokenRequestsFailedPerTimeWindow);
				this.TotalCertAuthRequestsFailed = new ExPerformanceCounter(base.CategoryName, "Total Certificate Authentication Requests Failed", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalCertAuthRequestsFailed);
				this.TotalCertAuthRequestsFailedPerTimeWindow = new ExPerformanceCounter(base.CategoryName, "Total Certificate Authentication Requests Failed/Time Window", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalCertAuthRequestsFailedPerTimeWindow);
				this.AveragePartnerInfoQueryTime = new ExPerformanceCounter(base.CategoryName, "Average Partner Info Query Time", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AveragePartnerInfoQueryTime);
				ExPerformanceCounter exPerformanceCounter3 = new ExPerformanceCounter(base.CategoryName, "Requests Received with Partner Token/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter3);
				this.TotalRequestsReceivedWithPartnerToken = new ExPerformanceCounter(base.CategoryName, "Total Requests Received with Partner Token", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter3
				});
				list.Add(this.TotalRequestsReceivedWithPartnerToken);
				ExPerformanceCounter exPerformanceCounter4 = new ExPerformanceCounter(base.CategoryName, "Unauthorized Requests Received with Partner Token/sec", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter4);
				this.TotalUnauthorizedRequestsReceivedWithPartnerToken = new ExPerformanceCounter(base.CategoryName, "Total Unauthorized Requests Received with Partner Token", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter4
				});
				list.Add(this.TotalUnauthorizedRequestsReceivedWithPartnerToken);
				long num = this.TotalPartnerTokenRequests.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter5 in list)
					{
						exPerformanceCounter5.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003FA8 File Offset: 0x000021A8
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

		// Token: 0x04000093 RID: 147
		public readonly ExPerformanceCounter TotalPartnerTokenRequests;

		// Token: 0x04000094 RID: 148
		public readonly ExPerformanceCounter TotalPartnerTokenRequestsPerTimeWindow;

		// Token: 0x04000095 RID: 149
		public readonly ExPerformanceCounter TotalPartnerTokenRequestsFailed;

		// Token: 0x04000096 RID: 150
		public readonly ExPerformanceCounter TotalPartnerTokenRequestsFailedPerTimeWindow;

		// Token: 0x04000097 RID: 151
		public readonly ExPerformanceCounter TotalCertAuthRequestsFailed;

		// Token: 0x04000098 RID: 152
		public readonly ExPerformanceCounter TotalCertAuthRequestsFailedPerTimeWindow;

		// Token: 0x04000099 RID: 153
		public readonly ExPerformanceCounter AveragePartnerInfoQueryTime;

		// Token: 0x0400009A RID: 154
		public readonly ExPerformanceCounter TotalRequestsReceivedWithPartnerToken;

		// Token: 0x0400009B RID: 155
		public readonly ExPerformanceCounter TotalUnauthorizedRequestsReceivedWithPartnerToken;
	}
}
