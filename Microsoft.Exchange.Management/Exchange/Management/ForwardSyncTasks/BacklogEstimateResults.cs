using System;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000362 RID: 866
	public class BacklogEstimateResults : BacklogEstimateBatch
	{
		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x00083507 File Offset: 0x00081707
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0008350F File Offset: 0x0008170F
		public TimeSpan ResponseTime { get; set; }

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x00083518 File Offset: 0x00081718
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x00083520 File Offset: 0x00081720
		public string RawResponse { get; set; }

		// Token: 0x06001E2D RID: 7725 RVA: 0x00083529 File Offset: 0x00081729
		public BacklogEstimateResults(BacklogEstimateBatch batch)
		{
			base.ContextBacklogs = batch.ContextBacklogs;
			base.NextPageToken = batch.NextPageToken;
			base.StatusCode = batch.StatusCode;
		}
	}
}
