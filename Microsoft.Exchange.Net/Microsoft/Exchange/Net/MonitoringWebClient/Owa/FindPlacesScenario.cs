using System;
using System.Security;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007DC RID: 2012
	internal class FindPlacesScenario : BaseTestStep
	{
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x0005A0BA File Offset: 0x000582BA
		// (set) Token: 0x060029DC RID: 10716 RVA: 0x0005A0C2 File Offset: 0x000582C2
		public Uri Uri { get; private set; }

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x0005A0CB File Offset: 0x000582CB
		// (set) Token: 0x060029DE RID: 10718 RVA: 0x0005A0D3 File Offset: 0x000582D3
		public string UserName { get; private set; }

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x0005A0DC File Offset: 0x000582DC
		// (set) Token: 0x060029E0 RID: 10720 RVA: 0x0005A0E4 File Offset: 0x000582E4
		public string UserDomain { get; private set; }

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x0005A0ED File Offset: 0x000582ED
		// (set) Token: 0x060029E2 RID: 10722 RVA: 0x0005A0F5 File Offset: 0x000582F5
		public SecureString Password { get; private set; }

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x0005A0FE File Offset: 0x000582FE
		// (set) Token: 0x060029E4 RID: 10724 RVA: 0x0005A106 File Offset: 0x00058306
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x0005A10F File Offset: 0x0005830F
		protected override TestId Id
		{
			get
			{
				return TestId.OwaFindPlacesScenario;
			}
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0005A112 File Offset: 0x00058312
		public FindPlacesScenario(Uri uri, string userName, string userDomain, SecureString password, ITestFactory factory)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.TestFactory = factory;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0005A13F File Offset: 0x0005833F
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x0005A164 File Offset: 0x00058364
		protected override void StartTest()
		{
			this.session.PersistentHeaders.Add("X-OWA-ActionName", "Monitoring");
			ITestStep testStep = this.TestFactory.CreateAuthenticateStep(this.Uri, this.UserName, this.UserDomain, this.Password, null, this.TestFactory);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x0005A1E8 File Offset: 0x000583E8
		private void AuthenticationCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			Uri uri = testStep.Result as Uri;
			if (uri != null)
			{
				this.Uri = uri;
			}
			ITestStep testStep2 = this.TestFactory.CreateOwaStartPageStep(this.Uri);
			testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.StartPageTestCompleted), tempResult);
			}, testStep2);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x0005A268 File Offset: 0x00058468
		private void StartPageTestCompleted(IAsyncResult result)
		{
			OwaStartPage owaStartPage = result.AsyncState as OwaStartPage;
			owaStartPage.EndExecute(result);
			string bodyFormat = string.Format("{{\"Query\":\"{0}\",\"Sources\":{1},\"MaxResults\":12,\"Culture\":\"en-US\"}}", "1 Microsoft Way, Redmond, WA, US", "2");
			RequestBody requestBody = RequestBody.Format(bodyFormat, new object[0]);
			ITestStep testStep = this.TestFactory.CreateOwaWebServiceStep(this.Uri, "FindPlaces", requestBody);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.FindPlacesLocationRequestCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x0005A2F4 File Offset: 0x000584F4
		private void FindPlacesLocationRequestCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			string bodyFormat = string.Format("{{\"Query\":\"{0}\",\"Sources\":{1},\"MaxResults\":12,\"Culture\":\"en-US\"}}", "Starbucks, Redmond, WA", "4");
			RequestBody requestBody = RequestBody.Format(bodyFormat, new object[0]);
			ITestStep testStep2 = this.TestFactory.CreateOwaWebServiceStep(this.Uri, "FindPlaces", requestBody);
			testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.FindPlacesPhonebookRequestCompleted), tempResult);
			}, testStep2);
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0005A368 File Offset: 0x00058568
		private void FindPlacesPhonebookRequestCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			this.KickoffLogoffStep();
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x0005A3A4 File Offset: 0x000585A4
		private void KickoffLogoffStep()
		{
			ITestStep testStep = this.TestFactory.CreateLogoffStep(this.Uri, "logoff.owa");
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LogOffStepCompleted), tempResult);
			}, testStep);
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x0005A3E4 File Offset: 0x000585E4
		private void LogOffStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024CB RID: 9419
		private const TestId ID = TestId.OwaFindPlacesScenario;

		// Token: 0x040024CC RID: 9420
		private const string LocationServices = "2";

		// Token: 0x040024CD RID: 9421
		private const string PhonebookServices = "4";

		// Token: 0x040024CE RID: 9422
		private const string FindPlacesRequestBody = "{{\"Query\":\"{0}\",\"Sources\":{1},\"MaxResults\":12,\"Culture\":\"en-US\"}}";
	}
}
