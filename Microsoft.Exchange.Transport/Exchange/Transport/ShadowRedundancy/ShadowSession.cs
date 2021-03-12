using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Protocols.Smtp;
using Microsoft.Exchange.Transport.Logging.MessageTracking;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200038F RID: 911
	internal abstract class ShadowSession : IShadowSession
	{
		// Token: 0x06002834 RID: 10292 RVA: 0x0009CC28 File Offset: 0x0009AE28
		public ShadowSession(ISmtpInSession inSession, ShadowRedundancyManager shadowRedundancyManager, ShadowHubPickerBase hubPicker, ISmtpOutConnectionHandler connectionHandler)
		{
			if (inSession == null)
			{
				throw new ArgumentNullException("inSession");
			}
			if (shadowRedundancyManager == null)
			{
				throw new ArgumentNullException("shadowRedundancyManager");
			}
			if (hubPicker == null)
			{
				throw new ArgumentNullException("hubPicker");
			}
			if (connectionHandler == null)
			{
				throw new ArgumentNullException("connectionHandler");
			}
			this.inSession = inSession;
			this.shadowRedundancyManager = shadowRedundancyManager;
			this.hubPicker = hubPicker;
			this.connectionHandler = connectionHandler;
			this.writeCompleteCallback = new InboundProxyLayer.CompletionCallback(this.WriteProxyDataComplete);
			this.negotiationTimer = ShadowRedundancyManager.PerfCounters.ShadowNegotiationLatencyCounter();
			this.selectionTimer = ShadowRedundancyManager.PerfCounters.ShadowSelectionLatencyCounter();
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x0009CCE7 File Offset: 0x0009AEE7
		// (set) Token: 0x06002836 RID: 10294 RVA: 0x0009CCEF File Offset: 0x0009AEEF
		public string ShadowServerContext
		{
			get
			{
				return this.shadowServerContext;
			}
			set
			{
				this.shadowServerContext = value;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x0009CCF8 File Offset: 0x0009AEF8
		internal bool Complete
		{
			get
			{
				return this.shadowComplete;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x0009CD00 File Offset: 0x0009AF00
		internal AckStatus CompletionStatus
		{
			get
			{
				return this.shadowCompletionStatus;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x0009CD08 File Offset: 0x0009AF08
		internal List<ShadowServerResponseInfo> ShadowServerResponses
		{
			get
			{
				return this.shadowServerResponses;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x0600283A RID: 10298 RVA: 0x0009CD10 File Offset: 0x0009AF10
		protected bool HasPendingProxyCallback
		{
			get
			{
				return this.hasPendingProxyCallback;
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x0009CD18 File Offset: 0x0009AF18
		protected bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0009CD20 File Offset: 0x0009AF20
		public IAsyncResult BeginOpen(TransportMailItem transportMailItem, AsyncCallback asyncCallback, object state)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.BeginOpen");
			this.DropBreadcrumb(ShadowBreadcrumbs.Open);
			if (asyncCallback == null)
			{
				throw new ArgumentNullException("asyncCallback");
			}
			this.mailItem = transportMailItem;
			this.LoadNewProxyLayer();
			ShadowPeerNextHopConnection connection = new ShadowPeerNextHopConnection(this, this.proxyLayer, Components.RoutingComponent.MailRouter, this.mailItem);
			IEnumerable<INextHopServer> shadowHubs = null;
			this.negotiationTimer.Start();
			this.selectionTimer.Start();
			if (this.hubPicker.TrySelectShadowServers(out shadowHubs))
			{
				this.connectionHandler.HandleShadowConnection(connection, shadowHubs);
			}
			else
			{
				this.AckMessage(AckStatus.Fail, SmtpResponse.ShadowRedundancyFailed);
			}
			return new AsyncResult(asyncCallback, state, true);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x0009CDCC File Offset: 0x0009AFCC
		public bool EndOpen(IAsyncResult asyncResult)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.EndOpen");
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return true;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x0009CDF4 File Offset: 0x0009AFF4
		public IAsyncResult BeginWrite(byte[] buffer, int offset, int count, bool seenEod, AsyncCallback asyncCallback, object state)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<int, int>((long)this.GetHashCode(), "ShadowSession.BeginWrite Offset={0} Count={1}", offset, count);
			if (asyncCallback == null)
			{
				throw new ArgumentNullException("asyncCallback");
			}
			if (this.proxyLayer == null)
			{
				throw new InvalidOperationException("BeginWrite called without calling BeginOpen");
			}
			lock (this.syncObject)
			{
				if (!this.shadowComplete && !this.IsClosed)
				{
					this.WriteInternal(buffer, offset, count, seenEod);
				}
			}
			return new AsyncResult(asyncCallback, state, true);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0009CE90 File Offset: 0x0009B090
		public bool EndWrite(IAsyncResult asyncResult)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.EndWrite");
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return true;
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0009CEB8 File Offset: 0x0009B0B8
		public IAsyncResult BeginComplete(AsyncCallback asyncCallback, object state)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.BeginComplete");
			this.DropBreadcrumb(ShadowBreadcrumbs.Complete);
			if (asyncCallback == null)
			{
				throw new ArgumentNullException("asyncCallback");
			}
			if (this.proxyLayer == null)
			{
				throw new InvalidOperationException("BeginComplete called without calling BeginOpen");
			}
			lock (this.syncObject)
			{
				this.completeAsyncResult = new AsyncResult(asyncCallback, state);
				if (this.shadowComplete)
				{
					this.LogMessage(this.shadowCompletionStatus);
					this.completeAsyncResult.IsCompleted = true;
				}
			}
			return this.completeAsyncResult;
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x0009CF64 File Offset: 0x0009B164
		public bool EndComplete(IAsyncResult asyncResult)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.EndComplete");
			if (!this.shadowComplete)
			{
				throw new InvalidOperationException("EndComplete called before callback fired");
			}
			return this.shadowCompletionStatus == AckStatus.Success;
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0009CF98 File Offset: 0x0009B198
		public void Close(AckStatus ackStatus, SmtpResponse smtpResponse)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<AckStatus, SmtpResponse>((long)this.GetHashCode(), "ShadowSession.Close({0},{1})", ackStatus, smtpResponse);
			this.DropBreadcrumb(ShadowBreadcrumbs.Close);
			lock (this.syncObject)
			{
				if (!this.isClosed)
				{
					this.isClosed = true;
					this.AckConnection(ackStatus, smtpResponse);
				}
			}
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x0009D00C File Offset: 0x0009B20C
		public void NotifyProxyFailover(string shadowServer, SmtpResponse smtpResponse)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.NotifyProxyFailover");
			this.DropBreadcrumb(ShadowBreadcrumbs.ProxyFailover);
			this.NotifyShadowServerResponse(shadowServer, smtpResponse);
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x0009D034 File Offset: 0x0009B234
		public bool MailItemRequiresShadowCopy(TransportMailItem mailItem)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.MailItemRequiresShadowCopy");
			return this.shadowRedundancyManager.Configuration.RejectMessageOnShadowFailure;
		}

		// Token: 0x06002845 RID: 10309
		public abstract void PrepareForNewCommand(BaseDataSmtpCommand newCommand);

		// Token: 0x06002846 RID: 10310
		protected abstract void LoadNewProxyLayer();

		// Token: 0x06002847 RID: 10311
		protected abstract void WriteInternal(byte[] buffer, int offset, int count, bool seenEod);

		// Token: 0x06002848 RID: 10312 RVA: 0x0009D05C File Offset: 0x0009B25C
		protected virtual void WriteProxyDataComplete(SmtpResponse response)
		{
			this.DropBreadcrumb(ShadowBreadcrumbs.WriteProxyDataComplete);
			this.hasPendingProxyCallback = false;
			if (Interlocked.Increment(ref this.shadowServerSelected) == 1 && this.selectionTimer != null)
			{
				this.selectionTimer.Stop();
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0009D090 File Offset: 0x0009B290
		protected void WriteToProxy(byte[] buffer, int offset, int count, bool seenEod, bool copyBuffer)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession writing raw data to proxy layer: length={0} offset={1} count={2} eod={3}", new object[]
			{
				buffer.Length,
				offset,
				count,
				seenEod
			});
			lock (this.syncObject)
			{
				if (this.IsClosed)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession is closed, skipping write to proxy layer");
					this.DropBreadcrumb(ShadowBreadcrumbs.WriteAfterCloseSkipped);
				}
				else
				{
					this.hasPendingProxyCallback = true;
					this.DropBreadcrumb(ShadowBreadcrumbs.WriteToProxy);
					this.proxyLayer.BeginWriteData(buffer, offset, count, seenEod, this.writeCompleteCallback, copyBuffer);
				}
			}
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0009D160 File Offset: 0x0009B360
		protected void WriteToProxy(WriteRecord writeRecord)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<int, bool>((long)this.GetHashCode(), "ShadowSession writing queued record to proxy layer: count={0} eod={1}", writeRecord.WriteBuffer.Length, writeRecord.Eod);
			this.WriteToProxy(writeRecord.WriteBuffer, 0, writeRecord.WriteBuffer.Length, writeRecord.Eod, false);
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0009D1B0 File Offset: 0x0009B3B0
		public void NotifyShadowServerResponse(string shadowServer, SmtpResponse response)
		{
			if (response.SmtpResponseType != SmtpResponseType.Success)
			{
				ShadowRedundancyManager.PerfCounters.ShadowFailure(shadowServer);
			}
			lock (this.syncObject)
			{
				this.shadowServerResponses.Add(new ShadowServerResponseInfo(shadowServer, response));
			}
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0009D214 File Offset: 0x0009B414
		public void NotifyLocalMessageDiscarded(TransportMailItem mailItem)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.NotifyLocalMessageDiscarded");
			this.DropBreadcrumb(ShadowBreadcrumbs.LocalMessageDiscarded);
			SystemProbeHelper.ShadowRedundancyTracer.TraceFail(mailItem, 0L, "Message failed to be stored locally on primary server");
			mailItem.DropBreadcrumb(Breadcrumb.MailItemDelivered);
			this.shadowRedundancyManager.NotifyMailItemDelivered(mailItem);
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0009D268 File Offset: 0x0009B468
		public void NotifyMessageRejected(TransportMailItem mailItem)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.NotifyMessageRejected");
			this.DropBreadcrumb(ShadowBreadcrumbs.LocalMessageRejected);
			SystemProbeHelper.ShadowRedundancyTracer.TraceFail(mailItem, 0L, "Message failed to be shadowed and shadowing was required to accept the message");
			this.shadowRedundancyManager.EventLogger.LogMessageDeferredDueToShadowFailure();
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0009D2B8 File Offset: 0x0009B4B8
		public void NotifyMessageComplete(TransportMailItem mailItem)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.NotifyMessageComplete updating TMI state");
			this.DropBreadcrumb(ShadowBreadcrumbs.MessageShadowingComplete);
			SystemProbeHelper.ShadowRedundancyTracer.TracePass<AckStatus>(this.mailItem, 0L, "Message accepted by the primary hub server. Shadow status = {0}", this.shadowCompletionStatus);
			this.mailItem.ShadowServerDiscardId = this.mailItem.ShadowMessageId.ToString();
			this.mailItem.ShadowServerContext = this.ShadowServerContext;
			this.mailItem.CommitLazy();
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x0009D340 File Offset: 0x0009B540
		internal void AckMessage(AckStatus status, SmtpResponse smtpResponse)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<AckStatus, SmtpResponse>((long)this.GetHashCode(), "ShadowSession.AckMessage Status={0} Response={1}", status, smtpResponse);
			lock (this.syncObject)
			{
				this.shadowComplete = true;
				this.shadowCompletionStatus = status;
				this.DropBreadcrumb(ShadowBreadcrumbs.SessionAckMessage);
				if (this.completeAsyncResult != null && !this.completeAsyncResult.IsCompleted)
				{
					ExTraceGlobals.ShadowRedundancyTracer.TraceDebug((long)this.GetHashCode(), "ShadowSession.AckMessage completing callback");
					this.LogMessage(status);
					if (this.negotiationTimer != null)
					{
						long num = this.negotiationTimer.Stop();
						if (status == AckStatus.Success)
						{
							ShadowRedundancyManager.PerfCounters.ShadowSuccessfulNegotiationLatencyCounter().AddSample(num);
							SystemProbeHelper.ShadowRedundancyTracer.TracePass<long>(this.mailItem, 0L, "Shadowed successfully in {0} ticks.", num);
						}
						else
						{
							SystemProbeHelper.ShadowRedundancyTracer.TraceFail<long>(this.mailItem, 0L, "Failure to shadow took {0} ticks.", num);
						}
					}
					this.completeAsyncResult.IsCompleted = true;
				}
			}
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0009D448 File Offset: 0x0009B648
		private void AckConnection(AckStatus status, SmtpResponse smtpResponse)
		{
			ExTraceGlobals.ShadowRedundancyTracer.TraceDebug<AckStatus, SmtpResponse>((long)this.GetHashCode(), "ShadowSession.AckConnection Status={0} Response={1}", status, smtpResponse);
			switch (status)
			{
			case AckStatus.Pending:
				break;
			case AckStatus.Success:
				return;
			case AckStatus.Retry:
			case AckStatus.Fail:
				this.DropBreadcrumb(ShadowBreadcrumbs.SessionAckConnectionFailure);
				this.AckMessage(AckStatus.Fail, smtpResponse);
				this.proxyLayer.NotifySmtpInStopProxy();
				return;
			default:
				switch (status)
				{
				case AckStatus.Resubmit:
				case AckStatus.Skip:
					break;
				case AckStatus.Quarantine:
					return;
				default:
					return;
				}
				break;
			}
			throw new InvalidOperationException("Invalid status");
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x0009D4C1 File Offset: 0x0009B6C1
		internal void DropBreadcrumb(ShadowBreadcrumbs breadcrumb)
		{
			this.breadcrumbs.Drop(breadcrumb);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x0009D4D0 File Offset: 0x0009B6D0
		private void LogMessage(AckStatus status)
		{
			if (status == AckStatus.Success)
			{
				MessageTrackingLog.TrackHighAvailabilityRedirect(MessageTrackingSource.SMTP, this.mailItem, string.Join<ShadowServerResponseInfo>(";", this.shadowServerResponses));
				return;
			}
			MessageTrackingLog.TrackHighAvailabilityRedirectFail(MessageTrackingSource.SMTP, this.mailItem, (this.shadowServerResponses.Count > 0) ? string.Join<ShadowServerResponseInfo>(";", this.shadowServerResponses) : "No suitable shadow servers");
		}

		// Token: 0x0400144E RID: 5198
		protected InboundProxyLayer.CompletionCallback writeCompleteCallback;

		// Token: 0x0400144F RID: 5199
		protected InboundProxyLayer proxyLayer;

		// Token: 0x04001450 RID: 5200
		protected ISmtpInSession inSession;

		// Token: 0x04001451 RID: 5201
		protected object syncObject = new object();

		// Token: 0x04001452 RID: 5202
		private ISmtpOutConnectionHandler connectionHandler;

		// Token: 0x04001453 RID: 5203
		private ShadowHubPickerBase hubPicker;

		// Token: 0x04001454 RID: 5204
		private TransportMailItem mailItem;

		// Token: 0x04001455 RID: 5205
		private AsyncResult completeAsyncResult;

		// Token: 0x04001456 RID: 5206
		private bool shadowComplete;

		// Token: 0x04001457 RID: 5207
		private AckStatus shadowCompletionStatus;

		// Token: 0x04001458 RID: 5208
		private ITimerCounter negotiationTimer;

		// Token: 0x04001459 RID: 5209
		private ITimerCounter selectionTimer;

		// Token: 0x0400145A RID: 5210
		private int shadowServerSelected;

		// Token: 0x0400145B RID: 5211
		private ShadowRedundancyManager shadowRedundancyManager;

		// Token: 0x0400145C RID: 5212
		private string shadowServerContext;

		// Token: 0x0400145D RID: 5213
		private bool hasPendingProxyCallback;

		// Token: 0x0400145E RID: 5214
		private bool isClosed;

		// Token: 0x0400145F RID: 5215
		private List<ShadowServerResponseInfo> shadowServerResponses = new List<ShadowServerResponseInfo>();

		// Token: 0x04001460 RID: 5216
		private Breadcrumbs<ShadowBreadcrumbs> breadcrumbs = new Breadcrumbs<ShadowBreadcrumbs>(64);
	}
}
