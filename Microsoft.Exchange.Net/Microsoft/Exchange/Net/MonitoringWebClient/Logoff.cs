using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200079C RID: 1948
	internal class Logoff : BaseTestStep
	{
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x0005342E File Offset: 0x0005162E
		// (set) Token: 0x06002732 RID: 10034 RVA: 0x00053436 File Offset: 0x00051636
		public Uri BaseUri { get; private set; }

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x0005343F File Offset: 0x0005163F
		// (set) Token: 0x06002734 RID: 10036 RVA: 0x00053447 File Offset: 0x00051647
		public string LogoffPath { get; private set; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x00053450 File Offset: 0x00051650
		protected override TestId Id
		{
			get
			{
				return TestId.Logoff;
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x00053454 File Offset: 0x00051654
		public Logoff(Uri uri, string logoffPath)
		{
			this.BaseUri = uri;
			this.LogoffPath = logoffPath;
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x00053480 File Offset: 0x00051680
		protected override void StartTest()
		{
			this.session.BeginGetFollowingRedirections(this.Id, new Uri(this.BaseUri, this.LogoffPath).ToString(), RedirectionOptions.FollowUntilNo302OrSpecificRedirection, delegate(IAsyncResult tempResult)
			{
				base.AsyncCallbackWrapper(new AsyncCallback(this.LogoffResponseReceived), tempResult);
			}, new Dictionary<string, object>
			{
				{
					"LastExpectedRedirection",
					new string[]
					{
						"logoff.aspx",
						"logon.aspx",
						"signout.aspx"
					}
				}
			});
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x000534F4 File Offset: 0x000516F4
		private void LogoffResponseReceived(IAsyncResult result)
		{
			this.session.EndGetFollowingRedirections<object>(result, null);
			base.ExecutionCompletedSuccessfully();
		}

		// Token: 0x0400236C RID: 9068
		private const TestId ID = TestId.Logoff;
	}
}
