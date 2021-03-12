using System;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000088 RID: 136
	public interface ISimplePropertyStorageWithChangeTracking : ISimplePropertyStorage, ISimpleReadOnlyPropertyStorage
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004E9 RID: 1257
		ISimpleReadOnlyPropertyBag OriginalBag { get; }

		// Token: 0x060004EA RID: 1258
		bool IsBlobPropertyChanged(Context context, StorePropTag propTag);

		// Token: 0x060004EB RID: 1259
		bool IsPhysicalColumnChanged(Context context, PhysicalColumn column);

		// Token: 0x060004EC RID: 1260
		object GetOriginalBlobPropertyValue(Context context, StorePropTag propTag);

		// Token: 0x060004ED RID: 1261
		bool TryGetOriginalBlobProperty(Context context, ushort propId, out StorePropTag propTag, out object value);

		// Token: 0x060004EE RID: 1262
		object GetOriginalPhysicalColumnValue(Context context, PhysicalColumn column);

		// Token: 0x060004EF RID: 1263
		void EnumerateOriginalBlobProperties(Context context, Func<StorePropTag, object, bool> action, bool showValue);
	}
}
