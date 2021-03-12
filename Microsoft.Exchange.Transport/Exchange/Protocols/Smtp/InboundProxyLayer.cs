using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Common.Cache;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Logging;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000422 RID: 1058
	internal abstract class InboundProxyLayer : IInboundProxyLayer
	{
		// Token: 0x060030FB RID: 12539 RVA: 0x000C3B8C File Offset: 0x000C1D8C
		public static SmtpResponse GetEncodedProxyFailureResponse(SessionSetupFailureReason failureReason)
		{
			switch (failureReason)
			{
			case SessionSetupFailureReason.None:
				return SmtpResponse.GenericProxyFailure;
			case SessionSetupFailureReason.UserLookupFailure:
				return SmtpResponse.EncodedProxyFailureResponseUserLookupFailure;
			case SessionSetupFailureReason.DnsLookupFailure:
				return SmtpResponse.EncodedProxyFailureResponseDnsError;
			case SessionSetupFailureReason.ConnectionFailure:
				return SmtpResponse.EncodedProxyFailureResponseConnectionFailure;
			case SessionSetupFailureReason.ProtocolError:
				return SmtpResponse.EncodedProxyFailureResponseProtocolError;
			case SessionSetupFailureReason.SocketError:
				return SmtpResponse.EncodedProxyFailureResponseSocketError;
			case SessionSetupFailureReason.Shutdown:
				return SmtpResponse.EncodedProxyFailureResponseShutdown;
			case SessionSetupFailureReason.BackEndLocatorFailure:
				return SmtpResponse.EncodedProxyFailureResponseBackEndLocatorFailure;
			default:
				throw new InvalidOperationException("Invalid session failure reason");
			}
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000C3C00 File Offset: 0x000C1E00
		protected InboundProxyLayer(ulong sessionId, IPEndPoint clientEndPoint, string clientHelloDomain, IEhloOptions ehloOptions, uint xProxyFromSeqNum, TransportMailItem mailItem, bool internalDestination, IEnumerable<INextHopServer> destinations, ulong maxUnconsumedBytes, IProtocolLogSession logSession, SmtpOutConnectionHandler smtpOutConnectionHandler, bool preserveTargetResponse, Permission permissions, AuthenticationSource authenticationSource, bool hasActiveCommand, IInboundProxyDestinationTracker inboundProxyDestinationTracker)
		{
			ArgumentValidator.ThrowIfNull("clientEndPoint", clientEndPoint);
			ArgumentValidator.ThrowIfNull("ehloOptions", ehloOptions);
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			ArgumentValidator.ThrowIfNull("destinations", destinations);
			ArgumentValidator.ThrowIfNull("logSession", logSession);
			ArgumentValidator.ThrowIfNull("inboundProxyDestinationTracker", inboundProxyDestinationTracker);
			this.sessionId = sessionId;
			this.clientEndPoint = clientEndPoint;
			this.clientHelloDomain = clientHelloDomain;
			this.smtpInEhloOptions = ehloOptions;
			this.xProxyFromSeqNum = xProxyFromSeqNum;
			this.mailItem = new InboundProxyRoutedMailItem(mailItem);
			this.proxyDestinations = destinations;
			this.internalDestination = internalDestination;
			this.proxyNextHopSolutionKey = new NextHopSolutionKey(NextHopType.Empty, internalDestination ? "InternalProxy" : "ExternalProxy", Guid.Empty);
			this.unconsumedBytes = 0UL;
			this.maxUnconsumedBytes = maxUnconsumedBytes;
			this.logSession = logSession;
			this.SmtpOutConnectionHandler = smtpOutConnectionHandler;
			this.preserveTargetResponse = preserveTargetResponse;
			this.permissions = permissions;
			this.authenticationSource = authenticationSource;
			this.hasActiveCommand = hasActiveCommand;
			this.inboundProxyDestinationTracker = inboundProxyDestinationTracker;
			if (Util.TryGetNextHopFqdnProperty(mailItem.ExtendedPropertyDictionary, out this.nextHopFqdn))
			{
				this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "NextHopFqdn: {0}", new object[]
				{
					this.nextHopFqdn
				});
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000C3D79 File Offset: 0x000C1F79
		public IInboundProxyDestinationTracker InboundProxyDestinationTracker
		{
			get
			{
				return this.inboundProxyDestinationTracker;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000C3D81 File Offset: 0x000C1F81
		public IPEndPoint ClientEndPoint
		{
			get
			{
				return this.clientEndPoint;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x000C3D89 File Offset: 0x000C1F89
		public string ClientHelloDomain
		{
			get
			{
				return this.clientHelloDomain;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000C3D91 File Offset: 0x000C1F91
		public ulong SessionId
		{
			get
			{
				return this.sessionId;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x000C3D99 File Offset: 0x000C1F99
		public string NextHopFqdn
		{
			get
			{
				return this.nextHopFqdn;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000C3DA1 File Offset: 0x000C1FA1
		public IEhloOptions SmtpInEhloOptions
		{
			get
			{
				return this.smtpInEhloOptions;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x000C3DA9 File Offset: 0x000C1FA9
		public long BytesRead
		{
			get
			{
				return this.bytesRead;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000C3DB1 File Offset: 0x000C1FB1
		public long BytesWritten
		{
			get
			{
				return this.bytesWritten;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000C3DB9 File Offset: 0x000C1FB9
		public uint XProxyFromSeqNum
		{
			get
			{
				return this.xProxyFromSeqNum;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000C3DC1 File Offset: 0x000C1FC1
		public Permission Permissions
		{
			get
			{
				return this.permissions;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000C3DC9 File Offset: 0x000C1FC9
		public AuthenticationSource AuthenticationSource
		{
			get
			{
				return this.authenticationSource;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003108 RID: 12552
		public abstract bool IsBdat { get; }

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003109 RID: 12553
		public abstract long OutboundChunkSize { get; }

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x0600310A RID: 12554
		public abstract bool IsLastChunk { get; }

		// Token: 0x0600310B RID: 12555 RVA: 0x000C3DD4 File Offset: 0x000C1FD4
		public static void CallOutstandingReadCallback(object state)
		{
			InboundProxyLayer.OutstandingReadCallbackAsyncState outstandingReadCallbackAsyncState = (InboundProxyLayer.OutstandingReadCallbackAsyncState)state;
			outstandingReadCallbackAsyncState.Callback(outstandingReadCallbackAsyncState.Buffer, outstandingReadCallbackAsyncState.LastBuffer);
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000C3E00 File Offset: 0x000C2000
		public static void CallOutstandingWriteCallback(object state)
		{
			InboundProxyLayer.OutstandingWriteCallbackAsyncState outstandingWriteCallbackAsyncState = (InboundProxyLayer.OutstandingWriteCallbackAsyncState)state;
			outstandingWriteCallbackAsyncState.Callback(outstandingWriteCallbackAsyncState.Response);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000C3E28 File Offset: 0x000C2028
		public void NotifySmtpInStopProxy()
		{
			if (!this.discardingInboundData)
			{
				lock (this.StateLock)
				{
					this.discardingInboundData = true;
				}
			}
			this.ShutdownProxySession();
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000C3E78 File Offset: 0x000C2078
		public void DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs breadcrumb)
		{
			this.breadcrumbs.Drop(breadcrumb);
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000C3E88 File Offset: 0x000C2088
		public void SetupProxySession()
		{
			this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.SetupProxySession);
			WaitCallback callBack = new WaitCallback(this.StartConnection);
			ThreadPool.QueueUserWorkItem(callBack);
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000C3EB0 File Offset: 0x000C20B0
		public void BeginWriteData(byte[] buffer, int offset, int count, bool endOfData, InboundProxyLayer.CompletionCallback writeCompleteCallback)
		{
			this.BeginWriteData(buffer, offset, count, endOfData, writeCompleteCallback, true);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000C3EC0 File Offset: 0x000C20C0
		public void BeginWriteData(byte[] buffer, int offset, int count, bool endOfData, InboundProxyLayer.CompletionCallback writeCompleteCallback, bool copyBuffer)
		{
			this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginWriteDataEnter);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<int, int, bool>((long)this.GetHashCode(), "BeginWriteData offset = {0}, count = {1}, endOfData = {2}", offset, count, endOfData);
			if (count < 0)
			{
				throw new ArgumentException("Count cannot be negative");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (writeCompleteCallback == null)
			{
				throw new ArgumentNullException("writeCompleteCallback");
			}
			if (this.discardingInboundData)
			{
				throw new InvalidOperationException("Should not write to the proxy layer when discarding");
			}
			if (this.eodSeen)
			{
				throw new InvalidOperationException("Should not write to the proxy layer when EOD has already been seen");
			}
			BufferCacheEntry bufferCacheEntry;
			if (copyBuffer)
			{
				bufferCacheEntry = this.SmtpOutConnectionHandler.BufferCache.GetBuffer(count);
				Buffer.BlockCopy(buffer, offset, bufferCacheEntry.Buffer, 0, count);
			}
			else
			{
				bufferCacheEntry = new BufferCacheEntry(buffer, false);
				count = buffer.Length;
			}
			bool flag = true;
			SmtpResponse response;
			lock (this.StateLock)
			{
				this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginWriteDataEnterLock);
				if (this.outstandingWriteCompleteCallback != null)
				{
					throw new InvalidOperationException("There is already a pending write");
				}
				response = this.targetResponse;
				if (this.targetResponse.Equals(SmtpResponse.Empty))
				{
					this.eodSeen = endOfData;
					if (count != 0)
					{
						this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginWriteDataEnqueueBuffer);
						this.bufferQueue.Enqueue(bufferCacheEntry);
						this.unconsumedBytes += (ulong)((long)count);
						this.bytesWritten += (long)count;
						if (this.outstandingReadCompleteCallback != null)
						{
							this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginWriteDataCallOutstandingReadCallback);
							BufferCacheEntry bufferCacheEntry2 = this.bufferQueue.Dequeue();
							this.unconsumedBytes -= (ulong)((long)bufferCacheEntry2.Buffer.Length);
							this.bytesRead += (long)bufferCacheEntry2.Buffer.Length;
							bool lastBuffer = this.eodSeen && this.bufferQueue.Count == 0;
							InboundProxyLayer.OutstandingReadCallbackAsyncState state = new InboundProxyLayer.OutstandingReadCallbackAsyncState(bufferCacheEntry2, lastBuffer, this.outstandingReadCompleteCallback);
							this.outstandingReadCompleteCallback = null;
							ThreadPool.QueueUserWorkItem(InboundProxyLayer.CallOutstandingReadCallBackDelegate, state);
						}
					}
					if (this.unconsumedBytes >= this.maxUnconsumedBytes || endOfData)
					{
						this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginWriteDataEnqueueWriteCallback);
						this.outstandingWriteCompleteCallback = writeCompleteCallback;
						flag = false;
					}
				}
				else
				{
					this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginWriteDataResponseAlreadySet);
				}
			}
			if (flag)
			{
				writeCompleteCallback(response);
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000C40E8 File Offset: 0x000C22E8
		public void BeginReadData(InboundProxyLayer.ReadCompletionCallback readCompleteCallback)
		{
			this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginReadDataEnter);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundProxyLayer.BeginReadData");
			if (!this.targetResponse.Equals(SmtpResponse.Empty))
			{
				throw new InvalidOperationException("Cannot read from proxy layer after acking message or connection");
			}
			BufferCacheEntry bufferCacheEntry = null;
			if (this.shutdownSmtpOutSession)
			{
				this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginReadDataShutdownSmtpOutSession);
				readCompleteCallback(null, false);
				return;
			}
			bool lastBuffer;
			lock (this.StateLock)
			{
				this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginReadDataEnterLock);
				if (!this.hasActiveCommand)
				{
					throw new InvalidOperationException("Cannot read from proxy layer when there is no active command");
				}
				if (this.outstandingReadCompleteCallback != null)
				{
					throw new InvalidOperationException("There is already a pending read from the proxy layer");
				}
				if (this.bufferQueue.Count != 0)
				{
					this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginReadDataDequeueBuffer);
					bufferCacheEntry = this.bufferQueue.Dequeue();
					this.unconsumedBytes -= (ulong)((long)bufferCacheEntry.Buffer.Length);
					this.bytesRead += (long)bufferCacheEntry.Buffer.Length;
					if (this.outstandingWriteCompleteCallback != null && !this.eodSeen && this.unconsumedBytes < this.maxUnconsumedBytes)
					{
						this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginReadDataCallOutstandingWriteCallback);
						InboundProxyLayer.OutstandingWriteCallbackAsyncState state = new InboundProxyLayer.OutstandingWriteCallbackAsyncState(this.targetResponse, this.outstandingWriteCompleteCallback);
						this.outstandingWriteCompleteCallback = null;
						ThreadPool.QueueUserWorkItem(InboundProxyLayer.CallOutstandingWriteCallBackDelegate, state);
					}
				}
				else
				{
					this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.BeginReadDataEnqueueReadCallback);
					this.outstandingReadCompleteCallback = readCompleteCallback;
				}
				lastBuffer = (this.eodSeen && this.bufferQueue.Count == 0);
			}
			if (bufferCacheEntry != null)
			{
				readCompleteCallback(bufferCacheEntry, lastBuffer);
			}
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000C4284 File Offset: 0x000C2484
		public void AckMessage(AckStatus status, SmtpResponse response, string source, SessionSetupFailureReason failureReason)
		{
			this.AckMessage(status, response, true, source, failureReason);
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000C4292 File Offset: 0x000C2492
		public void AckMessage(AckStatus status, SmtpResponse response, bool replaceFailureResponse, string source, SessionSetupFailureReason failureReason)
		{
			this.AckMessage(status, response, replaceFailureResponse, source, failureReason, false);
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000C42A2 File Offset: 0x000C24A2
		public void AckConnection(AckStatus status, SmtpResponse response, SessionSetupFailureReason failureReason)
		{
			this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.AckConnection);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<AckStatus, SmtpResponse>((long)this.GetHashCode(), "InboundProxyLayer.AckConnection. Ackstatus  = {0}. SmtpResponse = {1}.", status, response);
			this.AckMessage(status, response, true, "InboundProxyLayer.AckConnection", failureReason, true);
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000C42D4 File Offset: 0x000C24D4
		public void ReleaseMailItem()
		{
			this.mailItem.ReleaseFromActive();
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000C42E1 File Offset: 0x000C24E1
		public void Shutdown()
		{
			this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.Shutdown);
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "InboundProxyLayer.Shutdown");
			this.AckMessage(AckStatus.Fail, SmtpResponse.ServiceUnavailable, "InboundProxyLayer.Shutdown", SessionSetupFailureReason.Shutdown);
			this.ShutdownProxySession();
		}

		// Token: 0x06003118 RID: 12568
		public abstract void WaitForNewCommand(InboundBdatProxyLayer.CommandReceivedCallback commandReceivedCallback);

		// Token: 0x06003119 RID: 12569
		public abstract void AckCommandSuccessful();

		// Token: 0x0600311A RID: 12570
		protected abstract void ShutdownProxySession();

		// Token: 0x0600311B RID: 12571 RVA: 0x000C431C File Offset: 0x000C251C
		private void StartConnection(object state)
		{
			string text = string.Format("Starting outbound connection for inbound session {0}", this.SessionId.ToString("X16", NumberFormatInfo.InvariantInfo));
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), text);
			InboundProxyNextHopConnection connection = new InboundProxyNextHopConnection(this, this.proxyNextHopSolutionKey, this.mailItem);
			this.SmtpOutConnectionHandler.HandleProxyConnection(connection, this.proxyDestinations, this.internalDestination, text);
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000C438C File Offset: 0x000C258C
		private void AckMessage(AckStatus status, SmtpResponse response, bool replaceFailureResponse, string source, SessionSetupFailureReason failureReason, bool connectionAcked)
		{
			this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.AckMessageEnter);
			ExTraceGlobals.SmtpSendTracer.TraceDebug<AckStatus, SmtpResponse, bool>((long)this.GetHashCode(), "InboundProxyLayer.AckMessage. Ackstatus  = {0}. SmtpResponse = {1}. ReplaceFailureResponse = {2}", status, response, replaceFailureResponse);
			lock (this.StateLock)
			{
				this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.AckMessageEnterLock);
				if (this.targetResponse.Equals(SmtpResponse.Empty))
				{
					if (replaceFailureResponse && status != AckStatus.Success)
					{
						this.logSession.LogInformation(ProtocolLoggingLevel.Verbose, null, "Message or connection acked with status {0} and response {1}", new object[]
						{
							status,
							response
						});
					}
					if (status != AckStatus.Pending)
					{
						if (replaceFailureResponse && !this.preserveTargetResponse && status != AckStatus.Success)
						{
							response = InboundProxyLayer.GetEncodedProxyFailureResponse(failureReason);
						}
						if (connectionAcked && status == AckStatus.Success)
						{
							response = SmtpResponse.MessageNotProxiedResponse;
						}
						this.targetResponse = response;
						this.hasActiveCommand = false;
						if (this.outstandingWriteCompleteCallback != null)
						{
							this.DropBreadcrumb(InboundProxyLayer.InboundProxyLayerBreadcrumbs.AckMessageCallOutstandingWriteCallback);
							InboundProxyLayer.OutstandingWriteCallbackAsyncState state = new InboundProxyLayer.OutstandingWriteCallbackAsyncState(this.targetResponse, this.outstandingWriteCompleteCallback);
							this.outstandingWriteCompleteCallback = null;
							ThreadPool.QueueUserWorkItem(InboundProxyLayer.CallOutstandingWriteCallBackDelegate, state);
						}
					}
				}
				this.ackSource = this.ackSource + ":" + source;
			}
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000C44BC File Offset: 0x000C26BC
		public void ReturnBuffer(BufferCacheEntry bufferCacheEntry)
		{
			this.SmtpOutConnectionHandler.BufferCache.ReturnBuffer(bufferCacheEntry);
		}

		// Token: 0x040017B6 RID: 6070
		private const string InternalProxyNextHopDomain = "InternalProxy";

		// Token: 0x040017B7 RID: 6071
		private const string ExternalProxyNextHopDomain = "ExternalProxy";

		// Token: 0x040017B8 RID: 6072
		private const int NumberOfBreadcrumbs = 64;

		// Token: 0x040017B9 RID: 6073
		public static readonly WaitCallback CallOutstandingReadCallBackDelegate = new WaitCallback(InboundProxyLayer.CallOutstandingReadCallback);

		// Token: 0x040017BA RID: 6074
		public static readonly WaitCallback CallOutstandingWriteCallBackDelegate = new WaitCallback(InboundProxyLayer.CallOutstandingWriteCallback);

		// Token: 0x040017BB RID: 6075
		protected readonly object StateLock = new object();

		// Token: 0x040017BC RID: 6076
		protected readonly SmtpOutConnectionHandler SmtpOutConnectionHandler;

		// Token: 0x040017BD RID: 6077
		protected InboundProxyLayer.CompletionCallback outstandingWriteCompleteCallback;

		// Token: 0x040017BE RID: 6078
		protected InboundProxyLayer.ReadCompletionCallback outstandingReadCompleteCallback;

		// Token: 0x040017BF RID: 6079
		protected bool hasActiveCommand;

		// Token: 0x040017C0 RID: 6080
		protected SmtpResponse targetResponse = SmtpResponse.Empty;

		// Token: 0x040017C1 RID: 6081
		protected bool discardingInboundData;

		// Token: 0x040017C2 RID: 6082
		protected bool shutdownSmtpOutSession;

		// Token: 0x040017C3 RID: 6083
		protected bool eodSeen;

		// Token: 0x040017C4 RID: 6084
		private readonly ulong maxUnconsumedBytes;

		// Token: 0x040017C5 RID: 6085
		private readonly IProtocolLogSession logSession;

		// Token: 0x040017C6 RID: 6086
		private readonly ulong sessionId;

		// Token: 0x040017C7 RID: 6087
		private readonly string nextHopFqdn;

		// Token: 0x040017C8 RID: 6088
		private readonly uint xProxyFromSeqNum;

		// Token: 0x040017C9 RID: 6089
		private readonly bool preserveTargetResponse;

		// Token: 0x040017CA RID: 6090
		private readonly IEnumerable<INextHopServer> proxyDestinations;

		// Token: 0x040017CB RID: 6091
		private readonly bool internalDestination;

		// Token: 0x040017CC RID: 6092
		private readonly InboundProxyRoutedMailItem mailItem;

		// Token: 0x040017CD RID: 6093
		private readonly NextHopSolutionKey proxyNextHopSolutionKey;

		// Token: 0x040017CE RID: 6094
		private readonly string clientHelloDomain;

		// Token: 0x040017CF RID: 6095
		private readonly Permission permissions;

		// Token: 0x040017D0 RID: 6096
		private readonly AuthenticationSource authenticationSource;

		// Token: 0x040017D1 RID: 6097
		private readonly Queue<BufferCacheEntry> bufferQueue = new Queue<BufferCacheEntry>();

		// Token: 0x040017D2 RID: 6098
		private ulong unconsumedBytes;

		// Token: 0x040017D3 RID: 6099
		private readonly IPEndPoint clientEndPoint;

		// Token: 0x040017D4 RID: 6100
		private readonly IEhloOptions smtpInEhloOptions;

		// Token: 0x040017D5 RID: 6101
		private long bytesRead;

		// Token: 0x040017D6 RID: 6102
		private long bytesWritten;

		// Token: 0x040017D7 RID: 6103
		private string ackSource = string.Empty;

		// Token: 0x040017D8 RID: 6104
		private readonly IInboundProxyDestinationTracker inboundProxyDestinationTracker;

		// Token: 0x040017D9 RID: 6105
		private readonly Breadcrumbs<InboundProxyLayer.InboundProxyLayerBreadcrumbs> breadcrumbs = new Breadcrumbs<InboundProxyLayer.InboundProxyLayerBreadcrumbs>(64);

		// Token: 0x02000423 RID: 1059
		// (Invoke) Token: 0x06003120 RID: 12576
		public delegate void CompletionCallback(SmtpResponse response);

		// Token: 0x02000424 RID: 1060
		// (Invoke) Token: 0x06003124 RID: 12580
		public delegate void ReadCompletionCallback(BufferCacheEntry bytes, bool lastBuffer);

		// Token: 0x02000425 RID: 1061
		public enum InboundProxyLayerBreadcrumbs
		{
			// Token: 0x040017DB RID: 6107
			EMPTY,
			// Token: 0x040017DC RID: 6108
			SetupProxySession,
			// Token: 0x040017DD RID: 6109
			BeginWriteDataEnter,
			// Token: 0x040017DE RID: 6110
			BeginWriteDataEnterLock,
			// Token: 0x040017DF RID: 6111
			BeginWriteDataResponseAlreadySet,
			// Token: 0x040017E0 RID: 6112
			BeginWriteDataEnqueueBuffer,
			// Token: 0x040017E1 RID: 6113
			BeginWriteDataCallOutstandingReadCallback,
			// Token: 0x040017E2 RID: 6114
			BeginWriteDataEnqueueWriteCallback,
			// Token: 0x040017E3 RID: 6115
			BeginReadDataEnter,
			// Token: 0x040017E4 RID: 6116
			BeginReadDataShutdownSmtpOutSession,
			// Token: 0x040017E5 RID: 6117
			BeginReadDataEnterLock,
			// Token: 0x040017E6 RID: 6118
			BeginReadDataDequeueBuffer,
			// Token: 0x040017E7 RID: 6119
			BeginReadDataCallOutstandingWriteCallback,
			// Token: 0x040017E8 RID: 6120
			BeginReadDataEnqueueReadCallback,
			// Token: 0x040017E9 RID: 6121
			AckMessageEnter,
			// Token: 0x040017EA RID: 6122
			AckMessageEnterLock,
			// Token: 0x040017EB RID: 6123
			AckMessageCallOutstandingWriteCallback,
			// Token: 0x040017EC RID: 6124
			AckConnection,
			// Token: 0x040017ED RID: 6125
			BdatShutdownProxySessionEnter,
			// Token: 0x040017EE RID: 6126
			BdatShutdownProxySessionEnterLock,
			// Token: 0x040017EF RID: 6127
			BdatShutdownProxySessionCallOutstandingReadCallback,
			// Token: 0x040017F0 RID: 6128
			BdatShutdownProxySessionCallOutstandingWaitForCommandCallback,
			// Token: 0x040017F1 RID: 6129
			BdatCreateNewCommandEnter,
			// Token: 0x040017F2 RID: 6130
			BdatCreateNewCommandEnterLock,
			// Token: 0x040017F3 RID: 6131
			BdatCreateNewCommandCallOutstandingWaitForCommandCallback,
			// Token: 0x040017F4 RID: 6132
			BdatWaitForNewCommand,
			// Token: 0x040017F5 RID: 6133
			BdatWaitForNewCommandEnterLock,
			// Token: 0x040017F6 RID: 6134
			BdatAckCommandSuccessful,
			// Token: 0x040017F7 RID: 6135
			BdatAckCommandSuccessfulEnterLock,
			// Token: 0x040017F8 RID: 6136
			BdatAckCommandSuccessfulCallOutstandingWriteCallback,
			// Token: 0x040017F9 RID: 6137
			DataShutdownProxySessionEnter,
			// Token: 0x040017FA RID: 6138
			DataShutdownProxySessionEnterLock,
			// Token: 0x040017FB RID: 6139
			DataShutdownProxySessionCallOutstandingReadCallback,
			// Token: 0x040017FC RID: 6140
			Shutdown
		}

		// Token: 0x02000426 RID: 1062
		public class OutstandingReadCallbackAsyncState
		{
			// Token: 0x06003127 RID: 12583 RVA: 0x000C44F3 File Offset: 0x000C26F3
			public OutstandingReadCallbackAsyncState(BufferCacheEntry buffer, bool lastBuffer, InboundProxyLayer.ReadCompletionCallback callback)
			{
				if (callback == null)
				{
					throw new ArgumentNullException("callback");
				}
				this.Buffer = buffer;
				this.LastBuffer = lastBuffer;
				this.Callback = callback;
			}

			// Token: 0x040017FD RID: 6141
			public readonly BufferCacheEntry Buffer;

			// Token: 0x040017FE RID: 6142
			public readonly InboundProxyLayer.ReadCompletionCallback Callback;

			// Token: 0x040017FF RID: 6143
			public readonly bool LastBuffer;
		}

		// Token: 0x02000427 RID: 1063
		public class OutstandingWriteCallbackAsyncState
		{
			// Token: 0x06003128 RID: 12584 RVA: 0x000C451E File Offset: 0x000C271E
			public OutstandingWriteCallbackAsyncState(SmtpResponse response, InboundProxyLayer.CompletionCallback callback)
			{
				if (callback == null)
				{
					throw new ArgumentNullException("callback");
				}
				this.Response = response;
				this.Callback = callback;
			}

			// Token: 0x04001800 RID: 6144
			public readonly InboundProxyLayer.CompletionCallback Callback;

			// Token: 0x04001801 RID: 6145
			public readonly SmtpResponse Response;
		}
	}
}
