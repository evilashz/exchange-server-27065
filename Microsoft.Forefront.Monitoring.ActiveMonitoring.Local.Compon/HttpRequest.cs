using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Forefront.Reporting.Security;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000042 RID: 66
	public class HttpRequest : IHttpRequest
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0000B6C0 File Offset: 0x000098C0
		public HttpRequest()
		{
			this.userAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Active Monitoring)";
			this.acceptLanguage = "en-us";
			this.cookies = new CookieContainer();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000B6E9 File Offset: 0x000098E9
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x0000B6F1 File Offset: 0x000098F1
		public string AcceptLanguage
		{
			get
			{
				return this.acceptLanguage;
			}
			set
			{
				this.acceptLanguage = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000B6FA File Offset: 0x000098FA
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000B702 File Offset: 0x00009902
		public string UserAgent
		{
			get
			{
				return this.userAgent;
			}
			set
			{
				this.userAgent = value;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000B70B File Offset: 0x0000990B
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x0000B713 File Offset: 0x00009913
		public ProcessCookies ProcessCookies
		{
			get
			{
				return this.processCookies;
			}
			set
			{
				this.processCookies = value;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B71C File Offset: 0x0000991C
		public ServerResponse SendGetRequest(string uri, bool sslValidation, string componentId, bool allowRedirects = true, int timeout = 0, string authenticationType = null, string authenticationUser = null, string authenticationPassword = null, Dictionary<string, string> properties = null)
		{
			HttpWebRequest httpWebRequest = this.CreateRequest(uri, allowRedirects, timeout);
			httpWebRequest.Method = "GET";
			if (!sslValidation)
			{
				CertificateValidationManager.SetComponentId(httpWebRequest, componentId);
			}
			AuthenticatorSettings settings = new AuthenticatorSettings(authenticationUser, authenticationPassword, properties);
			HttpRequest.AuthenticateHttpWebRequest(authenticationType, httpWebRequest, settings);
			return this.GetServerResponse(httpWebRequest);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B768 File Offset: 0x00009968
		public ServerResponse SendPostRequest(string uri, bool allowRedirects, bool getHiddenInputValues, ref PostData postData, string contentType, string formName = null, int timeout = 0)
		{
			if (getHiddenInputValues)
			{
				ServerResponse serverResponse = this.SendGetRequest(uri, true, string.Empty, allowRedirects, timeout, null, null, null, null);
				string tagInnerHtml = HtmlUtility.GetTagInnerHtml("form", serverResponse.Text, formName);
				if (!string.IsNullOrEmpty(tagInnerHtml))
				{
					Dictionary<string, string> hiddenFormInputs = HtmlUtility.GetHiddenFormInputs(tagInnerHtml);
					if (hiddenFormInputs.Count > 0)
					{
						if (postData == null)
						{
							postData = new PostData(hiddenFormInputs);
						}
						else
						{
							postData.AddRange(hiddenFormInputs);
						}
					}
				}
				if (allowRedirects)
				{
					uri = serverResponse.Uri.AbsoluteUri;
				}
			}
			HttpWebRequest httpWebRequest = this.CreateRequest(uri, allowRedirects, timeout);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = contentType;
			if (postData != null && postData.Count > 0)
			{
				using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
				{
					string value = postData.ToString(contentType.Equals("application/x-www-form-urlencoded", StringComparison.InvariantCultureIgnoreCase));
					streamWriter.Write(value);
					streamWriter.Close();
					goto IL_E4;
				}
			}
			httpWebRequest.ContentLength = 0L;
			IL_E4:
			return this.GetServerResponse(httpWebRequest);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B870 File Offset: 0x00009A70
		private static void AuthenticateHttpWebRequest(string authenticationType, HttpWebRequest request, AuthenticatorSettings settings)
		{
			if (!string.IsNullOrEmpty(authenticationType))
			{
				Type type = Type.GetType(authenticationType);
				if (null == type)
				{
					throw new ArgumentException("Invalid HttpAuthenticationType is specified");
				}
				IAuthenticator authenticator = (IAuthenticator)Activator.CreateInstance(type);
				authenticator.PrepareRequest(request, settings);
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		private static HttpWebResponse GetHttpWebResponse(HttpWebRequest request)
		{
			HttpWebResponse httpWebResponse;
			try
			{
				httpWebResponse = (HttpWebResponse)request.GetResponse();
			}
			catch (WebException ex)
			{
				httpWebResponse = (ex.Response as HttpWebResponse);
				if (httpWebResponse == null)
				{
					throw;
				}
			}
			return httpWebResponse;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		private HttpWebRequest CreateRequest(string uri, bool allowRedirects, int timeout)
		{
			HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
			httpWebRequest.CookieContainer = this.cookies;
			httpWebRequest.UserAgent = this.userAgent;
			httpWebRequest.AllowAutoRedirect = allowRedirects;
			httpWebRequest.Headers.Add("Accept-Language", this.acceptLanguage);
			httpWebRequest.KeepAlive = true;
			if (timeout > 0)
			{
				httpWebRequest.Timeout = timeout;
			}
			return httpWebRequest;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000B958 File Offset: 0x00009B58
		private void PersistCookies(CookieContainer cookieContainer, Uri uri)
		{
			switch (this.processCookies)
			{
			case ProcessCookies.None:
				this.cookies = new CookieContainer();
				return;
			case ProcessCookies.SessionOnly:
			{
				this.cookies = new CookieContainer();
				if (cookieContainer.Count <= 0)
				{
					return;
				}
				CookieCollection cookieCollection = cookieContainer.GetCookies(uri);
				using (IEnumerator enumerator = cookieCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Cookie cookie = (Cookie)obj;
						if (cookie.Expires < DateTime.UtcNow)
						{
							this.cookies.Add(cookie);
						}
					}
					return;
				}
				break;
			}
			}
			this.cookies = cookieContainer;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000BA10 File Offset: 0x00009C10
		private ServerResponse GetServerResponse(HttpWebRequest request)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			ServerResponse result;
			using (HttpWebResponse httpWebResponse = HttpRequest.GetHttpWebResponse(request))
			{
				this.PersistCookies(request.CookieContainer, request.Address);
				string text = string.Empty;
				using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
				{
					text = streamReader.ReadToEnd();
				}
				result = new ServerResponse(httpWebResponse.ResponseUri, httpWebResponse.StatusCode, httpWebResponse.ContentType, stopwatch.Elapsed, text, httpWebResponse.Headers.ToString());
			}
			return result;
		}

		// Token: 0x0400012F RID: 303
		private string userAgent;

		// Token: 0x04000130 RID: 304
		private string acceptLanguage;

		// Token: 0x04000131 RID: 305
		private ProcessCookies processCookies;

		// Token: 0x04000132 RID: 306
		private CookieContainer cookies;
	}
}
