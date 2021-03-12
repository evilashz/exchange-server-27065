using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Protocols.Smtp;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x02000365 RID: 869
	internal class CommitCoordinator
	{
		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x00093A44 File Offset: 0x00091C44
		internal IAsyncResult LocalCommitResult
		{
			get
			{
				return this.localCommitResult;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00093A4C File Offset: 0x00091C4C
		internal IAsyncResult ShadowCommitResult
		{
			get
			{
				return this.shadowCommitResult;
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x00093A54 File Offset: 0x00091C54
		public CommitCoordinator(ISmtpInMailItemStorage storage, IShadowSession shadowSession, ProcessTransportRole transportRole)
		{
			ArgumentValidator.ThrowIfNull("storage", storage);
			ArgumentValidator.ThrowIfNull("shadowSession", shadowSession);
			this.storage = storage;
			this.shadowSession = shadowSession;
			this.processTransportRole = transportRole;
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x00093A90 File Offset: 0x00091C90
		public Task<SmtpResponse> CommitMailItemAsync(TransportMailItem transportMailItem)
		{
			TaskCompletionSource<SmtpResponse> taskCompletionSource = new TaskCompletionSource<SmtpResponse>();
			this.BeginCommitMailItem(transportMailItem, new AsyncCallback(this.EndCommitMailItem), taskCompletionSource);
			return taskCompletionSource.Task;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x00093AC0 File Offset: 0x00091CC0
		public IAsyncResult BeginCommitMailItem(TransportMailItem transportMailItem, AsyncCallback asyncCallback, object state)
		{
			ArgumentValidator.ThrowIfNull("transportMailItem", transportMailItem);
			ArgumentValidator.ThrowIfNull("asyncCallback", asyncCallback);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "CommitCoordinator.BeginCommitMailItem");
			if (this.finalCommitResult != null || this.mailItem != null)
			{
				throw new InvalidOperationException("BeginCommitMailItem already called");
			}
			SystemProbeHelper.ShadowRedundancyTracer.TracePass(this.mailItem, (long)this.GetHashCode(), "Message received by hub server");
			this.mailItem = transportMailItem;
			this.finalCommitResult = new AsyncResult(asyncCallback, state);
			this.StartCommit();
			return this.finalCommitResult;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x00093B50 File Offset: 0x00091D50
		public bool EndCommitMailItem(IAsyncResult asyncResult, out SmtpResponse smtpResponse, out Exception commitException)
		{
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "CommitCoordinator.EndCommitMailItem");
			if (asyncResult != this.finalCommitResult)
			{
				throw new InvalidOperationException("Mismatched async results");
			}
			return this.CompleteCommit(out smtpResponse, out commitException);
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x00093B90 File Offset: 0x00091D90
		private void EndCommitMailItem(IAsyncResult asyncResult)
		{
			TaskCompletionSource<SmtpResponse> taskCompletionSource = (TaskCompletionSource<SmtpResponse>)asyncResult.AsyncState;
			try
			{
				SmtpResponse result;
				Exception ex;
				if (this.EndCommitMailItem(asyncResult, out result, out ex))
				{
					taskCompletionSource.SetResult(SmtpResponse.Empty);
				}
				else if (ex != null)
				{
					taskCompletionSource.SetException(ex);
				}
				else
				{
					taskCompletionSource.SetResult(result);
				}
			}
			catch (Exception exception)
			{
				taskCompletionSource.SetException(exception);
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00093BF4 File Offset: 0x00091DF4
		private void StartCommit()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "CommitCoordinator.StartCommit");
			LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceiveCommitLocal, this.mailItem.LatencyTracker);
			this.localCommitResult = this.storage.BeginCommitMailItem(this.mailItem, new AsyncCallback(this.LocalCommitCallback), this);
			LatencyTracker.BeginTrackLatency(LatencyComponent.SmtpReceiveCommitRemote, this.mailItem.LatencyTracker);
			this.shadowCommitResult = this.shadowSession.BeginComplete(new AsyncCallback(this.ShadowCommitCallback), this);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x00093C80 File Offset: 0x00091E80
		private bool CompleteCommit(out SmtpResponse smtpResponse, out Exception commitException)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "CommitCoordinator.CompleteCommit");
			smtpResponse = SmtpResponse.Empty;
			bool flag = this.storage.EndCommitMailItem(this.mailItem, this.localCommitResult, out commitException);
			bool flag2 = this.shadowSession.EndComplete(this.shadowCommitResult);
			if (!flag)
			{
				this.shadowSession.NotifyLocalMessageDiscarded(this.mailItem);
				return false;
			}
			if (this.processTransportRole == ProcessTransportRole.Hub && ShadowRedundancyManager.PerfCounters != null)
			{
				ShadowRedundancyManager.PerfCounters.TrackMessageMadeRedundant(flag2);
			}
			if (!flag2 && this.shadowSession.MailItemRequiresShadowCopy(this.mailItem))
			{
				this.shadowSession.NotifyMessageRejected(this.mailItem);
				smtpResponse = SmtpResponse.ShadowRedundancyFailed;
				commitException = null;
				return false;
			}
			this.shadowSession.NotifyMessageComplete(this.mailItem);
			return true;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00093D54 File Offset: 0x00091F54
		private void LocalCommitCallback(IAsyncResult asyncResult)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "CommitCoordinator.LocalCommitCallback");
			LatencyTracker.EndTrackLatency(LatencyComponent.SmtpReceiveCommitLocal, this.mailItem.LatencyTracker);
			if (Interlocked.Increment(ref this.localComplete) == 1)
			{
				this.localCommitResult = asyncResult;
				this.JoinAllAsyncCommits();
			}
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x00093DA8 File Offset: 0x00091FA8
		private void ShadowCommitCallback(IAsyncResult asyncResult)
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "CommitCoordinator.ShadowCommitCallback");
			LatencyTracker.EndTrackLatency(LatencyComponent.SmtpReceiveCommitRemote, this.mailItem.LatencyTracker);
			if (Interlocked.Increment(ref this.shadowComplete) == 1)
			{
				this.shadowCommitResult = asyncResult;
				this.JoinAllAsyncCommits();
			}
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x00093DFC File Offset: 0x00091FFC
		private void JoinAllAsyncCommits()
		{
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "CommitCoordinator.JoinAllAsyncCommits");
			if (Interlocked.Decrement(ref this.commitsRemaining) == 0)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)this.GetHashCode(), "CommitCoordinator triggering final callback");
				this.finalCommitResult.IsCompleted = true;
			}
		}

		// Token: 0x0400136B RID: 4971
		private ISmtpInMailItemStorage storage;

		// Token: 0x0400136C RID: 4972
		private IShadowSession shadowSession;

		// Token: 0x0400136D RID: 4973
		private TransportMailItem mailItem;

		// Token: 0x0400136E RID: 4974
		private AsyncResult finalCommitResult;

		// Token: 0x0400136F RID: 4975
		private IAsyncResult localCommitResult;

		// Token: 0x04001370 RID: 4976
		private IAsyncResult shadowCommitResult;

		// Token: 0x04001371 RID: 4977
		private int localComplete;

		// Token: 0x04001372 RID: 4978
		private int shadowComplete;

		// Token: 0x04001373 RID: 4979
		private int commitsRemaining = 2;

		// Token: 0x04001374 RID: 4980
		private readonly ProcessTransportRole processTransportRole;
	}
}
