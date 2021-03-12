using System;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Data.QueueDigest
{
	// Token: 0x02000283 RID: 643
	[Serializable]
	public class QueueDigestDetails
	{
		// Token: 0x060016EC RID: 5868 RVA: 0x00047C1E File Offset: 0x00045E1E
		internal QueueDigestDetails(AggregatedQueueNormalDetails details)
		{
			this.QueueIdentity = details.QueueIdentity;
			this.ServerIdentity = details.ServerIdentity;
			this.MessageCount = details.MessageCount;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00047C4C File Offset: 0x00045E4C
		internal QueueDigestDetails(AggregatedQueueVerboseDetails details)
		{
			this.QueueIdentity = details.QueueIdentity;
			this.ServerIdentity = details.ServerIdentity;
			this.MessageCount = details.MessageCount;
			this.DeferredMessageCount = new int?(details.DeferredMessageCount);
			this.LockedMessageCount = new int?(details.LockedMessageCount);
			this.IncomingRate = new double?(details.IncomingRate);
			this.OutgoingRate = new double?(details.OutgoingRate);
			this.Velocity = new double?(details.Velocity);
			this.NextHopDomain = details.NextHopDomain;
			this.NextHopCategory = details.NextHopCategory;
			this.DeliveryType = details.DeliveryType;
			this.Status = details.Status;
			this.RiskLevel = details.RiskLevel;
			this.OutboundIPPool = details.OutboundIPPool;
			this.LastError = details.LastError;
			this.NextHopConnector = new Guid?(details.NextHopConnector);
			this.TlsDomain = details.TlsDomain;
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00047D4C File Offset: 0x00045F4C
		internal QueueDigestDetails(TransportQueueLog details)
		{
			this.QueueIdentity = details.QueueName;
			this.MessageCount = details.MessageCount;
			this.DeferredMessageCount = new int?(details.DeferredMessageCount);
			this.LockedMessageCount = new int?(details.LockedMessageCount);
			this.IncomingRate = new double?(details.IncomingRate);
			this.OutgoingRate = new double?(details.OutgoingRate);
			this.Velocity = new double?(details.Velocity);
			this.NextHopDomain = details.NextHopDomain;
			this.NextHopCategory = details.NextHopCategory;
			this.DeliveryType = details.DeliveryType;
			this.Status = details.Status;
			this.RiskLevel = details.RiskLevel;
			this.OutboundIPPool = details.OutboundIPPool;
			this.LastError = details.LastError;
			this.NextHopConnector = new Guid?(details.NextHopConnector);
			this.TlsDomain = details.TlsDomain;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00047E3D File Offset: 0x0004603D
		// (set) Token: 0x060016F0 RID: 5872 RVA: 0x00047E45 File Offset: 0x00046045
		public string QueueIdentity { get; private set; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00047E4E File Offset: 0x0004604E
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00047E56 File Offset: 0x00046056
		public string ServerIdentity { get; private set; }

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00047E5F File Offset: 0x0004605F
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00047E67 File Offset: 0x00046067
		public int MessageCount { get; private set; }

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00047E70 File Offset: 0x00046070
		// (set) Token: 0x060016F6 RID: 5878 RVA: 0x00047E78 File Offset: 0x00046078
		public int? DeferredMessageCount { get; private set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00047E81 File Offset: 0x00046081
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x00047E89 File Offset: 0x00046089
		public int? LockedMessageCount { get; private set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00047E92 File Offset: 0x00046092
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x00047E9A File Offset: 0x0004609A
		public double? IncomingRate { get; private set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060016FB RID: 5883 RVA: 0x00047EA3 File Offset: 0x000460A3
		// (set) Token: 0x060016FC RID: 5884 RVA: 0x00047EAB File Offset: 0x000460AB
		public double? OutgoingRate { get; private set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00047EB4 File Offset: 0x000460B4
		// (set) Token: 0x060016FE RID: 5886 RVA: 0x00047EBC File Offset: 0x000460BC
		public double? Velocity { get; private set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00047EC5 File Offset: 0x000460C5
		// (set) Token: 0x06001700 RID: 5888 RVA: 0x00047ECD File Offset: 0x000460CD
		public string NextHopDomain { get; private set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x00047ED6 File Offset: 0x000460D6
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x00047EDE File Offset: 0x000460DE
		public string NextHopCategory { get; private set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00047EE7 File Offset: 0x000460E7
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x00047EEF File Offset: 0x000460EF
		public Guid? NextHopConnector { get; private set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x00047EF8 File Offset: 0x000460F8
		// (set) Token: 0x06001706 RID: 5894 RVA: 0x00047F00 File Offset: 0x00046100
		public string DeliveryType { get; private set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x00047F09 File Offset: 0x00046109
		// (set) Token: 0x06001708 RID: 5896 RVA: 0x00047F11 File Offset: 0x00046111
		public string Status { get; private set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x00047F1A File Offset: 0x0004611A
		// (set) Token: 0x0600170A RID: 5898 RVA: 0x00047F22 File Offset: 0x00046122
		public string RiskLevel { get; private set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00047F2B File Offset: 0x0004612B
		// (set) Token: 0x0600170C RID: 5900 RVA: 0x00047F33 File Offset: 0x00046133
		public string OutboundIPPool { get; private set; }

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x00047F3C File Offset: 0x0004613C
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x00047F44 File Offset: 0x00046144
		public string LastError { get; private set; }

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00047F4D File Offset: 0x0004614D
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x00047F55 File Offset: 0x00046155
		public string TlsDomain { get; private set; }

		// Token: 0x06001711 RID: 5905 RVA: 0x00047F5E File Offset: 0x0004615E
		public override string ToString()
		{
			return this.QueueIdentity;
		}
	}
}
