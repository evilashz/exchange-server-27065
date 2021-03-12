using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x0200003B RID: 59
	internal class HttpClient : IDisposable
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public HttpClient(ExecutionLog protocolLog = null)
		{
			this.responseCallBack = this.WrapCallbackWithUnhandledExceptionRedirect(new WaitCallback(this.ResponseCallBack));
			this.requestCallBack = this.WrapCallbackWithUnhandledExceptionRedirect(new WaitCallback(this.RequestCallBack));
			this.readResponseCallBack = this.WrapCallbackWithUnhandledExceptionRedirect(new WaitCallback(this.ReadResponseCallBack));
			this.writeRequestCallBack = this.WrapCallbackWithUnhandledExceptionRedirect(new WaitCallback(this.WriteRequestCallBack));
			this.writeResponseCallBack = new AsyncCallback(this.WriteResponseCallBack);
			this.readRequestCallBack = new AsyncCallback(this.ReadRequestCallBack);
			this.breadcrumbs = new Breadcrumbs<HttpClient.Breadcrumbs>(64);
			this.timeoutTimer = new Timer(new TimerCallback(this.TimeoutCallback), null, -1, -1);
			this.protocolLog = protocolLog;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003C94 File Offset: 0x00001E94
		~HttpClient()
		{
			this.Dispose(false);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000F5 RID: 245 RVA: 0x00003CC4 File Offset: 0x00001EC4
		// (remove) Token: 0x060000F6 RID: 246 RVA: 0x00003CFC File Offset: 0x00001EFC
		public event EventHandler<HttpWebRequestEventArgs> SendingRequest;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000F7 RID: 247 RVA: 0x00003D34 File Offset: 0x00001F34
		// (remove) Token: 0x060000F8 RID: 248 RVA: 0x00003D6C File Offset: 0x00001F6C
		public event EventHandler<HttpWebResponseEventArgs> ResponseReceived;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000F9 RID: 249 RVA: 0x00003DA4 File Offset: 0x00001FA4
		// (remove) Token: 0x060000FA RID: 250 RVA: 0x00003DDC File Offset: 0x00001FDC
		public event EventHandler<DownloadCompleteEventArgs> DownloadCompleted;

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003E11 File Offset: 0x00002011
		public Uri LastKnownRequestedUri
		{
			get
			{
				this.CheckDisposed();
				return this.lastKnownRequestedUri;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003E1F File Offset: 0x0000201F
		internal bool CompletedSynchronously
		{
			get
			{
				this.CheckDisposed();
				return this.completedSynchronously;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003E2D File Offset: 0x0000202D
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

		// Token: 0x060000FE RID: 254 RVA: 0x00003E4D File Offset: 0x0000204D
		public ICancelableAsyncResult BeginDownload(Uri url, AsyncCallback requestCallback, object state)
		{
			return this.BeginDownload(url, new HttpSessionConfig(), requestCallback, state);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003E5D File Offset: 0x0000205D
		public ICancelableAsyncResult BeginDownload(Uri url, int timeoutInterval, AsyncCallback requestCallback, object state)
		{
			return this.BeginDownload(url, new HttpSessionConfig(timeoutInterval), requestCallback, state);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003E70 File Offset: 0x00002070
		public ICancelableAsyncResult BeginDownload(Uri url, HttpSessionConfig sessionConfig, AsyncCallback requestCallback, object state)
		{
			this.CheckDisposed();
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

		// Token: 0x06000101 RID: 257 RVA: 0x00003F18 File Offset: 0x00002118
		public DownloadResult EndDownload(ICancelableAsyncResult asyncResult)
		{
			this.CheckDisposed();
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
			DownloadResult downloadResult = new DownloadResult(cancelableHttpAsyncResult.Exception);
			if (downloadResult.IsSucceeded || this.sessionConfig.ReadWebExceptionResponseStream)
			{
				if (this.responseStream != null)
				{
					this.responseStream.Seek(0L, SeekOrigin.Begin);
					downloadResult.ResponseStream = this.responseStream;
					this.responseStream = null;
				}
				downloadResult.LastModified = this.lastModified;
				downloadResult.ETag = this.eTag;
				downloadResult.BytesDownloaded = this.bytesReceived;
				downloadResult.ResponseUri = this.responseUri;
				downloadResult.StatusCode = this.statusCode;
				downloadResult.ResponseHeaders = this.responseHeaders;
				downloadResult.Cookies = this.cookies;
			}
			downloadResult.LastKnownRequestedUri = this.lastKnownRequestedUri;
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveEndDownload);
			this.LogDebug("Download IsSucceeded {0}", new object[]
			{
				downloadResult.IsSucceeded
			});
			return downloadResult;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004057 File Offset: 0x00002257
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004068 File Offset: 0x00002268
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

		// Token: 0x06000104 RID: 260 RVA: 0x000040A8 File Offset: 0x000022A8
		internal bool TryClose(string reason)
		{
			this.CheckDisposed();
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
				}
				if (this.httpRequestStream != null)
				{
					try
					{
						this.httpRequestStream.Flush();
						this.httpRequestStream.Dispose();
						this.httpRequestStream = null;
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

		// Token: 0x06000105 RID: 261 RVA: 0x0000423C File Offset: 0x0000243C
		internal string GetBreadcrumbsSnapshot()
		{
			this.CheckDisposed();
			return this.breadcrumbs.ToString();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004250 File Offset: 0x00002450
		protected virtual void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			lock (this.syncRoot)
			{
				if (disposing)
				{
					this.TryClose("Client Disposed");
					this.timeoutTimer.Dispose();
					this.timeoutTimer = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000042C0 File Offset: 0x000024C0
		protected virtual IAsyncResult BeginGetResponse(object state, AsyncCallback callback, params object[] args)
		{
			return this.httpWebRequest.BeginGetResponse(callback, state);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000042CF File Offset: 0x000024CF
		protected virtual IAsyncResult BeginGetRequestStream(object state, AsyncCallback callback, params object[] args)
		{
			return this.httpWebRequest.BeginGetRequestStream(callback, state);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000042DE File Offset: 0x000024DE
		protected void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000042FB File Offset: 0x000024FB
		private static void SetEndCalled(CancelableHttpAsyncResult asyncResult)
		{
			if (asyncResult.EndCalled)
			{
				throw new InvalidOperationException("The End function can only be called once for each asynchronous operation.");
			}
			asyncResult.EndCalled = true;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004318 File Offset: 0x00002518
		private static byte[] GetHeadersByteArray(WebHeaderCollection headerCollection)
		{
			if (headerCollection != null)
			{
				string s = headerCollection.ToString().Replace(Environment.NewLine, " ");
				return Encoding.ASCII.GetBytes(s);
			}
			return null;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000434B File Offset: 0x0000254B
		private static bool IsRedirect(HttpStatusCode statusCode)
		{
			return statusCode == HttpStatusCode.Found || statusCode == HttpStatusCode.MovedPermanently || statusCode == HttpStatusCode.MultipleChoices || statusCode == HttpStatusCode.TemporaryRedirect || statusCode == HttpStatusCode.SeeOther;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004378 File Offset: 0x00002578
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

		// Token: 0x0600010E RID: 270 RVA: 0x0000441C File Offset: 0x0000261C
		private bool IsaMethodWithBody()
		{
			foreach (string strA in HttpClient.verbsWithBody)
			{
				if (string.Compare(strA, this.sessionConfig.Method, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004484 File Offset: 0x00002684
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

		// Token: 0x06000110 RID: 272 RVA: 0x0000461C File Offset: 0x0000281C
		private void LogConnect(string context)
		{
			if (this.protocolLog != null)
			{
				this.protocolLog.LogOneEntry("HttpClient:Connect", null, ExecutionLog.EventType.Verbose, context, null);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000463A File Offset: 0x0000283A
		private void LogSend(byte[] data)
		{
			if (this.protocolLog != null)
			{
				this.protocolLog.LogOneEntry("HttpClient:Send", null, ExecutionLog.EventType.Information, Encoding.ASCII.GetString(data), null);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004662 File Offset: 0x00002862
		private void LogReceive(byte[] data)
		{
			if (this.protocolLog != null)
			{
				this.protocolLog.LogOneEntry("HttpClient:Receive", null, ExecutionLog.EventType.Information, Encoding.ASCII.GetString(data), null);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000468A File Offset: 0x0000288A
		private void LogInformation(string informationString, bool isError = false)
		{
			if (this.protocolLog != null)
			{
				this.protocolLog.LogOneEntry("HttpClient", null, isError ? ExecutionLog.EventType.Error : ExecutionLog.EventType.Information, informationString, null);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000046B0 File Offset: 0x000028B0
		private void LogDebug(string formatString, params object[] args)
		{
			string informationString = string.Format(CultureInfo.InvariantCulture, formatString, args);
			this.LogDebug(informationString);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000046D1 File Offset: 0x000028D1
		private void LogDebug(string informationString)
		{
			this.LogInformation(informationString, false);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000046DC File Offset: 0x000028DC
		private void LogError(string formatString, params object[] args)
		{
			string errorString = string.Format(CultureInfo.InvariantCulture, formatString, args);
			this.LogError(errorString);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000046FD File Offset: 0x000028FD
		private void LogError(string errorString)
		{
			this.LogInformation(errorString, true);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004708 File Offset: 0x00002908
		private void LogDisconnect(string reason)
		{
			if (this.protocolLog != null)
			{
				this.LogInformation("Bytes Downloaded: " + this.bytesReceived, false);
				this.protocolLog.LogOneEntry("HttpClient:Disconnect", null, ExecutionLog.EventType.Verbose, "Disconnect reason:" + reason, null);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004757 File Offset: 0x00002957
		private void InitializeResponseStream()
		{
			this.responseStream = new MemoryStream();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004764 File Offset: 0x00002964
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

		// Token: 0x0600011B RID: 283 RVA: 0x00004A73 File Offset: 0x00002C73
		private void ResponseCallBack(object asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterResponseCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.ResponseCallBackAction), (IAsyncResult)asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveResponseCallback);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004AA8 File Offset: 0x00002CA8
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
							goto IL_2C2;
						}
						catch (UriFormatException innerException)
						{
							result = new BadRedirectedUriException(text, innerException);
							goto IL_2C2;
						}
					}
					result = new WebException("Too many automatic redirections were attempted.", null, WebExceptionStatus.ProtocolError, httpWebResponse);
				}
				else
				{
					this.lastKnownRequestedUri = httpWebResponse.ResponseUri;
					if (this.sessionConfig.MaximumResponseBodyLength != -1L && this.sessionConfig.MaximumResponseBodyLength < httpWebResponse.ContentLength)
					{
						result = new DownloadLimitExceededException(this.sessionConfig.MaximumResponseBodyLength);
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
				IL_2C2:;
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

		// Token: 0x0600011D RID: 285 RVA: 0x00004E18 File Offset: 0x00003018
		private void RequestCallBack(object asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterRequestCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.RequestCallBackAction), (IAsyncResult)asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveRequestCallback);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004E4C File Offset: 0x0000304C
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

		// Token: 0x0600011F RID: 287 RVA: 0x00004EF4 File Offset: 0x000030F4
		private void ReadResponseCallBack(object asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterReadCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.ReadResponseCallBackAction), (IAsyncResult)asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveReadCallback);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004F28 File Offset: 0x00003128
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
						result = new DownloadLimitExceededException(this.sessionConfig.MaximumResponseBodyLength);
					}
					else if (this.httpWebResponseContentLength >= 0L && this.httpWebResponseContentLength < this.bytesReceived)
					{
						result = new ServerProtocolViolationException(this.httpWebResponseContentLength);
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

		// Token: 0x06000121 RID: 289 RVA: 0x00005094 File Offset: 0x00003294
		private void ReadRequestCallBack(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterReadCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.ReadRequestCallBackAction), asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveReadCallback);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000050C4 File Offset: 0x000032C4
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

		// Token: 0x06000123 RID: 291 RVA: 0x000051EC File Offset: 0x000033EC
		private void WriteResponseCallBack(IAsyncResult asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterWriteCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.WriteResponseCallBackAction), asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveWriteCallback);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000521A File Offset: 0x0000341A
		private Exception WriteResponseCallBackAction(IAsyncResult asyncResult)
		{
			return this.WriteCallBackAction(asyncResult, this.responseStream, this.httpResponseStream, this.readResponseCallBack);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00005235 File Offset: 0x00003435
		private void WriteRequestCallBack(object asyncResult)
		{
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.EnterWriteCallback);
			this.AsyncCallBack(new HttpClient.AsyncCallbackAction(this.WriteRequestCallBackAction), (IAsyncResult)asyncResult);
			this.breadcrumbs.Drop(HttpClient.Breadcrumbs.LeaveWriteCallback);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000052F0 File Offset: 0x000034F0
		private AsyncCallback WrapCallbackWithUnhandledExceptionRedirect(WaitCallback callback)
		{
			return delegate(IAsyncResult asyncResult)
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						callback(asyncResult);
					});
				}
				catch (GrayException)
				{
					this.breadcrumbs.Drop(HttpClient.Breadcrumbs.CrashOnAnotherThread);
				}
			};
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000531D File Offset: 0x0000351D
		private Exception WriteRequestCallBackAction(IAsyncResult asyncResult)
		{
			return this.WriteCallBackAction(asyncResult, this.httpRequestStream, this.requestStream, this.readRequestCallBack);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005338 File Offset: 0x00003538
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

		// Token: 0x06000129 RID: 297 RVA: 0x000053CC File Offset: 0x000035CC
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

		// Token: 0x0600012A RID: 298 RVA: 0x00005444 File Offset: 0x00003644
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

		// Token: 0x0600012B RID: 299 RVA: 0x00005568 File Offset: 0x00003768
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

		// Token: 0x0600012C RID: 300 RVA: 0x000055D8 File Offset: 0x000037D8
		private IAsyncResult BeginGetRequestStreamAction(object state, AsyncCallback callback, params object[] args)
		{
			EventHandler<HttpWebRequestEventArgs> sendingRequest = this.SendingRequest;
			if (sendingRequest != null)
			{
				sendingRequest(this, new HttpWebRequestEventArgs(this.httpWebRequest));
			}
			return this.IssueHttpWebRequest(new HttpClient.AsyncRequest(this.BeginGetRequestStream), state, callback, args);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005617 File Offset: 0x00003817
		private IAsyncResult BeginGetResponseAction(object state, AsyncCallback callback, params object[] args)
		{
			return this.IssueHttpWebRequest(new HttpClient.AsyncRequest(this.BeginGetResponse), state, callback, args);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005630 File Offset: 0x00003830
		private IAsyncResult BeginRead(object state, AsyncCallback callback, params object[] args)
		{
			Stream stream = (Stream)args[0];
			return stream.BeginRead(this.Buffer, 0, 4096, callback, state);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000565C File Offset: 0x0000385C
		private IAsyncResult BeginWrite(object state, AsyncCallback callback, params object[] args)
		{
			Stream stream = (Stream)args[0];
			int count = (int)args[1];
			return stream.BeginWrite(this.Buffer, 0, count, callback, state);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000568C File Offset: 0x0000388C
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

		// Token: 0x06000131 RID: 305 RVA: 0x000056FC File Offset: 0x000038FC
		private void UnRegisterTimeout()
		{
			lock (this.syncRoot)
			{
				this.timeoutTimer.Change(-1, -1);
				this.pendingAsyncResult = null;
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000574C File Offset: 0x0000394C
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
					}
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000057E0 File Offset: 0x000039E0
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

		// Token: 0x04000079 RID: 121
		public const string BPropPatchMethod = "BPROPPATCH";

		// Token: 0x0400007A RID: 122
		public const string DeleteMethod = "DELETE";

		// Token: 0x0400007B RID: 123
		public const string LockMethod = "LOCK";

		// Token: 0x0400007C RID: 124
		public const string MoveMethod = "MOVE";

		// Token: 0x0400007D RID: 125
		public const string PropFindMethod = "PROPFIND";

		// Token: 0x0400007E RID: 126
		public const string PropPatchMethod = "PROPPATCH";

		// Token: 0x0400007F RID: 127
		public const string SearchMethod = "SEARCH";

		// Token: 0x04000080 RID: 128
		public const string UnLockMethod = "UNLOCK";

		// Token: 0x04000081 RID: 129
		private const string Seperator = " ";

		// Token: 0x04000082 RID: 130
		private const string OperationCompleted = "Operation Completed";

		// Token: 0x04000083 RID: 131
		private const string ClientDisposed = "Client Disposed";

		// Token: 0x04000084 RID: 132
		private const string IfHeader = "If";

		// Token: 0x04000085 RID: 133
		private const int BufferSize = 4096;

		// Token: 0x04000086 RID: 134
		private const int NumberOfBreadcrumbs = 64;

		// Token: 0x04000087 RID: 135
		private static List<string> verbsWithBody = new List<string>
		{
			"BPROPPATCH",
			"LOCK",
			"MKCOL",
			"POST",
			"PROPFIND",
			"PROPPATCH",
			"PUT",
			"SEARCH",
			"UNLOCK"
		};

		// Token: 0x04000088 RID: 136
		private readonly ExecutionLog protocolLog;

		// Token: 0x04000089 RID: 137
		private readonly AsyncCallback responseCallBack;

		// Token: 0x0400008A RID: 138
		private readonly AsyncCallback requestCallBack;

		// Token: 0x0400008B RID: 139
		private readonly AsyncCallback readResponseCallBack;

		// Token: 0x0400008C RID: 140
		private readonly AsyncCallback writeResponseCallBack;

		// Token: 0x0400008D RID: 141
		private readonly AsyncCallback readRequestCallBack;

		// Token: 0x0400008E RID: 142
		private readonly AsyncCallback writeRequestCallBack;

		// Token: 0x0400008F RID: 143
		private object syncRoot = new object();

		// Token: 0x04000090 RID: 144
		private bool sessionClosing;

		// Token: 0x04000091 RID: 145
		private bool completedSynchronously;

		// Token: 0x04000092 RID: 146
		private HttpWebRequest httpWebRequest;

		// Token: 0x04000093 RID: 147
		private Stream httpResponseStream;

		// Token: 0x04000094 RID: 148
		private Stream responseStream;

		// Token: 0x04000095 RID: 149
		private Stream httpRequestStream;

		// Token: 0x04000096 RID: 150
		private Stream requestStream;

		// Token: 0x04000097 RID: 151
		private byte[] buffer;

		// Token: 0x04000098 RID: 152
		private Breadcrumbs<HttpClient.Breadcrumbs> breadcrumbs;

		// Token: 0x04000099 RID: 153
		private long httpWebResponseContentLength;

		// Token: 0x0400009A RID: 154
		private DateTime? lastModified;

		// Token: 0x0400009B RID: 155
		private string eTag;

		// Token: 0x0400009C RID: 156
		private long bytesReceived;

		// Token: 0x0400009D RID: 157
		private long bytesUploaded;

		// Token: 0x0400009E RID: 158
		private Timer timeoutTimer;

		// Token: 0x0400009F RID: 159
		private IAsyncResult pendingAsyncResult;

		// Token: 0x040000A0 RID: 160
		private DateTime requestTimeout;

		// Token: 0x040000A1 RID: 161
		private Uri responseUri;

		// Token: 0x040000A2 RID: 162
		private Uri lastKnownRequestedUri;

		// Token: 0x040000A3 RID: 163
		private HttpStatusCode statusCode;

		// Token: 0x040000A4 RID: 164
		private WebHeaderCollection responseHeaders;

		// Token: 0x040000A5 RID: 165
		private CookieCollection cookies;

		// Token: 0x040000A6 RID: 166
		private HttpSessionConfig sessionConfig;

		// Token: 0x040000A7 RID: 167
		private int urlRedirections;

		// Token: 0x040000A8 RID: 168
		private bool doesRequestContainsBody;

		// Token: 0x040000A9 RID: 169
		private volatile bool disposed;

		// Token: 0x0200003C RID: 60
		// (Invoke) Token: 0x06000136 RID: 310
		private delegate IAsyncResult AsyncRequest(object state, AsyncCallback callback, params object[] args);

		// Token: 0x0200003D RID: 61
		// (Invoke) Token: 0x0600013A RID: 314
		private delegate Exception AsyncCallbackAction(IAsyncResult asyncResult);

		// Token: 0x0200003E RID: 62
		private enum Breadcrumbs
		{
			// Token: 0x040000AE RID: 174
			EnterBeginDownload = 1,
			// Token: 0x040000AF RID: 175
			EnterEndDownload,
			// Token: 0x040000B0 RID: 176
			EnterResponseCallback,
			// Token: 0x040000B1 RID: 177
			EnterReadCallback,
			// Token: 0x040000B2 RID: 178
			EnterWriteCallback,
			// Token: 0x040000B3 RID: 179
			EnterTimeoutCallback,
			// Token: 0x040000B4 RID: 180
			EnterTryClose,
			// Token: 0x040000B5 RID: 181
			EnterRequestCallback,
			// Token: 0x040000B6 RID: 182
			LeaveBeginDownload = 10,
			// Token: 0x040000B7 RID: 183
			LeaveEndDownload,
			// Token: 0x040000B8 RID: 184
			LeaveResponseCallback,
			// Token: 0x040000B9 RID: 185
			LeaveReadCallback,
			// Token: 0x040000BA RID: 186
			LeaveWriteCallback,
			// Token: 0x040000BB RID: 187
			LeaveTimeoutCallback,
			// Token: 0x040000BC RID: 188
			LeaveTryClose,
			// Token: 0x040000BD RID: 189
			LeaveRequestCallback,
			// Token: 0x040000BE RID: 190
			CrashOnAnotherThread = 20
		}
	}
}
