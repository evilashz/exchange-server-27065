using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Web;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000796 RID: 1942
	internal class AdfsAuthentication : BaseTestStep
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x00051D40 File Offset: 0x0004FF40
		// (set) Token: 0x060026B3 RID: 9907 RVA: 0x00051D48 File Offset: 0x0004FF48
		public Uri Uri { get; private set; }

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x00051D51 File Offset: 0x0004FF51
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x00051D59 File Offset: 0x0004FF59
		public string UserName { get; private set; }

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x00051D62 File Offset: 0x0004FF62
		// (set) Token: 0x060026B7 RID: 9911 RVA: 0x00051D6A File Offset: 0x0004FF6A
		public string UserDomain { get; private set; }

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x00051D73 File Offset: 0x0004FF73
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x00051D7B File Offset: 0x0004FF7B
		public SecureString Password { get; private set; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x00051D84 File Offset: 0x0004FF84
		// (set) Token: 0x060026BB RID: 9915 RVA: 0x00051D8C File Offset: 0x0004FF8C
		public AdfsLogonPage AdfsLogonPage { get; private set; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x00051D95 File Offset: 0x0004FF95
		// (set) Token: 0x060026BD RID: 9917 RVA: 0x00051D9D File Offset: 0x0004FF9D
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x00051DA6 File Offset: 0x0004FFA6
		protected override TestId Id
		{
			get
			{
				return TestId.AdfsAuthentication;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x00051DAA File Offset: 0x0004FFAA
		// (set) Token: 0x060026C0 RID: 9920 RVA: 0x00051DB2 File Offset: 0x0004FFB2
		private protected LiveIdBasePage TicketPostPage { protected get; private set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x00051DBB File Offset: 0x0004FFBB
		public override object Result
		{
			get
			{
				return this.TicketPostPage;
			}
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x00051DC3 File Offset: 0x0004FFC3
		public AdfsAuthentication(Uri uri, string userName, string userDomain, SecureString password, AdfsLogonPage adfsLogonPage)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.AdfsLogonPage = adfsLogonPage;
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00051E08 File Offset: 0x00050008
		protected override void StartTest()
		{
			if (this.AdfsLogonPage.IsIntegratedAuthChallenge)
			{
				this.session.AuthenticationData = new AuthenticationData?(new AuthenticationData
				{
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential(this.UserName, this.Password)
				});
				this.session.BeginGetFollowingRedirections(this.Id, this.AdfsLogonPage.PostUri, RedirectionOptions.FollowUntilNo302, delegate(IAsyncResult resultTemp)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.AdfsCredentialPostResponseReceived), resultTemp);
				}, new Dictionary<string, object>
				{
					{
						"CredentialPostCount",
						10
					}
				});
				return;
			}
			this.AdfsLogonPage.HiddenFields.Remove(this.AdfsLogonPage.UserNameFieldName);
			this.AdfsLogonPage.HiddenFields.Remove(this.AdfsLogonPage.PasswordFieldName);
			this.PostCredentials(new int?(1));
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x00051F04 File Offset: 0x00050104
		private void PostCredentials(int? credentialPostCount)
		{
			RequestBody body = RequestBody.Format("{4}&{0}={1}&{2}={3}", new object[]
			{
				HttpUtility.UrlEncode(this.AdfsLogonPage.UserNameFieldName),
				HttpUtility.UrlEncode(this.UserName),
				HttpUtility.UrlEncode(this.AdfsLogonPage.PasswordFieldName),
				this.Password,
				this.AdfsLogonPage.HiddenFieldsString
			});
			Cookie cookie = new Cookie("CkTst", "G" + ExDateTime.Now.UtcTicks, "/", this.Uri.Host);
			this.session.CookieContainer.Add(cookie);
			this.session.BeginPostFollowingRedirections(this.Id, this.AdfsLogonPage.PostUri, body, "application/x-www-form-urlencoded", null, RedirectionOptions.FollowUntilNo302, delegate(IAsyncResult resultTemp)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AdfsCredentialPostResponseReceived), resultTemp);
			}, new Dictionary<string, object>
			{
				{
					"CredentialPostCount",
					credentialPostCount
				}
			});
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x00052058 File Offset: 0x00050258
		private void AdfsCredentialPostResponseReceived(IAsyncResult result)
		{
			Dictionary<string, object> dictionary = result.AsyncState as Dictionary<string, object>;
			int? credentialPostCount = dictionary["CredentialPostCount"] as int?;
			this.TicketPostPage = this.session.EndPostFollowingRedirections<LiveIdBasePage>(result, delegate(HttpWebResponseWrapper response)
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
			if (this.TicketPostPage == null)
			{
				this.PostCredentials(credentialPostCount + 1);
				return;
			}
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x04002339 RID: 9017
		private const TestId ID = TestId.AdfsAuthentication;
	}
}
