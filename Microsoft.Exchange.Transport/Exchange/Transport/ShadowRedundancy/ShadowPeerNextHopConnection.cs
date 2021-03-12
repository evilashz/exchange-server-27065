using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000379 RID: 889
	internal class ShadowPeerNextHopConnection : NextHopConnection
	{
		// Token: 0x06002676 RID: 9846 RVA: 0x0009580C File Offset: 0x00093A0C
		public ShadowPeerNextHopConnection(ShadowSession shadowSession, IInboundProxyLayer proxyLayer, IMailRouter mailRouter, TransportMailItem mailItem) : base(null, 0L, DeliveryPriority.Normal, null)
		{
			if (shadowSession == null)
			{
				throw new ArgumentNullException("shadowSession");
			}
			if (proxyLayer == null)
			{
				throw new ArgumentNullException("proxyLayer");
			}
			if (mailRouter == null)
			{
				throw new ArgumentNullException("mailRouter");
			}
			if (mailItem == null)
			{
				throw new ArgumentNullException("mailItem");
			}
			this.shadowSession = shadowSession;
			this.proxyLayer = proxyLayer;
			this.mailRouter = mailRouter;
			this.mailItem = new ShadowRoutedMailItem(mailItem);
			this.recipientsPending = this.mailItem.Recipients.Count;
			this.recipientEnumerator = this.mailItem.Recipients.GetEnumerator();
			this.key = new NextHopSolutionKey(NextHopType.ShadowRedundancy, "ShadowRedundancy", Guid.Empty);
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002677 RID: 9847 RVA: 0x000958C5 File Offset: 0x00093AC5
		public ShadowSession ShadowSession
		{
			get
			{
				return this.shadowSession;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x000958CD File Offset: 0x00093ACD
		public IInboundProxyLayer ProxyLayer
		{
			get
			{
				return this.proxyLayer;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002679 RID: 9849 RVA: 0x000958D5 File Offset: 0x00093AD5
		public override NextHopSolutionKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x000958DD File Offset: 0x00093ADD
		public override IReadOnlyMailItem ReadOnlyMailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600267B RID: 9851 RVA: 0x000958E5 File Offset: 0x00093AE5
		public override RoutedMailItem RoutedMailItem
		{
			get
			{
				throw new NotSupportedException("This should not be called for shadowing");
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x000958F1 File Offset: 0x00093AF1
		public override int MaxMessageRecipients
		{
			get
			{
				return this.mailItem.Recipients.Count;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600267D RID: 9853 RVA: 0x00095903 File Offset: 0x00093B03
		public override int RecipientCount
		{
			get
			{
				return this.recipientsPending;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x0009590B File Offset: 0x00093B0B
		public override IList<MailRecipient> ReadyRecipientsList
		{
			get
			{
				throw new NotSupportedException("This should not be called for shadowing");
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x0600267F RID: 9855 RVA: 0x00095917 File Offset: 0x00093B17
		public override IEnumerable<MailRecipient> ReadyRecipients
		{
			get
			{
				return this.mailItem.Recipients;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x00095924 File Offset: 0x00093B24
		public override int ActiveQueueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002681 RID: 9857 RVA: 0x00095927 File Offset: 0x00093B27
		public override int TotalQueueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x0009592A File Offset: 0x00093B2A
		public override void ConnectionAttemptSucceeded()
		{
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0009592C File Offset: 0x00093B2C
		public override void CreateConnectionIfNecessary()
		{
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0009592E File Offset: 0x00093B2E
		public override RoutedMailItem GetNextRoutedMailItem()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x00095935 File Offset: 0x00093B35
		public override IReadOnlyMailItem GetNextMailItem()
		{
			return this.mailItem;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0009593D File Offset: 0x00093B3D
		public override MailRecipient GetNextRecipient()
		{
			if (this.recipientEnumerator.MoveNext())
			{
				return this.recipientEnumerator.Current;
			}
			return null;
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0009595C File Offset: 0x00093B5C
		public override void AckConnection(MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckStatus status, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, bool resubmitWithoutHighAvailablityRouting, SessionSetupFailureReason failureReason)
		{
			ShadowRedundancyManager.SendTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ShadowPeerNextHopConnection.AckConnection. Ackstatus  = {0}. SmtpResponse = {1}", status.ToString(), smtpResponse.ToString());
			this.shadowSession.DropBreadcrumb(ShadowBreadcrumbs.NextHopAckConnection);
			switch (status)
			{
			case AckStatus.Pending:
			case AckStatus.Resubmit:
			case AckStatus.Skip:
				throw new InvalidOperationException("Invalid status");
			case AckStatus.Success:
			case AckStatus.Retry:
			case AckStatus.Fail:
				this.shadowSession.Close(status, smtpResponse);
				return;
			case AckStatus.Expand:
			case AckStatus.Relay:
			case AckStatus.SuccessNoDsn:
			case AckStatus.Quarantine:
				return;
			default:
				return;
			}
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000959F0 File Offset: 0x00093BF0
		public override void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, MessageTrackingSource source, string messageTrackingSourceContext, LatencyComponent deliveryComponent, string remoteMta, bool shadowed, string primaryServer, bool reportEndToEndLatencies)
		{
			ShadowRedundancyManager.SendTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ShadowPeerNextHopConnection.AckMailItem. Ackstatus  = {0}. SmtpResponse = {1}", ackStatus.ToString(), smtpResponse.ToString());
			this.shadowSession.DropBreadcrumb(ShadowBreadcrumbs.NextHopConnectionAckMailItem);
			if (this.recipientsPending != 0 && ackStatus == AckStatus.Success)
			{
				throw new InvalidOperationException("Cannot ack message successfully until all pending recipients have been acked");
			}
			if (ackStatus == AckStatus.Pending)
			{
				this.recipientsPending = this.mailItem.Recipients.Count;
				this.recipientEnumerator = this.mailItem.Recipients.GetEnumerator();
				this.shadowSession.NotifyProxyFailover(primaryServer, smtpResponse);
				return;
			}
			this.mailItem = null;
			this.recipientEnumerator = null;
			this.shadowSession.NotifyShadowServerResponse(primaryServer, smtpResponse);
			if (ackStatus == AckStatus.Success)
			{
				ShadowRedundancyManager.PerfCounters.MessageShadowed(primaryServer, !this.mailRouter.IsInLocalSite(primaryServer));
			}
			this.shadowSession.AckMessage(ackStatus, smtpResponse);
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x00095AD8 File Offset: 0x00093CD8
		public override void AckRecipient(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			ShadowRedundancyManager.SendTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ShadowPeerNextHopConnection.AckRecipient. Ackstatus  = {0}. SmtpResponse = {1}", ackStatus.ToString(), smtpResponse.ToString());
			if (this.recipientsPending <= 0)
			{
				throw new InvalidOperationException("AckRecipient called but no recipients left to ack");
			}
			this.recipientsPending--;
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x00095B35 File Offset: 0x00093D35
		public override void ResetQueueLastRetryTimeAndError()
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x00095B3C File Offset: 0x00093D3C
		public override void NotifyConnectionFailedOver(string targetHostName, SmtpResponse failoverResponse, SessionSetupFailureReason failoverReason)
		{
			ShadowRedundancyManager.SendTracer.TraceDebug<string, SmtpResponse, SessionSetupFailureReason>((long)this.GetHashCode(), "ShadowPeerNextHopConnection.NotifyConnectionFailedOver. targetHostName={0} response={1} reason={2}", targetHostName, failoverResponse, failoverReason);
			this.shadowSession.DropBreadcrumb(ShadowBreadcrumbs.NextHopConnectionFailedOver);
			this.shadowSession.NotifyShadowServerResponse(targetHostName, failoverResponse);
		}

		// Token: 0x040013BB RID: 5051
		private IEnumerator<MailRecipient> recipientEnumerator;

		// Token: 0x040013BC RID: 5052
		private int recipientsPending;

		// Token: 0x040013BD RID: 5053
		private ShadowRoutedMailItem mailItem;

		// Token: 0x040013BE RID: 5054
		private NextHopSolutionKey key;

		// Token: 0x040013BF RID: 5055
		private ShadowSession shadowSession;

		// Token: 0x040013C0 RID: 5056
		private IInboundProxyLayer proxyLayer;

		// Token: 0x040013C1 RID: 5057
		private IMailRouter mailRouter;
	}
}
