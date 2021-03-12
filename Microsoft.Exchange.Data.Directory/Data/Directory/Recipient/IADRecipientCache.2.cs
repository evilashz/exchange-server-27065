using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001F9 RID: 505
	internal interface IADRecipientCache<TEntry> where TEntry : ADRawEntry, new()
	{
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001A4D RID: 6733
		OrganizationId OrganizationId { get; }

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001A4E RID: 6734
		IRecipientSession ADSession { get; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001A4F RID: 6735
		ReadOnlyCollection<ADPropertyDefinition> CachedADProperties { get; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001A50 RID: 6736
		ICollection<ProxyAddress> Keys { get; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001A51 RID: 6737
		ICollection<ProxyAddress> ClonedKeys { get; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001A52 RID: 6738
		ICollection<Result<TEntry>> Values { get; }

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001A53 RID: 6739
		int Count { get; }

		// Token: 0x06001A54 RID: 6740
		Result<TEntry> FindAndCacheRecipient(ProxyAddress proxyAddress);

		// Token: 0x06001A55 RID: 6741
		Result<TEntry> FindAndCacheRecipient(ADObjectId objectId);

		// Token: 0x06001A56 RID: 6742
		IList<Result<TEntry>> FindAndCacheRecipients(IList<ProxyAddress> proxyAddresses);

		// Token: 0x06001A57 RID: 6743
		void AddCacheEntry(ProxyAddress proxyAddress, Result<TEntry> result);

		// Token: 0x06001A58 RID: 6744
		bool ContainsKey(ProxyAddress proxyAddress);

		// Token: 0x06001A59 RID: 6745
		bool CopyEntryFrom(IADRecipientCache<TEntry> cacheToCopyFrom, string smtpAddress);

		// Token: 0x06001A5A RID: 6746
		bool CopyEntryFrom(IADRecipientCache<TEntry> cacheToCopyFrom, ProxyAddress proxyAddress);

		// Token: 0x06001A5B RID: 6747
		IEnumerable<TRecipient> ExpandGroup<TRecipient>(IADDistributionList group) where TRecipient : MiniRecipient, new();

		// Token: 0x06001A5C RID: 6748
		Result<TEntry> ReadSecurityDescriptor(ProxyAddress proxyAddress);

		// Token: 0x06001A5D RID: 6749
		void DropSecurityDescriptor(ProxyAddress proxyAddress);

		// Token: 0x06001A5E RID: 6750
		Result<TEntry> ReloadRecipient(ProxyAddress proxyAddress, IEnumerable<ADPropertyDefinition> extraProperties);

		// Token: 0x06001A5F RID: 6751
		bool Remove(ProxyAddress proxyAddress);

		// Token: 0x06001A60 RID: 6752
		bool TryGetValue(ProxyAddress proxyAddress, out Result<TEntry> result);
	}
}
