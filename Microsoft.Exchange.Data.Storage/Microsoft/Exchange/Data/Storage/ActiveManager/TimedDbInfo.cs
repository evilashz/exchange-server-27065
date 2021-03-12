using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActiveManager
{
	// Token: 0x020002F8 RID: 760
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TimedDbInfo
	{
		// Token: 0x0600227A RID: 8826 RVA: 0x0008A78C File Offset: 0x0008898C
		internal TimedDbInfo(DatabaseLocationInfo dbInfo)
		{
			this.m_dbInfo = dbInfo;
			this.m_negativeExpiringCounter = 0;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x0008A7A2 File Offset: 0x000889A2
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x0008A7AA File Offset: 0x000889AA
		internal int ExpiringCounter
		{
			get
			{
				return this.m_expiringCounter;
			}
			set
			{
				this.m_expiringCounter = value;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x0008A7B3 File Offset: 0x000889B3
		// (set) Token: 0x0600227E RID: 8830 RVA: 0x0008A7BB File Offset: 0x000889BB
		internal int NegativeExpiringCounter
		{
			get
			{
				return this.m_negativeExpiringCounter;
			}
			set
			{
				this.m_negativeExpiringCounter = value;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x0008A7C4 File Offset: 0x000889C4
		// (set) Token: 0x06002280 RID: 8832 RVA: 0x0008A7CC File Offset: 0x000889CC
		internal DatabaseLocationInfo DbLocationInfo
		{
			get
			{
				return this.m_dbInfo;
			}
			set
			{
				this.m_dbInfo = value;
				this.m_negativeExpiringCounter = 0;
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x0008A7DC File Offset: 0x000889DC
		internal void ResetExpiringCounter()
		{
			this.ExpiringCounter = 0;
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x0008A7E5 File Offset: 0x000889E5
		internal bool IsNegativeCacheExpired(int expiryThreshold)
		{
			return this.m_negativeExpiringCounter > expiryThreshold;
		}

		// Token: 0x06002283 RID: 8835 RVA: 0x0008A7F3 File Offset: 0x000889F3
		internal bool IsExpired(int expiryThreshold)
		{
			return this.ExpiringCounter > expiryThreshold;
		}

		// Token: 0x04001408 RID: 5128
		private int m_expiringCounter;

		// Token: 0x04001409 RID: 5129
		private int m_negativeExpiringCounter;

		// Token: 0x0400140A RID: 5130
		private DatabaseLocationInfo m_dbInfo;
	}
}
