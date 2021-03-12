using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x0200006F RID: 111
	internal class SiteGroupOfServersKey : GroupOfServersKey
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F778 File Offset: 0x0000D978
		public SiteGroupOfServersKey(ADObjectId siteId, int majorVersion)
		{
			this.siteId = siteId;
			this.majorVersion = majorVersion;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F790 File Offset: 0x0000D990
		public override bool Equals(object other)
		{
			SiteGroupOfServersKey siteGroupOfServersKey = other as SiteGroupOfServersKey;
			return siteGroupOfServersKey != null && this.siteId.Equals(siteGroupOfServersKey.siteId) && this.majorVersion == siteGroupOfServersKey.majorVersion;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F7D1 File Offset: 0x0000D9D1
		public override int GetHashCode()
		{
			return this.siteId.GetHashCode() + this.majorVersion;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000F7E5 File Offset: 0x0000D9E5
		public override string ToString()
		{
			return string.Format("{0}-[{1}]", this.siteId.ToString(), this.majorVersion);
		}

		// Token: 0x0400017A RID: 378
		private readonly ADObjectId siteId;

		// Token: 0x0400017B RID: 379
		private readonly int majorVersion;
	}
}
