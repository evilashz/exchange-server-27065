using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x02000A4C RID: 2636
	internal sealed class MSExchangeProvisioningCacheInstance : PerformanceCounterInstance
	{
		// Token: 0x06007884 RID: 30852 RVA: 0x0018F0B8 File Offset: 0x0018D2B8
		internal MSExchangeProvisioningCacheInstance(string instanceName, MSExchangeProvisioningCacheInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Provisioning Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.GlobalAggregateHits = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache global data aggregate hits.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GlobalAggregateHits);
				this.GlobalAggregateMisses = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache global data aggregate misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GlobalAggregateMisses);
				this.OrganizationAggregateHits = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache organization data aggregate hits.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationAggregateHits);
				this.OrganizationAggregateMisses = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache organization data aggregate misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationAggregateMisses);
				this.ReceivedInvalidationMsgNum = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache Received Invalidation Msg Counter.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReceivedInvalidationMsgNum);
				this.TotalCachedObjectNum = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache Total Cached Object Counter.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalCachedObjectNum);
				long num = this.GlobalAggregateHits.RawValue;
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

		// Token: 0x06007885 RID: 30853 RVA: 0x0018F268 File Offset: 0x0018D468
		internal MSExchangeProvisioningCacheInstance(string instanceName) : base(instanceName, "MSExchange Provisioning Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.GlobalAggregateHits = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache global data aggregate hits.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GlobalAggregateHits);
				this.GlobalAggregateMisses = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache global data aggregate misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.GlobalAggregateMisses);
				this.OrganizationAggregateHits = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache organization data aggregate hits.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationAggregateHits);
				this.OrganizationAggregateMisses = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache organization data aggregate misses.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.OrganizationAggregateMisses);
				this.ReceivedInvalidationMsgNum = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache Received Invalidation Msg Counter.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ReceivedInvalidationMsgNum);
				this.TotalCachedObjectNum = new ExPerformanceCounter(base.CategoryName, "Provisioning Cache Total Cached Object Counter.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.TotalCachedObjectNum);
				long num = this.GlobalAggregateHits.RawValue;
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

		// Token: 0x06007886 RID: 30854 RVA: 0x0018F418 File Offset: 0x0018D618
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

		// Token: 0x04004F5B RID: 20315
		public readonly ExPerformanceCounter GlobalAggregateHits;

		// Token: 0x04004F5C RID: 20316
		public readonly ExPerformanceCounter GlobalAggregateMisses;

		// Token: 0x04004F5D RID: 20317
		public readonly ExPerformanceCounter OrganizationAggregateHits;

		// Token: 0x04004F5E RID: 20318
		public readonly ExPerformanceCounter OrganizationAggregateMisses;

		// Token: 0x04004F5F RID: 20319
		public readonly ExPerformanceCounter ReceivedInvalidationMsgNum;

		// Token: 0x04004F60 RID: 20320
		public readonly ExPerformanceCounter TotalCachedObjectNum;
	}
}
