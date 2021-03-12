using System;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x02000436 RID: 1078
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MiniServerCacheEntry
	{
		// Token: 0x0600302E RID: 12334 RVA: 0x000C5BE0 File Offset: 0x000C3DE0
		public MiniServerCacheEntry(IADServer miniServer, TimeSpan timeToLive, TimeSpan timeToNegativeLive, TimeSpan timeToLiveMaximum)
		{
			this.m_timeRetrieved = DateTime.UtcNow;
			if (miniServer != null)
			{
				this.m_timeToExpire = this.m_timeRetrieved.Add(timeToLive);
			}
			else
			{
				this.m_timeToExpire = this.m_timeRetrieved.Add(timeToNegativeLive);
			}
			this.m_maximumTimeToExpire = this.m_timeRetrieved.Add(timeToLiveMaximum);
			this.m_miniServerData = miniServer;
		}

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x0600302F RID: 12335 RVA: 0x000C5C41 File Offset: 0x000C3E41
		public DateTime TimeRetrieved
		{
			get
			{
				return this.m_timeRetrieved;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x000C5C49 File Offset: 0x000C3E49
		public DateTime TimeToExpire
		{
			get
			{
				return this.m_timeToExpire;
			}
		}

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06003031 RID: 12337 RVA: 0x000C5C51 File Offset: 0x000C3E51
		public DateTime MaximumTimeToExpire
		{
			get
			{
				return this.m_maximumTimeToExpire;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000C5C59 File Offset: 0x000C3E59
		public IADServer MiniServerData
		{
			get
			{
				return this.m_miniServerData;
			}
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000C5C64 File Offset: 0x000C3E64
		public override string ToString()
		{
			string arg = (this.m_miniServerData == null) ? string.Empty : this.m_miniServerData.Name;
			return string.Format("[{0};exp={1:s},maxtime={2:s}]", arg, this.m_timeToExpire, this.m_maximumTimeToExpire);
		}

		// Token: 0x04001A39 RID: 6713
		private readonly DateTime m_timeRetrieved;

		// Token: 0x04001A3A RID: 6714
		private readonly DateTime m_timeToExpire;

		// Token: 0x04001A3B RID: 6715
		private readonly DateTime m_maximumTimeToExpire;

		// Token: 0x04001A3C RID: 6716
		private readonly IADServer m_miniServerData;
	}
}
