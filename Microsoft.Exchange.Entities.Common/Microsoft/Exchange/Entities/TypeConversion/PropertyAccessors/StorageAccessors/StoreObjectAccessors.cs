using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors.StorageAccessors
{
	// Token: 0x02000070 RID: 112
	internal static class StoreObjectAccessors
	{
		// Token: 0x040000E5 RID: 229
		public static readonly IStoragePropertyAccessor<IStoreObject, VersionedId> IdAccessor = new DelegatedStoragePropertyAccessor<IStoreObject, VersionedId>(delegate(IStoreObject container, out VersionedId value)
		{
			value = container.Id;
			return value != null;
		}, null, null, null, new PropertyDefinition[]
		{
			ItemSchema.Id
		});

		// Token: 0x040000E6 RID: 230
		public static readonly IStoragePropertyAccessor<IStoreObject, string> ItemClassAccessor = new DefaultStoragePropertyAccessor<IStoreObject, string>(StoreObjectSchema.ItemClass, false);

		// Token: 0x040000E7 RID: 231
		public static readonly IStoragePropertyAccessor<ICalendarFolder, byte[]> RecordKey = new DefaultStoragePropertyAccessor<IStoreObject, byte[]>(StoreObjectSchema.RecordKey, false);
	}
}
