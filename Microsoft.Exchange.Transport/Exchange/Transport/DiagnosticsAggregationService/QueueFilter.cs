using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Net.DiagnosticsAggregation;
using Microsoft.Exchange.Transport.QueueViewer;

namespace Microsoft.Exchange.Transport.DiagnosticsAggregationService
{
	// Token: 0x0200035E RID: 862
	internal class QueueFilter : IQueueFilter
	{
		// Token: 0x06002550 RID: 9552 RVA: 0x0009082D File Offset: 0x0008EA2D
		private QueueFilter(PagingEngine<ExtensibleQueueInfo, ExtensibleQueueInfoSchema> pagingEngine)
		{
			this.pagingEngine = pagingEngine;
		}

		// Token: 0x06002551 RID: 9553 RVA: 0x0009083C File Offset: 0x0008EA3C
		public static bool TryParse(string filterString, out IQueueFilter result)
		{
			ParsingException ex;
			return QueueFilter.TryParse(filterString, out result, out ex);
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x00090854 File Offset: 0x0008EA54
		public static bool TryParse(string filterString, out IQueueFilter result, out ParsingException parsingException)
		{
			result = null;
			parsingException = null;
			if (string.IsNullOrEmpty(filterString))
			{
				result = new NullQueueFilter();
				return true;
			}
			QueryFilter filter;
			try
			{
				MonadFilter monadFilter = new MonadFilter(filterString, null, ObjectSchema.GetInstance<ExtensibleQueueInfoSchema>());
				filter = DateTimeConverter.ConvertQueryFilter(monadFilter.InnerFilter);
			}
			catch (ParsingException ex)
			{
				parsingException = ex;
				return false;
			}
			PagingEngine<ExtensibleQueueInfo, ExtensibleQueueInfoSchema> pagingEngine = new PagingEngine<ExtensibleQueueInfo, ExtensibleQueueInfoSchema>();
			pagingEngine.SetFilter(filter);
			result = new QueueFilter(pagingEngine);
			return true;
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000908C8 File Offset: 0x0008EAC8
		public bool Match(LocalQueueInfo localQueue)
		{
			ExtensibleQueueInfo dataObject;
			bool flag = QueueFilter.TryCreateExtensibleQueueInfo(localQueue, out dataObject);
			return flag && this.pagingEngine.ApplyFilterConditions(dataObject);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x000908F0 File Offset: 0x0008EAF0
		private static bool TryCreateExtensibleQueueInfo(LocalQueueInfo localQueue, out ExtensibleQueueInfo result)
		{
			result = null;
			ExtensibleQueueInfo extensibleQueueInfo = new PropertyBagBasedQueueInfo(new QueueIdentity(QueueType.Delivery, 1L, localQueue.NextHopDomain));
			extensibleQueueInfo.NextHopDomain = localQueue.NextHopDomain;
			extensibleQueueInfo.MessageCount = localQueue.MessageCount;
			extensibleQueueInfo.DeferredMessageCount = localQueue.DeferredMessageCount;
			extensibleQueueInfo.LockedMessageCount = localQueue.LockedMessageCount;
			extensibleQueueInfo.IncomingRate = localQueue.IncomingRate;
			extensibleQueueInfo.OutgoingRate = localQueue.OutgoingRate;
			extensibleQueueInfo.Velocity = localQueue.Velocity;
			extensibleQueueInfo.LastError = localQueue.LastError;
			extensibleQueueInfo.TlsDomain = localQueue.TlsDomain;
			extensibleQueueInfo.NextHopConnector = localQueue.NextHopConnector;
			NextHopCategory nextHopCategory;
			if (!Enum.TryParse<NextHopCategory>(localQueue.NextHopCategory, out nextHopCategory))
			{
				return false;
			}
			extensibleQueueInfo.NextHopCategory = nextHopCategory;
			DeliveryType deliveryType;
			if (!Enum.TryParse<DeliveryType>(localQueue.DeliveryType, out deliveryType))
			{
				return false;
			}
			extensibleQueueInfo.DeliveryType = deliveryType;
			RiskLevel riskLevel;
			if (!Enum.TryParse<RiskLevel>(localQueue.RiskLevel, out riskLevel))
			{
				return false;
			}
			extensibleQueueInfo.RiskLevel = riskLevel;
			QueueStatus status;
			if (!Enum.TryParse<QueueStatus>(localQueue.Status, out status))
			{
				return false;
			}
			extensibleQueueInfo.Status = status;
			result = extensibleQueueInfo;
			return true;
		}

		// Token: 0x04001351 RID: 4945
		private PagingEngine<ExtensibleQueueInfo, ExtensibleQueueInfoSchema> pagingEngine;
	}
}
