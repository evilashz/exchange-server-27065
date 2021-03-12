using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000070 RID: 112
	internal sealed class MailboxAccessPartnerInfoCacheKey
	{
		// Token: 0x06000306 RID: 774 RVA: 0x00013F66 File Offset: 0x00012166
		public MailboxAccessPartnerInfoCacheKey(ADObjectId adObjectId, OrganizationId organizationId)
		{
			this.ADObjectId = adObjectId;
			this.OrganizationId = organizationId;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00013F7C File Offset: 0x0001217C
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00013F84 File Offset: 0x00012184
		public ADObjectId ADObjectId { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00013F8D File Offset: 0x0001218D
		// (set) Token: 0x0600030A RID: 778 RVA: 0x00013F95 File Offset: 0x00012195
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x0600030B RID: 779 RVA: 0x00013FA0 File Offset: 0x000121A0
		public override bool Equals(object obj)
		{
			MailboxAccessPartnerInfoCacheKey mailboxAccessPartnerInfoCacheKey = obj as MailboxAccessPartnerInfoCacheKey;
			return mailboxAccessPartnerInfoCacheKey != null && mailboxAccessPartnerInfoCacheKey.ADObjectId.Equals(this.ADObjectId) && mailboxAccessPartnerInfoCacheKey.OrganizationId.Equals(this.OrganizationId);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00013FDF File Offset: 0x000121DF
		public override int GetHashCode()
		{
			return this.ADObjectId.GetHashCode();
		}
	}
}
