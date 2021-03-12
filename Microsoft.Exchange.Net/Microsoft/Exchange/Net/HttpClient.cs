using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;
using Microsoft.Exchange.Net.Logging;
using Microsoft.Exchange.Net.WebApplicationClient;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000733 RID: 1843
	internal class HttpClient : DisposeTrackableBase
	{
		// Token: 0x0600234E RID: 9038 RVA: 0x0004802C File Offset: 0x0004622C
		public HttpClient()
		{
			this.responseCallBack = this.WrapCallbackWithUnhandledExceptionRedirect(new WaitCallback(this.ResponseCallBack));
			this.requestCallBack = this.WrapCallbackWithUnhandledExceptionRedirect(new WaitCallback(this.RequestCallBack));
			this.readResponseCallBack = this.WrapCallbackWithUnhandledExceptionRedirect(new WaitCallback(this.ReadResponseCallBack));
			this.writeRequestCallBack = this.WrapCallbackWithUnhandledExceptionRedirect(new WaitCallback(this.WriteRequestCallBack));
			this.writeResponseCallBack = new AsyncCallback(this.WriteResponseCallBack);
			this.readRequestCallBack = new AsyncCallback(this.ReadRequestCallBack);
			this.breadcrumbs = new Breadcrumbs<HttpClient.Breadcrumbs>(64);
			this.timeoutTimer = new Timer(new TimerCallback(this.TimeoutCallback), null, -1, -1);
		}

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x0600234F RID: 9039 RVA: 0x000480F8 File Offset: 0x000462F8
		// (remove) Token: 0x06002350 RID: 9040 RVA: 0x00048130 File Offset: 0x00046330
		public event EventHandler<DownloadCompleteEventArgs> DownloadCompleted;

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x00048165 File Offset: 0x00046365
		public Uri LastKnownRequestedUri
		{
			get
			{
				base.CheckDisposed();
				return this.lastKnownRequestedUri;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x00048173 File Offset: 0x00046373
		internal bool CompletedSynchronously
		{
			get
			{
				base.CheckDisposed();
				return this.completedSynchronously;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x00048181 File Offset: 0x00046381
		private byte[] Buffer
		{
			get
			{
				if (this.buffer == null)
				{
					this.buffer = new byte[4096];
				}
				return this.buffer;
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000481A1 File Offset: 0x000463A1
		public ICancelableAsyncResult BeginDownload(Uri url, CancelableAsyncCallback requestCallback, object state)
		{
			return this.BeginDownload(url, new HttpSessionConfig(), requestCallback, state);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000481B1 File Offset: 0x000463B1
		public ICancelableAsyncResult BeginDownload(Uri url, int timeoutInterval, CancelableAsyncCallback requestCallback, object state)
		{
			return this.BeginDownload(url, new HttpSessionConfig(timeoutInterval), requestCallback, state);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000481C4 File Offset: 0x000463C4
		public ICancelableAsyncResult BeginDownload(Uri url, HttpSessionConfig sessionConfig, CancelableAsyncCallback requestCallback, object state)
		{
			base.CheckDisposed();
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterBeginDownload);
			this.InitializeState(sessionConfig);
			Exception exception = null;
			CancelableHttpAsyncResult cancelableHttpAsyncResult = new CancelableHttpAsyncResult(requestCallback, state, this);
			this.lastKnownRequestedUri = url;
			try
			{
				exception = this.BeginDownloadAction(url, cancelableHttpAsyncResult);
			}
			catch (WebException ex)
			{
				exception = ex;
			}
			catch (SecurityException ex2)
			{
				exception = ex2;
			}
			catch (IOException ex3)
			{
				exception = ex3;
			}
			catch (HttpWebRequestException ex4)
			{
				exception = ex4;
			}
			this.HandleException(cancelableHttpAsyncResult, exception);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveBeginDownload);
			return cancelableHttpAsyncResult;
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x0004826C File Offset: 0x0004646C
		public DownloadResult EndDownload(ICancelableAsyncResult asyncResult)
		{
			base.CheckDisposed();
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterEndDownload);
			CancelableHttpAsyncResult cancelableHttpAsyncResult = (CancelableHttpAsyncResult)asyncResult;
			HttpClient.SetEndCalled(cancelableHttpAsyncResult);
			if (!cancelableHttpAsyncResult.IsCompleted)
			{
				cancelableHttpAsyncResult.AsyncWaitHandle.WaitOne();
			}
			if (this.DownloadCompleted != null)
			{
				this.DownloadCompleted(this, new DownloadCompleteEventArgs(this.bytesReceived, this.bytesUploaded));
			}
			DownloadResult result = new DownloadResult(cancelableHttpAsyncResult.Exception);
			if (result.IsSucceeded || this.sessionConfig.ReadWebExceptionResponseStream)
			{
				if (this.responseStream != null)
				{
					this.responseStream.Seek(0L, SeekOrigin.Begin);
					result.ResponseStream = this.responseStream;
					this.responseStream = null;
				}
				result.LastModified = this.lastModified;
				result.ETag = this.eTag;
				result.BytesDownloaded = this.bytesReceived;
				result.ResponseUri = this.responseUri;
				result.StatusCode = this.statusCode;
				result.ResponseHeaders = this.responseHeaders;
				result.Cookies = this.cookies;
			}
			result.LastKnownRequestedUri = this.lastKnownRequestedUri;
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveEndDownload);
			this.LogDebug("Download IsSucceeded {0}", new object[]
			{
				result.IsSucceeded
			});
			return result;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x000483BC File Offset: 0x000465BC
		internal static string GetDisconnectReason(Exception exception)
		{
			if (exception == null)
			{
				return "Operation Completed";
			}
			return string.Format(CultureInfo.InvariantCulture, "Download Failure: {0},{1}", new object[]
			{
				exception.GetType(),
				exception.Message
			});
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000483FC File Offset: 0x000465FC
		protected override void InternalDispose(bool disposing)
		{
			lock (this.syncRoot)
			{
				if (disposing)
				{
					this.TryClose("Client Disposed");
					this.timeoutTimer.Dispose();
					this.timeoutTimer = null;
				}
			}
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x00048458 File Offset: 0x00046658
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<HttpClient>(this);
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x00048460 File Offset: 0x00046660
		protected virtual IAsyncResult BeginGetResponse(object state, AsyncCallback callback, params object[] args)
		{
			return this.httpWebRequest.BeginGetResponse(callback, state);
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0004846F File Offset: 0x0004666F
		protected virtual IAsyncResult BeginGetRequestStream(object state, AsyncCallback callback, params object[] args)
		{
			return this.httpWebRequest.BeginGetRequestStream(callback, state);
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00048480 File Offset: 0x00046680
		internal bool TryClose(string reason)
		{
			base.CheckDisposed();
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterTryClose);
			lock (this.syncRoot)
			{
				if (this.sessionClosing)
				{
					this.LogDebug("Session Close already initiated ...");
					return false;
				}
				this.sessionClosing = true;
				if (this.httpWebRequest != null)
				{
					this.httpWebRequest.Abort();
					this.httpWebRequest = null;
					this.LogDebug("HttpWebRequest is Aborted");
				}
				if (this.httpRequestStream != null)
				{
					try
					{
						this.httpRequestStream.Flush();
						this.httpRequestStream.Dispose();
						this.httpRequestStream = null;
						this.LogDebug("HttpRequestStream is Closed");
					}
					catch (WebException ex)
					{
						this.httpRequestStream = null;
						this.LogError("this.httpRequestStream.Dispose() hit exception: {0}", new object[]
						{
							ex
						});
					}
				}
				this.requestStream = null;
				if (this.httpResponseStream != null)
				{
					this.httpResponseStream.Flush();
					this.httpResponseStream.Dispose();
					this.httpResponseStream = null;
					this.LogDebug("HttpResponseStream is Closed");
				}
				if (!string.Equals(reason, "Operation Completed", StringComparison.OrdinalIgnoreCase) && this.responseStream != null)
				{
					this.responseStream.Flush();
					this.responseStream.Dispose();
					this.responseStream = null;
					this.LogDebug("ResponseStream is Closed");
				}
				this.UnRegisterTimeout();
				this.LogDisconnect(reason);
			}
			this.LogDebug("Session Closed Successfully");
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveTryClose);
			return true;
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x0004862C File Offset: 0x0004682C
		internal string GetBreadcrumbsSnapshot()
		{
			base.CheckDisposed();
			return this.breadcrumbs.ToString();
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0004863F File Offset: 0x0004683F
		private static void SetEndCalled(CancelableHttpAsyncResult asyncResult)
		{
			if (asyncResult.EndCalled)
			{
				throw new InvalidOperationException("The End function can only be called once for each asynchronous operation.");
			}
			asyncResult.EndCalled = true;
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x0004865C File Offset: 0x0004685C
		private static byte[] GetHeadersByteArray(WebHeaderCollection headerCollection)
		{
			if (headerCollection != null)
			{
				string s = headerCollection.ToString().Replace(Environment.NewLine, " ");
				return Encoding.ASCII.GetBytes(s);
			}
			return null;
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0004868F File Offset: 0x0004688F
		private static bool IsRedirect(HttpStatusCode statusCode)
		{
			return statusCode == HttpStatusCode.Found || statusCode == HttpStatusCode.MovedPermanently || statusCode == HttpStatusCode.MultipleChoices || statusCode == HttpStatusCode.TemporaryRedirect || statusCode == HttpStatusCode.SeeOther;
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x000486BC File Offset: 0x000468BC
		private void InitializeState(HttpSessionConfig sessionConfig)
		{
			this.urlRedirections = 0;
			this.bytesReceived = 0L;
			this.bytesUploaded = 0L;
			this.completedSynchronously = true;
			this.eTag = string.Empty;
			this.httpResponseStream = null;
			this.httpRequestStream = null;
			this.httpWebRequest = null;
			this.lastModified = null;
			this.responseStream = null;
			this.requestStream = null;
			this.sessionClosing = false;
			this.httpWebResponseContentLength = -1L;
			this.sessionConfig = sessionConfig;
			this.doesRequestContainsBody = (string.Equals(this.sessionConfig.Method, "POST", StringComparison.OrdinalIgnoreCase) || this.IsaMethodWithBody());
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00048760 File Offset: 0x00046960
		private bool IsaMethodWithBody()
		{
			foreach (string strA in HttpClient.VerbsWithBody)
			{
				if (string.Compare(strA, this.sessionConfig.Method, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000487C8 File Offset: 0x000469C8
		private Exception BeginDownloadAction(Uri url, CancelableHttpAsyncResult cancelableAsyncResult)
		{
			Exception result = null;
			lock (this.syncRoot)
			{
				if (cancelableAsyncResult.IsCompleted)
				{
					return null;
				}
				if (this.sessionClosing)
				{
					return null;
				}
				if (!url.IsAbsoluteUri || (url.Scheme != Uri.UriSchemeHttp && url.Scheme != Uri.UriSchemeHttps))
				{
					result = new UnsupportedUriFormatException(url.ToString());
				}
				else
				{
					this.LogDebug("Download Start for: {0}", new object[]
					{
						url.AbsoluteUri
					});
					this.httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
					this.InitializeHttpSessionProperties();
					this.InitializeProtocolLogSession(cancelableAsyncResult.GetHashCode(), this.sessionConfig.ProtocolLog);
					this.LogConnect(url.AbsoluteUri);
					this.LogSend(HttpClient.GetHeadersByteArray(this.httpWebRequest.Headers));
					if (this.doesRequestContainsBody && this.sessionConfig.RequestStream != null)
					{
						this.httpWebRequest.ContentType = this.sessionConfig.ContentType;
						this.httpWebRequest.ContentLength = this.sessionConfig.RequestStream.Length;
						this.requestStream = this.sessionConfig.RequestStream;
						this.BeginAsyncRequestWithTimeout(new HttpClient.AsyncRequest(this.BeginGetRequestStreamAction), cancelableAsyncResult, this.requestCallBack, new object[0]);
					}
					else
					{
						this.BeginAsyncRequestWithTimeout(new HttpClient.AsyncRequest(this.BeginGetResponseAction), cancelableAsyncResult, this.responseCallBack, new object[0]);
					}
				}
			}
			return result;
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x00048974 File Offset: 0x00046B74
		private void LogConnect(string context)
		{
			if (this.logSession != null)
			{
				this.logSession.LogConnect(context);
			}
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x0004898A File Offset: 0x00046B8A
		private void LogSend(byte[] data)
		{
			if (this.logSession != null)
			{
				this.logSession.LogSend(data);
			}
		}

		// Token: 0x06002367 RID: 9063 RVA: 0x000489A0 File Offset: 0x00046BA0
		private void LogReceive(byte[] data)
		{
			if (this.logSession != null)
			{
				this.logSession.LogReceive(data);
			}
		}

		// Token: 0x06002368 RID: 9064 RVA: 0x000489B6 File Offset: 0x00046BB6
		private void LogInformation(ProtocolLoggingLevel loggingLevel, string informationString)
		{
			if (this.logSession != null)
			{
				this.logSession.LogInformation(loggingLevel, null, informationString);
			}
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000489CE File Offset: 0x00046BCE
		private void LogInformation(string informationString)
		{
			if (this.logSession != null)
			{
				this.logSession.LogInformation(ProtocolLoggingLevel.All, null, informationString);
			}
		}

		// Token: 0x0600236A RID: 9066 RVA: 0x000489E8 File Offset: 0x00046BE8
		private void LogDebug(string formatString, params object[] args)
		{
			string informationString = string.Format(CultureInfo.InvariantCulture, formatString, args);
			this.LogDebug(informationString);
		}

		// Token: 0x0600236B RID: 9067 RVA: 0x00048A09 File Offset: 0x00046C09
		private void LogDebug(string informationString)
		{
			HttpClient.Tracer.TraceDebug((long)this.GetHashCode(), informationString);
			this.LogInformation(informationString);
		}

		// Token: 0x0600236C RID: 9068 RVA: 0x00048A24 File Offset: 0x00046C24
		private void LogError(string formatString, params object[] args)
		{
			string errorString = string.Format(CultureInfo.InvariantCulture, formatString, args);
			this.LogError(errorString);
		}

		// Token: 0x0600236D RID: 9069 RVA: 0x00048A45 File Offset: 0x00046C45
		private void LogError(string errorString)
		{
			HttpClient.Tracer.TraceError((long)this.GetHashCode(), errorString);
			this.LogInformation(errorString);
		}

		// Token: 0x0600236E RID: 9070 RVA: 0x00048A60 File Offset: 0x00046C60
		private void LogDisconnect(string reason)
		{
			if (this.logSession != null)
			{
				this.LogInformation(ProtocolLoggingLevel.All, "Bytes Downloaded: " + this.bytesReceived);
				this.logSession.LogDisconnect(reason);
			}
		}

		// Token: 0x0600236F RID: 9071 RVA: 0x00048A92 File Offset: 0x00046C92
		private void InitializeProtocolLogSession(int sessionId, ProtocolLog protocolLog)
		{
			if (protocolLog != null)
			{
				this.logSession = protocolLog.OpenSession((ulong)((long)sessionId), this.httpWebRequest.RequestUri.Host, HttpClient.LocalServerName);
			}
		}

		// Token: 0x06002370 RID: 9072 RVA: 0x00048ABA File Offset: 0x00046CBA
		private void InitializeResponseStream()
		{
			this.responseStream = TemporaryStorage.Create();
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00048AC8 File Offset: 0x00046CC8
		private void InitializeHttpSessionProperties()
		{
			this.httpWebRequest.AllowAutoRedirect = (!this.doesRequestContainsBody && this.sessionConfig.AllowAutoRedirect);
			this.httpWebRequest.AuthenticationLevel = this.sessionConfig.AuthenticationLevel;
			this.httpWebRequest.CachePolicy = this.sessionConfig.CachePolicy;
			this.httpWebRequest.Credentials = this.sessionConfig.Credentials;
			HttpWebRequest.DefaultMaximumErrorResponseLength = this.sessionConfig.DefaultMaximumErrorResponseLength;
			this.httpWebRequest.ImpersonationLevel = this.sessionConfig.ImpersonationLevel;
			this.httpWebRequest.KeepAlive = this.sessionConfig.KeepAlive;
			this.httpWebRequest.MaximumAutomaticRedirections = this.sessionConfig.MaximumAutomaticRedirections;
			this.httpWebRequest.MaximumResponseHeadersLength = this.sessionConfig.MaximumResponseHeadersLength;
			this.httpWebRequest.Method = this.sessionConfig.Method;
			this.httpWebRequest.Pipelined = this.sessionConfig.Pipelined;
			this.httpWebRequest.PreAuthenticate = this.sessionConfig.PreAuthenticate;
			this.httpWebRequest.ProtocolVersion = this.sessionConfig.ProtocolVersion;
			this.httpWebRequest.Proxy = this.sessionConfig.Proxy;
			this.httpWebRequest.UnsafeAuthenticatedConnectionSharing = this.sessionConfig.UnsafeAuthenticatedConnectionSharing;
			this.httpWebRequest.UserAgent = this.sessionConfig.UserAgent;
			if (!string.IsNullOrEmpty(this.sessionConfig.IfNoneMatch))
			{
				this.httpWebRequest.Headers.Add(HttpRequestHeader.IfNoneMatch, this.sessionConfig.IfNoneMatch);
			}
			if (this.sessionConfig.IfModifiedSince != null)
			{
				this.httpWebRequest.IfModifiedSince = this.sessionConfig.IfModifiedSince.Value;
			}
			if (!string.IsNullOrEmpty(this.sessionConfig.IfHeader))
			{
				this.httpWebRequest.Headers.Add("If", this.sessionConfig.IfHeader);
			}
			if (this.sessionConfig.Headers != null)
			{
				this.httpWebRequest.Headers.Add(this.sessionConfig.Headers);
			}
			if (this.sessionConfig.ClientCertificates != null)
			{
				this.httpWebRequest.ClientCertificates = this.sessionConfig.ClientCertificates;
			}
			if (this.sessionConfig.CookieContainer != null)
			{
				this.httpWebRequest.CookieContainer = this.sessionConfig.CookieContainer;
			}
			if (this.sessionConfig.Rows != null)
			{
				this.httpWebRequest.AddRange("rows", 0, this.sessionConfig.Rows.Value);
			}
			if (!string.IsNullOrEmpty(this.sessionConfig.Accept))
			{
				this.httpWebRequest.Accept = this.sessionConfig.Accept;
			}
			if (this.sessionConfig.Expect100Continue != null)
			{
				this.httpWebRequest.ServicePoint.Expect100Continue = this.sessionConfig.Expect100Continue.Value;
			}
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00048DD7 File Offset: 0x00046FD7
		private void ResponseCallBack(object asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterResponseCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.ResponseCallBackAction), (IAsyncResult)asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveResponseCallback);
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x00048E0C File Offset: 0x0004700C
		private Exception ResponseCallBackAction(IAsyncResult asyncResult)
		{
			Exception result = null;
			bool flag = false;
			bool flag2 = false;
			Uri url = null;
			CancelableHttpAsyncResult cancelableHttpAsyncResult = (CancelableHttpAsyncResult)asyncResult.AsyncState;
			lock (this.syncRoot)
			{
				if (cancelableHttpAsyncResult.IsCompleted)
				{
					return null;
				}
				if (this.sessionClosing)
				{
					return null;
				}
				this.UnRegisterTimeout();
				HttpWebResponse httpWebResponse = null;
				try
				{
					httpWebResponse = (HttpWebResponse)this.httpWebRequest.EndGetResponse(asyncResult);
				}
				catch (WebException ex)
				{
					if (!this.sessionConfig.ReadWebExceptionResponseStream || ex.Response == null)
					{
						throw;
					}
					httpWebResponse = (HttpWebResponse)ex.Response;
					cancelableHttpAsyncResult.Exception = ex;
				}
				this.LogDebug("Response Uri: {0}", new object[]
				{
					httpWebResponse.ResponseUri
				});
				this.LogReceive(HttpClient.GetHeadersByteArray(httpWebResponse.Headers));
				EventHandler<HttpWebResponseEventArgs> responseReceived = this.ResponseReceived;
				if (responseReceived != null)
				{
					responseReceived(this, new HttpWebResponseEventArgs(this.httpWebRequest, httpWebResponse));
				}
				if (this.sessionConfig.AllowAutoRedirect && this.doesRequestContainsBody && HttpClient.IsRedirect(httpWebResponse.StatusCode))
				{
					this.LogDebug("Redirected to: {0}", new object[]
					{
						httpWebResponse.Headers[HttpResponseHeader.Location]
					});
					if (this.urlRedirections < this.sessionConfig.MaximumAutomaticRedirections)
					{
						this.sessionConfig.RequestStream.Position = 0L;
						string text = httpWebResponse.Headers[HttpResponseHeader.Location] + httpWebResponse.ResponseUri.Query;
						try
						{
							url = new Uri(text);
							this.urlRedirections++;
							this.lastKnownRequestedUri = url;
							flag2 = true;
							goto IL_2F4;
						}
						catch (UriFormatException innerException)
						{
							result = new BadRedirectedUriException(text, innerException);
							goto IL_2F4;
						}
					}
					result = new WebException("Too many automatic redirections were attempted.", null, WebExceptionStatus.ProtocolError, httpWebResponse);
				}
				else
				{
					this.lastKnownRequestedUri = httpWebResponse.ResponseUri;
					if (this.sessionConfig.MaximumResponseBodyLength != -1L && this.sessionConfig.MaximumResponseBodyLength < httpWebResponse.ContentLength)
					{
						result = new DownloadLimitExceededException(string.Format(CultureInfo.InvariantCulture, "{0} Bytes / {1} Bytes", new object[]
						{
							httpWebResponse.ContentLength,
							this.sessionConfig.MaximumResponseBodyLength
						}));
					}
					else
					{
						this.responseUri = httpWebResponse.ResponseUri;
						this.statusCode = httpWebResponse.StatusCode;
						this.responseHeaders = httpWebResponse.Headers;
						this.cookies = httpWebResponse.Cookies;
						this.httpWebResponseContentLength = httpWebResponse.ContentLength;
						string s = httpWebResponse.Headers[HttpResponseHeader.LastModified];
						DateTime value;
						if (DateTime.TryParse(s, out value))
						{
							this.lastModified = new DateTime?(value);
						}
						string text2 = httpWebResponse.Headers[HttpResponseHeader.ETag];
						if (!string.IsNullOrEmpty(text2) && text2.Length <= this.sessionConfig.MaxETagLength)
						{
							this.eTag = text2;
						}
						this.httpResponseStream = httpWebResponse.GetResponseStream();
						flag = true;
					}
				}
				IL_2F4:;
			}
			if (flag2)
			{
				result = this.BeginDownloadAction(url, cancelableHttpAsyncResult);
			}
			else if (flag)
			{
				this.BeginAsyncRequestWithTimeout(new HttpClient.AsyncRequest(this.BeginRead), cancelableHttpAsyncResult, this.readResponseCallBack, new object[]
				{
					this.httpResponseStream
				});
			}
			return result;
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x000491AC File Offset: 0x000473AC
		private void RequestCallBack(object asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterRequestCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.RequestCallBackAction), (IAsyncResult)asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveRequestCallback);
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x000491E0 File Offset: 0x000473E0
		private Exception RequestCallBackAction(IAsyncResult asyncResult)
		{
			CancelableHttpAsyncResult cancelableHttpAsyncResult = (CancelableHttpAsyncResult)asyncResult.AsyncState;
			lock (this.syncRoot)
			{
				if (cancelableHttpAsyncResult.IsCompleted)
				{
					return null;
				}
				if (this.sessionClosing)
				{
					return null;
				}
				this.UnRegisterTimeout();
				this.httpRequestStream = this.httpWebRequest.EndGetRequestStream(asyncResult);
			}
			this.BeginAsyncRequestWithTimeout(new HttpClient.AsyncRequest(this.BeginRead), cancelableHttpAsyncResult, this.readRequestCallBack, new object[]
			{
				this.requestStream
			});
			return null;
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x00049288 File Offset: 0x00047488
		private void ReadResponseCallBack(object asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterReadCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.ReadResponseCallBackAction), (IAsyncResult)asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveReadCallback);
		}

		// Token: 0x06002377 RID: 9079 RVA: 0x000492BC File Offset: 0x000474BC
		private Exception ReadResponseCallBackAction(IAsyncResult asyncResult)
		{
			CancelableHttpAsyncResult cancelableHttpAsyncResult = (CancelableHttpAsyncResult)asyncResult.AsyncState;
			Exception result = null;
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			lock (this.syncRoot)
			{
				if (cancelableHttpAsyncResult.IsCompleted)
				{
					return null;
				}
				if (this.sessionClosing)
				{
					return null;
				}
				this.UnRegisterTimeout();
				num = this.httpResponseStream.EndRead(asyncResult);
				if (num > 0)
				{
					this.bytesReceived += (long)num;
					if (this.sessionConfig.MaximumResponseBodyLength != -1L && this.sessionConfig.MaximumResponseBodyLength < this.bytesReceived)
					{
						result = new DownloadLimitExceededException(string.Format(CultureInfo.InvariantCulture, "{0} Bytes / {1} Bytes", new object[]
						{
							this.bytesReceived,
							this.sessionConfig.MaximumResponseBodyLength
						}));
					}
					else if (this.httpWebResponseContentLength >= 0L && this.httpWebResponseContentLength < this.bytesReceived)
					{
						result = new ServerProtocolViolationException(string.Format(CultureInfo.InvariantCulture, "{0} Bytes", new object[]
						{
							this.httpWebResponseContentLength
						}));
					}
					else
					{
						if (this.responseStream == null)
						{
							this.InitializeResponseStream();
						}
						flag2 = true;
					}
				}
				else
				{
					flag = this.TryClose("Operation Completed");
				}
			}
			if (flag2)
			{
				this.BeginAsyncRequestWithTimeout(new HttpClient.AsyncRequest(this.BeginWrite), cancelableHttpAsyncResult, this.writeResponseCallBack, new object[]
				{
					this.responseStream,
					num
				});
			}
			else if (flag)
			{
				this.LogDebug("Complete contents Downloaded");
				cancelableHttpAsyncResult.InvokeCompleted();
			}
			return result;
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00049488 File Offset: 0x00047688
		private void ReadRequestCallBack(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterReadCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.ReadRequestCallBackAction), asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveReadCallback);
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x000494B8 File Offset: 0x000476B8
		private Exception ReadRequestCallBackAction(IAsyncResult asyncResult)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			CancelableHttpAsyncResult cancelableHttpAsyncResult = (CancelableHttpAsyncResult)asyncResult.AsyncState;
			lock (this.syncRoot)
			{
				if (cancelableHttpAsyncResult.IsCompleted)
				{
					return null;
				}
				if (this.sessionClosing)
				{
					return null;
				}
				this.UnRegisterTimeout();
				this.bytesUploaded = this.requestStream.Position;
				num = this.requestStream.EndRead(asyncResult);
				if (num > 0)
				{
					flag2 = true;
				}
				else
				{
					this.httpRequestStream.Flush();
					this.httpRequestStream.Dispose();
					this.httpRequestStream = null;
					flag = true;
				}
			}
			if (flag2)
			{
				this.BeginAsyncRequestWithTimeout(new HttpClient.AsyncRequest(this.BeginWrite), cancelableHttpAsyncResult, this.writeRequestCallBack, new object[]
				{
					this.httpRequestStream,
					num
				});
			}
			else if (flag)
			{
				this.LogDebug("Complete request written");
				this.BeginAsyncRequestWithTimeout(new HttpClient.AsyncRequest(this.BeginGetResponseAction), cancelableHttpAsyncResult, this.responseCallBack, new object[0]);
			}
			return null;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x000495E0 File Offset: 0x000477E0
		private void WriteResponseCallBack(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterWriteCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.WriteResponseCallBackAction), asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveWriteCallback);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x0004960E File Offset: 0x0004780E
		private Exception WriteResponseCallBackAction(IAsyncResult asyncResult)
		{
			return this.WriteCallBackAction(asyncResult, this.responseStream, this.httpResponseStream, this.readResponseCallBack);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00049629 File Offset: 0x00047829
		private void WriteRequestCallBack(object asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterWriteCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.WriteRequestCallBackAction), (IAsyncResult)asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveWriteCallback);
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x000496AC File Offset: 0x000478AC
		private AsyncCallback WrapCallbackWithUnhandledExceptionRedirect(WaitCallback callback)
		{
			return delegate(IAsyncResult asyncResult)
			{
				try
				{
					callback(asyncResult);
				}
				catch (Exception exception)
				{
					this.breadcrumbs.Drop(HttpClient.Breadcrumbs.CrashOnAnotherThread);
					ExWatson.SendReportAndCrashOnAnotherThread(exception);
				}
			};
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x000496D9 File Offset: 0x000478D9
		private Exception WriteRequestCallBackAction(IAsyncResult asyncResult)
		{
			return this.WriteCallBackAction(asyncResult, this.httpRequestStream, this.requestStream, this.readRequestCallBack);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x000496F4 File Offset: 0x000478F4
		private Exception WriteCallBackAction(IAsyncResult asyncResult, Stream writeStream, Stream readStream, AsyncCallback readCallBack)
		{
			CancelableHttpAsyncResult cancelableHttpAsyncResult = (CancelableHttpAsyncResult)asyncResult.AsyncState;
			lock (this.syncRoot)
			{
				if (cancelableHttpAsyncResult.IsCompleted)
				{
					return null;
				}
				if (this.sessionClosing)
				{
					return null;
				}
				this.UnRegisterTimeout();
				writeStream.EndWrite(asyncResult);
			}
			this.BeginAsyncRequestWithTimeout(new HttpClient.AsyncRequest(this.BeginRead), cancelableHttpAsyncResult, readCallBack, new object[]
			{
				readStream
			});
			return null;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x00049788 File Offset: 0x00047988
		private void AsyncCallBack(HttpClient.AsyncCallbackAction callbackAction, IAsyncResult asyncResult)
		{
			Exception exception = null;
			try
			{
				exception = callbackAction(asyncResult);
			}
			catch (WebException ex)
			{
				exception = ex;
			}
			catch (IOException ex2)
			{
				exception = ex2;
			}
			catch (SecurityException ex3)
			{
				exception = ex3;
			}
			catch (HttpWebRequestException ex4)
			{
				exception = ex4;
			}
			this.HandleException((CancelableHttpAsyncResult)asyncResult.AsyncState, exception);
		}

		// Token: 0x06002381 RID: 9089 RVA: 0x00049800 File Offset: 0x00047A00
		private void TimeoutCallback(object ignored)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterTimeoutCallback);
			IAsyncResult asyncResult = null;
			bool flag = false;
			Exception ex = new DownloadTimeoutException();
			lock (this.syncRoot)
			{
				asyncResult = this.pendingAsyncResult;
				if (asyncResult == null || asyncResult.IsCompleted)
				{
					return;
				}
				CancelableHttpAsyncResult cancelableHttpAsyncResult = (CancelableHttpAsyncResult)asyncResult.AsyncState;
				if (cancelableHttpAsyncResult.IsCompleted)
				{
					return;
				}
				if (this.sessionClosing)
				{
					return;
				}
				DateTime utcNow = DateTime.UtcNow;
				if (utcNow.Ticks < this.requestTimeout.Ticks)
				{
					this.LogDebug("This request is not supposed to be timed-out yet. UtcNow:{0}, RequestTimeout: {1}.", new object[]
					{
						utcNow.Ticks,
						this.requestTimeout.Ticks
					});
					return;
				}
				flag = this.TryClose(HttpClient.GetDisconnectReason(ex));
			}
			if (flag)
			{
				this.LogDebug("Timeout Occured");
				CancelableHttpAsyncResult cancelableHttpAsyncResult2 = (CancelableHttpAsyncResult)asyncResult.AsyncState;
				cancelableHttpAsyncResult2.InvokeCompleted(ex);
			}
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveTimeoutCallback);
		}

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06002382 RID: 9090 RVA: 0x00049924 File Offset: 0x00047B24
		// (remove) Token: 0x06002383 RID: 9091 RVA: 0x0004995C File Offset: 0x00047B5C
		public event EventHandler<HttpWebRequestEventArgs> SendingRequest;

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x06002384 RID: 9092 RVA: 0x00049994 File Offset: 0x00047B94
		// (remove) Token: 0x06002385 RID: 9093 RVA: 0x000499CC File Offset: 0x00047BCC
		public event EventHandler<HttpWebResponseEventArgs> ResponseReceived;

		// Token: 0x06002386 RID: 9094 RVA: 0x00049A04 File Offset: 0x00047C04
		private IAsyncResult IssueHttpWebRequest(HttpClient.AsyncRequest asyncRequest, object state, AsyncCallback callback, params object[] args)
		{
			IAsyncResult result = null;
			Exception ex = null;
			CancelableHttpAsyncResult state2 = state as CancelableHttpAsyncResult;
			bool flag = true;
			int num = 0;
			while (flag && num < 2)
			{
				if (num > 0)
				{
					Thread.Sleep(100);
				}
				flag = false;
				ex = null;
				try
				{
					result = asyncRequest(state2, callback, args);
				}
				catch (InvalidOperationException ex2)
				{
					ex = ex2;
					flag = true;
				}
				num++;
			}
			if (ex != null)
			{
				throw new HttpWebRequestException(ex);
			}
			return result;
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00049A74 File Offset: 0x00047C74
		private IAsyncResult BeginGetRequestStreamAction(object state, AsyncCallback callback, params object[] args)
		{
			EventHandler<HttpWebRequestEventArgs> sendingRequest = this.SendingRequest;
			if (sendingRequest != null)
			{
				sendingRequest(this, new HttpWebRequestEventArgs(this.httpWebRequest));
			}
			return this.IssueHttpWebRequest(new HttpClient.AsyncRequest(this.BeginGetRequestStream), state, callback, args);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00049AB3 File Offset: 0x00047CB3
		private IAsyncResult BeginGetResponseAction(object state, AsyncCallback callback, params object[] args)
		{
			return this.IssueHttpWebRequest(new HttpClient.AsyncRequest(this.BeginGetResponse), state, callback, args);
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00049ACC File Offset: 0x00047CCC
		private IAsyncResult BeginRead(object state, AsyncCallback callback, params object[] args)
		{
			Stream stream = (Stream)args[0];
			return stream.BeginRead(this.Buffer, 0, 4096, callback, state);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00049AF8 File Offset: 0x00047CF8
		private IAsyncResult BeginWrite(object state, AsyncCallback callback, params object[] args)
		{
			Stream stream = (Stream)args[0];
			int count = (int)args[1];
			return stream.BeginWrite(this.Buffer, 0, count, callback, state);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x00049B28 File Offset: 0x00047D28
		private void BeginAsyncRequestWithTimeout(HttpClient.AsyncRequest asyncRequest, CancelableHttpAsyncResult cancelableHttpAsyncResult, AsyncCallback callback, params object[] args)
		{
			lock (this.syncRoot)
			{
				if (!cancelableHttpAsyncResult.IsCompleted)
				{
					if (!this.sessionClosing)
					{
						IAsyncResult asyncResult = asyncRequest(cancelableHttpAsyncResult, callback, args);
						if (!asyncResult.CompletedSynchronously)
						{
							this.completedSynchronously = false;
						}
						this.RegisterTimeout(asyncResult);
					}
				}
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00049B98 File Offset: 0x00047D98
		private void UnRegisterTimeout()
		{
			lock (this.syncRoot)
			{
				this.timeoutTimer.Change(-1, -1);
				this.pendingAsyncResult = null;
				this.LogDebug("Timeout Un-Registered");
			}
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x00049BF4 File Offset: 0x00047DF4
		private void RegisterTimeout(IAsyncResult asyncResult)
		{
			lock (this.syncRoot)
			{
				if (!this.sessionClosing)
				{
					if (!asyncResult.IsCompleted)
					{
						this.requestTimeout = DateTime.UtcNow.AddMilliseconds((double)(this.sessionConfig.Timeout - 1000));
						this.timeoutTimer.Change(this.sessionConfig.Timeout, -1);
						this.pendingAsyncResult = asyncResult;
						this.LogDebug("Timeout Registered");
					}
				}
			}
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x00049C94 File Offset: 0x00047E94
		private void HandleException(CancelableHttpAsyncResult cancelableAsyncResult, Exception exception)
		{
			if (exception != null)
			{
				string disconnectReason = HttpClient.GetDisconnectReason(exception);
				if (this.TryClose(disconnectReason))
				{
					this.LogError("Download Failed with Exception: {0}", new object[]
					{
						exception
					});
					cancelableAsyncResult.InvokeCompleted(exception);
				}
			}
		}

		// Token: 0x04002150 RID: 8528
		private const string Seperator = " ";

		// Token: 0x04002151 RID: 8529
		private const string OperationCompleted = "Operation Completed";

		// Token: 0x04002152 RID: 8530
		private const string ClientDisposed = "Client Disposed";

		// Token: 0x04002153 RID: 8531
		private const string IfHeader = "If";

		// Token: 0x04002154 RID: 8532
		private const int BufferSize = 4096;

		// Token: 0x04002155 RID: 8533
		public const string BPropPatchMethod = "BPROPPATCH";

		// Token: 0x04002156 RID: 8534
		public const string DeleteMethod = "DELETE";

		// Token: 0x04002157 RID: 8535
		public const string LockMethod = "LOCK";

		// Token: 0x04002158 RID: 8536
		public const string MoveMethod = "MOVE";

		// Token: 0x04002159 RID: 8537
		public const string PropFindMethod = "PROPFIND";

		// Token: 0x0400215A RID: 8538
		public const string PropPatchMethod = "PROPPATCH";

		// Token: 0x0400215B RID: 8539
		public const string SearchMethod = "SEARCH";

		// Token: 0x0400215C RID: 8540
		public const string UnLockMethod = "UNLOCK";

		// Token: 0x0400215D RID: 8541
		public const string MergeMethod = "MERGE";

		// Token: 0x0400215E RID: 8542
		private const int NumberOfBreadcrumbs = 64;

		// Token: 0x0400215F RID: 8543
		private readonly AsyncCallback responseCallBack;

		// Token: 0x04002160 RID: 8544
		private readonly AsyncCallback requestCallBack;

		// Token: 0x04002161 RID: 8545
		private readonly AsyncCallback readResponseCallBack;

		// Token: 0x04002162 RID: 8546
		private readonly AsyncCallback writeResponseCallBack;

		// Token: 0x04002163 RID: 8547
		private readonly AsyncCallback readRequestCallBack;

		// Token: 0x04002164 RID: 8548
		private readonly AsyncCallback writeRequestCallBack;

		// Token: 0x04002165 RID: 8549
		private static readonly Trace Tracer = ExTraceGlobals.HttpClientTracer;

		// Token: 0x04002166 RID: 8550
		private static readonly string LocalServerName = ComputerInformation.DnsFullyQualifiedDomainName;

		// Token: 0x04002167 RID: 8551
		private object syncRoot = new object();

		// Token: 0x04002168 RID: 8552
		private bool sessionClosing;

		// Token: 0x04002169 RID: 8553
		private bool completedSynchronously;

		// Token: 0x0400216A RID: 8554
		private HttpWebRequest httpWebRequest;

		// Token: 0x0400216B RID: 8555
		private Stream httpResponseStream;

		// Token: 0x0400216C RID: 8556
		private Stream responseStream;

		// Token: 0x0400216D RID: 8557
		private Stream httpRequestStream;

		// Token: 0x0400216E RID: 8558
		private Stream requestStream;

		// Token: 0x0400216F RID: 8559
		private byte[] buffer;

		// Token: 0x04002170 RID: 8560
		private Breadcrumbs<HttpClient.Breadcrumbs> breadcrumbs;

		// Token: 0x04002171 RID: 8561
		private long httpWebResponseContentLength;

		// Token: 0x04002172 RID: 8562
		private DateTime? lastModified;

		// Token: 0x04002173 RID: 8563
		private string eTag;

		// Token: 0x04002174 RID: 8564
		private long bytesReceived;

		// Token: 0x04002175 RID: 8565
		private long bytesUploaded;

		// Token: 0x04002176 RID: 8566
		private ProtocolLogSession logSession;

		// Token: 0x04002177 RID: 8567
		private Timer timeoutTimer;

		// Token: 0x04002178 RID: 8568
		private IAsyncResult pendingAsyncResult;

		// Token: 0x04002179 RID: 8569
		private DateTime requestTimeout;

		// Token: 0x0400217A RID: 8570
		private Uri responseUri;

		// Token: 0x0400217B RID: 8571
		private Uri lastKnownRequestedUri;

		// Token: 0x0400217C RID: 8572
		private HttpStatusCode statusCode;

		// Token: 0x0400217D RID: 8573
		private WebHeaderCollection responseHeaders;

		// Token: 0x0400217E RID: 8574
		private CookieCollection cookies;

		// Token: 0x0400217F RID: 8575
		private HttpSessionConfig sessionConfig;

		// Token: 0x04002180 RID: 8576
		private int urlRedirections;

		// Token: 0x04002181 RID: 8577
		private bool doesRequestContainsBody;

		// Token: 0x04002182 RID: 8578
		private static List<string> VerbsWithBody = new List<string>
		{
			"BPROPPATCH",
			"LOCK",
			"MKCOL",
			"POST",
			"PROPFIND",
			"PROPPATCH",
			"PUT",
			"SEARCH",
			"UNLOCK",
			"MERGE"
		};

		// Token: 0x02000734 RID: 1844
		// (Invoke) Token: 0x06002391 RID: 9105
		private delegate IAsyncResult AsyncRequest(object state, AsyncCallback callback, params object[] args);

		// Token: 0x02000735 RID: 1845
		// (Invoke) Token: 0x06002395 RID: 9109
		private delegate Exception AsyncCallbackAction(IAsyncResult asyncResult);

		// Token: 0x02000736 RID: 1846
		private enum Breadcrumbs
		{
			// Token: 0x04002187 RID: 8583
			EnterBeginDownload = 1,
			// Token: 0x04002188 RID: 8584
			EnterEndDownload,
			// Token: 0x04002189 RID: 8585
			EnterResponseCallback,
			// Token: 0x0400218A RID: 8586
			EnterReadCallback,
			// Token: 0x0400218B RID: 8587
			EnterWriteCallback,
			// Token: 0x0400218C RID: 8588
			EnterTimeoutCallback,
			// Token: 0x0400218D RID: 8589
			EnterTryClose,
			// Token: 0x0400218E RID: 8590
			EnterRequestCallback,
			// Token: 0x0400218F RID: 8591
			LeaveBeginDownload = 10,
			// Token: 0x04002190 RID: 8592
			LeaveEndDownload,
			// Token: 0x04002191 RID: 8593
			LeaveResponseCallback,
			// Token: 0x04002192 RID: 8594
			LeaveReadCallback,
			// Token: 0x04002193 RID: 8595
			LeaveWriteCallback,
			// Token: 0x04002194 RID: 8596
			LeaveTimeoutCallback,
			// Token: 0x04002195 RID: 8597
			LeaveTryClose,
			// Token: 0x04002196 RID: 8598
			LeaveRequestCallback,
			// Token: 0x04002197 RID: 8599
			CrashOnAnotherThread = 20
		}
	}
}
