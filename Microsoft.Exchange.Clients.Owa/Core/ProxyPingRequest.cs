using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000216 RID: 534
	internal class ProxyPingRequest : DisposeTrackableBase
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0006E21C File Offset: 0x0006C41C
		internal void BeginSend(OwaContext owaContext, AsyncCallback callback, object extraData)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug((long)this.GetHashCode(), "ProxyPingRequest.BeginSend");
			Uri uri = new UriBuilder(owaContext.SecondCasUri.Uri)
			{
				Path = OwaUrl.ProxyPing.GetExplicitUrl(owaContext)
			}.Uri;
			HttpWebRequest httpWebRequest = ProxyUtilities.CreateHttpWebRequestForProxying(owaContext, uri);
			httpWebRequest.Method = "GET";
			httpWebRequest.UserAgent = "OwaProxy";
			this.request = httpWebRequest;
			this.asyncResult = new OwaAsyncResult(callback, extraData);
			this.requestTimedOut = false;
			IAsyncResult asyncResult = ProxyUtilities.BeginGetResponse(this.request, new AsyncCallback(this.GetResponseCallback), this, out this.requestClock);
			this.timeoutWaitHandle = ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, new WaitOrTimerCallback(this.RequestTimeoutCallback), asyncResult, (long)owaContext.HttpContext.Server.ScriptTimeout * 1000L, true);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0006E2F8 File Offset: 0x0006C4F8
		internal HttpWebResponse EndSend(IAsyncResult asyncResult)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug((long)this.GetHashCode(), "ProxyPingRequest.EndSend");
			HttpWebResponse result;
			try
			{
				OwaAsyncResult owaAsyncResult = (OwaAsyncResult)asyncResult;
				if (owaAsyncResult.Exception != null)
				{
					throw new OwaAsyncOperationException("ProxyPingRequest async operation failed", owaAsyncResult.Exception);
				}
				HttpWebResponse httpWebResponse = this.response;
				this.response = null;
				result = httpWebResponse;
			}
			finally
			{
				this.Dispose();
			}
			return result;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x0006E368 File Offset: 0x0006C568
		private void GetResponseCallback(IAsyncResult asyncResult)
		{
			lock (this)
			{
				if (!this.requestTimedOut)
				{
					try
					{
						if (this.timeoutWaitHandle != null)
						{
							this.timeoutWaitHandle.Unregister(null);
							this.timeoutWaitHandle = null;
						}
						this.response = ProxyUtilities.EndGetResponse(this.request, asyncResult, this.requestClock);
					}
					catch (Exception exception)
					{
						this.asyncResult.CompleteRequest(false, exception);
						return;
					}
					this.asyncResult.CompleteRequest(false);
				}
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x0006E408 File Offset: 0x0006C608
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				if (this.response != null)
				{
					this.response.Close();
					this.response = null;
				}
				if (this.timeoutWaitHandle != null)
				{
					this.timeoutWaitHandle.Unregister(null);
					this.timeoutWaitHandle = null;
				}
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0006E443 File Offset: 0x0006C643
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProxyPingRequest>(this);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0006E44C File Offset: 0x0006C64C
		private void RequestTimeoutCallback(object state, bool timedOut)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug(0L, "ProxyEventHandler.RequestTimeoutCallback");
			if (!timedOut)
			{
				ExTraceGlobals.ProxyTracer.TraceDebug(0L, "Request is completed, aborting timeout");
				return;
			}
			lock (this)
			{
				if (this.asyncResult.IsCompleted)
				{
					ExTraceGlobals.ProxyTracer.TraceDebug(0L, "Request is completed, aborting timeout");
				}
				else
				{
					ExTraceGlobals.ProxyTracer.TraceDebug(0L, "Async request timed out");
					this.requestTimedOut = true;
					this.request.Abort();
					this.asyncResult.CompleteRequest(false, new OwaAsyncRequestTimeoutException("ProxyPingRequest request timeout", null));
				}
			}
		}

		// Token: 0x04000C50 RID: 3152
		private HttpWebRequest request;

		// Token: 0x04000C51 RID: 3153
		private HttpWebResponse response;

		// Token: 0x04000C52 RID: 3154
		private bool requestTimedOut;

		// Token: 0x04000C53 RID: 3155
		private RegisteredWaitHandle timeoutWaitHandle;

		// Token: 0x04000C54 RID: 3156
		private OwaAsyncResult asyncResult;

		// Token: 0x04000C55 RID: 3157
		private Stopwatch requestClock;
	}
}
