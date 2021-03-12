using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000086 RID: 134
	public interface ISimpleReadOnlyPropertyStorage
	{
		// Token: 0x060004E1 RID: 1249
		object GetBlobPropertyValue(Context context, StorePropTag propTag);

		// Token: 0x060004E2 RID: 1250
		object GetPhysicalColumnValue(Context context, PhysicalColumn column);

		// Token: 0x060004E3 RID: 1251
		bool TryGetBlobProperty(Context context, ushort propId, out StorePropTag propTag, out object value);

		// Token: 0x060004E4 RID: 1252
		void EnumerateBlobProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue);
	}
}
