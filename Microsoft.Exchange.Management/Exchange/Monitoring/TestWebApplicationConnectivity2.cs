using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Security;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.MonitoringWebClient;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005CE RID: 1486
	public abstract class TestWebApplicationConnectivity2 : TestVirtualDirectoryConnectivity
	{
		// Token: 0x0600342A RID: 13354 RVA: 0x000D35B8 File Offset: 0x000D17B8
		internal TestWebApplicationConnectivity2(LocalizedString applicationName, LocalizedString applicationShortName, string monitoringEventSourceInternal, string monitoringEventSourceExternal) : base(applicationName, applicationShortName, null, monitoringEventSourceInternal, monitoringEventSourceExternal)
		{
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x0600342B RID: 13355 RVA: 0x000D35D1 File Offset: 0x000D17D1
		// (set) Token: 0x0600342C RID: 13356 RVA: 0x000D35F2 File Offset: 0x000D17F2
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public string URL
		{
			get
			{
				if (!(this.explicitlySetUrl == null))
				{
					return string.Empty;
				}
				return this.explicitlySetUrl.ToString();
			}
			set
			{
				this.explicitlySetUrl = new Uri(value);
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x0600342D RID: 13357 RVA: 0x000D3600 File Offset: 0x000D1800
		// (set) Token: 0x0600342E RID: 13358 RVA: 0x000D3608 File Offset: 0x000D1808
		[ValidateNotNull]
		[Parameter(Mandatory = false)]
		public PSCredential MailboxCredential
		{
			get
			{
				return this.explicitlySetMailboxCredential;
			}
			set
			{
				this.explicitlySetMailboxCredential = value;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x0600342F RID: 13359 RVA: 0x000D3611 File Offset: 0x000D1811
		// (set) Token: 0x06003430 RID: 13360 RVA: 0x000D361E File Offset: 0x000D181E
		[Parameter(Mandatory = false)]
		public SwitchParameter AllowUnsecureAccess
		{
			get
			{
				return this.allowUnsecureAccess;
			}
			set
			{
				this.allowUnsecureAccess = value;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003431 RID: 13361 RVA: 0x000D362C File Offset: 0x000D182C
		// (set) Token: 0x06003432 RID: 13362 RVA: 0x000D3680 File Offset: 0x000D1880
		[Parameter(Mandatory = false)]
		[ValidateRange(0, 360)]
		public int RequestTimeout
		{
			get
			{
				if (this.requestTimeout != null)
				{
					return (int)this.requestTimeout.Value.TotalSeconds;
				}
				if (base.TestType == OwaConnectivityTestType.Internal)
				{
					return (int)TestWebApplicationConnectivity2.DefaultInternalRequestTimeOut.TotalSeconds;
				}
				return (int)TestWebApplicationConnectivity2.DefaultExternalRequestTimeOut.TotalSeconds;
			}
			set
			{
				this.requestTimeout = new TimeSpan?(TimeSpan.FromSeconds((double)value));
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000D3694 File Offset: 0x000D1894
		protected override DatacenterUserType DefaultUserType
		{
			get
			{
				return DatacenterUserType.EDU;
			}
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x000D3698 File Offset: 0x000D1898
		protected override uint GetDefaultTimeOut()
		{
			if (base.TestType == OwaConnectivityTestType.Internal)
			{
				return (uint)TestWebApplicationConnectivity2.DefaultInternalTimeOut.TotalSeconds;
			}
			return (uint)TestWebApplicationConnectivity2.DefaultExternalTimeOut.TotalSeconds;
		}

		// Token: 0x06003435 RID: 13365 RVA: 0x000D36CC File Offset: 0x000D18CC
		protected sealed override List<CasTransactionOutcome> ExecuteTests(TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			TaskLogger.LogEnter();
			try
			{
				VirtualDirectoryUriScope virtualDirectoryUriScope;
				Uri testUri = this.GetTestUri(instance, out virtualDirectoryUriScope);
				base.WriteVerbose(Strings.CasHealthWebAppStartTest(testUri));
				IRequestAdapter requestAdapter = this.CreateRequestAdapter(virtualDirectoryUriScope);
				IExceptionAnalyzer exceptionAnalyzer = this.CreateExceptionAnalyzer(testUri);
				IResponseTracker responseTracker = this.CreateResponseTracker();
				IHttpSession session = this.CreateHttpSession(requestAdapter, exceptionAnalyzer, responseTracker, instance);
				this.HookupEventHandlers(session);
				CasTransactionOutcome casTransactionOutcome = this.CreateOutcome(instance, testUri, responseTracker);
				string userName;
				string domain;
				SecureString secureString;
				this.GetUserParameters(instance, out userName, out domain, out secureString);
				ITestStep testStep = this.CreateScenario(instance, testUri, userName, domain, secureString, virtualDirectoryUriScope, instance.CasFqdn);
				testStep.BeginExecute(session, new AsyncCallback(this.ScenarioExecutionFinished), new object[]
				{
					testStep,
					instance,
					responseTracker,
					casTransactionOutcome,
					secureString
				});
			}
			finally
			{
				TaskLogger.LogExit();
			}
			return null;
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x000D37A8 File Offset: 0x000D19A8
		private void ScenarioExecutionFinished(IAsyncResult result)
		{
			TaskLogger.LogEnter();
			object[] array = result.AsyncState as object[];
			ITestStep testStep = array[0] as ITestStep;
			TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance = array[1] as TestCasConnectivity.TestCasConnectivityRunInstance;
			IResponseTracker responseTracker = array[2] as IResponseTracker;
			CasTransactionOutcome casTransactionOutcome = array[3] as CasTransactionOutcome;
			SecureString secureString = array[4] as SecureString;
			try
			{
				testStep.EndExecute(result);
				this.CompleteSuccessfulOutcome(casTransactionOutcome, testCasConnectivityRunInstance, responseTracker);
			}
			catch (Exception ex)
			{
				testCasConnectivityRunInstance.Outcomes.Enqueue(TestWebApplicationConnectivity2.GenerateVerboseMessage(ex.ToString()));
				this.CompleteFailedOutcome(casTransactionOutcome, testCasConnectivityRunInstance, responseTracker, ex);
			}
			finally
			{
				TaskLogger.LogExit();
				if (secureString != null)
				{
					secureString.Dispose();
				}
				if (testCasConnectivityRunInstance != null)
				{
					testCasConnectivityRunInstance.Outcomes.Enqueue(casTransactionOutcome);
					testCasConnectivityRunInstance.Result.Complete();
				}
			}
		}

		// Token: 0x06003437 RID: 13367
		internal abstract IExceptionAnalyzer CreateExceptionAnalyzer(Uri testUri);

		// Token: 0x06003438 RID: 13368
		internal abstract ITestStep CreateScenario(TestCasConnectivity.TestCasConnectivityRunInstance instance, Uri testUri, string userName, string domain, SecureString password, VirtualDirectoryUriScope testType, string serverFqdn);

		// Token: 0x06003439 RID: 13369
		internal abstract CasTransactionOutcome CreateOutcome(TestCasConnectivity.TestCasConnectivityRunInstance instance, Uri testUri, IResponseTracker responseTracker);

		// Token: 0x0600343A RID: 13370
		internal abstract void CompleteSuccessfulOutcome(CasTransactionOutcome outcome, TestCasConnectivity.TestCasConnectivityRunInstance instance, IResponseTracker responseTracker);

		// Token: 0x0600343B RID: 13371
		internal abstract void CompleteFailedOutcome(CasTransactionOutcome outcome, TestCasConnectivity.TestCasConnectivityRunInstance instance, IResponseTracker responseTracker, Exception e);

		// Token: 0x0600343C RID: 13372 RVA: 0x000D3880 File Offset: 0x000D1A80
		internal virtual IRequestAdapter CreateRequestAdapter(VirtualDirectoryUriScope urlType)
		{
			return new RequestAdapter
			{
				RequestTimeout = TimeSpan.FromSeconds((double)this.RequestTimeout)
			};
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000D38A8 File Offset: 0x000D1AA8
		internal virtual IResponseTracker CreateResponseTracker()
		{
			return new ResponseTracker();
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000D38BC File Offset: 0x000D1ABC
		internal virtual IHttpSession CreateHttpSession(IRequestAdapter requestAdapter, IExceptionAnalyzer exceptionAnalyzer, IResponseTracker responseTracker, TestCasConnectivity.TestCasConnectivityRunInstance instance)
		{
			return new HttpSession(requestAdapter, exceptionAnalyzer, responseTracker)
			{
				SslValidationOptions = (this.trustAllCertificates ? SslValidationOptions.NoSslValidation : SslValidationOptions.BasicCertificateValidation),
				UserAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; MSEXCHMON; TESTCONN2)",
				EventState = instance
			};
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000D38F8 File Offset: 0x000D1AF8
		internal virtual void HookupEventHandlers(IHttpSession session)
		{
			session.SendingRequest += this.SendingRequest;
			session.ResponseReceived += this.ResponseReceived;
			session.TestStarted += this.TestStarted;
			session.TestFinished += this.TestFinished;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000D394D File Offset: 0x000D1B4D
		protected Uri GetTestUri(TestCasConnectivity.TestCasConnectivityRunInstance instance, out VirtualDirectoryUriScope uriType)
		{
			if (this.explicitlySetUrl != null)
			{
				uriType = VirtualDirectoryUriScope.Unknown;
				return this.explicitlySetUrl;
			}
			uriType = instance.UrlType;
			return instance.baseUri;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000D3978 File Offset: 0x000D1B78
		protected static LocalizedString GenerateVerboseMessage(string verboseMessage)
		{
			string value = string.Format("[{1}] : {0}", verboseMessage, ExDateTime.Now.ToString("HH:mm:ss.fff"));
			return new LocalizedString(value);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x000D39AC File Offset: 0x000D1BAC
		private void SendingRequest(object sender, HttpWebEventArgs e)
		{
			this.LogVerbose(e.EventState, TestWebApplicationConnectivity2.GenerateVerboseMessage(Strings.CasHealthWebAppSendingRequest(e.Request.RequestUri)));
			this.LogVerbose(e.EventState, TestWebApplicationConnectivity2.GenerateVerboseMessage(Environment.NewLine + Environment.NewLine + e.Request.ToStringNoBody() + Environment.NewLine));
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x000D3A10 File Offset: 0x000D1C10
		private void ResponseReceived(object sender, HttpWebEventArgs e)
		{
			this.LogVerbose(e.EventState, TestWebApplicationConnectivity2.GenerateVerboseMessage(Strings.CasHealthWebAppLiveIdResponseReceived(e.Request.RequestUri, e.Response.StatusCode, e.Response.RespondingFrontEndServer)));
			this.LogVerbose(e.EventState, TestWebApplicationConnectivity2.GenerateVerboseMessage(Environment.NewLine + Environment.NewLine + e.Response.ToStringNoBody() + Environment.NewLine));
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x000D3A89 File Offset: 0x000D1C89
		private void TestStarted(object sender, TestEventArgs e)
		{
			this.LogVerbose(e.EventState, TestWebApplicationConnectivity2.GenerateVerboseMessage(Strings.CasHealthWebAppTestStepStarted(e.TestId.ToString())));
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x000D3AB6 File Offset: 0x000D1CB6
		private void TestFinished(object sender, TestEventArgs e)
		{
			this.LogVerbose(e.EventState, TestWebApplicationConnectivity2.GenerateVerboseMessage(Strings.CasHealthWebAppTestStepFinished(e.TestId.ToString())));
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x000D3AE4 File Offset: 0x000D1CE4
		private void LogVerbose(object eventState, LocalizedString stringToTrace)
		{
			TestCasConnectivity.TestCasConnectivityRunInstance testCasConnectivityRunInstance = eventState as TestCasConnectivity.TestCasConnectivityRunInstance;
			testCasConnectivityRunInstance.Outcomes.Enqueue(stringToTrace);
			this.verboseOutput.Append(stringToTrace.ToString() + Environment.NewLine);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x000D3B2C File Offset: 0x000D1D2C
		private void GetUserParameters(TestCasConnectivity.TestCasConnectivityRunInstance instance, out string userName, out string userDomain, out SecureString password)
		{
			if (this.explicitlySetMailboxCredential != null)
			{
				userName = this.explicitlySetMailboxCredential.UserName;
				userDomain = null;
				password = this.explicitlySetMailboxCredential.Password.Copy();
				return;
			}
			userName = instance.credentials.UserName;
			userDomain = instance.credentials.Domain;
			password = instance.credentials.Password.ConvertToSecureString();
		}

		// Token: 0x0400241E RID: 9246
		protected const string UserAgent = "Mozilla/4.0 (compatible; MSIE 9.0; Windows NT 6.1; MSEXCHMON; TESTCONN2)";

		// Token: 0x0400241F RID: 9247
		protected const int EventIdSuccess = 1000;

		// Token: 0x04002420 RID: 9248
		protected const int EventIdFailure = 1001;

		// Token: 0x04002421 RID: 9249
		private static readonly TimeSpan DefaultExternalTimeOut = TimeSpan.FromMinutes(3.0);

		// Token: 0x04002422 RID: 9250
		private static readonly TimeSpan DefaultInternalTimeOut = TimeSpan.FromMinutes(1.0);

		// Token: 0x04002423 RID: 9251
		private static readonly TimeSpan DefaultExternalRequestTimeOut = TimeSpan.FromMinutes(2.0);

		// Token: 0x04002424 RID: 9252
		private static readonly TimeSpan DefaultInternalRequestTimeOut = TimeSpan.FromSeconds(30.0);

		// Token: 0x04002425 RID: 9253
		private TimeSpan? requestTimeout;

		// Token: 0x04002426 RID: 9254
		protected StringBuilder verboseOutput = new StringBuilder();

		// Token: 0x04002427 RID: 9255
		private Uri explicitlySetUrl;

		// Token: 0x04002428 RID: 9256
		private PSCredential explicitlySetMailboxCredential;
	}
}
