using System;
using System.Net;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B16 RID: 2838
	internal sealed class FbaWebSession : WebSession
	{
		// Token: 0x06003D54 RID: 15700 RVA: 0x0009FEB4 File Offset: 0x0009E0B4
		public FbaWebSession(Uri loginUrl, NetworkCredential credentials) : base(loginUrl, credentials)
		{
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x0009FEE4 File Offset: 0x0009E0E4
		public override void Initialize()
		{
			base.ServiceAuthority.ToString();
			FbaWebSession.AuthenticationRedirectResponse authenticationRedirectResponse = base.Get<FbaWebSession.AuthenticationRedirectResponse>(base.ServiceAuthority, (HttpWebResponse response) => new FbaWebSession.AuthenticationRedirectResponse(response));
			FbaWebSession.FbaLogonPage fbaLogonPage = base.Get<FbaWebSession.FbaLogonPage>(new Uri(authenticationRedirectResponse.RedirectUrl), (HttpWebResponse response) => FbaWebSession.FbaLogonPage.Create(response, base.ServiceAuthority, base.Credentials));
			base.Post<FbaWebSession.AuthenticationRedirectResponse>(fbaLogonPage.FbaUrl, fbaLogonPage.PostForm, (HttpWebResponse response) => new FbaWebSession.AuthenticationRedirectResponse(response));
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x0009FF75 File Offset: 0x0009E175
		protected override void Authenticate(HttpWebRequest request)
		{
		}

		// Token: 0x02000B19 RID: 2841
		private abstract class FbaLogonPage : TextResponse
		{
			// Token: 0x06003D63 RID: 15715 RVA: 0x000A0018 File Offset: 0x0009E218
			public static FbaWebSession.FbaLogonPage Create(HttpWebResponse response, Uri appUrl, NetworkCredential credential)
			{
				FbaWebSession.FbaLogonPage fbaLogonPage = string.IsNullOrEmpty(response.Headers["X-OWA-Version"]) ? new FbaWebSession.IsaLogonPage() : new FbaWebSession.OwaLogonPage();
				fbaLogonPage.SetResponse(response);
				if (!fbaLogonPage.Contains("57A118C6-2DA9-419d-BE9A-F92B0F9A418B"))
				{
					throw new AuthenticationException();
				}
				fbaLogonPage.ServiceAuthority = appUrl;
				fbaLogonPage.Credential = credential;
				return fbaLogonPage;
			}

			// Token: 0x17000F28 RID: 3880
			// (get) Token: 0x06003D64 RID: 15716 RVA: 0x000A0074 File Offset: 0x0009E274
			public virtual HtmlFormBody PostForm
			{
				get
				{
					string text = this.Credential.UserName;
					if (text.Length > 20)
					{
						text = text.Substring(0, 20);
					}
					if (!string.IsNullOrEmpty(this.Credential.Domain))
					{
						text = this.Credential.Domain + "\\" + text;
					}
					return new HtmlFormBody
					{
						{
							"flags",
							"0"
						},
						{
							"username",
							text
						},
						{
							"password",
							this.Credential.Password
						}
					};
				}
			}

			// Token: 0x17000F29 RID: 3881
			// (get) Token: 0x06003D65 RID: 15717
			public abstract Uri FbaUrl { get; }

			// Token: 0x17000F2A RID: 3882
			// (get) Token: 0x06003D66 RID: 15718 RVA: 0x000A0103 File Offset: 0x0009E303
			// (set) Token: 0x06003D67 RID: 15719 RVA: 0x000A010B File Offset: 0x0009E30B
			private protected Uri ServiceAuthority { protected get; private set; }

			// Token: 0x17000F2B RID: 3883
			// (get) Token: 0x06003D68 RID: 15720 RVA: 0x000A0114 File Offset: 0x0009E314
			// (set) Token: 0x06003D69 RID: 15721 RVA: 0x000A011C File Offset: 0x0009E31C
			private protected NetworkCredential Credential { protected get; private set; }

			// Token: 0x0400358D RID: 13709
			private const string FbaLogonPageGuid = "57A118C6-2DA9-419d-BE9A-F92B0F9A418B";

			// Token: 0x0400358E RID: 13710
			private const string OwaVersionHeaderName = "X-OWA-Version";

			// Token: 0x0400358F RID: 13711
			private const int LogonUserNameMaxLength = 20;
		}

		// Token: 0x02000B1A RID: 2842
		private class OwaLogonPage : FbaWebSession.FbaLogonPage
		{
			// Token: 0x17000F2C RID: 3884
			// (get) Token: 0x06003D6B RID: 15723 RVA: 0x000A0130 File Offset: 0x0009E330
			public override HtmlFormBody PostForm
			{
				get
				{
					HtmlFormBody postForm = base.PostForm;
					postForm.Add("destination", base.ServiceAuthority.ToString());
					postForm.Add("trusted", "0");
					return postForm;
				}
			}

			// Token: 0x17000F2D RID: 3885
			// (get) Token: 0x06003D6C RID: 15724 RVA: 0x000A016B File Offset: 0x0009E36B
			public override Uri FbaUrl
			{
				get
				{
					return new Uri(base.ServiceAuthority, "/owa/auth/owaauth.dll");
				}
			}

			// Token: 0x04003592 RID: 13714
			private const string OwaFbaUrlPath = "/owa/auth/owaauth.dll";
		}

		// Token: 0x02000B1B RID: 2843
		private class IsaLogonPage : FbaWebSession.FbaLogonPage
		{
			// Token: 0x17000F2E RID: 3886
			// (get) Token: 0x06003D6E RID: 15726 RVA: 0x000A0188 File Offset: 0x0009E388
			public override HtmlFormBody PostForm
			{
				get
				{
					HtmlFormBody postForm = base.PostForm;
					postForm.Add("curl", base.ServiceAuthority.ToString());
					postForm.Add("trusted", "4");
					postForm.Add("formdir", "");
					postForm.Add("SubmitCreds", "Log On");
					return postForm;
				}
			}

			// Token: 0x17000F2F RID: 3887
			// (get) Token: 0x06003D6F RID: 15727 RVA: 0x000A01E4 File Offset: 0x0009E3E4
			public override Uri FbaUrl
			{
				get
				{
					return new UriBuilder(base.ServiceAuthority)
					{
						Path = "/CookieAuth.dll",
						Query = "Logon"
					}.Uri;
				}
			}

			// Token: 0x04003593 RID: 13715
			private const string IsaFbaUrlPath = "/CookieAuth.dll";

			// Token: 0x04003594 RID: 13716
			private const string IsaFbaUrlQuery = "Logon";
		}

		// Token: 0x02000B1D RID: 2845
		private class AuthenticationRedirectResponse : RedirectResponse
		{
			// Token: 0x06003D77 RID: 15735 RVA: 0x000A0274 File Offset: 0x0009E474
			public AuthenticationRedirectResponse(HttpWebResponse response) : base(response)
			{
				if (!base.IsRedirect)
				{
					throw new AuthenticationException();
				}
			}
		}
	}
}
