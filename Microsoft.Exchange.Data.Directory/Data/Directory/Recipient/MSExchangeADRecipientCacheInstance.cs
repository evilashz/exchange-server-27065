using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000A3B RID: 2619
	internal sealed class MSExchangeADRecipientCacheInstance : PerformanceCounterInstance
	{
		// Token: 0x06007836 RID: 30774 RVA: 0x0018BE04 File Offset: 0x0018A004
		internal MSExchangeADRecipientCacheInstance(string instanceName, MSExchangeADRecipientCacheInstance autoUpdateTotalInstance) : base(instanceName, "MSExchange Recipient Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.IndividualAddressLookupsTotal = new ExPerformanceCounter(base.CategoryName, "Individual Address Lookups", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.IndividualAddressLookupsTotal);
				this.BatchedAddressLookupsTotal = new ExPerformanceCounter(base.CategoryName, "Batched Address Lookups", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BatchedAddressLookupsTotal);
				this.ExpandGroupRequestsTotal = new ExPerformanceCounter(base.CategoryName, "Expand Group Requests", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandGroupRequestsTotal);
				this.RequestsPendingTotal = new ExPerformanceCounter(base.CategoryName, "Address Lookups Pending", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RequestsPendingTotal);
				this.AggregateHits = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate hits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AggregateHits);
				this.AggregateMisses = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate misses", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AggregateMisses);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate misses base counter.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.AggregateHits_Base = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate hits base counter.", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.AggregateHits_Base);
				this.AverageLookupQueryLatency = new ExPerformanceCounter(base.CategoryName, "Average Active Directory recipient cache lookup query latecy", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLookupQueryLatency);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Average Active Directory recipient cache lookup query latency base counter.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.AggregateLookupsTotal = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate total lookups", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.AggregateLookupsTotal);
				this.RepeatedQueryForTheSameRecipient = new ExPerformanceCounter(base.CategoryName, "Total repeated queries for the same recipient", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RepeatedQueryForTheSameRecipient);
				this.NumberOfQueriesPerRecipientCache50Percentile = new ExPerformanceCounter(base.CategoryName, "Number of queries per recipient cache, 50th percentile", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerRecipientCache50Percentile);
				this.NumberOfQueriesPerRecipientCache80Percentile = new ExPerformanceCounter(base.CategoryName, "Number of queries per recipient cache, 80th percentile", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerRecipientCache80Percentile);
				this.NumberOfQueriesPerRecipientCache95Percentile = new ExPerformanceCounter(base.CategoryName, "Number of queries per recipient cache, 95th percentile", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerRecipientCache95Percentile);
				this.NumberOfQueriesPerRecipientCache99Percentile = new ExPerformanceCounter(base.CategoryName, "Number of queries per recipient cache, 99th percentile", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerRecipientCache99Percentile);
				long num = this.IndividualAddressLookupsTotal.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter3 in list)
					{
						exPerformanceCounter3.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06007837 RID: 30775 RVA: 0x0018C168 File Offset: 0x0018A368
		internal MSExchangeADRecipientCacheInstance(string instanceName) : base(instanceName, "MSExchange Recipient Cache")
		{
			bool flag = false;
			List<ExPerformanceCounter> list = new List<ExPerformanceCounter>();
			try
			{
				this.IndividualAddressLookupsTotal = new ExPerformanceCounter(base.CategoryName, "Individual Address Lookups", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.IndividualAddressLookupsTotal);
				this.BatchedAddressLookupsTotal = new ExPerformanceCounter(base.CategoryName, "Batched Address Lookups", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.BatchedAddressLookupsTotal);
				this.ExpandGroupRequestsTotal = new ExPerformanceCounter(base.CategoryName, "Expand Group Requests", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.ExpandGroupRequestsTotal);
				this.RequestsPendingTotal = new ExPerformanceCounter(base.CategoryName, "Address Lookups Pending", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RequestsPendingTotal);
				this.AggregateHits = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate hits", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AggregateHits);
				this.AggregateMisses = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate misses", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AggregateMisses);
				ExPerformanceCounter exPerformanceCounter = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate misses base counter.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter);
				this.AggregateHits_Base = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate hits base counter.", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter
				});
				list.Add(this.AggregateHits_Base);
				this.AverageLookupQueryLatency = new ExPerformanceCounter(base.CategoryName, "Average Active Directory recipient cache lookup query latecy", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.AverageLookupQueryLatency);
				ExPerformanceCounter exPerformanceCounter2 = new ExPerformanceCounter(base.CategoryName, "Average Active Directory recipient cache lookup query latency base counter.", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(exPerformanceCounter2);
				this.AggregateLookupsTotal = new ExPerformanceCounter(base.CategoryName, "Active Directory recipient cache aggregate total lookups", instanceName, true, null, new ExPerformanceCounter[]
				{
					exPerformanceCounter2
				});
				list.Add(this.AggregateLookupsTotal);
				this.RepeatedQueryForTheSameRecipient = new ExPerformanceCounter(base.CategoryName, "Total repeated queries for the same recipient", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.RepeatedQueryForTheSameRecipient);
				this.NumberOfQueriesPerRecipientCache50Percentile = new ExPerformanceCounter(base.CategoryName, "Number of queries per recipient cache, 50th percentile", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerRecipientCache50Percentile);
				this.NumberOfQueriesPerRecipientCache80Percentile = new ExPerformanceCounter(base.CategoryName, "Number of queries per recipient cache, 80th percentile", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerRecipientCache80Percentile);
				this.NumberOfQueriesPerRecipientCache95Percentile = new ExPerformanceCounter(base.CategoryName, "Number of queries per recipient cache, 95th percentile", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerRecipientCache95Percentile);
				this.NumberOfQueriesPerRecipientCache99Percentile = new ExPerformanceCounter(base.CategoryName, "Number of queries per recipient cache, 99th percentile", instanceName, true, null, new ExPerformanceCounter[0]);
				list.Add(this.NumberOfQueriesPerRecipientCache99Percentile);
				long num = this.IndividualAddressLookupsTotal.RawValue;
				num += 1L;
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					foreach (ExPerformanceCounter exPerformanceCounter3 in list)
					{
						exPerformanceCounter3.Close();
					}
				}
			}
			this.counters = list.ToArray();
		}

		// Token: 0x06007838 RID: 30776 RVA: 0x0018C4CC File Offset: 0x0018A6CC
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

		// Token: 0x04004CE0 RID: 19680
		public readonly ExPerformanceCounter IndividualAddressLookupsTotal;

		// Token: 0x04004CE1 RID: 19681
		public readonly ExPerformanceCounter BatchedAddressLookupsTotal;

		// Token: 0x04004CE2 RID: 19682
		public readonly ExPerformanceCounter ExpandGroupRequestsTotal;

		// Token: 0x04004CE3 RID: 19683
		public readonly ExPerformanceCounter RequestsPendingTotal;

		// Token: 0x04004CE4 RID: 19684
		public readonly ExPerformanceCounter AggregateLookupsTotal;

		// Token: 0x04004CE5 RID: 19685
		public readonly ExPerformanceCounter AggregateHits;

		// Token: 0x04004CE6 RID: 19686
		public readonly ExPerformanceCounter AggregateHits_Base;

		// Token: 0x04004CE7 RID: 19687
		public readonly ExPerformanceCounter AggregateMisses;

		// Token: 0x04004CE8 RID: 19688
		public readonly ExPerformanceCounter AverageLookupQueryLatency;

		// Token: 0x04004CE9 RID: 19689
		public readonly ExPerformanceCounter RepeatedQueryForTheSameRecipient;

		// Token: 0x04004CEA RID: 19690
		public readonly ExPerformanceCounter NumberOfQueriesPerRecipientCache50Percentile;

		// Token: 0x04004CEB RID: 19691
		public readonly ExPerformanceCounter NumberOfQueriesPerRecipientCache80Percentile;

		// Token: 0x04004CEC RID: 19692
		public readonly ExPerformanceCounter NumberOfQueriesPerRecipientCache95Percentile;

		// Token: 0x04004CED RID: 19693
		public readonly ExPerformanceCounter NumberOfQueriesPerRecipientCache99Percentile;
	}
}
