using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Net.LiveIDAuthentication;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B31 RID: 2865
	internal sealed class WindowsLiveIdWebSession : WebSession
	{
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003DC2 RID: 15810 RVA: 0x000A0C5B File Offset: 0x0009EE5B
		// (set) Token: 0x06003DC3 RID: 15811 RVA: 0x000A0C63 File Offset: 0x0009EE63
		public bool FollowAllRedirects { get; set; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06003DC4 RID: 15812 RVA: 0x000A0C6C File Offset: 0x0009EE6C
		// (set) Token: 0x06003DC5 RID: 15813 RVA: 0x000A0C74 File Offset: 0x0009EE74
		public CookieCollection RPSCookies { get; private set; }

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000A0C80 File Offset: 0x0009EE80
		private string Organization
		{
			get
			{
				if (base.Credentials == null)
				{
					return null;
				}
				if (base.Credentials.UserName != null && base.Credentials.UserName.Contains("@"))
				{
					return base.Credentials.UserName.Substring(base.Credentials.UserName.IndexOf("@") + 1);
				}
				if (!string.IsNullOrEmpty(base.Credentials.Domain))
				{
					return base.Credentials.Domain;
				}
				return null;
			}
		}

		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x06003DC7 RID: 15815 RVA: 0x000A0D02 File Offset: 0x0009EF02
		// (set) Token: 0x06003DC8 RID: 15816 RVA: 0x000A0D0A File Offset: 0x0009EF0A
		private Uri BaseUrl { get; set; }

		// Token: 0x06003DC9 RID: 15817 RVA: 0x000A0D13 File Offset: 0x0009EF13
		public WindowsLiveIdWebSession(Uri loginUrl, NetworkCredential credentials, LiveIdAuthenticationConfiguration liveIdAuthenticationConfiguration) : base(loginUrl, credentials)
		{
			this.liveIdAuthenticationConfiguration = liveIdAuthenticationConfiguration;
			this.FollowAllRedirects = true;
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000A0D2B File Offset: 0x0009EF2B
		public WindowsLiveIdWebSession(Uri loginUrl, Uri baseUrl, NetworkCredential credentials, LiveIdAuthenticationConfiguration liveIdAuthenticationConfiguration) : base(loginUrl, credentials)
		{
			this.liveIdAuthenticationConfiguration = liveIdAuthenticationConfiguration;
			this.FollowAllRedirects = true;
			this.BaseUrl = baseUrl;
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x000A0D4B File Offset: 0x0009EF4B
		public WindowsLiveIdWebSession(Uri loginUrl, NetworkCredential credentials, Uri liveRSTEndpoint) : base(loginUrl, credentials)
		{
			this.liveIdAuthenticationConfiguration = new LiveIdAuthenticationConfiguration();
			this.liveIdAuthenticationConfiguration.LiveServiceLogin1Uri = liveRSTEndpoint;
			this.FollowAllRedirects = true;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x000A1100 File Offset: 0x0009F300
		public override void Initialize()
		{
			bool casUsingBusinessInstance = false;
			string text = null;
			string value = (this.liveIdAuthenticationConfiguration.MsoServiceLogin2Uri == null) ? null : this.liveIdAuthenticationConfiguration.MsoServiceLogin2Uri.Host;
			base.ServiceAuthority = new Uri(base.ServiceAuthority, string.Format("?targetName=www.{0}", this.Organization));
			int i = 0;
			while (i < 5)
			{
				var <>f__AnonymousType = base.Get(base.ServiceAuthority, (HttpWebResponse response) => new
				{
					Status = response.StatusCode,
					Redirect = ((response.Headers[HttpResponseHeader.Location] == null) ? null : new Uri(base.ServiceAuthority, response.Headers[HttpResponseHeader.Location]))
				});
				if (<>f__AnonymousType.Redirect == null || <>f__AnonymousType.Redirect.Host.Equals(this.liveIdAuthenticationConfiguration.LiveServiceLogin1Uri.Host, StringComparison.OrdinalIgnoreCase) || <>f__AnonymousType.Redirect.Host.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					if (<>f__AnonymousType.Redirect != null && <>f__AnonymousType.Redirect.Host.Equals(value, StringComparison.OrdinalIgnoreCase))
					{
						casUsingBusinessInstance = true;
					}
					text = this.ParseReturnUrl(<>f__AnonymousType.Redirect);
					if (text == null)
					{
						text = base.ServiceAuthority.ToString();
						break;
					}
					break;
				}
				else
				{
					base.ServiceAuthority = <>f__AnonymousType.Redirect;
					i++;
				}
			}
			string body = string.Empty;
			string value2 = string.Empty;
			string text2 = string.Empty;
			for (int j = 0; j < 50; j++)
			{
				if (string.IsNullOrEmpty(value2) || !base.ServiceAuthority.Host.EndsWith(value2))
				{
					if (!text.Contains("?") && !text.EndsWith("/"))
					{
						text += "/";
					}
					body = this.GetLiveIdSecurityToken(text, casUsingBusinessInstance);
					if (!text.Contains("wsignin"))
					{
						text += "?wa=wsignin1.0";
					}
					var <>f__AnonymousType2 = base.Post(new Uri(text), new StringBody(body), (HttpWebResponse response) => new
					{
						HasLiveIdCookie = (null != response.Cookies["RPSAuth"]),
						RPSAuthCookie = response.Cookies["RPSAuth"],
						RPSSecAuthCookie = response.Cookies["RPSSecAuth"],
						RedirectUrl = response.Headers[HttpResponseHeader.Location]
					});
					if (!<>f__AnonymousType2.HasLiveIdCookie)
					{
						throw new AuthenticationException();
					}
					this.RPSCookies = new CookieCollection();
					this.RPSCookies.Add(<>f__AnonymousType2.RPSAuthCookie);
					this.RPSCookies.Add(<>f__AnonymousType2.RPSSecAuthCookie);
					value2 = <>f__AnonymousType2.RPSAuthCookie.Domain.TrimStart(new char[]
					{
						'.'
					});
					text2 = <>f__AnonymousType2.RedirectUrl;
				}
				else
				{
					if (!this.FollowAllRedirects && this.RPSCookies != null && this.RPSCookies.Count == 2)
					{
						break;
					}
					text2 = base.Get<string>(base.ServiceAuthority, (HttpWebResponse getResponse) => getResponse.Headers[HttpResponseHeader.Location]);
				}
				if (string.IsNullOrEmpty(text2))
				{
					break;
				}
				base.ServiceAuthority = new Uri(base.ServiceAuthority, text2);
				if (base.ServiceAuthority.Host.Equals(this.liveIdAuthenticationConfiguration.LiveServiceLogin2Uri.Host, StringComparison.OrdinalIgnoreCase) || base.ServiceAuthority.Host.Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					throw new AuthenticationException();
				}
			}
			if (this.RPSCookies.Count < 2)
			{
				throw new AuthenticationException();
			}
			if (this.BaseUrl != null)
			{
				lock (base.SessionCookies)
				{
					base.SessionCookies = new WebSessionCookieContainer(this);
				}
				base.ServiceAuthority = this.BaseUrl;
				foreach (object obj in this.RPSCookies)
				{
					Cookie cookie = (Cookie)obj;
					base.AddCookie(new Cookie(cookie.Name, cookie.Value, "/"));
				}
			}
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x000A14DC File Offset: 0x0009F6DC
		protected override void Authenticate(HttpWebRequest request)
		{
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000A14E0 File Offset: 0x0009F6E0
		private string GetLiveIdSecurityToken(string serviceUrl, bool casUsingBusinessInstance)
		{
			if (!casUsingBusinessInstance)
			{
				return this.GetLiveIdCompactTicket(serviceUrl, "9b7d083a-7e93-4be0-8088-1e749d485544", "MBI_SSL", this.liveIdAuthenticationConfiguration.LiveServiceLogin1Uri);
			}
			HttpWebRequest httpWebRequest = base.CreateRequest(this.liveIdAuthenticationConfiguration.MsoGetUserRealmUri, "POST");
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			bool flag = base.Send<bool>(httpWebRequest, new StringBody(string.Format("login={0}&xml=1", base.Credentials.UserName)), new Func<HttpWebResponse, bool>(WindowsLiveIdWebSession.IsConsumerUser));
			if (flag)
			{
				string liveIdSamlTicket = this.GetLiveIdSamlTicket(this.liveIdAuthenticationConfiguration.MsoTokenIssuerUri.ToString(), "9b7d083a-7e93-4be0-8088-1e749d485544", "LBI_FED_STS_CLEAR", this.liveIdAuthenticationConfiguration.LiveServiceLogin2Uri);
				return this.GetLiveIdCompactTicketFromSaml(serviceUrl, "9b7d083a-7e93-4be0-8088-1e749d485544", "MBI_SSL", liveIdSamlTicket, this.liveIdAuthenticationConfiguration.MsoServiceLogin2Uri);
			}
			return this.GetLiveIdCompactTicketFromRst2(serviceUrl, "9b7d083a-7e93-4be0-8088-1e749d485544", "MBI_SSL", this.liveIdAuthenticationConfiguration.MsoServiceLogin2Uri);
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x000A15C8 File Offset: 0x0009F7C8
		private static bool IsConsumerUser(HttpWebResponse response)
		{
			bool result;
			using (Stream responseStream = response.GetResponseStream())
			{
				SafeXmlDocument safeXmlDocument = new SafeXmlDocument();
				safeXmlDocument.Load(responseStream);
				XmlNodeList elementsByTagName = safeXmlDocument.GetElementsByTagName("IsFederatedNS");
				if (elementsByTagName != null && elementsByTagName.Count > 0)
				{
					bool flag = false;
					if (elementsByTagName[0].InnerText != null && bool.TryParse(elementsByTagName[0].InnerText, out flag) && flag)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000A1680 File Offset: 0x0009F880
		private LiveIDAuthenticationClient CreateLiveIdAuthenticationClient(Uri targetUrl)
		{
			LiveIDAuthenticationClient liveIDAuthenticationClient = new LiveIDAuthenticationClient(targetUrl);
			liveIDAuthenticationClient.SendingRequest += delegate(object sender, HttpWebRequestEventArgs e)
			{
				base.SetupCertificateValidation(e.Request);
				this.OnSendingRequest(e.Request);
			};
			liveIDAuthenticationClient.ResponseReceived += delegate(object sender, HttpWebResponseEventArgs e)
			{
				this.OnResponseReceived(e.Request, e.Response);
			};
			return liveIDAuthenticationClient;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x000A16BC File Offset: 0x0009F8BC
		private string GetLiveIdCompactTicket(string serviceUrl, string clientApplicationID, string authenticationPolicy, Uri targetUrl)
		{
			string rawToken;
			using (LiveIDAuthenticationClient liveIDAuthenticationClient = this.CreateLiveIdAuthenticationClient(targetUrl))
			{
				AuthenticationResult authenticationResult;
				try
				{
					ICancelableAsyncResult asyncResult = liveIDAuthenticationClient.BeginGetToken(clientApplicationID, base.Credentials.UserName, base.Credentials.Password, authenticationPolicy, serviceUrl, null, null);
					authenticationResult = liveIDAuthenticationClient.EndGetToken(asyncResult);
				}
				catch (LocalizedException innerException)
				{
					throw new AuthenticationException(innerException);
				}
				if (authenticationResult.Exception != null)
				{
					if (authenticationResult.Exception is AuthenticationException)
					{
						throw authenticationResult.Exception;
					}
					throw new AuthenticationException(authenticationResult.Exception);
				}
				else
				{
					if (authenticationResult.Token == null || string.IsNullOrEmpty(authenticationResult.Token.RawToken))
					{
						throw new AuthenticationException();
					}
					rawToken = authenticationResult.Token.RawToken;
				}
			}
			return rawToken;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000A1788 File Offset: 0x0009F988
		private string GetLiveIdSamlTicket(string serviceUrl, string clientApplicationID, string authenticationPolicy, Uri targetUrl)
		{
			string assertionXml;
			using (LiveIDAuthenticationClient liveIDAuthenticationClient = this.CreateLiveIdAuthenticationClient(targetUrl))
			{
				AuthenticationResult authenticationResult;
				try
				{
					ICancelableAsyncResult asyncResult = liveIDAuthenticationClient.BeginGetRst2TicketFromCredentials(clientApplicationID, base.Credentials.UserName, base.Credentials.Password, authenticationPolicy, serviceUrl, null, null);
					authenticationResult = liveIDAuthenticationClient.EndGetRst2TicketFromCredentials(asyncResult);
				}
				catch (LocalizedException innerException)
				{
					throw new AuthenticationException(innerException);
				}
				if (authenticationResult.Exception != null)
				{
					if (authenticationResult.Exception is AuthenticationException)
					{
						throw authenticationResult.Exception;
					}
					throw new AuthenticationException(authenticationResult.Exception);
				}
				else
				{
					if (authenticationResult.SamlToken == null || string.IsNullOrEmpty(authenticationResult.SamlToken.AssertionXml))
					{
						throw new AuthenticationException();
					}
					assertionXml = authenticationResult.SamlToken.AssertionXml;
				}
			}
			return assertionXml;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000A1854 File Offset: 0x0009FA54
		private string GetLiveIdCompactTicketFromSaml(string serviceUrl, string clientApplicationID, string authenticationPolicy, string samlAssertion, Uri targetUrl)
		{
			string rawToken;
			using (LiveIDAuthenticationClient liveIDAuthenticationClient = this.CreateLiveIdAuthenticationClient(targetUrl))
			{
				AuthenticationResult authenticationResult;
				try
				{
					ICancelableAsyncResult asyncResult = liveIDAuthenticationClient.BeginGetRst2TicketFromSaml(clientApplicationID, samlAssertion, authenticationPolicy, serviceUrl, null, null);
					authenticationResult = liveIDAuthenticationClient.EndGetRst2TicketFromSaml(asyncResult);
				}
				catch (LocalizedException innerException)
				{
					throw new AuthenticationException(innerException);
				}
				if (authenticationResult.Exception != null)
				{
					if (authenticationResult.Exception is AuthenticationException)
					{
						throw authenticationResult.Exception;
					}
					throw new AuthenticationException(authenticationResult.Exception);
				}
				else
				{
					if (authenticationResult.Token == null || string.IsNullOrEmpty(authenticationResult.Token.RawToken))
					{
						throw new AuthenticationException();
					}
					rawToken = authenticationResult.Token.RawToken;
				}
			}
			return rawToken;
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000A190C File Offset: 0x0009FB0C
		private string GetLiveIdCompactTicketFromRst2(string serviceUrl, string clientApplicationID, string authenticationPolicy, Uri targetUrl)
		{
			string rawToken;
			using (LiveIDAuthenticationClient liveIDAuthenticationClient = this.CreateLiveIdAuthenticationClient(targetUrl))
			{
				AuthenticationResult authenticationResult;
				try
				{
					ICancelableAsyncResult asyncResult = liveIDAuthenticationClient.BeginGetRst2TicketFromCredentials(clientApplicationID, base.Credentials.UserName, base.Credentials.Password, authenticationPolicy, serviceUrl, null, null);
					authenticationResult = liveIDAuthenticationClient.EndGetRst2TicketFromCredentials(asyncResult);
				}
				catch (LocalizedException innerException)
				{
					throw new AuthenticationException(innerException);
				}
				if (authenticationResult.Exception != null)
				{
					if (authenticationResult.Exception is AuthenticationException)
					{
						throw authenticationResult.Exception;
					}
					throw new AuthenticationException(authenticationResult.Exception);
				}
				else
				{
					if (authenticationResult.Token == null || string.IsNullOrEmpty(authenticationResult.Token.RawToken))
					{
						throw new AuthenticationException();
					}
					rawToken = authenticationResult.Token.RawToken;
				}
			}
			return rawToken;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x000A19D8 File Offset: 0x0009FBD8
		private string ParseReturnUrl(Uri uri)
		{
			int num = uri.Query.IndexOf("wreply=", StringComparison.CurrentCultureIgnoreCase) + "wreply=".Length;
			if (num < 0)
			{
				return null;
			}
			int num2 = uri.Query.IndexOf("&", num, StringComparison.OrdinalIgnoreCase);
			return HttpUtility.UrlDecode((num2 > 0) ? uri.Query.Substring(num, num2 - num) : uri.Query.Substring(num));
		}

		// Token: 0x040035B4 RID: 13748
		private LiveIdAuthenticationConfiguration liveIdAuthenticationConfiguration;
	}
}
