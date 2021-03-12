using System;
using System.Collections.Generic;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Data.QueueDigest
{
	// Token: 0x02000284 RID: 644
	[Serializable]
	public class QueueDigestPresentationObject : ConfigurableObject
	{
		// Token: 0x06001712 RID: 5906 RVA: 0x00047F66 File Offset: 0x00046166
		public QueueDigestPresentationObject() : base(new SimpleProviderPropertyBag())
		{
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x00047F73 File Offset: 0x00046173
		// (set) Token: 0x06001714 RID: 5908 RVA: 0x00047F7B File Offset: 0x0004617B
		public string GroupByValue { get; set; }

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x00047F84 File Offset: 0x00046184
		// (set) Token: 0x06001716 RID: 5910 RVA: 0x00047F8C File Offset: 0x0004618C
		public int MessageCount { get; set; }

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x00047F95 File Offset: 0x00046195
		// (set) Token: 0x06001718 RID: 5912 RVA: 0x00047F9D File Offset: 0x0004619D
		public int DeferredMessageCount { get; set; }

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x00047FA6 File Offset: 0x000461A6
		// (set) Token: 0x0600171A RID: 5914 RVA: 0x00047FAE File Offset: 0x000461AE
		public int LockedMessageCount { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600171B RID: 5915 RVA: 0x00047FB7 File Offset: 0x000461B7
		// (set) Token: 0x0600171C RID: 5916 RVA: 0x00047FBF File Offset: 0x000461BF
		public int StaleMessageCount { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x00047FC8 File Offset: 0x000461C8
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x00047FD0 File Offset: 0x000461D0
		public double IncomingRate { get; set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x00047FD9 File Offset: 0x000461D9
		// (set) Token: 0x06001720 RID: 5920 RVA: 0x00047FE1 File Offset: 0x000461E1
		public double OutgoingRate { get; set; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x00047FEA File Offset: 0x000461EA
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x00047FF2 File Offset: 0x000461F2
		public List<QueueDigestDetails> Details { get; set; }

		// Token: 0x06001723 RID: 5923 RVA: 0x00047FFC File Offset: 0x000461FC
		internal static QueueDigestPresentationObject Create(AggregatedQueueInfo aggregatedQueueInfo)
		{
			QueueDigestPresentationObject queueDigestPresentationObject = new QueueDigestPresentationObject();
			queueDigestPresentationObject.GroupByValue = aggregatedQueueInfo.GroupByValue;
			queueDigestPresentationObject.MessageCount = aggregatedQueueInfo.MessageCount;
			queueDigestPresentationObject.DeferredMessageCount = aggregatedQueueInfo.DeferredMessageCount;
			queueDigestPresentationObject.LockedMessageCount = aggregatedQueueInfo.LockedMessageCount;
			queueDigestPresentationObject.StaleMessageCount = aggregatedQueueInfo.StaleMessageCount;
			queueDigestPresentationObject.IncomingRate = aggregatedQueueInfo.IncomingRate;
			queueDigestPresentationObject.OutgoingRate = aggregatedQueueInfo.OutgoingRate;
			queueDigestPresentationObject.Details = new List<QueueDigestDetails>();
			if (aggregatedQueueInfo.NormalDetails != null && aggregatedQueueInfo.NormalDetails.Count > 0)
			{
				using (List<AggregatedQueueNormalDetails>.Enumerator enumerator = aggregatedQueueInfo.NormalDetails.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						AggregatedQueueNormalDetails details = enumerator.Current;
						queueDigestPresentationObject.Details.Add(new QueueDigestDetails(details));
					}
					goto IL_112;
				}
			}
			if (aggregatedQueueInfo.VerboseDetails != null && aggregatedQueueInfo.VerboseDetails.Count > 0)
			{
				foreach (AggregatedQueueVerboseDetails details2 in aggregatedQueueInfo.VerboseDetails)
				{
					queueDigestPresentationObject.Details.Add(new QueueDigestDetails(details2));
				}
			}
			IL_112:
			queueDigestPresentationObject.Details.Sort(new Comparison<QueueDigestDetails>(QueueDigestPresentationObject.CompareQueueDigestDetails));
			return queueDigestPresentationObject;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00048150 File Offset: 0x00046350
		internal static QueueDigestPresentationObject Create(TransportQueueStatistics mtrtQueueAggregate, QueueDigestGroupBy groupBy)
		{
			QueueDigestPresentationObject queueDigestPresentationObject = new QueueDigestPresentationObject();
			queueDigestPresentationObject.DeferredMessageCount = mtrtQueueAggregate.DeferredMessageCount;
			queueDigestPresentationObject.IncomingRate = mtrtQueueAggregate.IncomingRate;
			queueDigestPresentationObject.LockedMessageCount = mtrtQueueAggregate.LockedMessageCount;
			queueDigestPresentationObject.MessageCount = mtrtQueueAggregate.MessageCount;
			queueDigestPresentationObject.OutgoingRate = mtrtQueueAggregate.OutgoingRate;
			QueueDigestPresentationObject.SetGroupByValue(queueDigestPresentationObject, mtrtQueueAggregate, groupBy);
			if (mtrtQueueAggregate.QueueLogs != null)
			{
				queueDigestPresentationObject.Details = new List<QueueDigestDetails>();
				foreach (TransportQueueLog details in mtrtQueueAggregate.QueueLogs)
				{
					queueDigestPresentationObject.Details.Add(new QueueDigestDetails(details));
				}
			}
			queueDigestPresentationObject.Details.Sort(new Comparison<QueueDigestDetails>(QueueDigestPresentationObject.CompareQueueDigestDetails));
			return queueDigestPresentationObject;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00048224 File Offset: 0x00046424
		private static int CompareQueueDigestDetails(QueueDigestDetails lhs, QueueDigestDetails rhs)
		{
			return rhs.MessageCount - lhs.MessageCount;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00048234 File Offset: 0x00046434
		private static void SetGroupByValue(QueueDigestPresentationObject result, TransportQueueStatistics mtrtResponse, QueueDigestGroupBy groupBy)
		{
			string groupByValue = string.Empty;
			switch (groupBy)
			{
			case QueueDigestGroupBy.NextHopDomain:
				groupByValue = mtrtResponse.NextHopDomain;
				break;
			case QueueDigestGroupBy.NextHopCategory:
				groupByValue = mtrtResponse.NextHopCategory;
				break;
			case QueueDigestGroupBy.NextHopKey:
				groupByValue = mtrtResponse.NextHopKey;
				break;
			case QueueDigestGroupBy.DeliveryType:
				groupByValue = mtrtResponse.DeliveryType;
				break;
			case QueueDigestGroupBy.Status:
				groupByValue = mtrtResponse.Status;
				break;
			case QueueDigestGroupBy.RiskLevel:
				groupByValue = mtrtResponse.RiskLevel;
				break;
			case QueueDigestGroupBy.LastError:
				groupByValue = mtrtResponse.LastError;
				break;
			case QueueDigestGroupBy.ServerName:
				groupByValue = mtrtResponse.ServerName;
				break;
			case QueueDigestGroupBy.OutboundIPPool:
				groupByValue = mtrtResponse.OutboundIPPool;
				break;
			default:
				throw new ArgumentOutOfRangeException("groupBy");
			}
			result.GroupByValue = groupByValue;
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x000482D8 File Offset: 0x000464D8
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return QueueDigestPresentationObject.schema;
			}
		}

		// Token: 0x04000D38 RID: 3384
		private static QueueDigestPresentationObjectSchema schema = ObjectSchema.GetInstance<QueueDigestPresentationObjectSchema>();
	}
}
