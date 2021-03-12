using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009B7 RID: 2487
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EwsStoreDataProviderCacheEntry
	{
		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06005BE2 RID: 23522 RVA: 0x0017F76B File Offset: 0x0017D96B
		public virtual bool AlternativeIdCacheEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x0017F76E File Offset: 0x0017D96E
		public bool TryGetItemId(string alternativeId, out ItemId itemId)
		{
			if (!this.AlternativeIdCacheEnabled)
			{
				itemId = null;
				return false;
			}
			return this.alternativeId2Id.TryGetValue(alternativeId, out itemId);
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x0017F78A File Offset: 0x0017D98A
		public void SetItemId(string alternativeId, ItemId itemId)
		{
			if (this.AlternativeIdCacheEnabled && itemId != null)
			{
				this.alternativeId2Id.Add(alternativeId, itemId);
			}
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x0017F7A4 File Offset: 0x0017D9A4
		public virtual void ClearItemCache(EwsStoreObject ewsStoreObject)
		{
			if (this.AlternativeIdCacheEnabled && !string.IsNullOrEmpty(ewsStoreObject.AlternativeId))
			{
				this.alternativeId2Id.Remove(ewsStoreObject.AlternativeId);
			}
		}

		// Token: 0x0400328A RID: 12938
		private MruDictionary<string, ItemId> alternativeId2Id = new MruDictionary<string, ItemId>(50, StringComparer.OrdinalIgnoreCase, null);

		// Token: 0x0400328B RID: 12939
		public string EwsUrl;

		// Token: 0x0400328C RID: 12940
		public int FailedCount;

		// Token: 0x0400328D RID: 12941
		public ExDateTime LastDiscoverTime = ExDateTime.MinValue;
	}
}
