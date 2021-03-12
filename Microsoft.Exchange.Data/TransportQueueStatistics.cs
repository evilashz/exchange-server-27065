using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000295 RID: 661
	internal class TransportQueueStatistics : ConfigurableObject
	{
		// Token: 0x060017E4 RID: 6116 RVA: 0x0004AF86 File Offset: 0x00049186
		public TransportQueueStatistics() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0004AFA0 File Offset: 0x000491A0
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.identity.ToString());
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0004AFC6 File Offset: 0x000491C6
		public string ServerName
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.ServerNameProperty];
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0004AFD8 File Offset: 0x000491D8
		public string TlsDomain
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.TlsDomainProperty];
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0004AFEA File Offset: 0x000491EA
		public string NextHopDomain
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.NextHopDomainProperty];
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0004AFFC File Offset: 0x000491FC
		public string NextHopKey
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.NextHopKeyProperty];
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0004B00E File Offset: 0x0004920E
		public string NextHopCategory
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.NextHopCategoryProperty];
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0004B020 File Offset: 0x00049220
		public string DeliveryType
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.DeliveryTypeProperty];
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0004B032 File Offset: 0x00049232
		public string RiskLevel
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.RiskLevelProperty];
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0004B044 File Offset: 0x00049244
		public string OutboundIPPool
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.OutboundIPPoolProperty];
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0004B056 File Offset: 0x00049256
		public string Status
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.StatusProperty];
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0004B068 File Offset: 0x00049268
		public string LastError
		{
			get
			{
				return (string)this[TransportQueueStatisticsSchema.LastErrorProperty];
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0004B07A File Offset: 0x0004927A
		public int QueueCount
		{
			get
			{
				return (int)this[TransportQueueStatisticsSchema.QueueCountProperty];
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0004B08C File Offset: 0x0004928C
		public int MessageCount
		{
			get
			{
				return (int)this[TransportQueueStatisticsSchema.MessageCountProperty];
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0004B09E File Offset: 0x0004929E
		public int DeferredMessageCount
		{
			get
			{
				return (int)this[TransportQueueStatisticsSchema.DeferredMessageCountProperty];
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0004B0B0 File Offset: 0x000492B0
		public int LockedMessageCount
		{
			get
			{
				return (int)this[TransportQueueStatisticsSchema.LockedMessageCountProperty];
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0004B0C2 File Offset: 0x000492C2
		public double IncomingRate
		{
			get
			{
				return (double)this[TransportQueueStatisticsSchema.IncomingRateProperty];
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0004B0D4 File Offset: 0x000492D4
		public double OutgoingRate
		{
			get
			{
				return (double)this[TransportQueueStatisticsSchema.OutgoingRateProperty];
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0004B0E6 File Offset: 0x000492E6
		public double Velocity
		{
			get
			{
				return (double)this[TransportQueueStatisticsSchema.VelocityProperty];
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0004B0F8 File Offset: 0x000492F8
		public MultiValuedProperty<TransportQueueLog> QueueLogs
		{
			get
			{
				if (this.queueLogs == null)
				{
					this.queueLogs = TransportQueueLog.Parse(this[TransportQueueStatisticsSchema.TransportQueueLogsProperty] as string);
				}
				return this.queueLogs;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0004B123 File Offset: 0x00049323
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TransportQueueStatistics.SchemaObject;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0004B12A File Offset: 0x0004932A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x04000E34 RID: 3636
		private static readonly TransportQueueStatisticsSchema SchemaObject = ObjectSchema.GetInstance<TransportQueueStatisticsSchema>();

		// Token: 0x04000E35 RID: 3637
		private readonly Guid identity = CombGuidGenerator.NewGuid();

		// Token: 0x04000E36 RID: 3638
		private MultiValuedProperty<TransportQueueLog> queueLogs;
	}
}
