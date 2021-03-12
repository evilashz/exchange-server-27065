using System;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200008B RID: 139
	public interface ISimplePropertyBagWithChangeTracking : ISimplePropertyBag, ISimpleReadOnlyPropertyBag, ISimplePropertyStorageWithChangeTracking, ISimplePropertyStorage, ISimpleReadOnlyPropertyStorage
	{
		// Token: 0x060004F7 RID: 1271
		bool IsPropertyChanged(Context context, StorePropTag propTag);

		// Token: 0x060004F8 RID: 1272
		object GetOriginalPropertyValue(Context context, StorePropTag propTag);
	}
}
