using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Ecp
{
	// Token: 0x020007AD RID: 1965
	internal abstract class EcpWebServiceCallBase : BaseTestStep
	{
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x00054749 File Offset: 0x00052949
		// (set) Token: 0x060027E5 RID: 10213 RVA: 0x00054751 File Offset: 0x00052951
		public Uri Uri { get; private set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060027E6 RID: 10214
		protected abstract Uri WebServiceRelativeUri { get; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060027E7 RID: 10215
		protected abstract RequestBody RequestBody { get; }

		// Token: 0x060027E8 RID: 10216 RVA: 0x0005475A File Offset: 0x0005295A
		public EcpWebServiceCallBase(Uri uri, Func<Uri, ITestStep> getFollowingTestStep = null)
		{
			this.Uri = uri;
			this.getFollowingTestStep = getFollowingTestStep;
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x00054788 File Offset: 0x00052988
		protected override void StartTest()
		{
			string value = this.session.CookieContainer.GetCookies(this.Uri)["msExchEcpCanary"].Value;
			Uri baseUri = new Uri(this.Uri, this.WebServiceRelativeUri);
			Uri relativeUri = new Uri(string.Format("?msExchEcpCanary={0}", value), UriKind.Relative);
			Uri uri = new Uri(baseUri, relativeUri);
			this.session.BeginPost(this.Id, uri.ToString(), this.RequestBody, "application/json", delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.WebServiceResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0005482C File Offset: 0x00052A2C
		private void WebServiceResponseReceived(IAsyncResult result)
		{
			this.session.EndPost<object>(result, null);
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

		// Token: 0x060027EB RID: 10219 RVA: 0x00054890 File Offset: 0x00052A90
		private void FollowingTestStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040023CB RID: 9163
		private Func<Uri, ITestStep> getFollowingTestStep;
	}
}
