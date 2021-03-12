using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200042B RID: 1067
	internal class InboundDataProxyLayer : InboundProxyLayer
	{
		// Token: 0x06003138 RID: 12600 RVA: 0x000C4934 File Offset: 0x000C2B34
		public InboundDataProxyLayer(ulong sessionId, IPEndPoint clientEndPoint, string clientHelloDomain, IEhloOptions ehloOptions, uint xProxyFromSeqNum, TransportMailItem mailItem, bool internalDestination, IEnumerable<INextHopServer> destinations, ulong maxUnconsumedBytes, IProtocolLogSession logSession, SmtpOutConnectionHandler smtpOutConnectionHandler, bool preserveTargetResponse, Permission permissions, AuthenticationSource authenticationSource, IInboundProxyDestinationTracker inboundProxyDestinationTracker) : base(sessionId, clientEndPoint, clientHelloDomain, ehloOptions, xProxyFromSeqNum, mailItem, internalDestination, destinations, maxUnconsumedBytes, logSession, smtpOutConnectionHandler, preserveTargetResponse, permissions, authenticationSource, true, inboundProxyDestinationTracker)
		{
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000C4963 File Offset: 0x000C2B63
		public override bool IsBdat
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000C4966 File Offset: 0x000C2B66
		public override long OutboundChunkSize
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x000C496A File Offset: 0x000C2B6A
		public override bool IsLastChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000C4970 File Offset: 0x000C2B70
		protected override void ShutdownProxySession()
		{
			base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.DataShutdownProxySessionEnter);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Shutting down proxy session");
			if (!this.shutdownSmtpOutSession)
			{
				lock (this.StateLock)
				{
					base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.DataShutdownProxySessionEnter);
					this.shutdownSmtpOutSession = true;
					if (this.outstandingReadCompleteCallback != null)
					{
						base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.DataShutdownProxySessionCallOutstandingReadCallback);
						InboundProxyLayer.OutstandingReadCallbackAsyncState state = new InboundProxyLayer.OutstandingReadCallbackAsyncState(null, true, this.outstandingReadCompleteCallback);
						this.outstandingReadCompleteCallback = null;
						ThreadPool.QueueUserWorkItem(InboundProxyLayer.CallOutstandingReadCallBackDelegate, state);
					}
				}
			}
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000C4A10 File Offset: 0x000C2C10
		public override void WaitForNewCommand(InboundBdatProxyLayer.CommandReceivedCallback commandReceivedCallback)
		{
			throw new InvalidOperationException("WaitForNewCommand should not be called for DATA since there should be only one");
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000C4A1C File Offset: 0x000C2C1C
		public override void AckCommandSuccessful()
		{
			throw new InvalidOperationException("AckCommandSuccessful should not be called for DATA");
		}
	}
}
