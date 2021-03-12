using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Logging.ConnectionLog;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003CA RID: 970
	internal class InternalDeliveryAgentEventSource
	{
		// Token: 0x06002C5A RID: 11354 RVA: 0x000B0D1B File Offset: 0x000AEF1B
		public InternalDeliveryAgentEventSource(DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession, RoutedMailItemWrapper currentMailItem, ulong sessionId, NextHopConnection nextHopConnection, string remoteHost, DeliveryAgentConnection.Stats stats)
		{
			this.mexSession = mexSession;
			this.currentMailItem = currentMailItem;
			this.sessionId = sessionId;
			this.nextHopConnection = nextHopConnection;
			this.remoteHost = remoteHost;
			this.stats = stats;
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x000B0D5B File Offset: 0x000AEF5B
		public bool ConnectionRegistered
		{
			get
			{
				return this.connectionRegistered;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000B0D63 File Offset: 0x000AEF63
		public bool MessageAcked
		{
			get
			{
				return this.messageAcked;
			}
		}

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x000B0D6B File Offset: 0x000AEF6B
		public bool AnyRecipientsAcked
		{
			get
			{
				return this.recipientResponses.Count > 0;
			}
		}

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06002C5E RID: 11358 RVA: 0x000B0D7B File Offset: 0x000AEF7B
		public bool ConnectionUnregistered
		{
			get
			{
				return this.connectionUnregistered;
			}
		}

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000B0D83 File Offset: 0x000AEF83
		public string RemoteHost
		{
			get
			{
				return this.remoteHost;
			}
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000B0D8C File Offset: 0x000AEF8C
		public void RegisterConnection(string remoteHost, SmtpResponse smtpResponse)
		{
			if (string.IsNullOrEmpty(remoteHost))
			{
				throw new ArgumentException("remoteHost");
			}
			this.ThrowIfInvalidResponse(smtpResponse);
			this.ThrowIfQueueResponseSet();
			this.remoteHost = remoteHost.Substring(0, Math.Min(remoteHost.Length, 64));
			ConnectionLog.DeliveryAgentStart(this.sessionId, this.mexSession.CurrentAgentName, this.nextHopConnection.Key);
			ConnectionLog.DeliveryAgentConnected(this.sessionId, this.remoteHost, smtpResponse);
			this.stats.ConnectionStarted();
			this.connectionRegistered = true;
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000B0E18 File Offset: 0x000AF018
		public void UnregisterConnection(SmtpResponse smtpResponse)
		{
			this.ThrowIfInvalidResponse(smtpResponse);
			this.ThrowIfQueueResponseSet();
			ConnectionLog.DeliveryAgentDisconnected(this.sessionId, this.remoteHost, smtpResponse);
			ConnectionLog.DeliveryAgentStop(this.sessionId, this.remoteHost, this.stats.NumMessagesDelivered, this.stats.NumBytesDelivered);
			if (this.currentMailItem != null)
			{
				this.AckMailItemPending();
			}
			this.AckConnection(AckStatus.Success, smtpResponse, null);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000B0E8C File Offset: 0x000AF08C
		public void FailQueue(SmtpResponse smtpResponse)
		{
			this.ThrowIfInvalidResponse(smtpResponse);
			this.ThrowIfQueueResponseSet();
			ConnectionLog.DeliveryAgentPermanentFailure(this.sessionId, this.remoteHost ?? this.nextHopConnection.Key.NextHopDomain, smtpResponse);
			this.AckConnection(AckStatus.Fail, smtpResponse, null);
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000B0EE0 File Offset: 0x000AF0E0
		public void DeferQueue(SmtpResponse smtpResponse)
		{
			this.DeferQueueInternal(smtpResponse, null);
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000B0EFD File Offset: 0x000AF0FD
		public void DeferQueue(SmtpResponse smtpResponse, TimeSpan interval)
		{
			this.DeferQueueInternal(smtpResponse, new TimeSpan?(interval));
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000B0F0C File Offset: 0x000AF10C
		public void AckMailItemSuccess(SmtpResponse smtpResponse)
		{
			this.ThrowIfInvalidResponse(smtpResponse);
			this.ThrowIfMailItemResponseSet();
			this.ThrowIfAnyRecipientResponseSet();
			this.stats.MessageDelivered(this.currentMailItem.Recipients.Count, this.currentMailItem.MimeStreamLength);
			this.AckMailItem(AckStatus.Success, smtpResponse, null);
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000B0F63 File Offset: 0x000AF163
		public void AckMailItemDefer(SmtpResponse smtpResponse)
		{
			this.ThrowIfInvalidResponse(smtpResponse);
			this.ThrowIfMailItemResponseSet();
			this.ThrowIfAnyRecipientResponseSet();
			this.AckMailItemDeferInternal(smtpResponse, new TimeSpan?(InternalDeliveryAgentEventSource.DefaultMailItemRetryInterval));
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000B0F8C File Offset: 0x000AF18C
		public void AckMailItemPending()
		{
			this.ThrowIfMailItemResponseSet();
			this.ThrowIfAnyRecipientResponseSet();
			this.AckMailItem(AckStatus.Pending, SmtpResponse.Empty, null);
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000B0FBC File Offset: 0x000AF1BC
		public void AckMailItemFail(SmtpResponse smtpResponse)
		{
			this.ThrowIfInvalidResponse(smtpResponse);
			this.ThrowIfMailItemResponseSet();
			this.ThrowIfAnyRecipientResponseSet();
			this.stats.MessageFailed();
			this.AckMailItem(AckStatus.Fail, smtpResponse, null);
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000B0FF8 File Offset: 0x000AF1F8
		public void AckRecipientSuccess(EnvelopeRecipient recipient, SmtpResponse smtpResponse)
		{
			this.AckRecipient(recipient, AckStatus.Success, smtpResponse);
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000B1003 File Offset: 0x000AF203
		public void AckRecipientDefer(EnvelopeRecipient recipient, SmtpResponse smtpResponse)
		{
			this.AckRecipient(recipient, AckStatus.Retry, smtpResponse);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000B100E File Offset: 0x000AF20E
		public void AckRecipientFail(EnvelopeRecipient recipient, SmtpResponse smtpResponse)
		{
			this.AckRecipient(recipient, AckStatus.Fail, smtpResponse);
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000B101C File Offset: 0x000AF21C
		public void AckRemainingRecipientsAndFinalizeMailItem(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			foreach (EnvelopeRecipient envelopeRecipient in this.currentMailItem.Recipients)
			{
				if (!this.recipientResponses.ContainsKey(envelopeRecipient.Address))
				{
					this.AckRecipient(envelopeRecipient, ackStatus, smtpResponse);
				}
			}
			this.UpdateRecipientStats();
			this.AckMailItem(AckStatus.Success, SmtpResponse.Empty, null);
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000B10A4 File Offset: 0x000AF2A4
		public void AddDsnParameters(string key, object value)
		{
			this.ThrowIfMailItemResponseSet();
			this.currentMailItem.RoutedMailItem.AddDsnParameters(key, value);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000B10BE File Offset: 0x000AF2BE
		public bool TryGetDsnParameters(string key, out object value)
		{
			this.ThrowIfMailItemResponseSet();
			value = null;
			return this.currentMailItem.RoutedMailItem.DsnParameters != null && this.currentMailItem.RoutedMailItem.DsnParameters.TryGetValue(key, out value);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000B10F4 File Offset: 0x000AF2F4
		public void AddDsnParameters(EnvelopeRecipient recipient, string key, object value)
		{
			this.ThrowIfInvalidRecipient(recipient);
			this.ThrowIfMailItemResponseSet();
			MailRecipientWrapper mailRecipientWrapper = (MailRecipientWrapper)recipient;
			mailRecipientWrapper.MailRecipient.AddDsnParameters(key, value);
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000B1124 File Offset: 0x000AF324
		public bool TryGetDsnParameters(EnvelopeRecipient recipient, string key, out object value)
		{
			this.ThrowIfInvalidRecipient(recipient);
			this.ThrowIfMailItemResponseSet();
			value = null;
			MailRecipientWrapper mailRecipientWrapper = (MailRecipientWrapper)recipient;
			return mailRecipientWrapper.MailRecipient.DsnParameters != null && mailRecipientWrapper.MailRecipient.DsnParameters.TryGetValue(key, out value);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000B1169 File Offset: 0x000AF369
		private void AckRecipient(EnvelopeRecipient recipient, AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			this.ThrowIfInvalidRecipient(recipient);
			this.ThrowIfInvalidResponse(smtpResponse);
			this.ThrowIfMailItemResponseSet();
			this.ThrowIfRecipientResponseSet(recipient.Address);
			this.recipientResponses.Add(recipient.Address, new AckStatusAndResponse(ackStatus, smtpResponse));
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000B11A4 File Offset: 0x000AF3A4
		private void UpdateRecipientStats()
		{
			if (this.recipientResponses.Count != this.currentMailItem.Recipients.Count)
			{
				throw new InvalidOperationException("All recipients need to be acked before updating stats");
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (AckStatusAndResponse ackStatusAndResponse in this.recipientResponses.Values)
			{
				switch (ackStatusAndResponse.AckStatus)
				{
				case AckStatus.Success:
					num++;
					break;
				case AckStatus.Retry:
					num2++;
					break;
				case AckStatus.Fail:
					num3++;
					break;
				default:
					throw new InvalidOperationException("Unexpected ack status");
				}
			}
			if (num > 0)
			{
				this.stats.MessageDelivered(num, this.currentMailItem.MimeStreamLength);
			}
			if (num2 > 0)
			{
				this.stats.MessageDeferred();
			}
			if (num3 > 0)
			{
				this.stats.MessageFailed();
			}
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000B129C File Offset: 0x000AF49C
		private void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, TimeSpan? retryInterval)
		{
			MailRecipient nextRecipient;
			while ((nextRecipient = this.nextHopConnection.GetNextRecipient()) != null)
			{
				AckStatusAndResponse ackStatusAndResponse;
				if (this.recipientResponses.TryGetValue(nextRecipient.Email, out ackStatusAndResponse))
				{
					this.nextHopConnection.AckRecipient(ackStatusAndResponse.AckStatus, ackStatusAndResponse.SmtpResponse);
				}
				else
				{
					this.nextHopConnection.AckRecipient(ackStatus, smtpResponse);
				}
			}
			this.nextHopConnection.AckMailItem(ackStatus, smtpResponse, null, retryInterval, MessageTrackingSource.AGENT, this.mexSession.CurrentAgentName, LatencyComponent.DeliveryAgent, true);
			if (this.currentMailItem != null)
			{
				this.currentMailItem.Close();
				this.currentMailItem = null;
			}
			this.messageAcked = true;
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000B1336 File Offset: 0x000AF536
		private void AckMailItemDeferInternal(SmtpResponse smtpResponse, TimeSpan? interval)
		{
			if (interval == null)
			{
				interval = new TimeSpan?(InternalDeliveryAgentEventSource.DefaultMailItemRetryInterval);
			}
			this.stats.MessageDeferred();
			this.AckMailItem(AckStatus.Retry, smtpResponse, interval);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000B1364 File Offset: 0x000AF564
		private void AckConnection(AckStatus ackStatus, SmtpResponse smtpResponse, TimeSpan? retryInterval)
		{
			if (this.stats.HasOpenConnection)
			{
				this.stats.ConnectionStopped();
			}
			if (ackStatus != AckStatus.Success)
			{
				this.stats.ConnectionFailed();
			}
			this.nextHopConnection.AckConnection(MessageTrackingSource.AGENT, this.mexSession.CurrentAgentName, ackStatus, smtpResponse, null, retryInterval);
			this.nextHopConnection = null;
			this.connectionUnregistered = true;
			this.messageAcked = true;
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000B13C8 File Offset: 0x000AF5C8
		private void DeferQueueInternal(SmtpResponse smtpResponse, TimeSpan? interval)
		{
			this.ThrowIfInvalidResponse(smtpResponse);
			this.ThrowIfQueueResponseSet();
			if (interval != null)
			{
				this.ThrowIfInvalidInterval(interval.Value);
			}
			if (this.currentMailItem != null)
			{
				this.AckMailItemPending();
			}
			ConnectionLog.DeliveryAgentQueueRetry(this.sessionId, this.remoteHost ?? this.nextHopConnection.Key.NextHopDomain, smtpResponse);
			this.AckConnection(AckStatus.Retry, smtpResponse, interval);
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000B1438 File Offset: 0x000AF638
		private void ThrowIfInvalidResponse(SmtpResponse smtpResponse)
		{
			if (smtpResponse.Equals(SmtpResponse.Empty))
			{
				throw new ArgumentException("smtpResponse");
			}
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000B1453 File Offset: 0x000AF653
		private void ThrowIfInvalidInterval(TimeSpan interval)
		{
			if (interval <= TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("interval");
			}
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000B146D File Offset: 0x000AF66D
		private void ThrowIfQueueResponseSet()
		{
			if (this.connectionRegistered || this.connectionUnregistered)
			{
				throw new InvalidOperationException("Only one of FailQueue(), DeferQueue(), RegisterConnection(), or UnregisterConnection() can be called, and it can only be called once.");
			}
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000B148A File Offset: 0x000AF68A
		private void ThrowIfMailItemResponseSet()
		{
			if (this.messageAcked)
			{
				throw new InvalidOperationException("Only one of AckMailItemSuccess(), AckMailItemDefer(), or AckMailItemFail() can be called, and it can only be called once.");
			}
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000B14A0 File Offset: 0x000AF6A0
		private void ThrowIfInvalidRecipient(EnvelopeRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			if (!(recipient is MailRecipientWrapper))
			{
				throw new ArgumentException("Invalid recipient type", "recipient");
			}
			if (!this.currentMailItem.Recipients.Contains(recipient.Address))
			{
				throw new ArgumentException("Recipient does not exist in message", "recipient");
			}
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000B14FB File Offset: 0x000AF6FB
		private void ThrowIfAnyRecipientResponseSet()
		{
			if (this.recipientResponses.Count > 0)
			{
				throw new InvalidOperationException("You cannot ack a mail item if you have already acked the mail item, acked any recipient, deferred or failed the queue, or unregistered the connection.");
			}
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000B1516 File Offset: 0x000AF716
		private void ThrowIfRecipientResponseSet(RoutingAddress recipient)
		{
			if (this.recipientResponses.ContainsKey(recipient))
			{
				throw new InvalidOperationException("You cannot ack a recipient if you have already acked the recipient, acked the mail item, deferred or failed the queue, or unregistered the connection.");
			}
		}

		// Token: 0x04001636 RID: 5686
		public static readonly TimeSpan DefaultMailItemRetryInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04001637 RID: 5687
		private DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession;

		// Token: 0x04001638 RID: 5688
		private RoutedMailItemWrapper currentMailItem;

		// Token: 0x04001639 RID: 5689
		private Dictionary<RoutingAddress, AckStatusAndResponse> recipientResponses = new Dictionary<RoutingAddress, AckStatusAndResponse>();

		// Token: 0x0400163A RID: 5690
		private ulong sessionId;

		// Token: 0x0400163B RID: 5691
		private NextHopConnection nextHopConnection;

		// Token: 0x0400163C RID: 5692
		private DeliveryAgentConnection.Stats stats;

		// Token: 0x0400163D RID: 5693
		private bool connectionRegistered;

		// Token: 0x0400163E RID: 5694
		private bool messageAcked;

		// Token: 0x0400163F RID: 5695
		private bool connectionUnregistered;

		// Token: 0x04001640 RID: 5696
		private string remoteHost;
	}
}
