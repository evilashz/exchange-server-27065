using System;
using System.Net;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200057E RID: 1406
	internal class ActiveSyncState
	{
		// Token: 0x06003177 RID: 12663 RVA: 0x000C97B5 File Offset: 0x000C79B5
		internal ActiveSyncState(HttpWebRequest request, CasTransactionOutcome outcome)
		{
			this.request = request;
			this.outcome = outcome;
		}

		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x000C97CB File Offset: 0x000C79CB
		internal HttpWebRequest Request
		{
			get
			{
				return this.request;
			}
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x000C97D3 File Offset: 0x000C79D3
		// (set) Token: 0x0600317A RID: 12666 RVA: 0x000C97DB File Offset: 0x000C79DB
		internal HttpWebResponse Response
		{
			get
			{
				return this.response;
			}
			set
			{
				this.response = value;
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000C97E4 File Offset: 0x000C79E4
		internal CasTransactionOutcome Outcome
		{
			get
			{
				return this.outcome;
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000C97EC File Offset: 0x000C79EC
		// (set) Token: 0x0600317D RID: 12669 RVA: 0x000C97F4 File Offset: 0x000C79F4
		public ExDateTime StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x04002315 RID: 8981
		private HttpWebRequest request;

		// Token: 0x04002316 RID: 8982
		private HttpWebResponse response;

		// Token: 0x04002317 RID: 8983
		private CasTransactionOutcome outcome;

		// Token: 0x04002318 RID: 8984
		private ExDateTime startTime;
	}
}
