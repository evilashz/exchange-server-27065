using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Data.QueueViewer
{
	// Token: 0x02000281 RID: 641
	[Serializable]
	public abstract class ExtensibleQueueInfo : ConfigurableObject, PagedDataObject, IConfigurable
	{
		// Token: 0x06001680 RID: 5760 RVA: 0x00046F95 File Offset: 0x00045195
		internal ExtensibleQueueInfo(QueueInfoPropertyBag propertyBag) : base(propertyBag)
		{
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x00046F9E File Offset: 0x0004519E
		public QueueIdentity QueueIdentity
		{
			get
			{
				return (QueueIdentity)this.Identity;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00046FAB File Offset: 0x000451AB
		public string[] PriorityDescriptions
		{
			get
			{
				return EnumUtils.DeliveryPriorityEnumNames;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x00046FB2 File Offset: 0x000451B2
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ExtensibleQueueInfo.schema;
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00046FBC File Offset: 0x000451BC
		public void ConvertDatesToLocalTime()
		{
			if (this.LastRetryTime != null)
			{
				this.LastRetryTime = new DateTime?(this.LastRetryTime.Value.ToLocalTime());
			}
			if (this.NextRetryTime != null)
			{
				this.NextRetryTime = new DateTime?(this.NextRetryTime.Value.ToLocalTime());
			}
			if (this.FirstRetryTime != null)
			{
				this.FirstRetryTime = new DateTime?(this.FirstRetryTime.Value.ToLocalTime());
			}
			string lastError;
			if (!string.IsNullOrEmpty(this.LastError) && Microsoft.Exchange.Extensibility.Internal.LastError.TryConvertLastRetryTimeToLocalTime(this.LastError, out lastError))
			{
				this.LastError = lastError;
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00047088 File Offset: 0x00045288
		public void ConvertDatesToUniversalTime()
		{
			if (this.LastRetryTime != null)
			{
				this.LastRetryTime = new DateTime?(this.LastRetryTime.Value.ToUniversalTime());
			}
			if (this.NextRetryTime != null)
			{
				this.NextRetryTime = new DateTime?(this.NextRetryTime.Value.ToUniversalTime());
			}
			string lastError;
			if (!string.IsNullOrEmpty(this.LastError) && Microsoft.Exchange.Extensibility.Internal.LastError.TryConvertLastRetryTimeToUniversalTime(this.LastError, out lastError))
			{
				this.LastError = lastError;
			}
		}

		// Token: 0x06001686 RID: 5766
		public abstract bool IsDeliveryQueue();

		// Token: 0x06001687 RID: 5767
		public abstract bool IsSubmissionQueue();

		// Token: 0x06001688 RID: 5768
		public abstract bool IsPoisonQueue();

		// Token: 0x06001689 RID: 5769
		public abstract bool IsShadowQueue();

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600168A RID: 5770
		// (set) Token: 0x0600168B RID: 5771
		public abstract DeliveryType DeliveryType { get; internal set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600168C RID: 5772
		// (set) Token: 0x0600168D RID: 5773
		public abstract string NextHopDomain { get; internal set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600168E RID: 5774
		// (set) Token: 0x0600168F RID: 5775
		public abstract string TlsDomain { get; internal set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001690 RID: 5776
		// (set) Token: 0x06001691 RID: 5777
		public abstract Guid NextHopConnector { get; internal set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001692 RID: 5778
		// (set) Token: 0x06001693 RID: 5779
		public abstract QueueStatus Status { get; internal set; }

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001694 RID: 5780
		// (set) Token: 0x06001695 RID: 5781
		public abstract int MessageCount { get; internal set; }

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001696 RID: 5782
		// (set) Token: 0x06001697 RID: 5783
		public abstract int[] MessageCountsPerPriority { get; internal set; }

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001698 RID: 5784
		// (set) Token: 0x06001699 RID: 5785
		public abstract string LastError { get; internal set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600169A RID: 5786
		// (set) Token: 0x0600169B RID: 5787
		public abstract int RetryCount { get; internal set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x0600169C RID: 5788
		// (set) Token: 0x0600169D RID: 5789
		public abstract DateTime? LastRetryTime { get; internal set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600169E RID: 5790
		// (set) Token: 0x0600169F RID: 5791
		public abstract DateTime? NextRetryTime { get; internal set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060016A0 RID: 5792
		// (set) Token: 0x060016A1 RID: 5793
		public abstract DateTime? FirstRetryTime { get; internal set; }

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060016A2 RID: 5794
		// (set) Token: 0x060016A3 RID: 5795
		public abstract int DeferredMessageCount { get; internal set; }

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060016A4 RID: 5796
		// (set) Token: 0x060016A5 RID: 5797
		public abstract int[] DeferredMessageCountsPerPriority { get; internal set; }

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060016A6 RID: 5798
		// (set) Token: 0x060016A7 RID: 5799
		public abstract int LockedMessageCount { get; internal set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060016A8 RID: 5800
		// (set) Token: 0x060016A9 RID: 5801
		public abstract RiskLevel RiskLevel { get; internal set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060016AA RID: 5802
		// (set) Token: 0x060016AB RID: 5803
		public abstract int OutboundIPPool { get; internal set; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060016AC RID: 5804
		// (set) Token: 0x060016AD RID: 5805
		public abstract NextHopCategory NextHopCategory { get; internal set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060016AE RID: 5806
		// (set) Token: 0x060016AF RID: 5807
		public abstract double IncomingRate { get; internal set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060016B0 RID: 5808
		// (set) Token: 0x060016B1 RID: 5809
		public abstract double OutgoingRate { get; internal set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060016B2 RID: 5810
		// (set) Token: 0x060016B3 RID: 5811
		public abstract double Velocity { get; internal set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060016B4 RID: 5812
		// (set) Token: 0x060016B5 RID: 5813
		public abstract string OverrideSource { get; internal set; }

		// Token: 0x04000D24 RID: 3364
		private static ExtensibleQueueInfoSchema schema = ObjectSchema.GetInstance<ExtensibleQueueInfoSchema>();
	}
}
