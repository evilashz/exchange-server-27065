﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000063 RID: 99
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class MsExchangeTransportSyncManagerBySLAPerfInstance : PerformanceCounterInstance
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0001BC5C File Offset: 0x00019E5C
		internal MsExchangeTransportSyncManagerBySLAPerfInstance(string instanceName, MsExchangeTransportSyncManagerBySLAPerfInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Transport Sync Manager By SLA")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SubscriptionsPollingFrequency95Percent = new ExPerformanceCounter(base.CategoryName, "95 percentile subscription polling frequency (seconds)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubscriptionsPollingFrequency95Percent);
				long num = this.SubscriptionsPollingFrequency95Percent.RawValue;
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

		// Token: 0x06000473 RID: 1139 RVA: 0x0001BD1C File Offset: 0x00019F1C
		internal MsExchangeTransportSyncManagerBySLAPerfInstance(string instanceName) : base(instanceName, "MSExchange Transport Sync Manager By SLA")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.SubscriptionsPollingFrequency95Percent = new ExPerformanceCounter(base.CategoryName, "95 percentile subscription polling frequency (seconds)", instanceName, null, new ExPerformanceCounter[0]);
				list.Add(this.SubscriptionsPollingFrequency95Percent);
				long num = this.SubscriptionsPollingFrequency95Percent.RawValue;
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

		// Token: 0x06000474 RID: 1140 RVA: 0x0001BDDC File Offset: 0x00019FDC
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

		// Token: 0x0400029B RID: 667
		public readonly ExPerformanceCounter SubscriptionsPollingFrequency95Percent;
	}
}
