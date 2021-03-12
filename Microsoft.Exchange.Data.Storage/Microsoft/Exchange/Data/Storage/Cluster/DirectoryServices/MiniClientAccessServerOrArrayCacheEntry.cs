using System;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x02000431 RID: 1073
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MiniClientAccessServerOrArrayCacheEntry
	{
		// Token: 0x06003000 RID: 12288 RVA: 0x000C5048 File Offset: 0x000C3248
		public MiniClientAccessServerOrArrayCacheEntry(IADMiniClientAccessServerOrArray miniClientAccessServerOrArray, TimeSpan timeToLive, TimeSpan timeToNegativeLive, TimeSpan timeToLiveMaximum)
		{
			this.m_timeRetrieved = DateTime.UtcNow;
			if (miniClientAccessServerOrArray != null)
			{
				this.m_timeToExpire = this.m_timeRetrieved.Add(timeToLive);
			}
			else
			{
				this.m_timeToExpire = this.m_timeRetrieved.Add(timeToNegativeLive);
			}
			this.m_maximumTimeToExpire = this.m_timeRetrieved.Add(timeToLiveMaximum);
			this.m_miniClientAccessServerOrArrayData = miniClientAccessServerOrArray;
		}

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000C50A9 File Offset: 0x000C32A9
		public DateTime TimeRetrieved
		{
			get
			{
				return this.m_timeRetrieved;
			}
		}

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06003002 RID: 12290 RVA: 0x000C50B1 File Offset: 0x000C32B1
		public DateTime TimeToExpire
		{
			get
			{
				return this.m_timeToExpire;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x000C50B9 File Offset: 0x000C32B9
		public DateTime MaximumTimeToExpire
		{
			get
			{
				return this.m_maximumTimeToExpire;
			}
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06003004 RID: 12292 RVA: 0x000C50C1 File Offset: 0x000C32C1
		public IADMiniClientAccessServerOrArray MiniClientAccessServerOrArrayData
		{
			get
			{
				return this.m_miniClientAccessServerOrArrayData;
			}
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000C50CC File Offset: 0x000C32CC
		public override string ToString()
		{
			string arg = (this.m_miniClientAccessServerOrArrayData == null) ? string.Empty : this.m_miniClientAccessServerOrArrayData.Name;
			return string.Format("[{0};exp={1:s},maxtime={2:s}]", arg, this.m_timeToExpire, this.m_maximumTimeToExpire);
		}

		// Token: 0x04001A24 RID: 6692
		private readonly DateTime m_timeRetrieved;

		// Token: 0x04001A25 RID: 6693
		private readonly DateTime m_timeToExpire;

		// Token: 0x04001A26 RID: 6694
		private readonly DateTime m_maximumTimeToExpire;

		// Token: 0x04001A27 RID: 6695
		private readonly IADMiniClientAccessServerOrArray m_miniClientAccessServerOrArrayData;
	}
}
