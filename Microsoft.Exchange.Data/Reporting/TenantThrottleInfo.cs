using System;

namespace Microsoft.Exchange.Data.Reporting
{
	// Token: 0x020002B2 RID: 690
	internal class TenantThrottleInfo : ConfigurableObject
	{
		// Token: 0x060018D9 RID: 6361 RVA: 0x0004E84D File Offset: 0x0004CA4D
		public TenantThrottleInfo() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060018DA RID: 6362 RVA: 0x0004E85C File Offset: 0x0004CA5C
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.TenantId.ToString());
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0004E882 File Offset: 0x0004CA82
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x0004E894 File Offset: 0x0004CA94
		public Guid TenantId
		{
			get
			{
				return (Guid)this[TenantThrottleInfoSchema.TenantIdProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.TenantIdProperty] = value;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0004E8A7 File Offset: 0x0004CAA7
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x0004E8B9 File Offset: 0x0004CAB9
		public DateTime TimeStamp
		{
			get
			{
				return (DateTime)this[TenantThrottleInfoSchema.TimeStampProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.TimeStampProperty] = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0004E8CC File Offset: 0x0004CACC
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x0004E8DE File Offset: 0x0004CADE
		public TenantThrottleState ThrottleState
		{
			get
			{
				return (TenantThrottleState)this[TenantThrottleInfoSchema.ThrottleStateProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.ThrottleStateProperty] = value;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0004E8F1 File Offset: 0x0004CAF1
		public bool IsThrottled
		{
			get
			{
				return this.ThrottleState == TenantThrottleState.Throttled || (this.ThrottleState == TenantThrottleState.Auto && this.ThrottlingFactor > 0.0);
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0004E919 File Offset: 0x0004CB19
		// (set) Token: 0x060018E3 RID: 6371 RVA: 0x0004E92B File Offset: 0x0004CB2B
		public int MessageCount
		{
			get
			{
				return (int)this[TenantThrottleInfoSchema.MessageCountProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.MessageCountProperty] = value;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0004E93E File Offset: 0x0004CB3E
		// (set) Token: 0x060018E5 RID: 6373 RVA: 0x0004E950 File Offset: 0x0004CB50
		public double AverageMessageSizeKb
		{
			get
			{
				return (double)this[TenantThrottleInfoSchema.AvgMessageSizeKbProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.AvgMessageSizeKbProperty] = value;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0004E963 File Offset: 0x0004CB63
		// (set) Token: 0x060018E7 RID: 6375 RVA: 0x0004E975 File Offset: 0x0004CB75
		public double AverageMessageCostMs
		{
			get
			{
				return (double)this[TenantThrottleInfoSchema.AvgMessageCostMsProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.AvgMessageCostMsProperty] = value;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0004E988 File Offset: 0x0004CB88
		// (set) Token: 0x060018E9 RID: 6377 RVA: 0x0004E99A File Offset: 0x0004CB9A
		public double ThrottlingFactor
		{
			get
			{
				return (double)this[TenantThrottleInfoSchema.ThrottlingFactorProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.ThrottlingFactorProperty] = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0004E9AD File Offset: 0x0004CBAD
		// (set) Token: 0x060018EB RID: 6379 RVA: 0x0004E9BF File Offset: 0x0004CBBF
		public int PartitionTenantCount
		{
			get
			{
				return (int)this[TenantThrottleInfoSchema.PartitionTenantCountProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.PartitionTenantCountProperty] = value;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0004E9D2 File Offset: 0x0004CBD2
		// (set) Token: 0x060018ED RID: 6381 RVA: 0x0004E9E4 File Offset: 0x0004CBE4
		public int PartitionMessageCount
		{
			get
			{
				return (int)this[TenantThrottleInfoSchema.PartitionMessageCountProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.PartitionMessageCountProperty] = value;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0004E9F7 File Offset: 0x0004CBF7
		// (set) Token: 0x060018EF RID: 6383 RVA: 0x0004EA09 File Offset: 0x0004CC09
		public double PartitionAverageMessageSizeKb
		{
			get
			{
				return (double)this[TenantThrottleInfoSchema.PartitionAvgMessageSizeKbProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.PartitionAvgMessageSizeKbProperty] = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0004EA1C File Offset: 0x0004CC1C
		// (set) Token: 0x060018F1 RID: 6385 RVA: 0x0004EA2E File Offset: 0x0004CC2E
		public double PartitionAverageMessageCostMs
		{
			get
			{
				return (double)this[TenantThrottleInfoSchema.PartitionAvgMessageCostMsProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.PartitionAvgMessageCostMsProperty] = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060018F2 RID: 6386 RVA: 0x0004EA41 File Offset: 0x0004CC41
		// (set) Token: 0x060018F3 RID: 6387 RVA: 0x0004EA53 File Offset: 0x0004CC53
		public double StandardDeviation
		{
			get
			{
				return (double)this[TenantThrottleInfoSchema.StandardDeviationProperty];
			}
			set
			{
				this[TenantThrottleInfoSchema.StandardDeviationProperty] = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0004EA66 File Offset: 0x0004CC66
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TenantThrottleInfo.SchemaObject;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0004EA6D File Offset: 0x0004CC6D
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000EAA RID: 3754
		private static readonly TenantThrottleInfoSchema SchemaObject = ObjectSchema.GetInstance<TenantThrottleInfoSchema>();
	}
}
