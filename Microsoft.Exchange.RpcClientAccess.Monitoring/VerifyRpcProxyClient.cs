using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class VerifyRpcProxyClient : IVerifyRpcProxyClient
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00004AB8 File Offset: 0x00002CB8
		public VerifyRpcProxyClient(RpcBindingInfo bindingInfo)
		{
			this.bindingInfo = bindingInfo;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public IAsyncResult BeginVerifyRpcProxy(bool makeHangingRequest, AsyncCallback asyncCallback, object asyncState)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.bindingInfo.Uri);
			httpWebRequest.AllowAutoRedirect = false;
			httpWebRequest.Method = "RPC_IN_DATA";
			httpWebRequest.Accept = "application/rpc";
			httpWebRequest.UserAgent = "MSRPC";
			httpWebRequest.KeepAlive = false;
			httpWebRequest.ContentLength = (makeHangingRequest ? 2147483647L : 0L);
			httpWebRequest.Timeout = (int)this.bindingInfo.Timeout.TotalMilliseconds;
			httpWebRequest.Credentials = this.bindingInfo.Credential;
			httpWebRequest.PreAuthenticate = true;
			httpWebRequest.Headers.Set(HttpRequestHeader.Pragma, "no-cache");
			if (string.IsNullOrEmpty(this.bindingInfo.WebProxyServer) || this.bindingInfo.WebProxyServer == null)
			{
				httpWebRequest.Proxy = WebRequest.GetSystemWebProxy();
			}
			else if (this.bindingInfo.WebProxyServer == "<none>")
			{
				httpWebRequest.Proxy = VerifyRpcProxyClient.WebProxy.Bypass();
			}
			else
			{
				httpWebRequest.Proxy = VerifyRpcProxyClient.WebProxy.Fixed(this.bindingInfo.WebProxyServer);
			}
			httpWebRequest.CookieContainer = new CookieContainer();
			httpWebRequest.CookieContainer.Add(httpWebRequest.RequestUri, this.bindingInfo.RpcHttpCookies);
			httpWebRequest.Headers.Add(this.bindingInfo.RpcHttpHeaders);
			httpWebRequest.ConnectionGroupName = string.Format("VerifyRpcProxyClient_{0:G}_{1}", ExDateTime.UtcNow, Guid.NewGuid());
			return new VerifyRpcProxyClient.VerifyRpcProxyContext(httpWebRequest, this.bindingInfo.RpcProxyAuthentication, this.bindingInfo.IgnoreInvalidRpcProxyServerCertificateSubject, asyncCallback, asyncState).Begin();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004C60 File Offset: 0x00002E60
		public VerifyRpcProxyResult EndVerifyRpcProxy(IAsyncResult asyncResult)
		{
			VerifyRpcProxyClient.VerifyRpcProxyContext verifyRpcProxyContext = (VerifyRpcProxyClient.VerifyRpcProxyContext)asyncResult;
			return verifyRpcProxyContext.End(asyncResult);
		}

		// Token: 0x0400005C RID: 92
		private readonly RpcBindingInfo bindingInfo;

		// Token: 0x0200002C RID: 44
		private class VerifyRpcProxyContext : ClientCallContext<VerifyRpcProxyResult>
		{
			// Token: 0x0600011E RID: 286 RVA: 0x00004C7B File Offset: 0x00002E7B
			static VerifyRpcProxyContext()
			{
				CertificateValidationManager.RegisterCallback("VerifyRpcProxy.Strict", VerifyRpcProxyClient.VerifyRpcProxyContext.GetCertificateValidationCallback(SslPolicyErrors.None));
				CertificateValidationManager.RegisterCallback("VerifyRpcProxy.Relaxed", VerifyRpcProxyClient.VerifyRpcProxyContext.GetCertificateValidationCallback(SslPolicyErrors.RemoteCertificateNameMismatch));
			}

			// Token: 0x0600011F RID: 287 RVA: 0x00004CE8 File Offset: 0x00002EE8
			public VerifyRpcProxyContext(HttpWebRequest request, HttpAuthenticationScheme rpcProxyAuthentication, bool ignoreSubjectMismatch, AsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
			{
				VerifyRpcProxyClient.VerifyRpcProxyContext <>4__this = this;
				Util.ThrowOnNullArgument(request, "request");
				this.HttpRequest = request;
				CertificateValidationManager.SetComponentId(request, ignoreSubjectMismatch ? "VerifyRpcProxy.Relaxed" : "VerifyRpcProxy.Strict");
				if (this.HttpRequest.Credentials != null)
				{
					this.HttpRequest.Credentials = new RestrictedCredentials(this.HttpRequest.Credentials, delegate(string requestedAuthType)
					{
						<>4__this.requestedRpcProxyAuthenticationTypes.Add(requestedAuthType);
						return StringComparer.OrdinalIgnoreCase.Equals(requestedAuthType, RpcHelper.HttpAuthenticationSchemeMapping.Get(rpcProxyAuthentication));
					});
				}
				else
				{
					this.HttpRequest.UseDefaultCredentials = true;
				}
				if (this.HttpRequest.ContentLength > 0L)
				{
					this.HttpRequest.SendChunked = true;
					this.requestStream = this.HttpRequest.GetRequestStream();
				}
				this.timer = new Timer(new TimerCallback(this.TimeoutCallback), this.HttpRequest, this.HttpRequest.Timeout, -1);
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000120 RID: 288 RVA: 0x00004DE3 File Offset: 0x00002FE3
			// (set) Token: 0x06000121 RID: 289 RVA: 0x00004DEB File Offset: 0x00002FEB
			private HttpWebRequest HttpRequest { get; set; }

			// Token: 0x06000122 RID: 290 RVA: 0x00004DF4 File Offset: 0x00002FF4
			public VerifyRpcProxyResult End(IAsyncResult asyncResult)
			{
				return base.GetResult();
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00004DFC File Offset: 0x00002FFC
			protected override IAsyncResult OnBegin(AsyncCallback asyncCallback, object asyncState)
			{
				IAsyncResult result = this.HttpRequest.BeginGetResponse(asyncCallback, asyncState);
				this.completedTimer = 0;
				return result;
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00004E20 File Offset: 0x00003020
			protected override VerifyRpcProxyResult OnEnd(IAsyncResult asyncResult)
			{
				HttpWebResponse httpWebResponse = null;
				VerifyRpcProxyResult result;
				try
				{
					httpWebResponse = (HttpWebResponse)this.HttpRequest.EndGetResponse(asyncResult);
					result = VerifyRpcProxyResult.CreateSuccessfulResult(httpWebResponse);
				}
				finally
				{
					this.TimerCleanup();
					Util.DisposeIfPresent(this.requestStream);
					Util.DisposeIfPresent(httpWebResponse);
				}
				return result;
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00004E74 File Offset: 0x00003074
			protected override VerifyRpcProxyResult ConvertExceptionToResult(Exception exception)
			{
				WebException ex = exception as WebException;
				if (ex != null)
				{
					try
					{
						return VerifyRpcProxyResult.CreateFailureResult((HttpWebResponse)ex.Response, ex);
					}
					finally
					{
						Util.DisposeIfPresent(ex.Response);
					}
				}
				return null;
			}

			// Token: 0x06000126 RID: 294 RVA: 0x00004EC0 File Offset: 0x000030C0
			protected override VerifyRpcProxyResult PostProcessResult(VerifyRpcProxyResult result)
			{
				result.RpcProxyUrl = this.HttpRequest.RequestUri.ToString();
				result.RequestedRpcProxyAuthenticationTypes = this.requestedRpcProxyAuthenticationTypes.ToArray();
				this.HttpRequest.ServicePoint.CloseConnectionGroup(this.HttpRequest.ConnectionGroupName);
				lock (VerifyRpcProxyClient.VerifyRpcProxyContext.sslErrorsLock)
				{
					CertificateValidationError serverCertificateValidationError;
					if (VerifyRpcProxyClient.VerifyRpcProxyContext.sslErrors.TryGetValue(this.HttpRequest, out serverCertificateValidationError))
					{
						result.ServerCertificateValidationError = serverCertificateValidationError;
						VerifyRpcProxyClient.VerifyRpcProxyContext.sslErrors.Remove(this.HttpRequest);
					}
				}
				return base.PostProcessResult(result);
			}

			// Token: 0x06000127 RID: 295 RVA: 0x00004FF0 File Offset: 0x000031F0
			private static RemoteCertificateValidationCallback GetCertificateValidationCallback(SslPolicyErrors errorsToIgnore)
			{
				return delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
				{
					HttpWebRequest httpWebRequest = sender as HttpWebRequest;
					if (httpWebRequest != null)
					{
						lock (VerifyRpcProxyClient.VerifyRpcProxyContext.sslErrorsLock)
						{
							VerifyRpcProxyClient.VerifyRpcProxyContext.sslErrors[httpWebRequest] = new CertificateValidationError
							{
								Certificate = certificate,
								Chain = chain,
								SslPolicyErrors = sslPolicyErrors
							};
						}
					}
					return (sslPolicyErrors & ~errorsToIgnore) == SslPolicyErrors.None;
				};
			}

			// Token: 0x06000128 RID: 296 RVA: 0x00005016 File Offset: 0x00003216
			private void TimeoutCallback(object state)
			{
				if (Interlocked.CompareExchange(ref this.completedTimer, 1, 0) == 0)
				{
					this.HttpRequest.Abort();
					this.TimerCleanup();
				}
			}

			// Token: 0x06000129 RID: 297 RVA: 0x00005038 File Offset: 0x00003238
			private void TimerCleanup()
			{
				Timer timer = Interlocked.Exchange<Timer>(ref this.timer, null);
				if (timer != null)
				{
					timer.Change(-1, -1);
					timer.Dispose();
				}
			}

			// Token: 0x0400005D RID: 93
			private const string StrictVerificationKey = "VerifyRpcProxy.Strict";

			// Token: 0x0400005E RID: 94
			private const string RelaxedVerificationKey = "VerifyRpcProxy.Relaxed";

			// Token: 0x0400005F RID: 95
			private static readonly IDictionary<HttpWebRequest, CertificateValidationError> sslErrors = new Dictionary<HttpWebRequest, CertificateValidationError>();

			// Token: 0x04000060 RID: 96
			private static readonly object sslErrorsLock = new object();

			// Token: 0x04000061 RID: 97
			private readonly List<string> requestedRpcProxyAuthenticationTypes = new List<string>();

			// Token: 0x04000062 RID: 98
			private Stream requestStream;

			// Token: 0x04000063 RID: 99
			private Timer timer;

			// Token: 0x04000064 RID: 100
			private int completedTimer;
		}

		// Token: 0x0200002D RID: 45
		private class WebProxy : IWebProxy
		{
			// Token: 0x0600012A RID: 298 RVA: 0x00005064 File Offset: 0x00003264
			private WebProxy(Uri proxy)
			{
				this.proxy = proxy;
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600012B RID: 299 RVA: 0x00005073 File Offset: 0x00003273
			// (set) Token: 0x0600012C RID: 300 RVA: 0x0000507B File Offset: 0x0000327B
			ICredentials IWebProxy.Credentials { get; set; }

			// Token: 0x0600012D RID: 301 RVA: 0x00005084 File Offset: 0x00003284
			public static IWebProxy Bypass()
			{
				return new VerifyRpcProxyClient.WebProxy(null);
			}

			// Token: 0x0600012E RID: 302 RVA: 0x0000508C File Offset: 0x0000328C
			public static IWebProxy Fixed(string proxyAndOptionalPort)
			{
				UriBuilder uriBuilder = new UriBuilder();
				string[] array = proxyAndOptionalPort.Split(new char[]
				{
					':'
				});
				int num;
				if (array.Length == 2 && int.TryParse(array[1], out num))
				{
					uriBuilder.Scheme = (RpcHelper.DetectShouldUseSsl((RpcProxyPort)num) ? Uri.UriSchemeHttps : Uri.UriSchemeHttp);
					uriBuilder.Host = array[0];
					uriBuilder.Port = num;
				}
				else
				{
					uriBuilder.Scheme = Uri.UriSchemeHttp;
					uriBuilder.Host = proxyAndOptionalPort;
				}
				return new VerifyRpcProxyClient.WebProxy(uriBuilder.Uri);
			}

			// Token: 0x0600012F RID: 303 RVA: 0x0000510E File Offset: 0x0000330E
			Uri IWebProxy.GetProxy(Uri destination)
			{
				return this.proxy;
			}

			// Token: 0x06000130 RID: 304 RVA: 0x00005116 File Offset: 0x00003316
			bool IWebProxy.IsBypassed(Uri host)
			{
				return this.proxy == null;
			}

			// Token: 0x04000066 RID: 102
			private readonly Uri proxy;
		}
	}
}
