using System;
using Microsoft.Exchange.Entities.DataModel.PropertyBags;

namespace Microsoft.Exchange.Entities.DataModel.Items
{
	// Token: 0x0200008D RID: 141
	public sealed class ItemAttachment : Attachment<ItemAttachmentSchema>, IItemAttachment, IAttachment, IEntity, IPropertyChangeTracker<PropertyDefinition>
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00007086 File Offset: 0x00005286
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00007099 File Offset: 0x00005299
		public IItem Item
		{
			get
			{
				return base.GetPropertyValueOrDefault<IItem>(base.Schema.ItemProperty);
			}
			set
			{
				base.SetPropertyValue<IItem>(base.Schema.ItemProperty, value);
			}
		}

		// Token: 0x0200008E RID: 142
		public new static class Accessors
		{
			// Token: 0x040001D2 RID: 466
			public static readonly EntityPropertyAccessor<ItemAttachment, IItem> Item = new EntityPropertyAccessor<ItemAttachment, IItem>(SchematizedObject<ItemAttachmentSchema>.SchemaInstance.ItemProperty, (ItemAttachment itemAttachment) => itemAttachment.Item, delegate(ItemAttachment itemAttachment, IItem item)
			{
				itemAttachment.Item = item;
			});
		}
	}
}
