using System;
using System.Net;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x02000798 RID: 1944
	internal class EstablishAffinity : BaseTestStep
	{
		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x0005241C File Offset: 0x0005061C
		// (set) Token: 0x060026DA RID: 9946 RVA: 0x00052424 File Offset: 0x00050624
		public Uri Uri { get; private set; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x0005242D File Offset: 0x0005062D
		// (set) Token: 0x060026DC RID: 9948 RVA: 0x00052435 File Offset: 0x00050635
		public string ServerToHit { get; private set; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x0005243E File Offset: 0x0005063E
		protected override TestId Id
		{
			get
			{
				return TestId.EstablishAffinity;
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x00052442 File Offset: 0x00050642
		public EstablishAffinity(Uri uri, string serverToHit)
		{
			this.Uri = uri;
			this.ServerToHit = serverToHit;
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0005246D File Offset: 0x0005066D
		protected override void StartTest()
		{
			this.session.BeginGet(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.ResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00052648 File Offset: 0x00050848
		private void ResponseReceived(IAsyncResult result)
		{
			try
			{
				var <>f__AnonymousType = this.session.EndGet(result, Enum.GetValues(typeof(HttpStatusCode)) as HttpStatusCode[], (HttpWebResponseWrapper response) => new
				{
					StatusCode = response.StatusCode,
					RedirectUrl = response.Headers["Location"],
					RespondingServer = response.RespondingFrontEndServer
				});
				if (<>f__AnonymousType.RespondingServer != null && (<>f__AnonymousType.StatusCode == HttpStatusCode.OK || <>f__AnonymousType.StatusCode == HttpStatusCode.Found) && this.ServerToHit.StartsWith(<>f__AnonymousType.RespondingServer, StringComparison.OrdinalIgnoreCase))
				{
					base.ExecutionCompletedSuccessfully();
					return;
				}
			}
			catch (ScenarioException)
			{
			}
			this.responseCount++;
			if (this.responseCount >= 50)
			{
				throw new ScenarioException(MonitoringWebClientStrings.NoResponseFromServerThroughPublicName(this.ServerToHit, this.responseCount, this.Uri), null, RequestTarget.Owa, FailureReason.ServerUnreachable, FailingComponent.Networking, "AffinityFailure");
			}
			this.session.CookieContainer = new ExCookieContainer();
			this.session.CloseConnections();
			this.session.BeginGet(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.ResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x04002349 RID: 9033
		private const TestId ID = TestId.EstablishAffinity;

		// Token: 0x0400234A RID: 9034
		internal const int MaxRequests = 50;

		// Token: 0x0400234B RID: 9035
		private int responseCount;
	}
}
