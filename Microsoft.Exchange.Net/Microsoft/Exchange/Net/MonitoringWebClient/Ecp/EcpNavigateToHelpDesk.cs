using System;
using System.Net;
using Microsoft.Exchange.Net.MonitoringWebClient.Ecp.Parsers;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007B2 RID: 1970
	internal class EcpNavigateToHelpDesk : BaseTestStep
	{
		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002808 RID: 10248 RVA: 0x00054AD2 File Offset: 0x00052CD2
		protected override TestId Id
		{
			get
			{
				return TestId.EcpNavigateToHelpDesk;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002809 RID: 10249 RVA: 0x00054AD6 File Offset: 0x00052CD6
		// (set) Token: 0x0600280A RID: 10250 RVA: 0x00054ADE File Offset: 0x00052CDE
		public Uri Uri { get; private set; }

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600280B RID: 10251 RVA: 0x00054AE7 File Offset: 0x00052CE7
		// (set) Token: 0x0600280C RID: 10252 RVA: 0x00054AEF File Offset: 0x00052CEF
		public string TargetMailbox { get; private set; }

		// Token: 0x0600280D RID: 10253 RVA: 0x00054AF8 File Offset: 0x00052CF8
		public EcpNavigateToHelpDesk(Uri uri, string targetMailbox, Func<Uri, ITestStep> getFollowingTestStep)
		{
			this.Uri = uri;
			this.TargetMailbox = targetMailbox;
			this.getFollowingTestStep = getFollowingTestStep;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x00054B2C File Offset: 0x00052D2C
		protected override void StartTest()
		{
			this.session.BeginGetFollowingRedirections(this.Id, new Uri(this.Uri, string.Format("/ecp/{0}/?exsvurl=1", this.TargetMailbox)).ToString(), delegate(IAsyncResult result)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.TargetOptionPageReceived), result);
			}, null);
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x00054BC4 File Offset: 0x00052DC4
		private void TargetOptionPageReceived(IAsyncResult result)
		{
			object obj = this.session.EndGetFollowingRedirections<object>(result, delegate(HttpWebResponseWrapper response)
			{
				LiveIdCompactTokenPage result2;
				if (LiveIdCompactTokenPage.TryParse(response, out result2))
				{
					return result2;
				}
				return EcpHelpDeskStartPage.Parse(response);
			});
			if (!(obj is EcpHelpDeskStartPage))
			{
				if (obj is LiveIdCompactTokenPage)
				{
					LiveIdCompactTokenPage liveIdCompactTokenPage = obj as LiveIdCompactTokenPage;
					this.session.BeginPost(this.Id, liveIdCompactTokenPage.PostUrl, RequestBody.Format(liveIdCompactTokenPage.HiddenFieldsString, new object[0]), "application/x-www-form-urlencoded", delegate(IAsyncResult resultTemp)
					{
						base.AsyncCallbackWrapper(new AsyncCallback(this.CompactTicketPostResponseRecived), resultTemp);
					}, null);
				}
				return;
			}
			EcpHelpDeskStartPage ecpHelpDeskStartPage = obj as EcpHelpDeskStartPage;
			this.Uri = ecpHelpDeskStartPage.FinalUri;
			ITestStep testStep = (this.getFollowingTestStep != null) ? this.getFollowingTestStep(this.Uri) : null;
			if (testStep != null)
			{
				testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.FollowingTestStepCompleted), tempResult);
				}, testStep);
				return;
			}
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x00054CB8 File Offset: 0x00052EB8
		private void FollowingTestStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x00054D24 File Offset: 0x00052F24
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
				base.AsyncCallbackWrapper(new AsyncCallback(this.TargetOptionPageReceived), tempResult);
			}, null);
		}

		// Token: 0x040023D6 RID: 9174
		private const TestId ID = TestId.EcpNavigateToHelpDesk;

		// Token: 0x040023D7 RID: 9175
		private Func<Uri, ITestStep> getFollowingTestStep;
	}
}
