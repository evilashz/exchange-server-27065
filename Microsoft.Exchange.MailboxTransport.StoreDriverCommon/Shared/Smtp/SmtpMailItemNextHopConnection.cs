using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MailboxTransport.StoreDriver;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Shared.Smtp
{
	// Token: 0x02000026 RID: 38
	internal class SmtpMailItemNextHopConnection : NextHopConnection, IDisposable
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00007084 File Offset: 0x00005284
		public SmtpMailItemNextHopConnection(NextHopSolutionKey key, IReadOnlyMailItem mailItem, ISmtpMailItemSenderNotifications notificationHandler) : base(null, 0L, DeliveryPriority.Normal, null)
		{
			this.key = key;
			this.mailItem = mailItem;
			this.readyRecipients = this.GetReadyRecipients();
			this.recipientsPending = this.readyRecipients.Count;
			this.recipientEnumerator = this.mailItem.Recipients.GetEnumerator();
			this.recipientEnumeratorAck = this.mailItem.Recipients.GetEnumerator();
			this.notificationHandler = notificationHandler;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000710D File Offset: 0x0000530D
		public WaitHandle AckConnectionEvent
		{
			get
			{
				return this.autoResetEvent;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00007115 File Offset: 0x00005315
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000711D File Offset: 0x0000531D
		public SmtpMailItemResult SmtpMailItemResult
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00007126 File Offset: 0x00005326
		public override NextHopSolutionKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000712E File Offset: 0x0000532E
		public override IReadOnlyMailItem ReadOnlyMailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00007136 File Offset: 0x00005336
		public override RoutedMailItem RoutedMailItem
		{
			get
			{
				throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00007142 File Offset: 0x00005342
		public override int MaxMessageRecipients
		{
			get
			{
				return this.mailItem.Recipients.Count;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00007154 File Offset: 0x00005354
		public override int RecipientCount
		{
			get
			{
				return this.recipientsPending;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000715C File Offset: 0x0000535C
		public override IList<MailRecipient> ReadyRecipientsList
		{
			get
			{
				throw new NotSupportedException("This should not be called on Mailbox Transport Submission");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00007168 File Offset: 0x00005368
		public override IEnumerable<MailRecipient> ReadyRecipients
		{
			get
			{
				return this.readyRecipients;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00007170 File Offset: 0x00005370
		public override int ActiveQueueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00007173 File Offset: 0x00005373
		public override int TotalQueueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007176 File Offset: 0x00005376
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00007185 File Offset: 0x00005385
		public override void ConnectionAttemptSucceeded()
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00007187 File Offset: 0x00005387
		public override RoutedMailItem GetNextRoutedMailItem()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000718E File Offset: 0x0000538E
		public override IReadOnlyMailItem GetNextMailItem()
		{
			if (this.mailItemSentForProcessing)
			{
				return null;
			}
			if (this.getMailItemBeingCalledForTheFirstTime)
			{
				SmtpMailItemNextHopConnection.InitializeSmtpLatencyTracking(this.mailItem.LatencyTracker);
				this.getMailItemBeingCalledForTheFirstTime = false;
			}
			return this.mailItem;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000071C0 File Offset: 0x000053C0
		public override MailRecipient GetNextRecipient()
		{
			while (this.recipientEnumerator.MoveNext())
			{
				MailRecipient mailRecipient = this.recipientEnumerator.Current;
				if (mailRecipient.Status == Status.Ready)
				{
					return mailRecipient;
				}
			}
			return null;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000071F4 File Offset: 0x000053F4
		public override void AckConnection(MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckStatus status, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, bool resubmitWithoutHighAvailablityRouting, SessionSetupFailureReason failureReason)
		{
			switch (status)
			{
			case AckStatus.Pending:
			case AckStatus.Skip:
				throw new InvalidOperationException("Invalid status");
			case AckStatus.Success:
			case AckStatus.Retry:
			case AckStatus.Fail:
			case AckStatus.Resubmit:
				if (this.result == null)
				{
					this.result = new SmtpMailItemResult();
				}
				this.result.ConnectionResponse = new AckStatusAndResponse(status, smtpResponse);
				if (details != null)
				{
					this.result.RemoteHostName = details.RemoteHostName;
				}
				if (this.notificationHandler != null)
				{
					this.notificationHandler.AckConnection(status, smtpResponse);
				}
				SmtpMailItemNextHopConnection.EndSmtpLatencyTracking(this.mailItem.LatencyTracker);
				if (this.autoResetEvent != null)
				{
					this.autoResetEvent.Set();
					return;
				}
				break;
			case AckStatus.Expand:
			case AckStatus.Relay:
			case AckStatus.SuccessNoDsn:
			case AckStatus.Quarantine:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000072B4 File Offset: 0x000054B4
		public override void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, MessageTrackingSource source, string messageTrackingSourceContext, LatencyComponent deliveryComponent, string remoteMta, bool shadowed, string primaryServer, bool reportEndToEndLatencies)
		{
			if (this.recipientsPending != 0 && ackStatus == AckStatus.Success)
			{
				throw new InvalidOperationException("Cannot ack message successfully until all pending recipients have been acked");
			}
			if (ackStatus == AckStatus.Pending)
			{
				this.recipientsPending = this.readyRecipients.Count;
				this.recipientEnumerator = this.mailItem.Recipients.GetEnumerator();
				this.recipientEnumeratorAck = this.mailItem.Recipients.GetEnumerator();
				this.result = null;
			}
			else
			{
				if (this.result == null)
				{
					this.result = new SmtpMailItemResult();
				}
				this.result.MessageResponse = new AckStatusAndResponse(ackStatus, smtpResponse);
				this.mailItemSentForProcessing = true;
			}
			if (this.notificationHandler != null)
			{
				this.notificationHandler.AckMessage(ackStatus, smtpResponse);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007364 File Offset: 0x00005564
		public override void AckRecipient(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			TraceHelper.SmtpSendTracer.TracePass<string, string>(TraceHelper.MessageProbeActivityId, (long)this.GetHashCode(), "InboundProxyNextHopConnection.AckRecipient. Ackstatus  = {0}. SmtpResponse = {1}", ackStatus.ToString(), smtpResponse.ToString());
			if (!this.recipientEnumeratorAck.MoveNext() || this.recipientsPending <= 0)
			{
				throw new InvalidOperationException("AckRecipient called but no recipients left to ack");
			}
			this.recipientsPending--;
			MailRecipient recipient = this.recipientEnumeratorAck.Current;
			switch (ackStatus)
			{
			case AckStatus.Pending:
			case AckStatus.Success:
			case AckStatus.Retry:
			case AckStatus.Fail:
				if (this.result == null)
				{
					this.result = new SmtpMailItemResult();
				}
				if (this.result.RecipientResponses == null)
				{
					this.result.RecipientResponses = new Dictionary<MailRecipient, AckStatusAndResponse>();
				}
				this.result.RecipientResponses.Add(recipient, new AckStatusAndResponse(ackStatus, smtpResponse));
				if (this.notificationHandler != null)
				{
					this.notificationHandler.AckRecipient(ackStatus, smtpResponse, recipient);
				}
				return;
			default:
				throw new InvalidOperationException(string.Format("AckRecipient with status: {0} is invalid", ackStatus));
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000746B File Offset: 0x0000566B
		public override void ResetQueueLastRetryTimeAndError()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007472 File Offset: 0x00005672
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.autoResetEvent.Dispose();
				this.autoResetEvent = null;
				this.disposed = true;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007495 File Offset: 0x00005695
		private static void InitializeSmtpLatencyTracking(LatencyTracker latencyTracker)
		{
			if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxSubmission)
			{
				LatencyTracker.BeginTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtp, latencyTracker);
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000074AC File Offset: 0x000056AC
		private static void EndSmtpLatencyTracking(LatencyTracker latencyTracker)
		{
			if (Components.Configuration.ProcessTransportRole == ProcessTransportRole.MailboxSubmission)
			{
				LatencyTracker.EndTrackLatency(LatencyComponent.MailboxTransportSubmissionStoreDriverSubmissionSmtp, LatencyComponent.SmtpSend, latencyTracker);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000074C8 File Offset: 0x000056C8
		private IList<MailRecipient> GetReadyRecipients()
		{
			List<MailRecipient> list = new List<MailRecipient>(this.mailItem.Recipients.Count);
			foreach (MailRecipient mailRecipient in this.mailItem.Recipients)
			{
				if (mailRecipient.Status == Status.Ready)
				{
					list.Add(mailRecipient);
				}
			}
			return list;
		}

		// Token: 0x04000075 RID: 117
		private IEnumerator<MailRecipient> recipientEnumerator;

		// Token: 0x04000076 RID: 118
		private int recipientsPending;

		// Token: 0x04000077 RID: 119
		private IEnumerator<MailRecipient> recipientEnumeratorAck;

		// Token: 0x04000078 RID: 120
		private IReadOnlyMailItem mailItem;

		// Token: 0x04000079 RID: 121
		private NextHopSolutionKey key;

		// Token: 0x0400007A RID: 122
		private bool mailItemSentForProcessing;

		// Token: 0x0400007B RID: 123
		private bool getMailItemBeingCalledForTheFirstTime = true;

		// Token: 0x0400007C RID: 124
		private AutoResetEvent autoResetEvent = new AutoResetEvent(false);

		// Token: 0x0400007D RID: 125
		private SmtpMailItemResult result;

		// Token: 0x0400007E RID: 126
		private ISmtpMailItemSenderNotifications notificationHandler;

		// Token: 0x0400007F RID: 127
		private bool disposed;

		// Token: 0x04000080 RID: 128
		private IList<MailRecipient> readyRecipients;
	}
}
