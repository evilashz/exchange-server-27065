using System;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x0200042B RID: 1067
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class AdObjectCacheEntry<TADWrapperObject> where TADWrapperObject : class, IADObjectCommon
	{
		// Token: 0x06002FC0 RID: 12224 RVA: 0x000C3D58 File Offset: 0x000C1F58
		public AdObjectCacheEntry(TADWrapperObject adObjectType, TimeSpan timeToLive, TimeSpan timeToNegativeLive, TimeSpan timeToLiveMaximum)
		{
			this.m_timeRetrieved = DateTime.UtcNow;
			if (adObjectType != null)
			{
				this.m_timeToExpire = this.m_timeRetrieved.Add(timeToLive);
			}
			else
			{
				this.m_timeToExpire = this.m_timeRetrieved.Add(timeToNegativeLive);
			}
			this.m_maximumTimeToExpire = this.m_timeRetrieved.Add(timeToLiveMaximum);
			this.m_adObjectData = adObjectType;
		}

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000C3DBE File Offset: 0x000C1FBE
		public DateTime TimeRetrieved
		{
			get
			{
				return this.m_timeRetrieved;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000C3DC6 File Offset: 0x000C1FC6
		public DateTime TimeToExpire
		{
			get
			{
				return this.m_timeToExpire;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000C3DCE File Offset: 0x000C1FCE
		public DateTime MaximumTimeToExpire
		{
			get
			{
				return this.m_maximumTimeToExpire;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x000C3DD6 File Offset: 0x000C1FD6
		public TADWrapperObject AdObjectData
		{
			get
			{
				return this.m_adObjectData;
			}
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x000C3DE0 File Offset: 0x000C1FE0
		public override string ToString()
		{
			string text;
			if (this.m_adObjectData != null)
			{
				TADWrapperObject adObjectData = this.m_adObjectData;
				text = adObjectData.Name;
			}
			else
			{
				text = string.Empty;
			}
			string arg = text;
			return string.Format("[{0};exp={1:s},maxtime={2:s}]", arg, this.m_timeToExpire, this.m_maximumTimeToExpire);
		}

		// Token: 0x04001A0C RID: 6668
		private readonly DateTime m_timeRetrieved;

		// Token: 0x04001A0D RID: 6669
		private readonly DateTime m_timeToExpire;

		// Token: 0x04001A0E RID: 6670
		private readonly DateTime m_maximumTimeToExpire;

		// Token: 0x04001A0F RID: 6671
		private readonly TADWrapperObject m_adObjectData;
	}
}
