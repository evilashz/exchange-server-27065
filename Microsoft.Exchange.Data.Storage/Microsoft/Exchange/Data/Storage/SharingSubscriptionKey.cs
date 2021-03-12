using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DDD RID: 3549
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct SharingSubscriptionKey : IEquatable<SharingSubscriptionKey>
	{
		// Token: 0x06007A35 RID: 31285 RVA: 0x0021C825 File Offset: 0x0021AA25
		public SharingSubscriptionKey(string sharerIdentity, string remoteFolderId)
		{
			this.sharerIdentity = sharerIdentity;
			this.remoteFolderId = remoteFolderId;
		}

		// Token: 0x170020B0 RID: 8368
		// (get) Token: 0x06007A36 RID: 31286 RVA: 0x0021C835 File Offset: 0x0021AA35
		public string SharerIdentity
		{
			get
			{
				return this.sharerIdentity;
			}
		}

		// Token: 0x170020B1 RID: 8369
		// (get) Token: 0x06007A37 RID: 31287 RVA: 0x0021C83D File Offset: 0x0021AA3D
		public string RemoteFolderId
		{
			get
			{
				return this.remoteFolderId;
			}
		}

		// Token: 0x06007A38 RID: 31288 RVA: 0x0021C845 File Offset: 0x0021AA45
		public bool Equals(SharingSubscriptionKey other)
		{
			return StringComparer.InvariantCultureIgnoreCase.Equals(this.SharerIdentity, other.SharerIdentity) && StringComparer.InvariantCulture.Equals(this.RemoteFolderId, other.RemoteFolderId);
		}

		// Token: 0x06007A39 RID: 31289 RVA: 0x0021C879 File Offset: 0x0021AA79
		public override string ToString()
		{
			return this.SharerIdentity + ":" + this.RemoteFolderId;
		}

		// Token: 0x04005428 RID: 21544
		private string sharerIdentity;

		// Token: 0x04005429 RID: 21545
		private string remoteFolderId;
	}
}
