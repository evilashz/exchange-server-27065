using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors.StorageAccessors
{
	// Token: 0x0200006F RID: 111
	internal static class ItemAccessors<TItem> where TItem : IItem
	{
		// Token: 0x040000D8 RID: 216
		public static readonly IStoragePropertyAccessor<TItem, ItemBody> Body = new StorageBodyPropertyAccessor<TItem>();

		// Token: 0x040000D9 RID: 217
		public static readonly IStoragePropertyAccessor<TItem, List<string>> Categories = new DelegatedStoragePropertyAccessor<TItem, List<string>>(delegate(TItem container, out List<string> value)
		{
			value = new List<string>(container.Categories);
			return true;
		}, delegate(TItem item, List<string> categories)
		{
			item.Categories.Clear();
			item.Categories.AddRange(categories);
		}, delegate(IDictionary<PropertyDefinition, int> indices, IList values, out List<string> value)
		{
			int index;
			object obj;
			if (indices.TryGetValue(ItemSchema.Categories, out index) && (obj = values[index]) is string[])
			{
				value = new List<string>((string[])obj);
				value.Sort();
				return true;
			}
			value = null;
			return false;
		}, null, new PropertyDefinition[]
		{
			ItemSchema.Categories
		});

		// Token: 0x040000DA RID: 218
		public static readonly IStoragePropertyAccessor<TItem, bool> HasAttachment = new DefaultStoragePropertyAccessor<TItem, bool>(ItemSchema.HasAttachment, false);

		// Token: 0x040000DB RID: 219
		public static readonly IStoragePropertyAccessor<TItem, Microsoft.Exchange.Data.Storage.Importance> Importance = new DefaultStoragePropertyAccessor<TItem, Microsoft.Exchange.Data.Storage.Importance>(ItemSchema.Importance, false);

		// Token: 0x040000DC RID: 220
		public static readonly IStoragePropertyAccessor<TItem, ExDateTime> LastModifiedTime = new DefaultStoragePropertyAccessor<TItem, ExDateTime>(StoreObjectSchema.LastModifiedTime, false);

		// Token: 0x040000DD RID: 221
		public static readonly IStoragePropertyAccessor<TItem, ExDateTime> DateTimeCreated = new DefaultStoragePropertyAccessor<TItem, ExDateTime>(StoreObjectSchema.CreationTime, false);

		// Token: 0x040000DE RID: 222
		public static readonly IStoragePropertyAccessor<TItem, string> Preview = new DefaultStoragePropertyAccessor<TItem, string>(ItemSchema.Preview, false);

		// Token: 0x040000DF RID: 223
		public static readonly IStoragePropertyAccessor<TItem, ExDateTime> ReceivedTime = new DefaultStoragePropertyAccessor<TItem, ExDateTime>(ItemSchema.ReceivedTime, false);

		// Token: 0x040000E0 RID: 224
		public static readonly IStoragePropertyAccessor<TItem, Microsoft.Exchange.Data.Storage.Sensitivity> Sensitivity = new DefaultStoragePropertyAccessor<TItem, Microsoft.Exchange.Data.Storage.Sensitivity>(ItemSchema.Sensitivity, false);

		// Token: 0x040000E1 RID: 225
		public static readonly IStoragePropertyAccessor<TItem, string> Subject = new DefaultStoragePropertyAccessor<TItem, string>(ItemSchema.Subject, false);
	}
}
