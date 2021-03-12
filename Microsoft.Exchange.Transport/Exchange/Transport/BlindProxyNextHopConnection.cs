using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000147 RID: 327
	internal class BlindProxyNextHopConnection : NextHopConnection
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x00038BC4 File Offset: 0x00036DC4
		public BlindProxyNextHopConnection(ProxySessionSetupHandler proxySetupHandler, NextHopSolutionKey key) : base(null, 0L, DeliveryPriority.Normal, null)
		{
			this.sessionSetupHandler = proxySetupHandler;
			this.key = key;
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x00038BDF File Offset: 0x00036DDF
		public override NextHopSolutionKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00038BE7 File Offset: 0x00036DE7
		public override IReadOnlyMailItem ReadOnlyMailItem
		{
			get
			{
				throw new NotSupportedException("This should not be called in blind proxy mode");
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00038BF3 File Offset: 0x00036DF3
		public override RoutedMailItem RoutedMailItem
		{
			get
			{
				throw new NotSupportedException("This should not be called in blind proxy mode");
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00038BFF File Offset: 0x00036DFF
		public override int MaxMessageRecipients
		{
			get
			{
				throw new NotSupportedException("This should not be called in blind proxy mode");
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x00038C0B File Offset: 0x00036E0B
		public override int RecipientCount
		{
			get
			{
				throw new NotSupportedException("This should not be called in blind proxy mode");
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00038C17 File Offset: 0x00036E17
		public override IList<MailRecipient> ReadyRecipientsList
		{
			get
			{
				throw new NotSupportedException("This should not be called in blind proxy mode");
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00038C23 File Offset: 0x00036E23
		public override IEnumerable<MailRecipient> ReadyRecipients
		{
			get
			{
				throw new NotSupportedException("This should not be called in blind proxy mode");
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00038C2F File Offset: 0x00036E2F
		public override int ActiveQueueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00038C32 File Offset: 0x00036E32
		public override int TotalQueueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00038C35 File Offset: 0x00036E35
		public override void ConnectionAttemptSucceeded()
		{
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00038C37 File Offset: 0x00036E37
		public override RoutedMailItem GetNextRoutedMailItem()
		{
			throw new NotSupportedException("This should not be called in blind proxy mode");
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00038C43 File Offset: 0x00036E43
		public override IReadOnlyMailItem GetNextMailItem()
		{
			throw new NotSupportedException("This should not be called in blind proxy mode");
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00038C4F File Offset: 0x00036E4F
		public override MailRecipient GetNextRecipient()
		{
			throw new NotSupportedException("This should not be called in blind proxy mode");
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00038C5B File Offset: 0x00036E5B
		public override void NotifyConnectionFailedOver(string targetHostName, SmtpResponse failoverResponse, SessionSetupFailureReason failoverReason)
		{
			this.sessionSetupHandler.LogError(failoverReason, failoverResponse.ToString());
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x00038C78 File Offset: 0x00036E78
		public override void AckConnection(MessageTrackingSource messageTrackingSource, string messageTrackingSourceContext, AckStatus status, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, bool resubmitWithoutHighAvailablityRouting, SessionSetupFailureReason failureReason)
		{
			switch (status)
			{
			case AckStatus.Pending:
			case AckStatus.Success:
			case AckStatus.Resubmit:
			case AckStatus.Skip:
				throw new InvalidOperationException("Invalid status for blind proxy");
			case AckStatus.Retry:
				if (!smtpResponse.Equals(SmtpResponse.Empty) && failureReason != SessionSetupFailureReason.None)
				{
					this.sessionSetupHandler.LogError(failureReason, smtpResponse.ToString());
				}
				this.sessionSetupHandler.HandleProxySessionDisconnection(smtpResponse, false, failureReason);
				return;
			case AckStatus.Fail:
				if (!smtpResponse.Equals(SmtpResponse.Empty) && failureReason != SessionSetupFailureReason.None)
				{
					this.sessionSetupHandler.LogError(failureReason, smtpResponse.ToString());
				}
				this.sessionSetupHandler.HandleProxySessionDisconnection(smtpResponse, true, failureReason);
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

		// Token: 0x06000E9D RID: 3741 RVA: 0x00038D3B File Offset: 0x00036F3B
		public override void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, MessageTrackingSource source, string messageTrackingSourceContext, LatencyComponent deliveryComponent, string remoteMta, bool shadowed, string primaryServer, bool reportEndToEndLatencies)
		{
			throw new NotSupportedException("This should not be called in blind proxy mode");
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00038D47 File Offset: 0x00036F47
		public override void AckRecipient(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			throw new NotSupportedException("This should not be called in blind proxy mode");
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00038D53 File Offset: 0x00036F53
		public override void ResetQueueLastRetryTimeAndError()
		{
			throw new NotSupportedException("This should not be called in blind proxy mode");
		}

		// Token: 0x04000717 RID: 1815
		private NextHopSolutionKey key;

		// Token: 0x04000718 RID: 1816
		private ProxySessionSetupHandler sessionSetupHandler;
	}
}
