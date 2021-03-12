using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001F8 RID: 504
	internal interface IADRecipientCache : IEnumerable<KeyValuePair<ProxyAddress, Result<ADRawEntry>>>, IEnumerable
	{
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001A3B RID: 6715
		OrganizationId OrganizationId { get; }

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001A3C RID: 6716
		IRecipientSession ADSession { get; }

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001A3D RID: 6717
		ReadOnlyCollection<ADPropertyDefinition> CachedADProperties { get; }

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001A3E RID: 6718
		ICollection<ProxyAddress> Keys { get; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001A3F RID: 6719
		IEnumerable<Result<ADRawEntry>> Values { get; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001A40 RID: 6720
		int Count { get; }

		// Token: 0x06001A41 RID: 6721
		Result<ADRawEntry> FindAndCacheRecipient(ProxyAddress proxyAddress);

		// Token: 0x06001A42 RID: 6722
		Result<ADRawEntry> FindAndCacheRecipient(ADObjectId objectId);

		// Token: 0x06001A43 RID: 6723
		IList<Result<ADRawEntry>> FindAndCacheRecipients(IList<ProxyAddress> proxyAddresses);

		// Token: 0x06001A44 RID: 6724
		void AddCacheEntry(ProxyAddress proxyAddress, Result<ADRawEntry> result);

		// Token: 0x06001A45 RID: 6725
		bool ContainsKey(ProxyAddress proxyAddress);

		// Token: 0x06001A46 RID: 6726
		bool CopyEntryFrom(IADRecipientCache cacheToCopyFrom, string smtpAddress);

		// Token: 0x06001A47 RID: 6727
		bool CopyEntryFrom(IADRecipientCache cacheToCopyFrom, ProxyAddress proxyAddress);

		// Token: 0x06001A48 RID: 6728
		Result<ADRawEntry> ReadSecurityDescriptor(ProxyAddress proxyAddress);

		// Token: 0x06001A49 RID: 6729
		void DropSecurityDescriptor(ProxyAddress proxyAddress);

		// Token: 0x06001A4A RID: 6730
		Result<ADRawEntry> ReloadRecipient(ProxyAddress proxyAddress, IEnumerable<ADPropertyDefinition> extraProperties);

		// Token: 0x06001A4B RID: 6731
		bool Remove(ProxyAddress proxyAddress);

		// Token: 0x06001A4C RID: 6732
		bool TryGetValue(ProxyAddress proxyAddress, out Result<ADRawEntry> result);
	}
}
