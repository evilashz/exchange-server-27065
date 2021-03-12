using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Net.MonitoringWebClient.Owa.Parsers;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007DA RID: 2010
	internal class OwaLogin : BaseTestStep
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x0005998B File Offset: 0x00057B8B
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x00059993 File Offset: 0x00057B93
		public Uri Uri { get; private set; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x0005999C File Offset: 0x00057B9C
		// (set) Token: 0x060029B0 RID: 10672 RVA: 0x000599A4 File Offset: 0x00057BA4
		public string UserName { get; private set; }

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000599AD File Offset: 0x00057BAD
		// (set) Token: 0x060029B2 RID: 10674 RVA: 0x000599B5 File Offset: 0x00057BB5
		public string UserDomain { get; private set; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x000599BE File Offset: 0x00057BBE
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x000599C6 File Offset: 0x00057BC6
		public SecureString Password { get; private set; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x000599CF File Offset: 0x00057BCF
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x000599D7 File Offset: 0x00057BD7
		public OwaLoginParameters OwaLoginParameters { get; private set; }

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x000599E0 File Offset: 0x00057BE0
		// (set) Token: 0x060029B8 RID: 10680 RVA: 0x000599E8 File Offset: 0x00057BE8
		public ITestFactory TestFactory { get; private set; }

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000599F1 File Offset: 0x00057BF1
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x000599F9 File Offset: 0x00057BF9
		public AuthenticationParameters AuthenticationParameters { get; set; }

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x00059A02 File Offset: 0x00057C02
		protected override TestId Id
		{
			get
			{
				return TestId.OwaLoginScenario;
			}
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x00059A05 File Offset: 0x00057C05
		public OwaLogin(Uri uri, string userName, string userDomain, SecureString password, OwaLoginParameters owaLoginParameters, ITestFactory factory)
		{
			this.Uri = uri;
			this.UserName = userName;
			this.UserDomain = userDomain;
			this.Password = password;
			this.OwaLoginParameters = owaLoginParameters;
			this.TestFactory = factory;
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00059A3A File Offset: 0x00057C3A
		protected override void Finally()
		{
			this.session.CloseConnections();
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00059A48 File Offset: 0x00057C48
		protected override void ExceptionThrown(ScenarioException e)
		{
			try
			{
				HttpWebResponseWrapperException ex = e.InnerException as HttpWebResponseWrapperException;
				if (e.FailureReason == FailureReason.CafeTimeoutContactingBackend && ex != null)
				{
					Uri uri = new Uri(ex.Request.RequestUri, "/owa/exhealth.reporttimeout");
					IAsyncResult asyncResult = this.session.BeginGet(this.Id, uri.ToString(), null, new Dictionary<string, object>
					{
						{
							"RequestTimeout",
							TimeSpan.FromSeconds(15.0)
						}
					});
					asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(20.0));
					this.session.EndGet<object>(asyncResult, null);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00059B2C File Offset: 0x00057D2C
		protected override void StartTest()
		{
			this.session.PersistentHeaders.Add("X-OWA-ActionName", "Monitoring");
			this.session.PersistentHeaders.Add("X-MonitoringInstance", ExMonitoringRequestTracker.Instance.MonitoringInstanceId);
			if (this.OwaLoginParameters.CafeOutboundRequestTimeout > TimeSpan.Zero)
			{
				this.session.PersistentHeaders.Add(WellKnownHeader.FrontEndToBackEndTimeout, this.OwaLoginParameters.CafeOutboundRequestTimeout.TotalSeconds.ToString());
			}
			if (this.AuthenticationParameters == null)
			{
				this.AuthenticationParameters = new AuthenticationParameters();
			}
			this.AuthenticationParameters.ShouldDownloadStaticFileOnLogonPage = this.OwaLoginParameters.ShouldDownloadStaticFileOnLogonPage;
			ITestStep testStep = this.TestFactory.CreateAuthenticateStep(this.Uri, this.UserName, this.UserDomain, this.Password, this.AuthenticationParameters, this.TestFactory);
			testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.AuthenticationCompleted), tempResult);
			}, testStep);
			if (this.OwaLoginParameters.ShouldMeasureClientLatency)
			{
				ITestStep testStep2 = this.TestFactory.CreateMeasureClientLatencyStep();
				testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.ClientLatencyMeasured), tempResult);
				}, testStep2);
			}
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00059C7C File Offset: 0x00057E7C
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

		// Token: 0x060029C1 RID: 10689 RVA: 0x00059CE4 File Offset: 0x00057EE4
		private void ClientLatencyMeasured(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00059D44 File Offset: 0x00057F44
		private void StartPageTestCompleted(IAsyncResult result)
		{
			OwaStartPage owaStartPage = result.AsyncState as OwaStartPage;
			owaStartPage.EndExecute(result);
			this.Uri = owaStartPage.Uri;
			this.startPage = owaStartPage.StartPage;
			if (this.startPage is Owa14StartPage)
			{
				ITestStep testStep = this.TestFactory.CreateOwaPingStep(this.Uri);
				testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.VersionSpecificTestCompleted), tempResult);
				}, testStep);
			}
			else
			{
				ITestStep testStep2 = this.TestFactory.CreateOwaWebServiceStep(this.Uri, "GetFolderMruConfiguration");
				testStep2.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.VersionSpecificTestCompleted), tempResult);
				}, testStep2);
			}
			if (this.OwaLoginParameters.ShouldDownloadStaticFile)
			{
				this.simultaneousStepCount = 2;
				ITestStep testStep3 = this.TestFactory.CreateOwaDownloadStaticFileStep(this.startPage.StaticFileUri);
				testStep3.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.DownloadStaticFileStepCompleted), tempResult);
				}, testStep3);
				return;
			}
			this.simultaneousStepCount = 1;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00059E58 File Offset: 0x00058058
		private void VersionSpecificTestCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			this.KickoffLogoffStep();
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00059E80 File Offset: 0x00058080
		private void DownloadStaticFileStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			this.KickoffLogoffStep();
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x00059EBC File Offset: 0x000580BC
		private void KickoffLogoffStep()
		{
			int num = Interlocked.Increment(ref this.currentFinishedStepCount);
			if (num >= this.simultaneousStepCount)
			{
				ITestStep testStep = this.TestFactory.CreateLogoffStep(this.Uri, "logoff.owa");
				testStep.BeginExecute(this.session, delegate(IAsyncResult tempResult)
				{
					base.AsyncCallbackWrapper(new AsyncCallback(this.LogOffStepCompleted), tempResult);
				}, testStep);
			}
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00059F18 File Offset: 0x00058118
		private void LogOffStepCompleted(IAsyncResult result)
		{
			ITestStep testStep = result.AsyncState as ITestStep;
			testStep.EndExecute(result);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024BC RID: 9404
		private const TestId ID = TestId.OwaLoginScenario;

		// Token: 0x040024BD RID: 9405
		private int simultaneousStepCount;

		// Token: 0x040024BE RID: 9406
		private int currentFinishedStepCount;

		// Token: 0x040024BF RID: 9407
		private OwaStartPage startPage;
	}
}
