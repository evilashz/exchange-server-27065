using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000213 RID: 531
	internal class ProxyProtocolRequest : DisposeTrackableBase
	{
		// Token: 0x0600120B RID: 4619 RVA: 0x0006D974 File Offset: 0x0006BB74
		internal void BeginSend(OwaContext owaContext, HttpRequest originalRequest, string targetUrl, string proxyRequestBody, AsyncCallback callback, object extraData)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug((long)this.GetHashCode(), "ProxyProtocolRequest.BeginSend");
			Uri uri = new UriBuilder(owaContext.SecondCasUri.Uri)
			{
				Path = targetUrl
			}.Uri;
			HttpWebRequest proxyRequestInstance = ProxyUtilities.GetProxyRequestInstance(originalRequest, owaContext, uri);
			proxyRequestInstance.ContentLength = (long)((proxyRequestBody != null) ? Encoding.UTF8.GetByteCount(proxyRequestBody) : 0);
			proxyRequestInstance.Method = "POST";
			this.proxyRequest = proxyRequestInstance;
			this.proxyRequestBody = proxyRequestBody;
			this.owaContext = owaContext;
			this.asyncResult = new OwaAsyncResult(callback, extraData);
			proxyRequestInstance.BeginGetRequestStream(new AsyncCallback(this.GetRequestStreamCallback), this);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0006DA20 File Offset: 0x0006BC20
		internal HttpWebResponse EndSend(IAsyncResult asyncResult)
		{
			ExTraceGlobals.ProxyCallTracer.TraceDebug((long)this.GetHashCode(), "ProxyProtocolRequest.EndSend");
			HttpWebResponse result;
			try
			{
				OwaAsyncResult owaAsyncResult = (OwaAsyncResult)asyncResult;
				if (owaAsyncResult.Exception != null)
				{
					throw new OwaAsyncOperationException("ProxyProtocolRequest async operation failed", owaAsyncResult.Exception);
				}
				HttpWebResponse httpWebResponse = this.proxyResponse;
				this.proxyResponse = null;
				result = httpWebResponse;
			}
			finally
			{
				this.Dispose();
			}
			return result;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0006DA90 File Offset: 0x0006BC90
		private void GetRequestStreamCallback(IAsyncResult asyncResult)
		{
			try
			{
				this.proxyRequestStream = this.proxyRequest.EndGetRequestStream(asyncResult);
				this.proxyStreamCopy = new ProxyStreamCopy(this.proxyRequestBody, this.proxyRequestStream, StreamCopyMode.SyncReadAsyncWrite);
				this.proxyStreamCopy.Encoding = Encoding.UTF8;
				this.proxyStreamCopy.BeginCopy(new AsyncCallback(this.CopyRequestStreamCallback), this);
			}
			catch (Exception exception)
			{
				this.asyncResult.CompleteRequest(false, exception);
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0006DB14 File Offset: 0x0006BD14
		private void CopyRequestStreamCallback(IAsyncResult result)
		{
			try
			{
				this.proxyStreamCopy.EndCopy(result);
				this.requestTimedOut = false;
				IAsyncResult asyncResult = ProxyUtilities.BeginGetResponse(this.proxyRequest, new AsyncCallback(this.GetResponseCallback), this, out this.requestClock);
				this.timeoutWaitHandle = ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, new WaitOrTimerCallback(this.RequestTimeoutCallback), asyncResult, (long)this.owaContext.HttpContext.Server.ScriptTimeout * 1000L, true);
			}
			catch (Exception exception)
			{
				this.asyncResult.CompleteRequest(false, exception);
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0006DBB4 File Offset: 0x0006BDB4
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
						this.proxyResponse = ProxyUtilities.EndGetResponse(this.proxyRequest, asyncResult, this.requestClock);
						ProxyUtilities.UpdateProxyUserContextIdFromResponse(this.proxyResponse, this.owaContext.UserContext);
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

		// Token: 0x06001210 RID: 4624 RVA: 0x0006DC68 File Offset: 0x0006BE68
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				if (this.proxyResponse != null)
				{
					this.proxyResponse.Close();
					this.proxyResponse = null;
				}
				if (this.proxyResponseStream != null)
				{
					this.proxyResponseStream.Close();
					this.proxyResponseStream = null;
				}
				if (this.proxyRequestStream != null)
				{
					this.proxyRequestStream.Close();
					this.proxyRequestStream = null;
				}
				if (this.timeoutWaitHandle != null)
				{
					this.timeoutWaitHandle.Unregister(null);
					this.timeoutWaitHandle = null;
				}
			}
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0006DCE2 File Offset: 0x0006BEE2
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ProxyProtocolRequest>(this);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0006DCEC File Offset: 0x0006BEEC
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
					this.proxyRequest.Abort();
					this.asyncResult.CompleteRequest(false, new OwaAsyncRequestTimeoutException("ProxyProtocolRequest request timeout", null));
				}
			}
		}

		// Token: 0x04000C40 RID: 3136
		private HttpWebRequest proxyRequest;

		// Token: 0x04000C41 RID: 3137
		private HttpWebResponse proxyResponse;

		// Token: 0x04000C42 RID: 3138
		private Stream proxyRequestStream;

		// Token: 0x04000C43 RID: 3139
		private Stream proxyResponseStream;

		// Token: 0x04000C44 RID: 3140
		private string proxyRequestBody;

		// Token: 0x04000C45 RID: 3141
		private OwaAsyncResult asyncResult;

		// Token: 0x04000C46 RID: 3142
		private ProxyStreamCopy proxyStreamCopy;

		// Token: 0x04000C47 RID: 3143
		private Stopwatch requestClock;

		// Token: 0x04000C48 RID: 3144
		private OwaContext owaContext;

		// Token: 0x04000C49 RID: 3145
		private bool requestTimedOut;

		// Token: 0x04000C4A RID: 3146
		private RegisteredWaitHandle timeoutWaitHandle;
	}
}
