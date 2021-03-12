using System;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x020005F6 RID: 1526
	internal class WSExecutionContext
	{
		// Token: 0x06003650 RID: 13904 RVA: 0x000E007A File Offset: 0x000DE27A
		public WSExecutionContext(TestCasConnectivity.TestCasConnectivityRunInstance instance, string mailboxFqdn)
		{
			this.instance = instance;
			this.mailboxFqdn = mailboxFqdn;
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x000E0090 File Offset: 0x000DE290
		public TestCasConnectivity.TestCasConnectivityRunInstance Instance
		{
			get
			{
				return this.instance;
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x000E0098 File Offset: 0x000DE298
		public string MailboxFqdn
		{
			get
			{
				return this.mailboxFqdn;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06003653 RID: 13907 RVA: 0x000E00A0 File Offset: 0x000DE2A0
		// (set) Token: 0x06003654 RID: 13908 RVA: 0x000E00A8 File Offset: 0x000DE2A8
		public ExchangeServiceBinding Esb
		{
			get
			{
				return this.esb;
			}
			set
			{
				this.esb = value;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x000E00B1 File Offset: 0x000DE2B1
		// (set) Token: 0x06003656 RID: 13910 RVA: 0x000E00B9 File Offset: 0x000DE2B9
		public string SyncState
		{
			get
			{
				return this.syncState;
			}
			set
			{
				this.syncState = value;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06003657 RID: 13911 RVA: 0x000E00C2 File Offset: 0x000DE2C2
		// (set) Token: 0x06003658 RID: 13912 RVA: 0x000E00CA File Offset: 0x000DE2CA
		public ItemIdType ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				this.itemId = value;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06003659 RID: 13913 RVA: 0x000E00D3 File Offset: 0x000DE2D3
		// (set) Token: 0x0600365A RID: 13914 RVA: 0x000E00DB File Offset: 0x000DE2DB
		public TimeSpan CreateItemLatency
		{
			get
			{
				return this.createItemLatency;
			}
			set
			{
				this.createItemLatency = value;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x0600365B RID: 13915 RVA: 0x000E00E4 File Offset: 0x000DE2E4
		// (set) Token: 0x0600365C RID: 13916 RVA: 0x000E00EC File Offset: 0x000DE2EC
		public bool End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
			}
		}

		// Token: 0x0400251C RID: 9500
		private TestCasConnectivity.TestCasConnectivityRunInstance instance;

		// Token: 0x0400251D RID: 9501
		private readonly string mailboxFqdn;

		// Token: 0x0400251E RID: 9502
		private ExchangeServiceBinding esb;

		// Token: 0x0400251F RID: 9503
		private string syncState;

		// Token: 0x04002520 RID: 9504
		private ItemIdType itemId;

		// Token: 0x04002521 RID: 9505
		private bool end;

		// Token: 0x04002522 RID: 9506
		private TimeSpan createItemLatency;
	}
}
