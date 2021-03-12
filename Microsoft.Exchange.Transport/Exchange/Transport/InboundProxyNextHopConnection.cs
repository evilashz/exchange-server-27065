using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000167 RID: 359
	internal class InboundProxyNextHopConnection : NextHopConnection
	{
		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003FCD0 File Offset: 0x0003DED0
		public InboundProxyNextHopConnection(IInboundProxyLayer proxyLayer, NextHopSolutionKey key, InboundProxyRoutedMailItem mailItem) : base(null, 0L, DeliveryPriority.Normal, null)
		{
			this.proxyLayer = proxyLayer;
			this.key = key;
			this.mailItem = mailItem;
			this.recipientsPending = this.mailItem.Recipients.Count;
			this.recipientEnumerator = this.mailItem.RecipientList.GetEnumerator();
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0003FD29 File Offset: 0x0003DF29
		public IInboundProxyLayer ProxyLayer
		{
			get
			{
				return this.proxyLayer;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0003FD31 File Offset: 0x0003DF31
		public override NextHopSolutionKey Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0003FD39 File Offset: 0x0003DF39
		public override IReadOnlyMailItem ReadOnlyMailItem
		{
			get
			{
				return this.mailItem;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0003FD41 File Offset: 0x0003DF41
		public override RoutedMailItem RoutedMailItem
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0003FD4D File Offset: 0x0003DF4D
		public override int MaxMessageRecipients
		{
			get
			{
				return this.mailItem.Recipients.Count;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0003FD5F File Offset: 0x0003DF5F
		public override int RecipientCount
		{
			get
			{
				return this.recipientsPending;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0003FD67 File Offset: 0x0003DF67
		public override IList<MailRecipient> ReadyRecipientsList
		{
			get
			{
				throw new NotSupportedException("This should not be called on Front End");
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0003FD73 File Offset: 0x0003DF73
		public override IEnumerable<MailRecipient> ReadyRecipients
		{
			get
			{
				return this.mailItem.RecipientList;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0003FD80 File Offset: 0x0003DF80
		public override int ActiveQueueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0003FD83 File Offset: 0x0003DF83
		public override int TotalQueueLength
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003FD86 File Offset: 0x0003DF86
		public override void ConnectionAttemptSucceeded()
		{
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003FD88 File Offset: 0x0003DF88
		public override RoutedMailItem GetNextRoutedMailItem()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003FD8F File Offset: 0x0003DF8F
		public override IReadOnlyMailItem GetNextMailItem()
		{
			return this.mailItem;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003FD97 File Offset: 0x0003DF97
		public override MailRecipient GetNextRecipient()
		{
			if (this.recipientEnumerator.MoveNext())
			{
				return this.recipientEnumerator.Current;
			}
			return null;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003FDB4 File Offset: 0x0003DFB4
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
				this.proxyLayer.AckConnection(status, smtpResponse, failureReason);
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

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003FE0E File Offset: 0x0003E00E
		public override void CreateConnectionIfNecessary()
		{
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003FE10 File Offset: 0x0003E010
		public override void AckMailItem(AckStatus ackStatus, SmtpResponse smtpResponse, AckDetails details, TimeSpan? retryInterval, MessageTrackingSource source, string messageTrackingSourceContext, LatencyComponent deliveryComponent, string remoteMta, bool shadowed, string primaryServer, bool reportEndToEndLatencies)
		{
			if (this.recipientsPending != 0 && ackStatus == AckStatus.Success)
			{
				throw new InvalidOperationException("Cannot ack message successfully until all pending recipients have been acked");
			}
			if (ackStatus == AckStatus.Pending)
			{
				this.recipientsPending = this.mailItem.Recipients.Count;
				this.recipientEnumerator = this.mailItem.RecipientList.GetEnumerator();
				return;
			}
			this.mailItem.FinalizeDeliveryLatencyTracking(deliveryComponent);
			if (ackStatus == AckStatus.Success)
			{
				LatencyFormatter latencyFormatter = new LatencyFormatter(this.mailItem, Components.Configuration.LocalServer.TransportServer.Fqdn, false);
				latencyFormatter.FormatAndUpdatePerfCounters();
			}
			this.proxyLayer.ReleaseMailItem();
			this.mailItem = null;
			this.recipientEnumerator = null;
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003FEB8 File Offset: 0x0003E0B8
		public override void AckRecipient(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string, string>((long)this.GetHashCode(), "InboundProxyNextHopConnection.AckRecipient. Ackstatus  = {0}. SmtpResponse = {1}", ackStatus.ToString(), smtpResponse.ToString());
			if (this.recipientsPending <= 0)
			{
				throw new InvalidOperationException("AckRecipient called but no recipients left to ack");
			}
			this.recipientsPending--;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003FF15 File Offset: 0x0003E115
		public override void ResetQueueLastRetryTimeAndError()
		{
			throw new NotSupportedException();
		}

		// Token: 0x040007EA RID: 2026
		private IEnumerator<MailRecipient> recipientEnumerator;

		// Token: 0x040007EB RID: 2027
		private int recipientsPending;

		// Token: 0x040007EC RID: 2028
		private InboundProxyRoutedMailItem mailItem;

		// Token: 0x040007ED RID: 2029
		private NextHopSolutionKey key;

		// Token: 0x040007EE RID: 2030
		private IInboundProxyLayer proxyLayer;
	}
}
