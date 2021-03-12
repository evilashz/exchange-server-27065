using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxLoadBalance.CapacityData;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxLoadBalanceClient
{
	// Token: 0x02000002 RID: 2
	public sealed class CapacitySummary : ConfigurableObject
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public CapacitySummary() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DD File Offset: 0x000002DD
		internal CapacitySummary(PropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020E6 File Offset: 0x000002E6
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public new string Identity
		{
			get
			{
				return (string)this[CapacitySummarySchema.Identity];
			}
			internal set
			{
				this[CapacitySummarySchema.Identity] = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002106 File Offset: 0x00000306
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000210E File Offset: 0x0000030E
		public Report.ListWithToString<LoadMetricValue> LoadMetrics { get; internal set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002117 File Offset: 0x00000317
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002129 File Offset: 0x00000329
		public ByteQuantifiedSize LogicalSize
		{
			get
			{
				return (ByteQuantifiedSize)this[CapacitySummarySchema.LogicalSize];
			}
			internal set
			{
				this[CapacitySummarySchema.LogicalSize] = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000214E File Offset: 0x0000034E
		public ByteQuantifiedSize MaximumSize
		{
			get
			{
				return (ByteQuantifiedSize)this[CapacitySummarySchema.MaximumSize];
			}
			internal set
			{
				this[CapacitySummarySchema.MaximumSize] = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002161 File Offset: 0x00000361
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002173 File Offset: 0x00000373
		public ByteQuantifiedSize PhysicalSize
		{
			get
			{
				return (ByteQuantifiedSize)this[CapacitySummarySchema.PhysicalSize];
			}
			internal set
			{
				this[CapacitySummarySchema.PhysicalSize] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002186 File Offset: 0x00000386
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002198 File Offset: 0x00000398
		public DateTime RetrievedTimeStamp
		{
			get
			{
				return (DateTime)this[CapacitySummarySchema.RetrievedTimestamp];
			}
			set
			{
				this[CapacitySummarySchema.RetrievedTimestamp] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021AB File Offset: 0x000003AB
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021BD File Offset: 0x000003BD
		public long TotalMailboxCount
		{
			get
			{
				return (long)this[CapacitySummarySchema.TotalMailboxCount];
			}
			internal set
			{
				this[CapacitySummarySchema.TotalMailboxCount] = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021D0 File Offset: 0x000003D0
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<CapacitySummarySchema>();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021D8 File Offset: 0x000003D8
		internal static CapacitySummary FromDatum(HeatMapCapacityData capacityDatum)
		{
			CapacitySummary capacitySummary = new CapacitySummary();
			capacitySummary.SetExchangeVersion(ExchangeObjectVersion.Current);
			capacitySummary.Identity = capacityDatum.Identity.Name;
			capacitySummary.PhysicalSize = capacityDatum.PhysicalSize;
			capacitySummary.LogicalSize = capacityDatum.LogicalSize;
			capacitySummary.MaximumSize = capacityDatum.TotalCapacity;
			capacitySummary.TotalMailboxCount = capacityDatum.TotalMailboxCount;
			capacitySummary.RetrievedTimeStamp = capacityDatum.RetrievedTimestamp;
			Report.ListWithToString<LoadMetricValue> listWithToString = new Report.ListWithToString<LoadMetricValue>();
			foreach (LoadMetric loadMetric in capacityDatum.LoadMetrics.Metrics)
			{
				LoadMetricValue item = new LoadMetricValue(loadMetric, capacityDatum.LoadMetrics[loadMetric]);
				listWithToString.Add(item);
			}
			capacitySummary.LoadMetrics = listWithToString;
			return capacitySummary;
		}
	}
}
