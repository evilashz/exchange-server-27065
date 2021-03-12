using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Rws
{
	// Token: 0x020007F4 RID: 2036
	internal class RwsCall : BaseTestStep
	{
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x0005D078 File Offset: 0x0005B278
		// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x0005D080 File Offset: 0x0005B280
		public Uri Uri { get; private set; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x0005D089 File Offset: 0x0005B289
		protected override TestId Id
		{
			get
			{
				return TestId.RwsCall;
			}
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0005D08D File Offset: 0x0005B28D
		public RwsCall(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x0005D0B1 File Offset: 0x0005B2B1
		protected override void StartTest()
		{
			this.session.BeginGetFollowingRedirections(this.Id, this.Uri.ToString(), delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.EndpointCallResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x0005D0DD File Offset: 0x0005B2DD
		private void EndpointCallResponseReceived(IAsyncResult result)
		{
			this.session.EndGet<object>(result, null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x04002551 RID: 9553
		private const TestId ID = TestId.RwsCall;
	}
}
