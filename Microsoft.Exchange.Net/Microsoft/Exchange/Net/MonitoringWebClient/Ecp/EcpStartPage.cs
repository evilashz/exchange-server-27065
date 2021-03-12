using System;
using System.Net;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp.Parsers;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007B5 RID: 1973
	internal class EcpStartPage : BaseTestStep
	{
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00054F53 File Offset: 0x00053153
		// (set) Token: 0x06002826 RID: 10278 RVA: 0x00054F5B File Offset: 0x0005315B
		public Uri Uri { get; private set; }

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x00054F64 File Offset: 0x00053164
		// (set) Token: 0x06002828 RID: 10280 RVA: 0x00054F6C File Offset: 0x0005316C
		public Uri CdnStaticFileUri { get; private set; }

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x00054F75 File Offset: 0x00053175
		protected override TestId Id
		{
			get
			{
				return TestId.EcpStartPage;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x00054F79 File Offset: 0x00053179
		public override object Result
		{
			get
			{
				return this.Uri;
			}
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x00054F81 File Offset: 0x00053181
		public EcpStartPage(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x0600282C RID: 10284 RVA: 0x00054FA5 File Offset: 0x000531A5
		protected override void StartTest()
		{
			this.session.BeginGetFollowingRedirections(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.EcpResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x0005502C File Offset: 0x0005322C
		private void EcpResponseReceived(IAsyncResult result)
		{
			object obj = this.session.EndGetFollowingRedirections<object>(result, delegate(HttpWebResponseWrapper response)
			{
				OwaLanguageSelectionPage result2;
				if (OwaLanguageSelectionPage.TryParse(response, out result2))
				{
					return result2;
				}
				LiveIdCompactTokenPage result3;
				if (LiveIdCompactTokenPage.TryParse(response, out result3))
				{
					return result3;
				}
				return EcpStartPage.Parse(response);
			});
			if (obj is EcpStartPage)
			{
				EcpStartPage ecpStartPage = obj as EcpStartPage;
				if (ecpStartPage.FinalUri != null && ecpStartPage.FinalUri != this.Uri)
				{
					this.Uri = ecpStartPage.FinalUri;
				}
				this.CdnStaticFileUri = ecpStartPage.StaticFileUri;
				base.ExecutionCompletedSuccessfully();
				return;
			}
			if (obj is LiveIdCompactTokenPage)
			{
				LiveIdCompactTokenPage liveIdCompactTokenPage = obj as LiveIdCompactTokenPage;
				this.session.BeginPost(this.Id, liveIdCompactTokenPage.PostUrl, RequestBody.Format(liveIdCompactTokenPage.HiddenFieldsString, new object[0]), "application/x-www-form-urlencoded", delegate(IAsyncResult resultTemp)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.CompactTicketPostResponseRecived), resultTemp);
				}, null);
				return;
			}
			OwaLanguageSelectionPage owaLanguageSelectionPage = obj as OwaLanguageSelectionPage;
			if (owaLanguageSelectionPage.FinalUri != null && owaLanguageSelectionPage.FinalUri != this.Uri)
			{
				this.Uri = owaLanguageSelectionPage.FinalUri;
			}
			RequestBody body = RequestBody.Format("lcid={0}&tzid={1}&destination={2}", new object[]
			{
				RequestBody.RequestBodyItemWrapper.Create("1033", true),
				RequestBody.RequestBodyItemWrapper.Create("Pacific Standard Time", true),
				RequestBody.RequestBodyItemWrapper.Create(owaLanguageSelectionPage.Destination, true)
			});
			this.session.BeginPost(this.Id, new Uri(this.Uri, owaLanguageSelectionPage.PostTarget).ToString(), body, "application/x-www-form-urlencoded", delegate(IAsyncResult resultTemp)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LanguageSelectionPostResponseReceived), resultTemp);
			}, null);
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x00055370 File Offset: 0x00053570
		private void LanguageSelectionPostResponseReceived(IAsyncResult result)
		{
			var <>f__AnonymousType = this.session.EndPost(result, (HttpWebResponseWrapper response) => new
			{
				StatusCode = response.StatusCode,
				Location = response.Headers["Location"],
				Response = response
			});
			if (<>f__AnonymousType.StatusCode == HttpStatusCode.Found)
			{
				this.session.BeginGet(this.Id, new Uri(this.Uri, <>f__AnonymousType.Location).ToString(), delegate(IAsyncResult resultTemp)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.LanguageSelectionPostAndRedirectedResponseReceived), resultTemp);
				}, null);
				return;
			}
			throw new ScenarioException(MonitoringWebClientStrings.NoRedirectToEcpAfterLanguageSelection, null, RequestTarget.Ecp, FailureReason.UnexpectedHttpResponseCode, FailingComponent.Ecp, "MissingStartPageAfterLanguageSelection");
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x0005540C File Offset: 0x0005360C
		private void LanguageSelectionPostAndRedirectedResponseReceived(IAsyncResult result)
		{
			EcpStartPage ecpStartPage = this.session.EndGet<EcpStartPage>(result, (HttpWebResponseWrapper response) => EcpStartPage.Parse(response));
			if (ecpStartPage.FinalUri != null && ecpStartPage.FinalUri != this.Uri)
			{
				this.Uri = ecpStartPage.FinalUri;
			}
			this.CdnStaticFileUri = ecpStartPage.StaticFileUri;
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000554C8 File Offset: 0x000536C8
		private void CompactTicketPostResponseRecived(IAsyncResult result)
		{
			Uri uri = this.session.EndPost<Uri>(result, delegate(HttpWebResponseWrapper response)
			{
				if (response.StatusCode != HttpStatusCode.Found)
				{
					return response.Request.RequestUri;
				}
				return new Uri(response.Headers["Location"]);
			});
			if (uri != null && uri != this.Uri)
			{
				this.Uri = uri;
			}
			this.session.BeginGetFollowingRedirections(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.EcpResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x040023E0 RID: 9184
		private const TestId ID = TestId.EcpStartPage;
	}
}
