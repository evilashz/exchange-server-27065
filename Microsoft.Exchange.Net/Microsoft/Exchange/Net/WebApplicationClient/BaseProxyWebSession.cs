using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Web;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B15 RID: 2837
	internal abstract class BaseProxyWebSession : AuthenticateWebSession
	{
		// Token: 0x06003D43 RID: 15683 RVA: 0x0009F804 File Offset: 0x0009DA04
		public BaseProxyWebSession(Uri serviceUrl) : base(serviceUrl, CredentialCache.DefaultNetworkCredentials)
		{
		}

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x0009F8CD File Offset: 0x0009DACD
		protected HashSet<string> AllowedRequestCookies
		{
			get
			{
				return this.allowedRequestCookies;
			}
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x0009F8D5 File Offset: 0x0009DAD5
		protected HashSet<string> AllowedResponseCookies
		{
			get
			{
				return this.allowedResponseCookies;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x0009F8DD File Offset: 0x0009DADD
		protected HashSet<string> BlockedRequestHeaders
		{
			get
			{
				return this.blockedRequestHeaders;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x0009F8E5 File Offset: 0x0009DAE5
		protected HashSet<string> BlockedResposeHeaders
		{
			get
			{
				return this.blockedResposeHeaders;
			}
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x0009F8F0 File Offset: 0x0009DAF0
		protected virtual HttpWebRequest CreateProxyRequest(HttpContext context)
		{
			HttpRequest request = context.Request;
			Uri uri = new Uri(base.ServiceAuthority, request.Url.PathAndQuery);
			HttpWebRequest httpWebRequest = base.CreateRequest(uri, request.RequestType);
			for (int i = 0; i < request.Headers.Count; i++)
			{
				string text = request.Headers.Keys[i];
				if (!WebHeaderCollection.IsRestricted(text, false) && !this.blockedRequestHeaders.Contains(text))
				{
					httpWebRequest.Headers[text] = request.Headers[text];
				}
			}
			httpWebRequest.UserAgent = request.UserAgent;
			string text2 = request.Headers["referer"];
			if (text2 != null)
			{
				httpWebRequest.Referer = text2;
			}
			text2 = request.Headers["accept"];
			if (text2 != null)
			{
				httpWebRequest.Accept = text2;
			}
			httpWebRequest.CookieContainer = new CookieContainer();
			foreach (string name in this.AllowedRequestCookies)
			{
				HttpCookie httpCookie = context.Request.Cookies[name];
				if (httpCookie != null)
				{
					Cookie cookie = new Cookie(httpCookie.Name, httpCookie.Value)
					{
						Domain = uri.Host,
						Expires = httpCookie.Expires,
						HttpOnly = httpCookie.HttpOnly,
						Path = httpCookie.Path,
						Secure = httpCookie.Secure
					};
					httpWebRequest.CookieContainer.Add(cookie);
				}
			}
			httpWebRequest.AllowAutoRedirect = false;
			httpWebRequest.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
			httpWebRequest.AuthenticationLevel = AuthenticationLevel.None;
			httpWebRequest.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
			httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
			httpWebRequest.PreAuthenticate = true;
			return httpWebRequest;
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x0009FAD4 File Offset: 0x0009DCD4
		protected override void Authenticate(HttpWebRequest request)
		{
			request.Credentials = base.Credentials.GetCredential(base.ServiceAuthority, "Kerberos");
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x0009FAF4 File Offset: 0x0009DCF4
		protected virtual RequestBody CreateProxyRequestBody(HttpContext context)
		{
			HttpRequest request = context.Request;
			if (request.ContentLength > 0)
			{
				if (request.InputStream.CanSeek && request.InputStream.Position > 0L)
				{
					request.InputStream.Position = 0L;
				}
				return new StreamBody(request.InputStream, request.ContentType);
			}
			return null;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x0009FBB4 File Offset: 0x0009DDB4
		public void SendProxyRequest(HttpContext context, Action onSuccess, Action<HttpContext, HttpWebResponse, Exception> onFailure)
		{
			HttpWebRequest proxyRequest = this.CreateProxyRequest(context);
			RequestBody requestBody = this.CreateProxyRequestBody(context);
			this.OnSendingProxyRequest(context, proxyRequest);
			base.Send<BaseProxyWebSession>(proxyRequest, requestBody, delegate(HttpWebResponse proxyResponse)
			{
				this.ProcessProxyResponse(context, proxyResponse);
				return this;
			}, delegate(BaseProxyWebSession responseData)
			{
				this.ReportProxySucceeded(context, proxyRequest, onSuccess);
			}, delegate(Exception exception)
			{
				this.HandleProxyFailure(context, proxyRequest, exception, onSuccess, onFailure);
			});
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x0009FC48 File Offset: 0x0009DE48
		private void HandleProxyFailure(HttpContext context, HttpWebRequest proxyRequest, Exception exception, Action onSuccess, Action<HttpContext, HttpWebResponse, Exception> onFailure)
		{
			HttpWebResponse responseFromException = WebSession.GetResponseFromException(exception);
			if (responseFromException != null && this.ShouldProcessFailedResponse((WebException)exception))
			{
				try
				{
					this.ProcessProxyResponse(context, responseFromException);
				}
				catch (Exception exception2)
				{
					this.ReportProxyFailure(context, proxyRequest, responseFromException, exception2, onFailure);
					return;
				}
				this.ReportProxySucceeded(context, proxyRequest, onSuccess);
				return;
			}
			this.ReportProxyFailure(context, proxyRequest, responseFromException, exception, onFailure);
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x0009FCB0 File Offset: 0x0009DEB0
		protected virtual bool ShouldProcessFailedResponse(WebException exception)
		{
			switch (exception.GetTroubleshootingID())
			{
			case WebExceptionTroubleshootingID.TrustFailure:
			case WebExceptionTroubleshootingID.ServiceUnavailable:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x0009FCDC File Offset: 0x0009DEDC
		private void ReportProxySucceeded(HttpContext context, HttpWebRequest proxyRequest, Action onSuccess)
		{
			try
			{
				this.OnProxyRequestSucceeded(context, proxyRequest);
			}
			finally
			{
				onSuccess();
			}
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x0009FD0C File Offset: 0x0009DF0C
		private void ReportProxyFailure(HttpContext context, HttpWebRequest proxyRequest, HttpWebResponse response, Exception exception, Action<HttpContext, HttpWebResponse, Exception> onFailure)
		{
			try
			{
				this.OnProxyRequestFailed(context, proxyRequest, response, exception);
			}
			finally
			{
				onFailure(context, response, exception);
			}
		}

		// Token: 0x06003D50 RID: 15696
		protected abstract void OnSendingProxyRequest(HttpContext context, HttpWebRequest request);

		// Token: 0x06003D51 RID: 15697
		protected abstract void OnProxyRequestSucceeded(HttpContext context, HttpWebRequest request);

		// Token: 0x06003D52 RID: 15698
		protected abstract void OnProxyRequestFailed(HttpContext context, HttpWebRequest request, HttpWebResponse response, Exception exception);

		// Token: 0x06003D53 RID: 15699 RVA: 0x0009FD44 File Offset: 0x0009DF44
		protected virtual void ProcessProxyResponse(HttpContext context, HttpWebResponse proxyResponse)
		{
			HttpResponse response = context.Response;
			response.TrySkipIisCustomErrors = true;
			response.ContentType = proxyResponse.ContentType;
			response.StatusCode = (int)proxyResponse.StatusCode;
			for (int i = 0; i < proxyResponse.Headers.Count; i++)
			{
				string text = proxyResponse.Headers.Keys[i];
				if (!WebHeaderCollection.IsRestricted(text, true) && !this.blockedResposeHeaders.Contains(text))
				{
					response.AddHeader(text, proxyResponse.Headers[text]);
				}
			}
			foreach (string name in this.AllowedResponseCookies)
			{
				Cookie cookie = proxyResponse.Cookies[name];
				if (cookie != null)
				{
					HttpCookie cookie2 = new HttpCookie(cookie.Name, cookie.Value)
					{
						Expires = cookie.Expires,
						HttpOnly = cookie.HttpOnly,
						Path = cookie.Path,
						Secure = cookie.Secure
					};
					context.Response.Cookies.Add(cookie2);
				}
			}
			using (Stream responseStream = proxyResponse.GetResponseStream())
			{
				if (responseStream != null)
				{
					responseStream.CopyTo(response.OutputStream);
				}
			}
		}

		// Token: 0x04003585 RID: 13701
		private HashSet<string> allowedRequestCookies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003586 RID: 13702
		private HashSet<string> allowedResponseCookies = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04003587 RID: 13703
		private HashSet<string> blockedRequestHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"accept-encoding",
			"cookie",
			"authorization",
			"proxy-authorization"
		};

		// Token: 0x04003588 RID: 13704
		private HashSet<string> blockedResposeHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"set-cookie",
			"server",
			"x-powered-by",
			"x-aspnet-version",
			"www-authenticate"
		};
	}
}
