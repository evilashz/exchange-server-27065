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
	// Token: 0x02000428 RID: 1064
	internal class InboundBdatProxyLayer : InboundProxyLayer
	{
		// Token: 0x06003129 RID: 12585 RVA: 0x000C4544 File Offset: 0x000C2744
		public InboundBdatProxyLayer(ulong sessionId, IPEndPoint clientEndPoint, string clientHelloDomain, IEhloOptions ehloOptions, uint xProxyFromSeqNum, TransportMailItem mailItem, bool internalDestination, IEnumerable<INextHopServer> destinations, ulong maxUnconsumedBytes, IProtocolLogSession logSession, SmtpOutConnectionHandler smtpOutConnectionHandler, bool preserveTargetResponse, Permission permissions, AuthenticationSource authenticationSource, IInboundProxyDestinationTracker inboundProxyDestinationTracker) : base(sessionId, clientEndPoint, clientHelloDomain, ehloOptions, xProxyFromSeqNum, mailItem, internalDestination, destinations, maxUnconsumedBytes, logSession, smtpOutConnectionHandler, preserveTargetResponse, permissions, authenticationSource, false, inboundProxyDestinationTracker)
		{
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000C4573 File Offset: 0x000C2773
		public override bool IsBdat
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000C4576 File Offset: 0x000C2776
		public override long OutboundChunkSize
		{
			get
			{
				return this.outboundChunkSize;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000C457E File Offset: 0x000C277E
		public override bool IsLastChunk
		{
			get
			{
				return this.isLastChunk;
			}
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000C4588 File Offset: 0x000C2788
		public static void CallOutstandingWaitForCommandCallback(object state)
		{
			InboundBdatProxyLayer.OutstandingWaitForCommandCallbackAsyncState outstandingWaitForCommandCallbackAsyncState = (InboundBdatProxyLayer.OutstandingWaitForCommandCallbackAsyncState)state;
			outstandingWaitForCommandCallbackAsyncState.Callback(outstandingWaitForCommandCallbackAsyncState.CommandReceived);
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000C45B0 File Offset: 0x000C27B0
		protected override void ShutdownProxySession()
		{
			base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatShutdownProxySessionEnter);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Shutting down proxy session");
			if (!this.shutdownSmtpOutSession)
			{
				lock (this.StateLock)
				{
					base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatShutdownProxySessionEnterLock);
					this.shutdownSmtpOutSession = true;
					if (this.outstandingReadCompleteCallback != null)
					{
						base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatShutdownProxySessionCallOutstandingReadCallback);
						InboundProxyLayer.OutstandingReadCallbackAsyncState state = new InboundProxyLayer.OutstandingReadCallbackAsyncState(null, true, this.outstandingReadCompleteCallback);
						this.outstandingReadCompleteCallback = null;
						ThreadPool.QueueUserWorkItem(InboundProxyLayer.CallOutstandingReadCallBackDelegate, state);
					}
					if (this.outstandingWaitForCommandCallback != null)
					{
						base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatShutdownProxySessionCallOutstandingWaitForCommandCallback);
						InboundBdatProxyLayer.OutstandingWaitForCommandCallbackAsyncState state2 = new InboundBdatProxyLayer.OutstandingWaitForCommandCallbackAsyncState(false, this.outstandingWaitForCommandCallback);
						this.outstandingWaitForCommandCallback = null;
						ThreadPool.QueueUserWorkItem(InboundBdatProxyLayer.CallOutstandingWaitForCommandCallBackDelegate, state2);
					}
				}
			}
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000C4684 File Offset: 0x000C2884
		public void CreateNewCommand(long inboundChunkSize, long outboundChunkSize, bool isLast)
		{
			base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatCreateNewCommandEnter);
			lock (this.StateLock)
			{
				base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatCreateNewCommandEnterLock);
				if (this.hasActiveCommand)
				{
					throw new InvalidOperationException("Active command already present");
				}
				if (this.isLastChunk && isLast)
				{
					throw new InvalidOperationException("Last command has already been sent");
				}
				if (this.discardingInboundData)
				{
					throw new InvalidOperationException("New command should not be created when already discarding data");
				}
				if (inboundChunkSize == 0L && !isLast)
				{
					throw new InvalidOperationException("Chunk size should not be zero unless last chunk");
				}
				this.outboundChunkSize = outboundChunkSize;
				this.inboundChunkSize = inboundChunkSize;
				this.isLastChunk = isLast;
				this.targetResponse = SmtpResponse.Empty;
				this.eodSeen = false;
				this.hasActiveCommand = true;
				if (this.outstandingWaitForCommandCallback != null)
				{
					base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatCreateNewCommandCallOutstandingWaitForCommandCallback);
					InboundBdatProxyLayer.OutstandingWaitForCommandCallbackAsyncState state = new InboundBdatProxyLayer.OutstandingWaitForCommandCallbackAsyncState(true, this.outstandingWaitForCommandCallback);
					this.outstandingWaitForCommandCallback = null;
					ThreadPool.QueueUserWorkItem(InboundBdatProxyLayer.CallOutstandingWaitForCommandCallBackDelegate, state);
				}
			}
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000C477C File Offset: 0x000C297C
		public override void WaitForNewCommand(InboundBdatProxyLayer.CommandReceivedCallback commandReceivedCallback)
		{
			base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatWaitForNewCommand);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundBdatProxyLayer.WaitForNewCommand");
			if (commandReceivedCallback == null)
			{
				throw new ArgumentNullException("commandReceivedCallback");
			}
			bool flag = false;
			lock (this.StateLock)
			{
				base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatWaitForNewCommandEnterLock);
				if (this.outstandingWaitForCommandCallback != null)
				{
					throw new InvalidOperationException("A pending WaitForCommand is already present");
				}
				if (this.hasActiveCommand)
				{
					flag = true;
				}
				else
				{
					this.outstandingWaitForCommandCallback = commandReceivedCallback;
				}
			}
			if (flag)
			{
				commandReceivedCallback(true);
			}
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000C481C File Offset: 0x000C2A1C
		public override void AckCommandSuccessful()
		{
			base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatAckCommandSuccessful);
			if (this.IsLastChunk)
			{
				throw new InvalidOperationException("AckCommandSuccessful should not be called for the last chunk");
			}
			if (!this.targetResponse.Equals(SmtpResponse.Empty))
			{
				throw new InvalidOperationException("Ack has already been called");
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundBdatProxyLayer.AckCommandSuccessful");
			lock (this.StateLock)
			{
				base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatAckCommandSuccessfulEnterLock);
				this.targetResponse = SmtpResponse.OctetsReceived(this.inboundChunkSize);
				if (this.outstandingWriteCompleteCallback != null)
				{
					base.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BdatAckCommandSuccessfulCallOutstandingWriteCallback);
					InboundProxyLayer.OutstandingWriteCallbackAsyncState state = new InboundProxyLayer.OutstandingWriteCallbackAsyncState(this.targetResponse, this.outstandingWriteCompleteCallback);
					this.outstandingWriteCompleteCallback = null;
					ThreadPool.QueueUserWorkItem(InboundProxyLayer.CallOutstandingWriteCallBackDelegate, state);
				}
				this.hasActiveCommand = false;
			}
		}

		// Token: 0x04001802 RID: 6146
		private static readonly WaitCallback CallOutstandingWaitForCommandCallBackDelegate = new WaitCallback(InboundBdatProxyLayer.CallOutstandingWaitForCommandCallback);

		// Token: 0x04001803 RID: 6147
		private long inboundChunkSize;

		// Token: 0x04001804 RID: 6148
		private long outboundChunkSize;

		// Token: 0x04001805 RID: 6149
		private bool isLastChunk;

		// Token: 0x04001806 RID: 6150
		private InboundBdatProxyLayer.CommandReceivedCallback outstandingWaitForCommandCallback;

		// Token: 0x02000429 RID: 1065
		// (Invoke) Token: 0x06003134 RID: 12596
		public delegate void CommandReceivedCallback(bool commandReceived);

		// Token: 0x0200042A RID: 1066
		public class OutstandingWaitForCommandCallbackAsyncState
		{
			// Token: 0x06003137 RID: 12599 RVA: 0x000C490F File Offset: 0x000C2B0F
			public OutstandingWaitForCommandCallbackAsyncState(bool commandReceived, InboundBdatProxyLayer.CommandReceivedCallback callback)
			{
				if (callback == null)
				{
					throw new ArgumentNullException("callback");
				}
				this.Callback = callback;
				this.CommandReceived = commandReceived;
			}

			// Token: 0x04001807 RID: 6151
			public readonly bool CommandReceived;

			// Token: 0x04001808 RID: 6152
			public readonly InboundBdatProxyLayer.CommandReceivedCallback Callback;
		}
	}
}
