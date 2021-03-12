using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Items;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.Entities.TypeConversion.PropertyAccessors.StorageAccessors;
using Microsoft.Exchange.Entities.TypeConversion.PropertyTranslationRules;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.TypeConversion.Translators
{
	// Token: 0x02000085 RID: 133
	internal class ItemTranslator<TStoreItem, TItemEntity, TItemSchema> : StorageEntityTranslator<TStoreItem, TItemEntity, TItemSchema>, IGenericItemTranslator where TStoreItem : IItem where TItemEntity : Item<TItemSchema>, new() where TItemSchema : ItemSchema, new()
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x00009A81 File Offset: 0x00007C81
		protected ItemTranslator(IEnumerable<ITranslationRule<TStoreItem, TItemEntity>> additionalRules = null) : base(ItemTranslator<TStoreItem, TItemEntity, TItemSchema>.CreateTranslationRules().AddRules(additionalRules))
		{
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00009A94 File Offset: 0x00007C94
		public new static ItemTranslator<TStoreItem, TItemEntity, TItemSchema> Instance
		{
			get
			{
				return ItemTranslator<TStoreItem, TItemEntity, TItemSchema>.SingletonInstance;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009A9B File Offset: 0x00007C9B
		IItem IGenericItemTranslator.ConvertToEntity(IItem storageItem)
		{
			return base.ConvertToEntity((TStoreItem)((object)storageItem));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00009AB0 File Offset: 0x00007CB0
		private static List<ITranslationRule<TStoreItem, TItemEntity>> CreateTranslationRules()
		{
			return new List<ITranslationRule<TStoreItem, TItemEntity>>
			{
				ItemAccessors<TStoreItem>.Body.MapTo(Item<TItemSchema>.Accessors.Body),
				ItemAccessors<TStoreItem>.Preview.MapTo(Item<TItemSchema>.Accessors.Preview),
				ItemAccessors<TStoreItem>.ReceivedTime.MapTo(Item<TItemSchema>.Accessors.ReceivedTime),
				ItemAccessors<TStoreItem>.Categories.MapTo(Item<TItemSchema>.Accessors.Categories),
				ItemAccessors<TStoreItem>.HasAttachment.MapTo(Item<TItemSchema>.Accessors.HasAttachments),
				ItemAccessors<TStoreItem>.Importance.MapTo(Item<TItemSchema>.Accessors.Importance, default(ImportanceConverter)),
				ItemAccessors<TStoreItem>.Sensitivity.MapTo(Item<TItemSchema>.Accessors.Sensitivity, default(SensitivityConverter)),
				ItemAccessors<TStoreItem>.Subject.MapTo(Item<TItemSchema>.Accessors.Subject),
				ItemAccessors<TStoreItem>.LastModifiedTime.MapTo(Item<TItemSchema>.Accessors.LastModifiedTime),
				ItemAccessors<TStoreItem>.DateTimeCreated.MapTo(Item<TItemSchema>.Accessors.DateTimeCreated),
				new AttachmentTranslationRule<TStoreItem, TItemEntity, TItemSchema>()
			};
		}

		// Token: 0x0400010F RID: 271
		private static readonly ItemTranslator<TStoreItem, TItemEntity, TItemSchema> SingletonInstance = new ItemTranslator<TStoreItem, TItemEntity, TItemSchema>(null);
	}
}
