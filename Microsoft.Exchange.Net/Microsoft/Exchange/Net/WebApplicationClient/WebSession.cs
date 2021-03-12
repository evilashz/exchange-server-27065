using System;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B13 RID: 2835
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public abstract class WebSession
	{
		// Token: 0x06003D18 RID: 15640 RVA: 0x0009F00C File Offset: 0x0009D20C
		protected WebSession(Uri loginUrl, NetworkCredential credentials)
		{
			if (null == loginUrl)
			{
				throw new ArgumentNullException("loginUrl");
			}
			if (credentials == null)
			{
				throw new ArgumentNullException("credentials");
			}
			if (!loginUrl.IsAbsoluteUri)
			{
				throw new ArgumentOutOfRangeException("loginUrl");
			}
			this.ServiceAuthority = loginUrl;
			this.Credentials = credentials;
			this.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0)";
			this.SessionCookies = new WebSessionCookieContainer(this);
		}

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x0009F079 File Offset: 0x0009D279
		// (set) Token: 0x06003D1A RID: 15642 RVA: 0x0009F081 File Offset: 0x0009D281
		public NetworkCredential Credentials { get; private set; }

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x0009F08A File Offset: 0x0009D28A
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x0009F092 File Offset: 0x0009D292
		public Uri ServiceAuthority { get; protected set; }

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x0009F09B File Offset: 0x0009D29B
		// (set) Token: 0x06003D1E RID: 15646 RVA: 0x0009F0A3 File Offset: 0x0009D2A3
		public string UserAgent { get; set; }

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x0009F0AC File Offset: 0x0009D2AC
		// (set) Token: 0x06003D20 RID: 15648 RVA: 0x0009F0B4 File Offset: 0x0009D2B4
		protected CookieContainer SessionCookies { get; set; }

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x0009F0BD File Offset: 0x0009D2BD
		// (set) Token: 0x06003D22 RID: 15650 RVA: 0x0009F0C5 File Offset: 0x0009D2C5
		public bool TrustAnySSLCertificate { get; set; }

		// Token: 0x06003D23 RID: 15651 RVA: 0x0009F0D0 File Offset: 0x0009D2D0
		public void AddCookie(Cookie cookie)
		{
			lock (this.SessionCookies)
			{
				this.SessionCookies.Add(this.ServiceAuthority, cookie);
			}
		}

		// Token: 0x06003D24 RID: 15652 RVA: 0x0009F11C File Offset: 0x0009D31C
		public CookieCollection GetCookies(Uri uri)
		{
			CookieCollection cookies;
			lock (this.SessionCookies)
			{
				cookies = this.SessionCookies.GetCookies(uri);
			}
			return cookies;
		}

		// Token: 0x06003D25 RID: 15653
		public abstract void Initialize();

		// Token: 0x06003D26 RID: 15654
		protected abstract void Authenticate(HttpWebRequest request);

		// Token: 0x06003D27 RID: 15655 RVA: 0x0009F164 File Offset: 0x0009D364
		public T Get<T>(Uri requestUri, Func<HttpWebResponse, T> processResponse)
		{
			HttpWebRequest request = this.CreateRequest(requestUri, "GET");
			return this.Send<T>(request, null, processResponse);
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x0009F188 File Offset: 0x0009D388
		public void Get<T>(Uri requestUri, Func<HttpWebResponse, T> processResponse, Action<T> onSuccess, Action<Exception> onFailure)
		{
			HttpWebRequest request = this.CreateRequest(requestUri, "GET");
			this.Send<T>(request, null, processResponse, onSuccess, onFailure);
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x0009F1B0 File Offset: 0x0009D3B0
		public T Post<T>(Uri requestUri, RequestBody requestBody, Func<HttpWebResponse, T> processResponse)
		{
			HttpWebRequest request = this.CreateRequest(requestUri, "POST");
			return this.Send<T>(request, requestBody, processResponse);
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x0009F1D4 File Offset: 0x0009D3D4
		public void Post<T>(Uri requestUri, RequestBody requestBody, Func<HttpWebResponse, T> processResponse, Action<T> onSuccess, Action<Exception> onFailure)
		{
			HttpWebRequest request = this.CreateRequest(requestUri, "POST");
			this.Send<T>(request, requestBody, processResponse, onSuccess, onFailure);
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x0009F1FC File Offset: 0x0009D3FC
		protected HttpWebRequest CreateRequest(Uri requestUri, string method)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
			httpWebRequest.Method = method;
			httpWebRequest.AllowAutoRedirect = false;
			httpWebRequest.UserAgent = this.UserAgent;
			httpWebRequest.Accept = "*/*";
			httpWebRequest.CachePolicy = WebSession.DefaultCachePolicy;
			return httpWebRequest;
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x0009F246 File Offset: 0x0009D446
		public T Send<T>(HttpWebRequest request, RequestBody requestBody, Func<HttpWebResponse, T> processResponse)
		{
			return this.EndSend<T>(this.BeginSend(request, requestBody, null, null), processResponse);
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x0009F2B4 File Offset: 0x0009D4B4
		public void Send<T>(HttpWebRequest request, RequestBody requestBody, Func<HttpWebResponse, T> processResponse, Action<T> onSuccess, Action<Exception> onFailure)
		{
			if (onSuccess == null)
			{
				throw new ArgumentNullException("onSuccess");
			}
			if (onFailure == null)
			{
				throw new ArgumentNullException("onFailure");
			}
			this.BeginSend(request, requestBody, delegate(IAsyncResult asyncResults)
			{
				T obj;
				try
				{
					obj = this.EndSend<T>(asyncResults, processResponse);
				}
				catch (Exception obj2)
				{
					onFailure(obj2);
					return;
				}
				onSuccess(obj);
			}, null);
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x0009F321 File Offset: 0x0009D521
		public IAsyncResult BeginSend(HttpWebRequest request, RequestBody requestBody, AsyncCallback callback, object asyncState)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.OnSendingRequest(request);
			this.Authenticate(request);
			this.SetupCertificateValidation(request);
			return new SendRequestOperation(request, requestBody, callback, asyncState);
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x0009F350 File Offset: 0x0009D550
		public T EndSend<T>(IAsyncResult results, Func<HttpWebResponse, T> processResponse)
		{
			if (processResponse == null)
			{
				throw new ArgumentNullException("processResponse");
			}
			T result;
			using (HttpWebResponse response = this.GetResponse(results))
			{
				result = processResponse(response);
			}
			return result;
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x0009F398 File Offset: 0x0009D598
		private HttpWebResponse GetResponse(IAsyncResult results)
		{
			SendRequestOperation sendRequestOperation = (SendRequestOperation)results;
			sendRequestOperation.AsyncWaitHandle.WaitOne();
			if (sendRequestOperation.Response != null)
			{
				lock (this.SessionCookies)
				{
					foreach (object obj in sendRequestOperation.Response.Cookies)
					{
						Cookie cookie = (Cookie)obj;
						try
						{
							cookie.Domain = cookie.Domain.TrimStart(new char[]
							{
								'.'
							});
							this.SessionCookies.Add(cookie);
						}
						catch (CookieException)
						{
						}
						try
						{
							cookie.Domain = '.' + cookie.Domain;
							this.SessionCookies.Add(cookie);
						}
						catch (CookieException)
						{
						}
					}
				}
			}
			this.UpdatePerformanceCounters(new RequestPerformance(sendRequestOperation));
			if (sendRequestOperation.Exception != null)
			{
				if (sendRequestOperation.Exception is WebException)
				{
					this.OnRequestException(sendRequestOperation.Request, sendRequestOperation.Exception as WebException);
				}
				throw sendRequestOperation.Exception;
			}
			this.OnResponseReceived(sendRequestOperation.Request, sendRequestOperation.Response);
			return sendRequestOperation.Response;
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x0009F508 File Offset: 0x0009D708
		protected internal void SetupCertificateValidation(HttpWebRequest request)
		{
			lock (this.SessionCookies)
			{
				WebSessionCookieContainer webSessionCookieContainer = new WebSessionCookieContainer(this);
				if (request.CookieContainer != null)
				{
					webSessionCookieContainer.Add(request.CookieContainer.GetCookies(request.RequestUri));
				}
				webSessionCookieContainer.Add(this.SessionCookies.GetCookies(request.RequestUri));
				request.CookieContainer = webSessionCookieContainer;
			}
			CertificateValidationManager.SetComponentId(request, "WebSession");
		}

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x06003D32 RID: 15666 RVA: 0x0009F594 File Offset: 0x0009D794
		// (remove) Token: 0x06003D33 RID: 15667 RVA: 0x0009F5CC File Offset: 0x0009D7CC
		public event EventHandler<HttpWebRequestEventArgs> SendingRequest;

		// Token: 0x06003D34 RID: 15668 RVA: 0x0009F604 File Offset: 0x0009D804
		protected virtual void OnSendingRequest(HttpWebRequest request)
		{
			EventHandler<HttpWebRequestEventArgs> sendingRequest = this.SendingRequest;
			if (sendingRequest != null)
			{
				sendingRequest(this, new HttpWebRequestEventArgs(request));
			}
		}

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06003D35 RID: 15669 RVA: 0x0009F628 File Offset: 0x0009D828
		// (remove) Token: 0x06003D36 RID: 15670 RVA: 0x0009F660 File Offset: 0x0009D860
		public event EventHandler<HttpWebResponseEventArgs> ResponseReceived;

		// Token: 0x06003D37 RID: 15671 RVA: 0x0009F698 File Offset: 0x0009D898
		protected virtual void OnResponseReceived(HttpWebRequest request, HttpWebResponse response)
		{
			EventHandler<HttpWebResponseEventArgs> responseReceived = this.ResponseReceived;
			if (responseReceived != null)
			{
				responseReceived(this, new HttpWebResponseEventArgs(request, response));
			}
		}

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06003D38 RID: 15672 RVA: 0x0009F6C0 File Offset: 0x0009D8C0
		// (remove) Token: 0x06003D39 RID: 15673 RVA: 0x0009F6F8 File Offset: 0x0009D8F8
		public event EventHandler<WebExceptionEventArgs> RequestException;

		// Token: 0x06003D3A RID: 15674 RVA: 0x0009F730 File Offset: 0x0009D930
		protected virtual void OnRequestException(HttpWebRequest request, WebException exception)
		{
			EventHandler<WebExceptionEventArgs> requestException = this.RequestException;
			if (requestException != null)
			{
				requestException(this, new WebExceptionEventArgs(request, exception));
			}
		}

		// Token: 0x06003D3B RID: 15675 RVA: 0x0009F755 File Offset: 0x0009D955
		protected virtual void UpdatePerformanceCounters(RequestPerformance requestPerformance)
		{
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x0009F757 File Offset: 0x0009D957
		static WebSession()
		{
			CertificateValidationManager.RegisterCallback("WebSession", new RemoteCertificateValidationCallback(WebSession.ServerCertificateValidator));
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x0009F77C File Offset: 0x0009D97C
		private static WebSession FromRequest(HttpWebRequest request)
		{
			WebSessionCookieContainer webSessionCookieContainer = (WebSessionCookieContainer)request.CookieContainer;
			return webSessionCookieContainer.WebSession;
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x0009F79C File Offset: 0x0009D99C
		private static bool ServerCertificateValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			WebSession webSession = WebSession.FromRequest((HttpWebRequest)sender);
			return webSession.TrustAnySSLCertificate || sslPolicyErrors == SslPolicyErrors.None;
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x0009F7C4 File Offset: 0x0009D9C4
		public static HttpWebResponse GetResponseFromException(Exception exception)
		{
			WebException ex = exception as WebException;
			if (ex != null)
			{
				return ex.Response as HttpWebResponse;
			}
			return null;
		}

		// Token: 0x04003579 RID: 13689
		private const string CertificateValidationComponentId = "WebSession";

		// Token: 0x0400357A RID: 13690
		private const string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0)";

		// Token: 0x0400357B RID: 13691
		private const string DefaultAccept = "*/*";

		// Token: 0x0400357C RID: 13692
		private static readonly RequestCachePolicy DefaultCachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
	}
}
