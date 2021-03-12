using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000146 RID: 326
	internal class NextHopConnection
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x00038018 File Offset: 0x00036218
		public NextHopConnection(RoutedMessageQueue routedMessageQueue, long connectionId, DeliveryPriority priority, ConnectionManager parent)
		{
			this.routedMessageQueue = routedMessageQueue;
			this.connectionId = connectionId;
			this.parent = parent;
			this.priority = priority;
			if (this.routedMessageQueue != null && this.routedMessageQueue.Key.NextHopType.DeliveryType == DeliveryType.SmtpDeliveryToMailbox)
			{
				this.generateSuccessDSNs = DsnFlags.Delivery;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00038081 File Offset: 0x00036281
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x00038089 File Offset: 0x00036289
		public virtual DsnFlags GenerateSuccessDSNs
		{
			get
			{
				return this.generateSuccessDSNs;
			}
			set
			{
				this.generateSuccessDSNs = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00038092 File Offset: 0x00036292
		public virtual NextHopSolutionKey Key
		{
			get
			{
				return this.routedMessageQueue.Key;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0003809F File Offset: 0x0003629F
		public virtual int ActiveQueueLength
		{
			get
			{
				return this.routedMessageQueue.ActiveQueueLength;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000380AC File Offset: 0x000362AC
		public virtual int TotalQueueLength
		{
			get
			{
				return this.routedMessageQueue.TotalQueueLength;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000380B9 File Offset: 0x000362B9
		public virtual IReadOnlyMailItem ReadOnlyMailItem
		{
			get
			{
				return this.RoutedMailItem;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x000380C1 File Offset: 0x000362C1
		public virtual RoutedMailItem RoutedMailItem
		{
			get
			{
				return this.routedMailItem;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x000380C9 File Offset: 0x000362C9
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x000380D1 File Offset: 0x000362D1
		public virtual int MaxMessageRecipients
		{
			get
			{
				return this.maxMessageRecipients;
			}
			set
			{
				this.maxMessageRecipients = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x000380DC File Offset: 0x000362DC
		public virtual int RecipientCount
		{
			get
			{
				if (this.readyRecipients == null)
				{
					return 0;
				}
				int num = 0;
				foreach (MailRecipient mailRecipient in this.readyRecipients)
				{
					if (mailRecipient.Status == Status.Ready)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0003813C File Offset: 0x0003633C
		public virtual IEnumerable<MailRecipient> ReadyRecipients
		{
			get
			{
				return this.readyRecipients;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00038144 File Offset: 0x00036344
		public virtual IList<MailRecipient> ReadyRecipientsList
		{
			get
			{
				return this.readyRecipients;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x0003814C File Offset: 0x0003634C
		public bool RetryQueueRequested
		{
			get
			{
				return this.retryQueueRequested;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00038154 File Offset: 0x00036354
		public SmtpResponse RetryQueueSmtpResponse
		{
			get
			{
				return this.retryQueueSmtpResponse;
			}
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003815C File Offset: 0x0003635C
		public virtual void ConnectionAttemptSucceeded()
		{
			if (!this.isConnectionAttemptSucceeded)
			{
				this.routedMessageQueue.ConnectionAttemptSucceeded(this.priority, this.connectionId);
				this.isConnectionAttemptSucceeded = true;
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00038184 File Offset: 0x00036384
		public virtual RoutedMailItem GetNextRoutedMailItem()
		{
			if (this.routedMailItem == null)
			{
				if (Components.RemoteDeliveryComponent.IsPaused)
				{
					return null;
				}
				this.routedMailItem = this.routedMessageQueue.GetNextMailItem(this.priority);
				if (this.routedMailItem != null)
				{
					this.readyRecipients = this.GetReadyRecipients();
					this.recipientResponses = new Queue<AckStatusAndResponse>();
					this.recipientsSent = new Queue<MailRecipient>();
					this.recipientsPending = 0;
					this.recipientEnumerator = this.readyRecipients.GetEnumerator();
					Components.QueueManager.GetQueuedRecipientsByAge().TrackEnteringSmtpSend(this.routedMailItem);
				}
			}
			return this.routedMailItem;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003821B File Offset: 0x0003641B
		public virtual IReadOnlyMailItem GetNextMailItem()
		{
			return this.GetNextRoutedMailItem();
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x00038224 File Offset: 0x00036424
		public virtual MailRecipient GetNextRecipient()
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2883988797U);
			while (this.recipientEnumerator.MoveNext())
			{
				MailRecipient mailRecipient = this.recipientEnumerator.Current;
				if (mailRecipient.Status == Status.Ready)
				{
					this.recipientsSent.Enqueue(mailRecipient);
					this.recipientsPending++;
					return mailRecipient;
				}
			}
			return null;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00038280 File Offset: 0x00036480
		public virtual void NotifyConnectionFailedOver(string targetHostName, SmtpResponse failoverResponse, SessionSetupFailureReason failoverReason)
		{
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x00038284 File Offset: 0x00036484
		public void AckConnection(AckStatus status, SmtpResponse smtpResponse, AckDetails details)
		{
			this.AckConnection(status, smtpResponse, details, null);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000382A4 File Offset: 0x000364A4
		public void AckConnection(AckStatus status, SmtpResponse smtpResponse, AckDetails details, SessionSetupFailureReason failureReason)
		{
			this.AckConnection(MessageTrackingSource.DNS, null, status, smtpResponse, details, null, false, failureReason);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000382C8 File Offset: 0x000364C8
		public void AckConnection(AckStatus status, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval)
		{
			this.AckConnection(MessageTrackingSource.DNS, status, smtpResponse, details, retryInterval);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x000382D6 File Offset: 0x000364D6
		public void AckConnection(MessageTrackingSource messageTrackingSource, AckStatus status, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval)
		{
			this.AckConnection(messageTrackingSource, null, status, smtpResponse, details, retryInterval);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000382E6 File Offset: 0x000364E6
		public virtual void AckConnection(MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckStatus status, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval)
		{
			this.AckConnection(messageTrackingSource, messageTrackingSourceContext, status, smtpResponse, details, retryInterval, false);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000382F8 File Offset: 0x000364F8
		public void AckConnection(MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckStatus status, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, bool resubmitWithoutHighAvailablityRouting)
		{
			this.AckConnection(messageTrackingSource, messageTrackingSourceContext, status, smtpResponse, details, retryInterval, resubmitWithoutHighAvailablityRouting, SessionSetupFailureReason.None);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00038318 File Offset: 0x00036518
		public virtual void AckConnection(MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckStatus status, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, bool resubmitWithoutHighAvailablityRouting, SessionSetupFailureReason failureReason)
		{
			this.routedMessageQueue.CloseConnection(this.priority, this.connectionId);
			this.parent.DecrementActiveConnections(this.routedMessageQueue.Key.NextHopType.DeliveryType);
			bool flag = NextHopConnection.IsSuccessNoNewConnectionResponse(smtpResponse);
			if (this.routedMailItem != null)
			{
				if (status == AckStatus.Skip)
				{
					throw new InvalidOperationException("NextHopConnection should not hold a message when the ack status is Skip");
				}
				this.AckMailItem(AckStatus.Pending, SmtpResponse.Empty, details, MessageTrackingSource.SMTP, LatencyComponent.SmtpSendConnect, false);
			}
			switch (status)
			{
			case AckStatus.Pending:
			case AckStatus.Success:
				this.routedMessageQueue.ResetConnectionRetryCount();
				if (!flag)
				{
					this.CreateConnectionIfNecessary();
					return;
				}
				break;
			case AckStatus.Retry:
				this.routedMessageQueue.Retry(this.parent.CallBackDelegate, retryInterval, smtpResponse, details);
				return;
			case AckStatus.Fail:
				if (!this.routedMessageQueue.Suspended && this.routedMessageQueue.Key.NextHopType != NextHopType.Heartbeat)
				{
					this.routedMessageQueue.NDRAllMessages(messageTrackingSource, messageTrackingSourceContext, smtpResponse, details);
					this.routedMessageQueue.LastError = smtpResponse;
				}
				break;
			case AckStatus.Expand:
			case AckStatus.Relay:
			case AckStatus.SuccessNoDsn:
			case AckStatus.Quarantine:
				break;
			case AckStatus.Resubmit:
				this.routedMessageQueue.LastError = smtpResponse;
				if (resubmitWithoutHighAvailablityRouting)
				{
					if (!this.Key.IsLocalDeliveryGroupRelay)
					{
						throw new InvalidOperationException("resubmitWithoutHighAvailablityRouting should not be true if the next hop solution is not for high availability");
					}
					this.routedMessageQueue.Resubmit(ResubmitReason.UnreachableSameVersionHubs, null);
					return;
				}
				else
				{
					bool flag2 = this.routedMessageQueue.EvaluateResubmitDueToConfigUpdate(this.parent.CallBackDelegate);
					if (flag2)
					{
						this.routedMessageQueue.Resubmit(ResubmitReason.ConfigUpdate, null);
						return;
					}
				}
				break;
			case AckStatus.Skip:
				this.routedMessageQueue.ResetConnectionRetryCount();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000384B9 File Offset: 0x000366B9
		public virtual void CreateConnectionIfNecessary()
		{
			this.parent.CreateConnectionIfNecessary(this.routedMessageQueue, this.priority);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x000384D4 File Offset: 0x000366D4
		public virtual void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, MessageTrackingSource source, string messageTrackingSourceContext, LatencyComponent deliveryComponent, string remoteMta, bool shadowed, string primaryServer, bool reportEndToEndLatencies)
		{
			this.AckMailItem(ackStatus, smtpResponse, details, null, source, messageTrackingSourceContext, deliveryComponent, remoteMta, shadowed, primaryServer, reportEndToEndLatencies);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00038504 File Offset: 0x00036704
		public virtual void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, MessageTrackingSource source, string messageTrackingSourceContext, LatencyComponent deliveryComponent, string remoteMta, bool shadowed, string primaryServer, bool reportEndToEndLatencies)
		{
			if (this.recipientsPending != 0 && ackStatus == AckStatus.Success)
			{
				throw new InvalidOperationException("Cannot ack message until all pending recipients have been acked");
			}
			DeferReason resubmitDeferReason = DeferReason.None;
			TimeSpan? resubmitDeferInterval = null;
			if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.Hub && this.routedMessageQueue.Key.NextHopType.DeliveryType == DeliveryType.SmtpDeliveryToMailbox && smtpResponse.StatusText != null)
			{
				if (smtpResponse.StatusText.Length == 1 && AckReason.IsMailboxTransportDeliveryPoisonMessageResponse(smtpResponse))
				{
					this.routedMailItem.Poison();
				}
				else if (smtpResponse.StatusText.Length > 1)
				{
					SmtpResponse smtpResponse2;
					AckStatus? ackStatus2;
					TimeSpan? timeSpan;
					this.ProcessMailboxTransportDeliveryResult(smtpResponse, out smtpResponse2, out ackStatus2, out timeSpan);
					smtpResponse = smtpResponse2;
					retryInterval = (timeSpan ?? retryInterval);
					ackStatus = (ackStatus2 ?? ackStatus);
					resubmitDeferReason = DeferReason.ReroutedByStoreDriver;
					resubmitDeferInterval = new TimeSpan?(TimeSpan.FromMinutes(Components.TransportAppConfig.Resolver.DeliverMoveMailboxRetryInterval));
				}
			}
			QueuedRecipientsByAgeToken queuedRecipientsByAgeToken = this.routedMailItem.QueuedRecipientsByAgeToken;
			this.routedMessageQueue.AckMessage(this.routedMailItem, this.recipientResponses, ackStatus, smtpResponse, details, resubmitDeferReason, resubmitDeferInterval, retryInterval, source, messageTrackingSourceContext, deliveryComponent, remoteMta, this.readyRecipients, shadowed, primaryServer, reportEndToEndLatencies);
			Components.QueueManager.GetQueuedRecipientsByAge().TrackExitingSmtpSend(queuedRecipientsByAgeToken);
			this.routedMailItem = null;
			this.readyRecipients = null;
			this.recipientResponses = null;
			this.recipientsSent = null;
			this.recipientEnumerator = null;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00038678 File Offset: 0x00036878
		public virtual void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, MessageTrackingSource source, LatencyComponent deliveryComponent, string remoteMta, bool shadowed, string primaryServer, bool reportEndToEndLatencies)
		{
			this.AckMailItem(ackStatus, smtpResponse, details, source, null, deliveryComponent, remoteMta, shadowed, primaryServer, reportEndToEndLatencies);
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003869C File Offset: 0x0003689C
		public virtual void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, MessageTrackingSource source, LatencyComponent deliveryComponent, bool reportEndToEndLatencies)
		{
			this.AckMailItem(ackStatus, smtpResponse, details, null, source, null, deliveryComponent, reportEndToEndLatencies);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000386C4 File Offset: 0x000368C4
		public virtual void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, MessageTrackingSource source, string messageTrackingSourceContext, LatencyComponent deliveryComponent, bool reportEndToEndLatencies)
		{
			this.AckMailItem(ackStatus, smtpResponse, details, retryInterval, source, messageTrackingSourceContext, deliveryComponent, null, false, string.Empty, reportEndToEndLatencies);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x000386EC File Offset: 0x000368EC
		public virtual void AckRecipient(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			if (this.recipientsPending <= 0)
			{
				throw new InvalidOperationException("AckRecipient called but no recipients left to ack");
			}
			this.recipientsPending--;
			if (ackStatus == AckStatus.Success)
			{
				DsnFlags dsnFlags = this.GenerateSuccessDSNs;
				if (dsnFlags != DsnFlags.None)
				{
					if (dsnFlags == DsnFlags.Relay)
					{
						ackStatus = AckStatus.Relay;
					}
				}
				else
				{
					ackStatus = AckStatus.SuccessNoDsn;
				}
			}
			this.recipientResponses.Enqueue(new AckStatusAndResponse(ackStatus, smtpResponse));
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00038748 File Offset: 0x00036948
		public virtual void ResetQueueLastRetryTimeAndError()
		{
			this.routedMessageQueue.LastRetryTime = DateTime.UtcNow;
			this.routedMessageQueue.LastError = SmtpResponse.Empty;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003876A File Offset: 0x0003696A
		internal void GetQueueCountsOnlyForIndividualPriorities(out int[] activeCount, out int[] retryCount)
		{
			if (this.routedMessageQueue == null)
			{
				activeCount = null;
				retryCount = null;
				return;
			}
			this.routedMessageQueue.GetQueueCountsOnlyForIndividualPriorities(out activeCount, out retryCount);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00038788 File Offset: 0x00036988
		private static bool IsSuccessNoNewConnectionResponse(SmtpResponse smtpResponse)
		{
			return string.Equals(smtpResponse.StatusCode, SmtpResponse.SuccessNoNewConnectionResponse.StatusCode, StringComparison.OrdinalIgnoreCase) && string.Equals(smtpResponse.EnhancedStatusCode, SmtpResponse.SuccessNoNewConnectionResponse.EnhancedStatusCode, StringComparison.OrdinalIgnoreCase) && smtpResponse.StatusText != null && smtpResponse.StatusText.Length != 0 && string.Equals(smtpResponse.StatusText[0], SmtpResponse.SuccessNoNewConnectionResponse.StatusText[0], StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00038810 File Offset: 0x00036A10
		private void ProcessMailboxTransportDeliveryResult(SmtpResponse smtpResponse, out SmtpResponse messageLevelSmtpResponse, out AckStatus? messageLevelAckStatus, out TimeSpan? messageLevelRetryInterval)
		{
			messageLevelSmtpResponse = SmtpResponse.Empty;
			messageLevelAckStatus = null;
			messageLevelRetryInterval = null;
			MailboxTransportDeliveryResult mailboxTransportDeliveryResult;
			string str;
			bool flag = MailboxTransportDeliveryResult.TryParse(smtpResponse, out mailboxTransportDeliveryResult, out str);
			if (flag)
			{
				messageLevelSmtpResponse = mailboxTransportDeliveryResult.MessageLevelSmtpResponse;
				messageLevelRetryInterval = mailboxTransportDeliveryResult.MessageLevelRetryInterval;
				if (mailboxTransportDeliveryResult.RetryQueue)
				{
					messageLevelAckStatus = new AckStatus?(AckStatus.Retry);
					this.OverrideRecipientResponses(AckStatus.Retry, messageLevelSmtpResponse);
					this.retryQueueRequested = true;
					this.retryQueueSmtpResponse = messageLevelSmtpResponse;
					return;
				}
				if (mailboxTransportDeliveryResult.MessageLevelResubmit)
				{
					messageLevelAckStatus = new AckStatus?(AckStatus.Resubmit);
					this.OverrideRecipientResponses(AckStatus.Resubmit, messageLevelSmtpResponse);
					return;
				}
				if (mailboxTransportDeliveryResult.RecipientResponseCount > 0)
				{
					int num = this.recipientResponses.Count((AckStatusAndResponse r) => r.AckStatus == AckStatus.Success);
					if (mailboxTransportDeliveryResult.RecipientResponseCount != num)
					{
						messageLevelSmtpResponse = new SmtpResponse("421", "4.4.0", new string[]
						{
							string.Format(CultureInfo.InvariantCulture, "smtp response from MailboxTransportDelivery has {0} recipients which is different from the number of successful RCPT TO's: {1}", new object[]
							{
								mailboxTransportDeliveryResult.RecipientResponseCount,
								num
							})
						});
						messageLevelAckStatus = new AckStatus?(AckStatus.Retry);
						this.OverrideRecipientResponses(AckStatus.Retry, messageLevelSmtpResponse);
						return;
					}
					this.OverrideRecipientResponses(mailboxTransportDeliveryResult.RecipientResponses);
					return;
				}
			}
			else
			{
				messageLevelSmtpResponse = new SmtpResponse("421", "4.4.0", new string[]
				{
					"failed to parse smtp response from Mailbox Transport Delivery: " + str
				});
				messageLevelAckStatus = new AckStatus?(AckStatus.Retry);
				this.OverrideRecipientResponses(AckStatus.Retry, messageLevelSmtpResponse);
			}
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000389BC File Offset: 0x00036BBC
		private void OverrideRecipientResponses(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			int count = this.recipientResponses.Count;
			this.recipientResponses = new Queue<AckStatusAndResponse>(count);
			for (int i = 0; i < count; i++)
			{
				this.recipientResponses.Enqueue(new AckStatusAndResponse(ackStatus, smtpResponse));
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00038A00 File Offset: 0x00036C00
		private void OverrideRecipientResponses(IEnumerable<MailboxTransportDeliveryResult.RecipientResponse> mbxTrResponses)
		{
			if (this.recipientResponses.Count != this.recipientsSent.Count)
			{
				throw new InvalidOperationException(string.Format("recipient response count {0} is different from recipients sent count {1}", this.recipientResponses.Count, this.recipientsSent.Count));
			}
			IEnumerator<MailboxTransportDeliveryResult.RecipientResponse> enumerator = mbxTrResponses.GetEnumerator();
			IEnumerator<MailRecipient> enumerator2 = this.recipientsSent.GetEnumerator();
			IEnumerator<AckStatusAndResponse> enumerator3 = this.recipientResponses.GetEnumerator();
			Queue<AckStatusAndResponse> queue = new Queue<AckStatusAndResponse>();
			while (enumerator3.MoveNext() && enumerator2.MoveNext())
			{
				AckStatusAndResponse item = enumerator3.Current;
				MailRecipient mailRecipient = enumerator2.Current;
				if (item.AckStatus == AckStatus.Success)
				{
					enumerator.MoveNext();
					MailboxTransportDeliveryResult.RecipientResponse recipientResponse = enumerator.Current;
					queue.Enqueue(new AckStatusAndResponse(recipientResponse.AckStatus, recipientResponse.SmtpResponse));
					if (recipientResponse.RetryOnDuplicateDelivery)
					{
						mailRecipient.ExtendedProperties.SetValue<bool>("Microsoft.Exchange.Transport.MailboxTransport.RetryOnDuplicateDelivery ", true);
					}
				}
				else
				{
					queue.Enqueue(item);
				}
			}
			this.recipientResponses = queue;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00038B04 File Offset: 0x00036D04
		private IList<MailRecipient> GetReadyRecipients()
		{
			int num;
			if (this.routedMessageQueue.Key.NextHopType.DeliveryType == DeliveryType.SmtpDeliveryToMailbox)
			{
				num = 47;
			}
			else if (this.maxMessageRecipients > 0)
			{
				num = Math.Max(this.maxMessageRecipients, 50);
			}
			else
			{
				num = this.routedMailItem.Recipients.Count;
			}
			List<MailRecipient> list = new List<MailRecipient>(num);
			foreach (MailRecipient mailRecipient in this.routedMailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Ready)
				{
					list.Add(mailRecipient);
					if (list.Count == num)
					{
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x04000706 RID: 1798
		private const int MinRecipientsBatchSize = 50;

		// Token: 0x04000707 RID: 1799
		private long connectionId;

		// Token: 0x04000708 RID: 1800
		private bool isConnectionAttemptSucceeded;

		// Token: 0x04000709 RID: 1801
		private bool retryQueueRequested;

		// Token: 0x0400070A RID: 1802
		private SmtpResponse retryQueueSmtpResponse = SmtpResponse.Empty;

		// Token: 0x0400070B RID: 1803
		private RoutedMessageQueue routedMessageQueue;

		// Token: 0x0400070C RID: 1804
		private int maxMessageRecipients;

		// Token: 0x0400070D RID: 1805
		private DsnFlags generateSuccessDSNs;

		// Token: 0x0400070E RID: 1806
		private IEnumerator<MailRecipient> recipientEnumerator;

		// Token: 0x0400070F RID: 1807
		private Queue<AckStatusAndResponse> recipientResponses;

		// Token: 0x04000710 RID: 1808
		private Queue<MailRecipient> recipientsSent;

		// Token: 0x04000711 RID: 1809
		private int recipientsPending;

		// Token: 0x04000712 RID: 1810
		private ConnectionManager parent;

		// Token: 0x04000713 RID: 1811
		private RoutedMailItem routedMailItem;

		// Token: 0x04000714 RID: 1812
		private IList<MailRecipient> readyRecipients;

		// Token: 0x04000715 RID: 1813
		private DeliveryPriority priority;
	}
}
