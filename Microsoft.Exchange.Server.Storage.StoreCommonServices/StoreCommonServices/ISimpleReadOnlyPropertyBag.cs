using System;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000089 RID: 137
	public interface ISimpleReadOnlyPropertyBag : ISimpleReadOnlyPropertyStorage
	{
		// Token: 0x060004F0 RID: 1264
		object GetPropertyValue(Context context, StorePropTag propertyTag);

		// Token: 0x060004F1 RID: 1265
		bool TryGetProperty(Context context, ushort propId, out StorePropTag propTag, out object value);

		// Token: 0x060004F2 RID: 1266
		void EnumerateProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue);
	}
}
