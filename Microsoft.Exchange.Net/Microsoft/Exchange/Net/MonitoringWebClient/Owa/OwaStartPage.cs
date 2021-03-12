using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007E1 RID: 2017
	internal class OwaStartPage : BaseTestStep
	{
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002A12 RID: 10770 RVA: 0x0005A695 File Offset: 0x00058895
		// (set) Token: 0x06002A13 RID: 10771 RVA: 0x0005A69D File Offset: 0x0005889D
		public Uri Uri { get; private set; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x0005A6A6 File Offset: 0x000588A6
		// (set) Token: 0x06002A15 RID: 10773 RVA: 0x0005A6AE File Offset: 0x000588AE
		public OwaStartPage StartPage { get; private set; }

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x0005A6B7 File Offset: 0x000588B7
		protected override TestId Id
		{
			get
			{
				return TestId.OwaStartPage;
			}
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x0005A6BB File Offset: 0x000588BB
		public OwaStartPage(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x0005A6E0 File Offset: 0x000588E0
		protected override void StartTest()
		{
			List<string> hostNames = this.session.GetHostNames(RequestTarget.LiveIdBusiness);
			foreach (string domain in hostNames)
			{
				Cookie cookie = new Cookie("MSPBack", "0", "/", domain);
				this.session.CookieContainer.Add(cookie);
			}
			this.session.BeginGetFollowingRedirections(this.Id, this.Uri.ToString(), RedirectionOptions.FollowUntilNo302OrSpecificRedirection, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.OwaResponseReceived), tempResult);
			}, new Dictionary<string, object>
			{
				{
					"LastExpectedRedirection",
					new string[]
					{
						"errorfe.aspx"
					}
				}
			});
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x0005A828 File Offset: 0x00058A28
		private void OwaResponseReceived(IAsyncResult result)
		{
			object obj = this.session.EndGetFollowingRedirections<object>(result, delegate(HttpWebResponseWrapper response)
			{
				List<string> hostNames = this.session.GetHostNames(RequestTarget.LiveIdBusiness);
				if (response.Request.RequestUri.ToString().IndexOf("login.srf", StringComparison.OrdinalIgnoreCase) >= 0 && hostNames.ContainsMatchingSuffix(response.Request.RequestUri.Host))
				{
					return response;
				}
				OwaLanguageSelectionPage result2;
				if (OwaLanguageSelectionPage.TryParse(response, out result2))
				{
					return result2;
				}
				return OwaStartPage.Parse(response);
			});
			if (!(obj is HttpWebResponseWrapper))
			{
				this.ParseOwaResponseReceived(obj);
				return;
			}
			HttpWebResponseWrapper httpWebResponseWrapper = obj as HttpWebResponseWrapper;
			string text = ParsingUtility.ParseFormAction(httpWebResponseWrapper);
			string text2 = null;
			Dictionary<string, string> dictionary = ParsingUtility.ParseHiddenFields(httpWebResponseWrapper);
			if (dictionary.Count > 0)
			{
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					if (text2 == null)
					{
						text2 = string.Format("{0}={1}", keyValuePair.Key, keyValuePair.Value);
					}
					else
					{
						text2 = string.Format("&{0}={1}", keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			if (text == null || text2 == null)
			{
				throw new MissingKeywordException(MonitoringWebClientStrings.MissingOwaStartPage(string.Format("{0},{1}", "Target URL", "POST data")), httpWebResponseWrapper.Request, httpWebResponseWrapper, "Single Namespace Auth");
			}
			this.session.BeginPostFollowingRedirections(this.Id, text, RequestBody.Format("{0}", new object[]
			{
				text2
			}), "application/x-www-form-urlencoded", null, RedirectionOptions.FollowUntilNo302OrSpecificRedirection, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.OwaFinalResponseReceived), tempResult);
			}, new Dictionary<string, object>
			{
				{
					"LastExpectedRedirection",
					new string[]
					{
						"errorfe.aspx"
					}
				}
			});
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x0005A9BC File Offset: 0x00058BBC
		private void OwaFinalResponseReceived(IAsyncResult result)
		{
			object resultingPage = this.session.EndPostFollowingRedirections<object>(result, delegate(HttpWebResponseWrapper response)
			{
				OwaLanguageSelectionPage result2;
				if (OwaLanguageSelectionPage.TryParse(response, out result2))
				{
					return result2;
				}
				return OwaStartPage.Parse(response);
			});
			this.ParseOwaResponseReceived(resultingPage);
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x0005AA10 File Offset: 0x00058C10
		private void ParseOwaResponseReceived(object resultingPage)
		{
			if (resultingPage is OwaStartPage)
			{
				this.StartPage = (resultingPage as OwaStartPage);
				if (this.StartPage.FinalUri != null && this.StartPage.FinalUri != this.Uri)
				{
					this.Uri = this.StartPage.FinalUri;
				}
				this.StoreCanary();
				base.ExecutionCompletedSuccessfully();
				return;
			}
			OwaLanguageSelectionPage owaLanguageSelectionPage = resultingPage as OwaLanguageSelectionPage;
			if (owaLanguageSelectionPage.FinalUri != null && owaLanguageSelectionPage.FinalUri != this.Uri)
			{
				this.Uri = new Uri(owaLanguageSelectionPage.FinalUri, this.Uri.PathAndQuery);
			}
			RequestBody body = RequestBody.Format("lcid={0}&tzid={1}", new object[]
			{
				RequestBody.RequestBodyItemWrapper.Create("1033", true),
				RequestBody.RequestBodyItemWrapper.Create("Pacific Standard Time", true)
			});
			this.session.BeginPost(this.Id, new Uri(this.Uri, owaLanguageSelectionPage.PostTarget).ToString(), body, "application/x-www-form-urlencoded", delegate(IAsyncResult resultTemp)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LanguageSelectionPostResponseReceived), resultTemp);
			}, null);
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x0005AB9C File Offset: 0x00058D9C
		private void LanguageSelectionPostResponseReceived(IAsyncResult result)
		{
			OwaStartPage startPage = null;
			bool flag = this.session.EndPost<bool>(result, delegate(HttpWebResponseWrapper response)
			{
				if (response.StatusCode == HttpStatusCode.Found)
				{
					return false;
				}
				if (response.StatusCode == HttpStatusCode.NotFound)
				{
					throw new PassiveDatabaseException(MonitoringWebClientStrings.PassiveDatabase, response.Request, response, "404 on language selection");
				}
				startPage = OwaStartPage.Parse(response);
				return true;
			});
			if (flag)
			{
				this.StartPage = startPage;
				this.StoreCanary();
				base.ExecutionCompletedSuccessfully();
				return;
			}
			this.session.BeginGetFollowingRedirections(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.OwaResponseReceivedAfterLanguagePost), tempResult);
			}, null);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x0005AC2B File Offset: 0x00058E2B
		private void OwaResponseReceivedAfterLanguagePost(IAsyncResult result)
		{
			this.StartPage = this.session.EndGetFollowingRedirections<OwaStartPage>(result, (HttpWebResponseWrapper response) => OwaStartPage.Parse(response));
			this.StoreCanary();
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x0005AC68 File Offset: 0x00058E68
		private void StoreCanary()
		{
			Cookie cookie = this.session.CookieContainer.GetCookies(this.Uri)["X-OWA-CANARY"];
			if (cookie != null)
			{
				this.session.PersistentHeaders.Add("x-owa-canary", cookie.Value);
			}
		}

		// Token: 0x040024DE RID: 9438
		private const TestId ID = TestId.OwaStartPage;
	}
}
