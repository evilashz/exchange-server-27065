using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000111 RID: 273
	internal class ProxyHandler
	{
		// Token: 0x06000E84 RID: 3716 RVA: 0x00051D5A File Offset: 0x0004FF5A
		public ProxyHandler(ProtocolLogger protocolLogger)
		{
			this.protocolLogger = protocolLogger;
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00051D84 File Offset: 0x0004FF84
		private bool HasBeenProxiedByMServ
		{
			get
			{
				return this.proxyInfo != null && !this.proxyInfo.CanImpersonate;
			}
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00051DA0 File Offset: 0x0004FFA0
		public IAsyncResult BeginProcessRequest(HttpContext context, IAsyncResult asyncResult, ProxyHandler.ProxiedRequestInfo proxyinfo)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			this.proxyInfo = proxyinfo;
			this.remoteUri = this.proxyInfo.RemoteUri;
			this.userName = this.proxyInfo.User;
			try
			{
				AirSyncDiagnostics.TraceDebug<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BeginProcessRequest called for serverName: {0}, user {1}", this.remoteUri, this.userName);
				if (!string.Equals(context.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) && !string.Equals(context.Request.HttpMethod, "OPTIONS", StringComparison.OrdinalIgnoreCase))
				{
					context.Response.StatusCode = 405;
					lazyAsyncResult.InvokeCallback();
					return lazyAsyncResult;
				}
				UriBuilder uriBuilder = new UriBuilder(this.proxyInfo.RemoteUri);
				if (!string.IsNullOrEmpty(context.Request.Url.Query))
				{
					if (!string.IsNullOrEmpty(context.Request.Url.Fragment))
					{
						string str = context.Request.Url.Fragment.Remove(0, 1);
						uriBuilder.Query = context.Request.Url.Query.Remove(0, 1) + "%23" + str;
					}
					else
					{
						uriBuilder.Query = context.Request.Url.Query.Remove(0, 1);
					}
				}
				this.cachedUri = uriBuilder.Uri;
				HttpWebRequest httpWebRequest = this.CopyClientRequest(context.Request, this.cachedUri);
				WindowsImpersonationContext windowsImpersonationContext = null;
				IAsyncResult asyncResult2 = null;
				try
				{
					if (this.proxyInfo.RequiresImpersonation)
					{
						windowsImpersonationContext = ((WindowsIdentity)context.User.Identity).Impersonate();
					}
					else
					{
						NetworkServiceImpersonator.Initialize();
						windowsImpersonationContext = NetworkServiceImpersonator.Impersonate();
						httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
					}
					this.backEndWebRequest = httpWebRequest;
					this.frontEndResult = lazyAsyncResult;
					this.frontEndHttpContext = context;
					try
					{
						this.clientRequestStream = this.frontEndHttpContext.Request.InputStream;
					}
					catch (COMException innerException)
					{
						this.protocolLogger.SetValue(ProtocolLoggerData.Error, "InputStreamUnavailable");
						throw new AirSyncPermanentException(HttpStatusCode.ServiceUnavailable, StatusCode.ServerErrorRetryLater, innerException, false);
					}
					this.issueTime = ExDateTime.Now;
					this.pendingAsyncOperation = ProxyHandler.PendingOperationState.BackEndGetRequestStream;
					asyncResult2 = this.backEndWebRequest.BeginGetRequestStream(new AsyncCallback(this.BackEndGetRequestStream), null);
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
						windowsImpersonationContext.Dispose();
					}
				}
				if (asyncResult2 != null && !asyncResult2.CompletedSynchronously)
				{
					this.RegisterTimeoutWaitHandle(asyncResult2, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
				}
			}
			catch (Exception ex)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BeginProcessRequest caught an exception\r\n{0}", arg);
				if (this.backEndWebRequest != null)
				{
					this.backEndWebRequest.Abort();
					this.backEndWebRequest = null;
				}
				if (ex is WebException)
				{
					AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BeginProcessRequest swallowing exception and returning HTTP 500");
					try
					{
						this.TryUnregisterTimeoutWaitHandle();
						this.frontEndHttpContext.Response.StatusCode = 500;
						this.frontEndResult.InvokeCallback();
						goto IL_2F0;
					}
					catch (HttpException ex2)
					{
						AirSyncUtility.ExceptionToStringHelper arg2 = new AirSyncUtility.ExceptionToStringHelper(ex2);
						AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "HttpException was thrown while setting the StatusCode.\r\n{0}", arg2);
						goto IL_2F0;
					}
					goto IL_2EE;
					IL_2F0:
					return lazyAsyncResult;
				}
				IL_2EE:
				throw;
			}
			return lazyAsyncResult;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00052108 File Offset: 0x00050308
		internal static bool SslCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return GlobalSettings.AllowInternalUntrustedCerts || sslPolicyErrors == SslPolicyErrors.None;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00052124 File Offset: 0x00050324
		private static bool HasValidAuthString(string gccHeaderValue)
		{
			int num = gccHeaderValue.IndexOf(',');
			return num > 0 && GccUtils.IsValidAuthString(gccHeaderValue.Substring(0, num).Trim());
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00052154 File Offset: 0x00050354
		private void BackEndGetRequestStream(IAsyncResult asynchronousResult)
		{
			lock (this)
			{
				if (this.requestWasTimedOut)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndGetRequestStream() Request was timed out while waiting for lock!");
					return;
				}
			}
			try
			{
				AirSyncDiagnostics.TraceDebug<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndGetRequestStream called for serverName: {0}, user {1}", this.remoteUri, this.userName);
				this.UnregisterTimeoutWaitHandle();
				this.backEndRequestStream = this.backEndWebRequest.EndGetRequestStream(asynchronousResult);
				this.pendingAsyncOperation = ProxyHandler.PendingOperationState.FrontEndReadContentFromClient;
				IAsyncResult asyncResult = this.clientRequestStream.BeginRead(this.asyncBuffer, 0, this.asyncBuffer.Length, new AsyncCallback(this.FrontEndReadContentFromClient), null);
				if (!asyncResult.CompletedSynchronously)
				{
					this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
				}
			}
			catch (Exception ex)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndGetRequestStream caught an exception\r\n{0}", arg);
				if (this.backEndWebRequest != null)
				{
					this.backEndWebRequest.Abort();
					this.backEndWebRequest = null;
				}
				this.HandleException(ex);
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00052274 File Offset: 0x00050474
		private void FrontEndReadContentFromClient(IAsyncResult asynchronousResult)
		{
			lock (this)
			{
				if (this.requestWasTimedOut)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.FrontEndReadContentFromClient() Request was timed out while waiting for lock!");
					return;
				}
			}
			try
			{
				AirSyncDiagnostics.TraceDebug<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.FrontEndReadContentFromClient called for serverName: {0}, user {1}", this.remoteUri, this.userName);
				this.UnregisterTimeoutWaitHandle();
				if (this.proxyLoginAttempts == 0 && this.cachedRequestBody == null)
				{
					this.cachedRequestBody = new MemoryStream(this.frontEndHttpContext.Request.ContentLength);
				}
				int num = this.clientRequestStream.EndRead(asynchronousResult);
				if (num > 0)
				{
					if (this.proxyLoginAttempts == 0)
					{
						this.cachedRequestBody.Write(this.asyncBuffer, 0, num);
					}
					lock (this)
					{
						this.pendingAsyncOperation = ProxyHandler.PendingOperationState.BackEndWriteContent;
						IAsyncResult asyncResult = this.backEndRequestStream.BeginWrite(this.asyncBuffer, 0, num, new AsyncCallback(this.BackEndWriteContent), null);
						if (!asyncResult.CompletedSynchronously)
						{
							this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
						}
						goto IL_16D;
					}
				}
				this.backEndRequestStream.Flush();
				this.backEndRequestStream.Dispose();
				this.backEndRequestStream = null;
				this.pendingAsyncOperation = ProxyHandler.PendingOperationState.BackEndGetResponse;
				IAsyncResult asyncResult2 = this.backEndWebRequest.BeginGetResponse(new AsyncCallback(this.BackEndGetResponse), null);
				if (!asyncResult2.CompletedSynchronously)
				{
					this.RegisterTimeoutWaitHandle(asyncResult2, (int)GlobalSettings.ProxyHandlerLongTimeout.TotalMilliseconds);
				}
				IL_16D:;
			}
			catch (Exception ex)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.FrontEndReadContentFromClient caught an exception\r\n{0}", arg);
				if (this.backEndWebRequest != null)
				{
					this.backEndWebRequest.Abort();
					this.backEndWebRequest = null;
				}
				this.HandleException(ex);
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00052480 File Offset: 0x00050680
		private void BackEndWriteContent(IAsyncResult asynchronousResult)
		{
			lock (this)
			{
				if (this.requestWasTimedOut)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndWriteContent() Request was timed out while waiting for lock!");
					return;
				}
			}
			try
			{
				AirSyncDiagnostics.TraceDebug<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndWriteContent called for serverName: {0}, user {1}", this.remoteUri, this.userName);
				this.UnregisterTimeoutWaitHandle();
				this.backEndRequestStream.EndWrite(asynchronousResult);
				this.pendingAsyncOperation = ProxyHandler.PendingOperationState.FrontEndReadContentFromClient;
				IAsyncResult asyncResult = this.clientRequestStream.BeginRead(this.asyncBuffer, 0, this.asyncBuffer.Length, new AsyncCallback(this.FrontEndReadContentFromClient), null);
				if (!asyncResult.CompletedSynchronously)
				{
					this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
				}
			}
			catch (Exception ex)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndWriteContent caught an exception\r\n{0}", arg);
				if (this.backEndWebRequest != null)
				{
					this.backEndWebRequest.Abort();
					this.backEndWebRequest = null;
				}
				this.HandleException(ex);
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0005259C File Offset: 0x0005079C
		private void TimeoutCallback(object state, bool timedOut)
		{
			if (!timedOut)
			{
				AirSyncDiagnostics.TraceInfo<ProxyHandler.PendingOperationState, TimeSpan>(ExTraceGlobals.RequestsTracer, null, "ProxyHandler.TimeoutCallback() Async request is being completed in the callback. State {0}! Processing time {1}", this.pendingAsyncOperation, ExDateTime.Now - this.issueTime);
				return;
			}
			IAsyncResult asyncResult = state as IAsyncResult;
			lock (this)
			{
				if (asyncResult.IsCompleted || this.requestWasTimedOut || this.frontEndResult == null || this.frontEndResult.IsCompleted)
				{
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.TimeoutCallback() Request was completed while waiting for lock. State {0} AsyncResult.IsCompleted {1} requestWasTimedOut {2} frontEndResult.IsCompleted {3}!", new object[]
					{
						this.pendingAsyncOperation,
						asyncResult.IsCompleted,
						this.requestWasTimedOut,
						this.frontEndResult == null || this.frontEndResult.IsCompleted
					});
				}
				else
				{
					AirSyncDiagnostics.TraceInfo<ProxyHandler.PendingOperationState, IAsyncResult>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.TimeoutCallback() Aborting request that timed out in state {0} ASyncResult {1}!", this.pendingAsyncOperation, asyncResult);
					string text = this.proxyInfo.RemoteUri.Host.ToString();
					AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_ProxyResultTimedOut, text, new string[]
					{
						text
					});
					this.requestWasTimedOut = true;
					this.backEndWebRequest.Abort();
					this.frontEndHttpContext.Response.StatusCode = 503;
					this.frontEndResult.InvokeCallback();
					this.ReleaseResources();
				}
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00052720 File Offset: 0x00050920
		private void BackEndGetResponse(IAsyncResult asynchronousResult)
		{
			lock (this)
			{
				if (this.requestWasTimedOut)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndGetResponse() Request was timed out while waiting for lock!");
					string text = this.remoteUri.Host.ToString();
					AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_ProxyResultTimedOut, text, new string[]
					{
						text
					});
					return;
				}
			}
			try
			{
				AirSyncDiagnostics.TraceDebug<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndGetResponse called for serverName: {0}, user {1}", this.remoteUri, this.userName);
				this.UnregisterTimeoutWaitHandle();
				try
				{
					this.httpWebResponse = (HttpWebResponse)this.backEndWebRequest.EndGetResponse(asynchronousResult);
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.ProtocolError)
					{
						HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
						if (httpWebResponse == null)
						{
							this.HandleException(ex);
							return;
						}
						if (httpWebResponse.StatusCode == (HttpStatusCode)441)
						{
							httpWebResponse.Close();
							if (!this.proxyInfo.AttemptProxyLogin)
							{
								AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "BackEndGetResponse(): authentication failure for {0}.", this.userName);
								this.TryUnregisterTimeoutWaitHandle();
								this.frontEndHttpContext.Response.StatusCode = 403;
								this.frontEndResult.InvokeCallback();
								return;
							}
							AirSyncDiagnostics.Assert(!this.proxyInfo.RequiresImpersonation && this.proxyInfo.AdditionalHeaders != null);
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "BackEndGetResponse(): proxy login required for {0}.", this.userName);
							if (this.proxyLoginAttempts > 0)
							{
								AirSyncDiagnostics.TraceFunction(ExTraceGlobals.RequestsTracer, this, "BackEndGetResponse(): too many login attempts.");
								this.protocolLogger.SetValue(ProtocolLoggerData.Error, "TooManyProxyLoginAttempts");
								this.TryUnregisterTimeoutWaitHandle();
								this.frontEndHttpContext.Response.StatusCode = 403;
								this.frontEndResult.InvokeCallback();
								return;
							}
							this.proxyLoginAttempts++;
							this.IssueProxyLoginRequest();
							return;
						}
						else
						{
							if (httpWebResponse.Headers != null)
							{
								WebHeaderCollection headers = httpWebResponse.Headers;
								string text2 = headers["WWW-Authenticate"];
								if (text2 != null && text2.IndexOf("Negotiate ") == -1)
								{
									if (this.HasBeenProxiedByMServ)
									{
										this.protocolLogger.SetValue(ProtocolLoggerData.Error, "Datacenter Config issue upon MServ proxy");
									}
									else
									{
										this.protocolLogger.SetValue(ProtocolLoggerData.Error, "NTLM not on the destination CAS");
										string text3 = this.proxyInfo.RemoteUri.Host.ToString();
										AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_SecondCasFailureNTLM, text3, new string[]
										{
											text3
										});
									}
								}
							}
							this.httpWebResponse = httpWebResponse;
						}
					}
					else
					{
						if (ex.Status == WebExceptionStatus.TrustFailure)
						{
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "SSL Certification failed for {0}.", this.proxyInfo.RemoteUri.Host);
							this.protocolLogger.SetValue(ProtocolLoggerData.Error, "SSL Certification Failed.");
							string text4 = this.proxyInfo.RemoteUri.Host.ToString();
							AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_SecondCasFailureSSL, text4, new string[]
							{
								text4
							});
							this.HandleException(ex);
							return;
						}
						this.HandleException(ex);
						return;
					}
				}
				if (GlobalSettings.ProxyHeaders != null && GlobalSettings.ProxyHeaders.Length > 0)
				{
					this.frontEndHttpContext.Response.ContentType = string.Empty;
					for (int i = 0; i < this.httpWebResponse.Headers.Count; i++)
					{
						string text5 = this.httpWebResponse.Headers.Keys[i];
						for (int j = 0; j < GlobalSettings.ProxyHeaders.Length; j++)
						{
							if (string.Equals(text5, GlobalSettings.ProxyHeaders[j], StringComparison.OrdinalIgnoreCase))
							{
								this.frontEndHttpContext.Response.AppendHeader(text5, this.httpWebResponse.Headers[text5]);
							}
						}
					}
				}
				this.backEndResponseStream = this.httpWebResponse.GetResponseStream();
				this.frontEndHttpContext.Response.StatusCode = (int)this.httpWebResponse.StatusCode;
				this.frontEndHttpContext.Response.StatusDescription = this.httpWebResponse.StatusDescription;
				this.pendingAsyncOperation = ProxyHandler.PendingOperationState.BackEndReadContent;
				IAsyncResult asyncResult = this.backEndResponseStream.BeginRead(this.asyncBuffer, 0, this.asyncBuffer.Length, new AsyncCallback(this.BackEndReadContent), null);
				if (!asyncResult.CompletedSynchronously)
				{
					this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
				}
			}
			catch (Exception ex2)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex2);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndGetResponse caught an exception\r\n{0}", arg);
				if (this.backEndWebRequest != null)
				{
					this.backEndWebRequest.Abort();
					this.backEndWebRequest = null;
				}
				this.HandleException(ex2);
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00052C0C File Offset: 0x00050E0C
		private void BackEndReadContent(IAsyncResult asynchronousResult)
		{
			lock (this)
			{
				if (this.requestWasTimedOut)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndReadContent() Request was timed out while waiting for lock!");
					string text = this.remoteUri.Host.ToString();
					AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_ProxyResultTimedOut, text, new string[]
					{
						text
					});
					return;
				}
			}
			try
			{
				AirSyncDiagnostics.TraceDebug<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndReadContent called for serverName: {0}, user {1}", this.remoteUri, this.userName);
				this.UnregisterTimeoutWaitHandle();
				int num = this.backEndResponseStream.EndRead(asynchronousResult);
				if (num > 0)
				{
					this.frontEndHttpContext.Response.OutputStream.Write(this.asyncBuffer, 0, num);
					this.backEndResponseStream = this.httpWebResponse.GetResponseStream();
					this.pendingAsyncOperation = ProxyHandler.PendingOperationState.BackEndReadContent;
					IAsyncResult asyncResult = this.backEndResponseStream.BeginRead(this.asyncBuffer, 0, this.asyncBuffer.Length, new AsyncCallback(this.BackEndReadContent), null);
					if (!asyncResult.CompletedSynchronously)
					{
						this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
					}
				}
				else
				{
					this.UnregisterTimeoutWaitHandle();
					this.frontEndResult.InvokeCallback();
					this.ReleaseResources();
				}
			}
			catch (Exception ex)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.BackEndReadContent caught an exception\r\n{0}", arg);
				if (this.backEndWebRequest != null)
				{
					this.backEndWebRequest.Abort();
					this.backEndWebRequest = null;
				}
				this.HandleException(ex);
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00052DA4 File Offset: 0x00050FA4
		private void IssueProxyLoginRequest()
		{
			AirSyncDiagnostics.TraceDebug<Uri, string>(ExTraceGlobals.RequestsTracer, this, "IssueProxyLoginRequest() invoked against {0} for {1}.", this.remoteUri, this.userName);
			HttpWebRequest httpWebRequest = this.CreateWebRequest(new UriBuilder(this.proxyInfo.RemoteUri)
			{
				Query = "cmd=ProxyLogin"
			}.Uri);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "text/xml";
			httpWebRequest.ContentLength = (long)this.proxyInfo.CscBuffer.Length;
			httpWebRequest.Headers["X-EAS-Proxy"] = this.proxyInfo.AdditionalHeaders["X-EAS-Proxy"];
			this.backEndWebRequest = httpWebRequest;
			this.issueTime = ExDateTime.Now;
			this.pendingAsyncOperation = ProxyHandler.PendingOperationState.BackEndGetRequestStream;
			IAsyncResult asyncResult = this.backEndWebRequest.BeginGetRequestStream(new AsyncCallback(this.ProxyLoginGetStreamCompleteCallback), null);
			if (!asyncResult.CompletedSynchronously)
			{
				this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
			}
			AirSyncDiagnostics.TraceFunction<Uri, string>(ExTraceGlobals.RequestsTracer, this, "IssueProxyLoginRequest() completed: {0}, {1}.", this.remoteUri, this.userName);
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00052EB0 File Offset: 0x000510B0
		private void ProxyLoginGetStreamCompleteCallback(IAsyncResult iar)
		{
			AirSyncDiagnostics.TraceFunction<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyLoginRequestCompleteCallback: {0}, {1}.", this.remoteUri, this.userName);
			lock (this)
			{
				if (this.requestWasTimedOut)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.ProxyLoginGetStreamCompleteCallback() Request was timed out while waiting for lock!");
					return;
				}
			}
			try
			{
				this.UnregisterTimeoutWaitHandle();
				this.backEndRequestStream = this.backEndWebRequest.EndGetRequestStream(iar);
				this.pendingAsyncOperation = ProxyHandler.PendingOperationState.ProxyLoginWritingCsc;
				IAsyncResult asyncResult = this.backEndRequestStream.BeginWrite(this.proxyInfo.CscBuffer, 0, this.proxyInfo.CscBuffer.Length, new AsyncCallback(this.ProxyLoginWriteCompleteCallback), null);
				if (!asyncResult.CompletedSynchronously)
				{
					this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
				}
			}
			catch (Exception ex)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "ProxyLoginRequestCompleteCallback caught an exception\r\n{0}", arg);
				if (this.backEndWebRequest != null)
				{
					this.backEndWebRequest.Abort();
					this.backEndWebRequest = null;
				}
				this.HandleException(ex);
			}
			AirSyncDiagnostics.TraceFunction<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyLoginRequestCompleteCallback: completed {0}, {1}.", this.remoteUri, this.userName);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00052FF8 File Offset: 0x000511F8
		private void ProxyLoginWriteCompleteCallback(IAsyncResult iar)
		{
			AirSyncDiagnostics.TraceFunction<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyLoginWriteCompleteCallback: {0}, {1}.", this.remoteUri, this.userName);
			lock (this)
			{
				if (this.requestWasTimedOut)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.ProxyLoginWriteCompleteCallback() Request was timed out while waiting for lock!");
					return;
				}
			}
			try
			{
				this.UnregisterTimeoutWaitHandle();
				this.backEndRequestStream.EndWrite(iar);
				this.pendingAsyncOperation = ProxyHandler.PendingOperationState.ProxyLoginGettingResponse;
				IAsyncResult asyncResult = this.backEndWebRequest.BeginGetResponse(new AsyncCallback(this.ProxyLoginGetResponseCompleteCallback), null);
				if (!asyncResult.CompletedSynchronously)
				{
					this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
				}
			}
			catch (Exception ex)
			{
				if (this.backEndWebRequest != null)
				{
					this.backEndWebRequest.Abort();
					this.backEndWebRequest = null;
				}
				this.HandleException(ex);
			}
			AirSyncDiagnostics.TraceFunction<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyLoginWriteCompleteCallback: completed {0}, {1}.", this.remoteUri, this.userName);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00053104 File Offset: 0x00051304
		private void ProxyLoginGetResponseCompleteCallback(IAsyncResult iar)
		{
			AirSyncDiagnostics.TraceFunction<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyLoginGetResponseCompleteCallback: {0}, {1}.", this.remoteUri, this.userName);
			lock (this)
			{
				if (this.requestWasTimedOut)
				{
					AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "ProxyHandler.ProxyLoginGetResponseCompleteCallback() Request was timed out while waiting for lock!");
					return;
				}
			}
			try
			{
				this.TryUnregisterTimeoutWaitHandle();
				this.httpWebResponse = (HttpWebResponse)this.backEndWebRequest.EndGetResponse(iar);
			}
			catch (WebException ex)
			{
				this.HandleException(ex);
				return;
			}
			finally
			{
				if (this.backEndRequestStream != null)
				{
					this.backEndRequestStream.Flush();
					this.backEndRequestStream.Dispose();
					this.backEndRequestStream = null;
				}
				if (this.httpWebResponse != null)
				{
					this.httpWebResponse.Close();
				}
			}
			if (this.httpWebResponse == null)
			{
				this.HandleException(new WebException("Received null response object."));
			}
			else
			{
				if (this.httpWebResponse.StatusCode != HttpStatusCode.OK)
				{
					this.HandleException(new WebException("Received HTTP status " + this.httpWebResponse.StatusCode + " from second CAS."));
					return;
				}
				this.backEndWebRequest = this.CopyClientRequest(this.frontEndHttpContext.Request, this.cachedUri);
				this.cachedRequestBody.Seek(0L, SeekOrigin.Begin);
				this.clientRequestStream = this.cachedRequestBody;
				this.cachedRequestBody = null;
				this.pendingAsyncOperation = ProxyHandler.PendingOperationState.BackEndGetRequestStream;
				IAsyncResult asyncResult = this.backEndWebRequest.BeginGetRequestStream(new AsyncCallback(this.BackEndGetRequestStream), null);
				if (!asyncResult.CompletedSynchronously)
				{
					this.RegisterTimeoutWaitHandle(asyncResult, (int)GlobalSettings.ProxyHandlerShortTimeout.TotalMilliseconds);
				}
				AirSyncDiagnostics.TraceFunction<Uri, string>(ExTraceGlobals.RequestsTracer, this, "ProxyLoginGetResponseCompleteCallback: completed {0}, {1}.", this.remoteUri, this.userName);
				return;
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000532E0 File Offset: 0x000514E0
		private HttpWebRequest CreateWebRequest(Uri uri)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
			httpWebRequest.UnsafeAuthenticatedConnectionSharing = !this.proxyInfo.RequiresImpersonation;
			if (!this.proxyInfo.RequiresImpersonation)
			{
				httpWebRequest.ServicePoint.ConnectionLimit = GlobalSettings.ProxyConnectionPoolConnectionLimit;
			}
			httpWebRequest.ServicePoint.SetTcpKeepAlive(true, 240000, 5000);
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.AllowAutoRedirect = false;
			httpWebRequest.KeepAlive = !this.proxyInfo.RequiresImpersonation;
			CertificateValidationManager.SetComponentId(httpWebRequest, "AirSync");
			if (this.proxyInfo.CanImpersonate)
			{
				ICredentials defaultCredentials = CredentialCache.DefaultCredentials;
				httpWebRequest.Credentials = defaultCredentials.GetCredential(this.proxyInfo.RemoteUri, "Kerberos");
				if (httpWebRequest.Credentials == null)
				{
					this.protocolLogger.SetValue(ProtocolLoggerData.Error, "NoKerberosCredentials");
					throw new AirSyncPermanentException(HttpStatusCode.InternalServerError, StatusCode.ServerError, null, false);
				}
			}
			return httpWebRequest;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000533C8 File Offset: 0x000515C8
		private void HandleException(Exception ex)
		{
			AirSyncDiagnostics.TraceError<Exception>(ExTraceGlobals.RequestsTracer, this, "HandleException() is swallowing exception and returning HTTP 500 - {0}", ex);
			WebException ex2 = ex as WebException;
			if (ex2 != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(ex2.Status);
				if (ex2.Response != null)
				{
					stringBuilder.Append(".");
					stringBuilder.Append(((HttpWebResponse)ex2.Response).StatusCode);
				}
				this.protocolLogger.SetValue(ProtocolLoggerData.Error, stringBuilder.ToString());
			}
			else if (ex is SocketException || ex is IOException)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append("SocketIOException: ");
				stringBuilder2.Append(ex.Message);
				this.protocolLogger.SetValue(ProtocolLoggerData.Error, stringBuilder2.ToString());
			}
			else
			{
				if (!(ex is AirSyncPermanentException))
				{
					throw ex;
				}
				StringBuilder stringBuilder3 = new StringBuilder();
				stringBuilder3.Append("AirSyncPermanentException: ");
				stringBuilder3.Append(ex.Message);
				this.protocolLogger.SetValue(ProtocolLoggerData.Error, stringBuilder3.ToString());
			}
			try
			{
				this.TryUnregisterTimeoutWaitHandle();
				this.frontEndHttpContext.Response.Clear();
				this.frontEndHttpContext.Response.StatusCode = 500;
				this.frontEndResult.InvokeCallback();
			}
			catch (HttpException ex3)
			{
				AirSyncUtility.ExceptionToStringHelper arg = new AirSyncUtility.ExceptionToStringHelper(ex3);
				AirSyncDiagnostics.TraceError<AirSyncUtility.ExceptionToStringHelper>(ExTraceGlobals.RequestsTracer, this, "HttpException was thrown while setting the StatusCode.\r\n{0}", arg);
			}
			finally
			{
				this.ReleaseResources();
			}
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00053550 File Offset: 0x00051750
		private HttpWebRequest CopyClientRequest(HttpRequest clientrequest, Uri proxyuri)
		{
			string b = HttpRequestHeader.Authorization.ToString();
			HttpWebRequest httpWebRequest = this.CreateWebRequest(proxyuri);
			httpWebRequest.Method = clientrequest.HttpMethod;
			for (int i = 0; i < clientrequest.Headers.Count; i++)
			{
				string text = clientrequest.Headers.Keys[i];
				if ((!(text == b) || !this.proxyInfo.CanImpersonate) && !WebHeaderCollection.IsRestricted(text))
				{
					httpWebRequest.Headers[text] = clientrequest.Headers[text];
				}
			}
			CertificateValidationManager.SetComponentId(httpWebRequest, "AirSync");
			foreach (string text2 in this.proxyInfo.AdditionalHeaders.Keys)
			{
				httpWebRequest.Headers[text2] = this.proxyInfo.AdditionalHeaders[text2];
			}
			if (GlobalSettings.IsGCCEnabled)
			{
				if (GlobalSettings.AreGccStoredSecretKeysValid)
				{
					string text3 = httpWebRequest.Headers["x-gcc-proxyinfo"];
					if (string.IsNullOrEmpty(text3) || (!string.IsNullOrEmpty(text3) && !ProxyHandler.HasValidAuthString(text3)))
					{
						StringBuilder stringBuilder = new StringBuilder(GccUtils.GetAuthStringForThisServer());
						stringBuilder.Append(", ");
						stringBuilder.Append(clientrequest.ServerVariables["REMOTE_ADDR"]);
						stringBuilder.Append(", ");
						stringBuilder.Append(clientrequest.ServerVariables["LOCAL_ADDR"]);
						httpWebRequest.Headers["x-gcc-proxyinfo"] = stringBuilder.ToString();
					}
				}
				else
				{
					AirSyncDiagnostics.LogPeriodicEvent(AirSyncEventLogConstants.Tuple_NoGccStoredSecretKey, "NoGccStoredSecretKey", new string[0]);
					AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, null, "No gcc stored secret key");
				}
			}
			httpWebRequest.Referer = clientrequest.Url.AbsoluteUri;
			httpWebRequest.ContentType = clientrequest.ContentType;
			httpWebRequest.UserAgent = clientrequest.UserAgent;
			return httpWebRequest;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00053758 File Offset: 0x00051958
		private void ReleaseResources()
		{
			if (this.backEndRequestStream != null)
			{
				this.backEndRequestStream.Dispose();
				this.backEndRequestStream = null;
			}
			if (this.backEndResponseStream != null)
			{
				this.backEndResponseStream.Dispose();
				this.backEndResponseStream = null;
			}
			if (this.cachedRequestBody != null)
			{
				this.cachedRequestBody.Dispose();
				this.cachedRequestBody = null;
			}
			if (this.httpWebResponse != null)
			{
				this.httpWebResponse.Close();
				this.httpWebResponse = null;
			}
			this.TryUnregisterTimeoutWaitHandle();
			this.proxyInfo = null;
			this.frontEndResult = null;
			this.backEndWebRequest = null;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000537E8 File Offset: 0x000519E8
		private void RegisterTimeoutWaitHandle(IAsyncResult result, int timeout)
		{
			lock (this.timeoutWaitHandleLockObject)
			{
				if (this.timeoutWaitHandle != null)
				{
					this.TryUnregisterTimeoutWaitHandle();
				}
				using (ExecutionContext.SuppressFlow())
				{
					this.timeoutWaitHandle = ThreadPool.RegisterWaitForSingleObject(result.AsyncWaitHandle, new WaitOrTimerCallback(this.TimeoutCallback), result, timeout, true);
				}
			}
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00053874 File Offset: 0x00051A74
		private void TryUnregisterTimeoutWaitHandle()
		{
			try
			{
				this.UnregisterTimeoutWaitHandle();
			}
			catch (AirSyncPermanentException ex)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("AirSyncPermanentException: ");
				stringBuilder.Append(ex.Message);
				this.protocolLogger.SetValue(ProtocolLoggerData.Error, stringBuilder.ToString());
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000538D0 File Offset: 0x00051AD0
		private void UnregisterTimeoutWaitHandle()
		{
			lock (this.timeoutWaitHandleLockObject)
			{
				if (this.timeoutWaitHandle != null)
				{
					bool flag2 = this.timeoutWaitHandle.Unregister(null);
					this.timeoutWaitHandle = null;
					if (!flag2)
					{
						this.protocolLogger.SetValue(ProtocolLoggerData.Error, "UnregisterWaitHandleFailed");
						throw new AirSyncPermanentException(HttpStatusCode.ServiceUnavailable, StatusCode.ServerErrorRetryLater, null, false);
					}
				}
			}
		}

		// Token: 0x040009EE RID: 2542
		private const int AsyncBufferSize = 4096;

		// Token: 0x040009EF RID: 2543
		private const int TcpKeepAliveTime4Minutes = 240000;

		// Token: 0x040009F0 RID: 2544
		private const int TcpKeepAliveInterval = 5000;

		// Token: 0x040009F1 RID: 2545
		private const string GccHeader = "x-gcc-proxyinfo";

		// Token: 0x040009F2 RID: 2546
		private RegisteredWaitHandle timeoutWaitHandle;

		// Token: 0x040009F3 RID: 2547
		private object timeoutWaitHandleLockObject = new object();

		// Token: 0x040009F4 RID: 2548
		private ProxyHandler.ProxiedRequestInfo proxyInfo;

		// Token: 0x040009F5 RID: 2549
		private Uri remoteUri;

		// Token: 0x040009F6 RID: 2550
		private string userName;

		// Token: 0x040009F7 RID: 2551
		private int proxyLoginAttempts;

		// Token: 0x040009F8 RID: 2552
		private ProtocolLogger protocolLogger;

		// Token: 0x040009F9 RID: 2553
		private LazyAsyncResult frontEndResult;

		// Token: 0x040009FA RID: 2554
		private HttpContext frontEndHttpContext;

		// Token: 0x040009FB RID: 2555
		private Stream backEndRequestStream;

		// Token: 0x040009FC RID: 2556
		private WebRequest backEndWebRequest;

		// Token: 0x040009FD RID: 2557
		private HttpWebResponse httpWebResponse;

		// Token: 0x040009FE RID: 2558
		private Uri cachedUri;

		// Token: 0x040009FF RID: 2559
		private MemoryStream cachedRequestBody;

		// Token: 0x04000A00 RID: 2560
		private Stream clientRequestStream;

		// Token: 0x04000A01 RID: 2561
		private Stream backEndResponseStream;

		// Token: 0x04000A02 RID: 2562
		private byte[] asyncBuffer = new byte[4096];

		// Token: 0x04000A03 RID: 2563
		private bool requestWasTimedOut;

		// Token: 0x04000A04 RID: 2564
		private ExDateTime issueTime;

		// Token: 0x04000A05 RID: 2565
		private ProxyHandler.PendingOperationState pendingAsyncOperation;

		// Token: 0x02000112 RID: 274
		private enum PendingOperationState
		{
			// Token: 0x04000A07 RID: 2567
			Invalid,
			// Token: 0x04000A08 RID: 2568
			BackEndGetRequestStream,
			// Token: 0x04000A09 RID: 2569
			FrontEndReadContentFromClient,
			// Token: 0x04000A0A RID: 2570
			BackEndWriteContent,
			// Token: 0x04000A0B RID: 2571
			BackEndGetResponse,
			// Token: 0x04000A0C RID: 2572
			BackEndReadContent,
			// Token: 0x04000A0D RID: 2573
			ProxyLoginIssued,
			// Token: 0x04000A0E RID: 2574
			ProxyLoginWritingCsc,
			// Token: 0x04000A0F RID: 2575
			ProxyLoginGettingResponse
		}

		// Token: 0x02000113 RID: 275
		internal class ProxiedRequestInfo
		{
			// Token: 0x06000E9A RID: 3738 RVA: 0x0005394C File Offset: 0x00051B4C
			internal ProxiedRequestInfo(IAirSyncUser user, Uri remoteUri) : this(remoteUri)
			{
				this.user = user.Name;
				this.requiresImpersonation = true;
				this.attemptProxyLogin = true;
				this.cscbuf = Encoding.ASCII.GetBytes(user.ClientSecurityContextWrapper.SerializedSecurityContext.Serialize());
			}

			// Token: 0x06000E9B RID: 3739 RVA: 0x0005399A File Offset: 0x00051B9A
			internal ProxiedRequestInfo(string user, Uri remoteUri) : this(remoteUri)
			{
				this.user = user;
				this.attemptProxyLogin = false;
			}

			// Token: 0x06000E9C RID: 3740 RVA: 0x000539B1 File Offset: 0x00051BB1
			private ProxiedRequestInfo(Uri remoteUri)
			{
				this.remoteUri = remoteUri;
				this.additionalHeaders = new Dictionary<string, string>();
			}

			// Token: 0x1700059A RID: 1434
			// (get) Token: 0x06000E9D RID: 3741 RVA: 0x000539CB File Offset: 0x00051BCB
			internal byte[] CscBuffer
			{
				get
				{
					return this.cscbuf;
				}
			}

			// Token: 0x1700059B RID: 1435
			// (get) Token: 0x06000E9E RID: 3742 RVA: 0x000539D3 File Offset: 0x00051BD3
			internal string User
			{
				get
				{
					return this.user;
				}
			}

			// Token: 0x1700059C RID: 1436
			// (get) Token: 0x06000E9F RID: 3743 RVA: 0x000539DB File Offset: 0x00051BDB
			internal Uri RemoteUri
			{
				get
				{
					return this.remoteUri;
				}
			}

			// Token: 0x1700059D RID: 1437
			// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x000539E3 File Offset: 0x00051BE3
			// (set) Token: 0x06000EA1 RID: 3745 RVA: 0x000539EB File Offset: 0x00051BEB
			internal bool RequiresImpersonation
			{
				get
				{
					return this.requiresImpersonation;
				}
				set
				{
					this.requiresImpersonation = value;
				}
			}

			// Token: 0x1700059E RID: 1438
			// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x000539F4 File Offset: 0x00051BF4
			internal bool CanImpersonate
			{
				get
				{
					return this.cscbuf != null;
				}
			}

			// Token: 0x1700059F RID: 1439
			// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00053A02 File Offset: 0x00051C02
			internal bool AttemptProxyLogin
			{
				get
				{
					return this.attemptProxyLogin;
				}
			}

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00053A0A File Offset: 0x00051C0A
			internal Dictionary<string, string> AdditionalHeaders
			{
				get
				{
					return this.additionalHeaders;
				}
			}

			// Token: 0x04000A10 RID: 2576
			private string user;

			// Token: 0x04000A11 RID: 2577
			private Uri remoteUri;

			// Token: 0x04000A12 RID: 2578
			private bool requiresImpersonation;

			// Token: 0x04000A13 RID: 2579
			private Dictionary<string, string> additionalHeaders;

			// Token: 0x04000A14 RID: 2580
			private bool attemptProxyLogin;

			// Token: 0x04000A15 RID: 2581
			private byte[] cscbuf;
		}
	}
}
