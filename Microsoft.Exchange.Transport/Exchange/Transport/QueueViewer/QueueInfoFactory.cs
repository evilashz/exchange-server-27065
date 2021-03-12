using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Transport.MessageDepot;
using Microsoft.Exchange.Transport.RemoteDelivery;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport.QueueViewer
{
	// Token: 0x0200035F RID: 863
	internal static class QueueInfoFactory
	{
		// Token: 0x06002555 RID: 9557 RVA: 0x000909FC File Offset: 0x0008EBFC
		internal static ExtensibleQueueInfo NewDeliveryQueueInfo(RoutedMessageQueue routedMessageQueue)
		{
			ExtensibleQueueInfo extensibleQueueInfo = new PropertyBagBasedQueueInfo(new QueueIdentity(QueueType.Delivery, routedMessageQueue.Id, routedMessageQueue.Key.NextHopDomain));
			extensibleQueueInfo.DeliveryType = routedMessageQueue.Key.NextHopType.DeliveryType;
			extensibleQueueInfo.NextHopCategory = routedMessageQueue.Key.NextHopType.NextHopCategory;
			extensibleQueueInfo.NextHopConnector = routedMessageQueue.Key.NextHopConnector;
			DateTime value;
			if (routedMessageQueue.Suspended)
			{
				extensibleQueueInfo.Status = QueueStatus.Suspended;
			}
			else if (routedMessageQueue.ActiveConnections > 0)
			{
				extensibleQueueInfo.Status = QueueStatus.Active;
			}
			else if (routedMessageQueue.AttemptingConnections > 0)
			{
				extensibleQueueInfo.Status = QueueStatus.Connecting;
			}
			else if (routedMessageQueue.GetRetryConnectionSchedule(out value))
			{
				extensibleQueueInfo.Status = QueueStatus.Retry;
				extensibleQueueInfo.NextRetryTime = new DateTime?(value);
			}
			else
			{
				extensibleQueueInfo.Status = QueueStatus.Ready;
			}
			DateTime firstRetryTime = routedMessageQueue.FirstRetryTime;
			if (firstRetryTime != DateTime.MinValue)
			{
				extensibleQueueInfo.FirstRetryTime = new DateTime?(firstRetryTime);
			}
			extensibleQueueInfo.RetryCount = routedMessageQueue.ConnectionRetryCount;
			extensibleQueueInfo.MessageCount = routedMessageQueue.CountNotDeleted + routedMessageQueue.InFlightMessages;
			extensibleQueueInfo.DeferredMessageCount = routedMessageQueue.DeferredCount;
			extensibleQueueInfo.LockedMessageCount = routedMessageQueue.LockedCount;
			int[] messageCountsPerPriority;
			int[] deferredMessageCountsPerPriority;
			routedMessageQueue.GetQueueCountsOnlyForIndividualPriorities(out messageCountsPerPriority, out deferredMessageCountsPerPriority);
			extensibleQueueInfo.MessageCountsPerPriority = messageCountsPerPriority;
			extensibleQueueInfo.DeferredMessageCountsPerPriority = deferredMessageCountsPerPriority;
			extensibleQueueInfo.LastError = QueueInfoFactory.GetLastErrorString(routedMessageQueue);
			DateTime lastRetryTime = routedMessageQueue.LastRetryTime;
			if (lastRetryTime != DateTime.MinValue)
			{
				extensibleQueueInfo.LastRetryTime = new DateTime?(lastRetryTime);
			}
			extensibleQueueInfo.TlsDomain = QueueInfoFactory.GetTlsDomain(routedMessageQueue, routedMessageQueue.Key.NextHopTlsDomain);
			extensibleQueueInfo.RiskLevel = routedMessageQueue.Key.RiskLevel;
			extensibleQueueInfo.OutboundIPPool = routedMessageQueue.Key.OutboundIPPool;
			extensibleQueueInfo.OverrideSource = routedMessageQueue.Key.OverrideSource;
			extensibleQueueInfo.IncomingRate = Math.Round(routedMessageQueue.IncomingRate, 2);
			extensibleQueueInfo.OutgoingRate = Math.Round(routedMessageQueue.OutgoingRate, 2);
			extensibleQueueInfo.Velocity = extensibleQueueInfo.OutgoingRate - extensibleQueueInfo.IncomingRate;
			return extensibleQueueInfo;
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00090C08 File Offset: 0x0008EE08
		private static string GetTlsDomain(RoutedMessageQueue routedMessageQueue, string tlsDomain)
		{
			string text = tlsDomain;
			SmtpSendConnectorConfig smtpSendConnectorConfig;
			if (text == null && routedMessageQueue.Key.NextHopConnector != Guid.Empty && Components.RoutingComponent.MailRouter.TryGetLocalSendConnector<SmtpSendConnectorConfig>(routedMessageQueue.Key.NextHopConnector, out smtpSendConnectorConfig) && smtpSendConnectorConfig.RequireTLS && smtpSendConnectorConfig.TlsAuthLevel == TlsAuthLevel.DomainValidation)
			{
				SmtpDomainWithSubdomains tlsDomain2 = smtpSendConnectorConfig.TlsDomain;
				if (tlsDomain2 != null)
				{
					text = tlsDomain2.ToString();
				}
				else
				{
					string text2 = "*." + routedMessageQueue.Key.NextHopDomain;
					SmtpDomainWithSubdomains smtpDomainWithSubdomains;
					if (SmtpDomainWithSubdomains.TryParse(text2, out smtpDomainWithSubdomains))
					{
						text = text2;
					}
				}
			}
			return text;
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00090CC0 File Offset: 0x0008EEC0
		internal static ExtensibleQueueInfo NewShadowQueueInfo(ShadowMessageQueue shadowMessageQueue)
		{
			ExtensibleQueueInfo extensibleQueueInfo = new PropertyBagBasedQueueInfo(new QueueIdentity(QueueType.Shadow, shadowMessageQueue.Id, shadowMessageQueue.Key.NextHopDomain));
			if (shadowMessageQueue.Suspended)
			{
				extensibleQueueInfo.Status = QueueStatus.Suspended;
			}
			else if (shadowMessageQueue.IsResubmissionSuppressed)
			{
				extensibleQueueInfo.Status = QueueStatus.Retry;
			}
			else
			{
				extensibleQueueInfo.Status = QueueStatus.Ready;
			}
			extensibleQueueInfo.MessageCount = shadowMessageQueue.Count;
			extensibleQueueInfo.DeliveryType = DeliveryType.ShadowRedundancy;
			extensibleQueueInfo.NextHopConnector = shadowMessageQueue.Key.NextHopConnector;
			extensibleQueueInfo.LastError = string.Empty;
			return extensibleQueueInfo;
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00090D4C File Offset: 0x0008EF4C
		internal static ExtensibleQueueInfo NewPoisonQueueInfo()
		{
			ExtensibleQueueInfo extensibleQueueInfo = new PropertyBagBasedQueueInfo(QueueIdentity.PoisonQueueIdentity);
			extensibleQueueInfo.DeliveryType = DeliveryType.Undefined;
			extensibleQueueInfo.NextHopConnector = Guid.Empty;
			extensibleQueueInfo.Status = QueueStatus.Ready;
			if (Components.MessageDepotQueueViewerComponent.Enabled)
			{
				IMessageDepotQueueViewer messageDepotQueueViewer = Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer;
				extensibleQueueInfo.MessageCount = Convert.ToInt32(messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Poisoned));
			}
			else
			{
				extensibleQueueInfo.MessageCount = Components.QueueManager.PoisonMessageQueue.Count;
			}
			return extensibleQueueInfo;
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00090DC0 File Offset: 0x0008EFC0
		internal static ExtensibleQueueInfo NewSubmissionQueueInfo()
		{
			ExtensibleQueueInfo extensibleQueueInfo = new PropertyBagBasedQueueInfo(QueueIdentity.SubmissionQueueIdentity);
			extensibleQueueInfo.DeliveryType = DeliveryType.Undefined;
			extensibleQueueInfo.NextHopCategory = NextHopCategory.Internal;
			extensibleQueueInfo.NextHopConnector = Guid.Empty;
			if (Components.MessageDepotQueueViewerComponent.Enabled)
			{
				extensibleQueueInfo.Status = (Components.ProcessingSchedulerComponent.ProcessingSchedulerAdmin.IsRunning ? QueueStatus.Ready : QueueStatus.Suspended);
				IMessageDepotQueueViewer messageDepotQueueViewer = Components.MessageDepotQueueViewerComponent.MessageDepotQueueViewer;
				extensibleQueueInfo.MessageCount = Convert.ToInt32(messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Ready)) + Convert.ToInt32(messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Expiring)) + Convert.ToInt32(messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Processing)) + Convert.ToInt32(messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Suspended));
				extensibleQueueInfo.DeferredMessageCount = Convert.ToInt32(messageDepotQueueViewer.GetCount(MessageDepotItemStage.Submission, MessageDepotItemState.Deferred));
				extensibleQueueInfo.LockedMessageCount = 0;
				extensibleQueueInfo.IncomingRate = 0.0;
				extensibleQueueInfo.OutgoingRate = 0.0;
				extensibleQueueInfo.Velocity = 0.0;
			}
			else
			{
				extensibleQueueInfo.Status = (Components.CategorizerComponent.SubmitMessageQueue.Suspended ? QueueStatus.Suspended : QueueStatus.Ready);
				extensibleQueueInfo.MessageCount = Components.CategorizerComponent.GetMailItemCount();
				extensibleQueueInfo.DeferredMessageCount = Components.CategorizerComponent.SubmitMessageQueue.DeferredCount;
				extensibleQueueInfo.LockedMessageCount = Components.CategorizerComponent.SubmitMessageQueue.LockedCount;
				extensibleQueueInfo.IncomingRate = Math.Round(Components.CategorizerComponent.SubmitMessageQueue.IncomingRate, 2);
				extensibleQueueInfo.OutgoingRate = Math.Round(Components.CategorizerComponent.SubmitMessageQueue.OutgoingRate, 2);
				extensibleQueueInfo.Velocity = extensibleQueueInfo.OutgoingRate - extensibleQueueInfo.IncomingRate;
			}
			return extensibleQueueInfo;
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00090F50 File Offset: 0x0008F150
		internal static ExtensibleQueueInfo NewUnreachableQueueInfo()
		{
			ExtensibleQueueInfo extensibleQueueInfo = new PropertyBagBasedQueueInfo(QueueIdentity.UnreachableQueueIdentity);
			extensibleQueueInfo.DeliveryType = DeliveryType.Unreachable;
			extensibleQueueInfo.NextHopConnector = Guid.Empty;
			UnreachableMessageQueue unreachableMessageQueue = RemoteDeliveryComponent.UnreachableMessageQueue;
			extensibleQueueInfo.Status = (unreachableMessageQueue.Suspended ? QueueStatus.Suspended : QueueStatus.Ready);
			extensibleQueueInfo.MessageCount = unreachableMessageQueue.CountNotDeleted;
			return extensibleQueueInfo;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00090FA0 File Offset: 0x0008F1A0
		private static string GetLastErrorString(RoutedMessageQueue routedMessageQueue)
		{
			if (routedMessageQueue.LastTransientError != null)
			{
				return routedMessageQueue.LastTransientError.ToString();
			}
			return routedMessageQueue.LastError.ToString();
		}
	}
}
