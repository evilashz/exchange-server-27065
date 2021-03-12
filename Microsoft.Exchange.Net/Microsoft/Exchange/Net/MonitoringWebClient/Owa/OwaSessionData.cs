using System;

namespace Microsoft.Exchange.Net.MonitoringWebClient.Owa
{
	// Token: 0x020007E0 RID: 2016
	internal class OwaSessionData : BaseTestStep
	{
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002A0A RID: 10762 RVA: 0x0005A5EF File Offset: 0x000587EF
		// (set) Token: 0x06002A0B RID: 10763 RVA: 0x0005A5F7 File Offset: 0x000587F7
		public Uri Uri { get; private set; }

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002A0C RID: 10764 RVA: 0x0005A600 File Offset: 0x00058800
		protected override TestId Id
		{
			get
			{
				return TestId.OwaSessionData;
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0005A604 File Offset: 0x00058804
		public OwaSessionData(Uri uri)
		{
			this.Uri = uri;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x0005A628 File Offset: 0x00058828
		protected override void StartTest()
		{
			this.session.BeginPost(this.Id, new Uri(this.Uri, "sessiondata.ashx").ToString(), OwaSessionData.EmptyRequestBody, "application/x-www-form-urlencoded", delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.SessionDataResponseReceived), tempResult);
			}, null);
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x0005A668 File Offset: 0x00058868
		private void SessionDataResponseReceived(IAsyncResult result)
		{
			this.session.EndGet<object>(result, null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x040024DB RID: 9435
		private const TestId ID = TestId.OwaSessionData;

		// Token: 0x040024DC RID: 9436
		private static readonly RequestBody EmptyRequestBody = RequestBody.Format(string.Empty, new object[0]);
	}
}
