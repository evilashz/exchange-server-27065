using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200079B RID: 1947
	internal class LiveIdAuthentication : BaseTestStep
	{
		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x00052CFC File Offset: 0x00050EFC
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x00052D04 File Offset: 0x00050F04
		public Uri Uri { get; private set; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x0600270F RID: 9999 RVA: 0x00052D0D File Offset: 0x00050F0D
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x00052D15 File Offset: 0x00050F15
		public string UserName { get; private set; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002711 RID: 10001 RVA: 0x00052D1E File Offset: 0x00050F1E
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x00052D26 File Offset: 0x00050F26
		public string UserDomain { get; private set; }

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002713 RID: 10003 RVA: 0x00052D2F File Offset: 0x00050F2F
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x00052D37 File Offset: 0x00050F37
		public SecureString Password { get; private set; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002715 RID: 10005 RVA: 0x00052D40 File Offset: 0x00050F40
		// (set) Token: 0x06002716 RID: 10006 RVA: 0x00052D48 File Offset: 0x00050F48
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x00052D51 File Offset: 0x00050F51
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x00052D59 File Offset: 0x00050F59
		private string UserNameNoDomain { get; set; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x00052D62 File Offset: 0x00050F62
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x00052D6A File Offset: 0x00050F6A
		public AuthenticationParameters AuthenticationParameters { get; private set; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x00052D73 File Offset: 0x00050F73
		protected override TestId Id
		{
			get
			{
				return TestId.LiveIdAuthentication;
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x00052D77 File Offset: 0x00050F77
		public override object Result
		{
			get
			{
				return this.ticketPostUri;
			}
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x00052D7F File Offset: 0x00050F7F
		public LiveIdAuthentication(Uri uri, string userName, string userDomain, SecureString password, AuthenticationParameters authenticationParameters, ITestFactory factory)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.AuthenticationParameters = authenticationParameters;
			this.TestFactory = factory;
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x00052DB4 File Offset: 0x00050FB4
		protected override void StartTest()
		{
			string[] array = this.UserName.Split(new char[]
			{
				'@'
			});
			if (array.Length != 2)
			{
				throw new ArgumentException("Invalid user name: " + this.UserName);
			}
			this.UserNameNoDomain = array[0];
			this.UserDomain = array[1];
			this.BeginGetLiveIdLogonPage();
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x00052E24 File Offset: 0x00051024
		private void BeginGetLiveIdLogonPage()
		{
			Uri targetUri = this.GetTargetUri(this.UserDomain);
			this.session.BeginGetFollowingRedirections(this.Id, targetUri.ToString(), delegate(IAsyncResult result)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LiveIdLogonPageReceived), result);
			}, null);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x00052E64 File Offset: 0x00051064
		private Uri GetTargetUri(string domainName)
		{
			bool flag = this.Uri.ToString().IndexOf("sdfpilot.outlook.com", StringComparison.OrdinalIgnoreCase) >= 0 || this.Uri.ToString().IndexOf("outlook.office365.com", StringComparison.OrdinalIgnoreCase) >= 0;
			if (this.AuthenticationParameters != null && !this.AuthenticationParameters.ShouldUseTenantHintOnLiveIdLogon && !flag)
			{
				return this.Uri;
			}
			if (this.Uri.Segments.Length > 1 && this.Uri.Segments[1].TrimEnd(new char[]
			{
				'/'
			}).Equals("ecp", StringComparison.OrdinalIgnoreCase))
			{
				return new Uri(this.Uri, string.Format("?realm={0}", domainName));
			}
			Uri uri = new Uri(this.Uri, domainName);
			if (flag && uri.ToString().IndexOf("/owa/", StringComparison.OrdinalIgnoreCase) < 0)
			{
				return new Uri(this.Uri, string.Format("owa/{0}", domainName));
			}
			return uri;
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x00052FA4 File Offset: 0x000511A4
		private void LiveIdLogonPageReceived(IAsyncResult result)
		{
			LiveIdLogonPage liveIdLogonPage = this.session.EndGetFollowingRedirections<LiveIdLogonPage>(result, delegate(HttpWebResponseWrapper response)
			{
				AdfsLogonPage result2;
				if (AdfsLogonPage.TryParse(response, out result2))
				{
					return result2;
				}
				LiveIdLogonPage result3;
				Exception ex;
				if (LiveIdLogonPage.TryParse(response, out result3, out ex))
				{
					return result3;
				}
				if (ex != null && this.retryCount >= 1)
				{
					throw ex;
				}
				return null;
			});
			if (liveIdLogonPage == null)
			{
				this.retryCount++;
				this.BeginGetLiveIdLogonPage();
				return;
			}
			if (liveIdLogonPage is AdfsLogonPage)
			{
				ITestStep testStep = this.TestFactory.CreateAdfsAuthenticateStep(liveIdLogonPage.PostUri, this.UserName, this.UserDomain, this.Password, liveIdLogonPage as AdfsLogonPage);
				testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.AdfsAuthenticationStepFinished), tempResult);
				}, testStep);
				return;
			}
			this.PostCredentials(liveIdLogonPage, new int?(1));
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00053044 File Offset: 0x00051244
		private void AdfsAuthenticationStepFinished(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			LiveIdBasePage ticketPostPage = (LiveIdBasePage)testStep.Result;
			this.PostTicket(ticketPostPage);
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0005308C File Offset: 0x0005128C
		private void PostCredentials(LiveIdLogonPage liveIdLogonPage, int? credentialPostCount)
		{
			RequestBody body = RequestBody.Format("login={0}&passwd={1}&{2}&user_id={3}&password={4}", new object[]
			{
				this.UserName,
				this.Password,
				liveIdLogonPage.HiddenFieldsString,
				this.UserName,
				this.Password
			});
			Cookie cookie = new Cookie("CkTst", "G" + ExDateTime.Now.UtcTicks, "/", liveIdLogonPage.PostUri.Host);
			this.session.CookieContainer.Add(cookie);
			this.session.BeginPostFollowingRedirections(this.Id, liveIdLogonPage.PostUrl, body, "application/x-www-form-urlencoded", null, RedirectionOptions.FollowUntilNo302ExpectCrossDomainOnFirstRedirect, delegate(IAsyncResult resultTemp)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LiveIdCredentialPostResponseReceived), resultTemp);
			}, new Dictionary<string, object>
			{
				{
					"CredentialPostCount",
					credentialPostCount
				},
				{
					"LiveIdPage",
					liveIdLogonPage
				}
			});
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000531C8 File Offset: 0x000513C8
		private void LiveIdCredentialPostResponseReceived(IAsyncResult result)
		{
			Dictionary<string, object> dictionary = result.AsyncState as Dictionary<string, object>;
			int? credentialPostCount = dictionary["CredentialPostCount"] as int?;
			LiveIdLogonPage liveIdLogonPage = dictionary["LiveIdPage"] as LiveIdLogonPage;
			LiveIdBasePage liveIdBasePage = this.session.EndPostFollowingRedirections<LiveIdBasePage>(result, delegate(HttpWebResponseWrapper response)
			{
				if (credentialPostCount < 5 && response.StatusCode == HttpStatusCode.Found)
				{
					return null;
				}
				LiveIdSamlTokenPage result2;
				if (LiveIdSamlTokenPage.TryParse(response, out result2))
				{
					return result2;
				}
				return LiveIdCompactTokenPage.Parse(response);
			});
			if (liveIdBasePage == null)
			{
				this.PostCredentials(liveIdLogonPage, credentialPostCount + 1);
				return;
			}
			this.PostTicket(liveIdBasePage);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00053280 File Offset: 0x00051480
		private void PostTicket(LiveIdBasePage ticketPostPage)
		{
			if (ticketPostPage is LiveIdSamlTokenPage)
			{
				this.session.BeginPost(this.Id, ticketPostPage.PostUrl, RequestBody.Format(ticketPostPage.HiddenFieldsString, new object[0]), "application/x-www-form-urlencoded", delegate(IAsyncResult resultTemp)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.LiveIdSamlPostResponseReceived), resultTemp);
				}, null);
				return;
			}
			this.SendCompactTicketRequest(ticketPostPage);
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x000532E8 File Offset: 0x000514E8
		private void LiveIdSamlPostResponseReceived(IAsyncResult result)
		{
			LiveIdBasePage ticketPostPage = this.session.EndPost<LiveIdCompactTokenPage>(result, (HttpWebResponseWrapper response) => LiveIdCompactTokenPage.Parse(response));
			this.SendCompactTicketRequest(ticketPostPage);
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x0005333B File Offset: 0x0005153B
		private void SendCompactTicketRequest(LiveIdBasePage ticketPostPage)
		{
			this.session.BeginPost(this.Id, ticketPostPage.PostUrl, RequestBody.Format(ticketPostPage.HiddenFieldsString, new object[0]), "application/x-www-form-urlencoded", delegate(IAsyncResult resultTemp)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.CompactTicketPostResponseRecived), resultTemp);
			}, null);
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0005340D File Offset: 0x0005160D
		private void CompactTicketPostResponseRecived(IAsyncResult result)
		{
			this.session.EndPost<HttpStatusCode>(result, delegate(HttpWebResponseWrapper response)
			{
				if (response.Headers["Set-Cookie"] == null || response.Headers["Set-Cookie"].IndexOf("RPSAuth", StringComparison.OrdinalIgnoreCase) < 0)
				{
					throw new MissingKeywordException(MonitoringWebClientStrings.MissingLiveIdAuthCookies, response.Request, response, "RPSAuth cookie");
				}
				if (response.StatusCode == HttpStatusCode.Found)
				{
					this.ticketPostUri = new Uri(response.Headers["Location"]);
				}
				else
				{
					this.ticketPostUri = response.Request.RequestUri;
				}
				return response.StatusCode;
			});
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x04002360 RID: 9056
		private const TestId ID = TestId.LiveIdAuthentication;

		// Token: 0x04002361 RID: 9057
		private const int MaxRetryNumber = 1;

		// Token: 0x04002362 RID: 9058
		private Uri ticketPostUri;

		// Token: 0x04002363 RID: 9059
		private int retryCount;
	}
}
