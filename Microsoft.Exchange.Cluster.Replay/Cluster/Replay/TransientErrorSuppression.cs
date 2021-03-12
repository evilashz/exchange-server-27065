using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000233 RID: 563
	internal abstract class TransientErrorSuppression<TKey>
	{
		// Token: 0x06001564 RID: 5476 RVA: 0x000555C8 File Offset: 0x000537C8
		protected TransientErrorSuppression()
		{
			this.InitializeTable();
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x000555D8 File Offset: 0x000537D8
		public void ReportSuccess(TKey key)
		{
			TransientErrorInfo existingOrNewErrorInfo = this.GetExistingOrNewErrorInfo(key);
			existingOrNewErrorInfo.ReportSuccess();
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x000555F4 File Offset: 0x000537F4
		public bool ReportSuccess(TKey key, TimeSpan suppressDuration)
		{
			TransientErrorInfo existingOrNewErrorInfo = this.GetExistingOrNewErrorInfo(key);
			return existingOrNewErrorInfo.ReportSuccess(suppressDuration);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00055610 File Offset: 0x00053810
		public bool ReportFailure(TKey key, TimeSpan suppressDuration)
		{
			TransientErrorInfo existingOrNewErrorInfo = this.GetExistingOrNewErrorInfo(key);
			return existingOrNewErrorInfo.ReportFailure(suppressDuration);
		}

		// Token: 0x06001568 RID: 5480
		protected abstract void InitializeTable();

		// Token: 0x06001569 RID: 5481 RVA: 0x0005562C File Offset: 0x0005382C
		private TransientErrorInfo GetExistingOrNewErrorInfo(TKey key)
		{
			TransientErrorInfo transientErrorInfo = null;
			if (!this.m_errorTable.TryGetValue(key, out transientErrorInfo))
			{
				transientErrorInfo = new TransientErrorInfo();
				this.m_errorTable[key] = transientErrorInfo;
			}
			return transientErrorInfo;
		}

		// Token: 0x04000868 RID: 2152
		protected Dictionary<TKey, TransientErrorInfo> m_errorTable;
	}
}
